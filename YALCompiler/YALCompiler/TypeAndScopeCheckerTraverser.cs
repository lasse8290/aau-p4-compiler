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
        List<YALType> targetTypes = new();

        foreach (var target in node.Targets)
        {
            switch (target)
            {
                case Identifier identifier:
                    if (CompilerUtilities.FindSymbol(identifier.Name, node) is Symbol symbol)
                    {
                        var idType = symbol.Type;
                        symbol.Initialized = true;
                        if (identifier is ArrayElementIdentifier arrayElementIdentifier)
                        {
                            idType.Types[0] = idType.Types[0] with {IsArray = false};
                            if (symbol.ArraySize is not null && 
                                arrayElementIdentifier.Index is SignedNumber index && 
                                index.Value > symbol.ArraySize!)
                            {
                                _errorHandler.AddError(new ArrayIndexOutOfBoundsException(
                                    index.Value, symbol.ArraySize.Value), node.LineNumber);
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

        switch (targetType.Types.Count)
        {
            case 0:
                // some error happened before and no target type was resolved,
                // no need to throw an unrelated error
                break;
            case 1:
                if (!Operators.CheckOperationIsValid(targetType, node.Operator))
                {
                    _errorHandler.AddError(new InvalidOperatorException(node.Operator, targetType.Types[0].Type), node.LineNumber);
                }
                break;
            default:
                if (node.Operator != Operators.AssignmentOperator.Equals)
                {
                    _errorHandler.AddError(new InvalidOperatorException(node.Operator, targetType), node.LineNumber);
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
            actualParams.Add(Visit(inputParameter) as YALType);
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
            formattedInputParams.Add((node.InputParameters[i] is Identifier {IsRef:true} ? "ref " : "") + 
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

    internal override object? Visit(SignedNumber node)
    {
        YALType? type = node.Negative switch
        {
            true => node.Value switch
            {
                <= sbyte.MaxValue + 1 => new YALType(Types.ValueType.int8),
                <= short.MaxValue + 1 => new YALType(Types.ValueType.int16),
                <= (ulong)int.MaxValue + 1 => new YALType(Types.ValueType.int32),
                <= (ulong)long.MaxValue + 1 => new YALType(Types.ValueType.int64),
                _ => null,
            },
            _ => node.Value switch
            {
                <= byte.MaxValue => node.Value <= (ulong)sbyte.MaxValue ? new YALType(Types.ValueType.int8) : new YALType(Types.ValueType.uint8),
                <= ushort.MaxValue => node.Value <= (ulong)short.MaxValue ? new YALType(Types.ValueType.int16) : new YALType(Types.ValueType.uint16),
                <= uint.MaxValue => node.Value <= int.MaxValue ? new YALType(Types.ValueType.int32) : new YALType(Types.ValueType.uint32),
                <= ulong.MaxValue => node.Value <= long.MaxValue ? new YALType(Types.ValueType.int64) : new YALType(Types.ValueType.uint64),
            }
        };
        
        if (type is null)
            _errorHandler.AddError(new SignedLongOutOfRangeException(node.Value), node.LineNumber);
        
        return type;
    }
    
    internal override object? Visit(SignedFloat node)
    {
        YALType type;
        
        if (node.Value <= float.MaxValue && node.Value >= float.MinValue)
        {
            type = new YALType(Types.ValueType.float32);
        }
        else
        {
            type = new YALType(Types.ValueType.float64);
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
            if (!symbol.Initialized && 
                !(node.Parent is BinaryAssignment binaryAssignment && binaryAssignment.Targets.Contains(node)) && 
                !node.IsRef)
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

        if (!Operators.CheckOperationIsValid(leftType, node.Operator))
        {
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, leftType), node.LineNumber);
        } else if (!Operators.CheckOperationIsValid(rightType, node.Operator))
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
        } else if (rightType.Types.Count > 1)
        {
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, rightType), node.LineNumber);
            return null;
        }

        // if (!Types.CheckCompoundExpressionTypesAreValid(leftType, rightType))
        // {
        //     _errorHandler.AddError(new TypeMismatchException(leftType?.ToString() ?? "null", rightType?.ToString() ?? "null"), node.LineNumber);
        // }

        if (!Operators.CheckOperationIsValid(leftType, node.Operator))
        {
            _errorHandler.AddError(new InvalidOperatorException(node.Operator, leftType), node.LineNumber);
            return null;
        } else if (!Operators.CheckOperationIsValid(rightType, node.Operator))
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
        YALType leastAssignableType = (YALType)Visit(node.Values[0]);
        for (int i = 1; i < node.Values.Count; i++)
        {
            YALType type = (YALType)Visit(node.Values[i]);
            leastAssignableType = Types.GetLeastAssignableType(leastAssignableType, type);
        }

        if (leastAssignableType is null) return null;
        
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
    
    internal override object? Visit(ForStatement node)
    {
        YALType? type = Visit(node.RunCondition) as YALType;
        if (type != new YALType(Types.ValueType.@bool))
        {
            _errorHandler.AddError(new InvalidPredicateException(node.RunCondition.ToString(), type.ToString()), node.LineNumber);
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