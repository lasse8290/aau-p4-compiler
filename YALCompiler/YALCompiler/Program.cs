// See https://aka.ms/new-console-template for more information

using System.Text;
using Antlr4.Runtime;
using YALParser;

Console.WriteLine("Hello, World!");

try {
    string input = "";
    StringBuilder text = new StringBuilder();

    text.Append("main { func(3 + (20+3)); i32 ef; bool bb = func(3,4,fr()); ef = 6; }");
    
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

/*    IToken token;

    do
    {
        token = speakLexer.NextToken();

        Console.WriteLine("Token: " + token.Type + " " + token.Text);
    } while (token != null && token.Type != YALGrammerLexer.Eof);

    speakLexer.Reset();*/

    YALGrammerParser.ProgramContext? n = speakParser.program();

    visitor.Visit(n);

/*    speakParser.Reset();

    var x = visitor.VisitFunction(speakParser.function());

    var y = visitor.VisitAssignment(speakParser.assignment());*/

} catch (Exception e) {
    Console.WriteLine(e);
}

// Note for Richard: to run antlr
// java -jar antlr-4.12.0-complete.jar -Dlanguage=CSharp -o C:/Users/rilar/source/repos/aau-p4-compiler/YALCompiler/./YALParser/gen -listener -visitor -lib C:/Users/rilar/source/repos/aau-p4-compiler/YALCompiler/YALParser/Gammar C:/Users/rilar/source/repos/aau-p4-compiler/YALCompiler/YALParser/Gammar/YALGrammer.g4
// YouTube video: literally the source
// https://www.youtube.com/watch?v=bfiAvWZWnDA