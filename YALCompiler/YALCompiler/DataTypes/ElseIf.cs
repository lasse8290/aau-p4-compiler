namespace YALCompiler.DataTypes;

public class ElseIf: ASTNode
{
    public Expression Predicate { get; set; }
}