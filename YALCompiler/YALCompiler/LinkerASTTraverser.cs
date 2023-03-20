﻿using System.Reflection;
using YALCompiler.DataTypes;

namespace YALCompiler;

public class LinkerASTTraverser : ASTTraverser
{
    public LinkerASTTraverser(ASTNode node) : base(node)
    {
    }
    
    protected override void traverse(ASTNode root)
    {
        if (root == null) {
            return;
        }

        Stack<ASTNode> stack = new Stack<ASTNode>();
        stack.Push(root);

        while (stack.Count > 0) {
            ASTNode node = stack.Pop();
            
            var s = node
                .GetType()
                .GetProperties()
                .Where(p => typeof(ASTNode).IsAssignableFrom(p.PropertyType) && p.Name != "Parent")
                .ToList();
            
            foreach (PropertyInfo p in s)
            {
                var pp = p.GetValue(node);
                if (pp is ASTNode astNode)
                {
                    astNode.Parent = node;
                    stack.Push(astNode);
                }
            }

            callVisitor(node);

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