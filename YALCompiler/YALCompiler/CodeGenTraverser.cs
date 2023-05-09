using System.Text;
using StringTemplating;
using YALCompiler.DataTypes;
using YALCompiler.Helpers;
using Boolean = YALCompiler.DataTypes.Boolean;

namespace YALCompiler;

public class CodeGenTraverser : ASTTraverser
{
    private readonly StringBuilder _declarationsBuilder = new();
    private readonly HashSet<string>  _externalLibraries   = new();
    private readonly Dictionary<string, string> _externalNicknames = new();
    private readonly Template                   _template          = new("program");
    private          StringBuilder              _scopeBuilder      = new();

    public CodeGenTraverser(ASTNode node) : base(node)
    {
    }

    // Helper function to get the variable name from an ASTNode
    private string GetVariableName(ASTNode node)
    {
        var localName = node switch
        {
            Identifier identifier                   => identifier.Name,
            VariableDeclaration variableDeclaration => variableDeclaration.Variable.Name,
            _                                       => "Da hell, how is this possible! 🤷‍♂️"
        };


        var testNode = node;

        while (testNode.Parent != null)
        {
            if (testNode.Parent.SymbolTable is not null && testNode.Parent.SymbolTable.ContainsKey(localName))
            {
                if (testNode.Parent is Function function)
                {
                    var symbol = function.InputParameters.FirstOrDefault(x => x.Name == localName);
                    if (symbol != null) return $"{(symbol.IsRef ? "*" : "")}(((COMPILER_PARAMETERS_{function.Name}*) pvParameters)->input.{localName})";

                    symbol = function.OutputParameters.FirstOrDefault(x => x.Name == localName);
                    if (symbol != null) return $"((COMPILER_PARAMETERS_{function.Name}*) pvParameters)->output->{localName}";
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

        // Invoke external functions
        foreach (var child in _startNode.FunctionTable)
            if (child.Value is ExternalFunction)
                InvokeVisitor(child.Value);
        
        // Visit children
        foreach (var child in _startNode.Children)
            stringBuilder.AppendLine((string)InvokeVisitor(child) + ";");

        // Build includes from external libraries
        StringBuilder _includeBuilder = new();
        foreach (string libraryName in _externalLibraries)
            _includeBuilder.AppendLine($"#include <{libraryName}>");

        _template.SetKeys(new List<Tuple<string, string>>
        {
            new("declarations", _declarationsBuilder.ToString()),
            new("includes", _includeBuilder.ToString()),
            new("program", stringBuilder.ToString())
        });
    }

    public override string ToString()
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
        _externalLibraries.Add(externalFunction.LibraryName);
        _externalNicknames[externalFunction.Name] = externalFunction.FunctionName;
        return "";
    }

    internal override object? Visit(IfStatement ifStatementNode)
    {
        StringBuilder sb = new();

        foreach (var node in ifStatementNode.Children) sb.Append((string)InvokeVisitor(node));

        return sb.ToString();
    }

    internal override object? Visit(If ifNode)
    {
        // Visit the predicate of the if statement
        var predicateCode = (string)InvokeVisitor(ifNode.Predicate);

        StringBuilder scopeBuilder = new();
        _scopeBuilder = scopeBuilder;

        // Visit the children of the if statement
        var childrenBuilder = new StringBuilder();
        foreach (var child in ifNode.Children)
            childrenBuilder.AppendLine((string)InvokeVisitor(child) + ";");

        // Generate the if statement code using a template
        var template = new Template("if");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("predicate", predicateCode),
            new("body", $"{scopeBuilder} {childrenBuilder}")
        });

        return template.ReplacePlaceholders(true);
    }

    internal override object? Visit(ElseIf elseIfNode)
    {
        StringBuilder scopeBuilder = new();
        _scopeBuilder = scopeBuilder;

        // Visit the Predicate of the ElseIf node
        var predicateCode = (string)InvokeVisitor(elseIfNode.Predicate);

        // Visit the children of the ElseIf node
        StringBuilder body = new();
        foreach (var child in elseIfNode.Children) body.AppendLine((string)InvokeVisitor(child));


        var template = new Template("else_if");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("predicate", predicateCode),
            new("body", $"{scopeBuilder} {body}")
        });

        return template.ReplacePlaceholders(true);
    }

    internal override object? Visit(Else elseNode)
    {
        StringBuilder scopeBuilder = new();
        _scopeBuilder = scopeBuilder;

        StringBuilder bodyBuilder = new();
        foreach (var child in elseNode.Children) bodyBuilder.AppendLine((string)InvokeVisitor(child));

        var template = new Template("else");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("body", $"{scopeBuilder} {bodyBuilder}")
        });

        return template.ReplacePlaceholders(true);
    }

    internal override object? Visit(Function function)
    {
        StringBuilder scopeBuilder = new();
        _scopeBuilder = scopeBuilder;

        var inputArguments = "void *pvParameters";

        if (function.Name == "main")
        {
            function.Name  = "setup";
            inputArguments = "";
        }

        Template inputTemplate = new("parameter_input_struct");
        inputTemplate.SetKeys(new List<Tuple<string, string>>
        {
            new("name", function.Name),
            new("initialized_parameters", string.Concat(function.InputParameters.Select(symbol => $"{symbol.Type.ToCPPType().First()}{(symbol.IsRef ? "*" : "")} {symbol.Name};\n")))
        });

        Template outputTemplate = new("parameter_output_struct");
        outputTemplate.SetKeys(new List<Tuple<string, string>>
        {
            new("name", function.Name),
            new("initialized_parameters", string.Concat(function.OutputParameters.Select(symbol => $"{symbol.Type.ToCPPType().First()}{(symbol.IsRef ? "*" : "")} {symbol.Name};\n")))
        });

        _declarationsBuilder
            .AppendLine(inputTemplate.ReplacePlaceholders(true))
            .AppendLine(outputTemplate.ReplacePlaceholders(true))
            .AppendLine($"#define COMPILER_PARAMETERS_{function.Name} COMPILER_PARAMETERS<COMPILER_INPUT_STRUCT_{function.Name}, COMPILER_OUTPUT_STRUCT_{function.Name}>");

        var bodyBuilder = new StringBuilder();
        foreach (var child in function.Children)
            bodyBuilder.AppendLine($"{InvokeVisitor(child) ?? ""};");

        var template = new Template("function");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("name", function.Name),
            new("input_arguments", inputArguments),
            new("body", $"{scopeBuilder} {bodyBuilder}")
        });

        return template.ReplacePlaceholders(true);
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
        StringBuilder scopeBuilder = new();
        _scopeBuilder = scopeBuilder;

        var stringBuilder = new StringBuilder();
        foreach (var child in whileLoop.Children)
            stringBuilder.AppendLine((string)InvokeVisitor(child) + ";");

        var template = new Template("while");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("predicate", (string)InvokeVisitor(whileLoop.Predicate)),
            new("body", stringBuilder.ToString())
        });

        return $"{scopeBuilder} {template.ReplacePlaceholders(true)}";
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

        if (binaryAssignment.Targets.Count == 1)
            return GetSimpleBinaryAssignment((string)InvokeVisitor(binaryAssignment.Targets[0]), (string)InvokeVisitor(binaryAssignment.Values[0]), op);

        var functionCallsBuilder = new StringBuilder();
        var assignmentsBuilder   = new StringBuilder();

        var assignmentCount = 0;
        for (var i = 0; i < binaryAssignment.Values.Count; i++)
        {
            var value = binaryAssignment.Values[i];

            if (value is FunctionCall functionCall)
            {
                functionCallsBuilder.Append($"{(string)InvokeVisitor(value)};");
                foreach (var outputParameter in functionCall.Function.OutputParameters)
                {
                    var targetName = (string)InvokeVisitor(binaryAssignment.Targets[assignmentCount]);
                    assignmentsBuilder.Append(GetSimpleBinaryAssignment(targetName, $"_{functionCall.GetHashCode().ToString()}.{outputParameter.Name}", op));
                    //HOTFIX
                    if (binaryAssignment.Targets.Count > 1)
                        assignmentsBuilder.Append(";");

                    assignmentCount++;
                }
            }
            else
            {
                var targetName = (string)InvokeVisitor(binaryAssignment.Targets[assignmentCount]);
                assignmentsBuilder.Append(GetSimpleBinaryAssignment(targetName, $"{(string)InvokeVisitor(value)}", op));
                assignmentCount++;
                //HOTFIX
                if (binaryAssignment.Targets.Count > 1)
                    assignmentsBuilder.Append(";");
            }
        }

        // Set up and populate the template with the generated code
        var template = new Template("binary_assignment_literal");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("functionCalls", functionCallsBuilder.ToString()),
            new("assignments", assignmentsBuilder.ToString())
        });

        return template.ReplacePlaceholders(true);
    }

    internal override object? Visit(VariableDeclaration variableDeclaration)
    {
        var arrayLength = variableDeclaration.Variable.ArraySize;
        var isArray     = variableDeclaration.Variable.Type.Types.First().IsArray;

        var template = new Template("variable_declaration");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("array", isArray ? $"[{(arrayLength == null ? "" : arrayLength)}]" : ""),
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
            new("name", $"{(identifier.IsRef ? "&" : "")}{GetVariableName(identifier)}")
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
        var argumentCounter = 0;
        string GetInputSeparator()
        {
            argumentCounter++;
            return argumentCounter == functionCall.Function.InputParameters.Count ? "" : ",";
        }

        var suffix = functionCall.Function.OutputParameters.Count == 1 ? $"->{functionCall.Function.OutputParameters[0].Name}" : "";

        var functionCallBuilder    = new StringBuilder();
        var inputParametersBuilder = new StringBuilder();

        foreach (var expression in functionCall.InputParameters)
            if (expression is FunctionCall inputFunctionCall)
            {
                functionCallBuilder.Append($"{(string)InvokeVisitor(inputFunctionCall)};");
                for (var y = 0; y < inputFunctionCall.Function.OutputParameters.Count; y++)
                    inputParametersBuilder.Append($"_{inputFunctionCall.GetHashCode().ToString()}.{inputFunctionCall.Function.OutputParameters[y].Name}{GetInputSeparator()}");
            }
            else
            {
                inputParametersBuilder.Append($"{(string)InvokeVisitor(expression)}{GetInputSeparator()}");
            }
        

        if (functionCall.Function is ExternalFunction)
        {
            var template = new Template("function_call_external");

            template.SetKeys(new List<Tuple<string, string>>
            {
                new("input_parameters", inputParametersBuilder.ToString()),
                new("function", _externalNicknames[functionCall.Function.Name])
            });
            return template.ReplacePlaceholders(true);
        }
        else
        {
            _scopeBuilder.AppendLine($"COMPILER_OUTPUT_STRUCT_{functionCall.Function.Name} _{functionCall.GetHashCode().ToString()};");

            var template      = new Template("function_call");
            var lambdaBuilder = new Template("input_lambda");

            lambdaBuilder.SetKeys(new List<Tuple<string, string>>
            {
                new("functionCalls", functionCallBuilder.ToString()),
                new("function", functionCall.Function.Name),
                new("input_parameters", inputParametersBuilder.ToString())
            });
            template.SetKeys(new List<Tuple<string, string>>
            {
                new("function", functionCall.Function.Name),
                new("lambda", lambdaBuilder.ReplacePlaceholders(true)),
                new("output", $"&_{functionCall.GetHashCode().ToString()}"),
                new("is_async", functionCall.Function.IsAsync ? "1" : "0"),
                new("is_await", functionCall.Await ? "1" : "0"),
                new("suffix", suffix)
            });
            return template.ReplacePlaceholders(true);
        }
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

        if (returnStatement.function.Name != "setup")
        {
            StringBuilder outputParametersBuilder = new();
            if (returnStatement.function.IsAsync)
                outputParametersBuilder
                    .AppendLine($"xTaskNotify(((COMPILER_PARAMETERS_{returnStatement.function.Name}*) pvParameters)->taskhandle, 0, eNoAction);")
                    .AppendLine("vTaskDelete(NULL);");

            template.SetKeys(new List<Tuple<string, string>>
            {
                new("output_parameters", outputParametersBuilder.ToString())
            });
            outputParametersBuilder.AppendLine("delete pvParameters;");
        }

        return template.ReplacePlaceholders(true);
    }
}