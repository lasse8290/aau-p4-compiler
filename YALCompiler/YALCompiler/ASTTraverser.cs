using System.Data;
using System.Reflection;
using YALCompiler.DataTypes;
using Boolean = YALCompiler.DataTypes.Boolean;

namespace YALCompiler;


public abstract class ASTTraverser
{
    private ASTNode _startNode;

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

    public void BeginTraverse()
    {
        traverse(_startNode);
    }
    
    protected virtual void traverse(ASTNode root)
    {
        if (root == null) {
            return;
        }

        Stack<ASTNode> stack = new Stack<ASTNode>();
        stack.Push(root);

        while (stack.Count > 0) {
            ASTNode node = stack.Pop();
            
            // var s = node
            //     .GetType()
            //     .GetProperties()
            //     .Where(p => typeof(ASTNode).IsAssignableFrom(p.PropertyType) && p.Name != "Parent")
            //     .ToList();
            //
            // foreach (PropertyInfo p in s)
            // {
            //     var pp = p.GetValue(node);
            //     if (pp is ASTNode astNode)
            //         astNode.Parent = node;
            // }

            callVisitor(node);

            if (node.Children != null) {
                for (int i = node.Children.Count - 1; i >= 0; i--) {
                    ASTNode child = node.Children[i];
                    //child.Parent = node; // Set the child's parent to the current node
                    stack.Push(child);
                }
            }
        }
    }
    private void _traverse(ASTNode node)
    {
        foreach (var child in node.Children)
        {
           callVisitor(child);
            
            _traverse(child);
        }
    }

    protected void callVisitor(ASTNode node)
    {
        switch (node)
            {
                case Boolean booleanNode:
                    visit(booleanNode);
                    break;
                case SignedFloat signedFloatNode:
                    visit(signedFloatNode);
                    break;
                case SignedNumber signedNumberNode:
                    visit(signedNumberNode);
                    break;
                case ExternalFunction externalFunctionNode:
                    visit(externalFunctionNode);
                    break;
                case ArrayElementIdentifier arrayElementIdentifierNode:
                    visit(arrayElementIdentifierNode);
                    break;
                case Identifier identifierNode:
                    visit(identifierNode);
                    break;
                case Predicate predicateNode:
                    visit(predicateNode);
                    break;
                case Function function:
                    visit(function);
                    break;
                case StringLiteral stringLiteralNode:
                    visit(stringLiteralNode);
                    break;
                case TupleDeclaration tupleDeclarationNode:
                    visit(tupleDeclarationNode);
                    break;
                case UnaryCompoundExpression unaryCompoundExpressionNode:
                    visit(unaryCompoundExpressionNode);
                    break;
                case VariableDeclaration variableDeclarationNode:
                    visit(variableDeclarationNode);
                    break;
                case UnaryAssignment unaryAssignmentNode:
                    visit(unaryAssignmentNode);
                    break;
                case BinaryAssignment binaryAssignmentNode:
                    visit(binaryAssignmentNode);
                    break;
                case IfStatement ifStmt:
                    visit(ifStmt);
                    break;
                case If ifNode:
                    visit(ifNode);
                    break;
                case Else elseNode:
                    visit(elseNode);
                    break;
                case ElseIf elseIfNode:
                    visit(elseIfNode);
                    break;
                case ForStatement forStatementNode:
                    visit(forStatementNode);
                    break;
                case WhileStatement whileStatementNode:
                    visit(whileStatementNode);
                    break;
                case ReturnStatement returnStatementNode:
                    visit(returnStatementNode);
                    break;
                case FunctionCall functionCallNode:
                    visit(functionCallNode);
                    break;
                case CompoundExpression compoundExpressionNode:
                    visit(compoundExpressionNode);
                    break;
                case Expression expressionNode:
                    visit(expressionNode);
                    break;
                case DataTypes.Program program:
                    visit(program);
                    break;
                case ASTNode astNode:
                    visit(astNode);
                    break;
                default:
                    throw new ArgumentException("Invalid child type");
            }
    }
}