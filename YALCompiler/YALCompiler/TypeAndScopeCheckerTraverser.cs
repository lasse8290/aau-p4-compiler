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
    
    internal override object? Visit(BinaryAssignment node)
    {
        YALType? targetType = null;
        YALType? targetParentArrayType = null;
        YALType? valueType = null;
        
        switch (node.Target)
        {
            case Identifier identifier:
                if (CompilerUtilities.FindSymbol(identifier.IdValue, node) is Symbol symbol)
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

        valueType = Visit(node.Value) as YALType;

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

    internal override object? Visit(UnaryAssignment node)
    {
        YALType? targetType = null;
        if (node.Target is Identifier identifier)
        {
            if (CompilerUtilities.FindSymbol(identifier.IdValue, node) is Symbol symbol)
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

    internal override object? Visit(FunctionCall node)
    {
        Function? function = CompilerUtilities.FindFunction(node.Identifier, node);
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
            if ((YALType)function.InputParameters[i].Type != (YALType)Visit(node.InputParameters[i]))
            {
                _errorHandler.AddError(
                    new InvalidFunctionCallInputParameters(
                        function.InputParameters.Select(s => s.Type as SingleType).ToList(),
                        node.InputParameters.Select(s => Visit(s) as SingleType).ToList()),
                    node.LineNumber);
                break;
            }
        }

        node.Function = function;
        return function.ReturnType;
    }

    internal override object? Visit(SignedNumber node)
    {
        SingleType? type = node.Negative switch
        {
            true => node.Value switch
            {
                <= sbyte.MaxValue + 1 => new SingleType(Types.ValueType.int8),
                <= short.MaxValue + 1 => new SingleType(Types.ValueType.int16),
                <= (ulong)int.MaxValue + 1 => new SingleType(Types.ValueType.int32),
                <= (ulong)long.MaxValue + 1 => new SingleType(Types.ValueType.int64),
                _ => null,
            },
            _ => node.Value switch
            {
                <= byte.MaxValue => new SingleType(Types.ValueType.uint8),
                <= ushort.MaxValue => new SingleType(Types.ValueType.uint16),
                <= uint.MaxValue => new SingleType(Types.ValueType.uint32),
                <= ulong.MaxValue => new SingleType(Types.ValueType.uint64),
            }
        };
        
        if (type is null)
            _errorHandler.AddError(new SignedLongOutOfRangeException(node.Value), node.LineNumber);
        
        return type;
    }
    
    internal override object? Visit(SignedFloat node)
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
    
    internal override object? Visit(Expression node)
    {
        Type nodeType = node.GetType();
        MethodInfo? visitMethod = GetType().GetMethod(nameof(Visit), BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, new Type[] { nodeType }, null);

        if (visitMethod != null)
        {
            return visitMethod.Invoke(this, new object[] { node });
        }
        else
        {
            //throw new ArgumentException($"No matching Visit method found for type {nodeType.Name}");
            return null;
        }
    }

    internal override object? Visit(Identifier node)
    {
        if (CompilerUtilities.FindSymbol(node.IdValue, node) is Symbol symbol)
        {
            return symbol.Type;
        } else if (CompilerUtilities.FindFunction(node.IdValue, node) is Function function)
        {
            return function.ReturnType;
        }
        else
        {
            _errorHandler.AddError(new IdentifierNotFoundException(node.IdValue), node.LineNumber);
            return null;
        }
    }

    internal override object? Visit(CompoundExpression node)
    {
        YALType? leftType = Visit(node.Left) as YALType;
        YALType? rightType = Visit(node.Right) as YALType;

        if (!Types.CheckCompoundExpressionTypesAreValid(leftType, rightType))
        {
            _errorHandler.AddError(new TypeMismatchException(leftType.ToString(), rightType.ToString()), node.LineNumber);
        }
        
        SingleType leftSingleType = (SingleType)leftType;
        SingleType rightSingleType = (SingleType)rightType;

        if (!Operators.CheckOperationIsValid(leftSingleType.Type, node.Operator))
        {
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, leftSingleType.Type), node.LineNumber);
        } else if (!Operators.CheckOperationIsValid(rightSingleType.Type, node.Operator))
        {
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, rightSingleType.Type), node.LineNumber);
        }
        
        node.Type = Types.GetLeastAssignableType(leftSingleType, rightSingleType);
        return node.Type;
    }

    internal override object? Visit(StringLiteral node)
    {
        return new SingleType(Types.ValueType.@string);
    }
    
    internal override object? Visit(CompoundPredicate node)
    {
        YALType? leftType = Visit(node.Left) as YALType;
        YALType? rightType = Visit(node.Right) as YALType;

        if (leftType is null || rightType is null)
        {
            _errorHandler.AddError(new TypeMismatchException(leftType?.ToString() ?? "null", rightType?.ToString() ?? "null"), node.LineNumber);
            return null;
        }

        // if (!Types.CheckCompoundExpressionTypesAreValid(leftType, rightType))
        // {
        //     _errorHandler.AddError(new TypeMismatchException(leftType?.ToString() ?? "null", rightType?.ToString() ?? "null"), node.LineNumber);
        // }

        SingleType leftSingleType = (SingleType)leftType;
        SingleType rightSingleType = (SingleType)rightType;

        if (!Operators.CheckOperationIsValid(leftSingleType.Type, node.Operator))
        {
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, leftSingleType.Type), node.LineNumber);
            return null;
        } else if (!Operators.CheckOperationIsValid(rightSingleType.Type, node.Operator))
        {
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, rightSingleType.Type), node.LineNumber);
            return null;
        }
        return new SingleType(Types.ValueType.@bool);
    }

    internal override object? Visit(DataTypes.Boolean node) => new SingleType(Types.ValueType.@bool);
    
    internal override object? Visit(Predicate node) => new SingleType(Types.ValueType.@bool);

    internal override object? Visit(ArrayLiteral node)
    {
        if (node.Values.Count == 0)
        {
            return null;
        }
        SingleType leastAssignableType = (SingleType)Visit(node.Values[0]);
        for (int i = 1; i < node.Values.Count; i++)
        {
            SingleType type = (SingleType)Visit(node.Values[i]);
            leastAssignableType = Types.GetLeastAssignableType(leastAssignableType, type);
        }

        leastAssignableType.IsArray = true;
        return leastAssignableType;
    }

}