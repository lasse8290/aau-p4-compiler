using System.Text;
using Microsoft.Extensions.Primitives;

namespace YALCompiler.Helpers;

public class YALType: IEquatable<YALType>
{
    public List<(Types.ValueType Type, bool IsArray)> Types { get; }= new();

    public YALType()
    {
        
    }

    public YALType(params (Types.ValueType type, bool isArray)[] types)
    {
        Types.AddRange(types);
    }
    
    public YALType(params YALType[] types)
    {
        foreach (var type in types)
        {
            Types.AddRange(type.Types);
        }
    }
    
    public YALType(Types.ValueType type)
    {
        Types.Add((type, false));
    }
    
    public YALType(Types.ValueType type, bool isArray)
    {
        Types.Add((type, isArray));
    }

    public YALType(string type, bool isArray = false)
    {
        Types.ValueType? t = YALCompiler.Helpers.Types.Match(type); 
        if (t is not null) Types.Add((t, isArray));
        throw new Exception($"Invalid type '{type}'");
    }

    public bool Equals(YALType other)
    {
        return Types.SequenceEqual(other.Types);
    }

    public static bool operator ==(YALType first, YALType second)
    {
        if (first is null || second is null)
            return false;
        
        return first.Equals(second);
    }
    public static bool operator !=(YALType first, YALType second)
    {
        if (first is null || second is null)
            return true;
        
        return !first.Equals(second);
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        if (Types.Count > 1)
            sb.Append("(");
        sb.Append(string.Join(", ", Types.Select(t => t.Type + (t.IsArray ? "[]" : ""))));
        if (Types.Count > 1)
            sb.Append("}");
        return sb.ToString();
    }
}