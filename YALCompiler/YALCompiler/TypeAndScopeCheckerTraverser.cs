using YALCompiler.DataTypes;
using YALCompiler.ErrorHandlers;
using YALCompiler.Exceptions;
using YALCompiler.Helpers;

namespace YALCompiler;

public class TypeAndScopeCheckerTraverser : ASTTraverser
{
    private readonly ErrorHandler _errorHandler;
    private readonly WarningsHandler _warningsHandler;
    public TypeAndScopeCheckerTraverser(ASTNode node, ErrorHandler errorHandler, WarningsHandler warningsHandler): base(node)
    {
        _errorHandler = errorHandler;
        _warningsHandler = warningsHandler;
    }
    
    internal override object? visit(BinaryAssignment node)
    {
        YALType? targetType = null;
        YALType? valueType = null;
        
        switch (node.Target)
        {
            case Identifier identifier:
                if (Utilities.FindSymbol(identifier.IdValue, node) is Symbol symbol)
                {
                    targetType = symbol.Type;
                }
                else
                {
                    _errorHandler.AddError(new IdentifierNotFoundException(identifier.IdValue), node.LineNumber);
                }

                break;
            case VariableDeclaration variableDeclaration:
                targetType = variableDeclaration.Variable.Type;
                break;
            case TupleDeclaration tupleDeclaration:
                targetType = new TupleType(tupleDeclaration.Variables.Select(v => (SingleType)v.Type).ToArray());
                break;
        }

        valueType = visit(node.Value) as YALType;

        if (targetType != valueType)
        {
            _errorHandler.AddError(new TypeMismatchException(valueType?.ToString() ?? "null", targetType.ToString()), node.LineNumber);
        }
        
        return valueType;

    }





    internal override object? visit(SignedNumber signedNumber)
    {
        SingleType type;
        if (signedNumber.Negative)
        {
            
        }
    }
}