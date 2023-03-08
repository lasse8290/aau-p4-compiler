namespace YALCompiler.DataTypes;

public class SignedNumber : Expression
{
    public long Value { get; }

    public SignedNumber(long value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}