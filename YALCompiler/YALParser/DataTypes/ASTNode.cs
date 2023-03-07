using YALParser.Helpers;

namespace YALParser.DataTypes;

public abstract class ASTNode
{
    public List<ASTNode> Children { get; } = new();
    public ASTNode? Parent { get; set; } = default;
    public Dictionary<string, (object, YALType, bool)> SymbolTable { get; } = new(); // <ID, (Value, Type, Initialized)>
}