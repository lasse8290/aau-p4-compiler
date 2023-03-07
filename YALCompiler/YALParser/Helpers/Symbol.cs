namespace YALParser.Helpers;

public class Symbol
{
    public string Id { get; set; }
    public object? Value { get; set; }
    public YALType Type { get; set; }
    public bool Initialized { get; set; } = false;

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
}