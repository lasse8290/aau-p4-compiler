namespace YALCompiler.DataTypes;

public class If: ASTNode
{
    public Expression Predicate { get; set; }
}