﻿using System.Reflection;
using YALCompiler.DataTypes;

namespace YALCompiler;

/// <summary>
/// Linking all nodes to their parents
/// </summary>
public class LinkerASTTraverser : ASTTraverser
{
    public LinkerASTTraverser(ASTNode node) : base(node) { }
    
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
                    pp.Parent = node;
                    stack.Push(pp);
                }
            }

            if (node.Children != null) {
                for (int i = node.Children.Count - 1; i >= 0; i--) {
                    ASTNode child = node.Children[i];
                    child.Parent = node; // Set the child's parent to the current node
                    stack.Push(child);
                }
            }
        }
    }
}