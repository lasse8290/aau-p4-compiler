using YALCompiler.DataTypes;

namespace YALCompiler.Exceptions;

public class InvalidPredicate: Exception
{
    public InvalidPredicate(string predicate): base($"Invalid predicate: {predicate}")
    {
        
    }
}