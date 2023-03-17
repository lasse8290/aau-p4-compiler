namespace YALCompiler.DataTypes;

public class SignedFloat : Expression
{
    public double Value { get; }
    public bool Negative { get; set; }

    public SignedFloat(double value, bool isNegative)
    {
        Value = value;
        Negative = isNegative;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}