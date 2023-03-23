namespace YALCompiler.DataTypes;

public class ForStatement : ASTNode
{
    public BinaryAssignment DeclarationAssignment { get; set; }
    public Predicate RunCondition { get; set; }
    public Assignment LoopAssignment { get; set; }
}