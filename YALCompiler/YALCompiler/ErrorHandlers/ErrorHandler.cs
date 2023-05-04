using Antlr4.Runtime;

namespace YALCompiler.ErrorHandlers;

public class ErrorHandler
{
    public List<string> Errors { get; } = new();
    public List<Exception> ErrorsWithoutLineNumber { get; } = new();

    public void AddError(Exception e, ParserRuleContext context)
    {
        Errors.Add($"Error occurred at line {context.Start.Line}: {e.Message}");
        ErrorsWithoutLineNumber.Add(e);
    }
    
    public void AddError(Exception e, int lineNumber)
    {
        Errors.Add($"Error occurred at line {lineNumber}: {e.Message}");
        ErrorsWithoutLineNumber.Add(e);
    } 
    
    public override string ToString() => string.Join(Environment.NewLine, Errors);
}