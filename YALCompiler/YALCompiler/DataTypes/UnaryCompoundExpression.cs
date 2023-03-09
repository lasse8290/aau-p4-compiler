using YALCompiler.Helpers;
using static YALCompiler.Helpers.Operators;

namespace YALCompiler.DataTypes;

public class UnaryCompoundExpression : Expression
{
    public YALType Type { get; } = new SingleType(Types.ValueType.@bool);
    public ExpressionOperator Operator { get; set; }
    public Expression Expression { get; set; }
}