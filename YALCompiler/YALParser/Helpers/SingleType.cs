using YALParser.Exceptions;

namespace YALParser.Helpers;

public class SingleType : YALType
{
    public Types.ValueType Type { get; set; }

    public SingleType(Types.ValueType type)
    {
        Type = type;
    }
    
    public SingleType(string type)
    {
        Types.ValueType? t = Types.Match(type);
        
        if (t is null)
            throw new TypeNotRecognizedException(type);
        
        Type = (Types.ValueType)t;
    }

    public override string ToString() => Type.ToString();
}