using YALCompiler.DataTypes;

namespace YALCompiler.Helpers;

public class Symbol : ISymbol
{
    public string Id { get; set; }
    public object? Value { get; set; }
    public YALType? Type { get; set; } = null;
    public bool Initialized { get; set; } = false;
    public bool IsRef { get; set; } = false;
    public ulong? ArraySize { get; set; }

    public Symbol(string id)
    {
        Id = id;
    }

    public Symbol(string id, object? value)
    {
        Id = id;
        Value = value;
        Initialized = true;
    }

    public Symbol(string id, object? value, YALType type) : this(id, value)
    {
        Type = type;
    }
}