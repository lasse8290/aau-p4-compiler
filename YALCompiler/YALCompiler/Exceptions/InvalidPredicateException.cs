using YALCompiler.DataTypes;

namespace YALCompiler.Exceptions;

public class InvalidPredicateException: Exception
{
    public InvalidPredicateException(string predicate): base($"Invalid predicate: {predicate}")
    {
        
    }
    
    public InvalidPredicateException(string predicate, string type): base($"Invalid predicate: {predicate} of type {type}")
    {
        
    }
}