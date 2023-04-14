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
                if (CompilerUtilities.FindSymbol(identifier.Name, node) is Symbol symbol)
                {
                    targetType = symbol.Type;
                    symbol.Initialized = true;
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

                    symbol.Initialized = true;
                }
                else
                {
                    _errorHandler.AddError(new IdentifierNotFoundException(identifier.Name), node.LineNumber);
                }

                break;
            case VariableDeclaration variableDeclaration:
                targetType = variableDeclaration.Variable.Type;
                variableDeclaration.Variable.Initialized = true;
                break;
            case TupleDeclaration tupleDeclaration:
                targetType = new TupleType(tupleDeclaration.Variables.Select(v => (SingleType)v.Type).ToArray());
                foreach (var variable in tupleDeclaration.Variables)
                {
                    variable.Initialized = true;
                }
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
        switch (node.Target)
        {
            case Identifier identifier:
                if (CompilerUtilities.FindSymbol(identifier.Name, node) is Symbol symbol)
                {
                    targetType = symbol.Type;
                    symbol.Initialized = true;
                }
                else
                {
                    _errorHandler.AddError(new IdentifierNotFoundException(identifier.Name), node.LineNumber);
                }

                break;
            default:
                _errorHandler.AddError(new InvalidAssignment(node), node.LineNumber);
                return targetType;
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
                _errorHandler.AddError(new InvalidOperatorException(node.Operator, tupleType), node.LineNumber);
                break;
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
        
        List<SingleType?> actualParams = new();
        List<string> actualParamTypes = new();
        bool hasError = false;
        for (int i = 0; i < function.InputParameters.Count; i++)
        {
            var actualParam = Visit(node.InputParameters[i]) as SingleType;
            actualParams.Add(actualParam);
            actualParamTypes.Add((node.InputParameters[i].IsRef ? "ref " : "") + (actualParam?.ToString() ?? "null"));
            if (!Types.CheckTypesAreAssignable(function.InputParameters[i].Type, actualParam) ||
                function.InputParameters[i].IsRef != node.InputParameters[i].IsRef)
            {
                hasError = true;
            }
        }

        if (hasError)
        {
            _errorHandler.AddError(
                new InvalidFunctionCallInputParameters(
                    function.InputParameters,
                    actualParamTypes),
                node.LineNumber);    
        }
        
        
        
        //check if await is used that it is within an async function
        if (node.Await)
        {
            Function? parentFunction = null;
            ASTNode? tempNode = node;
            while (parentFunction is null && tempNode is not null)
            {
                tempNode = tempNode.Parent;
                if (tempNode is Function functionNode)
                    parentFunction = functionNode;
            }

            if (parentFunction is not null && !parentFunction.IsAsync)
                _errorHandler.AddError(new InvalidAwaitException(), node.LineNumber);

            if (!function.IsAsync)
                _errorHandler.AddError(new CannotAwaitNonAsyncFunctionException(), node.LineNumber);
        }
        else if (function.IsAsync && !node.Parent.Children.Contains(node))
        {
            _errorHandler.AddError(new CannotUseAsyncFunctionAsExpressionWithoutAwaitException(), node.LineNumber);
        }
        
        node.Function = function;
        return function.ReturnType;
    }

    internal override object? Visit(SignedNumber node)
    {
        YALType? type = node.Negative switch
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
                <= byte.MaxValue => node.Value <= (ulong)sbyte.MaxValue ? new SingleType(Types.ValueType.int8) : new SingleType(Types.ValueType.uint8),
                <= ushort.MaxValue => node.Value <= (ulong)short.MaxValue ? new SingleType(Types.ValueType.int16) : new SingleType(Types.ValueType.uint16),
                <= uint.MaxValue => node.Value <= int.MaxValue ? new SingleType(Types.ValueType.int32) : new SingleType(Types.ValueType.uint32),
                <= ulong.MaxValue => node.Value <= long.MaxValue ? new SingleType(Types.ValueType.int64) : new SingleType(Types.ValueType.uint64),
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
        if (CompilerUtilities.FindSymbol(node.Name, node) is Symbol symbol)
        {
            if (!symbol.Initialized && !(node.Parent is BinaryAssignment binaryAssignment && binaryAssignment.Target == node))
                _errorHandler.AddError(new UninitializedVariableException(node.Name), node.LineNumber);
                
            return symbol.Type;
        } else if (CompilerUtilities.FindFunction(node.Name, node) is Function function)
        {
            return function.ReturnType;
        }
        else
        {
            _errorHandler.AddError(new IdentifierNotFoundException(node.Name), node.LineNumber);
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
        else
        {
            node.Type = Types.GetLeastAssignableType(leftSingleType, rightSingleType);
        }
        
        return node.Type;
    }

    internal override object? Visit(UnaryCompoundExpression node)
    {
        SingleType? type = Visit(node.Expression) as SingleType;

        if(type is not null && !Operators.CheckOperationIsValid(type.Type, node.Operator))
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, type.Type), node.LineNumber);

        return type;
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
        
        //This part invalidates comparison of tuples
        if (leftType is TupleType leftTupleType)
        {
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, leftTupleType), node.LineNumber);
            return null;
        } else if (rightType is TupleType rightTupleType)
        {
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, rightTupleType), node.LineNumber);
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

    internal override object? Visit(If node)
    {
        YALType? type = Visit(node.Predicate) as YALType;
        if (type is not SingleType singleType || singleType.Type != Types.ValueType.@bool)
        {
            _errorHandler.AddError(new InvalidPredicate(node.Predicate.ToString(), type?.ToString() ?? "null"), node.LineNumber);
        }

        return null;
    }
    
    internal override object? Visit(ElseIf node)
    {
        YALType? type = Visit(node.Predicate) as YALType;
        if (type is not SingleType singleType || singleType.Type != Types.ValueType.@bool)
        {
            _errorHandler.AddError(new InvalidPredicate(node.Predicate.ToString(), type.ToString()), node.LineNumber);
        }

        return null;
    }
    
    internal override object? Visit(WhileStatement node)
    {
        YALType? type = Visit(node.Predicate) as YALType;
        if (type is not SingleType singleType || singleType.Type != Types.ValueType.@bool)
        {
            _errorHandler.AddError(new InvalidPredicate(node.Predicate.ToString(), type.ToString()), node.LineNumber);
        }

        return null;
    }
    
    internal override object? Visit(ForStatement node)
    {
        YALType? type = Visit(node.RunCondition) as YALType;
        if (type is not SingleType singleType || singleType.Type != Types.ValueType.@bool)
        {
            _errorHandler.AddError(new InvalidPredicate(node.RunCondition.ToString(), type.ToString()), node.LineNumber);
        }

        return null;
    }

    internal override object? Visit(ReturnStatement node)
    {
        Function? parentFunction = null;
        ASTNode? tempNode = node;
        while (parentFunction is null && tempNode is not null)
        {
            tempNode = tempNode.Parent;
            if (tempNode is Function functionNode)
                parentFunction = functionNode;
                
        }

        if (parentFunction is not null)
        {
            node.function = parentFunction;
            foreach (Symbol outParam in parentFunction.OutputParameters)
            {
                if (!outParam.Initialized)
                    _errorHandler.AddError(new UninitializedVariableException(outParam.Id), node.LineNumber);
            }
        }

        return null;
    }

    internal override object? Visit(Function node)
    {
        return node.ReturnType;
    }
}