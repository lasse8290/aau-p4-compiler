using System.ComponentModel;
using System.Reflection;
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
        YALType? targetParentArrayType = null;
        YALType? valueType = null;
        
        switch (node.Target)
        {
            case Identifier identifier:
                if (Utilities.FindSymbol(identifier.IdValue, node) is Symbol symbol)
                {
                    targetType = symbol.Type;
                    if (identifier is ArrayElementIdentifier arrayElementIdentifier)
                    {
                        targetParentArrayType = symbol.Type;
                        ((SingleType)targetType).IsArray = false;
                        if (symbol.ArraySize is not null && 
                            arrayElementIdentifier.Index is SignedNumber index && 
                            index.Value > symbol.ArraySize!)
                        {
                            _errorHandler.AddError(new ArrayIndexOutOfBoundsException(
                                index.Value, symbol.ArraySize.Value), node.LineNumber);
                        }
                    }
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

        if (!Types.CheckTypesAreAssignable(targetType, valueType))
        {
            _errorHandler.AddError(new TypeMismatchException(valueType?.ToString() ?? "null",
                targetType?.ToString() ?? "null"), node.LineNumber);
        }

        switch (targetType)
        {
            case SingleType singleType:
                if (!Operators.CheckOperationIsValid(singleType.Type, node.Operator))
                {
                    _errorHandler.AddError(new InvalidOperatorException(node.Operator, singleType.Type), node.LineNumber);
                }
                
                break;
            case TupleType tupleType:
                if (node.Operator != Operators.AssignmentOperator.Equals)
                {
                    _errorHandler.AddError(new InvalidOperatorException(node.Operator, tupleType), node.LineNumber);
                }

                break;
        }

        return valueType;

    }

    internal override object? visit(UnaryAssignment node)
    {
        YALType? targetType = null;
        if (node.Target is Identifier identifier)
        {
            if (Utilities.FindSymbol(identifier.IdValue, node) is Symbol symbol)
            {
                targetType = symbol.Type;
                switch (targetType)
                {
                    case SingleType singleType:
                        if (!Operators.CheckOperationIsValid(singleType.Type, node.Operator))
                        {
                            _errorHandler.AddError(new InvalidOperatorException(node.Operator, singleType.Type), node.LineNumber);
                        }

                        break;
                    case TupleType tupleType:
                        _errorHandler.AddError(new InvalidOperatorException(node.Operator, tupleType), node.LineNumber);
                        break;
                }
            }
            else
            {
                _errorHandler.AddError(new IdentifierNotFoundException(identifier.IdValue), node.LineNumber);
            }
        }
        else
        {
            _errorHandler.AddError(new InvalidAssignment(node), node.LineNumber);
        }

        return targetType;
    }

    internal override object? visit(FunctionCall node)
    {
        Function? function = Utilities.FindFunction(node.Identifier, node);
        if (function == null)
        {
            _errorHandler.AddError(new IdentifierNotFoundException(node.Identifier), node.LineNumber);
            return null;
        }
        
        //check input params are correct too
        if (function.InputParameters.Count != node.InputParameters.Count)
        {
            _errorHandler.AddError(
                new InvalidFunctionCallInputParameters(
                    function.InputParameters.Count, node.InputParameters.Count),
                node.LineNumber);
        }
        
        for (int i = 0; i < function.InputParameters.Count; i++)
        {
            if ((YALType)function.InputParameters[i].Type != (YALType)visit(node.InputParameters[i]))
            {
                _errorHandler.AddError(
                    new InvalidFunctionCallInputParameters(
                        function.InputParameters.Select(s => s.Type as SingleType).ToList(),
                        node.InputParameters.Select(s => visit(s) as SingleType).ToList()),
                    node.LineNumber);
                break;
            }
        }
        
        return function.ReturnType;
    }


    internal override object? visit(SignedNumber node)
    {
        SingleType type = node.Negative switch
        {
            true => node.Value switch
            {
                <= sbyte.MaxValue + 1 => new SingleType(Types.ValueType.int8),
                <= short.MaxValue + 1 => new SingleType(Types.ValueType.int16),
                <= (ulong)int.MaxValue + 1 => new SingleType(Types.ValueType.int32),
                <= (ulong)long.MaxValue + 1 => new SingleType(Types.ValueType.int64),
            },
            _ => node.Value switch
            {
                <= byte.MaxValue => new SingleType(Types.ValueType.uint8),
                <= ushort.MaxValue => new SingleType(Types.ValueType.uint16),
                <= uint.MaxValue => new SingleType(Types.ValueType.uint32),
                <= ulong.MaxValue => new SingleType(Types.ValueType.uint64),
            }
        };
        return type;
    }
    
    internal override object? visit(SignedFloat node)
    {
        SingleType type;
        
        if (node.Value <= float.MaxValue && node.Value >= float.MinValue)
        {
            type = new SingleType(Types.ValueType.float32);
        }
        else
        {
            type = new SingleType(Types.ValueType.float64);
        }
        
        return type;
    }
    
    // internal override object? visit(Expression node) => node switch
    // {
    //     ArrayElementIdentifier arrayElementIdentifier => visit(arrayElementIdentifier),
    //     BinaryAssignment binaryAssignment => visit(binaryAssignment),
    //     
    //     SignedNumber signedNumber => visit(signedNumber),
    //     SignedFloat signedFloat => visit(signedFloat),
    //     Identifier identifier => visit(identifier),
    //     FunctionCall functionCall => visit(functionCall),
    //     UnaryAssignment unaryAssignment => visit(unaryAssignment),
    //     
    // };
    
    internal override object? visit(Expression node)
    {
        Type nodeType = node.GetType();
        MethodInfo visitMethod = GetType().GetMethod("visit", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, new Type[] { nodeType }, null);

        if (visitMethod != null)
        {
            return visitMethod.Invoke(this, new object[] { node });
        }
        else
        {
            throw new ArgumentException($"No matching Visit method found for type {nodeType.Name}");
        }
    }

    internal override object? visit(Identifier node)
    {
        if (Utilities.FindSymbol(node.IdValue, node) is Symbol symbol)
        {
            return symbol.Type;
        } else if (Utilities.FindFunction(node.IdValue, node) is Function function)
        {
            return function.ReturnType;
        }
        else
        {
            _errorHandler.AddError(new IdentifierNotFoundException(node.IdValue), node.LineNumber);
            return null;
        }
    }

    internal override object? visit(CompoundExpression node)
    {
        SingleType? leftType = visit(node.Left) as SingleType;
        SingleType? rightType = visit(node.Right) as SingleType;

        if (!Operators.CheckOperationIsValid(leftType.Type, node.Operator))
        {
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, leftType.Type), node.LineNumber);
        } else if (!Operators.CheckOperationIsValid(rightType.Type, node.Operator))
        {
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, rightType.Type), node.LineNumber);
        }

        node.Type = rightType;
        return node.Type;
    }


}