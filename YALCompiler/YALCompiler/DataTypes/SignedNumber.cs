namespace YALCompiler.DataTypes;

public class SignedNumber : Expression
{
    public ulong Value { get; }
    public bool Negative { get; set; }

    public SignedNumber(ulong value, bool isNegative)
    {
        Value = value;
        Negative = isNegative;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}