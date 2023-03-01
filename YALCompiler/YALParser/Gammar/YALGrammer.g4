grammar YALGrammer;

yalg: program EOF;

program: function*;

function: ID '{' (command ';' )* '}';

command: variableDeclaration | assignment | functionCall;

variableDeclaration: TYPE ID ; 

assignment: (variableDeclaration | ID) '=' expression;

expression: baseExpression 
            | '('? baseExpression OPERATOR '('? baseExpression (OPERATOR baseExpression)* ')'? ')'?;

baseExpression: 
            ID 
            | functionCall 
            | NUMBER 
            | '(' baseExpression ')' ;

functionCall: ID '(' (expression (',' expression)*)? ')';


fragment LOWERCASE: [a-z];
fragment UPPERCASE: [A-Z];
fragment DIGIT: [0-9];
fragment LETTER: LOWERCASE | UPPERCASE;

OPERATOR: '+' | '-' | '*' | '/' ;

TYPE: 'i32' | 'f32' | 'string' | 'bool';
IN: 'in';

ID: LETTER (LETTER | DIGIT)*;

NUMBER: DIGIT (DIGIT)*;

WHITESPACE          : (' '|'\t')+ -> skip ;
NEWLINE             : ('\r'? '\n' | '\r')+ -> skip ;