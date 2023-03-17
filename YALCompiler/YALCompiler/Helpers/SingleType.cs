using YALCompiler.Exceptions;

namespace YALCompiler.Helpers;

public class SingleType : YALType
{
    public Types.ValueType Type { get; set; }
    public bool IsArray { get; set; } = false;

    public SingleType(Types.ValueType type, bool isArray = false)
    {
        Type = type;
        IsArray = isArray;
    }
    
    public SingleType(string type, bool isArray = false)
    {
        Types.ValueType? t = Types.Match(type);
        
        if (t is null)
            throw new TypeNotRecognizedException(type);
        
        Type = (Types.ValueType)t;
        IsArray = isArray;
    }

    public override string ToString() => Type.ToString();
}