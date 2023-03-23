namespace YALCompiler.DataTypes;

public class ArrayLiteral : Expression
{
    public List<Expression> Values { get; set; } = new();
}