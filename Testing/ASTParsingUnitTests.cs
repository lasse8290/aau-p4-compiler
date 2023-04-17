using YALCompiler.DataTypes;
using YALCompiler.Helpers;
using FluentAssertions;

namespace Testing;

public class ASTParsingUnitTests : TestingHelper
{

    public static TheoryData<string, object> FunctionDeclaration =>
        new() {
            { "my_function: {};", new Function {
                Id = "my_function",
                Children = new List<ASTNode> { new ReturnStatement() }
            } },
            { "async function_name_100: {};", new Function {
                Id = "function_name_100",
                IsAsync = true,
                Children = new List<ASTNode> { new ReturnStatement() }
            } }
        };

    [Theory]
    [MemberData(nameof(FunctionDeclaration))]
    public void Correct_Function_Declaration(string input, object expected)
    {
        var actual = Setup(input, nameof(YALGrammerParser.functionDeclaration));

        actual.Should().BeEquivalentTo(expected, excludings: new string[] { "LineNumber", "Parent", "SymbolTable" });
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
    public void Should_Create_x_Functions(string input, int expected)
    {
        ASTNode _program = (ASTNode)Setup(input, nameof(YALGrammerParser.program));

        _program.Children.Count.Should().Be(expected);
    }

    [Theory]
    [InlineData("", 0, 0)]
    [InlineData("out (string a)", 0, 1)]
    [InlineData("in (string a)", 1, 0)]
    // [InlineData("in (string a) out ()", 1, 0)] We need to discuss if this is it allowed?
    [InlineData("in (int32 a, int32 b)", 2, 0)]
    [InlineData("in (int32 a, int32 b) out (int32 c)", 2, 1)]
    [InlineData("in (int32 a, int32 b) out (int32 c, int32 d)", 2, 2)]
    [InlineData("in (int32 a) out (int32 c, int32 d)", 1, 2)]
    public void Correct_Function_Parameters_Count(string input, int expectedInputParametersCount, int expectedOutputParametersCount)
    {
        Function func = (Function)Setup($"my_function: {input} {{}}", nameof(YALGrammerParser.functionDeclaration));

        int actualInputParamsCount = func.InputParameters.Count;
        int actualOutputParamsCount = func.OutputParameters.Count;

        actualInputParamsCount.Should().Be(expectedInputParametersCount);
        actualOutputParamsCount.Should().Be(expectedOutputParametersCount);
    }

    public static TheoryData<string, object> FormalInputParameters =>
        new() {
            { "in (bool b)", new List<Symbol> {
                new Symbol("b") { Type = new YALType(Types.ValueType.@bool) }
            } },
            { "in (bool b, string s)", new List<Symbol> {
                new Symbol("b") { Type = new YALType(Types.ValueType.@bool) },
                new Symbol("s") { Type = new YALType(Types.ValueType.@string) }
            } },
            { "in (bool b, string s, float64 f)", new List<Symbol> {
                new Symbol("b") { Type = new YALType(Types.ValueType.@bool) },
                new Symbol("s") { Type = new YALType(Types.ValueType.@string) },
                new Symbol("f") { Type = new YALType(Types.ValueType.float64) }
            } },
        };

    [Theory]
    [MemberData(nameof(FormalInputParameters))]
    public void Formal_Parameters(string input, List<Symbol> expected)
    {
        var actual = Setup(input, nameof(YALGrammerParser.formalInputParams));

        actual.Should().BeEquivalentTo(expected, excludings: new string[] { "LineNumber", "Parent" });
    }

    [Theory]
    [InlineData("my_function: { }", 1)]
    [InlineData("my_function: { int32 hej = 1+2; }", 2)]
    [InlineData("my_function: { int32 hej = 3+4; int32 hej2 = 4+5; }", 3)]
    [InlineData("my_function: { int32 hej = 5+6; int32 hej3 = 6+7; int32 hej4 = 7+8 }", 4)]
    public void Correct_Amount_Of_BlockStatements(string input, int expected)
    {
        Function actual = (Function)Setup(input, nameof(YALGrammerParser.functionDeclaration));

        actual.Children.Count.Should().Be(expected);
    }

    [Theory]
    [InlineData("int32 hej = 5+2", typeof(BinaryAssignment))]
    [InlineData("functionCall()", typeof(FunctionCall))]
    [InlineData("int32 i", typeof(List<VariableDeclaration>))]
    [InlineData("i++", typeof(UnaryAssignment))]
    public void Correct_SingleStatement_Type(string input, Type expected)
    {
        var actual = Setup(input, nameof(YALGrammerParser.singleStatement));

        actual.Should().BeOfType(expected);
    }

    [Theory]
    [InlineData("for (int32 i = 5; i < 5; i++) { }", typeof(ForStatement))]
    [InlineData("while (i < 5) { }", typeof(WhileStatement))]
    [InlineData("if (i < 5) { }", typeof(IfStatement))]
    public void BlockStatement_Type(string input, Type expected)
    {
        var actual = Setup(input, nameof(YALGrammerParser.blockStatement));

        actual.Should().BeOfType(expected);
    }

    [Theory]
    [InlineData("for (int32 i = 5; i < 10; i++) {}")]
    public void For_Loop(string input)
    {
        var actual = Setup(input, nameof(YALGrammerParser.forStatement));

        actual.Should().BeOfType(typeof(ForStatement));
    }

    public static TheoryData<string, object> Expressions =>
        new() {
            { "i++", new UnaryAssignment {
                Target = new Identifier("i"),
                Operator = Operators.AssignmentOperator.PostIncrement
            } },
            { "i--", new UnaryAssignment {
                Target = new Identifier("i"),
                Operator = Operators.AssignmentOperator.PostDecrement
            } },
            { "++i", new UnaryAssignment {
                Target = new Identifier("i"),
                Operator = Operators.AssignmentOperator.PreIncrement
            } },
            { "--i", new UnaryAssignment {
                Target = new Identifier("i"),
                Operator = Operators.AssignmentOperator.PreDecrement
            } },
            { "5 * 2", new CompoundExpression {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.Multiplication
            } },
            { "5 / 2", new CompoundExpression {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.Division
            } },
            { "5 % 2", new CompoundExpression {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.Modulo
            } },
            { "5 % 2", new CompoundExpression {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.Modulo
            } },
            { "5 + 2", new CompoundExpression {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.Addition
            } },
            { "5 - 2", new CompoundExpression {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.Subtraction
            } },
            { "5 << 2", new CompoundExpression {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.LeftShift
            } },
            { "5 >> 2", new CompoundExpression {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.RightShift
            } },
            { "5 & 2", new CompoundExpression {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.BitwiseAnd
            } },
            { "5 ^ 2", new CompoundExpression {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.BitwiseXor
            } },
            { "5 | 2", new CompoundExpression {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.ExpressionOperator.BitwiseOr
            } },
            { "~2", new SignedNumber(2, isNegative: false) {
                BitwiseNegated = true,
            } },
            { "5 < 2", new CompoundPredicate {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.PredicateOperator.LessThan
            } },
            { "5 <= 2", new CompoundPredicate {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.PredicateOperator.LessThanOrEqual
            } },
            { "5 > 2", new CompoundPredicate {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.PredicateOperator.GreaterThan
            } },
            { "5 >= 2", new CompoundPredicate {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.PredicateOperator.GreaterThanOrEqual
            } },
            { "5 == 2", new CompoundPredicate {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.PredicateOperator.Equals
            } },
            { "5 != 2", new CompoundPredicate {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.PredicateOperator.NotEquals
            } },
            { "5 && 2", new CompoundPredicate {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.PredicateOperator.And
            } },
            { "5 || 2", new CompoundPredicate {
                Left = new SignedNumber(5, isNegative: false),
                Right = new SignedNumber(2, isNegative: false),
                Operator = Operators.PredicateOperator.Or
            } },
            { @"hi = ""whaaat"", k", new BinaryAssignment {
                Operator = Operators.AssignmentOperator.Equals,
                Targets = new List<ASTNode> {
                    new Identifier("hi")
                },
                Values = new List<Expression> {
                    new StringLiteral("whaaat"),
                    new Identifier("k"),
                },
            }},
            { @"my_custom_variable", new Identifier("my_custom_variable") },
            { "myCall(param1, param2)", new FunctionCall("myCall", await: false) {
                InputParameters = new List<Expression> {
                    new Identifier("param1"),
                    new Identifier("param2")
                }
            }},
            { "0.4", new SignedFloat(0.4) },
            { "-0.4", new SignedFloat(-0.4) },
            { "5", new SignedNumber(5, isNegative: false) },
            { "-5", new SignedNumber(5, isNegative: true) },
            { @"""my_string""", new StringLiteral("my_string") },
            { "true", new YALCompiler.DataTypes.Boolean { LiteralValue = true } },
            { "false", new YALCompiler.DataTypes.Boolean { LiteralValue = false } },
            { "(5)", new SignedNumber(5, isNegative: false) },
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
            { "expr1, expr2, expr3", new List<Expression> {
                new Identifier("expr1"),
                new Identifier("expr2"),
                new Identifier("expr3"),
            }},
        };

    [Theory]
    [MemberData(nameof(Expressions))]
    public void Expression_Correct_Values(string input, object expected)
    {
        var actual = Setup(input, nameof(YALGrammerParser.expression));

        actual.Should().BeEquivalentTo(expected, excludings: new string[] { "LineNumber", "Parent" });
    }
}