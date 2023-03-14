grammar YALGrammer;

program: (externalFunctionDeclaration | globalVariableDeclaration | functionDeclaration)* EOF;

globalVariableDeclaration: TYPE ('[' POSITIVE_NUMBER ']')? ID ';';

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


simpleAssignment: identifier '=' predicate      # IdAssignment
                | identifier '+=' expression    # IdAdditionAssignment
                | identifier '-=' expression    # IdSubtractionAssignment
                | identifier '*=' expression    # IdMultiplicationAssignment
                | identifier '\\=' expression   # IdDivisionAssignment
                | identifier '%=' expression    # IdModuloAssignment
                | identifier '++'               # IdPostIncrementAssignment
                | identifier '--'               # IdPostDecrementAssignment
                | '--' identifier               # IdPreDecrementAssignment
                | '++' identifier               # IdPreIncrementAssignment
                ;
            
declarationAssignment: variableDeclaration '=' predicate;

tupleAssignment: tupleDeclaration '=' expression;

tupleDeclaration: '(' variableDeclarationFormat (',' variableDeclarationFormat)* ')' ;

expression:   expression '++'               # PostIncrement
            | expression '--'               # PostDecrement 
            | '++' expression               # PreIncrement 
            | '--' expression               # PreDecrement
            | '~' expression                # BitwiseUnaryNot
            | expression operator=('*' | '/' | '%') expression     # MultiplicationDivisionModulo 
            //| expression '/' expression     # Division
            //| expression '%' expression     # Modulo
            | expression operator=('+' | '-') expression     # AdditionSubtraction
            //| expression '-' expression     # Subtraction
            | expression operator=('<<' | '>>') expression    # LeftRightShift
            //| expression '>>' expression    # RightShift
            | expression '&' expression     # BitwiseAnd
            | expression '^' expression     # BitwiseXor
            | expression '|' expression     # BitwiseOr
            | expression '~' expression     # BitwiseNot
            | simpleAssignment              # VariableAssignment
            | identifier                    # Variable  
            | functionCall                  # FunctionCallExpression
            | POSITIVE_NUMBER                 # PositiveNumberLiteral
            | NEGATIVE_NUMBER                 # NegativeNumberLiteral
            | STRING                        # StringLiteral
            | '(' expression ')'            # ParenthesizedExpression
            | '{' (expression (',' expression)*)? '}'  # ArrayLiteral
            ;

functionCall: AWAIT? ID '(' actualInputParams ')';

actualInputParams: (expression (',' expression)*)? ;

predicate:  '!' predicate                  # Not
            | predicate '&&' predicate     # And
            | predicate '||' predicate     # Or
            | predicate '<' predicate      # LessThan
            | predicate '<=' predicate     # LessThanOrEqual
            | predicate '>' predicate      # GreaterThan
            | predicate '>=' predicate     # GreaterThanOrEqual
            | predicate '==' predicate     # Equals
            | predicate '!=' predicate     # NotEquals
            | '(' predicate ')'            # ParenthesizedPredicate
            | BOOLEAN                      # BooleanLiteral
            | expression                   # ExpressionPredicate
            ;
            
ifStatement:        'if' '(' predicate ')' statementBlock elseIfStatement* elseStatement? ;
elseIfStatement:    'else if' '(' predicate ')' statementBlock ;
elseStatement:      'else' statementBlock ;

whileStatement: 'while' '(' predicate ')' statementBlock;

forStatement: 'for' '(' declarationAssignment ';' predicate ';' assignment ')' statementBlock;

identifier:  ID '[' expression ']'  # ArrayElementIdentifier
            | ID                    # SimpleIdentifier
            ;

fragment LOWERCASE:             [a-z];
fragment UPPERCASE:             [A-Z];
fragment DIGIT:                 [0-9];
fragment LETTER:                LOWERCASE | UPPERCASE;
fragment DOUBLE_QUOTATION_MARK: '"' ;
fragment SINGLE_QUOTATION_MARK: '\'' ;


EXTERNAL:           'external' ;

ASYNC:              'async' ;
AWAIT:              'await' ;

RETURN:             'return' ;

TYPE:               'int8' | 'int16' | 'int32' | 'int64' |
                    'uint8' | 'uint16' | 'uint32' | 'uint64' |
                    'float32' | 'float64' |
                    'char' | 'string' | 'bool' ;
        
IN:                 'in';
OUT:                'out';

STRING:               (SINGLE_QUOTATION_MARK ( '\\' SINGLE_QUOTATION_MARK | . )*? SINGLE_QUOTATION_MARK)
                    | (DOUBLE_QUOTATION_MARK ( '\\' DOUBLE_QUOTATION_MARK | . )*? DOUBLE_QUOTATION_MARK) ;

ID:                 (LETTER | '_') (LETTER | DIGIT | '_')*;

NEGATIVE_NUMBER:    '-' POSITIVE_NUMBER;
POSITIVE_NUMBER:    DIGIT (DIGIT)*;

BOOLEAN:            'true' | 'false';

TIMES:             '*' ;
DIV:               '/' ;
MOD:               '%' ;
PLUS:              '+' ;
MINUS:             '-' ;
LSHIFT:            '<<' ;
RSHIFT:            '>>' ;

WHITESPACE          : (' '|'\t')+ -> skip ;
NEWLINE             : ('\r'? '\n' | '\r')+ -> skip ;
COMMENT             : '/*' .*? '*/' -> skip ;
LINE_COMMENT        : '//' ~[\r\n]* -> skip ;