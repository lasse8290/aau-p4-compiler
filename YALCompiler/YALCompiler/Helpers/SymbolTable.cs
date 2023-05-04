using YALCompiler.Exceptions;

namespace YALCompiler.Helpers;

public class SymbolTable
{
    private readonly Dictionary<string, Symbol> _symbols = new ();
    
    public Symbol this[string id]
    {
        get => _symbols[id];
        set {
            if (value is Symbol)
            {
                _symbols[id] = value;
            }
            else
            {
                _symbols[id] = new Symbol(id, value);
            }
        }
    }

    public void Add(Symbol symbol)
    {
        Add(symbol.Name, symbol);
    }
    
    public bool ContainsKey(string id)
    {
        return _symbols.ContainsKey(id);
    }
    
    public void Add(string id, Symbol symbol)
    {
        if (_symbols.ContainsKey(id))
        {
            throw new VariableAlreadyExistsException(id);
        }
        _symbols.Add(id, symbol);
    }
}