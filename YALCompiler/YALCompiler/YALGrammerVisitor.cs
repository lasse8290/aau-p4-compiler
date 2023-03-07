using YALCompiler.DataTypes;
using YALCompiler.ErrorHandlers;
using YALCompiler.Exceptions;
using YALCompiler.Helpers;
using static YALCompiler.Helpers.Utilities;

namespace YALCompiler;

public class YALGrammerVisitor : YALGrammerBaseVisitor<object> {
    
    private readonly ErrorHandler _errorHandler;
    private readonly WarningsHandler _warningsHandler;

    public YALGrammerVisitor(ErrorHandler errorHandler, WarningsHandler warningsHandler)
    {
        _errorHandler = errorHandler;
        _warningsHandler = warningsHandler;
    }

    public override object VisitProgram(YALGrammerParser.ProgramContext context)
    {
        DataTypes.Program program = new();
        
        foreach(var gvd in context.globalVariableDeclaration())
        {
            if (Visit(gvd) is Symbol symbol)
            {
                try
                {
                    program.SymbolTable.Add(symbol);
                }
                catch (VariableAlreadyExistsException e)
                {
                    _errorHandler.AddError(e, gvd);                    
                }
            }
        }

        foreach (var func in context.functionDeclaration())
        {
            if (Visit(func) is Function f)
            {
                f.Parent = program;
                program.Children.Add(f);
            }
        }
        
        return program;
    }
    
    public override object VisitGlobalVariableDeclaration(YALGrammerParser.GlobalVariableDeclarationContext context)
    {
        string id = context.ID().GetText();
        SingleType? type = null;
        try
        {
            type = new(context.TYPE().GetText());
        }
        catch (TypeNotRecognizedException e)
        {
            _errorHandler.AddError(e, context);
        }
        
        var s = new Symbol(id);
        s.Type = type;
        
        if (context.ARRAY_DEFINER() != null)
        {
            var text = context.ARRAY_DEFINER().GetText();
            bool arr = int.TryParse(text.Substring(1, text.Length - 2), out int arraySize);
            if (arr)
            {
                s.ArraySize = arraySize;
            }
            else
            {
                //unable to parse array size
            }
        }
        
        return new Symbol(id);
    }

    public override object VisitFunctionDeclaration(YALGrammerParser.FunctionDeclarationContext context)
    {
        var func = new Function
        {
            Id = context.ID().GetText(),
            IsAsync = context.ASYNC() != null
        };

        //handle input params
        if (context.formalInputParams() != null && Visit(context.formalInputParams()) is List<Symbol> inSymbols)
        {
            foreach (var symbol in inSymbols)
            {
                try
                {
                    func.SymbolTable.Add(symbol);
                }
                catch (VariableAlreadyExistsException e)
                {
                    _errorHandler.AddError(e, context);                    
                }
            }
        }
        
        //handle output params
        if (context.formalOutputParams() != null && Visit(context.formalOutputParams()) is List<Symbol> outSymbols)
        {
            foreach (var symbol in outSymbols)
            {
                try
                {
                    func.SymbolTable.Add(symbol);
                }
                catch (VariableAlreadyExistsException e)
                {
                    _errorHandler.AddError(e, context);                    
                }
            }
        }
        
        //handle statementsBlock
        var x = Visit(context.statementBlock());
        
        return func;
    }
    
    public override object VisitFormalInputParams(YALGrammerParser.FormalInputParamsContext context)
    {
        var paramVars = new List<Symbol>();
        foreach (var varDecl in context.variableDeclarationFormat())
        {
            if (Visit(varDecl) is Symbol symbol)
            {
                paramVars.Add(symbol);
            }
        }
        return paramVars;
    }
    
    public override object VisitFormalOutputParams(YALGrammerParser.FormalOutputParamsContext context)
    {
        var paramVars = new List<Symbol>();
        foreach (var varDecl in context.variableDeclarationFormat())
        {
            if (Visit(varDecl) is Symbol symbol)
            {
                paramVars.Add(symbol);
            }
        }
        return paramVars;
    }

    public override object VisitArrayDeclaration(YALGrammerParser.ArrayDeclarationContext context)
    {
        var symbol = new Symbol(context.ID().GetText());
        
        try
        {
            symbol.Type = new SingleType(context.TYPE().GetText());
        }
        catch (TypeNotRecognizedException e)
        {
            _errorHandler.AddError(e, context);
        }
        
        try
        {
            symbol.ArraySize = GetArraySizeFromDefiner(context.ARRAY_DEFINER().GetText());
        }
        catch (ArraySizeNotRecognizedException e)
        {
            _errorHandler.AddError(e, context);
        }

        return symbol;
    } 
    
    public override object VisitSimpleVariableDeclaration(YALGrammerParser.SimpleVariableDeclarationContext context)
    {
        var symbol = new Symbol(context.ID().GetText());
        
        try
        {
            symbol.Type = new SingleType(context.TYPE().GetText());
        }
        catch (TypeNotRecognizedException e)
        {
            _errorHandler.AddError(e, context);
        }

        return symbol;
    }

    public override object VisitStatementBlock(YALGrammerParser.StatementBlockContext context)
    {
        List<object> statements = new();
        foreach (var statement in context.children)
        {
            if (Visit(statement) is ASTNode node)
                statements.Add(node);
        }
        return statements;
    }

    public override object VisitIfStatement(YALGrammerParser.IfStatementContext context)
    {
        var ifStatement = new IfStatement();
        var ifPath = new If();
        
        if (Visit(context.predicate()) is {} predicate)
            ifPath.Predicate = predicate as string; //fix type
        
        if (Visit(context.statementBlock()) is ASTNode ifNode)
            ifPath.Children.Add(ifNode);
        
        ifStatement.Children.Add(ifPath);
        
        if (context.elseIfStatement() != null)
        {
            foreach (var elseIf in context.elseIfStatement())
            {
                var elseIfPath = new ElseIf();
                
                if (Visit(elseIf.predicate()) is {} elseIfPredicate)
                    ifPath.Predicate = elseIfPredicate as string; //fix type
                
                if (Visit(elseIf.statementBlock()) is ASTNode elseIfNode)
                    elseIfPath.Children.Add(elseIfNode);

                ifStatement.Children.Add(elseIfPath);
            }
        }
        
        if (context.elseStatement() != null)
        {
            var elsePath = new Else();
            
            if (Visit(context.elseStatement().statementBlock()) is ASTNode elseNode)
                elsePath.Children.Add(elseNode);
            
            ifStatement.Children.Add(elsePath);
        }
        
        return ifStatement;
    }

    
    
} 