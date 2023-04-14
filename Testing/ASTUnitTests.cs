using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using YALCompiler;
using YALCompiler.DataTypes;
using YALCompiler.Helpers;
using YALCompiler.ErrorHandlers;
using System.Reflection;
using FluentAssertions;

namespace Testing;

public class ASTUnitTests
{
    YALGrammerVisitor visitor = new YALGrammerVisitor(new ErrorHandler(), new WarningsHandler());

    public IParseTree Parse(YALGrammerParser parser, string methodName) => (IParseTree)parser.GetType().GetMethod(methodName).Invoke(parser, null);

    Action<ASTNode, ASTNode> LineNumbersRemover = (node, parent) =>
    {
        parent.LineNumber = 0;
        node.LineNumber = 0;
    };

    private YALGrammerParser Setup(string input)
    {
        AntlrInputStream inputStream = new AntlrInputStream(input);
        YALGrammerLexer lexer = new YALGrammerLexer(inputStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
        YALGrammerParser parser = new YALGrammerParser(commonTokenStream);

        return parser;
    }

    private ASTNode Setup(string input, string methodName, bool removeLineNumbers = false)
    {
        YALGrammerParser parser = Setup(input);
        IParseTree tree = Parse(parser, methodName);

        ASTNode root = (ASTNode)visitor.Visit(tree);

        // Not linking because Assert.Equivalence doesn't work with it because of parents
        if (removeLineNumbers)
        {
            LinkerASTTraverser lineNumberRemove = new(root, LineNumbersRemover);
            lineNumberRemove.BeginTraverse();
            root.LineNumber = 0;
        }

        return root;
    }

    [Theory]
    [InlineData(@"external <""my_library""> print1: in (string _string);", "print1")]
    [InlineData(@"external <""hej""> print2: in (string _string);", "print2")]
    public void External_Function_Exists_In_Symbol_Table(string input, string expectedName)
    {
        ExternalFunction externalVarDcl = (ExternalFunction)Setup(input, nameof(YALGrammerParser.externalFunctionDeclaration));

        Assert.Equal(expectedName, externalVarDcl.Id);
    }

    [Theory]
    [InlineData("my_function: {}", "my_function")]
    [InlineData("function_name_100: {}", "function_name_100")]
    public void Correct_Function_Name(string input, string functionName)
    {
        Function func = (Function)Setup(input, nameof(YALGrammerParser.functionDeclaration));

        Assert.IsType<Function>(func);
        Assert.Equal(functionName, func.Id);
    }

    public static TheoryData<string, int> functionsData =>
        new() {
            { "", 0 },
            { String.Concat(Enumerable.Range(0, 40).Select(i => $"function{i}: {{}}")), 40 },
            { String.Concat(Enumerable.Range(0, 15).Select(i => $"function{i}: {{}}")), 15 },
            { String.Concat(Enumerable.Range(0, 2).Select(i => $"function{i}: {{}}")), 2 }
        };

    [Theory]
    [MemberData(nameof(functionsData))]
    public void Should_Create_x_Functions(string input, int expectedFunctionsCount)
    {
        ASTNode _program = Setup(input, nameof(YALGrammerParser.program));

        Assert.Equal(expectedFunctionsCount, _program.Children.Count);
    }

    [Theory]
    [InlineData("", 0, 0)]
    [InlineData("out (string a)", 0, 1)]
    [InlineData("in (string a) out ()", 1, 0)]
    [InlineData("in (int32 a, int32 b)", 2, 0)]
    [InlineData("in (int32 a, int32 b) out: (int32 c, int32 d)", 2, 2)]
    public void Correct_Function_Parameters_Count(string input, int expectedInputParametersCount, int expectedOutputParametersCount)
    {
        Function func = (Function)Setup($"my_function: {input} {{}}", nameof(YALGrammerParser.functionDeclaration));

        Assert.Equal(expectedInputParametersCount, func.InputParameters.Count);
        Assert.Equal(expectedOutputParametersCount, func.OutputParameters.Count);
    }

    public static TheoryData<string, List<Types.ValueType>> InputParametersData =>
        new() {
            { "in (int32 a, bool b)", new List<Types.ValueType> { Types.ValueType.int32, Types.ValueType.@bool } },
            { "in (float32 c, float64 d)", new List<Types.ValueType> { Types.ValueType.float32, Types.ValueType.float64 } },
            { "out (int32 a, bool b)", new List<Types.ValueType> { Types.ValueType.int32, Types.ValueType.@bool } },
            { "out (float32 c, float64 d)", new List<Types.ValueType> { Types.ValueType.float32, Types.ValueType.float64 } },
        };

    [Theory]
    [MemberData(nameof(InputParametersData))]
    public void Formal_Parameters(string parameters, List<Types.ValueType> expected)
    {
        IParseTree tree;
        switch (parameters.Split(" ")[0])
        {
            case "in":
                tree = Setup(parameters).formalInputParams();
                break;
            case "out":
                tree = Setup(parameters).formalOutputParams();
                break;
            default:
                throw new Exception("First lexer must be 'in' or 'out'");
        }
        List<Symbol> list = (List<Symbol>)visitor.Visit(tree);

        for (int i = 0; i < expected.Count; i++)
        {
            Types.ValueType expectedType = expected[i];
            Symbol param = (Symbol)list[i];

            Assert.Equal(expectedType, ((SingleType)param.Type!).Type);
        }
    }

    [Theory]
    [InlineData("my_function: { }", 1)]
    [InlineData("my_function: { int32 hej = 1+2; }", 2)]
    [InlineData("my_function: { int32 hej = 3+4; int32 hej2 = 4+5; }", 3)]
    [InlineData("my_function: { int32 hej = 5+6; int32 hej3 = 6+7; int32 hej4 = 7+8 }", 4)]
    public void Correct_Amount_Of_BlockStatements(string input, int expected)
    {
        Function func = (Function)Setup(input, nameof(YALGrammerParser.functionDeclaration));

        Assert.Equal(expected, func.Children.Count);
    }

    [Theory]
    [InlineData("int32 hej = 5+2;", typeof(BinaryAssignment))]
    [InlineData("functionCall();", typeof(FunctionCall))]
    [InlineData("int32 i;", typeof(VariableDeclaration))]
    [InlineData("i++;", typeof(UnaryAssignment))]
    public void Correct_SingleStatement_Type(string input, Type expected)
    {
        var stmt = Setup(input, nameof(YALGrammerParser.singleStatement));

        Assert.IsType(expected, stmt);
    }

    [Theory]
    [InlineData("for (int32 i = 5; i < 5; i++) { }", typeof(ForStatement))]
    [InlineData("while (i < 5) { }", typeof(WhileStatement))]
    [InlineData("if (i < 5) { }", typeof(IfStatement))]
    public void BlockStatement_Type(string input, Type expected)
    {
        var stmt = Setup(input, nameof(YALGrammerParser.blockStatement));

        Assert.IsType(expected, stmt);
    }

    [Theory]
    [InlineData("for (int32 i = 5; i < 10; i++) {}")]
    public void For_Loop(string input)
    {
        var for_stmt = Setup(input, nameof(YALGrammerParser.forStatement));

        Assert.IsType(typeof(ForStatement), for_stmt);
    }

    public static TheoryData<string, Expression> Expressions =>
        new() {
            #region PostIncrementDecrement
            { "i++", new UnaryAssignment() {
                Target = new Identifier("i"),
                Operator = Operators.AssignmentOperator.PostIncrement
            } },
            { "i--", new UnaryAssignment() {
                Target = new Identifier("i"),
                Operator = Operators.AssignmentOperator.PostDecrement
            } },
            #endregion
            #region PrefixUnary
            { "++i", new UnaryAssignment() {
                Target = new Identifier("i"),
                Operator = Operators.AssignmentOperator.PreIncrement
            } },
            { "--i", new UnaryAssignment() {
                Target = new Identifier("i"),
                Operator = Operators.AssignmentOperator.PreDecrement
            } },
            #endregion
            #region MultiplicationDivisionModulo
            { "5 * 2", new CompoundExpression() {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.Multiplication
            } },
            { "5 / 2", new CompoundExpression() {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.Division
            } },
            { "5 % 2", new CompoundExpression() {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.Modulo
            } },
            { "5 % 2", new CompoundExpression() {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.Modulo
            } },
            #endregion
            #region AdditionSubtraction
            { "5 + 2", new CompoundExpression() {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.Addition
            } },
            { "5 - 2", new CompoundExpression() {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.Subtraction
            } },
            #endregion
            #region LeftRightShift
            { "5 << 2", new CompoundExpression() {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.LeftShift
            } },
            { "5 >> 2", new CompoundExpression() {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.RightShift
            } },
            #endregion
            #region Bitwise
            { "5 & 2", new CompoundExpression() {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.BitwiseAnd
            } },
            { "5 ^ 2", new CompoundExpression() {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.BitwiseXor
            } },
            { "5 | 2", new CompoundExpression() {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.BitwiseOr
            } },
            { "5 ~ 2", new CompoundExpression() {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.BitwiseNot
            } },
            #endregion
            #region VariableAssignment
            { @"hi = ""whaaat""", new BinaryAssignment {
                Target = new Identifier("hi"),
                Operator = Operators.AssignmentOperator.Equals,
                Value = new StringLiteral("whaaat")
            }},
            #endregion
            #region Variable
            { @"my_custom_variable", new Identifier("my_custom_variable") },
            #endregion
            #region FunctionCallExpression
            { "myCall(param1, param2)", new FunctionCall("myCall", await: false) {
                InputParameters = new List<Expression> {
                    new Identifier("param1"),
                    new Identifier("param2")
                }
            }},
            #endregion
            #region FloatLiteral
            { "0.4", new SignedFloat(0.4) },
            { "-0.4", new SignedFloat(-0.4) },
            #endregion
            #region NumberLiteral
            { "5", new SignedNumber(5, isNegative: false) },
            { "-5", new SignedNumber(5, isNegative: true) },
            #endregion
            #region StringLiteral
            { @"""my_string""", new StringLiteral("my_string") },
            #endregion
            #region ParenthesizedExpression
            { "(5)", new SignedNumber(5, isNegative: false) },
            #endregion
            #region ArrayLiteral
            { "{ 5+2, 3+2 }", new ArrayLiteral {
                Values = {
                    new CompoundExpression {
                        Left = new SignedNumber(5, isNegative: false),
                        Right = new SignedNumber(2, isNegative: false),
                        Operator = Operators.ExpressionOperator.Addition,
                    },
                    new CompoundExpression {
                        Left = new SignedNumber(3, isNegative: false),
                        Right = new SignedNumber(2, isNegative: false),
                        Operator = Operators.ExpressionOperator.Addition,
                    }
                }
            }},
            #endregion
        };

    [Theory]
    [MemberData(nameof(Expressions))]
    public void Expression_Correct_Type_And_Values(string input, Expression expected)
    {
        var expr = Setup(input, nameof(YALGrammerParser.expression), removeLineNumbers: true);

        CAssert.Equivalent_Wrapper(expected, expr, options => options.Exclude(k => k.LineNumber));
    }
}