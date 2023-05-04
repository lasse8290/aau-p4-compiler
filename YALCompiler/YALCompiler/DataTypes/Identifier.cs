namespace YALCompiler.DataTypes;

public class Identifier : Expression
{
    public string Name { get; set; }
    public bool IsRef { get; set; } = false;
    public Identifier(string name)
    {
        Name = name;
    }

    public override string ToString() => Name;
}