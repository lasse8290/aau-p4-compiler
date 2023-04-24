using YALCompiler.DataTypes;
using YALCompiler.Helpers;
using FluentAssertions;

namespace Testing;

public class ASTParsingUnitTests : TestingHelper
{
    public static TheoryData<string, object> FunctionDeclaration =>
        new() {
            { "func: {};", new Function {
                Id = "func",
                IsAsync = false,
            } },
            { "async async_func: {};", new Function {
                Id = "async_func",
                IsAsync = true,
            } },
            { "func: in (string a) {};", new Function {
                Id = "func",
                InputParameters = new List<Symbol> {
                    new Symbol("a") {
                        Type = new YALType((Types.ValueType.@string, false))
                    }
                },
                IsAsync = false,
            } },
            { "func: in (string a, int32 b) {};", new Function {
                Id = "func",
                InputParameters = new List<Symbol> {
                    new Symbol("a") {
                        Type = new YALType((Types.ValueType.@string, false))
                    },
                    new Symbol("b") {
                        Type = new YALType((Types.ValueType.int32, false))
                    }
                },
                IsAsync = false,
            } },
            { "func: out (float64 a, int16 b) {};", new Function {
                Id = "func",
                OutputParameters = new List<Symbol> {
                    new Symbol("a") {
                        Type = new YALType((Types.ValueType.float64, false))
                    },
                    new Symbol("b") {
                        Type = new YALType((Types.ValueType.int16, false))
                    }
                },
                ReturnType = new YALType((Types.ValueType.float64, false), (Types.ValueType.int16, false)),
                IsAsync = false,
            } },
        };

    [Theory]
    [MemberData(nameof(FunctionDeclaration))]
    public void Correct_Function_Declaration(string input, object expected)
    {
        var actual = Setup(input, nameof(YALGrammerParser.functionDeclaration));

        actual.Should().BeEquivalentTo(expected, excludings: new string[] { "LineNumber", "Parent", "SymbolTable", "Children", "Initialized" });
    }
    public static TheoryData<string, object> ExternalFunctions =>
        new() {
            { @"external <""arduino/digitalWrite""> externalDigitalWrite: in (int64 pin, int64 value)", new ExternalFunction {
                LibraryName = "arduino",
                FunctionName = "digitalWrite",
                Id = "externalDigitalWrite",
                IsAsync = false,
                InputParameters = new List<Symbol> {
                    new Symbol("pin") {
                        Type = new YALType((Types.ValueType.int64, false))
                    },
                    new Symbol("value") {
                        Type = new YALType((Types.ValueType.int64, false))
                    }
                }
            } }
        };

    [Theory]
    [MemberData(nameof(ExternalFunctions))]
    public void External_Functions(string input, object expected)
    {
        ExternalFunction actual = (ExternalFunction)Setup(input, nameof(YALGrammerParser.externalFunctionDeclaration));

        actual.Should().BeEquivalentTo(expected, excludings: new string[] { "LineNumber", "Parent", "SymbolTable", "Children", "Initialized" });
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

    public static TheoryData<string, object> FormalParameters =>
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
            { "out (bool b)", new List<Symbol> {
                new Symbol("b") { Type = new YALType(Types.ValueType.@bool) }
            } },
            { "out (bool b, string s)", new List<Symbol> {
                new Symbol("b") { Type = new YALType(Types.ValueType.@bool) },
                new Symbol("s") { Type = new YALType(Types.ValueType.@string) }
            } },
            { "out (bool b, string s, float64 f)", new List<Symbol> {
                new Symbol("b") { Type = new YALType(Types.ValueType.@bool) },
                new Symbol("s") { Type = new YALType(Types.ValueType.@string) },
                new Symbol("f") { Type = new YALType(Types.ValueType.float64) }
            } },
        };

    [Theory]
    [MemberData(nameof(FormalParameters))]
    public void Correct_Formal_Parameters(string input, List<Symbol> expected)
    {
        string type = "";

        switch (input.Split(" ")[0])
        {
            case "in":
                type = nameof(YALGrammerParser.formalInputParams);
                break;
            case "out":
                type = nameof(YALGrammerParser.formalOutputParams);
                break;
        }

        var actual = Setup(input, type);

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

    public static TheoryData<string, object> IfStatement =>
        new() {
            { "if (true) { }", new IfStatement {
                Children = {
                    new If { Predicate = new YALCompiler.DataTypes.Boolean { LiteralValue = true } }
                }
            } },
            { "if (true) { } else { }", new IfStatement {
                Children = {
                    new If { Predicate = new YALCompiler.DataTypes.Boolean { LiteralValue = true } },
                    new Else { }
                }
            } },
            { "if (false) { } else if (true) { }", new IfStatement {
                Children = {
                    new If { Predicate = new YALCompiler.DataTypes.Boolean { LiteralValue = false } },
                    new ElseIf { Predicate = new YALCompiler.DataTypes.Boolean { LiteralValue = true } }
                }
            } },
            { "if (false) { } else if (false) {} else { }", new IfStatement {
                Children = {
                    new If { Predicate = new YALCompiler.DataTypes.Boolean { LiteralValue = false } },
                    new ElseIf { Predicate = new YALCompiler.DataTypes.Boolean { LiteralValue = false } },
                    new Else { }
                }
            } }
        };

    [Theory]
    [MemberData(nameof(IfStatement))]
    public void Correct_If_Statement(string input, object expected)
    {
        var actual = Setup(input, nameof(YALGrammerParser.ifStatement));

        actual.Should().BeEquivalentTo(expected, excludings: new string[] { "LineNumber", "Parent" });
    }

    public static TheoryData<string, object> LoopStatements =>
        new() {
            { "for (int32 i = 5; i < 10; i++) {}", new ForStatement {
                DeclarationAssignment = new BinaryAssignment {
                    Targets = new List<ASTNode> {
                        new VariableDeclaration {
                            Variable = new Symbol("i") { Type = new YALType(Types.ValueType.int32) },
                        }
                    },
                    Values = new List<Expression> {
                        new SignedNumber(5, isNegative: false)
                    },
                    Operator = Operators.AssignmentOperator.Equals
                },
                RunCondition = new CompoundPredicate {
                    Left = new Identifier("i"),
                    Right = new SignedNumber(10, isNegative: false),
                    Operator = Operators.PredicateOperator.LessThan
                },
                LoopAssignment = new UnaryAssignment {
                    Target = new Identifier("i"),
                    Operator = Operators.AssignmentOperator.PostIncrement
                }
            } },
            { "while (i < 5) {}", new WhileStatement {
                Predicate = new CompoundPredicate {
                    Left = new Identifier("i"),
                    Right = new SignedNumber(5, isNegative: false),
                    Operator = Operators.PredicateOperator.LessThan
                }
            } }
        };

    [Theory]
    [MemberData(nameof(LoopStatements))]
    public void Loops(string input, object expected)
    {
        var actual = Setup(input, nameof(YALGrammerParser.blockStatement));

        actual.Should().BeEquivalentTo(expected, excludings: new string[] { "LineNumber", "Parent", "SymbolTable" });
    }

    public static TheoryData<string, object> Identifiers =>
        new() {
            { "identifier[5]", new ArrayElementIdentifier("identifier", new SignedNumber(5, isNegative: false)) },
            { "identifier", new Identifier("identifier") },
            { "ref identifier", new Identifier("identifier") { IsRef = true } },
            { "id1, id2, id3", new List<Identifier> {
                new Identifier("id1"),
                new Identifier("id2"),
                new Identifier("id3"),
            } },
            { "(id)", new Identifier("id") },
        };

    [Theory]
    [MemberData(nameof(Identifiers))]
    public void Identifier(string input, object expected)
    {
        var actual = Setup(input, nameof(YALGrammerParser.identifier));

        actual.Should().BeEquivalentTo(expected, excludings: new string[] { "LineNumber", "Parent", "SymbolTable" });
    }

    public static TheoryData<string, object> SimpleAssignments =>
       new() {
            { "i = k", new BinaryAssignment {
                Targets = new List<ASTNode> { new Identifier("i") },
                Values = new List<Expression> { new Identifier("k") },
                Operator = Operators.AssignmentOperator.Equals
            }},
            { "i += k", new BinaryAssignment {
                Targets = new List<ASTNode> { new Identifier("i") },
                Values = new List<Expression> { new Identifier("k") },
                Operator = Operators.AssignmentOperator.AdditionAssignment
            }},
            { "i -= k", new BinaryAssignment {
                Targets = new List<ASTNode> { new Identifier("i") },
                Values = new List<Expression> { new Identifier("k") },
                Operator = Operators.AssignmentOperator.SubtractionAssignment
            }},
            { "i *= k", new BinaryAssignment {
                Targets = new List<ASTNode> { new Identifier("i") },
                Values = new List<Expression> { new Identifier("k") },
                Operator = Operators.AssignmentOperator.MultiplicationAssignment
            }},
            /* This to be uncommented when grammar has been fixed */
            /*{ "i /= k", new BinaryAssignment {
                Targets = new List<ASTNode> { new Identifier("i") },
                Values = new List<Expression> { new Identifier("k") },
                Operator = Operators.AssignmentOperator.DivisionAssignment
            }},*/
            { "i %= k", new BinaryAssignment {
                Targets = new List<ASTNode> { new Identifier("i") },
                Values = new List<Expression> { new Identifier("k") },
                Operator = Operators.AssignmentOperator.ModuloAssignment
            }},
            { "++i", new UnaryAssignment {
                Target = new Identifier("i"),
                Operator = Operators.AssignmentOperator.PreIncrement
            }},
            { "--i", new UnaryAssignment {
                Target = new Identifier("i"),
                Operator = Operators.AssignmentOperator.PreDecrement
            }},
            { "i++", new UnaryAssignment {
                Target = new Identifier("i"),
                Operator = Operators.AssignmentOperator.PostIncrement
            }},
            { "i--", new UnaryAssignment {
                Target = new Identifier("i"),
                Operator = Operators.AssignmentOperator.PostDecrement
            }},
       };

    [Theory]
    [MemberData(nameof(SimpleAssignments))]
    public void SimpleAssignment(string input, object expected)
    {
        var actual = Setup(input, nameof(YALGrammerParser.simpleAssignment));

        actual.Should().BeEquivalentTo(expected, excludings: new string[] { "LineNumber", "Parent" });
    }

    public static TheoryData<string, object> DeclarationAssignments =>
       new() {
            { "int32 i = 1", new BinaryAssignment {
                Targets = new List<ASTNode> {
                    new VariableDeclaration {
                        Variable = new Symbol("i") { Type = new YALType(Types.ValueType.int32) },
                    }
                },
                Values = new List<Expression> { new SignedNumber(1, isNegative: false ) },
                Operator = Operators.AssignmentOperator.Equals
            }},
       };

    [Theory]
    [MemberData(nameof(DeclarationAssignments))]
    public void DeclarationAssignment(string input, object expected)
    {
        var actual = Setup(input, nameof(YALGrammerParser.declarationAssignment));

        actual.Should().BeEquivalentTo(expected, excludings: new string[] { "Initialized", "LineNumber", "Parent" });
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
            { "5 * my_string", new CompoundExpression {
                Left = new SignedNumber(5, isNegative: false),
                Right = new Identifier("my_string"),
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
            { @"5+2, expr1 + 5, ""dd""", new List<Expression> {
                new CompoundExpression {
                    Left = new SignedNumber(5, isNegative: false),
                    Right = new SignedNumber(2, isNegative: false),
                    Operator = Operators.ExpressionOperator.Addition,
                },
                new CompoundExpression {
                    Left = new Identifier("expr1"),
                    Right = new SignedNumber(5, isNegative: false),
                    Operator = Operators.ExpressionOperator.Addition
                },
                new StringLiteral("dd"),
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