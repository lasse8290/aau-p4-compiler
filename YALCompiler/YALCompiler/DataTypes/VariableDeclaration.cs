using YALCompiler.Helpers;

namespace YALCompiler.DataTypes;

public class VariableDeclaration : ASTNode, IAssignable
{
    public Symbol Variable { get; set; }
}