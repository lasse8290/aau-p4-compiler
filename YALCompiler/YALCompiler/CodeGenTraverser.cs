using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using StringTemplating;
using YALCompiler.DataTypes;
using YALCompiler.Helpers;
using Boolean = YALCompiler.DataTypes.Boolean;

namespace YALCompiler;

public class CodeGenTraverser : ASTTraverser
{
    private readonly Template _template = new("program");
    private readonly StringBuilder _declarationsBuilder = new();
    private readonly StringBuilder _includeBuilder = new();

    private readonly Dictionary<string, string> _externalNicknames = new();
    private readonly List<string> _externalLibraries = new();

    public CodeGenTraverser(ASTNode node) : base(node)
    {
    }

    public override void BeginTraverse()
    {
        var stringBuilder = new StringBuilder();

        foreach(var child in _startNode.FunctionTable){
            if(child.Value is ExternalFunction){
                var x = (string)InvokeVisitor(child.Value);
            }
        }

        foreach (var child in _startNode.Children){
            stringBuilder.AppendLine((string)InvokeVisitor(child) + ";");
        }

        _template.SetKeys(new List<Tuple<string, string>>
        {
            new("declarations", _declarationsBuilder.ToString()),
            new("includes", _includeBuilder.ToString()),
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

    internal override object? Visit(ExternalFunction externalFunction){
        if (!_externalLibraries.Contains(externalFunction.LibraryName)) {
            _externalLibraries.Add(externalFunction.LibraryName);
            _includeBuilder.AppendLine($"#include <{externalFunction.LibraryName}>");
        }
        _externalNicknames[externalFunction.Id] = externalFunction.FunctionName;
        return "";
    }
    
    internal override object? Visit(IfStatement ifStatementNode)
    {
        StringBuilder sb = new();

        foreach (ASTNode node in ifStatementNode.Children) {
            sb.Append((string)InvokeVisitor(node));
        }

        return sb.ToString();
    }

    internal override object? Visit(If ifNode)
    {
        // Visit the predicate of the if statement
        var predicateCode = (string)InvokeVisitor(ifNode.Predicate);

        // Visit the children of the if statement
        var childrenBuilder = new StringBuilder();
        foreach (var child in ifNode.Children)
            childrenBuilder.AppendLine((string)InvokeVisitor(child) + ";");

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
        StringBuilder initializedParametersBuilder = new();
        string inputArguments = "void *pvParameters";
        string ugabuga = "COMPILER_PARAMETERS_" + function.Id + " *_COMPILER_PARAMETERS = (COMPILER_PARAMETERS_" + function.Id + "*) pvParameters;";

        if (function.Id == "main")
        {
            function.Id = "setup";
            inputArguments = "";
            ugabuga = "";
        }

        Template inputTemplate = new("parameter_input_struct");
        inputTemplate.SetKeys(new List<Tuple<string, string>>
            {
                new("name", function.Id),
                new("initialized_parameters", string.Concat(function.InputParameters.Select(symbol => $"{symbol.Type.ToCPPType().First()} {symbol.Id};\n")))
            });

        Template outputTemplate = new("parameter_output_struct");
        outputTemplate.SetKeys(new List<Tuple<string, string>>
            {
                new("name", function.Id),
                new("initialized_parameters", string.Concat(function.OutputParameters.Select(symbol => $"{symbol.Type.ToCPPType().First()} {symbol.Id};\n"))),
            });

        _declarationsBuilder
        .AppendLine(inputTemplate.ReplacePlaceholders())
        .AppendLine(outputTemplate.ReplacePlaceholders())
        .AppendLine($"#define COMPILER_PARAMETERS_{function.Id} COMPILER_PARAMETERS<COMPILER_INPUT_STRUCT_{function.Id}, COMPILER_OUTPUT_STRUCT_{function.Id}>");

        foreach (var symbol in function.InputParameters)
            initializedParametersBuilder.AppendLine($"{symbol.Type.ToCPPType().First()} {symbol.Id} = _COMPILER_PARAMETERS->input->{symbol.Id};");

        foreach (var symbol in function.OutputParameters)
            initializedParametersBuilder.AppendLine($"{symbol.Type.ToCPPType().First()} {symbol.Id} = _COMPILER_PARAMETERS->output->{symbol.Id};");

        var bodyBuilder = new StringBuilder();
        foreach (var child in function.Children)
            bodyBuilder.AppendLine($"{InvokeVisitor(child) ?? ""};");

        Template template = new Template("function");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("name", function.Id),
            new("ugabuga", ugabuga),
            new("input_arguments", inputArguments),
            new("initialized_parameters", initializedParametersBuilder.ToString()),
            new("body", bodyBuilder.ToString()),
        });

        return template.ReplacePlaceholders(true);
    }

    internal override object? Visit(CompoundPredicate compoundPredicate)
    {
        var left = (string)InvokeVisitor(compoundPredicate.Left);
        var right = (string)InvokeVisitor(compoundPredicate.Right);

        var op = compoundPredicate.Operator switch
        {
            Operators.PredicateOperator.Equals => "==",
            Operators.PredicateOperator.NotEquals => "!=",
            Operators.PredicateOperator.LessThan => "<",
            Operators.PredicateOperator.GreaterThan => ">",
            Operators.PredicateOperator.LessThanOrEqual => "<=",
            Operators.PredicateOperator.GreaterThanOrEqual => ">=",
            Operators.PredicateOperator.And => "&&",
            _ => throw new InvalidOperationException($"Unknown predicate operator: {compoundPredicate.Operator}")
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
            stringBuilder.AppendLine((string)InvokeVisitor(child) + ";");

        var template = new Template("while");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("predicate", (string)InvokeVisitor(whileLoop.Predicate)),
            new("body", stringBuilder.ToString())
        });

        return template.ReplacePlaceholders();
    }

    internal override object? Visit(SignedFloat signedFloat)
    {
        return signedFloat.ToString();
    }

    internal override object? Visit(ForStatement forStatement)
    {
        string condition = "true";        
        StringBuilder stringBuilder = new();
        foreach (var child in forStatement.Children)
            stringBuilder.AppendLine((string)InvokeVisitor(child) + ";");

        var template = new Template("for_statement");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("condition", condition),
            new("body", stringBuilder.ToString())
        });


        return template.ReplacePlaceholders();
    }

    internal override object? Visit(BinaryAssignment binaryAssignment)
    {
        StringBuilder prefixBuilder = new();
        foreach (var value in binaryAssignment.Values)
            if (value is FunctionCall functionCall)
                prefixBuilder.AppendLine($"COMPILER_OUTPUT_STRUCT_{(functionCall.Function.Id)} output;");

        var op = binaryAssignment.Operator switch
        {
            Operators.AssignmentOperator.Equals => "=",
            Operators.AssignmentOperator.AdditionAssignment => "+=",
            Operators.AssignmentOperator.SubtractionAssignment => "-=",
            Operators.AssignmentOperator.MultiplicationAssignment => "*=",
            Operators.AssignmentOperator.DivisionAssignment => "/=",
            Operators.AssignmentOperator.ModuloAssignment => "%=",
        };

        StringBuilder leftBuilder = new();
        foreach (var target in binaryAssignment.Targets)
            leftBuilder.Append((string)InvokeVisitor(target));

        StringBuilder rightBuilder = new();
        foreach (var value in binaryAssignment.Values)
            rightBuilder.Append($"COMPILER_ASSIGN({(string)InvokeVisitor(value)})");

        StringBuilder assignmentBuilder = new();
        foreach (var target in binaryAssignment.Targets) {
            string name = "";
            switch (target)
            {
                case Identifier identifier:
                    name = identifier.Name;
                    break;
                case VariableDeclaration variableDecleration:
                    name = variableDecleration.Variable.Id;
                    break;
            }

            rightBuilder.Append($"COMPILER_ASSIGN(&{name}, 1);");
        }


        var template = new Template("binary_assignment");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("prefix", prefixBuilder.ToString()),
            new("left", leftBuilder.ToString()),
            new("operator", op),
            new("right", rightBuilder.ToString()),
        });

        return template.ReplacePlaceholders(true);
    }

    internal override object? Visit(VariableDeclaration variableDeclaration)
    {
        var template = new Template("variable_declaration");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("type", variableDeclaration.Variable.Type.ToCPPType().First()),
            new("identifier", variableDeclaration.Variable.Id)
        });
        return template.ReplacePlaceholders();
    }

    internal override object? Visit(Identifier identifier)
    {
        var template = new Template("identifier");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("name", identifier.Name)
        });

        return (identifier.Negated ? "!" : "") + template.ReplacePlaceholders();
    }

    internal override object? Visit(UnaryAssignment unaryAssignment)
    {
        var operand = (string)InvokeVisitor(unaryAssignment.Target);

        var template = new Template("unary_assignment");

        template["operand"] = operand;
        switch (unaryAssignment.Operator)
        {
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
            new("array_name", arrayElementIdentifier.Name),
            new("index", (string)InvokeVisitor(arrayElementIdentifier.Index))
        });

        return (arrayElementIdentifier.Negated ? "!" : "") + template.ReplacePlaceholders();
    }

    internal override object? Visit(FunctionCall functionCall)
    {
        StringBuilder argumentsBuilder = new StringBuilder();
        for (int i = 0; i < functionCall.InputParameters.Count; i++)
        {
            Expression? expression = functionCall.InputParameters[i];
            string potentialComma = (i == functionCall.InputParameters.Count - 1) ? "" : ",";
            argumentsBuilder.Append($"{(string)InvokeVisitor(expression)}{potentialComma}");
        }

        // Suffix hotfix
        string suffix = (functionCall.Function.OutputParameters.Count == 1) ? $".{functionCall.Function.OutputParameters[0].Id}" : "";
    
        Template template = new Template("function_call");
        if (functionCall.Function is ExternalFunction) {
            template = new Template("function_call_external");
            template.SetKeys(new List<Tuple<string, string>>
            {
                new("function", _externalNicknames[functionCall.Function.Id]),
                new("arguments", argumentsBuilder.ToString()),
            });
        } else {
            template.SetKeys(new List<Tuple<string, string>>
            {
                new("function", functionCall.Function.Id),
                new("is_async", functionCall.Function.IsAsync ? "1" : "0"),
                new("is_await", functionCall.Await ? "1" : "0"),
                new("arguments", argumentsBuilder.ToString()),
                new("suffix", suffix),
            });
        }

        return template.ReplacePlaceholders(true);
    }

    internal override object? Visit(CompoundExpression compoundExpression)
    {
        var left = (string)InvokeVisitor(compoundExpression.Left);
        var right = (string)InvokeVisitor(compoundExpression.Right);

        var op = compoundExpression.Operator switch
        {
            Operators.ExpressionOperator.Addition => "+",
            Operators.ExpressionOperator.Subtraction => "-",
            Operators.ExpressionOperator.Multiplication => "*",
            Operators.ExpressionOperator.Division => "/",
            Operators.ExpressionOperator.Modulo => "%",
            _ => throw new InvalidOperationException($"Unknown compound expression operator: {compoundExpression.Operator}")
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

        if (returnStatement.function.Id != "setup")
        {
            StringBuilder outputParametersBuilder = new();
            foreach (var symbol in returnStatement.function.OutputParameters)
                outputParametersBuilder.AppendLine($"_COMPILER_PARAMETERS->output->{symbol.Id} = {symbol.Id};");

            if (returnStatement.function.IsAsync)
                outputParametersBuilder
                .AppendLine("xTaskNotify(_COMPILER_PARAMETERS->taskhandle, 0, eNoAction);")
                .AppendLine("vTaskDelete(NULL);");

            template.SetKeys(new List<Tuple<string, string>>
        {
            new("output_parameters", outputParametersBuilder.ToString())
        });
        }
        return template.ReplacePlaceholders(true);
    }
}