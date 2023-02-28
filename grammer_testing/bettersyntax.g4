grammar bettersyntax;

program : { function_declaration scope };

function_declaration: identifier;

scope: '{' { commands } '}' ;

commands: identifier ;

letter : 'A' | 'B' | 'C' | 'D' | 'E' | 'F' | 'G'
       | 'H' | 'I' | 'J' | 'K' | 'L' | 'M' | 'N'
       | 'O' | 'P' | 'Q' | 'R' | 'S' | 'T' | 'U'
       | 'V' | 'W' | 'X' | 'Y' | 'Z' | 'a' | 'b'
       | 'c' | 'd' | 'e' | 'f' | 'g' | 'h' | 'i'
       | 'j' | 'k' | 'l' | 'm' | 'n' | 'o' | 'p'
       | 'q' | 'r' | 's' | 't' | 'u' | 'v' | 'w'
       | 'x' | 'y' | 'z' ;

digit : '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9' ;

// number :  [ '-' ] digit { digit } ;

character: letter | digit | '_' ;
 
identifier: letter { letter | digit | '_' } ;

operator: '+' | '-' | '*' | '/' ;