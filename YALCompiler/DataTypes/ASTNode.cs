using YALCompiler.Exceptions;
using YALCompiler.Helpers;

namespace YALCompiler.DataTypes;

public abstract class ASTNode
{
    public List<ASTNode> Children { get; set; } = new();
    public ASTNode? Parent { get; set; } = default;
    public Dictionary<string, Symbol> SymbolTable { get; } = new();   
    public Dictionary<string, Function> FunctionTable { get; } = new();
    public int LineNumber { get; set; }

    public void AddSymbolOrFunction(Symbol symbol)
    {
        if (!SymbolTable.ContainsKey(symbol.Id) && !FunctionTable.ContainsKey(symbol.Id))
        {
            SymbolTable.Add(symbol.Id, symbol);
        }
        else
        {
            throw new VariableAlreadyExistsException(symbol.Id);
        }
    }
    
    public void AddSymbolOrFunction(Function function)
    {
        if (!SymbolTable.ContainsKey(function.Id) && !FunctionTable.ContainsKey(function.Id))
        {
            FunctionTable.Add(function.Id, function);
        }
        else
        {
            throw new VariableAlreadyExistsException(function.Id);
        }
    }
    
    public override string ToString() => GetType().Name;
}