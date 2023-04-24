using FluentAssertions;
using YALCompiler;
using YALCompiler.DataTypes;
using YALCompiler.ErrorHandlers;
using YALCompiler.Exceptions;
using YALCompiler.Helpers;

public class TypeAndScopeTesting : TestingHelper
{
    public static TheoryData<string, Exception> TypeAndScopeCheckingData =>
        new() {
            { "main: { k = 10; }", new IdentifierNotFoundException("k") },
            { "main: { int32 a; int32 b = a + 1; }", new UninitializedVariableException("a") },

            // //{ "main: { int32 a = 1; int32 a = 1; }", new VariableAlreadyExistsException("a") },
            // //{ "main: { int66 a; }", new TypeNotRecognizedException("int66") },

            { @"main: { bool a = ""a""; }", new TypeMismatchException("string", "bool") },
            { "main: { bool a = 1; }", new TypeMismatchException("int8", "bool") },
            { "main: { bool a = 1.2; }", new TypeMismatchException("float32", "bool") },

            { @"main: { int8 a = ""a""; }", new TypeMismatchException("string", "int8") },
            { "main: { int8 a = false; }", new TypeMismatchException("bool", "int8") },
            { "main: { int8 a = 1.2; }", new TypeMismatchException("float32", "int8") },

            { "main: { int32 k = 1; bool a = k; }", new TypeMismatchException("int32", "bool") },

            { "main: { string a = 1; }", new TypeMismatchException("int8", "string") },
            { "main: { string a = false; }", new TypeMismatchException("bool", "string") },
            { "main: { string a = 1.2; }", new TypeMismatchException("float32", "string") },

            { "main: { char a = 1; }", new TypeMismatchException("int8", "char") },
            { @"main: { char a = ""dd""; }", new TypeMismatchException("string", "char") },
            { "main: { char a = false; }", new TypeMismatchException("bool", "char") },
            
            { "main: { float64 a = false; }", new TypeMismatchException("bool", "float64") },
            { @"main: { float64 a = ""a""; }", new TypeMismatchException("string", "float64") },

            // { "main: { int8 a = -99999999999999999; }", new SignedLongOutOfRangeException(99999999999999999) },

            { "main: { if (1) { } }", new InvalidPredicateException("1", "int8") },
            { "main: { int32 a = 1; if (a) { } }", new InvalidPredicateException("a", "int32") },
            { @"main: { if (""a"") { } }", new InvalidPredicateException("a", "string") },

            { @"main: { string a = ""a"" * ""b"" ;}", new InvalidOperatorException(Operators.ExpressionOperator.Multiplication, Types.ValueType.@string) },
            { @"main: { string a =""a"" / ""b""; }", new InvalidOperatorException(Operators.ExpressionOperator.Division, Types.ValueType.@string) },
            { @"main: { int8 a = ""a"" / 2; }", new InvalidOperatorException(Operators.ExpressionOperator.Division, Types.ValueType.@string) },
            { @"main: { string a =""a"" % ""b""; }", new InvalidOperatorException(Operators.ExpressionOperator.Modulo, Types.ValueType.@string) },
            { @"main: { string a =""a"" << ""b""; }", new InvalidOperatorException(Operators.ExpressionOperator.LeftShift, Types.ValueType.@string) },
            { @"main: { string a =""a"" >> ""b""; }", new InvalidOperatorException(Operators.ExpressionOperator.RightShift, Types.ValueType.@string) },
            { @"main: { string a =""a"" | ""b""; }", new InvalidOperatorException(Operators.ExpressionOperator.BitwiseOr, Types.ValueType.@string) },
            { @"main: { string a =""a"" ^ ""b""; }", new InvalidOperatorException(Operators.ExpressionOperator.BitwiseXor, Types.ValueType.@string) },

            { "main: { bool a = true * false; }", new InvalidOperatorException(Operators.ExpressionOperator.Multiplication, Types.ValueType.@bool) },
            { "main: { bool a = true / false; }", new InvalidOperatorException(Operators.ExpressionOperator.Division, Types.ValueType.@bool) },
            { "main: { bool a = true % false; }", new InvalidOperatorException(Operators.ExpressionOperator.Modulo, Types.ValueType.@bool) },
            { "main: { bool a = true + false; }", new InvalidOperatorException(Operators.ExpressionOperator.Addition, Types.ValueType.@bool) },
            { "main: { bool a = true - false; }", new InvalidOperatorException(Operators.ExpressionOperator.Subtraction, Types.ValueType.@bool) },
            { "main: { bool a = true << false; }", new InvalidOperatorException(Operators.ExpressionOperator.LeftShift, Types.ValueType.@bool) },
            { "main: { bool a = true >> false; }", new InvalidOperatorException(Operators.ExpressionOperator.RightShift, Types.ValueType.@bool) },
            { "main: { bool a = true | false; }", new InvalidOperatorException(Operators.ExpressionOperator.BitwiseOr, Types.ValueType.@bool) },
            { "main: { bool a = true ^ false; }", new InvalidOperatorException(Operators.ExpressionOperator.BitwiseXor, Types.ValueType.@bool) },

            // { "int32 a = 1;", new InvalidGlobalScopedAssignmentException() },

            { "func: in (bool a) {} main: { func(1); }", new InvalidFunctionCallInputParameters(new List<Symbol> { new Symbol("a", null, new YALType(Types.ValueType.@bool)) }, new List<string> { "int8" }) },
            { "func: in (bool a, bool b) {} main: { func(false, 1); }", new InvalidFunctionCallInputParameters(new List<Symbol> { new Symbol("a", null, new YALType(Types.ValueType.@bool)), new Symbol("b", null, new YALType(Types.ValueType.@bool)) }, new List<string> { "bool", "int8" }) },

            // InvalidExpressionException
            { "async awaitable: {} main: { await awaitable(); }", new InvalidAwaitException() },

            { "main: { 5++; }", new InvalidAssignmentException(new UnaryAssignment {
                Operator = Operators.AssignmentOperator.PostIncrement
            }) },
            { "main: { 5.2++; }", new InvalidAssignmentException(new UnaryAssignment {
                Operator = Operators.AssignmentOperator.PostIncrement
            }) },
            { @"main: { ""a""--; }", new InvalidAssignmentException(new UnaryAssignment {
                Operator = Operators.AssignmentOperator.PostDecrement
            }) },
            { "main: { --false; }", new InvalidAssignmentException(new UnaryAssignment {
                Operator = Operators.AssignmentOperator.PreDecrement
            }) },
            { "main: { ++true; }", new InvalidAssignmentException(new UnaryAssignment {
                Operator = Operators.AssignmentOperator.PreIncrement
            }) },

            { "main: { i += 1; }", new IdentifierNotFoundException("i") },
            { "main: { kdaskskdsak += 1; }", new IdentifierNotFoundException("kdaskskdsak") },
            // { "main: { i++; }", new IdentifierNotFoundException("i") },
            // { "main: { i--; }", new IdentifierNotFoundException("i") },

            { "non_awaitable: {} async main: { await non_awaitable(); }", new CannotAwaitNonAsyncFunctionException() },
            { "async awaitable: out (int8 a) { a = 1; } x: in (int8 a) {} async main: { x(awaitable()) }", new CannotUseAsyncFunctionAsExpressionWithoutAwaitException() },

            // { "main: { int8[1] a; int8 b = a[2]; }", new ArrayIndexOutOfBoundsException(2, 1) },
        };

    [Theory]
    [MemberData(nameof(TypeAndScopeCheckingData))]
    public void TypeAndScopeChecking(string input, Exception expected)
    {
        var actual = Setup(input, nameof(YALGrammerParser.program));

        ErrorHandler errorHandler = new ErrorHandler();
        WarningsHandler warningsHandler = new WarningsHandler();

        LinkerASTTraverser linker = new((ASTNode)actual);
        linker.BeginTraverse();

        TypeAndScopeCheckerTraverser traverser = new((ASTNode)actual, errorHandler, warningsHandler);
        traverser.BeginTraverse();

        errorHandler.ErrorsWithoutLineNumber.Should().ContainEquivalentOf(expected);
    }
}