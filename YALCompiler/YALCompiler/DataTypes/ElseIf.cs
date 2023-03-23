namespace YALCompiler.DataTypes;

public class ElseIf: ASTNode
{
    public Predicate Predicate { get; set; }
}