using YALCompiler.Helpers;

namespace YALCompiler.Exceptions;

public class InvalidAwaitException: Exception
{
    public InvalidAwaitException(): 
        base($"The 'await' keyword can only be used in async functions")
    {
        
    }
}