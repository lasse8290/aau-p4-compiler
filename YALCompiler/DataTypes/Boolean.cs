using YALCompiler.Helpers;

namespace YALCompiler.DataTypes;

public class Boolean: Predicate
{
    public YALType Type { get; } = new YALType(Types.ValueType.@bool);
    public bool? LiteralValue { get; set; } = null;
}