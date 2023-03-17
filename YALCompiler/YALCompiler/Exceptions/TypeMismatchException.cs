namespace YALCompiler.Exceptions;

public class TypeMismatchException: Exception
{
    public TypeMismatchException(string sourceType, string destType): 
        base($"Type mismatch: cannot convert from '{sourceType}' to '{destType}'")
    {
        
    }
}