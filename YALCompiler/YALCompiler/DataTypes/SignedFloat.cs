namespace YALCompiler.DataTypes;

public class SignedFloat : Expression
{
    public float Value { get; }

    public SignedFloat(float value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}