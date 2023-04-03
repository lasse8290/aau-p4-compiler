using System.Diagnostics;
using StringTemplating;
using Antlr4.Runtime;
using YALCompiler.ErrorHandlers;
using System.Reflection;

namespace YALCompiler;
public static class Program
{
    public static void Main()
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        Console.WriteLine(path);

        try
        {

            Stopwatch sw = new();
            sw.Start();
            Template.LoadTemplates($"{path}/Templates", "txt");
            var text = File.ReadAllText($"{path}/Grammar/examples.yal");
            var errorHandler = new ErrorHandler();
            var warningsHandler = new WarningsHandler();
            sw.Stop();
            Console.WriteLine("Loaded templates in " + sw.ElapsedMilliseconds + "ms");
            sw.Restart();
            AntlrInputStream inputStream = new AntlrInputStream(text.ToString());
            YALGrammerLexer speakLexer = new YALGrammerLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);
            YALGrammerParser speakParser = new YALGrammerParser(commonTokenStream);
            YALGrammerVisitor visitor = new YALGrammerVisitor(errorHandler, warningsHandler);
            sw.Stop();


            Console.WriteLine("Parsed source code in " + sw.ElapsedMilliseconds + "ms");
            sw.Restart();
            YALGrammerParser.ProgramContext? n = speakParser.program();
            YALCompiler.DataTypes.Program node = (YALCompiler.DataTypes.Program)visitor.Visit(n);
            sw.Stop();
            Console.WriteLine("Built AST in " + sw.ElapsedMilliseconds + "ms");
            sw.Restart();
            LinkerASTTraverser linker = new(node);
            linker.BeginTraverse();
            sw.Stop();
            Console.WriteLine("Linked AST in " + sw.ElapsedMilliseconds + "ms");
            sw.Restart();
            TypeAndScopeCheckerTraverser traverser = new(node, errorHandler, warningsHandler);
            traverser.BeginTraverse();
            sw.Stop();
            Console.WriteLine(warningsHandler.GetAsString());
            Console.WriteLine(errorHandler.GetAsString());
            Console.WriteLine("Type and scope checked AST in " + sw.ElapsedMilliseconds + "ms");
            sw.Restart();
            CodeGenTraverser cgt = new(node);
            cgt.BeginTraverse();
            sw.Stop();
            Console.WriteLine("Generated code in " + sw.ElapsedMilliseconds + "ms");
            string generatedCode = cgt.GetGeneratedCode();

            Console.WriteLine(generatedCode);

            string filePath = Path.Combine($"{path}", "GenCode.txt");

            File.WriteAllText(filePath, generatedCode);


            Console.WriteLine("Done");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}