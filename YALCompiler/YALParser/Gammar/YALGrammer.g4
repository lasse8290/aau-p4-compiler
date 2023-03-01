grammar YALGrammer;

yalg: program EOF;

program: function*;

function: ID '{' variableDeclaration* '}';

variableDeclaration: TYPE ID ';'; 

assignment: (variableDeclaration | ID) '=' expression ';';
expression: ID | ID '(' expression (',' expression)* ')';


fragment LOWERCASE: [a-z];
fragment UPPERCASE: [A-Z];
fragment DIGIT: [0-9];

TYPE: 'i32' | 'f32' | 'string' | 'bool';
IN: 'in';

fragment LETTER: LOWERCASE | UPPERCASE;
ID: LETTER (LETTER | DIGIT)+ | LETTER;

WHITESPACE          : (' '|'\t')+ -> skip ;
NEWLINE             : ('\r'? '\n' | '\r')+ -> skip ;