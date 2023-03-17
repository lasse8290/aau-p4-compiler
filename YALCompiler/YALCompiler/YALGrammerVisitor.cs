using System.Linq.Expressions;
using YALCompiler.DataTypes;
using YALCompiler.ErrorHandlers;
using YALCompiler.Exceptions;
using YALCompiler.Helpers;
using Expression = YALCompiler.DataTypes.Expression;

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
                    program.AddSymbolOrFunction(symbol);
                }
                catch (VariableAlreadyExistsException e)
                {
                    _errorHandler.AddError(e, gvd);                    
                }
            }
        }
        
        foreach (var func in context.externalFunctionDeclaration())
        {
            if (Visit(func) is ExternalFunction f)
            {
                try
                {
                    program.AddSymbolOrFunction(f);
                }
                catch (VariableAlreadyExistsException e)
                {
                    _errorHandler.AddError(e, func);                    
                }
            }
        }

        foreach (var func in context.functionDeclaration())
        {
            if (Visit(func) is Function f)
            {
                program.Children.Add(f);
                
                try
                {
                    program.AddSymbolOrFunction(f);
                }
                catch (VariableAlreadyExistsException e)
                {
                    _errorHandler.AddError(e, func);                    
                }
            }
        }
        program.LinkChildrenNodesToParent();
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
        
        var symbol = new Symbol(id);
        symbol.Type = type;

        if (context.POSITIVE_NUMBER() != null && int.TryParse(context.POSITIVE_NUMBER().GetText(), out int size))
        {
            symbol.ArraySize = size;
        }
        
        if (context.predicate() != null && Visit(context.predicate()) is Predicate predicate)
        {
            symbol.Value = predicate;
            symbol.Initialized = true;
        }
        
        return symbol;
    }

    public override object VisitExternalFunctionDeclaration(YALGrammerParser.ExternalFunctionDeclarationContext context)
    {
        var func = new ExternalFunction
        {
            LibraryName = context.STRING().GetText().Trim().Substring(1, context.STRING().GetText().Trim().Length - 2),
            Id = context.ID().GetText(),
            
        };

        //handle input params
        if (context.formalInputParams() != null && Visit(context.formalInputParams()) is List<Symbol> inSymbols)
        {
            foreach (var symbol in inSymbols)
            {
                try
                {
                    func.AddSymbolOrFunction(symbol);
                    func.InputParameters.Add(symbol);
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
                    func.AddSymbolOrFunction(symbol);
                    func.OutputParameters.Add(symbol);
                }
                catch (VariableAlreadyExistsException e)
                {
                    _errorHandler.AddError(e, context);                    
                }
            }
        }

        if (func.OutputParameters.Count == 1)
        {
            func.ReturnType = func.OutputParameters[0].Type;
        } else if (func.OutputParameters.Count > 1)
        {
            func.ReturnType = new TupleType(func.OutputParameters.Select(param => param.Type as SingleType).ToArray());
        }
        
        return func;
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
                    func.AddSymbolOrFunction(symbol);
                    func.InputParameters.Add(symbol);
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
                    func.AddSymbolOrFunction(symbol);
                    func.OutputParameters.Add(symbol);
                }
                catch (VariableAlreadyExistsException e)
                {
                    _errorHandler.AddError(e, context);                    
                }
            }
        }

        if (func.OutputParameters.Count == 1)
        {
            func.ReturnType = func.OutputParameters[0].Type;
        } else if (func.OutputParameters.Count > 1)
        {
            func.ReturnType = new TupleType(func.OutputParameters.Select(param => param.Type as SingleType).ToArray());
        }

        StatementBlock statementBlock = Visit(context.statementBlock()) as StatementBlock;
        foreach (ASTNode stmt in statementBlock.Statements)
        {
            func.Children.Add(stmt);
        }
        
        foreach (Symbol symbol in statementBlock.LocalVariables)
        {
            try
            {
                func.AddSymbolOrFunction(symbol);
            }
            catch (VariableAlreadyExistsException e)
            {
                _errorHandler.AddError(e, context);                    
            }
        }

        func.LineNumber = context.Start.Line;
        return func;
    }
    
    public override object VisitFormalInputParams(YALGrammerParser.FormalInputParamsContext context)
    {
        var paramVars = new List<Symbol>();
        foreach (var varDecl in context.variableDeclarationFormat())
        {
            if (Visit(varDecl) is VariableDeclaration {Variable: Symbol symbol})
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
            if (Visit(varDecl) is VariableDeclaration {Variable: Symbol symbol})
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

        if (int.TryParse(context.POSITIVE_NUMBER().GetText(), out int size))
        {
            symbol.ArraySize = size;
        }

        return new VariableDeclaration {Variable = symbol};
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

        return new VariableDeclaration {Variable = symbol};
    }

    public override object VisitStatementBlock(YALGrammerParser.StatementBlockContext context)
    {
        var statementBlock = new StatementBlock();
        foreach (var statement in context.children)
        {
            var stmt = Visit(statement);
            
            if (stmt is ASTNode node)
                statementBlock.Statements.Add(node);
            
            switch (stmt)
            {
                case VariableDeclaration varDecl:
                    statementBlock.LocalVariables.Add(varDecl.Variable);
                    break;
                case TupleDeclaration tupleDecl:
                    statementBlock.LocalVariables.AddRange(tupleDecl.Variables);
                    break;
                case BinaryAssignment { Target: VariableDeclaration variableDeclaration }:
                    statementBlock.LocalVariables.Add(variableDeclaration.Variable);
                    break;
                case BinaryAssignment { Target: TupleDeclaration tupleDeclaration }:
                    statementBlock.LocalVariables.AddRange(tupleDeclaration.Variables);
                    break;
            }
        }
        return statementBlock;
    }

    public override object VisitBlockStatement(YALGrammerParser.BlockStatementContext context)
    {
        ASTNode node = null;
        if (context.ifStatement() != null)
            node = Visit(context.ifStatement()) as ASTNode;
            
        if (context.whileStatement() != null)
            node = Visit(context.whileStatement()) as ASTNode;

        if (context.forStatement() != null)
            node = Visit(context.forStatement()) as ASTNode;

        if (node is not null)
            node.LineNumber = context.Start.Line;
        
        return node;
    }

    public override object VisitSingleStatement(YALGrammerParser.SingleStatementContext context)
    {
        ASTNode node = null;
        if (context.variableDeclaration() != null)
            node = Visit(context.variableDeclaration()) as ASTNode;
        
        if (context.assignment() != null)
            node = Visit(context.assignment()) as ASTNode;

        if (context.functionCall() != null)
            node = Visit(context.functionCall()) as ASTNode;

        if (context.RETURN() != null)
            node = new ReturnStatement();
        
        if (node is not null)
            node.LineNumber = context.Start.Line;
        
        return node;
    }

    public override object VisitIfStatement(YALGrammerParser.IfStatementContext context)
    {
        var ifStatement = new IfStatement();
        var ifPath = new If();

        if (Visit(context.predicate()) is Predicate predicate)
            ifPath.Predicate = predicate;
        
        StatementBlock statementBlock = Visit(context.statementBlock()) as StatementBlock;
        foreach (ASTNode stmt in statementBlock.Statements)
        {
            ifPath.Children.Add(stmt);
        }
        
        foreach (Symbol symbol in statementBlock.LocalVariables)
        {
            try
            {
                ifPath.SymbolTable.Add(symbol);
            }
            catch (VariableAlreadyExistsException e)
            {
                _errorHandler.AddError(e, context);                    
            }
        }
        ifPath.LineNumber = context.Start.Line;
        ifStatement.Children.Add(ifPath);
        
        if (context.elseIfStatement() != null)
        {
            foreach (var elseIf in context.elseIfStatement())
            {
                var elseIfPath = new ElseIf();

                if (Visit(elseIf.predicate()) is Predicate elseIfPredicate)
                    elseIfPath.Predicate = elseIfPredicate;

                StatementBlock elseIfStatementBlock = Visit(elseIf.statementBlock()) as StatementBlock;
                foreach (ASTNode stmt in elseIfStatementBlock.Statements)
                {
                    elseIfPath.Children.Add(stmt);
                }
        
                foreach (Symbol symbol in elseIfStatementBlock.LocalVariables)
                {
                    try
                    {
                        elseIfPath.SymbolTable.Add(symbol);
                    }
                    catch (VariableAlreadyExistsException e)
                    {
                        _errorHandler.AddError(e, context);                    
                    }
                }
                elseIfPath.LineNumber = elseIf.Start.Line;
                ifStatement.Children.Add(elseIfPath);
            }
        }
        
        if (context.elseStatement() != null)
        {
            var elsePath = new Else();

            StatementBlock elseStatementBlock = Visit(context.elseStatement().statementBlock()) as StatementBlock;
            foreach (ASTNode stmt in elseStatementBlock.Statements)
            {
                elsePath.Children.Add(stmt);
            }
        
            foreach (Symbol symbol in elseStatementBlock.LocalVariables)
            {
                try
                {
                    elsePath.SymbolTable.Add(symbol);
                }
                catch (VariableAlreadyExistsException e)
                {
                    _errorHandler.AddError(e, context);                    
                }
            }
            elsePath.LineNumber = context.elseStatement().Start.Line;
            ifStatement.Children.Add(elsePath);
        }
        
        return ifStatement;
    }

    public override object VisitWhileStatement(YALGrammerParser.WhileStatementContext context)
    {
        WhileStatement whileStatement = new WhileStatement
        {
            Predicate = Visit(context.predicate()) as Predicate
        };
        
        StatementBlock statementBlock = Visit(context.statementBlock()) as StatementBlock;
        foreach (ASTNode stmt in statementBlock.Statements)
        {
            whileStatement.Children.Add(stmt);
        }
        
        foreach (Symbol symbol in statementBlock.LocalVariables)
        {
            try
            {
                whileStatement.SymbolTable.Add(symbol);
            }
            catch (VariableAlreadyExistsException e)
            {
                _errorHandler.AddError(e, context);                    
            }
        }

        return whileStatement;
    }

    public override object VisitForStatement(YALGrammerParser.ForStatementContext context)
    {
        var forStatement = new ForStatement();
        
        BinaryAssignment declAssignment = Visit(context.declarationAssignment()) as BinaryAssignment;
        if (declAssignment is BinaryAssignment { Target: VariableDeclaration })
            forStatement.DeclarationAssignment = declAssignment;
        
        forStatement.RunCondition = Visit(context.predicate()) as Predicate;
        
        forStatement.LoopAssignment = Visit(context.assignment()) as Assignment;

        StatementBlock statementBlock = Visit(context.statementBlock()) as StatementBlock;
        foreach (ASTNode stmt in statementBlock.Statements)
        {
            forStatement.Children.Add(stmt);
        }
        
        foreach (Symbol symbol in statementBlock.LocalVariables)
        {
            try
            {
                forStatement.SymbolTable.Add(symbol);
            }
            catch (VariableAlreadyExistsException e)
            {
                _errorHandler.AddError(e, context);                    
            }
        }

        return forStatement;
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

    public override object VisitComparison(YALGrammerParser.ComparisonContext context)
    {
        var compoundPredicate = new CompoundPredicate
        {
            Left = Visit(context.predicate(0)) as Expression,
            Right = Visit(context.predicate(1)) as Expression
        };
        switch (context.@operator.Type)
        {
            case YALGrammerLexer.LESS_THAN:
                compoundPredicate.Operator = Operators.PredicateOperator.LessThan;
                break;
            case YALGrammerLexer.LESS_THAN_OR_EQUAL:
                compoundPredicate.Operator = Operators.PredicateOperator.LessThanOrEqual;
                break;
            case YALGrammerLexer.GREATER_THAN:
                compoundPredicate.Operator = Operators.PredicateOperator.GreaterThan;
                break; 
            case YALGrammerLexer.GREATER_THAN_OR_EQUAL:
                compoundPredicate.Operator = Operators.PredicateOperator.GreaterThanOrEqual;
                break;
            case YALGrammerLexer.EQUAL:
                compoundPredicate.Operator = Operators.PredicateOperator.Equals;
                break;
            case YALGrammerLexer.NOT_EQUAL:
                compoundPredicate.Operator = Operators.PredicateOperator.NotEquals;
                break;
        }
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
        return Visit(context.identifier());
    }

    public override object VisitNumberLiteral(YALGrammerParser.NumberLiteralContext context)
    {
        if (UInt64.TryParse(context.POSITIVE_NUMBER().GetText(), out var number))
            return new SignedNumber(number, context.MINUS() == null);
        return null;
    }
    
    public override object VisitFloatLiteral(YALGrammerParser.FloatLiteralContext context)
    {
        if (double.TryParse(context.FLOAT().GetText(), out var number))
            return new SignedFloat(number, context.MINUS() == null);
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

    public override object VisitMultiplicationDivisionModulo(YALGrammerParser.MultiplicationDivisionModuloContext context)
    {
        var compoundExpression = new CompoundExpression
        {
            Left = Visit(context.expression(0)) as Expression,
            Right = Visit(context.expression(1)) as Expression
        };
        switch (context.@operator.Type)
        {
            case YALGrammerLexer.TIMES:
                compoundExpression.Operator = Operators.ExpressionOperator.Multiplication;
                break;
            case YALGrammerLexer.DIV:
                compoundExpression.Operator = Operators.ExpressionOperator.Division;
                break;
            case YALGrammerLexer.MOD:
                compoundExpression.Operator = Operators.ExpressionOperator.Modulo;
                break;
        }
        return compoundExpression;
    }
    
    public override object VisitAdditionSubtraction(YALGrammerParser.AdditionSubtractionContext context)
    {
        var compoundExpression = new CompoundExpression
        {
            Left = Visit(context.expression(0)) as Expression,
            Right = Visit(context.expression(1)) as Expression
        };
        switch (context.@operator.Type)
        {
            case YALGrammerLexer.PLUS:
                compoundExpression.Operator = Operators.ExpressionOperator.Addition;
                break;
            case YALGrammerLexer.MINUS:
                compoundExpression.Operator = Operators.ExpressionOperator.Subtraction;
                break;
        }
        return compoundExpression;
    }

    public override object VisitLeftRightShift(YALGrammerParser.LeftRightShiftContext context)
    {
        var compoundExpression = new CompoundExpression
        {
            Left = Visit(context.expression(0)) as Expression,
            Right = Visit(context.expression(1)) as Expression
        };
        switch (context.@operator.Type)
        {
            case YALGrammerLexer.RSHIFT:
                compoundExpression.Operator = Operators.ExpressionOperator.RightShift;
                break;
            case YALGrammerLexer.LSHIFT:
                compoundExpression.Operator = Operators.ExpressionOperator.LeftShift;
                break;
        }
        return compoundExpression;
    }

    public override object VisitBitwiseAnd(YALGrammerParser.BitwiseAndContext context)
    {
        var compoundExpression = new CompoundExpression
        {
            Operator = Operators.ExpressionOperator.BitwiseAnd,
            Left = Visit(context.expression(0)) as Expression,
            Right = Visit(context.expression(1)) as Expression
        };
        return compoundExpression;
    }
    
    public override object VisitBitwiseOr(YALGrammerParser.BitwiseOrContext context)
    {
        var compoundExpression = new CompoundExpression
        {
            Operator = Operators.ExpressionOperator.BitwiseOr,
            Left = Visit(context.expression(0)) as Expression,
            Right = Visit(context.expression(1)) as Expression
        };
        return compoundExpression;
    }
    
    public override object VisitBitwiseXor(YALGrammerParser.BitwiseXorContext context)
    {
        var compoundExpression = new CompoundExpression
        {
            Operator = Operators.ExpressionOperator.BitwiseXor,
            Left = Visit(context.expression(0)) as Expression,
            Right = Visit(context.expression(1)) as Expression
        };
        return compoundExpression;
    }
    
    public override object VisitBitwiseNot(YALGrammerParser.BitwiseNotContext context)
    {
        var compoundExpression = new CompoundExpression
        {
            Operator = Operators.ExpressionOperator.BitwiseNot,
            Left = Visit(context.expression(0)) as Expression,
            Right = Visit(context.expression(1)) as Expression,
        };
        return compoundExpression;
    }

    public override object VisitPostIncrementDecrement(YALGrammerParser.PostIncrementDecrementContext context)
    {
        var compoundExpression = new UnaryCompoundExpression()
        {
            Expression = Visit(context.expression()) as Expression,
        };
        switch (context.@operator.Type)
        {
            case YALGrammerLexer.INCREMENT:
                compoundExpression.Operator = Operators.ExpressionOperator.PostIncrement;
                break;
            case YALGrammerLexer.DECREMENT:
                compoundExpression.Operator = Operators.ExpressionOperator.PostDecrement;
                break;
        }
        return compoundExpression;
    }
    
    public override object VisitPrefixUnary(YALGrammerParser.PrefixUnaryContext context)
    {
        var compoundExpression = new UnaryCompoundExpression()
        {
            Expression = Visit(context.expression()) as Expression,
        };
        switch (context.@operator.Type)
        {
            case YALGrammerLexer.INCREMENT:
                compoundExpression.Operator = Operators.ExpressionOperator.PreIncrement;
                break;
            case YALGrammerLexer.DECREMENT:
                compoundExpression.Operator = Operators.ExpressionOperator.PreDecrement;
                break;
            case YALGrammerLexer.BITWISE_NOT:
                compoundExpression.Operator = Operators.ExpressionOperator.BitwiseNot;
                break;
        }
        return compoundExpression;
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

    #region AssignmentVisitors

    public override object VisitIdAssignment(YALGrammerParser.IdAssignmentContext context)
    {
        var assignment = new BinaryAssignment()
        {
            Target = Visit(context.identifier()) as Identifier,
            Value = Visit(context.predicate()) as Expression
        };
        switch (context.@operator.Type)
        {
            case YALGrammerLexer.EQUAL:
                assignment.Operator = Operators.AssignmentOperator.Equals;
                break;
            case YALGrammerLexer.PLUS_EQUAL:
                assignment.Operator = Operators.AssignmentOperator.AdditionAssignment;
                break;
            case YALGrammerLexer.MINUS_EQUAL:
                assignment.Operator = Operators.AssignmentOperator.SubtractionAssignment;
                break;
            case YALGrammerLexer.MULTIPLY_EQUAL:
                assignment.Operator = Operators.AssignmentOperator.MultiplicationAssignment;
                break;
            case YALGrammerLexer.DIVIDE_EQUAL:
                assignment.Operator = Operators.AssignmentOperator.DivisionAssignment;
                break;
            case YALGrammerLexer.MODULO_EQUAL:
                assignment.Operator = Operators.AssignmentOperator.ModuloAssignment;
                break;
        }
        return assignment;
    }

    public override object VisitIdPostIncrementDecrementAssignment(YALGrammerParser.IdPostIncrementDecrementAssignmentContext context)
    {
        var assignment = new UnaryAssignment()
        {
            Target = Visit(context.identifier()) as Identifier,
        };
        switch (context.@operator.Type)
        {
            case YALGrammerLexer.INCREMENT:
                assignment.Operator = Operators.AssignmentOperator.PostIncrement;
                break;
            case YALGrammerLexer.DECREMENT:
                assignment.Operator = Operators.AssignmentOperator.PostDecrement;
                break;
        }
        return assignment;
    }
    
    public override object VisitIdPreIncrementDecrementAssignment(YALGrammerParser.IdPreIncrementDecrementAssignmentContext context)
    {
        var assignment = new UnaryAssignment()
        {
            Target = Visit(context.identifier()) as Identifier,
        };
        switch (context.@operator.Type)
        {
            case YALGrammerLexer.INCREMENT:
                assignment.Operator = Operators.AssignmentOperator.PreIncrement;
                break;
            case YALGrammerLexer.DECREMENT:
                assignment.Operator = Operators.AssignmentOperator.PreDecrement;
                break;
        }
        return assignment;
    }

    #endregion

    public override object VisitDeclarationAssignment(YALGrammerParser.DeclarationAssignmentContext context)
    {
        var assignment = new BinaryAssignment()
        {
            Target = Visit(context.variableDeclaration()),
            Operator = Operators.AssignmentOperator.Equals,
            Value = Visit(context.predicate()) as Expression
        };
        return assignment;
    }
    
    public override object VisitTupleAssignment(YALGrammerParser.TupleAssignmentContext context)
    {
        var assignment = new BinaryAssignment()
        {
            Target = Visit(context.tupleDeclaration()),
            Operator = Operators.AssignmentOperator.Equals,
            Value = Visit(context.expression()) as Expression
        };
        return assignment;
    }
    
    public override object VisitTupleDeclaration(YALGrammerParser.TupleDeclarationContext context)
    {
        var tupleDeclaration = new TupleDeclaration();
        foreach (var variableDeclaration in context.variableDeclarationFormat())
        {
            if (Visit(variableDeclaration) is VariableDeclaration { Variable: Symbol symbol})
            tupleDeclaration.Variables.Add(symbol);
        }

        return tupleDeclaration;
    }

    public override object VisitSimpleIdentifier(YALGrammerParser.SimpleIdentifierContext context)
    {
        return new Identifier(context.ID().GetText());
    }

    public override object VisitArrayElementIdentifier(YALGrammerParser.ArrayElementIdentifierContext context)
    {
        return new ArrayElementIdentifier(context.ID().GetText(), Visit(context.expression()) as Expression);
    }

    
    
} 