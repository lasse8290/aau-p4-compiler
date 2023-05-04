using YALCompiler.DataTypes;

namespace YALCompiler.Helpers;

public class Symbol : ISymbol
{
    public string Name { get; set; }
    public object? Value { get; set; }
    public YALType? Type { get; set; } = null;
    public bool Initialized { get; set; } = false;
    public bool IsRef { get; set; } = false;
    public ulong? ArraySize { get; set; }

    public Symbol(string name)
    {
        Name = name;
    }

    public Symbol(string name, object? value)
    {
        Name = name;
        Value = value;
        Initialized = true;
    }

    public Symbol(string id, object? value, YALType type) : this(id, value)
    {
        Type = type;
    }
}