using YALParser.Helpers;

namespace YALParser.DataTypes;

public abstract class ASTNode
{
    public List<ASTNode> Children { get; } = new();
    public ASTNode? Parent { get; set; } = default;
    public Dictionary<string, (object, YALType)> SymbolTable { get; } = new(); // <ID, (Value, Type)>
}