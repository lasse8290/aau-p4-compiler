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
    internal virtual object? visit(If node) => node;
    internal virtual object? visit(Else node) => node;
    internal virtual object? visit(ElseIf node) => node;
    internal virtual object? visit(ForStatement node) => node;
    internal virtual object? visit(WhileStatement node) => node;
    internal virtual object? visit(ReturnStatement node) => node;
    internal virtual object? visit(FunctionCall node) => node;
    internal virtual object? visit(ExternalFunction node) => node;
    internal virtual object? visit(ArrayElementIdentifier node) => node;
    internal virtual object? visit(Expression node) => node;

    internal virtual object? visit(ASTNode node)
    {
        
        // Type objType = node.GetType();
        //
        // dynamic specializedObj = Convert.ChangeType(node, objType);
        //
        // return visit(specializedObj);

        // Type nodeType = node.GetType();
        // MethodInfo visitMethod = GetType().GetMethod("visit", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, new Type[] { nodeType }, null);
        // dynamic specializedObj = Convert.ChangeType(node, nodeType);
        //
        // if (visitMethod != null)
        // {
        //     return visitMethod.Invoke(this, new object[] { specializedObj });
        // }
        // else
        // {
        //     throw new ArgumentException($"No matching Visit method found for type {nodeType.Name}");
        // }

        return node;
    }

    public void BeginTraverse()
    {
        _traverse(_startNode);
    }
    
    private void _traverse(ASTNode node)
    {
        foreach (var child in node.Children)
        {
            
           //visit(child);

            switch (child)
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
                case Expression expressionNode:
                    visit(expressionNode);
                    break;
                default:
                    throw new ArgumentException("Invalid child type");
            }
            
            _traverse(child);
        }
    }
}