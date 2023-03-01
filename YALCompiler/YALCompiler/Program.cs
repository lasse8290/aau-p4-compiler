// See https://aka.ms/new-console-template for more information

using System.Text;
using Antlr4.Runtime;

Console.WriteLine("Hello, World!");

try {
    string input = "";
    StringBuilder text = new StringBuilder();
    Console.WriteLine("Input the chat.");

    // to type the EOF character and end the input: use CTRL+D, then press <enter>
    while ((input = Console.ReadLine()) != "\u0004" && input != "øøø") {
        text.AppendLine(input);
    }

    AntlrInputStream inputStream = new AntlrInputStream(text.ToString());
    YALGrammerLexer speakLexer = new YALGrammerLexer(inputStream);
    CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);
    YALGrammerParser speakParser = new YALGrammerParser(commonTokenStream);

    IToken token;

    do {
        token = speakLexer.NextToken();

        Console.WriteLine("Token: " + token.Type + " " + token.Text);
    } while (token != null && token.Type != YALGrammerLexer.Eof);


    
} catch (Exception e) {
    Console.WriteLine(e);
}
