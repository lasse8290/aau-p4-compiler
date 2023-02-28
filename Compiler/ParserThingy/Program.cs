// See https://aka.ms/new-console-template for more information

using System.Text;
using Antlr4.Runtime;

static void Main(string[] args)
{
    try
    {
        string input = "";
        StringBuilder text = new StringBuilder();
        Console.WriteLine("Input the chat.");
        
        // to type the EOF character and end the input: use CTRL+D, then press <enter>
        while ((input = Console.ReadLine()) != "u0004")
        {
            text.AppendLine(input);
        }
        
        AntlrInputStream inputStream = new AntlrInputStream(text.ToString());
        bettersyntaxLexer speakLexer = new bettersyntaxLexer(inputStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);
        bettersyntaxParser speakParser = new bettersyntaxParser(commonTokenStream);

        bettersyntaxParser.Function_declarationContext fd = speakParser.function_declaration();
        bettersyntaxParser.ProgramContext pc = speakParser.program();
        bettersyntaxParser.ScopeContext pcScope = speakParser.scope();
        
        bettersyntaxBaseVisitor<object> visitor = new bettersyntaxBaseVisitor<object>();        
        visitor.Visit(pc);
        
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex);                
    }
}

Console.WriteLine("Hello, World!");