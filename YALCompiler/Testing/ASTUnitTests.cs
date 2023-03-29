using System.Collections;
using Antlr4.Runtime.Tree;
using Iced.Intel;
using YALCompiler;
using YALCompiler.DataTypes;
using YALCompiler.ErrorHandlers;
using YALCompiler.Helpers;

namespace Testing;
using Antlr4.Runtime;

public class UnitTest1
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
    [InlineData("my_function: {}", "my_function")]
    public void Assert_Correct_Function_Name(string code, string functionName)
    {
        ASTNode node = Setup(code);
        
        Function func = (Function)node.Children[0];

        Assert.IsType<Function>(node.Children[0]);
        Assert.Equal(functionName, func.Id);
    }
    
    public static IEnumerable<object[]> Functions =>
        new List<object[]>
        {
            new object[] { "", 0 },
            new object[] { String.Concat(Enumerable.Range(0, 100).Select(i => $"function{i}: {{}}")), 100 },
            new object[] { String.Concat(Enumerable.Range(0, 10).Select(i => $"function{i}: {{}}")), 10 },
        };

    [Theory]
    [MemberData(nameof(Functions))]
    public void Should_Create_x_Functions(string code, int expectedFunctionsCount)
    {
        ASTNode node = Setup(code);

        Assert.Equal(expectedFunctionsCount, node.Children.Count);
    }

    [Theory]
    [InlineData("my_function: {}", 0, 0)]
    [InlineData("my_function: in: () {}", 0, 0)]
    [InlineData("my_function: out: () {}", 0, 0)]
    [InlineData("my_function: in: () out: () {}", 0, 0)]
    [InlineData("my_function: in: () out: (string a) {}", 0, 1)]
    [InlineData("my_function: in: (string a) out: () {}", 1, 0)]
    [InlineData("my_function: in: (int32 a, int32 b) {}", 2, 0)]
    [InlineData("my_function: in: (int32 a, int32 b) out: (int32 c, int32 d) {}", 2, 2)]
    public void Assert_Correct_Function_Parameters_Count(string code, int expectedInputParametersCount, int expectedOutputParametersCount)
    {
        ASTNode node = Setup(code);
        
        Function func = (Function)node.Children[0];
        
        Assert.Equal(expectedInputParametersCount, func.InputParameters.Count);
        Assert.Equal(expectedOutputParametersCount, func.OutputParameters.Count);
    }

    [Fact]
    public void Assert_Correct_Input_Parameters()
    {
        ASTNode node = Setup("my_function: in (int32 a, string b) {}");
        
        Function func = (Function)node.Children[0];
        Symbol param1 = (Symbol)func.InputParameters[0];
        Symbol param2 = (Symbol)func.InputParameters[1];

        Assert.Equal("a", param1.Id);
        Assert.IsType<SingleType>(param1.Type);
        Assert.Equal(Types.ValueType.int32 ,((SingleType)param1.Type).Type);
        
        Assert.Equal("b", param2.Id);
        Assert.IsType<SingleType>(param2.Type);
        Assert.Equal(Types.ValueType.@string ,((SingleType)param2.Type).Type);
    }
    
    [Fact]
    public void Should_Create_Correct_Output_Parameters()
    {
        ASTNode node = Setup("my_function: out (int32 a, string b) {}");
        
        Function func = (Function)node.Children[0];
        Symbol param1 = (Symbol)func.OutputParameters[0];
        Symbol param2 = (Symbol)func.OutputParameters[1];
        
        Assert.IsType<Function>(node.Children[0]);
        Assert.Equal(2, func.OutputParameters.Count);
        Assert.Equal("a", param1.Id);
        
        Assert.IsType<SingleType>(param1.Type);
        Assert.Equal(Types.ValueType.int32 ,((SingleType)param1.Type).Type);
        Assert.Equal("b", param2.Id);
        
        Assert.IsType<SingleType>(param2.Type);
        Assert.Equal(Types.ValueType.@string ,((SingleType)param2.Type).Type);
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
    [InlineData("my_function: { hej++; }", typeof(UnaryAssignment))]
    [InlineData("my_function: { int32 hej = 5+2; }", typeof(BinaryAssignment))]
    [InlineData("my_function: { for (int32 i = 5; i < 5; i++) { } }", typeof(ForStatement))]
    public void Assert_Correct_Statement_Type(string code, Type expectedStatementType)
    {
        ASTNode node = Setup(code);
        
        Function func = (Function)node.Children[0];
        ASTNode stmt = func.Children[0];
        var k = stmt.GetType();
        Assert.Equal(expectedStatementType, stmt.GetType());
    }
}
