namespace YALParser.Helpers;

public class TupleType : YALType
{
    public List<Types.ValueType> Types { get; set; } = new();

    public TupleType(params Types.ValueType[] types)
    {
        Types.AddRange(types);
    }

    public override string ToString() => "(" + string.Join(", ", Types) + ")";
}