using YALCompiler.DataTypes;

namespace YALCompiler.Helpers;

public static class Extensions
{
    public static void LinkChildrenNodesToParent(this ASTNode node)
    {
        foreach (var child in node.Children)
        {
            child.Parent = node;
            child.LinkChildrenNodesToParent();
        }
    }
}