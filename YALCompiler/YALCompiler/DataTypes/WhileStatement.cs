namespace YALCompiler.DataTypes;

public class WhileStatement: ASTNode
{
    public Predicate Predicate { get; set; }
}