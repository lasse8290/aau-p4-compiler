//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.12.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/rilar/source/repos/aau-p4-compiler/YALCompiler/YALCompiler/Grammar\YALGrammer.g4 by ANTLR 4.12.0

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="YALGrammerParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.12.0")]
[System.CLSCompliant(false)]
public interface IYALGrammerVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] YALGrammerParser.ProgramContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.externalFunctionDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExternalFunctionDeclaration([NotNull] YALGrammerParser.ExternalFunctionDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.functionDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionDeclaration([NotNull] YALGrammerParser.FunctionDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.formalInputParams"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFormalInputParams([NotNull] YALGrammerParser.FormalInputParamsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.formalOutputParams"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFormalOutputParams([NotNull] YALGrammerParser.FormalOutputParamsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.statementBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatementBlock([NotNull] YALGrammerParser.StatementBlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.blockStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlockStatement([NotNull] YALGrammerParser.BlockStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.singleStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSingleStatement([NotNull] YALGrammerParser.SingleStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.variableDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableDeclaration([NotNull] YALGrammerParser.VariableDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ReferenceVariableDeclaration</c>
	/// labeled alternative in <see cref="YALGrammerParser.variableDeclarationFormat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReferenceVariableDeclaration([NotNull] YALGrammerParser.ReferenceVariableDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ArrayDeclaration</c>
	/// labeled alternative in <see cref="YALGrammerParser.variableDeclarationFormat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrayDeclaration([NotNull] YALGrammerParser.ArrayDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>SimpleVariableDeclaration</c>
	/// labeled alternative in <see cref="YALGrammerParser.variableDeclarationFormat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSimpleVariableDeclaration([NotNull] YALGrammerParser.SimpleVariableDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignment([NotNull] YALGrammerParser.AssignmentContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>IdAssignment</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdAssignment([NotNull] YALGrammerParser.IdAssignmentContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>IdPreIncrementDecrementAssignment</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdPreIncrementDecrementAssignment([NotNull] YALGrammerParser.IdPreIncrementDecrementAssignmentContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>IdPostIncrementDecrementAssignment</c>
	/// labeled alternative in <see cref="YALGrammerParser.simpleAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdPostIncrementDecrementAssignment([NotNull] YALGrammerParser.IdPostIncrementDecrementAssignmentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.declarationAssignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclarationAssignment([NotNull] YALGrammerParser.DeclarationAssignmentContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ParenthesizedExpression</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenthesizedExpression([NotNull] YALGrammerParser.ParenthesizedExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Variable</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariable([NotNull] YALGrammerParser.VariableContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Or</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOr([NotNull] YALGrammerParser.OrContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PrefixUnary</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrefixUnary([NotNull] YALGrammerParser.PrefixUnaryContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>AdditionSubtraction</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAdditionSubtraction([NotNull] YALGrammerParser.AdditionSubtractionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>FloatLiteral</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFloatLiteral([NotNull] YALGrammerParser.FloatLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>BooleanLiteral</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBooleanLiteral([NotNull] YALGrammerParser.BooleanLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>VariableAssignment</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableAssignment([NotNull] YALGrammerParser.VariableAssignmentContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ArrayLiteral</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrayLiteral([NotNull] YALGrammerParser.ArrayLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>FunctionCallExpression</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCallExpression([NotNull] YALGrammerParser.FunctionCallExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Not</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNot([NotNull] YALGrammerParser.NotContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>LeftRightShift</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLeftRightShift([NotNull] YALGrammerParser.LeftRightShiftContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>StringLiteral</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStringLiteral([NotNull] YALGrammerParser.StringLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>BitwiseXor</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBitwiseXor([NotNull] YALGrammerParser.BitwiseXorContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>BitwiseOr</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBitwiseOr([NotNull] YALGrammerParser.BitwiseOrContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Comparison</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComparison([NotNull] YALGrammerParser.ComparisonContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>And</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnd([NotNull] YALGrammerParser.AndContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>BitwiseAnd</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBitwiseAnd([NotNull] YALGrammerParser.BitwiseAndContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PostIncrementDecrement</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPostIncrementDecrement([NotNull] YALGrammerParser.PostIncrementDecrementContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>BitwiseNot</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBitwiseNot([NotNull] YALGrammerParser.BitwiseNotContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>MultiplicationDivisionModulo</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultiplicationDivisionModulo([NotNull] YALGrammerParser.MultiplicationDivisionModuloContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ExpressionList</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpressionList([NotNull] YALGrammerParser.ExpressionListContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>NumberLiteral</c>
	/// labeled alternative in <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumberLiteral([NotNull] YALGrammerParser.NumberLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCall([NotNull] YALGrammerParser.FunctionCallContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.ifStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfStatement([NotNull] YALGrammerParser.IfStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.elseIfStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseIfStatement([NotNull] YALGrammerParser.ElseIfStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.elseStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitElseStatement([NotNull] YALGrammerParser.ElseStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.whileStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileStatement([NotNull] YALGrammerParser.WhileStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.forStatement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitForStatement([NotNull] YALGrammerParser.ForStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ReferenceIdentifier</c>
	/// labeled alternative in <see cref="YALGrammerParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReferenceIdentifier([NotNull] YALGrammerParser.ReferenceIdentifierContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ParenthesizedIdentifier</c>
	/// labeled alternative in <see cref="YALGrammerParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenthesizedIdentifier([NotNull] YALGrammerParser.ParenthesizedIdentifierContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ArrayElementIdentifier</c>
	/// labeled alternative in <see cref="YALGrammerParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArrayElementIdentifier([NotNull] YALGrammerParser.ArrayElementIdentifierContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>SimpleIdentifier</c>
	/// labeled alternative in <see cref="YALGrammerParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSimpleIdentifier([NotNull] YALGrammerParser.SimpleIdentifierContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>IdentifierList</c>
	/// labeled alternative in <see cref="YALGrammerParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdentifierList([NotNull] YALGrammerParser.IdentifierListContext context);
}
