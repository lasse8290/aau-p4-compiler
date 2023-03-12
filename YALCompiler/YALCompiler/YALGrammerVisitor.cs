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
                program.Children.Add(f);
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
        
        if (context.ARRAY_DEFINER() != null)
        {
            var text = context.ARRAY_DEFINER().GetText();
            bool arr = int.TryParse(text.Substring(1, text.Length - 2), out int arraySize);
            if (arr)
            {
                symbol.ArraySize = arraySize;
            }
            else
            {
                //unable to parse array size
            }
        }
        
        return symbol;
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
                    func.SymbolTable.Add(symbol);
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
                func.SymbolTable.Add(symbol);
            }
            catch (VariableAlreadyExistsException e)
            {
                _errorHandler.AddError(e, context);                    
            }
        }
        
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
        
        try
        {
            symbol.ArraySize = GetArraySizeFromDefiner(context.ARRAY_DEFINER().GetText());
        }
        catch (ArraySizeNotRecognizedException e)
        {
            _errorHandler.AddError(e, context);
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
        if (context.ifStatement() != null)
            return Visit(context.ifStatement());
            
        if (context.whileStatement() != null)
            return Visit(context.whileStatement());

        if (context.forStatement() != null)
            return Visit(context.forStatement());

        return null;
    }

    public override object VisitSingleStatement(YALGrammerParser.SingleStatementContext context)
    {
        if (context.variableDeclaration() != null)
            return Visit(context.variableDeclaration());
        
        if (context.assignment() != null)
            return Visit(context.assignment());

        if (context.functionCall() != null)
            return Visit(context.functionCall());

        if (context.RETURN() != null)
            return new ReturnStatement();

        return null;
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

        ifStatement.Children.Add(ifPath);
        
        if (context.elseIfStatement() != null)
        {
            foreach (var elseIf in context.elseIfStatement())
            {
                var elseIfPath = new ElseIf();

                if (Visit(elseIf.predicate()) is Predicate elseIfPredicate)
                    elseIfPath.Predicate = elseIfPredicate;

                StatementBlock elseIfStatementBlock = Visit(context.statementBlock()) as StatementBlock;
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

                ifStatement.Children.Add(elseIfPath);
            }
        }
        
        if (context.elseStatement() != null)
        {
            var elsePath = new Else();

            StatementBlock elseStatementBlock = Visit(context.statementBlock()) as StatementBlock;
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
        return Visit(context.identifier());
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
    
    public override object VisitMultiplication(YALGrammerParser.MultiplicationContext context)
    {
        var compoundExpression = new CompoundExpression
        {
            Operator = Operators.ExpressionOperator.Multiplication,
            Left = Visit(context.expression(0)) as Expression,
            Right = Visit(context.expression(1)) as Expression
        };
        return compoundExpression;
    }
    
    public override object VisitDivision(YALGrammerParser.DivisionContext context)
    {
        var compoundExpression = new CompoundExpression
        {
            Operator = Operators.ExpressionOperator.Division,
            Left = Visit(context.expression(0)) as Expression,
            Right = Visit(context.expression(1)) as Expression
        };
        return compoundExpression;
    }
    
    public override object VisitModulo(YALGrammerParser.ModuloContext context)
    {
        var compoundExpression = new CompoundExpression
        {
            Operator = Operators.ExpressionOperator.Modulo,
            Left = Visit(context.expression(0)) as Expression,
            Right = Visit(context.expression(1)) as Expression
        };
        return compoundExpression;
    }
    
    public override object VisitAddition(YALGrammerParser.AdditionContext context)
    {
        var compoundExpression = new CompoundExpression
        {
            Operator = Operators.ExpressionOperator.Addition,
            Left = Visit(context.expression(0)) as Expression,
            Right = Visit(context.expression(1)) as Expression
        };
        return compoundExpression;
    }
    
    public override object VisitSubtraction(YALGrammerParser.SubtractionContext context)
    {
        var compoundExpression = new CompoundExpression
        {
            Operator = Operators.ExpressionOperator.Subtraction,
            Left = Visit(context.expression(0)) as Expression,
            Right = Visit(context.expression(1)) as Expression
        };
        return compoundExpression;
    }
    
    public override object VisitLeftShift(YALGrammerParser.LeftShiftContext context)
    {
        var compoundExpression = new CompoundExpression
        {
            Operator = Operators.ExpressionOperator.LeftShift,
            Left = Visit(context.expression(0)) as Expression,
            Right = Visit(context.expression(1)) as Expression
        };
        return compoundExpression;
    }
    
    public override object VisitRightShift(YALGrammerParser.RightShiftContext context)
    {
        var compoundExpression = new CompoundExpression
        {
            Operator = Operators.ExpressionOperator.RightShift,
            Left = Visit(context.expression(0)) as Expression,
            Right = Visit(context.expression(1)) as Expression
        };
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
    
    public override object VisitBitwiseUnaryNot(YALGrammerParser.BitwiseUnaryNotContext context)
    {
        var compoundExpression = new UnaryCompoundExpression()
        {
            Operator = Operators.ExpressionOperator.BitwiseNot,
            Expression = Visit(context.expression()) as Expression,
        };
        return compoundExpression;
    }
    
    public override object VisitPostIncrement(YALGrammerParser.PostIncrementContext context)
    {
        var compoundExpression = new UnaryCompoundExpression()
        {
            Operator = Operators.ExpressionOperator.InlineIncrement,
            Expression = Visit(context.expression()) as Expression,
        };
        return compoundExpression;
    }
    
    public override object VisitPostDecrement(YALGrammerParser.PostDecrementContext context)
    {
        var compoundExpression = new UnaryCompoundExpression()
        {
            Operator = Operators.ExpressionOperator.InlineDecrement,
            Expression = Visit(context.expression()) as Expression,
        };
        return compoundExpression;
    }
    
    public override object VisitPreIncrement(YALGrammerParser.PreIncrementContext context)
    {
        var compoundExpression = new UnaryCompoundExpression()
        {
            Operator = Operators.ExpressionOperator.InlineIncrement,
            Expression = Visit(context.expression()) as Expression,
        };
        return compoundExpression;
    }
    
    public override object VisitPreDecrement(YALGrammerParser.PreDecrementContext context)
    {
        var compoundExpression = new UnaryCompoundExpression()
        {
            Operator = Operators.ExpressionOperator.InlineDecrement,
            Expression = Visit(context.expression()) as Expression,
        };
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
            Operator = Operators.AssignmentOperator.Equals,
            Value = Visit(context.predicate()) as Expression
        };
        return assignment;
    }
    
    public override object VisitIdAdditionAssignment(YALGrammerParser.IdAdditionAssignmentContext context)
    {
        var assignment = new BinaryAssignment()
        {
            Target = Visit(context.identifier()) as Identifier,
            Operator = Operators.AssignmentOperator.AdditionAssignment,
            Value = Visit(context.expression()) as Expression
        };
        return assignment;
    }
    
    public override object VisitIdSubtractionAssignment(YALGrammerParser.IdSubtractionAssignmentContext context)
    {
        var assignment = new BinaryAssignment()
        {
            Target = Visit(context.identifier()) as Identifier,
            Operator = Operators.AssignmentOperator.SubtractionAssignment,
            Value = Visit(context.expression()) as Expression
        };
        return assignment;
    }
    
    public override object VisitIdMultiplicationAssignment(YALGrammerParser.IdMultiplicationAssignmentContext context)
    {
        var assignment = new BinaryAssignment()
        {
            Target = Visit(context.identifier()) as Identifier,
            Operator = Operators.AssignmentOperator.MultiplicationAssignment,
            Value = Visit(context.expression()) as Expression
        };
        return assignment;
    }
    
    public override object VisitIdDivisionAssignment(YALGrammerParser.IdDivisionAssignmentContext context)
    {
        var assignment = new BinaryAssignment()
        {
            Target = Visit(context.identifier()) as Identifier,
            Operator = Operators.AssignmentOperator.DivisionAssignment,
            Value = Visit(context.expression()) as Expression
        };
        return assignment;
    }
    
    public override object VisitIdModuloAssignment(YALGrammerParser.IdModuloAssignmentContext context)
    {
        var assignment = new BinaryAssignment()
        {
            Target = Visit(context.identifier()) as Identifier,
            Operator = Operators.AssignmentOperator.ModuloAssignment,
            Value = Visit(context.expression()) as Expression
        };
        return assignment;
    }
    
    public override object VisitIdPostIncrementAssignment(YALGrammerParser.IdPostIncrementAssignmentContext context)
    {
        var assignment = new UnaryAssignment()
        {
            Target = Visit(context.identifier()) as Identifier,
            Operator = Operators.AssignmentOperator.PostIncrement,
        };
        return assignment;
    }
    
    public override object VisitIdPostDecrementAssignment(YALGrammerParser.IdPostDecrementAssignmentContext context)
    {
        var assignment = new UnaryAssignment()
        {
            Target = Visit(context.identifier()) as Identifier,
            Operator = Operators.AssignmentOperator.PostDecrement,
        };
        return assignment;
    }
    
    public override object VisitIdPreIncrementAssignment(YALGrammerParser.IdPreIncrementAssignmentContext context)
    {
        var assignment = new UnaryAssignment()
        {
            Target = Visit(context.identifier()) as Identifier,
            Operator = Operators.AssignmentOperator.PreIncrement,
        };
        return assignment;
    }
    
    public override object VisitIdPreDecrementAssignment(YALGrammerParser.IdPreDecrementAssignmentContext context)
    {
        var assignment = new UnaryAssignment()
        {
            Target = Visit(context.identifier()) as Identifier,
            Operator = Operators.AssignmentOperator.PreDecrement,
        };
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