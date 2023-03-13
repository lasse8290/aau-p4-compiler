using YALCompiler.Exceptions;
using YALCompiler.Helpers;

namespace YALCompiler.DataTypes;

public abstract class ASTNode
{
    public List<ASTNode> Children { get; } = new();
    public ASTNode? Parent { get; set; } = default;
    public Table<Symbol> SymbolTable { get; } = new();   
    public Table<Function> FunctionTable { get; } = new();

    public void AddSymbolOrFunction(Symbol symbol)
    {
        if (!SymbolTable.ContainsKey(symbol.Id) && !FunctionTable.ContainsKey(symbol.Id))
        {
            SymbolTable.Add(symbol);
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
            FunctionTable.Add(function);
        }
        else
        {
            throw new VariableAlreadyExistsException(function.Id);
        }
    }
}