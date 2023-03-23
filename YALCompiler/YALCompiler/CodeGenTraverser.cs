using System.Text;
using StringTemplating;
using YALCompiler.DataTypes;
using YALCompiler.Helpers;
using Boolean = YALCompiler.DataTypes.Boolean;

namespace YALCompiler;

public class CodeGenTraverser : ASTTraverser
{
    private readonly Template _template = new("program");

    public CodeGenTraverser(ASTNode node) : base(node)
    {
    }
    
    public override void BeginTraverse()
    {
        var stringBuilder = new StringBuilder();
        foreach (var child in _startNode.Children)
            stringBuilder.AppendLine((string)InvokeVisitor(child));

        _template.SetKeys(new List<Tuple<string, string>>
        {
            new("program", stringBuilder.ToString())
        });
    }
    
    public string GetGeneratedCode()
    {
        return _template.ReplacePlaceholders();
    }

    internal override object? Visit(Boolean boolean)
    {
        var template = new Template("boolean");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("boolean", boolean.LiteralValue == true ? "1" : "0")
        });

        return (boolean.Negated ? "!" : "") + template.ReplacePlaceholders();
    }

    internal override object? Visit(IfStatement ifStatementNode)
    {
        StringBuilder sb = new();

        foreach (ASTNode node in ifStatementNode.Children)
            sb.Append((string)InvokeVisitor(node));

        return sb.ToString();
    }
    
    internal override object? Visit(If ifNode)
    {
        // Visit the predicate of the if statement
        var predicateCode = (string)InvokeVisitor(ifNode.Predicate);

        // Visit the children of the if statement
        var childrenBuilder = new StringBuilder();
        foreach (var child in ifNode.Children) childrenBuilder.AppendLine((string)InvokeVisitor(child));

        // Generate the if statement code using a template
        var template = new Template("if");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("predicate", predicateCode),
            new("body", childrenBuilder.ToString())
        });

        return template.ReplacePlaceholders();
    }

    internal override object? Visit(ElseIf elseIfNode)
    {
        // Visit the Predicate of the ElseIf node
        var predicateCode = (string)InvokeVisitor(elseIfNode.Predicate);

        // Visit the children of the ElseIf node
        StringBuilder body = new();
        foreach (var child in elseIfNode.Children) body.AppendLine((string)InvokeVisitor(child));


        var template = new Template("else_if");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("predicate", predicateCode),
            new("body", body.ToString())
        });

        return template.ReplacePlaceholders();

    }

    internal override object? Visit(Else elseNode)
    {
        StringBuilder body = new();
        foreach (var child in elseNode.Children) body.AppendLine((string)InvokeVisitor(child));

        var template = new Template("else");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("body", body.ToString())
        });

        return template.ReplacePlaceholders();
    }

    internal override object? Visit(Function function)
    {
        var stringBuilder = new StringBuilder();
        foreach (var child in function.Children)
        {
            stringBuilder.Append((string)InvokeVisitor(child));
            stringBuilder.AppendLine(";");
        }

        var template = new Template("function");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("type", "void"),
            new("name", function.Id),
            new("body", stringBuilder.ToString())
        });

        return template.ReplacePlaceholders();
    }

    internal override object? Visit(CompoundPredicate compoundPredicate)
    {
        var left  = (string)InvokeVisitor(compoundPredicate.Left);
        var right = (string)InvokeVisitor(compoundPredicate.Right);

        var op = compoundPredicate.Operator switch
        {
            Operators.PredicateOperator.Equals             => "==",
            Operators.PredicateOperator.NotEquals          => "!=",
            Operators.PredicateOperator.LessThan           => "<",
            Operators.PredicateOperator.GreaterThan        => ">",
            Operators.PredicateOperator.LessThanOrEqual    => "<=",
            Operators.PredicateOperator.GreaterThanOrEqual => ">=",
            Operators.PredicateOperator.And                => "&&",
            _                                              => throw new InvalidOperationException($"Unknown predicate operator: {compoundPredicate.Operator}")
        };

        var template = new Template("compound_predicate");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("left", left),
            new("operator", op),
            new("right", right)
        });

        return (compoundPredicate.Negated ? "!" : "") + template.ReplacePlaceholders();
    }

    internal override object? Visit(SignedNumber signedNumber)
    {
        var template = new Template("signed_number");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("signed_number", (signedNumber.Negative ? "-" : "") + signedNumber.Value)
        });

        return template.ReplacePlaceholders();
    }

    internal override object? Visit(WhileStatement whileLoop)
    {
        var stringBuilder = new StringBuilder();
        foreach (var child in whileLoop.Children)
        {
            stringBuilder.Append((string)InvokeVisitor(child));
            stringBuilder.AppendLine(";");
        }

        var template = new Template("while");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("predicate", (string)InvokeVisitor(whileLoop.Predicate)),
            new("body", stringBuilder.ToString())
        });

        return template.ReplacePlaceholders();
    }

    internal override object? Visit(ForStatement forStatement)
    {
        var declarationAssignment = (string)InvokeVisitor(forStatement.DeclarationAssignment);
        var runCondition          = (string)InvokeVisitor(forStatement.RunCondition);
        var loopAssignment        = (string)InvokeVisitor(forStatement.LoopAssignment);
        var stringBuilder         = new StringBuilder();
        foreach (var child in forStatement.Children)
        {
            stringBuilder.Append((string)InvokeVisitor(child));
            stringBuilder.AppendLine(";");
        }


        var template = new Template("for_statement");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("declaration_assignment", declarationAssignment),
            new("run_condition", runCondition),
            new("loop_assignment", loopAssignment),
            new("body", stringBuilder.ToString())
        });


        return template.ReplacePlaceholders();
    }

    internal override object? Visit(BinaryAssignment binaryAssignment)
    {
        var op = binaryAssignment.Operator switch
        {
            Operators.AssignmentOperator.Equals                   => "=",
            Operators.AssignmentOperator.AdditionAssignment       => "+=",
            Operators.AssignmentOperator.SubtractionAssignment    => "-=",
            Operators.AssignmentOperator.MultiplicationAssignment => "*=",
            Operators.AssignmentOperator.DivisionAssignment       => "/=",
            Operators.AssignmentOperator.ModuloAssignment         => "%=",
            _                                                     => throw new InvalidOperationException($"Unknown assignment operator: {binaryAssignment.Operator}")
        };

        var template = new Template("binary_assignment");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("left", (string)InvokeVisitor(binaryAssignment.Target)),
            new("right", (string)InvokeVisitor(binaryAssignment.Value)),
            new("op", op)
        });

        return template.ReplacePlaceholders();
    }

    internal override object? Visit(VariableDeclaration variableDeclaration)
    {
        var template = new Template("variable_declaration");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("type", variableDeclaration.Variable.Type.ToCPPType()),
            new("identifier", variableDeclaration.Variable.Id)
        });
        return template.ReplacePlaceholders();
    }

    internal override object? Visit(Identifier identifier)
    {
        var template = new Template("identifier");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("name", identifier.IdValue)
        });

        return (identifier.Negated ? "!" : "") + template.ReplacePlaceholders();
    }

    internal override object? Visit(UnaryAssignment unaryAssignment)
    {
        var operand = (string)InvokeVisitor(unaryAssignment.Target);
        
        var template = new Template("unary_assignment");

        template["operand"] = operand;
        switch(unaryAssignment.Operator){
            case Operators.AssignmentOperator.PreIncrement:
                template["pre_operator"] = "++";
                break;
            case Operators.AssignmentOperator.PreDecrement:
                template["post_operator"] = "--";
                break;
            case Operators.AssignmentOperator.PostIncrement:
                template["post_operator"] = "++";
                break;
            case Operators.AssignmentOperator.PostDecrement:
                template["post_operator"] = "--";
                break;
                
            default: throw new InvalidOperationException($"Unknown unary operator: {unaryAssignment.Operator}");
        }

        return template.ReplacePlaceholders(true);
    }

    internal override object? Visit(ArrayLiteral arrayLiteral)
    {
        var elementsBuilder = new StringBuilder();
        for (var i = 0; i < arrayLiteral.Values.Count; i++)
        {
            elementsBuilder.Append((string)InvokeVisitor(arrayLiteral.Values[i]));
            if (i < arrayLiteral.Values.Count - 1) elementsBuilder.Append(", ");
        }

        var template = new Template("array_literal");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("elements", elementsBuilder.ToString())
        });

        return template.ReplacePlaceholders();
    }

    internal override object? Visit(ArrayElementIdentifier arrayElementIdentifier)
    {
        var template = new Template("array_element_identifier");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("array_name", arrayElementIdentifier.IdValue),
            new("index", (string)InvokeVisitor(arrayElementIdentifier.Index))
        });

        return (arrayElementIdentifier.Negated ? "!" : "") + template.ReplacePlaceholders();
    }

    internal override object? Visit(FunctionCall functionCall)
    {
        var functionName = functionCall.Identifier;
        var argumentList = new StringBuilder();
        for (var i = 0; i < functionCall.InputParameters.Count; i++)
        {
            argumentList.Append((string)InvokeVisitor(functionCall.InputParameters[i]));
            if (i < functionCall.InputParameters.Count - 1) argumentList.Append(", ");
        }

        var template = new Template("function_call");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("function_name", functionName),
            new("arguments", argumentList.ToString())
        });

        return template.ReplacePlaceholders();
    }

    internal override object? Visit(CompoundExpression compoundExpression)
    {
        var left  = (string)InvokeVisitor(compoundExpression.Left);
        var right = (string)InvokeVisitor(compoundExpression.Right);

        var op = compoundExpression.Operator switch
        {
            Operators.ExpressionOperator.Addition       => "+",
            Operators.ExpressionOperator.Subtraction    => "-",
            Operators.ExpressionOperator.Multiplication => "*",
            Operators.ExpressionOperator.Division       => "/",
            Operators.ExpressionOperator.Modulo         => "%",
            _                                           => throw new InvalidOperationException($"Unknown compound expression operator: {compoundExpression.Operator}")
        };

        var template = new Template("compound_expression");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("left", left),
            new("operator", op),
            new("right", right)
        });

        return template.ReplacePlaceholders();
    }

    internal override object? Visit(StringLiteral stringLiteral)
    {
        var template = new Template("string_literal");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("string_value", stringLiteral.Value)
        });

        return template.ReplacePlaceholders();
    }

    internal override object? Visit(ReturnStatement returnStatement)
    {
        var template = new Template("return_statement");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("return_statement", "return")
        });

        return template.ReplacePlaceholders();
    }
}