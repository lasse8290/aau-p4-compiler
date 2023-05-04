namespace YALCompiler.DataTypes;

public class SignedFloat : Expression
{
    public double Value { get; }
    public SignedFloat(double value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}