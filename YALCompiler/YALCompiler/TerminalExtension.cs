namespace YALCompiler; 

public static class TerminalExtension {
    
    public static void LogError(string message) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}