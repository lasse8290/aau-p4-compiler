namespace YALCompiler.DataTypes;

public class Integer : Expression
{
    public Int64 Value { get; }

    public Integer(long value)
    {
        Value = value;
    }
    
    public override string ToString()
    {
        return Value.ToString();
    }
}