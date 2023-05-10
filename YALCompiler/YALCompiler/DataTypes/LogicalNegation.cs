namespace YALCompiler.DataTypes;

public class LogicalNegation : Expression
{
    public Expression Expression { get; set; }

    public LogicalNegation(Expression expression)
    {
        Expression = expression;
    }
}