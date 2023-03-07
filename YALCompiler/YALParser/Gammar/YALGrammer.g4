grammar YALGrammer;
 
yalg: program EOF;
 
program: function*;
 
function: ID ':' inputParamsDeclaration? outputParamsDeclaration? '{' (command ';' )* '}';
 
inputParamsDeclaration: IN '(' variableDeclarationFormat (',' variableDeclarationFormat)* ')';
outputParamsDeclaration: OUT '(' variableDeclarationFormat (',' variableDeclarationFormat)* ')';
 
command: variableDeclaration | assignment | functionCall | RETURN ;
 
variableDeclarationFormat: TYPE ARRAY_DEFINER? ID ;
 
variableDeclaration: variableDeclarationFormat 
                    | STRUCT_OR_UNION ID '{' (variableDeclaration ';')* '}'
                    | enumDeclaration
                    | tupleVariableDeclaration; 
 
enumDeclaration: ENUM ID '{' ((ID (',' ID)*) | (ID '=' NUMBER (',' ID '=' NUMBER)*)) '}' ; 
 
assignment: (variableDeclaration | ID | tupleId) '=' expression;
 
tupleVariableDeclaration: '(' TYPE ID (',' TYPE ID)* ')' ;
 
tupleId: '(' ID (',' ID)* ')' ;
 
expression: expression '*' expression |
            expression '/' expression |
            expression '+' expression |
            expression '-' expression |
            ID 
            | functionCall 
            | NUMBER 
            | STRING
            | '(' expression ')' 
            | '{' (expression (',' expression)*)? '}' ;
 
functionCall: ID '(' (expression (',' expression)*)? ')';
 
 
fragment LOWERCASE: [a-z];
fragment UPPERCASE: [A-Z];
fragment DIGIT: [0-9];
fragment LETTER: LOWERCASE | UPPERCASE;
fragment DOUBLE_QUOTATION_MARK: '"' ;
fragment SINGLE_QUOTATION_MARK: '\'' ;
 
ARRAY_DEFINER: '[' NUMBER? ']' ;
 
OPERATOR: '*' | '/' | '+' | '-' ;
 
RETURN: 'return' ;
 
TYPE:   'int8' | 'int16' | 'int32' | 'int64' |
        'uint8' | 'uint16' | 'uint32' | 'uint64' |
        'float32' | 'float64' |
        'char' | 'string' | 'bool' ;
 
STRUCT_OR_UNION: 'struct' | 'union' ;
ENUM: 'enum' ;
 
IN: 'in';
OUT: 'out';
 
STRING: (SINGLE_QUOTATION_MARK ( '\\' SINGLE_QUOTATION_MARK | . )*? SINGLE_QUOTATION_MARK)
        | (DOUBLE_QUOTATION_MARK ( '\\' DOUBLE_QUOTATION_MARK | . )*? DOUBLE_QUOTATION_MARK) ;
 
ID: LETTER (LETTER | DIGIT)*;
 
NUMBER: DIGIT (DIGIT)*;
 
WHITESPACE          : (' '|'\t')+ -> skip ;
NEWLINE             : ('\r'? '\n' | '\r')+ -> skip ;
COMMENT             : '/*' .*? '*/' -> skip ;
LINE_COMMENT        : '//' ~[\r\n]* -> skip ;