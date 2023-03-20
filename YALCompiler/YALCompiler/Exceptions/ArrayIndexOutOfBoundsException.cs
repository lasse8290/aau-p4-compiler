namespace YALCompiler.Exceptions;

public class ArrayIndexOutOfBoundsException: Exception
{
    public ArrayIndexOutOfBoundsException(ulong index, ulong arrSize): 
        base($"Array index out of bounds: {index}. Array index must be between 0 and {arrSize}.")
    {
        
    }
}