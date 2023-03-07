using YALCompiler.Helpers;

namespace YALCompiler.DataTypes;

public abstract class ASTNode
{
    public List<ASTNode> Children { get; } = new();
    public ASTNode? Parent { get; set; } = default;
    public SymbolTable SymbolTable { get; } = new();
}