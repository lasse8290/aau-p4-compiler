using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using YALCompiler;
using YALCompiler.DataTypes;
using YALCompiler.Helpers;

namespace Testing;

public class ASTUnitTests
{
    YALGrammerVisitor visitor = new YALGrammerVisitor();

    private static YALGrammerParser Setup(string input)
    {
        AntlrInputStream inputStream = new AntlrInputStream(input);
        YALGrammerLexer lexer = new YALGrammerLexer(inputStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
        YALGrammerParser parser = new YALGrammerParser(commonTokenStream);

        return parser;
    }

    [Theory]
    [InlineData(@"external <""my_library""> print1: in (string _string);", "print1")]
    [InlineData(@"external <""hej""> print2: in (string _string);", "print2")]
    public void Assert_External_Function_Exists_In_Symbol_Table(string input, string expectedName) {
        YALGrammerParser parser = Setup(input);
        IParseTree tree = parser.externalFunctionDeclaration();
        ExternalFunction externalVarDcl = (ExternalFunction)visitor.Visit(tree);

        Assert.Equal(expectedName, externalVarDcl.Id);
    }
    
    [Theory]
    [InlineData("my_function: {}", "my_function")]
    public void Correct_Function_Name(string input, string functionName)
    {
        YALGrammerParser parser = Setup(input);
        IParseTree tree = parser.functionDeclaration();
        Function func = (Function)visitor.Visit(tree);

        Assert.IsType<Function>(func);
        Assert.Equal(functionName, func.Id);
    }
    
    public static TheoryData<string, int> functionsData =>
        new () {
            { "", 0 },
            { String.Concat(Enumerable.Range(0, 15).Select(i => $"function{i}: {{}}")), 15 },
            { String.Concat(Enumerable.Range(0, 2).Select(i => $"function{i}: {{}}")), 2 }
        };

    [Theory]
    [MemberData(nameof(functionsData))]
    public void Should_Create_x_Functions(string input, int expectedFunctionsCount)
    {
        YALGrammerParser parser = Setup(input);
        IParseTree tree = parser.program();
        ASTNode program = (ASTNode)visitor.Visit(tree);

        Assert.Equal(expectedFunctionsCount, program.Children.Count);
    }

    [Theory]
    [InlineData("", 0, 0)]
    [InlineData("in ()", 0, 0)]
    [InlineData("out: ()", 0, 0)]
    [InlineData("in () out: ()", 0, 0)]
    [InlineData("in () out: (string a)", 0, 1)]
    [InlineData("in (string a) out: ()", 1, 0)]
    [InlineData("in (int32 a, int32 b)", 2, 0)]
    [InlineData("in (int32 a, int32 b) out: (int32 c, int32 d)", 2, 2)]
    public void Assert_Correct_Function_Parameters_Count(string input, int expectedInputParametersCount, int expectedOutputParametersCount)
    {
        YALGrammerParser parser = Setup($"my_function: {input} {{}}");
        IParseTree tree = parser.functionDeclaration();
        Function func = (Function)visitor.Visit(tree);
        
        Assert.Equal(expectedInputParametersCount, func.InputParameters.Count);
        Assert.Equal(expectedOutputParametersCount, func.OutputParameters.Count);
    }

    public static TheoryData<string, List<Types.ValueType>> InputParametersData =>
        new () {
            { "(int32 a, bool b)", new List<Types.ValueType> { Types.ValueType.int32, Types.ValueType.@bool } },
            { "(float32 c, float64 d)", new List<Types.ValueType> { Types.ValueType.float32, Types.ValueType.float64 } },
        };

    [Theory]
    [MemberData(nameof(InputParametersData))]
    public void Assert_Input_Parameters(string parameters, List<Types.ValueType> expectedParameterTypes)
    {
        IParseTree tree = Setup(parameters).formalInputParams();
        List<Symbol> list = (List<Symbol>)visitor.Visit(tree);
        
        for (int i = 0; i < expectedParameterTypes.Count; i++) {
            Types.ValueType expectedType = expectedParameterTypes[i];
            Symbol param = (Symbol)list[i];

            Assert.Equal(expectedType, ((SingleType)param.Type!).Type);
        }
    }

    public static TheoryData<string, List<Types.ValueType>> OutputParametersData =>
        new () {
            { "out (int32 a, bool b)", new List<Types.ValueType> { Types.ValueType.int32, Types.ValueType.@bool } },
            { "out (float32 c, float64 d)", new List<Types.ValueType> { Types.ValueType.float32, Types.ValueType.float64 } },
        };
    [Theory]
    [MemberData(nameof(OutputParametersData))]
    public void Assert_Output_Parameters(string parameters, List<Types.ValueType> expectedParameterTypes)
    {
        IParseTree tree = Setup(parameters).formalOutputParams();
        List<Symbol> list = (List<Symbol>)visitor.Visit(tree);
        
        for (int i = 0; i < expectedParameterTypes.Count; i++) {
            Types.ValueType expectedType = expectedParameterTypes[i];
            Symbol param = (Symbol)list[i];

            Assert.Equal(expectedType, ((SingleType)param.Type!).Type);
        }
    }
    
    [Theory]
    [InlineData("my_function: { }", 0)]
    [InlineData("my_function: { int32 hej = 1+2; }", 1)]
    [InlineData("my_function: { int32 hej = 3+4; int32 hej2 = 4+5; }", 2)]
    [InlineData("my_function: { int32 hej = 5+6; int32 hej3 = 6+7; int32 hej4 = 7+8 }", 3)]
    public void Assert_Correct_Amount_Of_BlockStatement(string input, int expectedStatementsCount)
    {
        YALGrammerParser parser = Setup(input);
        IParseTree tree = parser.functionDeclaration();
        Function func = (Function)visitor.Visit(tree);
        
        Assert.Equal(expectedStatementsCount, func.Children.Count);
    }

    [Theory]
    [InlineData("int32 hej = 5+2;", typeof(BinaryAssignment))]
    [InlineData("functionCall();", typeof(FunctionCall))]
    [InlineData("int32 i;", typeof(VariableDeclaration))]
    [InlineData("i++;", typeof(UnaryAssignment))]
    public void Assert_Correct_SingleStatement(string input, Type expectedStatementType)
    {
        YALGrammerParser parser = Setup(input);
        IParseTree tree = parser.singleStatement();
        var stmt = visitor.Visit(tree);

        Assert.Equal(expectedStatementType, stmt.GetType());
    }

    [Theory]
    [InlineData("for (int32 i = 5; i < 5; i++) { }", typeof(ForStatement))]
    [InlineData("while (i < 5) { }", typeof(WhileStatement))]
    [InlineData("if (i < 5) { }", typeof(IfStatement))]
    public void Assert_Correct_BlockStatement(string input, Type expectedStatementType)
    {
        YALGrammerParser parser = Setup(input);
        IParseTree tree = parser.blockStatement();
        var stmt = visitor.Visit(tree);

        Assert.Equal(expectedStatementType, stmt.GetType());
    }
}
