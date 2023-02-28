grammar grammar_example;

program : { function_declaration scope } ;

function_declaration : identifier ":" [ "in"  "("  params  ")" ]  [ "out"  "("  params  ")" ] ;
 
scope : "{" commands "}" ;

commands : { command } ;

command : ( declaration | assignment | declaration_assignment | function_call ) ";"; 

declaration : type identifier ;

assignment : identifier "=" expression | identifier ( increment_operator | decrement_operator );

declaration_assignment : type identifier "=" expression;

expression : expression { operator expression } | identifier | constant | function_call;
            
function_call : identifier "(" call_params")" ;

params : type identifier { "," type identifier };

call_params : expression {"," expression} ;

type : "int32" | "int64" ;

letter : "A" | "B" | "C" | "D" | "E" | "F" | "G"
       | "H" | "I" | "J" | "K" | "L" | "M" | "N"
       | "O" | "P" | "Q" | "R" | "S" | "T" | "U"
       | "V" | "W" | "X" | "Y" | "Z" | "a" | "b"
       | "c" | "d" | "e" | "f" | "g" | "h" | "i"
       | "j" | "k" | "l" | "m" | "n" | "o" | "p"
       | "q" | "r" | "s" | "t" | "u" | "v" | "w"
       | "x" | "y" | "z" ;

digit : "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9" ;

number : [ "-" ] digit { digit };

character: letter | digit | "_" ;
 
identifier: letter { letter | digit | "_" } ;

operator: "+" | "-" | "*" | "/" ;

constant: number ;

increment_operator : "++" ;
decrement_operator : "--" ;
