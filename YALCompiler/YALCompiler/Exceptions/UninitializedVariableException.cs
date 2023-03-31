namespace YALCompiler.Exceptions;

public class UninitializedVariableException: Exception
{
    public UninitializedVariableException(string identifier): 
        base($"Variable '{identifier}' is not initialized.")
    {
        
    }
}