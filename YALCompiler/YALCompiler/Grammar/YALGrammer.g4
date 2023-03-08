grammar YALGrammer;

program: (globalVariableDeclaration | functionDeclaration)* EOF;

globalVariableDeclaration: TYPE ARRAY_DEFINER ID ';';

functionDeclaration: ASYNC? ID ':' formalInputParams? formalOutputParams? statementBlock;

formalInputParams:  IN  '(' variableDeclarationFormat (',' variableDeclarationFormat)* ')';
formalOutputParams: OUT '(' variableDeclarationFormat (',' variableDeclarationFormat)* ')';

statementBlock: '{' ( blockStatement | singleStatement ';' )* '}' ;

blockStatement: ifStatement
                | whileStatement
                | forStatement
                ;
                
singleStatement: variableDeclaration 
                 | enumDeclaration
                 | assignment 
                 | functionCall 
                 | RETURN 
                 ;

variableDeclaration: variableDeclarationFormat                              # SimpleVariableDeclarationFormat
                    /*| STRUCT_OR_UNION ID ID                                 # StructOrUnionVariableDeclaration
                    | STRUCT_OR_UNION ID '{' (variableDeclaration ';')* '}' # StructOrUnionTypeDeclaration*/
                                                           /*# EnumVariableDeclaration*/
                    | tupleDeclaration                                      # TupleVariableDeclaration
                    ;

variableDeclarationFormat: TYPE ARRAY_DEFINER ID    # ArrayDeclaration 
                           | TYPE ID                # SimpleVariableDeclaration
                           ;
                    
enumDeclaration: ENUM ID '{' ((ID (',' ID)*) | (ID '=' POSITIVE_NUMBER (',' ID '=' POSITIVE_NUMBER)*)) '}' ; 

assignment: simpleAssignment
            | declarationAssignment
            | tupleAssignment
            /*| structVariableAssignment*/
            ;

/*structVariableAssignment: ID '.' ID '=' predicate;*/

simpleAssignment: ID '=' predicate      # IdAssignment
                | ID '+=' expression    # IdAdditionAssignment
                | ID '-=' expression    # IdSubtractionAssignment
                | ID '++'               # IdPostIncrement
                | ID '--'               # IdPostDecrement
                | '--' ID               # IdPreDecrement
                | '++' ID               # IdPreIncrement
                ;
            
declarationAssignment: variableDeclaration '=' predicate;

tupleAssignment: tupleId '=' expression;

tupleDeclaration: '(' TYPE ID (',' TYPE ID)* ')' ;

tupleId: '(' ID (',' ID)* ')' ;

expression:  '++' expression                # PreIncrement
            | '--' expression               # PreDecrement
            | expression '++'               # PostIncrement
            | expression '--'               # PostDecrement  
            | expression '*' expression     # Multiplication 
            | expression '/' expression     # Division
            | expression '%' expression     # Modulo
            | expression '+' expression     # Addition
            | expression '-' expression     # Subtraction
            | expression '<<' expression    # LeftShift
            | expression '>>' expression    # RightShift
            | expression '&' expression     # BitwiseAnd
            | expression '^' expression     # BitwiseXor
            | expression '|' expression     # BitwiseOr
            | simpleAssignment              # VariableAssignment
            | ID                            # Variable  
            | AWAIT functionCall            # AsyncFunctionCallExpression
            | functionCall                  # FunctionCallExpression
            | SIGNED_NUMBER                 # NumberLiteral
            | STRING                        # StringLiteral
            | '(' expression ')'            # ParenthesizedExpression
            | '{' (expression (',' expression)*)? '}'  # ArrayLiteral
            ;

functionCall: ID '(' actualInputParams ')';

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

fragment LOWERCASE:             [a-z];
fragment UPPERCASE:             [A-Z];
fragment DIGIT:                 [0-9];
fragment LETTER:                LOWERCASE | UPPERCASE;
fragment DOUBLE_QUOTATION_MARK: '"' ;
fragment SINGLE_QUOTATION_MARK: '\'' ;

ARRAY_DEFINER:      '[' POSITIVE_NUMBER? ']' ;

ASYNC:              'async' ;
AWAIT:              'await' ;

RETURN:             'return' ;

TYPE:               'int8' | 'int16' | 'int32' | 'int64' |
                    'uint8' | 'uint16' | 'uint32' | 'uint64' |
                    'float32' | 'float64' |
                    'char' | 'string' | 'bool' ;
                            
/*STRUCT_OR_UNION:    'struct' 
                    | 'union' ;*/
                    
ENUM:               'enum' ;
        
IN:                 'in';
OUT:                'out';

STRING:             (SINGLE_QUOTATION_MARK ( '\\' SINGLE_QUOTATION_MARK | . )*? SINGLE_QUOTATION_MARK)
                    | (DOUBLE_QUOTATION_MARK ( '\\' DOUBLE_QUOTATION_MARK | . )*? DOUBLE_QUOTATION_MARK) ;

ID:                 LETTER (LETTER | DIGIT)*;


SIGNED_NUMBER:      NEGATIVE_NUMBER | POSITIVE_NUMBER ;
NEGATIVE_NUMBER:    '-' POSITIVE_NUMBER;
POSITIVE_NUMBER:    DIGIT (DIGIT)*;

BOOLEAN:            'true' | 'false';

WHITESPACE          : (' '|'\t')+ -> skip ;
NEWLINE             : ('\r'? '\n' | '\r')+ -> skip ;
COMMENT             : '/*' .*? '*/' -> skip ;
LINE_COMMENT        : '//' ~[\r\n]* -> skip ;