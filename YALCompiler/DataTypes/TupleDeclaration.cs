using YALCompiler.Helpers;

namespace YALCompiler.DataTypes;

public class TupleDeclaration : ASTNode
{
    public List<Symbol> Variables { get; set; } = new();
}