namespace YALCompiler.DataTypes;

public class If: ASTNode
{
    public Predicate Predicate { get; set; }
}