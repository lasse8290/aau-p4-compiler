using YALCompiler.Helpers;

namespace YALCompiler.Exceptions;

public class InvalidFunctionCallInputParameters: Exception
{
    public InvalidFunctionCallInputParameters(List<SingleType> expected, List<SingleType> actual): 
        base($"Invalid function call: expected ({string.Join(", ", expected.Select(st => st.Type.ToString()))}) but got ({string.Join(", ", actual.Select(st => st.Type.ToString()))})")
    {
        
    }
    
    public InvalidFunctionCallInputParameters(int expectedCount, int actualCount): 
        base($"Invalid function call: expected {expectedCount} parameters but got {actualCount} parameters")
    {
        
    }
}