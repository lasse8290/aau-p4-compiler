﻿using System.Data;
using System.Reflection;
using YALCompiler.DataTypes;
using Boolean = YALCompiler.DataTypes.Boolean;

namespace YALCompiler;


public abstract class ASTTraverser
{
    protected ASTNode _startNode;

    public ASTTraverser(ASTNode node)
    {
        _startNode = node;
    }

    internal virtual object? Visit(Boolean                 node) => node;
    internal virtual object? Visit(SignedFloat             node) => node;
    internal virtual object? Visit(SignedNumber            node) => node;
    internal virtual object? Visit(Identifier              node) => node;
    internal virtual object? Visit(CompoundPredicate       node) => node;
    internal virtual object? Visit(Predicate               node) => node;
    internal virtual object? Visit(StringLiteral           node) => node;
    internal virtual object? Visit(ArrayLiteral            node) => node;
    internal virtual object? Visit(TupleDeclaration        node) => node;
    internal virtual object? Visit(UnaryCompoundExpression node) => node;
    internal virtual object? Visit(VariableDeclaration     node) => node;
    internal virtual object? Visit(StatementBlock          node) => node;
    internal virtual object? Visit(UnaryAssignment         node) => node;
    internal virtual object? Visit(BinaryAssignment        node) => node;
    internal virtual object? Visit(IfStatement             node) => node;
    internal virtual object? Visit(If                      node) => node;
    internal virtual object? Visit(Else                    node) => node;
    internal virtual object? Visit(ElseIf                  node) => node;
    internal virtual object? Visit(ForStatement            node) => node;
    internal virtual object? Visit(WhileStatement          node) => node;
    internal virtual object? Visit(ReturnStatement         node) => node;
    internal virtual object? Visit(FunctionCall            node) => node;
    internal virtual object? Visit(ExternalFunction        node) => node;
    internal virtual object? Visit(ArrayElementIdentifier  node) => node;
    internal virtual object? Visit(CompoundExpression      node) => node;
    internal virtual object? Visit(Expression              node) => node;
    internal virtual object? Visit(YALCompiler.DataTypes.Program                 node) => node;
    internal virtual object? Visit(ASTNode                 node) => node;
    internal virtual object? Visit(Function                node) => node;

    private static readonly Dictionary<Type, List<PropertyInfo>> _propertyCache    = new();
    private readonly        Dictionary<Type, MethodInfo?>        _visitMethodCache = new();
    
    public virtual void BeginTraverse() 
    {
        var stack = new Stack<ASTNode>();
        stack.Push(_startNode);

        while (stack.Count > 0)
        {
            var node = stack.Pop();
            InvokeVisitor(node);

            var properties = GetNodeChildProperties(node.GetType());

            foreach (var property in properties)
            {
                var childNode = property.GetValue(node) as ASTNode;
                if (childNode != null)
                {
                    childNode.Parent = node;
                    stack.Push(childNode);
                }
            }

            for (int i = node.Children.Count - 1; i >= 0; i--)
                stack.Push(node.Children[i]);
        }
    }

    protected virtual object? InvokeVisitor(ASTNode node)
    {
        Type        nodeType    = node.GetType();
        MethodInfo? visitMethod = GetVisitMethodForNodeType(nodeType);
        if (visitMethod != null)
            return visitMethod.Invoke(this, new object[] { node });

        return node;
    }
    
    private MethodInfo? GetVisitMethodForNodeType(Type nodeType)
    {
        if (!_visitMethodCache.TryGetValue(nodeType, out var visitMethod))
        {
            visitMethod                 = GetType().GetMethod(nameof(Visit), BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, new Type[] { nodeType }, null);
            _visitMethodCache[nodeType] = visitMethod;
        }

        return visitMethod;
    }
    
    internal static List<PropertyInfo> GetNodeChildProperties(Type nodeType)
    {
        if (!_propertyCache.TryGetValue(nodeType, out var properties))
        {
            properties = nodeType
                         .GetProperties()
                         .Where(p => typeof(ASTNode).IsAssignableFrom(p.PropertyType) && p.Name != "Parent")
                         .ToList();

            _propertyCache[nodeType] = properties;
        }

        return properties;
    }

}