namespace YALCompiler.DataTypes;

public class Expression : ASTNode
{
    public bool Negated { get; set; } = false;
    public bool BitwiseNegated { get; set; } = false;
}