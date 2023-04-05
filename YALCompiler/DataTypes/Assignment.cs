using static YALCompiler.Helpers.Operators;

namespace YALCompiler.DataTypes;

public class Assignment : Expression
{
    public ASTNode Target { get; set; }
    public AssignmentOperator Operator { get; set; } = AssignmentOperator.Equals;

    public override string ToString() => Operator switch
    {
        AssignmentOperator.PreDecrement => $"{Operator.ToStringValue()}{Target}",
        AssignmentOperator.PreIncrement => $"{Operator.ToStringValue()}{Target}",
        AssignmentOperator.BitwiseNot => $"{Operator.ToStringValue()}{Target}",
        _ => $"{Target}{Operator.ToStringValue()}",
    };
}