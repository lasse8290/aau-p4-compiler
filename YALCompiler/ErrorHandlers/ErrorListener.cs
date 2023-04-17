using Antlr4.Runtime;

public class MyErrorListener : BaseErrorListener {
    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        throw new Exception("Syntax is wrong");
    }
}
