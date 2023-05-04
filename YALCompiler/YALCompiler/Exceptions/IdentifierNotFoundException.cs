namespace YALCompiler.Exceptions;

public class IdentifierNotFoundException: Exception
{
    public IdentifierNotFoundException(string identifier): base($"Identifier \"{identifier}\" unable to be found")
    {
        
    }
}