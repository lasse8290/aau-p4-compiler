using YALCompiler.Helpers;

namespace YALCompiler.DataTypes;

public class Function : ASTNode, ISymbol
{
    public bool IsAsync { get; set; }
    public string Id { get; set; }
    public List<Symbol> InputParameters { get; } = new();
    public List<Symbol> OutputParameters { get; } = new();
    public YALType ReturnType { get; set; }
}