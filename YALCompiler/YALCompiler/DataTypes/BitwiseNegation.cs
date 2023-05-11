namespace YALCompiler.DataTypes;

public class BitwiseNegation : Expression
{
    public Expression Expression { get; set; }

    public BitwiseNegation(Expression expression)
    {
        Expression = expression;
    }
}