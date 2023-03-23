namespace YALCompiler.DataTypes;

public class Identifier : Predicate
{
    public string IdValue { get; }

    public Identifier(string idValue)
    {
        IdValue = idValue;
    }

    public override string ToString()
    {
        return IdValue;
    }
}