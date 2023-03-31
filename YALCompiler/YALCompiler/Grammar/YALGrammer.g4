grammar YALGrammer;

program: (externalFunctionDeclaration | globalVariableDeclaration | functionDeclaration)* EOF;

globalVariableDeclaration: TYPE ('[' POSITIVE_NUMBER? ']')? ID ('=' expression)? ';';

externalFunctionDeclaration: EXTERNAL '<' STRING '>' ID ':' formalInputParams? formalOutputParams? ';';

functionDeclaration: ASYNC? ID ':' formalInputParams? formalOutputParams? statementBlock;

formalInputParams:  IN  '(' variableDeclarationFormat (',' variableDeclarationFormat)* ')';
formalOutputParams: OUT '(' variableDeclarationFormat (',' variableDeclarationFormat)* ')';

statementBlock: '{' ( blockStatement | singleStatement ';'+ )* '}' ;

blockStatement: ifStatement
                | whileStatement
                | forStatement
                ;
                
singleStatement: variableDeclaration 
                 | assignment 
                 | functionCall 
                 | RETURN 
                 ;

variableDeclaration: variableDeclarationFormat  # SimpleVariableDeclarationFormat
                    | tupleDeclaration          # TupleVariableDeclaration
                    ;

variableDeclarationFormat: TYPE '[' POSITIVE_NUMBER? ']' ID     # ArrayDeclaration 
                           | TYPE ID                            # SimpleVariableDeclaration
                           ;
                    
assignment: simpleAssignment
            | declarationAssignment
            | tupleAssignment
            ;


simpleAssignment: identifier operator=('=' | '+=' | '-=' | '*=' | '\\=' | '%=') expression      # IdAssignment
                | operator=('++' | '--') identifier                                             # IdPreIncrementDecrementAssignment
                | identifier operator=('++' | '--')                                             # IdPostIncrementDecrementAssignment
                ;
            
declarationAssignment:  variableDeclaration '=' expression;

tupleAssignment:        tupleDeclaration '=' expression;

tupleDeclaration:       '(' variableDeclarationFormat (',' variableDeclarationFormat)* ')' ;

expression: '!' expression                                      # Not
            | expression operator=( '++' | '--' )               # PostIncrementDecrement
            | operator=( '++' | '--' | '~' ) expression         # PrefixUnary 
            | expression operator=('*' | '/' | '%') expression  # MultiplicationDivisionModulo 
            | expression operator=('+' | '-') expression        # AdditionSubtraction
            | expression operator=('<<' | '>>') expression      # LeftRightShift
            | expression '&' expression                         # BitwiseAnd
            | expression '^' expression                         # BitwiseXor
            | expression '|' expression                         # BitwiseOr
            | expression '~' expression                         # BitwiseNot
            | expression operator=('<' | '<=' | '>' | '>=' | '==' | '!=') expression  # Comparison
            | expression '&&' expression                        # And
            | expression '||' expression                        # Or
            | simpleAssignment                                  # VariableAssignment
            | identifier                                        # Variable  
            | functionCall                                      # FunctionCallExpression
            | '-'? FLOAT                                        # FloatLiteral
            | '-'? POSITIVE_NUMBER                              # NumberLiteral
            | STRING                                            # StringLiteral
            | BOOLEAN                                           # BooleanLiteral
            | '(' expression ')'                                # ParenthesizedExpression
            | '{' (expression (',' expression)*)? '}'           # ArrayLiteral
            ;

functionCall:       AWAIT? ID '(' actualInputParams ')';

actualInputParams:  (expression (',' expression)*)? ;
            
ifStatement:        'if' '(' expression ')' statementBlock elseIfStatement* elseStatement? ;
elseIfStatement:    'else if' '(' expression ')' statementBlock ;
elseStatement:      'else' statementBlock ;

whileStatement:     'while' '(' expression ')' statementBlock;

forStatement:       'for' '(' declarationAssignment ';' expression ';' assignment ')' statementBlock;

identifier:  ID '[' expression ']'  # ArrayElementIdentifier
            | ID                    # SimpleIdentifier
            | '(' identifier ')'    # ParenthesizedIdentifier
            ;

fragment LOWERCASE:             [a-z];
fragment UPPERCASE:             [A-Z];
fragment DIGIT:                 [0-9];
fragment LETTER:                LOWERCASE | UPPERCASE;
fragment DOUBLE_QUOTATION_MARK: '"' ;
fragment SINGLE_QUOTATION_MARK: '\'' ;


EXTERNAL:               'external' ;
    
ASYNC:                  'async' ;
AWAIT:                  'await' ;
    
RETURN:                 'return' ;
    
TYPE:                   'int8' | 'int16' | 'int32' | 'int64' |
                        'uint8' | 'uint16' | 'uint32' | 'uint64' |
                        'float32' | 'float64' |
                        'char' | 'string' | 'bool' ;
            
IN:                     'in';
OUT:                    'out';
    
STRING:                   (SINGLE_QUOTATION_MARK ( '\\' SINGLE_QUOTATION_MARK | . )*? SINGLE_QUOTATION_MARK)
                        | (DOUBLE_QUOTATION_MARK ( '\\' DOUBLE_QUOTATION_MARK | . )*? DOUBLE_QUOTATION_MARK) ;
    
BOOLEAN:                'true' | 'false';

ID:                     (LETTER | '_') (LETTER | DIGIT | '_')*;
    
POSITIVE_NUMBER:        DIGIT (DIGIT)*;
    
FLOAT:                  DIGIT (DIGIT)* '.' DIGIT (DIGIT)*;


TIMES:                  '*' ;
DIV:                    '/' ;
MOD:                    '%' ;
PLUS:                   '+' ;
MINUS:                  '-' ;
LSHIFT:                 '<<' ;
RSHIFT:                 '>>' ;
INCREMENT:              '++' ;
DECREMENT:              '--' ;
LESS_THAN:              '<' ;
LESS_THAN_OR_EQUAL:     '<=' ;
GREATER_THAN:           '>' ;
GREATER_THAN_OR_EQUAL:  '>=' ;
EQUALS:                 '==' ;
NOT_EQUAL:              '!=' ;
EQUAL:                  '=' ;
PLUS_EQUAL:             '+=' ;
MINUS_EQUAL:            '-=' ;
MULTIPLY_EQUAL:         '*=' ;
DIVIDE_EQUAL:           '\\=' ;
MODULO_EQUAL:           '%=' ;
BITWISE_NOT:            '~' ;

LBRACKET:               '[' ;
RBRACKET:               ']' ;


WHITESPACE              : (' '|'\t')+ -> skip ;
NEWLINE                 : ('\r'? '\n' | '\r')+ -> skip ;
COMMENT                 : '/*' .*? '*/' -> skip ;
LINE_COMMENT            : '//' ~[\r\n]* -> skip ;