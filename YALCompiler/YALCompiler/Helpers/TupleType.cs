namespace YALCompiler.Helpers;

public class TupleType : YALType
{
    public List<SingleType> Types { get; set; } = new();

    public TupleType(params SingleType[] types)
    {
        Types.AddRange(types);
    }

    public override string ToString() => "(" + string.Join(", ", Types) + ")";
}