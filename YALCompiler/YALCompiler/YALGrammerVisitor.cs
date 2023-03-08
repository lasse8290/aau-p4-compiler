using System.Diagnostics;
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
        var ifPath = new If
        {
            Parent = ifStatement
        };

        if (Visit(context.predicate()) is Predicate predicate)
            ifPath.Predicate = predicate;

        if (Visit(context.statementBlock()) is ASTNode ifNode)
        {
            ifNode.Parent = ifPath;
            ifPath.Children.Add(ifNode);
            
        }
        
        ifStatement.Children.Add(ifPath);
        
        if (context.elseIfStatement() != null)
        {
            foreach (var elseIf in context.elseIfStatement())
            {
                var elseIfPath = new ElseIf
                {
                    Parent = ifStatement
                };

                if (Visit(elseIf.predicate()) is Predicate elseIfPredicate)
                    elseIfPath.Predicate = elseIfPredicate;

                if (Visit(elseIf.statementBlock()) is ASTNode elseIfNode)
                {
                    elseIfNode.Parent = elseIfPath;
                    elseIfPath.Children.Add(elseIfNode);
                }

                ifStatement.Children.Add(elseIfPath);
            }
        }
        
        if (context.elseStatement() != null)
        {
            var elsePath = new Else
            {
                Parent = ifStatement
            };

            if (Visit(context.elseStatement().statementBlock()) is ASTNode elseNode)
            {
                elseNode.Parent = elsePath;
                elsePath.Children.Add(elseNode);
            }
            
            ifStatement.Children.Add(elsePath);
        }
        
        return ifStatement;
    }

    #region PredicateVisitors

    public override object VisitNot(YALGrammerParser.NotContext context)
    {
        if (Visit(context.predicate()) is not Predicate predicate) return null;
        predicate.Negated = true;
        return predicate;
    }

    public override object VisitAnd(YALGrammerParser.AndContext context)
    {
        var compoundPredicate = new CompoundPredicate
        {
            Operator = Operators.PredicateOperator.And,
            Left = Visit(context.predicate(0)) as Expression,
            Right = Visit(context.predicate(1)) as Expression
        };
        return compoundPredicate;
    }
    
    public override object VisitOr(YALGrammerParser.OrContext context)
    {
        var compoundPredicate = new CompoundPredicate
        {
            Operator = Operators.PredicateOperator.Or,
            Left = Visit(context.predicate(0)) as Expression,
            Right = Visit(context.predicate(1)) as Expression
        };
        return compoundPredicate;
    }
    
    public override object VisitLessThan(YALGrammerParser.LessThanContext context)
    {
        var compoundPredicate = new CompoundPredicate
        {
            Operator = Operators.PredicateOperator.LessThan,
            Left = Visit(context.predicate(0)) as Expression,
            Right = Visit(context.predicate(1)) as Expression
        };
        return compoundPredicate;
    }
    
    public override object VisitLessThanOrEqual(YALGrammerParser.LessThanOrEqualContext context)
    {
        var compoundPredicate = new CompoundPredicate
        {
            Operator = Operators.PredicateOperator.LessThanOrEqual,
            Left = Visit(context.predicate(0)) as Expression,
            Right = Visit(context.predicate(1)) as Expression
        };
        return compoundPredicate;
    }
    
    public override object VisitGreaterThan(YALGrammerParser.GreaterThanContext context)
    {
        var compoundPredicate = new CompoundPredicate
        {
            Operator = Operators.PredicateOperator.GreaterThan,
            Left = Visit(context.predicate(0)) as Expression,
            Right = Visit(context.predicate(1)) as Expression
        };
        return compoundPredicate;
    }
    
    public override object VisitGreaterThanOrEqual(YALGrammerParser.GreaterThanOrEqualContext context)
    {
        var compoundPredicate = new CompoundPredicate
        {
            Operator = Operators.PredicateOperator.GreaterThanOrEqual,
            Left = Visit(context.predicate(0)) as Expression,
            Right = Visit(context.predicate(1)) as Expression
        };
        return compoundPredicate;
    }
    
    public override object VisitEquals(YALGrammerParser.EqualsContext context)
    {
        var compoundPredicate = new CompoundPredicate
        {
            Operator = Operators.PredicateOperator.Equals,
            Left = Visit(context.predicate(0)) as Expression,
            Right = Visit(context.predicate(1)) as Expression
        };
        return compoundPredicate;
    }
    
    public override object VisitNotEquals(YALGrammerParser.NotEqualsContext context)
    {
        var compoundPredicate = new CompoundPredicate
        {
            Operator = Operators.PredicateOperator.NotEquals,
            Left = Visit(context.predicate(0)) as Expression,
            Right = Visit(context.predicate(1)) as Expression
        };
        return compoundPredicate;
    }

    public override object VisitParenthesizedPredicate(YALGrammerParser.ParenthesizedPredicateContext context)
    {
        return Visit(context.predicate());
    }

    public override object VisitBooleanLiteral(YALGrammerParser.BooleanLiteralContext context)
    {
        DataTypes.Boolean boolean = new();
        bool? val = context.BOOLEAN().GetText() switch
        {
            "true" => true,
            "false" => false,
            _ => null
        };
        boolean.LiteralValue = val;
        return boolean;
    }
    
    #endregion
    
    #region ExpressionVisitors

    public override object VisitVariable(YALGrammerParser.VariableContext context)
    {
        return new Identifier(context.ID().GetText());
    }
    
    public override object VisitNumberLiteral(YALGrammerParser.NumberLiteralContext context)
    {
        if (long.TryParse(context.SIGNED_NUMBER().GetText(), out var number))
            return new SignedNumber(number);
        return null;
    }

    public override object VisitStringLiteral(YALGrammerParser.StringLiteralContext context)
    {
        return new StringLiteral(context.STRING().GetText());
    }

    public override object VisitParenthesizedExpression(YALGrammerParser.ParenthesizedExpressionContext context)
    {
        return Visit(context.expression());
    }

    #endregion

    public override object VisitFunctionCall(YALGrammerParser.FunctionCallContext context)
    {
        var functionCall = new FunctionCall(context.ID().GetText(), context.AWAIT() != null);
        functionCall.InputParameters = Visit(context.actualInputParams()) as List<Expression>;
        
        return functionCall;
    }

    public override object VisitActualInputParams(YALGrammerParser.ActualInputParamsContext context)
    {
        List<Expression> actualInputParams = new();
        foreach (var expression in context.expression())
        {
            if (Visit(expression) is Expression expr)
                actualInputParams.Add(expr);
        }

        return actualInputParams;
    }


    
} 