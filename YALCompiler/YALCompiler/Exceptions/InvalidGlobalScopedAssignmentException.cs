using YALCompiler.Helpers;

namespace YALCompiler.Exceptions;

public class InvalidGlobalScopedAssignmentException: Exception
{
    public InvalidGlobalScopedAssignmentException(): 
        base($"Invalid global-scoped assignment")
    {
        
    }
}