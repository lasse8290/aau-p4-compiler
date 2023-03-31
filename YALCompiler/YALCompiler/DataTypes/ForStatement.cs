namespace YALCompiler.DataTypes;

public class ForStatement : ASTNode
{
    public BinaryAssignment DeclarationAssignment { get; set; }
    public Expression RunCondition { get; set; }
    public Assignment LoopAssignment { get; set; }
}