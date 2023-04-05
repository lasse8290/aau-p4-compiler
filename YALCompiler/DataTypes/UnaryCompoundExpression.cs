using YALCompiler.Helpers;
using static YALCompiler.Helpers.Operators;

namespace YALCompiler.DataTypes;

public class UnaryCompoundExpression : Expression
{
    public YALType Type { get; set; }
    public ExpressionOperator Operator { get; set; }
    public Expression Expression { get; set; }
}