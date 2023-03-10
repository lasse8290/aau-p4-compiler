namespace YALCompiler.Helpers;

public class TupleType : YALType
{
    public List<Types.ValueType> Types { get; set; } = new();

    public TupleType(params Types.ValueType[] types)
    {
        Types.AddRange(types);
    }
    
    public TupleType(params SingleType[] types)
    {
        Types.AddRange(types.Select(t => t.Type));
    }

    public override string ToString() => "(" + string.Join(", ", Types) + ")";
}