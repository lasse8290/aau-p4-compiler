//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.11.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/rilar/source/repos/aau-p4-compiler/YALCompiler/YALCompiler/Grammar\YALGrammer.g4 by ANTLR 4.11.1

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
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.11.1")]
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
	/// Enter a parse tree produced by <see cref="YALGrammerParser.globalVariableDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGlobalVariableDeclaration([NotNull] YALGrammerParser.GlobalVariableDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.globalVariableDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGlobalVariableDeclaration([NotNull] YALGrammerParser.GlobalVariableDeclarationContext context);
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
	/// Enter a parse tree produced by the <c>TupleVariableDeclaration</c>
	/// labeled alternative in <see cref="YALGrammerParser.variableDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTupleVariableDeclaration([NotNull] YALGrammerParser.TupleVariableDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>TupleVariableDeclaration</c>
	/// labeled alternative in <see cref="YALGrammerParser.variableDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTupleVariableDeclaration([NotNull] YALGrammerParser.TupleVariableDeclarationContext context);
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
	/// Enter a parse tree produced by <see cref="YALGrammerParser.enumDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEnumDeclaration([NotNull] YALGrammerParser.EnumDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.enumDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEnumDeclaration([NotNull] YALGrammerParser.EnumDeclarationContext context);
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
	/// Enter a parse tree produced by the <c>IdAdditionAssignment</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIdAdditionAssignment([NotNull] YALGrammerParser.IdAdditionAssignmentContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>IdAdditionAssignment</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIdAdditionAssignment([NotNull] YALGrammerParser.IdAdditionAssignmentContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>IdSubtractionAssignment</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIdSubtractionAssignment([NotNull] YALGrammerParser.IdSubtractionAssignmentContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>IdSubtractionAssignment</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIdSubtractionAssignment([NotNull] YALGrammerParser.IdSubtractionAssignmentContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>IdPostIncrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIdPostIncrement([NotNull] YALGrammerParser.IdPostIncrementContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>IdPostIncrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIdPostIncrement([NotNull] YALGrammerParser.IdPostIncrementContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>IdPostDecrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIdPostDecrement([NotNull] YALGrammerParser.IdPostDecrementContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>IdPostDecrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIdPostDecrement([NotNull] YALGrammerParser.IdPostDecrementContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>IdPreDecrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIdPreDecrement([NotNull] YALGrammerParser.IdPreDecrementContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>IdPreDecrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIdPreDecrement([NotNull] YALGrammerParser.IdPreDecrementContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>IdPreIncrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIdPreIncrement([NotNull] YALGrammerParser.IdPreIncrementContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>IdPreIncrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIdPreIncrement([NotNull] YALGrammerParser.IdPreIncrementContext context);
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
	/// Enter a parse tree produced by <see cref="YALGrammerParser.tupleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTupleAssignment([NotNull] YALGrammerParser.TupleAssignmentContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.tupleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTupleAssignment([NotNull] YALGrammerParser.TupleAssignmentContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="YALGrammerParser.tupleDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTupleDeclaration([NotNull] YALGrammerParser.TupleDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.tupleDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTupleDeclaration([NotNull] YALGrammerParser.TupleDeclarationContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="YALGrammerParser.tupleId"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTupleId([NotNull] YALGrammerParser.TupleIdContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.tupleId"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTupleId([NotNull] YALGrammerParser.TupleIdContext context);
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
	/// Enter a parse tree produced by the <c>PreIncrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPreIncrement([NotNull] YALGrammerParser.PreIncrementContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>PreIncrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPreIncrement([NotNull] YALGrammerParser.PreIncrementContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Multiplication</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMultiplication([NotNull] YALGrammerParser.MultiplicationContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Multiplication</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMultiplication([NotNull] YALGrammerParser.MultiplicationContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Addition</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAddition([NotNull] YALGrammerParser.AdditionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Addition</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAddition([NotNull] YALGrammerParser.AdditionContext context);
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
	/// Enter a parse tree produced by the <c>Modulo</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterModulo([NotNull] YALGrammerParser.ModuloContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Modulo</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitModulo([NotNull] YALGrammerParser.ModuloContext context);
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
	/// Enter a parse tree produced by the <c>PostDecrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPostDecrement([NotNull] YALGrammerParser.PostDecrementContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>PostDecrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPostDecrement([NotNull] YALGrammerParser.PostDecrementContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>RightShift</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRightShift([NotNull] YALGrammerParser.RightShiftContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>RightShift</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRightShift([NotNull] YALGrammerParser.RightShiftContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>LeftShift</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLeftShift([NotNull] YALGrammerParser.LeftShiftContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>LeftShift</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLeftShift([NotNull] YALGrammerParser.LeftShiftContext context);
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
	/// Enter a parse tree produced by the <c>AsyncFunctionCallExpression</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAsyncFunctionCallExpression([NotNull] YALGrammerParser.AsyncFunctionCallExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>AsyncFunctionCallExpression</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAsyncFunctionCallExpression([NotNull] YALGrammerParser.AsyncFunctionCallExpressionContext context);
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
	/// Enter a parse tree produced by the <c>Subtraction</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSubtraction([NotNull] YALGrammerParser.SubtractionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Subtraction</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSubtraction([NotNull] YALGrammerParser.SubtractionContext context);
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
	/// Enter a parse tree produced by the <c>Division</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDivision([NotNull] YALGrammerParser.DivisionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Division</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDivision([NotNull] YALGrammerParser.DivisionContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>PostIncrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPostIncrement([NotNull] YALGrammerParser.PostIncrementContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>PostIncrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPostIncrement([NotNull] YALGrammerParser.PostIncrementContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>PreDecrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPreDecrement([NotNull] YALGrammerParser.PreDecrementContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>PreDecrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPreDecrement([NotNull] YALGrammerParser.PreDecrementContext context);
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
	/// Enter a parse tree produced by <see cref="YALGrammerParser.actualInputParams"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterActualInputParams([NotNull] YALGrammerParser.ActualInputParamsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="YALGrammerParser.actualInputParams"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitActualInputParams([NotNull] YALGrammerParser.ActualInputParamsContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Not</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNot([NotNull] YALGrammerParser.NotContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Not</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNot([NotNull] YALGrammerParser.NotContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>LessThan</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLessThan([NotNull] YALGrammerParser.LessThanContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>LessThan</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLessThan([NotNull] YALGrammerParser.LessThanContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Equals</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEquals([NotNull] YALGrammerParser.EqualsContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Equals</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEquals([NotNull] YALGrammerParser.EqualsContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Or</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOr([NotNull] YALGrammerParser.OrContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Or</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOr([NotNull] YALGrammerParser.OrContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>ParenthesizedPredicate</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParenthesizedPredicate([NotNull] YALGrammerParser.ParenthesizedPredicateContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ParenthesizedPredicate</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParenthesizedPredicate([NotNull] YALGrammerParser.ParenthesizedPredicateContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>LessThanOrEqual</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLessThanOrEqual([NotNull] YALGrammerParser.LessThanOrEqualContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>LessThanOrEqual</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLessThanOrEqual([NotNull] YALGrammerParser.LessThanOrEqualContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>GreaterThan</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGreaterThan([NotNull] YALGrammerParser.GreaterThanContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>GreaterThan</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGreaterThan([NotNull] YALGrammerParser.GreaterThanContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>BooleanLiteral</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBooleanLiteral([NotNull] YALGrammerParser.BooleanLiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>BooleanLiteral</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBooleanLiteral([NotNull] YALGrammerParser.BooleanLiteralContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>And</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAnd([NotNull] YALGrammerParser.AndContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>And</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAnd([NotNull] YALGrammerParser.AndContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>GreaterThanOrEqual</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGreaterThanOrEqual([NotNull] YALGrammerParser.GreaterThanOrEqualContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>GreaterThanOrEqual</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGreaterThanOrEqual([NotNull] YALGrammerParser.GreaterThanOrEqualContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>NotEquals</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNotEquals([NotNull] YALGrammerParser.NotEqualsContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>NotEquals</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNotEquals([NotNull] YALGrammerParser.NotEqualsContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>ExpressionPredicate</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpressionPredicate([NotNull] YALGrammerParser.ExpressionPredicateContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ExpressionPredicate</c>
	/// labeled alternative in <see cref="YALGrammerParser.predicate"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpressionPredicate([NotNull] YALGrammerParser.ExpressionPredicateContext context);
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
}