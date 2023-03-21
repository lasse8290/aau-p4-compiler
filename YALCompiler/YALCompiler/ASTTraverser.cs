using System.Data;
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

    internal virtual object? visit(Boolean node) => node;
    internal virtual object? visit(SignedFloat node) => node;
    internal virtual object? visit(SignedNumber node) => node;
    internal virtual object? visit(Identifier node) => node;
    internal virtual object? visit(CompoundPredicate node) => node;
    internal virtual object? visit(Predicate node) => node;
    internal virtual object? visit(StringLiteral node) => node;
    internal virtual object? visit(TupleDeclaration node) => node;
    internal virtual object? visit(UnaryCompoundExpression node) => node;
    internal virtual object? visit(VariableDeclaration node) => node;
    internal virtual object? visit(StatementBlock node) => node;
    internal virtual object? visit(UnaryAssignment node) => node;
    internal virtual object? visit(BinaryAssignment node) => node;
    internal virtual object? visit(IfStatement node) => node;
    internal virtual object? visit(If node) => node;
    internal virtual object? visit(Else node) => node;
    internal virtual object? visit(ElseIf node) => node;
    internal virtual object? visit(ForStatement node) => node;
    internal virtual object? visit(WhileStatement node) => node;
    internal virtual object? visit(ReturnStatement node) => node;
    internal virtual object? visit(FunctionCall node) => node;
    internal virtual object? visit(ExternalFunction node) => node;
    internal virtual object? visit(ArrayElementIdentifier node) => node;
    internal virtual object? visit(CompoundExpression node) => node;
    internal virtual object? visit(Expression node) => node;
    internal virtual object? visit(Program node) => node;
    internal virtual object? visit(ASTNode node) => node;

    public virtual void BeginTraverse()
    {
        var stack = new Stack<ASTNode>();
        stack.Push(_startNode);

        while (stack.Count > 0)
        {
            var node = stack.Pop();
            callVisitor(node);
            foreach (var child in node.Children)
                stack.Push(child);
        }
    }

    public virtual void callVisitor(ASTNode node)
    {
        Type nodeType = node.GetType();
        MethodInfo visitMethod = GetType().GetMethod("visit", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, new Type[] { nodeType }, null);

        if (visitMethod != null)
            visitMethod.Invoke(this, new object[] { node });
    }
}