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
    public TypeAndScopeCheckerTraverser(ASTNode node, ErrorHandler errorHandler, WarningsHandler warningsHandler) : base(node)
    {
        _errorHandler = errorHandler;
        _warningsHandler = warningsHandler;
    }

    internal override object? Visit(BinaryAssignment node)
    {
        List<YALType> targetTypes = new();

        foreach (var target in node.Targets)
        {
            switch (target)
            {
                case Identifier identifier:
                    if (CompilerUtilities.FindSymbol(identifier.Name, node) is Symbol symbol)
                    {
                        var idType = symbol.Type;
                        if (node.Operator == Operators.AssignmentOperator.Equals)
                        {
                            symbol.Initialized = true;
                        }
                        else if (!symbol.Initialized)
                        {
                            _errorHandler.AddError(new UninitializedVariableException(identifier.Name), node.LineNumber);
                        }
                        if (identifier is ArrayElementIdentifier arrayElementIdentifier)
                        {
                            idType.Types[0] = idType.Types[0] with {IsArray = false};
                            if (symbol.ArraySize is not null && 
                                (arrayElementIdentifier.Index is Integer index && 
                                index.Value >= (long)symbol.ArraySize ||
                                arrayElementIdentifier.Index is UnsignedInteger uIndex && 
                                uIndex.Value >= (ulong)symbol.ArraySize))
                            {
                                switch (arrayElementIdentifier.Index)
                                {
                                    case Integer i:
                                        if (i.Value > (long)symbol.ArraySize)
                                            _errorHandler.AddError(new ArrayIndexOutOfBoundsException(
                                                i.Value, symbol.ArraySize.Value), node.LineNumber);
                                        break;
                                    case UnsignedInteger ui:
                                        if (ui.Value > (ulong)symbol.ArraySize)
                                            _errorHandler.AddError(new ArrayIndexOutOfBoundsException(
                                                ui.Value, symbol.ArraySize.Value), node.LineNumber);
                                        break;
                                }
                            }
                        }
                        targetTypes.Add(symbol.Type);
                        symbol.Initialized = true;
                    }
                    else
                    {
                        _errorHandler.AddError(new IdentifierNotFoundException(identifier.Name), node.LineNumber);
                    }

                    break;
                case VariableDeclaration variableDeclaration:
                    targetTypes.Add(variableDeclaration.Variable.Type);
                    variableDeclaration.Variable.Initialized = true;
                    break;
            }
        }

        YALType targetType = new(targetTypes.ToArray());

        YALType? valueType = null;

        List<YALType> valueTypes = new();

        foreach (var value in node.Values)
        {
            if (Visit(value) is YALType type)
                valueTypes.Add(type);
        }

        valueType = new(valueTypes.ToArray());

        if (!Types.CheckTypesAreAssignable(targetType, valueType))
        {
            _errorHandler.AddError(new TypeMismatchException(valueType?.ToString() ?? "null",
                targetType?.ToString() ?? "null"), node.LineNumber);
        }
        
        if (!Operators.CheckOperationIsValid(targetType, node.Operator))
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, targetType), node.LineNumber);

        return targetType;
    }

    internal override object? Visit(UnaryAssignment node)
    {
        YALType? targetType = null;
        switch (node.Target)
        {
            case Identifier identifier:

                if ((targetType = Visit(identifier) as YALType) is null)
                {
                    return null;
                }
                break;
            default:
                _errorHandler.AddError(new InvalidAssignmentException(node), node.LineNumber);
                return targetType;
        }

        switch (targetType.Types.Count)
        {
            case 0:
                // some error happened before and no target type was resolved,
                // no need to throw an unrelated error
                break;
            case 1:
                if (!Operators.CheckOperationIsValid(targetType, node.Operator))
                {
                    _errorHandler.AddError(new InvalidOperatorException(node.Operator, targetType), node.LineNumber);
                }

                break;
            default:
                _errorHandler.AddError(new InvalidOperatorException(node.Operator, targetType), node.LineNumber);
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

        bool hasError = false;

        List<YALType?> formalInputParams = new();

        foreach (var inputParameter in function.InputParameters)
        {
            formalInputParams.Add(inputParameter.Type);
        }

        List<YALType?> actualParams = new();

        foreach (var inputParameter in node.InputParameters)
        {
            if (Visit(inputParameter) is YALType inputParamType)
                actualParams.Add(inputParamType);
        }

        YALType finalFormalInputParam = new YALType(formalInputParams.ToArray());
        YALType finalActualInputParam = new YALType(actualParams.ToArray());

        if (!Types.CheckTypesAreAssignable(finalFormalInputParam, finalActualInputParam))
        {
            hasError = true;
        }

        int relativeIndex = 0;
        List<string> formattedInputParams = new();

        for (int i = 0; i < node.InputParameters.Count; i++)
        {
            if (node.InputParameters[i] is Identifier id &&
                function.InputParameters[relativeIndex].IsRef != id.IsRef)
            {
                hasError = true;
                _errorHandler.AddError(new TypeMismatchException((id.IsRef ? "ref " : "") + actualParams[i],
                                                                 (function.InputParameters[relativeIndex].IsRef ? "ref " : "") +
                                                                 string.Join(", ", formalInputParams[relativeIndex].Types.Select(t => t.Type))),
                                       node.LineNumber);
            }
            relativeIndex += actualParams[i].Types.Count;
            formattedInputParams.Add((node.InputParameters[i] is Identifier { IsRef: true } ? "ref " : "") +
                                     string.Join(", ", actualParams[i].Types.Select(t => t.Type)));
        }


        if (hasError)
        {
            _errorHandler.AddError(
                new InvalidFunctionCallInputParameters(
                    function.InputParameters,
                    formattedInputParams),
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

    internal override object? Visit(Integer node)
    {
        return node.Value switch
        {
            <= sbyte.MaxValue and >= sbyte.MinValue => new YALType(Types.ValueType.int8),
            <= short.MaxValue and >= short.MinValue => new YALType(Types.ValueType.int16),
            <= int.MaxValue and >= int.MinValue => new YALType(Types.ValueType.int32),
            <= long.MaxValue and >= long.MinValue => new YALType(Types.ValueType.int64)
        };
    }
    
    internal override object? Visit(UnsignedInteger node)
    {
        return node.Value switch
        {
            <= byte.MaxValue and >= byte.MinValue => new YALType(Types.ValueType.uint8),
            <= ushort.MaxValue and >= ushort.MinValue => new YALType(Types.ValueType.uint16),
            <= uint.MaxValue and >= uint.MinValue => new YALType(Types.ValueType.uint32),
            <= ulong.MaxValue and >= ulong.MinValue => new YALType(Types.ValueType.uint64)
        };
    }

    internal override object? Visit(SignedFloat node)
    {
        return node.Value switch
        {
            <= float.MaxValue and >= float.MinValue => new YALType(Types.ValueType.float32),
            <= double.MaxValue and >= double.MinValue => new YALType(Types.ValueType.float64)
        };
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
            if (!symbol.Initialized && 
                !(node.Parent is BinaryAssignment binaryAssignment && binaryAssignment.Targets.Contains(node) && binaryAssignment.Operator == Operators.AssignmentOperator.Equals) && 
                !node.IsRef)
                _errorHandler.AddError(new UninitializedVariableException(node.Name), node.LineNumber);

            return symbol.Type;
        }
        else
        {
            _errorHandler.AddError(new IdentifierNotFoundException(node.Name), node.LineNumber);
            return null;
        }
    }
    
    internal override object? Visit(ArrayElementIdentifier node)
    {
        if (CompilerUtilities.FindSymbol(node.Name, node) is Symbol symbol)
        {
            if (!symbol.Initialized &&
                !(node.Parent is BinaryAssignment binaryAssignment && binaryAssignment.Targets.Contains(node) &&
                  binaryAssignment.Operator == Operators.AssignmentOperator.Equals) &&
                !node.IsRef)
            {
                _errorHandler.AddError(new UninitializedVariableException(node.Name), node.LineNumber);
                return null;
            }

            
            
            YALType? _indexType = (YALType)Visit(node.Index);
            if (_indexType is YALType indexType)
            {
                if (!Types.CheckTypesAreAssignable(new YALType(Types.ValueType.int64, false), indexType) &&
                    !Types.CheckTypesAreAssignable(new YALType(Types.ValueType.uint64, false), indexType))
                {
                    _errorHandler.AddError(new TypeMismatchException("int64", indexType.ToString()), node.LineNumber);
                }

                switch (node.Index)
                {
                    case Integer integer:
                        if (integer.Value < 0)
                            _errorHandler.AddError(new ArrayIndexOutOfBoundsException(integer.Value, symbol.ArraySize ?? 0), node.LineNumber);
                        else if (symbol.ArraySize is ulong arrSize && (ulong)integer.Value >= arrSize)
                            _errorHandler.AddError(new ArrayIndexOutOfBoundsException(integer.Value, arrSize), node.LineNumber);
                        break;
                    case UnsignedInteger unsignedInteger:
                        if (symbol.ArraySize is ulong _arrSize && unsignedInteger.Value >= _arrSize)
                            _errorHandler.AddError(new ArrayIndexOutOfBoundsException(unsignedInteger.Value, _arrSize), node.LineNumber);
                        break;
                }
            }
            else
            {
                return null;
            }
            
            YALType type = symbol.Type;
            type.Types[0] = type.Types[0] with { IsArray = false };
            
            return symbol.Type;
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
        if (!Operators.CheckOperationIsValid(leftType, node.Operator))
        {
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, leftType), node.LineNumber);
        }
        else if (!Operators.CheckOperationIsValid(rightType, node.Operator))
        {
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, rightType), node.LineNumber);
        }
        else
        {
            node.Type = Types.GetLeastAssignableType(leftType, rightType);
        }

        return node.Type;
    }

    internal override object? Visit(StringLiteral node)
    {
        return new YALType(Types.ValueType.@string);
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
        if (leftType.Types.Count > 1)
        {
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, leftType), node.LineNumber);
            return null;
        }
        else if (rightType.Types.Count > 1)
        {
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, rightType), node.LineNumber);
            return null;
        }

        if (!Operators.CheckOperationIsValid(leftType, node.Operator))
        {
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, leftType), node.LineNumber);
            return null;
        }
        else if (!Operators.CheckOperationIsValid(rightType, node.Operator))
        {
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, rightType), node.LineNumber);
            return null;
        }
        return new YALType(Types.ValueType.@bool);
    }

    internal override object? Visit(DataTypes.Boolean node) => new YALType(Types.ValueType.@bool);

    internal override object? Visit(Predicate node) => new YALType(Types.ValueType.@bool);

    internal override object? Visit(ArrayLiteral node)
    {
        if (node.Values.Count == 0)
        {
            return null;
        }
        List<YALType> types = new();
        foreach (var value in node.Values)
        {
            YALType? type = Visit(value) as YALType;
            types.Add(type);
        }

        YALType? leastAssignableType = Types.GetLeastAssignableType(types.ToArray());
        if (leastAssignableType is null || leastAssignableType.Types.Count == 0) return null;
        leastAssignableType.Types[0] = leastAssignableType.Types[0] with { IsArray = true };
        return leastAssignableType;
    }

    internal override object? Visit(If node)
    {
        YALType? type = Visit(node.Predicate) as YALType;
        if (type != new YALType(Types.ValueType.@bool))
        {
            _errorHandler.AddError(new InvalidPredicateException(node.Predicate.ToString(), type?.ToString() ?? "null"), node.LineNumber);
        }

        return null;
    }

    internal override object? Visit(ElseIf node)
    {
        YALType? type = Visit(node.Predicate) as YALType;
        if (type != new YALType(Types.ValueType.@bool))
        {
            _errorHandler.AddError(new InvalidPredicateException(node.Predicate.ToString(), type.ToString()), node.LineNumber);
        }

        return null;
    }

    internal override object? Visit(WhileStatement node)
    {
        YALType? type = Visit(node.Predicate) as YALType;
        if (type != new YALType(Types.ValueType.@bool))
        {
            _errorHandler.AddError(new InvalidPredicateException(node.Predicate.ToString(), type.ToString()), node.LineNumber);
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
                    _errorHandler.AddError(new UninitializedVariableException(outParam.Name), node.LineNumber);
            }
        }

        return null;
    }

    internal override object? Visit(Function node)
    {
        return node.ReturnType;
    }

    internal override object? Visit(VariableDeclaration node)
    {
        if (node.Variable.ArraySize is not null)
            node.Variable.Initialized = true;

        return node.Variable.Type;
    }

    internal override object? Visit(LogicalNegation node)
    {
        if (Visit(node.Expression) is not YALType expressionType)
        {
            _errorHandler.AddError(new InvalidExpressionException(node.ToString()), node.LineNumber);
            return null;
        }

        if (!Operators.CheckOperationIsValid(expressionType, Operators.PredicateOperator.Not))
        {
            _errorHandler.AddError(new InvalidOperatorException(Operators.PredicateOperator.Not, expressionType), node.LineNumber);
        }

        return expressionType;
    }
    
    internal override object? Visit(BitwiseNegation node)
    {
        if (Visit(node.Expression) is not YALType expressionType)
        {
            _errorHandler.AddError(new InvalidExpressionException(node.ToString()), node.LineNumber);
            return null;
        }

        if (!Operators.CheckOperationIsValid(expressionType, Operators.ExpressionOperator.BitwiseNot))
        {
            _errorHandler.AddError(new InvalidOperatorException(Operators.ExpressionOperator.BitwiseNot, expressionType), node.LineNumber);
        }

        return expressionType;
    }
}