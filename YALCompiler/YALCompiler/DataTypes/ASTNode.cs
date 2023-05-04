using YALCompiler.Exceptions;
using YALCompiler.Helpers;

namespace YALCompiler.DataTypes;

public abstract class ASTNode
{
    public List<ASTNode> Children { get; set; } = new();
    public ASTNode? Parent { get; set; } = default;
    public Dictionary<string, Symbol>? SymbolTable { get; private set; } = null;   
    public Dictionary<string, Function>? FunctionTable { get; private set; } = null;
    public int LineNumber { get; set; }

    public void AddSymbolOrFunction(Symbol symbol)
    {
        if (SymbolTable is null) SymbolTable = new();
        if (!SymbolTable.ContainsKey(symbol.Name) && (FunctionTable is null || !FunctionTable.ContainsKey(symbol.Name)))
        {
            SymbolTable.Add(symbol.Name, symbol);
        }
        else
        {
            throw new VariableAlreadyExistsException(symbol.Name);
        }
    }
    
    public void AddSymbolOrFunction(Function function)
    {
        if (FunctionTable is null) FunctionTable = new();
        if ((SymbolTable is null || !SymbolTable.ContainsKey(function.Name)) && !FunctionTable.ContainsKey(function.Name))
        {
            FunctionTable.Add(function.Name, function);
        }
        else
        {
            throw new VariableAlreadyExistsException(function.Name);
        }
    }
    
    public override string ToString() => GetType().Name;
}