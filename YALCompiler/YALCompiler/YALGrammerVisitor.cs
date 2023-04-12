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
    
    public YALGrammerVisitor() {}

    public override object VisitProgram(YALGrammerParser.ProgramContext context)
    {
        DataTypes.Program program = new();

        foreach(var gvd in context.variableDeclaration())
        {
            if (Visit(gvd) is VariableDeclaration variableDeclaration)
            {
                try
                {
                    program.AddSymbolOrFunction(variableDeclaration.Variable);
                    program.Children.Add(variableDeclaration);
                }
                catch (VariableAlreadyExistsException e)
                {
                    _errorHandler.AddError(e, gvd);                    
                }
            }
        }
        
        foreach(var gvd in context.assignment())
        {
            var assignment = Visit(gvd);
            if (assignment is BinaryAssignment binaryAssignment)
            {
                foreach (var target in binaryAssignment.Targets)
                {
                    try
                    {
                        if (target is VariableDeclaration variableDeclaration)
                        {
                            program.AddSymbolOrFunction(variableDeclaration.Variable);
                            program.Children.Add(binaryAssignment);
                            continue;
                        }
                        _errorHandler.AddError(new InvalidGlobalScopedAssignmentException(), gvd);
                    }
                    catch (VariableAlreadyExistsException e)
                    {
                        _errorHandler.AddError(e, gvd);                    
                    }
                }
                continue;
            }
            _errorHandler.AddError(new InvalidGlobalScopedAssignmentException(), gvd);
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
        return program;
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
                    symbol.Initialized = true;
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
            func.ReturnType = new YALType(func.OutputParameters.Select(param => param.Type).ToArray());
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
                    symbol.Initialized = true;
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
            func.ReturnType = new YALType(func.OutputParameters.Select(param => param.Type).ToArray());
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
        
        if (func.Children.LastOrDefault() is not ReturnStatement)
            func.Children.Add(new ReturnStatement());

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

    public override object VisitSimpleVariableDeclarationFormat(YALGrammerParser.SimpleVariableDeclarationFormatContext context)
    {
        var varDecls = new List<VariableDeclaration>();
        foreach (var varDecl in context.variableDeclarationFormat())
        {
            varDecls.Add(Visit(varDecl) as VariableDeclaration);
        }
        return varDecls;
    }

    
    public override object VisitArrayDeclaration(YALGrammerParser.ArrayDeclarationContext context)
    {
        var symbol = new Symbol(context.ID().GetText());
        
        try
        {
            symbol.Type = new YALType(context.TYPE().GetText(), true);
        }
        catch (TypeNotRecognizedException e)
        {
            _errorHandler.AddError(e, context);
        }

        if (context.POSITIVE_NUMBER() != null && ulong.TryParse(context.POSITIVE_NUMBER().GetText(), out ulong size))
        {
            symbol.ArraySize = size;
        }

        return new VariableDeclaration {Variable = symbol, LineNumber = context.Start.Line};
    }

    public override object VisitReferenceVariableDeclarationFormat(YALGrammerParser.ReferenceVariableDeclarationFormatContext context)
    {
        var variable = Visit(context.variableDeclarationFormat()) as VariableDeclaration;
        if (variable is not null) variable.Variable.IsRef = true;
        return variable;
    }
    
    public override object VisitSimpleVariableDeclaration(YALGrammerParser.SimpleVariableDeclarationContext context)
    {
        var symbol = new Symbol(context.ID().GetText());
        
        try
        {
            symbol.Type = new YALType(context.TYPE().GetText());
        }
        catch (TypeNotRecognizedException e)
        {
            _errorHandler.AddError(e, context);
        }

        return new VariableDeclaration {Variable = symbol, LineNumber = context.Start.Line};
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
                case BinaryAssignment binaryAssignment:
                    foreach (var target in binaryAssignment.Targets)
                    {
                        switch (target)
                        {
                            case Symbol symbol:
                                statementBlock.LocalVariables.Add(symbol);
                                break;
                            case VariableDeclaration variableDeclarationDecl:
                                statementBlock.LocalVariables.Add(variableDeclarationDecl.Variable);
                                break;
                        }
                    }
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
        var ifStatement = new IfStatement() { LineNumber = context.Start.Line};
        var ifPath = new If();

         if (Visit(context.expression()) is Expression expression) {
            ifPath.Predicate = expression;
        }
        else
        {
            _errorHandler.AddError(new InvalidPredicateException(context.expression().GetText()), context);
        }
        
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

                if (Visit(elseIf.expression()) is Expression elseIfExpression){
                    elseIfPath.Predicate = elseIfExpression;
                }
                else
                {
                    _errorHandler.AddError(new InvalidPredicateException(context.expression().GetText()), elseIf);
                }
                
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
        WhileStatement whileStatement = new WhileStatement();

        if (Visit(context.expression()) is Expression expression)
        {
            whileStatement.Predicate = expression;
        }
        else
        {
            _errorHandler.AddError(new InvalidPredicateException(context.expression().GetText()), context);
        }
        
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
        whileStatement.LineNumber = context.Start.Line;
        return whileStatement;
    }

    public override object VisitForStatement(YALGrammerParser.ForStatementContext context)
    {
        var forStatement = new ForStatement();
        
        BinaryAssignment declAssignment = Visit(context.declarationAssignment()) as BinaryAssignment;
        if (declAssignment is BinaryAssignment)
        {
            foreach (var target in declAssignment.Targets)
            {
                if (target is VariableDeclaration varDecl)
                {
                    forStatement.DeclarationAssignment = declAssignment;
                    forStatement.SymbolTable.Add(varDecl.Variable);    
                }
            }
        }

        if (Visit(context.expression()) is Expression expression)
        {
            forStatement.RunCondition = expression;
        }
        else
        {
            _errorHandler.AddError(new InvalidPredicateException(context.expression().GetText()), context);
        }

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
        forStatement.LineNumber = context.Start.Line;
        return forStatement;
    }



    #region PredicateVisitors

    public override object VisitNot(YALGrammerParser.NotContext context)
    {
        if (Visit(context.expression()) is not Expression expression) return null;
        expression.Negated = true;
        expression.LineNumber = context.Start.Line;
        return expression;
    }

    public override object VisitAnd(YALGrammerParser.AndContext context)
    {
        var compoundPredicate = new CompoundPredicate
        {
            Operator = Operators.PredicateOperator.And,
            Left = Visit(context.expression(0)) as Expression,
            Right = Visit(context.expression(1)) as Expression
        };
        compoundPredicate.LineNumber = context.Start.Line;
        return compoundPredicate;
    }
    
    public override object VisitOr(YALGrammerParser.OrContext context)
    {
        var compoundPredicate = new CompoundPredicate
        {
            Operator = Operators.PredicateOperator.Or,
            Left = Visit(context.expression(0)) as Expression,
            Right = Visit(context.expression(1)) as Expression
        };
        compoundPredicate.LineNumber = context.Start.Line;
        return compoundPredicate;
    }

    public override object VisitComparison(YALGrammerParser.ComparisonContext context)
    {
        var compoundPredicate = new CompoundPredicate
        {
            Left = Visit(context.expression(0)) as Expression,
            Right = Visit(context.expression(1)) as Expression
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
            case YALGrammerLexer.EQUALS:
                compoundPredicate.Operator = Operators.PredicateOperator.Equals;
                break;
            case YALGrammerLexer.NOT_EQUAL:
                compoundPredicate.Operator = Operators.PredicateOperator.NotEquals;
                break;
        }
        compoundPredicate.LineNumber = context.Start.Line;
        return compoundPredicate;
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
        boolean.LineNumber = context.Start.Line;
        return boolean;
    }
    
    #endregion
    
    #region ExpressionVisitors

    public override object VisitReferenceExpression(YALGrammerParser.ReferenceExpressionContext context)
    {
        var expression = Visit(context.expression()) as Expression;
        if (expression is not null) expression.IsRef = true;
        return expression;
    }

    
    public override object VisitVariable(YALGrammerParser.VariableContext context)
    {
        return Visit(context.identifier());
    }

    public override object VisitNumberLiteral(YALGrammerParser.NumberLiteralContext context)
    {
        if (UInt64.TryParse(context.POSITIVE_NUMBER().GetText(), out var number))
            return new SignedNumber(number, context.MINUS() != null) { LineNumber = context.Start.Line};
        return null;
    }
    
    public override object VisitFloatLiteral(YALGrammerParser.FloatLiteralContext context)
    {
        if (double.TryParse(context.MINUS() == null ? context.FLOAT().GetText() : "-" + context.FLOAT().GetText(), out var number))
            return new SignedFloat(number) { LineNumber = context.Start.Line};
        return null;
    }

    public override object VisitStringLiteral(YALGrammerParser.StringLiteralContext context)
    {
        return new StringLiteral(context.STRING().GetText()) { LineNumber = context.Start.Line};
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
        compoundExpression.LineNumber = context.Start.Line;
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

        compoundExpression.LineNumber = context.Start.Line;
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
        compoundExpression.LineNumber = context.Start.Line;
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
        compoundExpression.LineNumber = context.Start.Line;
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
        compoundExpression.LineNumber = context.Start.Line;
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
        compoundExpression.LineNumber = context.Start.Line;
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
        compoundExpression.LineNumber = context.Start.Line;
        return compoundExpression;
    }

    public override object VisitPostIncrementDecrement(YALGrammerParser.PostIncrementDecrementContext context)
    {
        var compoundExpression = new UnaryAssignment()
        {
            Target = Visit(context.expression()) as Expression,
        };
        switch (context.@operator.Type)
        {
            case YALGrammerLexer.INCREMENT:
                compoundExpression.Operator = Operators.AssignmentOperator.PostIncrement;
                break;
            case YALGrammerLexer.DECREMENT:
                compoundExpression.Operator = Operators.AssignmentOperator.PostDecrement;
                break;
        }
        compoundExpression.LineNumber = context.Start.Line;
        return compoundExpression;
    }
    
    public override object VisitPrefixUnary(YALGrammerParser.PrefixUnaryContext context)
    {
        var compoundExpression = new UnaryAssignment()
        {
            Target = Visit(context.expression()) as Expression,
        };
        switch (context.@operator.Type)
        {
            case YALGrammerLexer.INCREMENT:
                compoundExpression.Operator = Operators.AssignmentOperator.PreIncrement;
                break;
            case YALGrammerLexer.DECREMENT:
                compoundExpression.Operator = Operators.AssignmentOperator.PreDecrement;
                break;
            case YALGrammerLexer.BITWISE_NOT:
                compoundExpression.Operator = Operators.AssignmentOperator.BitwiseNot;
                break;
        }
        compoundExpression.LineNumber = context.Start.Line;
        return compoundExpression;
    }

    #endregion

    public override object VisitFunctionCall(YALGrammerParser.FunctionCallContext context)
    {
        var functionCall = new FunctionCall(context.ID().GetText(), context.AWAIT() != null);
        functionCall.InputParameters = Visit(context.expression()) as List<Expression>;
        foreach (var inputParameter in functionCall.InputParameters)
        {
            inputParameter.Parent = functionCall;
        }

        functionCall.LineNumber = context.Start.Line;
        return functionCall;
    }

    #region AssignmentVisitors

    public override object VisitIdAssignment(YALGrammerParser.IdAssignmentContext context)
    {
        var assignment = new BinaryAssignment();
        
        List<Identifier> targets = Visit(context.identifier()) as List<Identifier>;
        
        foreach (var target in targets)
        {
            target.Parent = assignment;
            assignment.Targets.Add(target);
        }
        
        List<Expression> values = Visit(context.identifier()) as List<Expression>;

        foreach (var value in values)
        {
            value.Parent = assignment;
            assignment.Values.Add(value);
        }

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
        assignment.LineNumber = context.Start.Line;
        return assignment;
    }

    public override object VisitIdPostIncrementDecrementAssignment(YALGrammerParser.IdPostIncrementDecrementAssignmentContext context)
    {
        var target = Visit(context.identifier());
        var assignment = new UnaryAssignment();
        if (target is Identifier identifier)
        {
            assignment.Target = identifier;
        }
        else
        {
            _errorHandler.AddError(new InvalidAssignmentException(context.GetText()), context);
        }
        switch (context.@operator.Type)
        {
            case YALGrammerLexer.INCREMENT:
                assignment.Operator = Operators.AssignmentOperator.PostIncrement;
                break;
            case YALGrammerLexer.DECREMENT:
                assignment.Operator = Operators.AssignmentOperator.PostDecrement;
                break;
        }
        assignment.LineNumber = context.Start.Line;
        return assignment;
    }
    
    public override object VisitIdPreIncrementDecrementAssignment(YALGrammerParser.IdPreIncrementDecrementAssignmentContext context)
    {
        var target = Visit(context.identifier());
        var assignment = new UnaryAssignment();
        if (target is Identifier identifier)
        {
            assignment.Target = identifier;
        }
        else
        {
            _errorHandler.AddError(new InvalidAssignmentException(context.GetText()), context);
        }
        switch (context.@operator.Type)
        {
            case YALGrammerLexer.INCREMENT:
                assignment.Operator = Operators.AssignmentOperator.PreIncrement;
                break;
            case YALGrammerLexer.DECREMENT:
                assignment.Operator = Operators.AssignmentOperator.PreDecrement;
                break;
        }
        assignment.LineNumber = context.Start.Line;
        return assignment;
    }

    public override object VisitExpressionList(YALGrammerParser.ExpressionListContext context)
    {
        var expressionList = new List<Expression>();
        foreach (var expression in context.expression())
        {
            var expr = Visit(expression) as Expression;
            if (expr is null) continue;
            expressionList.Add(expr);
        }

        return expressionList;
    }

    #endregion

    public override object VisitDeclarationAssignment(YALGrammerParser.DeclarationAssignmentContext context)
    {
        var assignment = new BinaryAssignment()
        {
            Operator = Operators.AssignmentOperator.Equals,
        };

        var targets = Visit(context.variableDeclaration()) as List<VariableDeclaration>;
        if (targets is not null)
        {
            foreach (var target in targets)
            {
                target.Parent = assignment;
                assignment.Targets.Add(target);                
            }
        }
        
        var values = Visit(context.expression()) as List<Expression>;
        if (values is not null)
        {
            foreach (var value in values)
            {
                value.Parent = assignment;
                assignment.Values.Add(value);                
            }
        }

        assignment.LineNumber = context.Start.Line;
        return assignment;
    }

    public override object VisitSimpleIdentifier(YALGrammerParser.SimpleIdentifierContext context)
    {
        return new Identifier(context.ID().GetText()) {LineNumber = context.Start.Line};
    }
    
    public override object VisitParenthesizedIdentifier(YALGrammerParser.ParenthesizedIdentifierContext context)
    {
        return Visit(context.identifier());
    }

    public override object VisitArrayElementIdentifier(YALGrammerParser.ArrayElementIdentifierContext context)
    {
        return new ArrayElementIdentifier(context.ID().GetText(), Visit(context.expression()) as Expression)  {LineNumber = context.Start.Line};
    }

    public override object VisitIdentifierList(YALGrammerParser.IdentifierListContext context)
    {
        var identifierList = new List<Identifier>();
        foreach (var identifier in context.identifier())
        {
            var id = Visit(identifier) as Identifier;
            if (id is null) continue;
            identifierList.Add(id);
        }

        return identifierList;
    }


    public override object VisitArrayLiteral(YALGrammerParser.ArrayLiteralContext context)
    {
        ArrayLiteral arrayLiteral = new();
        foreach (var expression in context.expression())
        {
            if (Visit(expression) is Expression expr)
                arrayLiteral.Values.Add(expr);
        }

        arrayLiteral.LineNumber = context.Start.Line;
        return arrayLiteral;
    }
} 