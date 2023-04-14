using YALCompiler.DataTypes;

namespace YALCompiler.Exceptions;

public class InvalidAssignmentException: Exception
{
    public InvalidAssignmentException(Assignment assignment): base($"Invalid assignment: {assignment}")
    {
        
    }
    
    public InvalidAssignmentException(string assignment): base($"Invalid assignment: {assignment}")
    {
        
    }
}