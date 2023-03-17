using StringTemplating;
using Antlr4.Runtime;
using Microsoft.VisualBasic.CompilerServices;
using YALCompiler;
using YALCompiler.ErrorHandlers;
using YALCompiler.Helpers;

try
{
    var text = File.ReadAllText("Grammar/examples.yal");
    
    AntlrInputStream inputStream = new AntlrInputStream(text.ToString());
    YALGrammerLexer speakLexer = new YALGrammerLexer(inputStream);
    CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);
    YALGrammerParser speakParser = new YALGrammerParser(commonTokenStream);

    var errorHandler = new ErrorHandler();
    var warningsHandler = new WarningsHandler();
    YALGrammerVisitor visitor = new YALGrammerVisitor(errorHandler, warningsHandler);

    YALGrammerParser.ProgramContext? n = speakParser.program();

    YALCompiler.DataTypes.Program node = (YALCompiler.DataTypes.Program)visitor.Visit(n);

    TypeAndScopeCheckerTraverser traverser = new(node, errorHandler, warningsHandler);
    traverser.BeginTraverse();
    
    Console.WriteLine(errorHandler.GetAsString());
    Console.WriteLine(warningsHandler.GetAsString());
    Console.WriteLine("Done");

} catch (Exception e) {
    Console.WriteLine(e);
}


IEnumerable<string> names = Template.LoadTemplates("Templates", "txt");


var program = new Template("program");
program["include"] = "#include <stdio.h>";
program["setup_body"] = """printf(\Hello World\);""";

var declartion_assignment = new Template("declaration_assignment");

declartion_assignment.SetKeys(new List<Tuple<string, string>>() { 
    new("type", "int"),
    new("id", "i"),
    new("value", "0")
});

var ifStatement = new Template("if");
ifStatement.SetKeys(new List<Tuple<string, string>>() {
    new("condition", "i == 0"),
    new("body", """printf(\i is 0\);""")
});

program.SetKeys(new List<Tuple<string, Template>>() {
    new("loop_body", declartion_assignment),
    new("loop_body", ifStatement)
});

Console.WriteLine(program.ReplacePlaceholders(true));