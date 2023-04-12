namespace YALCompiler.DataTypes;

public class Identifier : Expression, IAssignable
{
    public string Name { get; set; }
    public Identifier(string name)
    {
        Name = name;
    }

    public override string ToString() => Name;
}