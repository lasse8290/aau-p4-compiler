using YALCompiler.Helpers;

namespace YALCompiler.DataTypes;

public class StatementBlock
{
    public List<ASTNode> Statements { get; set; } = new();
    public List<Symbol> LocalVariables { get; } = new();
}