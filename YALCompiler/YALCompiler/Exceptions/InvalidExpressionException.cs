using YALCompiler.DataTypes;

namespace YALCompiler.Exceptions;

public class InvalidExpressionException: Exception
{
    public InvalidExpressionException(string expression): base($"Invalid expression: {expression}")
    {
        
    }
    
    public InvalidExpressionException(Expression expression): base($"Invalid expression: {expression}")
    {
        
    }
}