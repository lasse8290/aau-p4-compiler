using YALCompiler.Helpers;

namespace YALCompiler.Exceptions;

public class InvalidOperatorException: Exception
{
    public InvalidOperatorException(Operators.PredicateOperator @operator, Types.ValueType targetType): 
        base($"Invalid operator exception: cannot use operator {@operator.ToStringValue()} on type {targetType}")
    {
        
    }
    
    public InvalidOperatorException(Operators.AssignmentOperator @operator, Types.ValueType targetType): 
        base($"Invalid operator exception: cannot use operator {@operator.ToStringValue()} on type {targetType}")
    {
        
    }
    
    public InvalidOperatorException(Operators.ExpressionOperator @operator, Types.ValueType targetType): 
        base($"Invalid operator exception: cannot use operator {@operator.ToStringValue()} on type {targetType}")
    {
        
    }
    
    public InvalidOperatorException(Operators.PredicateOperator @operator, YALType type): 
        base($"Invalid operator exception: cannot use operator {@operator.ToStringValue()} on type {
            type.Types.Count switch {
                0 => "null",
                1 => type.Types[0].Type + (type.Types[0].IsArray ? "[]" : ""),
                _ => $"({string.Join(", ", type.Types.Select(t => t.Type + (t.IsArray ? "[]" : "")).ToArray())})"
            }
        }")
    {
        
    }
    
    public InvalidOperatorException(Operators.AssignmentOperator @operator, YALType type): 
        base($"Invalid operator exception: cannot use operator {@operator.ToStringValue()} on type {
            type.Types.Count switch {
                0 => "null",
                1 => type.Types[0].Type + (type.Types[0].IsArray ? "[]" : ""),
                _ => $"({string.Join(", ", type.Types.Select(t => t.Type + (t.IsArray ? "[]" : "")).ToArray())})"
            }
        }")
    {
        
    }
    
    public InvalidOperatorException(Operators.ExpressionOperator @operator, YALType type): 
        base($"Invalid operator exception: cannot use operator {@operator.ToStringValue()} on type {
            type.Types.Count switch {
                0 => "null",
                1 => type.Types[0].Type + (type.Types[0].IsArray ? "[]" : ""),
                _ => $"({string.Join(", ", type.Types.Select(t => t.Type + (t.IsArray ? "[]" : "")).ToArray())})"
            }
        }")
    {
        
    }
}