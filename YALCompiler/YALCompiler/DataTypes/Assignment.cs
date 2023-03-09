using static YALCompiler.Helpers.Operators;

namespace YALCompiler.DataTypes;

public class Assignment : ASTNode
{
    public object Target { get; set; }
    public AssignmentOperator Operator { get; set; } = AssignmentOperator.Equals;
}