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
            stringBuilder.AppendLine((string)InvokeVisitor(child));
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
        return _template.ReplacePlaceholders(true);
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
        _externalNicknames[externalFunction.Name] = externalFunction.FunctionName;
        return "";
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
        return "";
    }

    internal override object? Visit(ElseIf elseIfNode)
    {
        return "";
    }

    internal override object? Visit(Else elseNode)
    {
        return "";
    }

   internal override object? Visit(Function function)
    {
        StringBuilder initializedParametersBuilder = new();
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
        .AppendLine(inputTemplate.ReplacePlaceholders())
        .AppendLine(outputTemplate.ReplacePlaceholders())
        .AppendLine($"#define COMPILER_PARAMETERS_{function.Name} COMPILER_PARAMETERS<COMPILER_INPUT_STRUCT_{function.Name}, COMPILER_OUTPUT_STRUCT_{function.Name}>");

        foreach (var symbol in function.InputParameters)
            initializedParametersBuilder.AppendLine($"{symbol.Type.ToCPPType().First()} {symbol.Name} = _COMPILER_PARAMETERS->input->{symbol.Name};");

        foreach (var symbol in function.OutputParameters)
            initializedParametersBuilder.AppendLine($"{symbol.Type.ToCPPType().First()} {symbol.Name} = _COMPILER_PARAMETERS->output->{symbol.Name};");

        var bodyBuilder = new StringBuilder();
        foreach (var child in function.Children)
            bodyBuilder.AppendLine(InvokeVisitor(child) as string);

        Template template = new Template("function");
        template.SetKeys(new List<Tuple<string, string>>
        {
            new("name", function.Name),
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

    // fix this to have 2 visitors: Integer and UnsignedInteger
    
    // internal override object? Visit(SignedNumber signedNumber)
    // {
    //     var template = new Template("signed_number");
    //     template.SetKeys(new List<Tuple<string, string>>
    //     {
    //         new("signed_number", (signedNumber.Negative ? "-" : "") + signedNumber.Value)
    //     });
    //
    //     return template.ReplacePlaceholders();
    // }

    internal override object? Visit(WhileStatement whileLoop)
    {
        return "";
    }

    internal override object? Visit(SignedFloat signedFloat)
    {
        return signedFloat.ToString();
    }

    internal override object? Visit(BinaryAssignment binaryAssignment)
    {
        StringBuilder targetsBuilder = new(); 
        foreach (var target in binaryAssignment.Targets) {
            var targetName = target switch
            {
                VariableDeclaration variableDeclaration => variableDeclaration.Variable.Name,
                Identifier identifier => identifier.Name,
            };
            
            targetsBuilder.AppendLine($"COMPILER_ASSIGN(&{targetName});");
        }
        
        foreach(var value in binaryAssignment.Values)
            targetsBuilder.AppendLine((string)InvokeVisitor(value));

        return targetsBuilder.ToString();
    }

    internal override object? Visit(VariableDeclaration variableDeclaration)
    {
        return "";
    }

    internal override object? Visit(Identifier identifier)
    {
        return "";
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
        return "";
    }

    internal override object? Visit(ArrayElementIdentifier arrayElementIdentifier)
    {
        return "";
    }

    internal override object? Visit(FunctionCall functionCall)
    {
        return $"COMPILER_OUTPUT_STRUCT_{functionCall.Function.Name} struct;";
    }

    internal override object? Visit(CompoundExpression compoundExpression)
    {
        return "";
    }

    internal override object? Visit(StringLiteral stringLiteral)
    {
        return "";
    }

    internal override object? Visit(ReturnStatement returnStatement)
    {
        return "";
    }
}