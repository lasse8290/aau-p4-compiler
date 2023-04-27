namespace YALCompiler.DataTypes;

public class UnsignedInteger : Expression
{
    public UInt64 Value { get; }
    
    public UnsignedInteger(ulong value)
    {
        Value = value;
    }
    
    public override string ToString()
    {
        return Value.ToString();
    }
}