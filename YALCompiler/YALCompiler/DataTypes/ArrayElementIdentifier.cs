namespace YALCompiler.DataTypes;

public class ArrayElementIdentifier: Identifier
{
    public Expression Index { get; }
    
    public ArrayElementIdentifier(string name, Expression index) : base(name)
    {
        Index = index;
    }

    public override string ToString()
    {
        return $"{Name}[{Index}]";
    }
}
