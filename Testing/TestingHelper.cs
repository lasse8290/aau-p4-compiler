using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using YALCompiler;
using YALCompiler.ErrorHandlers;

public abstract class TestingHelper {
    YALGrammerVisitor visitor = new YALGrammerVisitor(new ErrorHandler(), new WarningsHandler());
    
    public YALGrammerParser Setup(string input)
    {
        AntlrInputStream inputStream = new AntlrInputStream(input);
        YALGrammerLexer lexer = new YALGrammerLexer(inputStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
        YALGrammerParser parser = new YALGrammerParser(commonTokenStream);

        return parser;
    }

    public object Setup(string input, string methodName)
    {
        YALGrammerParser parser = Setup(input);
        IParseTree tree = Parse(parser, methodName);

        var root = visitor.Visit(tree);

        return root;
    }

    public IParseTree Parse(YALGrammerParser parser, string methodName)
    {
        var method = parser.GetType().GetMethod(methodName);
        return (IParseTree)(method?.Invoke(parser, null) ?? throw new InvalidOperationException($"Method invocation of {methodName} returned null."));
    }
}