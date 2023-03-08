using YALCompiler.Helpers;

namespace YALCompiler.DataTypes;

public class Function : ASTNode
{
    public bool IsAsync { get; set; }
    public string Id { get; set; }
    public Dictionary<string, string> InputParameters { get; } = new();
    public Dictionary<string, string> OutputParameters { get; } = new();
    public List<YALType> ReturnType { get; set; }
}