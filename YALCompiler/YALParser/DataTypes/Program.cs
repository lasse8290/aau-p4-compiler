namespace YALParser.DataTypes;

public class Program : ASTNode
{
    public List<Function> Functions { get; } = new();
}