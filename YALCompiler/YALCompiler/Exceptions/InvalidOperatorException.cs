using YALCompiler.Helpers;

namespace YALCompiler.Exceptions;

public class InvalidOperatorException: Exception
{
    public InvalidOperatorException(Operators.PredicateOperator @operator, Types.ValueType targetType): 
        base($"Invalid operator exception: cannot use operator {@operator} on type {targetType}")
    {
        
    }
    
    public InvalidOperatorException(Operators.AssignmentOperator @operator, Types.ValueType targetType): 
        base($"Invalid operator exception: cannot use operator {@operator} on type {targetType}")
    {
        
    }
    
    public InvalidOperatorException(Operators.ExpressionOperator @operator, Types.ValueType targetType): 
        base($"Invalid operator exception: cannot use operator {@operator} on type {targetType}")
    {
        
    }
    
    public InvalidOperatorException(Operators.PredicateOperator @operator, TupleType tupleType): 
        base($"Invalid operator exception: cannot use operator {@operator} on type {tupleType}")
    {
        
    }
    
    public InvalidOperatorException(Operators.AssignmentOperator @operator, TupleType tupleType): 
        base($"Invalid operator exception: cannot use operator {@operator} on type {tupleType}")
    {
        
    }
    
    public InvalidOperatorException(Operators.ExpressionOperator @operator, TupleType tupleType): 
        base($"Invalid operator exception: cannot use operator {@operator} on type {tupleType}")
    {
        
    }
}