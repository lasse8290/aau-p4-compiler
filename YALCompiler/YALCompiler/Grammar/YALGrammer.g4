grammar YALGrammer;

program: (
		externalFunctionDeclaration
		| variableDeclaration ';'
		| assignment ';'
		| functionDeclaration
	)* EOF;

externalFunctionDeclaration:
	EXTERNAL '<' STRING '>' ID ':' formalInputParams? formalOutputParams? ';';

functionDeclaration:
	ASYNC? ID ':' formalInputParams? formalOutputParams? statementBlock;

formalInputParams: IN '(' variableDeclaration? ')';
formalOutputParams: OUT '(' variableDeclaration? ')';

statementBlock:
	'{' (blockStatement | singleStatement ';'+)* '}';

blockStatement: ifStatement | whileStatement;

singleStatement:
	variableDeclaration
	| assignment
	| functionCall
	| RETURN;

variableDeclaration:
	variableDeclarationFormat (',' variableDeclarationFormat)*;

variableDeclarationFormat:
	REF variableDeclarationFormat	# ReferenceVariableDeclaration
	| TYPE ID						# SimpleVariableDeclaration;

assignment: simpleAssignment | declarationAssignment;

simpleAssignment:
	identifier (',' identifier)* operator = (
		'='
		| '+='
		| '-='
		| '*='
		| '/='
		| '%='
	) expression							# IdAssignment
	| operator = ('++' | '--') identifier	# IdPreIncrementDecrementAssignment
	| identifier operator = ('++' | '--')	# IdPostIncrementDecrementAssignment;

declarationAssignment: variableDeclaration '=' expression;

expression:
	'!' expression											# Not
	| '~' expression										# BitwiseNot
	| expression operator = ('++' | '--')					# PostIncrementDecrement
	| operator = ('++' | '--') expression				    # PrefixUnary
	| expression operator = ('*' | '/' | '%') expression	# MultiplicationDivisionModulo
	| expression operator = ('+' | '-') expression			# AdditionSubtraction
	| expression operator = ('<<' | '>>') expression		# LeftRightShift
	| expression '&' expression								# BitwiseAnd
	| expression '^' expression								# BitwiseXor
	| expression '|' expression								# BitwiseOr
	| expression operator = (
		'<'
		| '<='
		| '>'
		| '>='
		| '=='
		| '!='
	) expression					# Comparison
	| expression '&&' expression	# And
	| expression '||' expression	# Or
	| simpleAssignment				# VariableAssignment
	| identifier					# Variable
	| functionCall					# FunctionCallExpression
	| '-'? FLOAT					# FloatLiteral
	| '-'? POSITIVE_INT				# IntLiteral
	| POSITIVE_UINT					# UintLiteral
	| STRING						# StringLiteral
	| BOOLEAN						# BooleanLiteral
	| '(' expression ')'			# ParenthesizedExpression
	| '{' expression? '}'			# ArrayLiteral
	| expression (',' expression)+	# ExpressionList;

functionCall: AWAIT? ID '(' expression? ')';

ifStatement:
	'if' '(' expression ')' statementBlock elseIfStatement* elseStatement?;
elseIfStatement: 'else if' '(' expression ')' statementBlock;
elseStatement: 'else' statementBlock;

whileStatement: 'while' '(' expression ')' statementBlock;

identifier:
	ID							    # SimpleIdentifier
	| REF identifier				# ReferenceIdentifier;

fragment LOWERCASE: [a-z];
fragment UPPERCASE: [A-Z];
fragment DIGIT: [0-9];
fragment LETTER: LOWERCASE | UPPERCASE;
fragment DOUBLE_QUOTATION_MARK: '"';
fragment SINGLE_QUOTATION_MARK: '\'';
fragment UINT_SUFFIX: 'u' | 'U';
fragment FLOAT_SUFFIX: 'f' | 'F';

EXTERNAL: 'external';

ASYNC: 'async';
AWAIT: 'await';

RETURN: 'return';

TYPE:
	'int8'
	| 'int16'
	| 'int32'
	| 'int64'
	| 'uint8'
	| 'uint16'
	| 'uint32'
	| 'uint64'
	| 'float32'
	| 'float64'
	| 'string'
	| 'bool';

IN: 'in';
OUT: 'out';

REF: 'ref';

STRING: (
		SINGLE_QUOTATION_MARK ('\\' SINGLE_QUOTATION_MARK | .)*? SINGLE_QUOTATION_MARK
	)
	| (
		DOUBLE_QUOTATION_MARK ('\\' DOUBLE_QUOTATION_MARK | .)*? DOUBLE_QUOTATION_MARK
	);

BOOLEAN: 'true' | 'false';

ID: (LETTER | '_') (LETTER | DIGIT | '_')*;

POSITIVE_INT: DIGIT (DIGIT)*;
POSITIVE_UINT: DIGIT (DIGIT)* UINT_SUFFIX;

FLOAT: DIGIT (DIGIT)* '.' DIGIT (DIGIT)* FLOAT_SUFFIX?;

TIMES: '*';
DIV: '/';
MOD: '%';
PLUS: '+';
MINUS: '-';
LSHIFT: '<<';
RSHIFT: '>>';
INCREMENT: '++';
DECREMENT: '--';
LESS_THAN: '<';
LESS_THAN_OR_EQUAL: '<=';
GREATER_THAN: '>';
GREATER_THAN_OR_EQUAL: '>=';
EQUALS: '==';
NOT_EQUAL: '!=';
EQUAL: '=';
PLUS_EQUAL: '+=';
MINUS_EQUAL: '-=';
MULTIPLY_EQUAL: '*=';
DIVIDE_EQUAL: '/=';
MODULO_EQUAL: '%=';
BITWISE_NOT: '~';

LBRACKET: '[';
RBRACKET: ']';

WHITESPACE: (' ' | '\t')+ -> skip;
NEWLINE: ('\r'? '\n' | '\r')+ -> skip;
COMMENT: '/*' .*? '*/' -> skip;
LINE_COMMENT: '//' ~[\r\n]* -> skip;