using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using YALCompiler;
using YALCompiler.DataTypes;
using YALCompiler.Helpers;

namespace Testing;

public class ASTUnitTests
{
    private static ASTNode Setup(string _string)
    {
        AntlrInputStream inputStream = new AntlrInputStream(_string);
        YALGrammerLexer speakLexer = new YALGrammerLexer(inputStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);
        YALGrammerParser speakParser = new YALGrammerParser(commonTokenStream);
        IParseTree tree = speakParser.program();
        YALGrammerVisitor visitor = new YALGrammerVisitor();
        ASTNode node = (ASTNode)visitor.Visit(tree);

        return node;
    }

    [Theory]
    [InlineData(@"external <""my_library""> print1: in (string _string);external <""my_library""> print2: in (string _string);", new string[] { "print1", "print2" })]
    [InlineData(@"external <""my_library2""> printhello1: in (string _string);external <""my_library3""> printhello2: in (string _string);", new string[] { "printhello1", "printhello2" })]
    public void Assert_External_Function_Declaration_Exists_In_Symbol_Table(string code, string[] expectedNames) {
        ASTNode node = Setup(code);

        foreach (string expectedName in expectedNames) {
            bool exists = node.FunctionTable.ContainsKey(expectedName);

            Assert.True(exists);
        }

    }
    
    [Theory]
    [InlineData("my_function: {}", "my_function")]
    public void Assert_Correct_Function_Name(string code, string functionName)
    {
        ASTNode node = Setup(code);
        
        Function func = (Function)node.Children[0];

        Assert.IsType<Function>(node.Children[0]);
        Assert.Equal(functionName, func.Name);
    }

    public static TheoryData<string, int> functionsData =>
        new () {
            { "", 0 },
            { String.Concat(Enumerable.Range(0, 15).Select(i => $"function{i}: {{}}")), 15 },
            { String.Concat(Enumerable.Range(0, 2).Select(i => $"function{i}: {{}}")), 2 }
        };

    [Theory]
    [MemberData(nameof(functionsData))]
    public void Should_Create_x_Functions(string code, int expectedFunctionsCount)
    {
        ASTNode node = Setup(code);

        Assert.Equal(expectedFunctionsCount, node.Children.Count);
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
    public void Assert_Correct_Function_Parameters_Count(string code, int expectedInputParametersCount, int expectedOutputParametersCount)
    {
        ASTNode node = Setup($"my_function: {code} {{}}");
        
        Function func = (Function)node.Children[0];
        
        Assert.Equal(expectedInputParametersCount, func.InputParameters.Count);
        Assert.Equal(expectedOutputParametersCount, func.OutputParameters.Count);
    }

    public static TheoryData<string, string, List<Types.ValueType>> ParametersData =>
        new () {
            { "in", "int32 a, bool b", new List<Types.ValueType> { Types.ValueType.int32, Types.ValueType.@bool } },
            { "out", "int32 a, bool b", new List<Types.ValueType> { Types.ValueType.int32, Types.ValueType.@bool } },
            { "in", "float32 c, float64 d", new List<Types.ValueType> { Types.ValueType.float32, Types.ValueType.float64 } },
            { "out", "float32 c, float64 d", new List<Types.ValueType> { Types.ValueType.float32, Types.ValueType.float64 } },
        };

    [Theory]
    [MemberData(nameof(ParametersData))]
    public void Assert_Correct_Parameter_Types(string type, string parameters, List<Types.ValueType> expectedParameterTypes)
    {
        ASTNode node = Setup($"my_function: {type} ({parameters}) {{ }}");

        Function func = (Function)node.Children[0];
        
        for (int i = 0; i < expectedParameterTypes.Count; i++) {
            Types.ValueType expectedType = expectedParameterTypes[i];
            Symbol param;

            switch (type) {
                case "in": 
                    param = (Symbol)func.InputParameters[i]; break;
                case "out":
                    param = (Symbol)func.OutputParameters[i]; break;
                default:
                    throw new Exception("Type must be either in or out");
            }

            Assert.Equal(expectedType ,((SingleType)param.Type!).Type);
        }
    }
    
    [Theory]
    [InlineData("my_function: { }", 0)]
    [InlineData("my_function: { int32 hej = 1+2; }", 1)]
    [InlineData("my_function: { int32 hej = 3+4; int32 hej2 = 4+5; }", 2)]
    [InlineData("my_function: { int32 hej = 5+6; int32 hej3 = 6+7; int32 hej4 = 7+8 }", 3)]
    public void Assert_Correct_Amount_Of_BlockStatement(string code, int expectedStatementsCount)
    {
        ASTNode node = Setup(code);
        
        Function func = (Function)node.Children[0];
        Assert.Equal(expectedStatementsCount, func.Children.Count);
    }

    [Theory]
    [InlineData("my_function: { hej++ }", typeof(UnaryAssignment))]
    [InlineData("my_function: { int32 hej = 5+2; }", typeof(BinaryAssignment))]
    [InlineData("my_function: { for (int32 i = 5; i < 5; i++) { } }", typeof(ForStatement))]
    [InlineData("my_function: { while (i < 5) { } }", typeof(WhileStatement))]
    [InlineData("my_function: { functionCall(); }", typeof(FunctionCall))]
    [InlineData("my_function: { int32 i; }", typeof(VariableDeclaration))]
    [InlineData("my_function: { i++; }", typeof(UnaryAssignment))]
    public void Assert_Correct_Statement_Type(string code, Type expectedStatementType)
    {
        ASTNode node = Setup(code);
        
        Function func = (Function)node.Children[0];
        ASTNode stmt = func.Children[0];

        Assert.Equal(expectedStatementType, stmt.GetType());
    }
}
