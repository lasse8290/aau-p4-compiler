namespace YALParser.Helpers;

public class TupleType : YALType
{
    public List<string> Types { get; set; } = new();

    public TupleType(params string[] types)
    {
        Types.AddRange(types);
    }

    public override string ToString() => "(" + string.Join(", ", Types) + ")";
}