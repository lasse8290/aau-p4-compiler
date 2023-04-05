using YALCompiler.DataTypes;
using YALCompiler.Helpers;

namespace YALCompiler.Exceptions;

public class InvalidFunctionCallInputParameters: Exception
{
    public InvalidFunctionCallInputParameters(List<SingleType?> expected, List<SingleType?> actual): 
        base($"Invalid function call: expected ({string.Join(", ", expected.Select(st => st?.Type.ToString() ?? "null"))}) but got ({string.Join(", ", actual.Select(st => st?.Type.ToString() ?? "null"))})")
    {
        
    }

    public InvalidFunctionCallInputParameters(List<Symbol> expected, List<string> actual) :
        base($"Invalid function call: expected ({string.Join(", ", expected.Select(s => (s.IsRef ? "ref " : "") + s?.Type?.ToString() ?? "null"))}) but got ({string.Join(", ", actual)})")
    {
        
    }
    
    public InvalidFunctionCallInputParameters(int expectedCount, int actualCount): 
        base($"Invalid function call: expected {expectedCount} parameters but got {actualCount} parameters")
    {
        
    }
}