using YALCompiler.Helpers;

namespace YALCompiler.DataTypes;

public class VariableDeclaration : ASTNode
{
    public List<Symbol> Variable { get; } = new();
}