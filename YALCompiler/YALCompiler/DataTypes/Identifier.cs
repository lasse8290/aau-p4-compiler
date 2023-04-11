namespace YALCompiler.DataTypes;

public class Identifier : Expression
{
    public List<string> Names { get; } = new();
    public Identifier(string name)
    {
        Names.Add(name);
    }

    public Identifier()
    {
    }

    public override string ToString()
    {
        return string.Join(", ", Names);
    }
}