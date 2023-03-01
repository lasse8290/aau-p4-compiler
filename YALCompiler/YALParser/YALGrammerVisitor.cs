using Antlr4.Runtime.Misc;

namespace YALParser;

public class YALGrammerVisitor : YALGrammerBaseVisitor<object> {
    private Dictionary<string, object?> Variables { get; } = new();
    private List<FunctionCall> functionCalls = new();

    public override object VisitProgram(YALGrammerParser.ProgramContext context) {

        return base.VisitProgram(context);
    }

    public override object VisitFunction(YALGrammerParser.FunctionContext context) {
        return base.VisitFunction(context);
    }

    public override object VisitCommand([NotNull] YALGrammerParser.CommandContext context)
    {
        if (context.variableDeclaration() is { } varDecl)
        {
            return Visit(varDecl);
        } else if (context.assignment() is { } assignment)
        { 
            return Visit(assignment);
        }
        else if (context.functionCall() is { } functionCall)
        {
            return Visit(functionCall);
        }

        return null;
    }

    public override object VisitAssignment(YALGrammerParser.AssignmentContext context)
    {
        string? varName;

        if (context.ID() is { } id) { // check if id is not null
            varName = id.GetText();
        } else
        {
            varName = Visit(context.variableDeclaration()).ToString();
        }

        if (varName is null)
        {
            throw new ArgumentNullException(nameof(varName));
        }

        var value = Visit(context.expression());

        Variables[varName] = value;

        return value;

    }

    public override string VisitVariableDeclaration([NotNull] YALGrammerParser.VariableDeclarationContext context) // returns variable name
    {
        var varName = context.ID().GetText();

        Variables[varName] = null;

        return varName;
    }

    public override object VisitExpression([NotNull] YALGrammerParser.ExpressionContext context)
    {
        var baseExpressions = context.baseExpression();

        if (baseExpressions.Length == 1)
        {
            return Visit(baseExpressions[0]);
        }
        else if (baseExpressions.Length > 1)
        {
            // right now this basically just returns the whole expression as a string.. should probably be done smarter. i'm too tired
            return context.GetText();
        }

        return context.GetText();
    }

    public override object VisitBaseExpression([NotNull] YALGrammerParser.BaseExpressionContext context)
    {
        if (context.ID() is { } id) {
            
            return id;
        } else if (context.NUMBER() is { } n)
        {
            // it's just a number 
            return int.Parse(n.GetText());
        } else if (context.functionCall() is { } functionCall)
        {
            object? funcCall = Visit(context.functionCall());
            if (funcCall is FunctionCall f)
                functionCalls.Add(f);
        }

        return null;
    }

    public override object VisitFunctionCall([NotNull] YALGrammerParser.FunctionCallContext context)
    {
        var id = context.ID().GetText();

        var expressions = new List<object>();

        expressions.AddRange(context.expression().Select(Visit));

        return new FunctionCall(id, expressions);
    }

    struct FunctionCall
    {
        string ID;
        IEnumerable<object> Expressions;
        public FunctionCall(string id, IEnumerable<object> expressions)
        {
            ID = id;
            Expressions = expressions;
        }
    }
}