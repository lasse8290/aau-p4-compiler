using Antlr4.Runtime;

public class ParserErrorListener : BaseErrorListener {
    public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
    {
        throw new Exception("Parser errors");
    }
}
