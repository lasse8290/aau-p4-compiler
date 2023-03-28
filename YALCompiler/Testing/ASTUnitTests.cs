using Antlr4.Runtime.Tree;
using YALCompiler;
using YALCompiler.DataTypes;
using YALCompiler.Helpers;

namespace Testing;
using Antlr4.Runtime;

public class UnitTest1
{
    private static ASTNode Setup(string input)
    {
        AntlrInputStream inputStream = new AntlrInputStream(input);
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
    [InlineData("my_function2: {}", "my_function2")]
    [InlineData("my_function988934: {}", "my_function988934")]
    public void Assert_Correct_Function_Name(string text, string functionName)
    {
        ASTNode node = Setup(text);
        
        Function func = (Function)node.Children[0];

        Assert.IsType<Function>(node.Children[0]);
        Assert.Equal(functionName, func.Id);
        Assert.Empty(func.InputParameters);
        Assert.Empty(func.Children);
        Assert.Empty(func.OutputParameters);
        Assert.False(func.IsAsync);
        Assert.Null(func.ReturnType);
    }

    [Fact]
    public void Assert_Multiple_Input_Parameters()
    {
        ASTNode node = Setup("my_function: in: (int32 a, string b) {}");
        
        Function func = (Function)node.Children[0];
        Symbol param1 = func.InputParameters[0];
        Symbol param2 = func.InputParameters[1];
        
        Assert.IsType<Function>(node.Children[0]);
        Assert.Equal(2, func.InputParameters.Count);
        Assert.Equal("a", param1.Id);
        
        Assert.IsType<SingleType>(param1.Type);
        Assert.Equal(Types.ValueType.int32 ,((SingleType)param1.Type).Type);
        Assert.Equal("b", param2.Id);
        
        Assert.IsType<SingleType>(param2.Type);
        Assert.Equal(Types.ValueType.@string ,((SingleType)param2.Type).Type);
    }
    
    public static IEnumerable<object[]> GetData()
    {
        yield return new object[] { ("foo", 1), 42 };
        yield return new object[] { ("bar", 2, true), 13 };
        yield return new object[] { ("baz", 3, false, "extra"), 7 };
    }
    
    [Fact]
    public void Assert_Multiple_Output_Parameters()
    {
        ASTNode node = Setup("my_function: out: (int32 a, string b) {}");
        
        Function func = (Function)node.Children[0];
        Symbol param1 = func.OutputParameters[0];
        Symbol param2 = func.OutputParameters[1];
        
        Assert.IsType<Function>(node.Children[0]);
        Assert.Equal(2, func.OutputParameters.Count);
        Assert.Equal("a", param1.Id);
        
        Assert.IsType<SingleType>(param1.Type);
        Assert.Equal(Types.ValueType.int32 ,((SingleType)param1.Type).Type);
        Assert.Equal("b", param2.Id);
        
        Assert.IsType<SingleType>(param2.Type);
        Assert.Equal(Types.ValueType.@string ,((SingleType)param2.Type).Type);
    }

    [Fact]
    public void Assert_Empty_Function_Should_Have_No_Children()
    {
        ASTNode node = Setup("my_function: { }");
        
        Function func = node.Children[0] as Function;
        Assert.Empty(func.Children);
    }
    
    [Fact]
    public void Assert_Correct_Assignment_Expression()
    {
        ASTNode node = Setup("my_function: { int32 hej = 5+2; }");

        Function func = node.Children[0] as Function;

        BinaryAssignment assignment = func.Children[0] as BinaryAssignment;
        CompoundExpression expr = assignment.Value as CompoundExpression;
        SignedNumber left = expr.Left as SignedNumber;
        SignedNumber right = expr.Right as SignedNumber;

        Assert.Equal(Operators.AssignmentOperator.Equals, assignment.Operator);
        Assert.IsType<SignedNumber>(left);
        Assert.IsType<SignedNumber>(right);
        Assert.Equal((ulong)5, left.Value);
        Assert.Equal((ulong)2, right.Value);
    }
}