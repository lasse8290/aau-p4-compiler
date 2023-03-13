using YALCompiler.Exceptions;

namespace YALCompiler.DataTypes;

public class Table<T> where T : ISymbol
{
    private readonly Dictionary<string, T> _symbols = new ();
    
    public T this[string id]
    {
        get => _symbols[id];
        set {
            if (value is T)
            {
                _symbols[id] = value;
            }
        }
    }

    public void Add(T item)
    {
        Add(item.Id, item);
    }
    
    public bool ContainsKey(string id)
    {
        return _symbols.ContainsKey(id);
    }
    
    public void Add(string id, T symbol)
    {
        if (_symbols.ContainsKey(id))
        {
            throw new VariableAlreadyExistsException(id);
        }
        _symbols.Add(id, symbol);
    }
}