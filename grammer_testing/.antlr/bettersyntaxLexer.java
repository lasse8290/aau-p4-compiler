// Generated from /home/lasse/repo/git/aau-p4-compiler/grammer_testing/bettersyntax.g4 by ANTLR 4.9.2
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class bettersyntaxLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.9.2", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, T__20=21, T__21=22, T__22=23, T__23=24, 
		T__24=25, T__25=26, T__26=27, T__27=28, T__28=29, T__29=30, T__30=31, 
		T__31=32, T__32=33, T__33=34, T__34=35, T__35=36, T__36=37, T__37=38, 
		T__38=39, T__39=40, T__40=41, T__41=42, T__42=43, T__43=44, T__44=45, 
		T__45=46, T__46=47, T__47=48, T__48=49, T__49=50, T__50=51, T__51=52, 
		T__52=53, T__53=54, T__54=55, T__55=56, T__56=57, T__57=58, T__58=59, 
		T__59=60, T__60=61, T__61=62, T__62=63, T__63=64, T__64=65, T__65=66, 
		T__66=67, T__67=68, T__68=69;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	private static String[] makeRuleNames() {
		return new String[] {
			"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
			"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
			"T__17", "T__18", "T__19", "T__20", "T__21", "T__22", "T__23", "T__24", 
			"T__25", "T__26", "T__27", "T__28", "T__29", "T__30", "T__31", "T__32", 
			"T__33", "T__34", "T__35", "T__36", "T__37", "T__38", "T__39", "T__40", 
			"T__41", "T__42", "T__43", "T__44", "T__45", "T__46", "T__47", "T__48", 
			"T__49", "T__50", "T__51", "T__52", "T__53", "T__54", "T__55", "T__56", 
			"T__57", "T__58", "T__59", "T__60", "T__61", "T__62", "T__63", "T__64", 
			"T__65", "T__66", "T__67", "T__68"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'{'", "'}'", "'A'", "'B'", "'C'", "'D'", "'E'", "'F'", "'G'", 
			"'H'", "'I'", "'J'", "'K'", "'L'", "'M'", "'N'", "'O'", "'P'", "'Q'", 
			"'R'", "'S'", "'T'", "'U'", "'V'", "'W'", "'X'", "'Y'", "'Z'", "'a'", 
			"'b'", "'c'", "'d'", "'e'", "'f'", "'g'", "'h'", "'i'", "'j'", "'k'", 
			"'l'", "'m'", "'n'", "'o'", "'p'", "'q'", "'r'", "'s'", "'t'", "'u'", 
			"'v'", "'w'", "'x'", "'y'", "'z'", "'0'", "'1'", "'2'", "'3'", "'4'", 
			"'5'", "'6'", "'7'", "'8'", "'9'", "'_'", "'+'", "'-'", "'*'", "'/'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}


	public bettersyntaxLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "bettersyntax.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2G\u0117\b\1\4\2\t"+
		"\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4"+
		",\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64\t"+
		"\64\4\65\t\65\4\66\t\66\4\67\t\67\48\t8\49\t9\4:\t:\4;\t;\4<\t<\4=\t="+
		"\4>\t>\4?\t?\4@\t@\4A\tA\4B\tB\4C\tC\4D\tD\4E\tE\4F\tF\3\2\3\2\3\3\3\3"+
		"\3\4\3\4\3\5\3\5\3\6\3\6\3\7\3\7\3\b\3\b\3\t\3\t\3\n\3\n\3\13\3\13\3\f"+
		"\3\f\3\r\3\r\3\16\3\16\3\17\3\17\3\20\3\20\3\21\3\21\3\22\3\22\3\23\3"+
		"\23\3\24\3\24\3\25\3\25\3\26\3\26\3\27\3\27\3\30\3\30\3\31\3\31\3\32\3"+
		"\32\3\33\3\33\3\34\3\34\3\35\3\35\3\36\3\36\3\37\3\37\3 \3 \3!\3!\3\""+
		"\3\"\3#\3#\3$\3$\3%\3%\3&\3&\3\'\3\'\3(\3(\3)\3)\3*\3*\3+\3+\3,\3,\3-"+
		"\3-\3.\3.\3/\3/\3\60\3\60\3\61\3\61\3\62\3\62\3\63\3\63\3\64\3\64\3\65"+
		"\3\65\3\66\3\66\3\67\3\67\38\38\39\39\3:\3:\3;\3;\3<\3<\3=\3=\3>\3>\3"+
		"?\3?\3@\3@\3A\3A\3B\3B\3C\3C\3D\3D\3E\3E\3F\3F\2\2G\3\3\5\4\7\5\t\6\13"+
		"\7\r\b\17\t\21\n\23\13\25\f\27\r\31\16\33\17\35\20\37\21!\22#\23%\24\'"+
		"\25)\26+\27-\30/\31\61\32\63\33\65\34\67\359\36;\37= ?!A\"C#E$G%I&K\'"+
		"M(O)Q*S+U,W-Y.[/]\60_\61a\62c\63e\64g\65i\66k\67m8o9q:s;u<w=y>{?}@\177"+
		"A\u0081B\u0083C\u0085D\u0087E\u0089F\u008bG\3\2\2\2\u0116\2\3\3\2\2\2"+
		"\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2\17\3\2"+
		"\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27\3\2\2\2\2\31\3\2\2\2"+
		"\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2!\3\2\2\2\2#\3\2\2\2\2%\3\2\2"+
		"\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2\2-\3\2\2\2\2/\3\2\2\2\2\61\3\2\2"+
		"\2\2\63\3\2\2\2\2\65\3\2\2\2\2\67\3\2\2\2\29\3\2\2\2\2;\3\2\2\2\2=\3\2"+
		"\2\2\2?\3\2\2\2\2A\3\2\2\2\2C\3\2\2\2\2E\3\2\2\2\2G\3\2\2\2\2I\3\2\2\2"+
		"\2K\3\2\2\2\2M\3\2\2\2\2O\3\2\2\2\2Q\3\2\2\2\2S\3\2\2\2\2U\3\2\2\2\2W"+
		"\3\2\2\2\2Y\3\2\2\2\2[\3\2\2\2\2]\3\2\2\2\2_\3\2\2\2\2a\3\2\2\2\2c\3\2"+
		"\2\2\2e\3\2\2\2\2g\3\2\2\2\2i\3\2\2\2\2k\3\2\2\2\2m\3\2\2\2\2o\3\2\2\2"+
		"\2q\3\2\2\2\2s\3\2\2\2\2u\3\2\2\2\2w\3\2\2\2\2y\3\2\2\2\2{\3\2\2\2\2}"+
		"\3\2\2\2\2\177\3\2\2\2\2\u0081\3\2\2\2\2\u0083\3\2\2\2\2\u0085\3\2\2\2"+
		"\2\u0087\3\2\2\2\2\u0089\3\2\2\2\2\u008b\3\2\2\2\3\u008d\3\2\2\2\5\u008f"+
		"\3\2\2\2\7\u0091\3\2\2\2\t\u0093\3\2\2\2\13\u0095\3\2\2\2\r\u0097\3\2"+
		"\2\2\17\u0099\3\2\2\2\21\u009b\3\2\2\2\23\u009d\3\2\2\2\25\u009f\3\2\2"+
		"\2\27\u00a1\3\2\2\2\31\u00a3\3\2\2\2\33\u00a5\3\2\2\2\35\u00a7\3\2\2\2"+
		"\37\u00a9\3\2\2\2!\u00ab\3\2\2\2#\u00ad\3\2\2\2%\u00af\3\2\2\2\'\u00b1"+
		"\3\2\2\2)\u00b3\3\2\2\2+\u00b5\3\2\2\2-\u00b7\3\2\2\2/\u00b9\3\2\2\2\61"+
		"\u00bb\3\2\2\2\63\u00bd\3\2\2\2\65\u00bf\3\2\2\2\67\u00c1\3\2\2\29\u00c3"+
		"\3\2\2\2;\u00c5\3\2\2\2=\u00c7\3\2\2\2?\u00c9\3\2\2\2A\u00cb\3\2\2\2C"+
		"\u00cd\3\2\2\2E\u00cf\3\2\2\2G\u00d1\3\2\2\2I\u00d3\3\2\2\2K\u00d5\3\2"+
		"\2\2M\u00d7\3\2\2\2O\u00d9\3\2\2\2Q\u00db\3\2\2\2S\u00dd\3\2\2\2U\u00df"+
		"\3\2\2\2W\u00e1\3\2\2\2Y\u00e3\3\2\2\2[\u00e5\3\2\2\2]\u00e7\3\2\2\2_"+
		"\u00e9\3\2\2\2a\u00eb\3\2\2\2c\u00ed\3\2\2\2e\u00ef\3\2\2\2g\u00f1\3\2"+
		"\2\2i\u00f3\3\2\2\2k\u00f5\3\2\2\2m\u00f7\3\2\2\2o\u00f9\3\2\2\2q\u00fb"+
		"\3\2\2\2s\u00fd\3\2\2\2u\u00ff\3\2\2\2w\u0101\3\2\2\2y\u0103\3\2\2\2{"+
		"\u0105\3\2\2\2}\u0107\3\2\2\2\177\u0109\3\2\2\2\u0081\u010b\3\2\2\2\u0083"+
		"\u010d\3\2\2\2\u0085\u010f\3\2\2\2\u0087\u0111\3\2\2\2\u0089\u0113\3\2"+
		"\2\2\u008b\u0115\3\2\2\2\u008d\u008e\7}\2\2\u008e\4\3\2\2\2\u008f\u0090"+
		"\7\177\2\2\u0090\6\3\2\2\2\u0091\u0092\7C\2\2\u0092\b\3\2\2\2\u0093\u0094"+
		"\7D\2\2\u0094\n\3\2\2\2\u0095\u0096\7E\2\2\u0096\f\3\2\2\2\u0097\u0098"+
		"\7F\2\2\u0098\16\3\2\2\2\u0099\u009a\7G\2\2\u009a\20\3\2\2\2\u009b\u009c"+
		"\7H\2\2\u009c\22\3\2\2\2\u009d\u009e\7I\2\2\u009e\24\3\2\2\2\u009f\u00a0"+
		"\7J\2\2\u00a0\26\3\2\2\2\u00a1\u00a2\7K\2\2\u00a2\30\3\2\2\2\u00a3\u00a4"+
		"\7L\2\2\u00a4\32\3\2\2\2\u00a5\u00a6\7M\2\2\u00a6\34\3\2\2\2\u00a7\u00a8"+
		"\7N\2\2\u00a8\36\3\2\2\2\u00a9\u00aa\7O\2\2\u00aa \3\2\2\2\u00ab\u00ac"+
		"\7P\2\2\u00ac\"\3\2\2\2\u00ad\u00ae\7Q\2\2\u00ae$\3\2\2\2\u00af\u00b0"+
		"\7R\2\2\u00b0&\3\2\2\2\u00b1\u00b2\7S\2\2\u00b2(\3\2\2\2\u00b3\u00b4\7"+
		"T\2\2\u00b4*\3\2\2\2\u00b5\u00b6\7U\2\2\u00b6,\3\2\2\2\u00b7\u00b8\7V"+
		"\2\2\u00b8.\3\2\2\2\u00b9\u00ba\7W\2\2\u00ba\60\3\2\2\2\u00bb\u00bc\7"+
		"X\2\2\u00bc\62\3\2\2\2\u00bd\u00be\7Y\2\2\u00be\64\3\2\2\2\u00bf\u00c0"+
		"\7Z\2\2\u00c0\66\3\2\2\2\u00c1\u00c2\7[\2\2\u00c28\3\2\2\2\u00c3\u00c4"+
		"\7\\\2\2\u00c4:\3\2\2\2\u00c5\u00c6\7c\2\2\u00c6<\3\2\2\2\u00c7\u00c8"+
		"\7d\2\2\u00c8>\3\2\2\2\u00c9\u00ca\7e\2\2\u00ca@\3\2\2\2\u00cb\u00cc\7"+
		"f\2\2\u00ccB\3\2\2\2\u00cd\u00ce\7g\2\2\u00ceD\3\2\2\2\u00cf\u00d0\7h"+
		"\2\2\u00d0F\3\2\2\2\u00d1\u00d2\7i\2\2\u00d2H\3\2\2\2\u00d3\u00d4\7j\2"+
		"\2\u00d4J\3\2\2\2\u00d5\u00d6\7k\2\2\u00d6L\3\2\2\2\u00d7\u00d8\7l\2\2"+
		"\u00d8N\3\2\2\2\u00d9\u00da\7m\2\2\u00daP\3\2\2\2\u00db\u00dc\7n\2\2\u00dc"+
		"R\3\2\2\2\u00dd\u00de\7o\2\2\u00deT\3\2\2\2\u00df\u00e0\7p\2\2\u00e0V"+
		"\3\2\2\2\u00e1\u00e2\7q\2\2\u00e2X\3\2\2\2\u00e3\u00e4\7r\2\2\u00e4Z\3"+
		"\2\2\2\u00e5\u00e6\7s\2\2\u00e6\\\3\2\2\2\u00e7\u00e8\7t\2\2\u00e8^\3"+
		"\2\2\2\u00e9\u00ea\7u\2\2\u00ea`\3\2\2\2\u00eb\u00ec\7v\2\2\u00ecb\3\2"+
		"\2\2\u00ed\u00ee\7w\2\2\u00eed\3\2\2\2\u00ef\u00f0\7x\2\2\u00f0f\3\2\2"+
		"\2\u00f1\u00f2\7y\2\2\u00f2h\3\2\2\2\u00f3\u00f4\7z\2\2\u00f4j\3\2\2\2"+
		"\u00f5\u00f6\7{\2\2\u00f6l\3\2\2\2\u00f7\u00f8\7|\2\2\u00f8n\3\2\2\2\u00f9"+
		"\u00fa\7\62\2\2\u00fap\3\2\2\2\u00fb\u00fc\7\63\2\2\u00fcr\3\2\2\2\u00fd"+
		"\u00fe\7\64\2\2\u00fet\3\2\2\2\u00ff\u0100\7\65\2\2\u0100v\3\2\2\2\u0101"+
		"\u0102\7\66\2\2\u0102x\3\2\2\2\u0103\u0104\7\67\2\2\u0104z\3\2\2\2\u0105"+
		"\u0106\78\2\2\u0106|\3\2\2\2\u0107\u0108\79\2\2\u0108~\3\2\2\2\u0109\u010a"+
		"\7:\2\2\u010a\u0080\3\2\2\2\u010b\u010c\7;\2\2\u010c\u0082\3\2\2\2\u010d"+
		"\u010e\7a\2\2\u010e\u0084\3\2\2\2\u010f\u0110\7-\2\2\u0110\u0086\3\2\2"+
		"\2\u0111\u0112\7/\2\2\u0112\u0088\3\2\2\2\u0113\u0114\7,\2\2\u0114\u008a"+
		"\3\2\2\2\u0115\u0116\7\61\2\2\u0116\u008c\3\2\2\2\3\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}