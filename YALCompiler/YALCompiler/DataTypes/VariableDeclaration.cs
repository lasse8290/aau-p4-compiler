using YALCompiler.Helpers;

namespace YALCompiler.DataTypes;

public class VariableDeclaration : ASTNode
{
    public Symbol Variable { get; set; }
}