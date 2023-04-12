using System.Reflection;
using YALCompiler.DataTypes;

namespace YALCompiler;

public class LinkerASTTraverser : ASTTraverser
{
    Action<ASTNode, ASTNode> action;

    public LinkerASTTraverser(ASTNode node, Action<ASTNode, ASTNode> action) : base(node) {
        this.action = action;
    }
    
    public override void BeginTraverse()
    {
        if (_startNode == null) {
            return;
        }

        Stack<ASTNode> stack = new Stack<ASTNode>();
        stack.Push(_startNode);

        while (stack.Count > 0) {
            ASTNode node = stack.Pop();
            
            var properties = GetNodeChildProperties(node.GetType());

            foreach (PropertyInfo p in properties)
            {
                var pp = p.GetValue(node) as ASTNode;
                if (pp != null)
                {
                    action(node, pp);
                    stack.Push(pp);
                }
            }

            if (node.Children != null) {
                for (int i = node.Children.Count - 1; i >= 0; i--) {
                    ASTNode child = node.Children[i];
                    action(node, child);
                    stack.Push(child);
                }
            }
        }
    }
}