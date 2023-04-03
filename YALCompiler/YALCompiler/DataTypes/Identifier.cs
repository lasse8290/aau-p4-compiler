namespace YALCompiler.DataTypes;

public class Identifier : Predicate
{
    public string Name { get; }
    public Identifier(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }
}