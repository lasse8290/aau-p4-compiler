namespace YALCompiler.DataTypes;

public class StringLiteral : Expression
{
    public string Value { get; }

    public StringLiteral(string value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }
}