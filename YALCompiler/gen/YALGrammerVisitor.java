// Generated from java-escape by ANTLR 4.11.1
import org.antlr.v4.runtime.tree.ParseTreeVisitor;

/**
 * This interface defines a complete generic visitor for a parse tree produced
 * by {@link YALGrammerParser}.
 *
 * @param <T> The return type of the visit operation. Use {@link Void} for
 * operations with no return type.
 */
public interface YALGrammerVisitor<T> extends ParseTreeVisitor<T> {
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#program}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitProgram(YALGrammerParser.ProgramContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#globalVariableDeclaration}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitGlobalVariableDeclaration(YALGrammerParser.GlobalVariableDeclarationContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#functionDeclaration}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFunctionDeclaration(YALGrammerParser.FunctionDeclarationContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#formalInputParams}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFormalInputParams(YALGrammerParser.FormalInputParamsContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#formalOutputParams}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFormalOutputParams(YALGrammerParser.FormalOutputParamsContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#statementBlock}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitStatementBlock(YALGrammerParser.StatementBlockContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#blockStatement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitBlockStatement(YALGrammerParser.BlockStatementContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#singleStatement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitSingleStatement(YALGrammerParser.SingleStatementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code SimpleVariableDeclarationFormat}
	 * labeled alternative in {@link YALGrammerParser#variableDeclaration}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitSimpleVariableDeclarationFormat(YALGrammerParser.SimpleVariableDeclarationFormatContext ctx);
	/**
	 * Visit a parse tree produced by the {@code TupleVariableDeclaration}
	 * labeled alternative in {@link YALGrammerParser#variableDeclaration}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitTupleVariableDeclaration(YALGrammerParser.TupleVariableDeclarationContext ctx);
	/**
	 * Visit a parse tree produced by the {@code ArrayDeclaration}
	 * labeled alternative in {@link YALGrammerParser#variableDeclarationFormat}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitArrayDeclaration(YALGrammerParser.ArrayDeclarationContext ctx);
	/**
	 * Visit a parse tree produced by the {@code SimpleVariableDeclaration}
	 * labeled alternative in {@link YALGrammerParser#variableDeclarationFormat}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitSimpleVariableDeclaration(YALGrammerParser.SimpleVariableDeclarationContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#enumDeclaration}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitEnumDeclaration(YALGrammerParser.EnumDeclarationContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#assignment}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAssignment(YALGrammerParser.AssignmentContext ctx);
	/**
	 * Visit a parse tree produced by the {@code IdAssignment}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIdAssignment(YALGrammerParser.IdAssignmentContext ctx);
	/**
	 * Visit a parse tree produced by the {@code IdAdditionAssignment}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIdAdditionAssignment(YALGrammerParser.IdAdditionAssignmentContext ctx);
	/**
	 * Visit a parse tree produced by the {@code IdSubtractionAssignment}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIdSubtractionAssignment(YALGrammerParser.IdSubtractionAssignmentContext ctx);
	/**
	 * Visit a parse tree produced by the {@code IdPostIncrement}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIdPostIncrement(YALGrammerParser.IdPostIncrementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code IdPostDecrement}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIdPostDecrement(YALGrammerParser.IdPostDecrementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code IdPreDecrement}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIdPreDecrement(YALGrammerParser.IdPreDecrementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code IdPreIncrement}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIdPreIncrement(YALGrammerParser.IdPreIncrementContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#declarationAssignment}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitDeclarationAssignment(YALGrammerParser.DeclarationAssignmentContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#tupleAssignment}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitTupleAssignment(YALGrammerParser.TupleAssignmentContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#tupleDeclaration}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitTupleDeclaration(YALGrammerParser.TupleDeclarationContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#tupleId}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitTupleId(YALGrammerParser.TupleIdContext ctx);
	/**
	 * Visit a parse tree produced by the {@code ParenthesizedExpression}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitParenthesizedExpression(YALGrammerParser.ParenthesizedExpressionContext ctx);
	/**
	 * Visit a parse tree produced by the {@code PreIncrement}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitPreIncrement(YALGrammerParser.PreIncrementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Multiplication}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitMultiplication(YALGrammerParser.MultiplicationContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Addition}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAddition(YALGrammerParser.AdditionContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Variable}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitVariable(YALGrammerParser.VariableContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Modulo}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitModulo(YALGrammerParser.ModuloContext ctx);
	/**
	 * Visit a parse tree produced by the {@code VariableAssignment}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitVariableAssignment(YALGrammerParser.VariableAssignmentContext ctx);
	/**
	 * Visit a parse tree produced by the {@code PostDecrement}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitPostDecrement(YALGrammerParser.PostDecrementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code RightShift}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitRightShift(YALGrammerParser.RightShiftContext ctx);
	/**
	 * Visit a parse tree produced by the {@code LeftShift}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLeftShift(YALGrammerParser.LeftShiftContext ctx);
	/**
	 * Visit a parse tree produced by the {@code ArrayLiteral}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitArrayLiteral(YALGrammerParser.ArrayLiteralContext ctx);
	/**
	 * Visit a parse tree produced by the {@code FunctionCallExpression}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFunctionCallExpression(YALGrammerParser.FunctionCallExpressionContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Subtraction}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitSubtraction(YALGrammerParser.SubtractionContext ctx);
	/**
	 * Visit a parse tree produced by the {@code StringLiteral}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitStringLiteral(YALGrammerParser.StringLiteralContext ctx);
	/**
	 * Visit a parse tree produced by the {@code BitwiseXor}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitBitwiseXor(YALGrammerParser.BitwiseXorContext ctx);
	/**
	 * Visit a parse tree produced by the {@code BitwiseOr}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitBitwiseOr(YALGrammerParser.BitwiseOrContext ctx);
	/**
	 * Visit a parse tree produced by the {@code BitwiseAnd}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitBitwiseAnd(YALGrammerParser.BitwiseAndContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Division}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitDivision(YALGrammerParser.DivisionContext ctx);
	/**
	 * Visit a parse tree produced by the {@code PostIncrement}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitPostIncrement(YALGrammerParser.PostIncrementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code PreDecrement}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitPreDecrement(YALGrammerParser.PreDecrementContext ctx);
	/**
	 * Visit a parse tree produced by the {@code NumberLiteral}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitNumberLiteral(YALGrammerParser.NumberLiteralContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#functionCall}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitFunctionCall(YALGrammerParser.FunctionCallContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#actualInputParams}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitActualInputParams(YALGrammerParser.ActualInputParamsContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Not}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitNot(YALGrammerParser.NotContext ctx);
	/**
	 * Visit a parse tree produced by the {@code LessThan}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLessThan(YALGrammerParser.LessThanContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Equals}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitEquals(YALGrammerParser.EqualsContext ctx);
	/**
	 * Visit a parse tree produced by the {@code Or}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitOr(YALGrammerParser.OrContext ctx);
	/**
	 * Visit a parse tree produced by the {@code ParenthesizedPredicate}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitParenthesizedPredicate(YALGrammerParser.ParenthesizedPredicateContext ctx);
	/**
	 * Visit a parse tree produced by the {@code LessThanOrEqual}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLessThanOrEqual(YALGrammerParser.LessThanOrEqualContext ctx);
	/**
	 * Visit a parse tree produced by the {@code GreaterThan}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitGreaterThan(YALGrammerParser.GreaterThanContext ctx);
	/**
	 * Visit a parse tree produced by the {@code BooleanLiteral}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitBooleanLiteral(YALGrammerParser.BooleanLiteralContext ctx);
	/**
	 * Visit a parse tree produced by the {@code And}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAnd(YALGrammerParser.AndContext ctx);
	/**
	 * Visit a parse tree produced by the {@code GreaterThanOrEqual}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitGreaterThanOrEqual(YALGrammerParser.GreaterThanOrEqualContext ctx);
	/**
	 * Visit a parse tree produced by the {@code NotEquals}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitNotEquals(YALGrammerParser.NotEqualsContext ctx);
	/**
	 * Visit a parse tree produced by the {@code ExpressionPredicate}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitExpressionPredicate(YALGrammerParser.ExpressionPredicateContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#ifStatement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIfStatement(YALGrammerParser.IfStatementContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#elseIfStatement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitElseIfStatement(YALGrammerParser.ElseIfStatementContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#elseStatement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitElseStatement(YALGrammerParser.ElseStatementContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#whileStatement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitWhileStatement(YALGrammerParser.WhileStatementContext ctx);
	/**
	 * Visit a parse tree produced by {@link YALGrammerParser#forStatement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitForStatement(YALGrammerParser.ForStatementContext ctx);
}