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
    public void Should_Create_Correct_Function(string text, string functionName)
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
    public void Should_Create_Correct_Input_Parameters()
    {
        ASTNode node = Setup("my_function: in: (int32 a, string b) {}");
        
        Function func = (Function)node.Children[0];
        Symbol param1 = (Symbol)func.InputParameters[0];
        Symbol param2 = (Symbol)func.InputParameters[1];
        
        Assert.IsType<Function>(node.Children[0]);
        Assert.Equal(2, func.InputParameters.Count);
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
        ASTNode node = Setup("my_function: out: (int32 a, string b) {}");
        
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
    
    [Fact]
    public void Should_Create_Correct_Statement()
    {
        ASTNode node = Setup("my_function: {  }");
        
        Function func = (Function)node.Children[0];
        BinaryAssignment assignment = (BinaryAssignment)func.Children[0];
        CompoundExpression expr = (CompoundExpression)assignment.Value;
        SignedNumber left = (SignedNumber)expr.Left;
        SignedNumber right = (SignedNumber)expr.Right;
        
        Assert.Equal(1, func.Children.Count);
        Assert.IsType<SignedNumber>(left);
        Assert.IsType<SignedNumber>(right);
        Assert.Equal((ulong)5, left.Value);
        Assert.Equal((ulong)2, right.Value);
        
        /*Function func = (Function)node.Children[0];
        Symbol param1 = (Symbol)func.OutputParameters[0];
        Symbol param2 = (Symbol)func.OutputParameters[1];
        
        Assert.IsType<Function>(node.Children[0]);
        Assert.Equal(2, func.OutputParameters.Count);
        Assert.Equal("a", param1.Id);
        
        Assert.IsType<SingleType>(param1.Type);
        Assert.Equal(Types.ValueType.int32 ,((SingleType)param1.Type).Type);
        Assert.Equal("b", param2.Id);
        
        Assert.IsType<SingleType>(param2.Type);
        Assert.Equal(Types.ValueType.@string ,((SingleType)param2.Type).Type);*/
    }
}