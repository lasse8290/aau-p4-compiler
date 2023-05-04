using YALCompiler.Helpers;

namespace YALCompiler.DataTypes;

public class Function : ASTNode, ISymbol
{
    public bool IsAsync { get; set; }
    public string Name { get; set; }
    public List<Symbol> InputParameters { get; set; } = new();
    public List<Symbol> OutputParameters { get; set; } = new();
    public YALType ReturnType { get; set; } = new();
}