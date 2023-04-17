using StringTemplating;
using Antlr4.Runtime;
using YALCompiler.ErrorHandlers;
using System.Reflection;
using YALCompiler.DataTypes;
using YALCompiler;

public class Transpiler
{
    public string InputFilePath { get; set; }
    public string OutputFilePath { get; set; }

    public Transpiler(string InputFilePath, string OutputFilePath)
    {
        this.InputFilePath = InputFilePath;
        this.OutputFilePath = OutputFilePath;
    }

    public void Transpile()
    {
        try
        {
            string? templatesPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Templates";
            if (templatesPath == null) throw new Exception($"Path {templatesPath} not found");

            var errorHandler = new ErrorHandler();
            var warningsHandler = new WarningsHandler();

            Template.LoadTemplates(templatesPath, "txt");
            string source_code = File.ReadAllText(InputFilePath);

            AntlrInputStream input = new AntlrInputStream(source_code.ToString());
            YALGrammerLexer lexer = new YALGrammerLexer(input);
            CommonTokenStream stream = new CommonTokenStream(lexer);
            YALGrammerParser parser = new YALGrammerParser(stream);
            parser.AddErrorListener(new MyErrorListener());

            YALGrammerVisitor visitor = new YALGrammerVisitor(errorHandler, warningsHandler);

            var program = parser.program();
            ASTNode root = (ASTNode)visitor.Visit(program);

            LinkerASTTraverser parentsLinker = new(root);
            parentsLinker.BeginTraverse();

            TypeAndScopeCheckerTraverser traverser = new(root, errorHandler, warningsHandler);
            traverser.BeginTraverse();

            if (warningsHandler.Warnings.Count > 0)
            {
                Console.WriteLine("WARNINGS:");
                Console.WriteLine(warningsHandler);
            }

            if (errorHandler.Errors.Count > 0)
            {
                Console.WriteLine("ERRORS:");
                Console.WriteLine(errorHandler);
                return;
            }

            CodeGenTraverser cgt = new(root);
            cgt.BeginTraverse();

            File.WriteAllText(OutputFilePath, cgt.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}