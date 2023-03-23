using YALCompiler.Helpers;
using static YALCompiler.Helpers.Operators;

namespace YALCompiler.DataTypes;

public class Boolean: Predicate
{
    public YALType Type { get; } = new SingleType(Types.ValueType.@bool);
    public bool? LiteralValue { get; set; } = null;
}