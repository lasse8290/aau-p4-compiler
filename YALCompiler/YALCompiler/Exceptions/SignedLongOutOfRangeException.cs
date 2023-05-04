namespace YALCompiler.Exceptions;

public class SignedLongOutOfRangeException: Exception
{
    public SignedLongOutOfRangeException(ulong number): 
        base($"Signed Long out of range: {number}. Must be between {long.MinValue} and {long.MaxValue}.")
    {
        
    }
}