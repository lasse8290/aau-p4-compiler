//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.12.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/rilar/Documents/GitHub/aau-p4-compiler/YALCompiler/YALCompiler/Grammar\YALGrammer.g4 by ANTLR 4.12.0

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="YALGrammerParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.12.0")]
[System.CLSCompliant(false)]
public interface IYALGrammerListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="YALGrammerParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgram([NotNull] YALGrammerParser.ProgramContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgram([NotNull] YALGrammerParser.ProgramContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="YALGrammerParser.externalFunctionDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExternalFunctionDeclaration([NotNull] YALGrammerParser.ExternalFunctionDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.externalFunctionDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExternalFunctionDeclaration([NotNull] YALGrammerParser.ExternalFunctionDeclarationContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="YALGrammerParser.functionDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionDeclaration([NotNull] YALGrammerParser.FunctionDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.functionDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionDeclaration([NotNull] YALGrammerParser.FunctionDeclarationContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="YALGrammerParser.formalInputParams"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFormalInputParams([NotNull] YALGrammerParser.FormalInputParamsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.formalInputParams"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFormalInputParams([NotNull] YALGrammerParser.FormalInputParamsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="YALGrammerParser.formalOutputParams"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFormalOutputParams([NotNull] YALGrammerParser.FormalOutputParamsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.formalOutputParams"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFormalOutputParams([NotNull] YALGrammerParser.FormalOutputParamsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="YALGrammerParser.statementBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatementBlock([NotNull] YALGrammerParser.StatementBlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.statementBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatementBlock([NotNull] YALGrammerParser.StatementBlockContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="YALGrammerParser.blockStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlockStatement([NotNull] YALGrammerParser.BlockStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.blockStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlockStatement([NotNull] YALGrammerParser.BlockStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="YALGrammerParser.singleStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSingleStatement([NotNull] YALGrammerParser.SingleStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.singleStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSingleStatement([NotNull] YALGrammerParser.SingleStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>SimpleVariableDeclarationFormat</c>
	/// labeled alternative in <see cref="YALGrammerParser.variableDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSimpleVariableDeclarationFormat([NotNull] YALGrammerParser.SimpleVariableDeclarationFormatContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>SimpleVariableDeclarationFormat</c>
	/// labeled alternative in <see cref="YALGrammerParser.variableDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSimpleVariableDeclarationFormat([NotNull] YALGrammerParser.SimpleVariableDeclarationFormatContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>ReferenceVariableDeclarationFormat</c>
	/// labeled alternative in <see cref="YALGrammerParser.variableDeclarationFormat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReferenceVariableDeclarationFormat([NotNull] YALGrammerParser.ReferenceVariableDeclarationFormatContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ReferenceVariableDeclarationFormat</c>
	/// labeled alternative in <see cref="YALGrammerParser.variableDeclarationFormat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReferenceVariableDeclarationFormat([NotNull] YALGrammerParser.ReferenceVariableDeclarationFormatContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>ArrayDeclaration</c>
	/// labeled alternative in <see cref="YALGrammerParser.variableDeclarationFormat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArrayDeclaration([NotNull] YALGrammerParser.ArrayDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ArrayDeclaration</c>
	/// labeled alternative in <see cref="YALGrammerParser.variableDeclarationFormat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArrayDeclaration([NotNull] YALGrammerParser.ArrayDeclarationContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>SimpleVariableDeclaration</c>
	/// labeled alternative in <see cref="YALGrammerParser.variableDeclarationFormat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSimpleVariableDeclaration([NotNull] YALGrammerParser.SimpleVariableDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>SimpleVariableDeclaration</c>
	/// labeled alternative in <see cref="YALGrammerParser.variableDeclarationFormat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSimpleVariableDeclaration([NotNull] YALGrammerParser.SimpleVariableDeclarationContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="YALGrammerParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssignment([NotNull] YALGrammerParser.AssignmentContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssignment([NotNull] YALGrammerParser.AssignmentContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>IdAssignment</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIdAssignment([NotNull] YALGrammerParser.IdAssignmentContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>IdAssignment</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIdAssignment([NotNull] YALGrammerParser.IdAssignmentContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>IdPreIncrementDecrementAssignment</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIdPreIncrementDecrementAssignment([NotNull] YALGrammerParser.IdPreIncrementDecrementAssignmentContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>IdPreIncrementDecrementAssignment</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIdPreIncrementDecrementAssignment([NotNull] YALGrammerParser.IdPreIncrementDecrementAssignmentContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>IdPostIncrementDecrementAssignment</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIdPostIncrementDecrementAssignment([NotNull] YALGrammerParser.IdPostIncrementDecrementAssignmentContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>IdPostIncrementDecrementAssignment</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIdPostIncrementDecrementAssignment([NotNull] YALGrammerParser.IdPostIncrementDecrementAssignmentContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="YALGrammerParser.declarationAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDeclarationAssignment([NotNull] YALGrammerParser.DeclarationAssignmentContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.declarationAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDeclarationAssignment([NotNull] YALGrammerParser.DeclarationAssignmentContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>ParenthesizedExpression</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParenthesizedExpression([NotNull] YALGrammerParser.ParenthesizedExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ParenthesizedExpression</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParenthesizedExpression([NotNull] YALGrammerParser.ParenthesizedExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Variable</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVariable([NotNull] YALGrammerParser.VariableContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Variable</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVariable([NotNull] YALGrammerParser.VariableContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Or</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOr([NotNull] YALGrammerParser.OrContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Or</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOr([NotNull] YALGrammerParser.OrContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>PrefixUnary</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrefixUnary([NotNull] YALGrammerParser.PrefixUnaryContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>PrefixUnary</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrefixUnary([NotNull] YALGrammerParser.PrefixUnaryContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>AdditionSubtraction</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAdditionSubtraction([NotNull] YALGrammerParser.AdditionSubtractionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>AdditionSubtraction</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAdditionSubtraction([NotNull] YALGrammerParser.AdditionSubtractionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>FloatLiteral</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFloatLiteral([NotNull] YALGrammerParser.FloatLiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>FloatLiteral</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFloatLiteral([NotNull] YALGrammerParser.FloatLiteralContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>BooleanLiteral</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBooleanLiteral([NotNull] YALGrammerParser.BooleanLiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>BooleanLiteral</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBooleanLiteral([NotNull] YALGrammerParser.BooleanLiteralContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>VariableAssignment</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVariableAssignment([NotNull] YALGrammerParser.VariableAssignmentContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>VariableAssignment</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVariableAssignment([NotNull] YALGrammerParser.VariableAssignmentContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>ArrayLiteral</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArrayLiteral([NotNull] YALGrammerParser.ArrayLiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ArrayLiteral</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArrayLiteral([NotNull] YALGrammerParser.ArrayLiteralContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>FunctionCallExpression</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionCallExpression([NotNull] YALGrammerParser.FunctionCallExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>FunctionCallExpression</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionCallExpression([NotNull] YALGrammerParser.FunctionCallExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Not</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNot([NotNull] YALGrammerParser.NotContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Not</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNot([NotNull] YALGrammerParser.NotContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>LeftRightShift</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLeftRightShift([NotNull] YALGrammerParser.LeftRightShiftContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>LeftRightShift</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLeftRightShift([NotNull] YALGrammerParser.LeftRightShiftContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>StringLiteral</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStringLiteral([NotNull] YALGrammerParser.StringLiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>StringLiteral</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStringLiteral([NotNull] YALGrammerParser.StringLiteralContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>BitwiseXor</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBitwiseXor([NotNull] YALGrammerParser.BitwiseXorContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>BitwiseXor</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBitwiseXor([NotNull] YALGrammerParser.BitwiseXorContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>BitwiseOr</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBitwiseOr([NotNull] YALGrammerParser.BitwiseOrContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>BitwiseOr</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBitwiseOr([NotNull] YALGrammerParser.BitwiseOrContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Comparison</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterComparison([NotNull] YALGrammerParser.ComparisonContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Comparison</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitComparison([NotNull] YALGrammerParser.ComparisonContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>And</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAnd([NotNull] YALGrammerParser.AndContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>And</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAnd([NotNull] YALGrammerParser.AndContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>BitwiseAnd</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBitwiseAnd([NotNull] YALGrammerParser.BitwiseAndContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>BitwiseAnd</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBitwiseAnd([NotNull] YALGrammerParser.BitwiseAndContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>ReferenceExpression</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReferenceExpression([NotNull] YALGrammerParser.ReferenceExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ReferenceExpression</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReferenceExpression([NotNull] YALGrammerParser.ReferenceExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>PostIncrementDecrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPostIncrementDecrement([NotNull] YALGrammerParser.PostIncrementDecrementContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>PostIncrementDecrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPostIncrementDecrement([NotNull] YALGrammerParser.PostIncrementDecrementContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>MultiplicationDivisionModulo</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMultiplicationDivisionModulo([NotNull] YALGrammerParser.MultiplicationDivisionModuloContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>MultiplicationDivisionModulo</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMultiplicationDivisionModulo([NotNull] YALGrammerParser.MultiplicationDivisionModuloContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>BitwiseNot</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBitwiseNot([NotNull] YALGrammerParser.BitwiseNotContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>BitwiseNot</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBitwiseNot([NotNull] YALGrammerParser.BitwiseNotContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>ExpressionList</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpressionList([NotNull] YALGrammerParser.ExpressionListContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ExpressionList</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpressionList([NotNull] YALGrammerParser.ExpressionListContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>NumberLiteral</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumberLiteral([NotNull] YALGrammerParser.NumberLiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>NumberLiteral</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumberLiteral([NotNull] YALGrammerParser.NumberLiteralContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="YALGrammerParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionCall([NotNull] YALGrammerParser.FunctionCallContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionCall([NotNull] YALGrammerParser.FunctionCallContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="YALGrammerParser.ifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIfStatement([NotNull] YALGrammerParser.IfStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.ifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIfStatement([NotNull] YALGrammerParser.IfStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="YALGrammerParser.elseIfStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterElseIfStatement([NotNull] YALGrammerParser.ElseIfStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.elseIfStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitElseIfStatement([NotNull] YALGrammerParser.ElseIfStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="YALGrammerParser.elseStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterElseStatement([NotNull] YALGrammerParser.ElseStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.elseStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitElseStatement([NotNull] YALGrammerParser.ElseStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="YALGrammerParser.whileStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWhileStatement([NotNull] YALGrammerParser.WhileStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.whileStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWhileStatement([NotNull] YALGrammerParser.WhileStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="YALGrammerParser.forStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterForStatement([NotNull] YALGrammerParser.ForStatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.forStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitForStatement([NotNull] YALGrammerParser.ForStatementContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>ParenthesizedIdentifier</c>
	/// labeled alternative in <see cref="YALGrammerParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParenthesizedIdentifier([NotNull] YALGrammerParser.ParenthesizedIdentifierContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ParenthesizedIdentifier</c>
	/// labeled alternative in <see cref="YALGrammerParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParenthesizedIdentifier([NotNull] YALGrammerParser.ParenthesizedIdentifierContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>ArrayElementIdentifier</c>
	/// labeled alternative in <see cref="YALGrammerParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterArrayElementIdentifier([NotNull] YALGrammerParser.ArrayElementIdentifierContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ArrayElementIdentifier</c>
	/// labeled alternative in <see cref="YALGrammerParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitArrayElementIdentifier([NotNull] YALGrammerParser.ArrayElementIdentifierContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>SimpleIdentifier</c>
	/// labeled alternative in <see cref="YALGrammerParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSimpleIdentifier([NotNull] YALGrammerParser.SimpleIdentifierContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>SimpleIdentifier</c>
	/// labeled alternative in <see cref="YALGrammerParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSimpleIdentifier([NotNull] YALGrammerParser.SimpleIdentifierContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>IdentifierList</c>
	/// labeled alternative in <see cref="YALGrammerParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIdentifierList([NotNull] YALGrammerParser.IdentifierListContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>IdentifierList</c>
	/// labeled alternative in <see cref="YALGrammerParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIdentifierList([NotNull] YALGrammerParser.IdentifierListContext context);
}
