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
    
    public static PredicateOperator? Match(string type)
    {
        return PredicateOperator.TryParse(type, out PredicateOperator t) ? t : null;
    }
}