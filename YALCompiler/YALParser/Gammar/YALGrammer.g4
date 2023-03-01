grammar YALGrammer;

program: function;

function: ID '{' variableDeclaration* assignment* '}';

variableDeclaration: TYPE ID';'; 

assignment: (variableDeclaration | ID) '=' expression ';';
expression: ID | ID '(' expression (',' expression)* ')';


fragment LOWERCASE: [a-z];
fragment UPPERCASE: [A-Z];
fragment DIGIT: [0-9];

TYPE: 'i32' | 'f32' | 'string' | 'bool';
IN: 'in';

LETTER: LOWERCASE | UPPERCASE;
ID: LETTER (LETTER | DIGIT)*;

WHITESPACE          : (' '|'\t')+ -> skip ;
NEWLINE             : ('\r'? '\n' | '\r')+ -> skip ;