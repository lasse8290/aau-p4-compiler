using Antlr4.Runtime;

namespace YALCompiler.ErrorHandlers;

public class WarningsHandler
{
    public List<string> Warnings { get; } = new();
    public void AddWarning(Exception e, ParserRuleContext context)
    {
        Warnings.Add($"Warning at line {context.Start.Line}: {e.Message}");
    } 
    
    public string GetAsString() => string.Join(Environment.NewLine, Warnings);

}