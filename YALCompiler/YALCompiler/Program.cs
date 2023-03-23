using StringTemplating;
using Antlr4.Runtime;
using Microsoft.VisualBasic.CompilerServices;
using YALCompiler;
using YALCompiler.ErrorHandlers;
using YALCompiler.Helpers;

try
{
    Template.LoadTemplates("Templates", "txt");
    var                 text            = File.ReadAllText("Grammar/examples.yal");
    var                 errorHandler    = new ErrorHandler();
    var                 warningsHandler = new WarningsHandler();
    
    AntlrInputStream                 inputStream       = new AntlrInputStream(text.ToString());
    YALGrammerLexer                  speakLexer        = new YALGrammerLexer(inputStream);
    CommonTokenStream                commonTokenStream = new CommonTokenStream(speakLexer);
    YALGrammerParser                 speakParser       = new YALGrammerParser(commonTokenStream);
    YALGrammerVisitor                visitor           = new YALGrammerVisitor(errorHandler, warningsHandler);
    YALGrammerParser.ProgramContext? n                 = speakParser.program();
    
    YALCompiler.DataTypes.Program    node              = (YALCompiler.DataTypes.Program)visitor.Visit(n);
    LinkerASTTraverser               linker            = new(node);
    linker.BeginTraverse();
    TypeAndScopeCheckerTraverser traverser = new(node, errorHandler, warningsHandler);
    traverser.BeginTraverse();
    
    Console.WriteLine(warningsHandler.GetAsString());
    Console.WriteLine(errorHandler.GetAsString());
    CodeGenTraverser cgt = new(node);
    cgt.BeginTraverse();
    string generatedCode = cgt.GetGeneratedCode();
    
    Console.WriteLine(generatedCode);

    

    Console.WriteLine("Done");

} catch (Exception e) {
    Console.WriteLine(e);
}