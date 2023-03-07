namespace YALCompiler.Exceptions;

public class VariableAlreadyExistsException: Exception
{
    public VariableAlreadyExistsException(string variable): base($"Variable \"{variable}\" already exists")
    {
        
    }
}