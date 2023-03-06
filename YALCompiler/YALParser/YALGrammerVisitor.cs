using Antlr4.Runtime.Misc;

namespace YALParser;

public class YALGrammerVisitor : YALGrammerBaseVisitor<object> {
    private Dictionary<string, object?> Variables { get; } = new();

    public override object VisitProgram(YALGrammerParser.ProgramContext context) {

        return base.VisitProgram(context);
    }
    
    
} 