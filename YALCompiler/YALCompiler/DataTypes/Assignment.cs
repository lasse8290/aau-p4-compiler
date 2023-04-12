using static YALCompiler.Helpers.Operators;

namespace YALCompiler.DataTypes;

public class Assignment : Expression
{
    public AssignmentOperator Operator { get; set; } = AssignmentOperator.Equals;
}