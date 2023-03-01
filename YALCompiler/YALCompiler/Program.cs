// See https://aka.ms/new-console-template for more information

using System.Text;
using Antlr4.Runtime;
using YALParser;

Console.WriteLine("Hello, World!");

try {
    string input = "";
    StringBuilder text = new StringBuilder();

    text.Append("main { i32 fuck; } ");
    
    /*
    // to type the EOF character and end the input: use CTRL+D, then press <enter>
    while ((input = Console.ReadLine()) != "\u0004" && input != "øøø") {
        text.AppendLine(input);
    }
    */
    AntlrInputStream inputStream = new AntlrInputStream(text.ToString());
    YALGrammerLexer speakLexer = new YALGrammerLexer(inputStream);
    CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);
    YALGrammerParser speakParser = new YALGrammerParser(commonTokenStream);

    YALGrammerVisitor visitor = new YALGrammerVisitor();

    IToken token;

    do {
        token = speakLexer.NextToken();

        Console.WriteLine("Token: " + token.Type + " " + token.Text);
    } while (token != null && token.Type != YALGrammerLexer.Eof);

    speakLexer.Reset();
    
    YALGrammerParser.ProgramContext? n = speakParser.program();
    
    var x = visitor.VisitAssignment(speakParser.assignment());

} catch (Exception e) {
    Console.WriteLine(e);
}
