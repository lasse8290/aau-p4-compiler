namespace YALParser.Helpers;

public class SingleType : YALType
{
    public string Type { get; set; }

    public SingleType(string type)
    {
        Type = type;
    }

    public override string ToString() => Type;
}