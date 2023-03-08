namespace YALCompiler.Exceptions;

public class ArraySizeNotRecognizedException: Exception
{
    public ArraySizeNotRecognizedException(string definer): base($"Array size definer {definer} not recognized")
    {
        
    }
}