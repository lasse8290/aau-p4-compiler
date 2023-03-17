namespace YALCompiler.DataTypes;

public class SignedNumber : Expression
{
    public UInt64 Value { get; }
    public bool Negative { get; set; }

    public SignedNumber(UInt64 value, bool isNegative)
    {
        Value = value;
        Negative = isNegative;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}