using YALCompiler.DataTypes;

namespace YALCompiler.Exceptions;

public class InvalidAssignment: Exception
{
    public InvalidAssignment(Assignment assignment): base($"Invalid assignment: {assignment}")
    {
        
    }
}