using StringTemplating;
using Antlr4.Runtime;
using YALCompiler.ErrorHandlers;
using YALCompiler.DataTypes;
using YALCompiler;

public class Transpiler
{
    ErrorHandler errorHandler = new ErrorHandler();
    WarningsHandler warningsHandler = new WarningsHandler();
    public string InputFilePath { get; set; }
    public string OutputFilePath { get; set; }
    string TemplatesPath = Directory.GetParent(Directory.GetCurrentDirectory()) + "/StringTemplating/Templates";
    string SourceCode;
    ASTNode root = default!;

    public Transpiler(string InputFilePath, string OutputFilePath)
    {
        this.InputFilePath = InputFilePath;
        this.OutputFilePath = OutputFilePath;

        Template.LoadTemplates(TemplatesPath, "txt");
        SourceCode = File.ReadAllText(InputFilePath);
    }


    public void Transpile()
    {
        try
        {
            Parse();

            AssignParentChildRelations();

            PerformSemanticAnalysis();

            PrintWarnings();

            CheckErrors();

            string code = GenerateCode();

            File.WriteAllText(OutputFilePath, code);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    // Generates Abstract Syntax Tree
    public void Parse()
    {
        AntlrInputStream input = new AntlrInputStream(SourceCode.ToString());
        YALGrammerLexer lexer = new YALGrammerLexer(input);
        CommonTokenStream stream = new CommonTokenStream(lexer);
        YALGrammerParser parser = new YALGrammerParser(stream);
        parser.AddErrorListener(new ParserErrorListener());

        YALGrammerVisitor visitor = new YALGrammerVisitor(errorHandler, warningsHandler);

        var program = parser.program();
        root = (ASTNode)visitor.Visit(program);
    }

    // Sets up the parent-child relationships between nodes in the AST
    public void AssignParentChildRelations() {
        LinkerASTTraverser parentsLinker = new(root);
        parentsLinker.BeginTraverse();
    }

    // Ensures types and scopes in the program are valid
    public void PerformSemanticAnalysis() {
        TypeAndScopeCheckerTraverser traverser = new(root, errorHandler, warningsHandler);
        traverser.BeginTraverse();
    }

    // Printing warnings if any
    public void PrintWarnings() {
        if (warningsHandler.Warnings.Count > 0)
        {
            Console.WriteLine("WARNINGS:");
            Console.WriteLine(warningsHandler);
        }
    }

    // Check for errors and printing them
    public void CheckErrors() {
        if (errorHandler.Errors.Count > 0)
        {
            throw new Exception(errorHandler.ToString());
        }
    }

    // Generate code and returning it
    public string GenerateCode() {
        CodeGenTraverser cgt = new(root);
        cgt.BeginTraverse();

        return cgt.ToString();
    }
}