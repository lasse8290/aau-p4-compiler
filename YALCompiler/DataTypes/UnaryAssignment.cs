using YALCompiler.Helpers;

namespace YALCompiler.DataTypes;

public class UnaryAssignment : Assignment
{
    public Expression Target { get; set; }
    public override string ToString() => Operator switch
    {
        Operators.AssignmentOperator.PreDecrement => $"{Operator.ToStringValue()}{Target}",
        Operators.AssignmentOperator.PreIncrement => $"{Operator.ToStringValue()}{Target}",
        Operators.AssignmentOperator.BitwiseNot => $"{Operator.ToStringValue()}{Target}",
        _ => $"{Target}{Operator.ToStringValue()}",
    };
}