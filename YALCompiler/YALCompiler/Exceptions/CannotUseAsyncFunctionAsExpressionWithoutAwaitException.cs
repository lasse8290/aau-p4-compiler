namespace YALCompiler.Exceptions;

public class CannotUseAsyncFunctionAsExpressionWithoutAwaitException: Exception
{
    public CannotUseAsyncFunctionAsExpressionWithoutAwaitException(): 
        base($"Cannot use async function as expression without 'await'.")
    {
        
    }
}