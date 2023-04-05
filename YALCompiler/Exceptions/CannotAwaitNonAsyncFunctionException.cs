using YALCompiler.Helpers;

namespace YALCompiler.Exceptions;

public class CannotAwaitNonAsyncFunctionException: Exception
{
    public CannotAwaitNonAsyncFunctionException(): 
        base($"The 'await' keyword can only be used on async functions")
    {
        
    }
}