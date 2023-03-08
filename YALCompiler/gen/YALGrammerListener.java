// Generated from java-escape by ANTLR 4.11.1
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link YALGrammerParser}.
 */
public interface YALGrammerListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#program}.
	 * @param ctx the parse tree
	 */
	void enterProgram(YALGrammerParser.ProgramContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#program}.
	 * @param ctx the parse tree
	 */
	void exitProgram(YALGrammerParser.ProgramContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#globalVariableDeclaration}.
	 * @param ctx the parse tree
	 */
	void enterGlobalVariableDeclaration(YALGrammerParser.GlobalVariableDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#globalVariableDeclaration}.
	 * @param ctx the parse tree
	 */
	void exitGlobalVariableDeclaration(YALGrammerParser.GlobalVariableDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#functionDeclaration}.
	 * @param ctx the parse tree
	 */
	void enterFunctionDeclaration(YALGrammerParser.FunctionDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#functionDeclaration}.
	 * @param ctx the parse tree
	 */
	void exitFunctionDeclaration(YALGrammerParser.FunctionDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#formalInputParams}.
	 * @param ctx the parse tree
	 */
	void enterFormalInputParams(YALGrammerParser.FormalInputParamsContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#formalInputParams}.
	 * @param ctx the parse tree
	 */
	void exitFormalInputParams(YALGrammerParser.FormalInputParamsContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#formalOutputParams}.
	 * @param ctx the parse tree
	 */
	void enterFormalOutputParams(YALGrammerParser.FormalOutputParamsContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#formalOutputParams}.
	 * @param ctx the parse tree
	 */
	void exitFormalOutputParams(YALGrammerParser.FormalOutputParamsContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#statementBlock}.
	 * @param ctx the parse tree
	 */
	void enterStatementBlock(YALGrammerParser.StatementBlockContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#statementBlock}.
	 * @param ctx the parse tree
	 */
	void exitStatementBlock(YALGrammerParser.StatementBlockContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#blockStatement}.
	 * @param ctx the parse tree
	 */
	void enterBlockStatement(YALGrammerParser.BlockStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#blockStatement}.
	 * @param ctx the parse tree
	 */
	void exitBlockStatement(YALGrammerParser.BlockStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#singleStatement}.
	 * @param ctx the parse tree
	 */
	void enterSingleStatement(YALGrammerParser.SingleStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#singleStatement}.
	 * @param ctx the parse tree
	 */
	void exitSingleStatement(YALGrammerParser.SingleStatementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code SimpleVariableDeclarationFormat}
	 * labeled alternative in {@link YALGrammerParser#variableDeclaration}.
	 * @param ctx the parse tree
	 */
	void enterSimpleVariableDeclarationFormat(YALGrammerParser.SimpleVariableDeclarationFormatContext ctx);
	/**
	 * Exit a parse tree produced by the {@code SimpleVariableDeclarationFormat}
	 * labeled alternative in {@link YALGrammerParser#variableDeclaration}.
	 * @param ctx the parse tree
	 */
	void exitSimpleVariableDeclarationFormat(YALGrammerParser.SimpleVariableDeclarationFormatContext ctx);
	/**
	 * Enter a parse tree produced by the {@code TupleVariableDeclaration}
	 * labeled alternative in {@link YALGrammerParser#variableDeclaration}.
	 * @param ctx the parse tree
	 */
	void enterTupleVariableDeclaration(YALGrammerParser.TupleVariableDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by the {@code TupleVariableDeclaration}
	 * labeled alternative in {@link YALGrammerParser#variableDeclaration}.
	 * @param ctx the parse tree
	 */
	void exitTupleVariableDeclaration(YALGrammerParser.TupleVariableDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ArrayDeclaration}
	 * labeled alternative in {@link YALGrammerParser#variableDeclarationFormat}.
	 * @param ctx the parse tree
	 */
	void enterArrayDeclaration(YALGrammerParser.ArrayDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ArrayDeclaration}
	 * labeled alternative in {@link YALGrammerParser#variableDeclarationFormat}.
	 * @param ctx the parse tree
	 */
	void exitArrayDeclaration(YALGrammerParser.ArrayDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by the {@code SimpleVariableDeclaration}
	 * labeled alternative in {@link YALGrammerParser#variableDeclarationFormat}.
	 * @param ctx the parse tree
	 */
	void enterSimpleVariableDeclaration(YALGrammerParser.SimpleVariableDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by the {@code SimpleVariableDeclaration}
	 * labeled alternative in {@link YALGrammerParser#variableDeclarationFormat}.
	 * @param ctx the parse tree
	 */
	void exitSimpleVariableDeclaration(YALGrammerParser.SimpleVariableDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#enumDeclaration}.
	 * @param ctx the parse tree
	 */
	void enterEnumDeclaration(YALGrammerParser.EnumDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#enumDeclaration}.
	 * @param ctx the parse tree
	 */
	void exitEnumDeclaration(YALGrammerParser.EnumDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#assignment}.
	 * @param ctx the parse tree
	 */
	void enterAssignment(YALGrammerParser.AssignmentContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#assignment}.
	 * @param ctx the parse tree
	 */
	void exitAssignment(YALGrammerParser.AssignmentContext ctx);
	/**
	 * Enter a parse tree produced by the {@code IdAssignment}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 */
	void enterIdAssignment(YALGrammerParser.IdAssignmentContext ctx);
	/**
	 * Exit a parse tree produced by the {@code IdAssignment}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 */
	void exitIdAssignment(YALGrammerParser.IdAssignmentContext ctx);
	/**
	 * Enter a parse tree produced by the {@code IdAdditionAssignment}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 */
	void enterIdAdditionAssignment(YALGrammerParser.IdAdditionAssignmentContext ctx);
	/**
	 * Exit a parse tree produced by the {@code IdAdditionAssignment}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 */
	void exitIdAdditionAssignment(YALGrammerParser.IdAdditionAssignmentContext ctx);
	/**
	 * Enter a parse tree produced by the {@code IdSubtractionAssignment}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 */
	void enterIdSubtractionAssignment(YALGrammerParser.IdSubtractionAssignmentContext ctx);
	/**
	 * Exit a parse tree produced by the {@code IdSubtractionAssignment}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 */
	void exitIdSubtractionAssignment(YALGrammerParser.IdSubtractionAssignmentContext ctx);
	/**
	 * Enter a parse tree produced by the {@code IdPostIncrement}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 */
	void enterIdPostIncrement(YALGrammerParser.IdPostIncrementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code IdPostIncrement}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 */
	void exitIdPostIncrement(YALGrammerParser.IdPostIncrementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code IdPostDecrement}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 */
	void enterIdPostDecrement(YALGrammerParser.IdPostDecrementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code IdPostDecrement}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 */
	void exitIdPostDecrement(YALGrammerParser.IdPostDecrementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code IdPreDecrement}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 */
	void enterIdPreDecrement(YALGrammerParser.IdPreDecrementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code IdPreDecrement}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 */
	void exitIdPreDecrement(YALGrammerParser.IdPreDecrementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code IdPreIncrement}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 */
	void enterIdPreIncrement(YALGrammerParser.IdPreIncrementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code IdPreIncrement}
	 * labeled alternative in {@link YALGrammerParser#simpleAssignment}.
	 * @param ctx the parse tree
	 */
	void exitIdPreIncrement(YALGrammerParser.IdPreIncrementContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#declarationAssignment}.
	 * @param ctx the parse tree
	 */
	void enterDeclarationAssignment(YALGrammerParser.DeclarationAssignmentContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#declarationAssignment}.
	 * @param ctx the parse tree
	 */
	void exitDeclarationAssignment(YALGrammerParser.DeclarationAssignmentContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#tupleAssignment}.
	 * @param ctx the parse tree
	 */
	void enterTupleAssignment(YALGrammerParser.TupleAssignmentContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#tupleAssignment}.
	 * @param ctx the parse tree
	 */
	void exitTupleAssignment(YALGrammerParser.TupleAssignmentContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#tupleDeclaration}.
	 * @param ctx the parse tree
	 */
	void enterTupleDeclaration(YALGrammerParser.TupleDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#tupleDeclaration}.
	 * @param ctx the parse tree
	 */
	void exitTupleDeclaration(YALGrammerParser.TupleDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#tupleId}.
	 * @param ctx the parse tree
	 */
	void enterTupleId(YALGrammerParser.TupleIdContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#tupleId}.
	 * @param ctx the parse tree
	 */
	void exitTupleId(YALGrammerParser.TupleIdContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ParenthesizedExpression}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterParenthesizedExpression(YALGrammerParser.ParenthesizedExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ParenthesizedExpression}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitParenthesizedExpression(YALGrammerParser.ParenthesizedExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code PreIncrement}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterPreIncrement(YALGrammerParser.PreIncrementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code PreIncrement}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitPreIncrement(YALGrammerParser.PreIncrementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Multiplication}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterMultiplication(YALGrammerParser.MultiplicationContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Multiplication}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitMultiplication(YALGrammerParser.MultiplicationContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Addition}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterAddition(YALGrammerParser.AdditionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Addition}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitAddition(YALGrammerParser.AdditionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Variable}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterVariable(YALGrammerParser.VariableContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Variable}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitVariable(YALGrammerParser.VariableContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Modulo}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterModulo(YALGrammerParser.ModuloContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Modulo}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitModulo(YALGrammerParser.ModuloContext ctx);
	/**
	 * Enter a parse tree produced by the {@code VariableAssignment}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterVariableAssignment(YALGrammerParser.VariableAssignmentContext ctx);
	/**
	 * Exit a parse tree produced by the {@code VariableAssignment}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitVariableAssignment(YALGrammerParser.VariableAssignmentContext ctx);
	/**
	 * Enter a parse tree produced by the {@code PostDecrement}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterPostDecrement(YALGrammerParser.PostDecrementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code PostDecrement}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitPostDecrement(YALGrammerParser.PostDecrementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code RightShift}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterRightShift(YALGrammerParser.RightShiftContext ctx);
	/**
	 * Exit a parse tree produced by the {@code RightShift}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitRightShift(YALGrammerParser.RightShiftContext ctx);
	/**
	 * Enter a parse tree produced by the {@code LeftShift}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterLeftShift(YALGrammerParser.LeftShiftContext ctx);
	/**
	 * Exit a parse tree produced by the {@code LeftShift}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitLeftShift(YALGrammerParser.LeftShiftContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ArrayLiteral}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterArrayLiteral(YALGrammerParser.ArrayLiteralContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ArrayLiteral}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitArrayLiteral(YALGrammerParser.ArrayLiteralContext ctx);
	/**
	 * Enter a parse tree produced by the {@code FunctionCallExpression}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterFunctionCallExpression(YALGrammerParser.FunctionCallExpressionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code FunctionCallExpression}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitFunctionCallExpression(YALGrammerParser.FunctionCallExpressionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Subtraction}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterSubtraction(YALGrammerParser.SubtractionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Subtraction}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitSubtraction(YALGrammerParser.SubtractionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code StringLiteral}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterStringLiteral(YALGrammerParser.StringLiteralContext ctx);
	/**
	 * Exit a parse tree produced by the {@code StringLiteral}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitStringLiteral(YALGrammerParser.StringLiteralContext ctx);
	/**
	 * Enter a parse tree produced by the {@code BitwiseXor}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterBitwiseXor(YALGrammerParser.BitwiseXorContext ctx);
	/**
	 * Exit a parse tree produced by the {@code BitwiseXor}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitBitwiseXor(YALGrammerParser.BitwiseXorContext ctx);
	/**
	 * Enter a parse tree produced by the {@code BitwiseOr}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterBitwiseOr(YALGrammerParser.BitwiseOrContext ctx);
	/**
	 * Exit a parse tree produced by the {@code BitwiseOr}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitBitwiseOr(YALGrammerParser.BitwiseOrContext ctx);
	/**
	 * Enter a parse tree produced by the {@code BitwiseAnd}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterBitwiseAnd(YALGrammerParser.BitwiseAndContext ctx);
	/**
	 * Exit a parse tree produced by the {@code BitwiseAnd}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitBitwiseAnd(YALGrammerParser.BitwiseAndContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Division}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterDivision(YALGrammerParser.DivisionContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Division}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitDivision(YALGrammerParser.DivisionContext ctx);
	/**
	 * Enter a parse tree produced by the {@code PostIncrement}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterPostIncrement(YALGrammerParser.PostIncrementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code PostIncrement}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitPostIncrement(YALGrammerParser.PostIncrementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code PreDecrement}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterPreDecrement(YALGrammerParser.PreDecrementContext ctx);
	/**
	 * Exit a parse tree produced by the {@code PreDecrement}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitPreDecrement(YALGrammerParser.PreDecrementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code NumberLiteral}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterNumberLiteral(YALGrammerParser.NumberLiteralContext ctx);
	/**
	 * Exit a parse tree produced by the {@code NumberLiteral}
	 * labeled alternative in {@link YALGrammerParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitNumberLiteral(YALGrammerParser.NumberLiteralContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#functionCall}.
	 * @param ctx the parse tree
	 */
	void enterFunctionCall(YALGrammerParser.FunctionCallContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#functionCall}.
	 * @param ctx the parse tree
	 */
	void exitFunctionCall(YALGrammerParser.FunctionCallContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#actualInputParams}.
	 * @param ctx the parse tree
	 */
	void enterActualInputParams(YALGrammerParser.ActualInputParamsContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#actualInputParams}.
	 * @param ctx the parse tree
	 */
	void exitActualInputParams(YALGrammerParser.ActualInputParamsContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Not}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void enterNot(YALGrammerParser.NotContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Not}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void exitNot(YALGrammerParser.NotContext ctx);
	/**
	 * Enter a parse tree produced by the {@code LessThan}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void enterLessThan(YALGrammerParser.LessThanContext ctx);
	/**
	 * Exit a parse tree produced by the {@code LessThan}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void exitLessThan(YALGrammerParser.LessThanContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Equals}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void enterEquals(YALGrammerParser.EqualsContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Equals}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void exitEquals(YALGrammerParser.EqualsContext ctx);
	/**
	 * Enter a parse tree produced by the {@code Or}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void enterOr(YALGrammerParser.OrContext ctx);
	/**
	 * Exit a parse tree produced by the {@code Or}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void exitOr(YALGrammerParser.OrContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ParenthesizedPredicate}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void enterParenthesizedPredicate(YALGrammerParser.ParenthesizedPredicateContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ParenthesizedPredicate}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void exitParenthesizedPredicate(YALGrammerParser.ParenthesizedPredicateContext ctx);
	/**
	 * Enter a parse tree produced by the {@code LessThanOrEqual}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void enterLessThanOrEqual(YALGrammerParser.LessThanOrEqualContext ctx);
	/**
	 * Exit a parse tree produced by the {@code LessThanOrEqual}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void exitLessThanOrEqual(YALGrammerParser.LessThanOrEqualContext ctx);
	/**
	 * Enter a parse tree produced by the {@code GreaterThan}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void enterGreaterThan(YALGrammerParser.GreaterThanContext ctx);
	/**
	 * Exit a parse tree produced by the {@code GreaterThan}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void exitGreaterThan(YALGrammerParser.GreaterThanContext ctx);
	/**
	 * Enter a parse tree produced by the {@code BooleanLiteral}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void enterBooleanLiteral(YALGrammerParser.BooleanLiteralContext ctx);
	/**
	 * Exit a parse tree produced by the {@code BooleanLiteral}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void exitBooleanLiteral(YALGrammerParser.BooleanLiteralContext ctx);
	/**
	 * Enter a parse tree produced by the {@code And}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void enterAnd(YALGrammerParser.AndContext ctx);
	/**
	 * Exit a parse tree produced by the {@code And}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void exitAnd(YALGrammerParser.AndContext ctx);
	/**
	 * Enter a parse tree produced by the {@code GreaterThanOrEqual}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void enterGreaterThanOrEqual(YALGrammerParser.GreaterThanOrEqualContext ctx);
	/**
	 * Exit a parse tree produced by the {@code GreaterThanOrEqual}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void exitGreaterThanOrEqual(YALGrammerParser.GreaterThanOrEqualContext ctx);
	/**
	 * Enter a parse tree produced by the {@code NotEquals}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void enterNotEquals(YALGrammerParser.NotEqualsContext ctx);
	/**
	 * Exit a parse tree produced by the {@code NotEquals}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void exitNotEquals(YALGrammerParser.NotEqualsContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ExpressionPredicate}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void enterExpressionPredicate(YALGrammerParser.ExpressionPredicateContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ExpressionPredicate}
	 * labeled alternative in {@link YALGrammerParser#predicate}.
	 * @param ctx the parse tree
	 */
	void exitExpressionPredicate(YALGrammerParser.ExpressionPredicateContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#ifStatement}.
	 * @param ctx the parse tree
	 */
	void enterIfStatement(YALGrammerParser.IfStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#ifStatement}.
	 * @param ctx the parse tree
	 */
	void exitIfStatement(YALGrammerParser.IfStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#elseIfStatement}.
	 * @param ctx the parse tree
	 */
	void enterElseIfStatement(YALGrammerParser.ElseIfStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#elseIfStatement}.
	 * @param ctx the parse tree
	 */
	void exitElseIfStatement(YALGrammerParser.ElseIfStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#elseStatement}.
	 * @param ctx the parse tree
	 */
	void enterElseStatement(YALGrammerParser.ElseStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#elseStatement}.
	 * @param ctx the parse tree
	 */
	void exitElseStatement(YALGrammerParser.ElseStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#whileStatement}.
	 * @param ctx the parse tree
	 */
	void enterWhileStatement(YALGrammerParser.WhileStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#whileStatement}.
	 * @param ctx the parse tree
	 */
	void exitWhileStatement(YALGrammerParser.WhileStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link YALGrammerParser#forStatement}.
	 * @param ctx the parse tree
	 */
	void enterForStatement(YALGrammerParser.ForStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link YALGrammerParser#forStatement}.
	 * @param ctx the parse tree
	 */
	void exitForStatement(YALGrammerParser.ForStatementContext ctx);
}