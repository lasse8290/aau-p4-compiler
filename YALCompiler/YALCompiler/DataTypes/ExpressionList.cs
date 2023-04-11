namespace YALCompiler.DataTypes;

public class ExpressionList : Expression
{
    public List<Expression> Expressions { get; set; } = new();
}