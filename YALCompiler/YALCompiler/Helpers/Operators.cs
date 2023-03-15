namespace YALCompiler.Helpers;

public static class Operators
{
    public enum PredicateOperator
    {
        And,
        Or,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        Equals,
        NotEquals,
    }

    public enum ExpressionOperator
    {
        Multiplication,
        Division,
        Modulo,
        Addition,
        Subtraction,
        LeftShift,
        RightShift,
        BitwiseAnd,
        BitwiseXor,
        BitwiseOr,
        BitwiseNot,
        PreIncrement,
        PreDecrement,
        PostIncrement,
        PostDecrement,
    }

    public enum AssignmentOperator
    {
        Equals,
        AdditionAssignment,
        SubtractionAssignment,
        MultiplicationAssignment,
        DivisionAssignment,
        ModuloAssignment,
        PreIncrement,
        PreDecrement,
        PostIncrement,
        PostDecrement,
    }
    
    public static PredicateOperator? Match(string type)
    {
        return PredicateOperator.TryParse(type, out PredicateOperator t) ? t : null;
    }
}