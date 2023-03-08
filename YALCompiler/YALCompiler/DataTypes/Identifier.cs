namespace YALCompiler.DataTypes;

public class Identifier : Expression
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