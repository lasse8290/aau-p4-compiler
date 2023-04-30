using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.Diagnostics.Tracing.Parsers.ClrPrivate;
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

    // Helper function to get the variable name from an ASTNode
    string GetVariableName(ASTNode node)
    {
        string localName = node switch
        {
            Identifier identifier                   => identifier.Name,
            VariableDeclaration variableDeclaration => variableDeclaration.Variable.Name,
            _                                       => "Da hell, how is this possible! 🤷‍♂️"
        };
            
        ASTNode testNode = node;
        while (testNode.Parent != null)
        {
            if (testNode.Parent.SymbolTable is not null && testNode.Parent.SymbolTable.ContainsKey(localName))
            {
                if (testNode.Parent is Function function)
                {
                    if(function.InputParameters.Any(x => x.Name == localName))
                        return $"_COMPILER_PARAMETERS->input->{localName}";
                    if(function.OutputParameters.Any(x => x.Name == localName))
                        return $"_COMPILER_PARAMETERS->output->{localName}";
                }
                break;
            }

            testNode = testNode.Parent;
        }

        return localName;
    }
    
    public override void BeginTraverse()
    {
        var stringBuilder = new StringBuilder();

        foreach (var child in _startNode.FunctionTable)
        {
            if (child.Value is ExternalFunction)
            {
                var x = (string)InvokeVisitor(child.Value);
            }
        }

        foreach (var child in _startNode.Children)
        {
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

    internal override object? Visit(ExternalFunction externalFunction)
    {
        if (!_externalLibraries.Contains(externalFunction.LibraryName))
        {
            _externalLibraries.Add(externalFunction.LibraryName);
            _includeBuilder.AppendLine($"#include <{externalFunction.LibraryName}>");
        }
        _externalNicknames[externalFunction.Name] = externalFunction.FunctionName;
        return "";
    }

    internal override object? Visit(IfStatement ifStatementNode)
    {
        StringBuilder sb = new();

        foreach (ASTNode node in ifStatementNode.Children)
        {
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
        string inputArguments = "void *pvParameters";
        string ugabuga = "COMPILER_PARAMETERS_" + function.Name + " *_COMPILER_PARAMETERS = (COMPILER_PARAMETERS_" + function.Name + "*) pvParameters;";

        if (function.Name == "main")
        {
            function.Name = "setup";
            inputArguments = "";
            ugabuga = "";
        }

        Template inputTemplate = new("parameter_input_struct");
        inputTemplate.SetKeys(new List<Tuple<string, string>>
        {
            new("name", function.Name),
            new("initialized_parameters", string.Concat(function.InputParameters.Select(symbol => $"{symbol.Type.ToCPPType().First()} {symbol.Name};\n")))
        });

        Template outputTemplate = new("parameter_output_struct");
        outputTemplate.SetKeys(new List<Tuple<string, string>>
        {
            new("name", function.Name),
            new("initialized_parameters", string.Concat(function.OutputParameters.Select(symbol => $"{symbol.Type.ToCPPType().First()} {symbol.Name};\n"))),
        });

        _declarationsBuilder
        .AppendLine(inputTemplate.ReplacePlaceholders(true))
        .AppendLine(outputTemplate.ReplacePlaceholders(true))
        .AppendLine($"#define COMPILER_PARAMETERS_{function.Name} COMPILER_PARAMETERS<COMPILER_INPUT_STRUCT_{function.Name}, COMPILER_OUTPUT_STRUCT_{function.Name}>");



        var bodyBuilder = new StringBuilder();
        foreach (var child in function.Children)
            bodyBuilder.AppendLine($"{InvokeVisitor(child) ?? ""};");

        Template template = new Template("function");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("name", function.Name),
            new("ugabuga", ugabuga),
            new("input_arguments", inputArguments),
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

    internal override object? Visit(UnsignedInteger uInt)
    {
        return uInt.ToString();
    }

    internal override object? Visit(Integer integer)
    {
        return integer.ToString();
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

    internal override object? Visit(BinaryAssignment binaryAssignment)
    {
        

        // Helper function to generate simple binary assignments
        string GetSimpleBinaryAssignment(string Target, string Value, string Operator)
        {
            var template = new Template("binary_assignment");
            template.SetKeys(new List<Tuple<string, string>>
            {
                new("left", Target),
                new("right", Value),
                new("operator", Operator)
            });
            return template.ReplacePlaceholders(true);
        }
        
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
        
        var declarationBuilder   = new StringBuilder();
        var functionCallsBuilder = new StringBuilder();
        var assignmentsBuilder   = new StringBuilder();

        int assignmentCount = 0;
        for (int i = 0; i < binaryAssignment.Values.Count; i++)
        {
            var target = binaryAssignment.Targets[assignmentCount];
            var value  = binaryAssignment.Values[i];
            
            //If the target is a variable declaration, we need to declare the variable before assigning it.
            if (target is VariableDeclaration variableDeclaration)
                declarationBuilder.AppendLine((string)InvokeVisitor(variableDeclaration));
            
            //If the value is a function call, we need to build the function call and assign the output parameters to the targets
            if (value is FunctionCall functionCall)  {
                functionCallsBuilder.Append($"COMPILER_OUTPUT_STRUCT_{functionCall.Function.Name} struct_{i} = {(string)InvokeVisitor(value)};");
                foreach (var outputParameter in functionCall.Function.OutputParameters){
                    string targetName = GetVariableName(binaryAssignment.Targets[assignmentCount]);
                    assignmentsBuilder.Append(GetSimpleBinaryAssignment(targetName, $"struct_{i}.{outputParameter.Name}", op));
                    assignmentCount++;
                }
            }
            else //If the value is not a function call, we can just assign it to the target
            {
                string targetName = GetVariableName(binaryAssignment.Targets[assignmentCount]);
                assignmentsBuilder.Append(GetSimpleBinaryAssignment(targetName, $"{(string)InvokeVisitor(value)}", op));
                assignmentCount++;
            }
        }
        
        // Set up and populate the template with the generated code
        var template = new Template("binary_assignment_literal");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("functionCalls", functionCallsBuilder.ToString()),
            new("assignments", assignmentsBuilder.ToString()),
            new("declarations", declarationBuilder.ToString())
        });

        return template.ReplacePlaceholders(true);
    }
    
    internal override object? Visit(VariableDeclaration variableDeclaration)
    {
        var template = new Template("variable_declaration");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("type", variableDeclaration.Variable.Type.ToCPPType().First()),
            new("identifier", variableDeclaration.Variable.Name)
        });
        return template.ReplacePlaceholders();
    }

    internal override object? Visit(Identifier identifier)
    {
        var template = new Template("identifier");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("name", GetVariableName(identifier))
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

        if (functionCall.Function is ExternalFunction)
        {
            Template template = new Template("function_call_external");
            template.SetKeys(new List<Tuple<string, string>>
            {
                new("function", _externalNicknames[functionCall.Function.Name]),
                new("arguments", argumentsBuilder.ToString()),
            });
            return template.ReplacePlaceholders(true); 
        }
        else
        {
            Template template = new Template("function_call");
            template.SetKeys(new List<Tuple<string, string>>
            {
                new("function", functionCall.Function.Name),
                new("is_async", functionCall.Function.IsAsync ? "1" : "0"),
                new("is_await", functionCall.Await ? "1" : "0"),
                new("arguments", argumentsBuilder.ToString()),
            });
            return template.ReplacePlaceholders(true); 
        }

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

        if (returnStatement.function.Name != "setup")
        {
            StringBuilder outputParametersBuilder = new();
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