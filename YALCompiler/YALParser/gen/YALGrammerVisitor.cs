//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.12.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/rilar/source/repos/aau-p4-compiler/YALCompiler/YALParser/Gammar/YALGrammer.g4 by ANTLR 4.12.0

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
	/// Visit a parse tree produced by <see cref="YALGrammerParser.yalg"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitYalg([NotNull] YALGrammerParser.YalgContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] YALGrammerParser.ProgramContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.function"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunction([NotNull] YALGrammerParser.FunctionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.command"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCommand([NotNull] YALGrammerParser.CommandContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.variableDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableDeclaration([NotNull] YALGrammerParser.VariableDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignment([NotNull] YALGrammerParser.AssignmentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] YALGrammerParser.ExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.baseExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBaseExpression([NotNull] YALGrammerParser.BaseExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="YALGrammerParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCall([NotNull] YALGrammerParser.FunctionCallContext context);
}
