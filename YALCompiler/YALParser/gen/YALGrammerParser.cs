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

using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.12.0")]
[System.CLSCompliant(false)]
public partial class YALGrammerParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, OPERATOR=8, TYPE=9, 
		IN=10, ID=11, NUMBER=12, WHITESPACE=13, NEWLINE=14;
	public const int
		RULE_yalg = 0, RULE_program = 1, RULE_function = 2, RULE_command = 3, 
		RULE_variableDeclaration = 4, RULE_assignment = 5, RULE_expression = 6, 
		RULE_baseExpression = 7, RULE_functionCall = 8;
	public static readonly string[] ruleNames = {
		"yalg", "program", "function", "command", "variableDeclaration", "assignment", 
		"expression", "baseExpression", "functionCall"
	};

	private static readonly string[] _LiteralNames = {
		null, "'{'", "';'", "'}'", "'='", "'('", "')'", "','", null, null, "'in'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, "OPERATOR", "TYPE", "IN", 
		"ID", "NUMBER", "WHITESPACE", "NEWLINE"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "YALGrammer.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static YALGrammerParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public YALGrammerParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public YALGrammerParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	public partial class YalgContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ProgramContext program() {
			return GetRuleContext<ProgramContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode Eof() { return GetToken(YALGrammerParser.Eof, 0); }
		public YalgContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_yalg; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IYALGrammerListener typedListener = listener as IYALGrammerListener;
			if (typedListener != null) typedListener.EnterYalg(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IYALGrammerListener typedListener = listener as IYALGrammerListener;
			if (typedListener != null) typedListener.ExitYalg(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IYALGrammerVisitor<TResult> typedVisitor = visitor as IYALGrammerVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitYalg(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public YalgContext yalg() {
		YalgContext _localctx = new YalgContext(Context, State);
		EnterRule(_localctx, 0, RULE_yalg);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 18;
			program();
			State = 19;
			Match(Eof);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ProgramContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public FunctionContext[] function() {
			return GetRuleContexts<FunctionContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public FunctionContext function(int i) {
			return GetRuleContext<FunctionContext>(i);
		}
		public ProgramContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_program; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IYALGrammerListener typedListener = listener as IYALGrammerListener;
			if (typedListener != null) typedListener.EnterProgram(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IYALGrammerListener typedListener = listener as IYALGrammerListener;
			if (typedListener != null) typedListener.ExitProgram(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IYALGrammerVisitor<TResult> typedVisitor = visitor as IYALGrammerVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProgram(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ProgramContext program() {
		ProgramContext _localctx = new ProgramContext(Context, State);
		EnterRule(_localctx, 2, RULE_program);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 24;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while (_la==ID) {
				{
				{
				State = 21;
				function();
				}
				}
				State = 26;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class FunctionContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode ID() { return GetToken(YALGrammerParser.ID, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public CommandContext[] command() {
			return GetRuleContexts<CommandContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public CommandContext command(int i) {
			return GetRuleContext<CommandContext>(i);
		}
		public FunctionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_function; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IYALGrammerListener typedListener = listener as IYALGrammerListener;
			if (typedListener != null) typedListener.EnterFunction(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IYALGrammerListener typedListener = listener as IYALGrammerListener;
			if (typedListener != null) typedListener.ExitFunction(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IYALGrammerVisitor<TResult> typedVisitor = visitor as IYALGrammerVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitFunction(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public FunctionContext function() {
		FunctionContext _localctx = new FunctionContext(Context, State);
		EnterRule(_localctx, 4, RULE_function);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 27;
			Match(ID);
			State = 28;
			Match(T__0);
			State = 34;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while (_la==TYPE || _la==ID) {
				{
				{
				State = 29;
				command();
				State = 30;
				Match(T__1);
				}
				}
				State = 36;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			State = 37;
			Match(T__2);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class CommandContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public VariableDeclarationContext variableDeclaration() {
			return GetRuleContext<VariableDeclarationContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public AssignmentContext assignment() {
			return GetRuleContext<AssignmentContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public FunctionCallContext functionCall() {
			return GetRuleContext<FunctionCallContext>(0);
		}
		public CommandContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_command; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IYALGrammerListener typedListener = listener as IYALGrammerListener;
			if (typedListener != null) typedListener.EnterCommand(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IYALGrammerListener typedListener = listener as IYALGrammerListener;
			if (typedListener != null) typedListener.ExitCommand(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IYALGrammerVisitor<TResult> typedVisitor = visitor as IYALGrammerVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitCommand(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public CommandContext command() {
		CommandContext _localctx = new CommandContext(Context, State);
		EnterRule(_localctx, 6, RULE_command);
		try {
			State = 42;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,2,Context) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 39;
				variableDeclaration();
				}
				break;
			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 40;
				assignment();
				}
				break;
			case 3:
				EnterOuterAlt(_localctx, 3);
				{
				State = 41;
				functionCall();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class VariableDeclarationContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode TYPE() { return GetToken(YALGrammerParser.TYPE, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode ID() { return GetToken(YALGrammerParser.ID, 0); }
		public VariableDeclarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_variableDeclaration; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IYALGrammerListener typedListener = listener as IYALGrammerListener;
			if (typedListener != null) typedListener.EnterVariableDeclaration(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IYALGrammerListener typedListener = listener as IYALGrammerListener;
			if (typedListener != null) typedListener.ExitVariableDeclaration(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IYALGrammerVisitor<TResult> typedVisitor = visitor as IYALGrammerVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitVariableDeclaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public VariableDeclarationContext variableDeclaration() {
		VariableDeclarationContext _localctx = new VariableDeclarationContext(Context, State);
		EnterRule(_localctx, 8, RULE_variableDeclaration);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 44;
			Match(TYPE);
			State = 45;
			Match(ID);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class AssignmentContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ExpressionContext expression() {
			return GetRuleContext<ExpressionContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public VariableDeclarationContext variableDeclaration() {
			return GetRuleContext<VariableDeclarationContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode ID() { return GetToken(YALGrammerParser.ID, 0); }
		public AssignmentContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_assignment; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IYALGrammerListener typedListener = listener as IYALGrammerListener;
			if (typedListener != null) typedListener.EnterAssignment(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IYALGrammerListener typedListener = listener as IYALGrammerListener;
			if (typedListener != null) typedListener.ExitAssignment(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IYALGrammerVisitor<TResult> typedVisitor = visitor as IYALGrammerVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAssignment(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public AssignmentContext assignment() {
		AssignmentContext _localctx = new AssignmentContext(Context, State);
		EnterRule(_localctx, 10, RULE_assignment);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 49;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case TYPE:
				{
				State = 47;
				variableDeclaration();
				}
				break;
			case ID:
				{
				State = 48;
				Match(ID);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			State = 51;
			Match(T__3);
			State = 52;
			expression();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ExpressionContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public BaseExpressionContext[] baseExpression() {
			return GetRuleContexts<BaseExpressionContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public BaseExpressionContext baseExpression(int i) {
			return GetRuleContext<BaseExpressionContext>(i);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode[] OPERATOR() { return GetTokens(YALGrammerParser.OPERATOR); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OPERATOR(int i) {
			return GetToken(YALGrammerParser.OPERATOR, i);
		}
		public ExpressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_expression; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IYALGrammerListener typedListener = listener as IYALGrammerListener;
			if (typedListener != null) typedListener.EnterExpression(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IYALGrammerListener typedListener = listener as IYALGrammerListener;
			if (typedListener != null) typedListener.ExitExpression(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IYALGrammerVisitor<TResult> typedVisitor = visitor as IYALGrammerVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitExpression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ExpressionContext expression() {
		ExpressionContext _localctx = new ExpressionContext(Context, State);
		EnterRule(_localctx, 12, RULE_expression);
		int _la;
		try {
			State = 77;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,9,Context) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 54;
				baseExpression();
				}
				break;
			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 56;
				ErrorHandler.Sync(this);
				switch ( Interpreter.AdaptivePredict(TokenStream,4,Context) ) {
				case 1:
					{
					State = 55;
					Match(T__4);
					}
					break;
				}
				State = 58;
				baseExpression();
				State = 59;
				Match(OPERATOR);
				State = 61;
				ErrorHandler.Sync(this);
				switch ( Interpreter.AdaptivePredict(TokenStream,5,Context) ) {
				case 1:
					{
					State = 60;
					Match(T__4);
					}
					break;
				}
				State = 63;
				baseExpression();
				State = 68;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
				while (_la==OPERATOR) {
					{
					{
					State = 64;
					Match(OPERATOR);
					State = 65;
					baseExpression();
					}
					}
					State = 70;
					ErrorHandler.Sync(this);
					_la = TokenStream.LA(1);
				}
				State = 72;
				ErrorHandler.Sync(this);
				switch ( Interpreter.AdaptivePredict(TokenStream,7,Context) ) {
				case 1:
					{
					State = 71;
					Match(T__5);
					}
					break;
				}
				State = 75;
				ErrorHandler.Sync(this);
				switch ( Interpreter.AdaptivePredict(TokenStream,8,Context) ) {
				case 1:
					{
					State = 74;
					Match(T__5);
					}
					break;
				}
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class BaseExpressionContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode ID() { return GetToken(YALGrammerParser.ID, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public FunctionCallContext functionCall() {
			return GetRuleContext<FunctionCallContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode NUMBER() { return GetToken(YALGrammerParser.NUMBER, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public BaseExpressionContext baseExpression() {
			return GetRuleContext<BaseExpressionContext>(0);
		}
		public BaseExpressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_baseExpression; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IYALGrammerListener typedListener = listener as IYALGrammerListener;
			if (typedListener != null) typedListener.EnterBaseExpression(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IYALGrammerListener typedListener = listener as IYALGrammerListener;
			if (typedListener != null) typedListener.ExitBaseExpression(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IYALGrammerVisitor<TResult> typedVisitor = visitor as IYALGrammerVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitBaseExpression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public BaseExpressionContext baseExpression() {
		BaseExpressionContext _localctx = new BaseExpressionContext(Context, State);
		EnterRule(_localctx, 14, RULE_baseExpression);
		try {
			State = 86;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,10,Context) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 79;
				Match(ID);
				}
				break;
			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 80;
				functionCall();
				}
				break;
			case 3:
				EnterOuterAlt(_localctx, 3);
				{
				State = 81;
				Match(NUMBER);
				}
				break;
			case 4:
				EnterOuterAlt(_localctx, 4);
				{
				State = 82;
				Match(T__4);
				State = 83;
				baseExpression();
				State = 84;
				Match(T__5);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class FunctionCallContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode ID() { return GetToken(YALGrammerParser.ID, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ExpressionContext[] expression() {
			return GetRuleContexts<ExpressionContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public ExpressionContext expression(int i) {
			return GetRuleContext<ExpressionContext>(i);
		}
		public FunctionCallContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_functionCall; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IYALGrammerListener typedListener = listener as IYALGrammerListener;
			if (typedListener != null) typedListener.EnterFunctionCall(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IYALGrammerListener typedListener = listener as IYALGrammerListener;
			if (typedListener != null) typedListener.ExitFunctionCall(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IYALGrammerVisitor<TResult> typedVisitor = visitor as IYALGrammerVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitFunctionCall(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public FunctionCallContext functionCall() {
		FunctionCallContext _localctx = new FunctionCallContext(Context, State);
		EnterRule(_localctx, 16, RULE_functionCall);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 88;
			Match(ID);
			State = 89;
			Match(T__4);
			State = 98;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & 6176L) != 0)) {
				{
				State = 90;
				expression();
				State = 95;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
				while (_la==T__6) {
					{
					{
					State = 91;
					Match(T__6);
					State = 92;
					expression();
					}
					}
					State = 97;
					ErrorHandler.Sync(this);
					_la = TokenStream.LA(1);
				}
				}
			}

			State = 100;
			Match(T__5);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	private static int[] _serializedATN = {
		4,1,14,103,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,6,2,7,
		7,7,2,8,7,8,1,0,1,0,1,0,1,1,5,1,23,8,1,10,1,12,1,26,9,1,1,2,1,2,1,2,1,
		2,1,2,5,2,33,8,2,10,2,12,2,36,9,2,1,2,1,2,1,3,1,3,1,3,3,3,43,8,3,1,4,1,
		4,1,4,1,5,1,5,3,5,50,8,5,1,5,1,5,1,5,1,6,1,6,3,6,57,8,6,1,6,1,6,1,6,3,
		6,62,8,6,1,6,1,6,1,6,5,6,67,8,6,10,6,12,6,70,9,6,1,6,3,6,73,8,6,1,6,3,
		6,76,8,6,3,6,78,8,6,1,7,1,7,1,7,1,7,1,7,1,7,1,7,3,7,87,8,7,1,8,1,8,1,8,
		1,8,1,8,5,8,94,8,8,10,8,12,8,97,9,8,3,8,99,8,8,1,8,1,8,1,8,0,0,9,0,2,4,
		6,8,10,12,14,16,0,0,109,0,18,1,0,0,0,2,24,1,0,0,0,4,27,1,0,0,0,6,42,1,
		0,0,0,8,44,1,0,0,0,10,49,1,0,0,0,12,77,1,0,0,0,14,86,1,0,0,0,16,88,1,0,
		0,0,18,19,3,2,1,0,19,20,5,0,0,1,20,1,1,0,0,0,21,23,3,4,2,0,22,21,1,0,0,
		0,23,26,1,0,0,0,24,22,1,0,0,0,24,25,1,0,0,0,25,3,1,0,0,0,26,24,1,0,0,0,
		27,28,5,11,0,0,28,34,5,1,0,0,29,30,3,6,3,0,30,31,5,2,0,0,31,33,1,0,0,0,
		32,29,1,0,0,0,33,36,1,0,0,0,34,32,1,0,0,0,34,35,1,0,0,0,35,37,1,0,0,0,
		36,34,1,0,0,0,37,38,5,3,0,0,38,5,1,0,0,0,39,43,3,8,4,0,40,43,3,10,5,0,
		41,43,3,16,8,0,42,39,1,0,0,0,42,40,1,0,0,0,42,41,1,0,0,0,43,7,1,0,0,0,
		44,45,5,9,0,0,45,46,5,11,0,0,46,9,1,0,0,0,47,50,3,8,4,0,48,50,5,11,0,0,
		49,47,1,0,0,0,49,48,1,0,0,0,50,51,1,0,0,0,51,52,5,4,0,0,52,53,3,12,6,0,
		53,11,1,0,0,0,54,78,3,14,7,0,55,57,5,5,0,0,56,55,1,0,0,0,56,57,1,0,0,0,
		57,58,1,0,0,0,58,59,3,14,7,0,59,61,5,8,0,0,60,62,5,5,0,0,61,60,1,0,0,0,
		61,62,1,0,0,0,62,63,1,0,0,0,63,68,3,14,7,0,64,65,5,8,0,0,65,67,3,14,7,
		0,66,64,1,0,0,0,67,70,1,0,0,0,68,66,1,0,0,0,68,69,1,0,0,0,69,72,1,0,0,
		0,70,68,1,0,0,0,71,73,5,6,0,0,72,71,1,0,0,0,72,73,1,0,0,0,73,75,1,0,0,
		0,74,76,5,6,0,0,75,74,1,0,0,0,75,76,1,0,0,0,76,78,1,0,0,0,77,54,1,0,0,
		0,77,56,1,0,0,0,78,13,1,0,0,0,79,87,5,11,0,0,80,87,3,16,8,0,81,87,5,12,
		0,0,82,83,5,5,0,0,83,84,3,14,7,0,84,85,5,6,0,0,85,87,1,0,0,0,86,79,1,0,
		0,0,86,80,1,0,0,0,86,81,1,0,0,0,86,82,1,0,0,0,87,15,1,0,0,0,88,89,5,11,
		0,0,89,98,5,5,0,0,90,95,3,12,6,0,91,92,5,7,0,0,92,94,3,12,6,0,93,91,1,
		0,0,0,94,97,1,0,0,0,95,93,1,0,0,0,95,96,1,0,0,0,96,99,1,0,0,0,97,95,1,
		0,0,0,98,90,1,0,0,0,98,99,1,0,0,0,99,100,1,0,0,0,100,101,5,6,0,0,101,17,
		1,0,0,0,13,24,34,42,49,56,61,68,72,75,77,86,95,98
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
