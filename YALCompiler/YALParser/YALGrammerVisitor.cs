namespace YALParser; 

public class YALGrammerVisitor : YALGrammerBaseVisitor<int> {
    public override int VisitAssignment(YALGrammerParser.AssignmentContext context) {
        return base.VisitAssignment(context);
    }

    public override int VisitProgram(YALGrammerParser.ProgramContext context) {
        
        return base.VisitProgram(context);
    }

    public override int VisitFunction(YALGrammerParser.FunctionContext context) {
        return base.VisitFunction(context);
    }
}