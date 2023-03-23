using YALCompiler.Helpers;
using static YALCompiler.Helpers.Operators;

namespace YALCompiler.DataTypes;

public class CompoundExpression : Expression
{
    public YALType Type { get; set; } = new SingleType(Types.ValueType.@bool);
    public ExpressionOperator Operator { get; set; }
    public Expression Left { get; set; }
    public Expression Right { get; set; }
}