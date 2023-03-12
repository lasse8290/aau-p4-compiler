namespace YALCompiler.DataTypes;

public class ArrayElementIdentifier: Identifier
{
    public Expression Index { get; }
    
    public ArrayElementIdentifier(string idValue, Expression index) : base(idValue)
    {
        Index = index;
    }

    public override string ToString()
    {
        return $"{IdValue}[{Index}]";
    }
}
