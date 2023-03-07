namespace YALParser.Helpers;

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
}