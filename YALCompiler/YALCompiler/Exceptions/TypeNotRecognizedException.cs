namespace YALCompiler.Exceptions;

public class TypeNotRecognizedException: Exception
{
    public TypeNotRecognizedException(string type): base($"Type {type} not recognized")
    {
        
    }
}