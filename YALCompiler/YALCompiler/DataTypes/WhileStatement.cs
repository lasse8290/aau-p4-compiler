namespace YALCompiler.DataTypes;

public class WhileStatement: ASTNode
{
    public Expression Predicate { get; set; }
}