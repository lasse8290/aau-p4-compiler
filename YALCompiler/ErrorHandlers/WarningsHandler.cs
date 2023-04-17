using Antlr4.Runtime;

namespace YALCompiler.ErrorHandlers;

public class WarningsHandler
{
    public List<string> Warnings { get; } = new();
    public void AddWarning(Exception e, ParserRuleContext context)
    {
        Warnings.Add($"Warning at line {context.Start.Line}: {e.Message}");
    } 
    
    public void AddWarning(Exception e, int lineNumber)
    {
        Warnings.Add($"Warning at line {lineNumber}: {e.Message}");
    } 
    
    public override string ToString() => string.Join(Environment.NewLine, Warnings);

}