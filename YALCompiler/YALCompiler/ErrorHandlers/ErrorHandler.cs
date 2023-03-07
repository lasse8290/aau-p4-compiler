using Antlr4.Runtime;

namespace YALCompiler.ErrorHandlers;

public class ErrorHandler
{
    public List<string> Errors { get; } = new();

    public void AddError(Exception e, ParserRuleContext context)
    {
        Errors.Add($"Error occurred at line {context.Start.Line}: {e.Message}");
    } 
    
    public string GetAsString() => string.Join(Environment.NewLine, Errors);
}