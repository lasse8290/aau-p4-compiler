using YALCompiler.Helpers;

namespace YALCompiler.DataTypes;

public class BinaryAssignment: Assignment
{
    public List<IAssignable> Targets { get; set; } = new();
    public List<Expression> Values { get; set; } = new();
    public override string ToString() => $"{string.Join(", ", Targets.Select(t => t.ToString()))} {Operator.ToStringValue()} {Value}";

}