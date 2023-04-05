using YALCompiler.Helpers;

namespace YALCompiler.DataTypes;

public class BinaryAssignment: Assignment
{
    public Expression Value { get; set; }
    public override string ToString() => $"{Target} {Operator.ToStringValue()} {Value}";

}