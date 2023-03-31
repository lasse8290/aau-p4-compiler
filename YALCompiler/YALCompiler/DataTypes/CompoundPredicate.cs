using YALCompiler.Helpers;
using static YALCompiler.Helpers.Operators;

namespace YALCompiler.DataTypes;

public class CompoundPredicate: Boolean
{
    public PredicateOperator Operator { get; set; }
    public Expression Left { get; set; }
    public Expression Right { get; set; }
    public override string ToString()
    {
        return $"({Left} {Operator} {Right})";
    }
}