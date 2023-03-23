namespace YALCompiler.DataTypes;

public class ArrayLiteral : Expression
{
    public List<Expression> Values { get; set; } = new();
    // public Expression this[int index]
    // {
    //     get => Values[index];
    //     set
    //     {
    //         if (Values.Count >= index)
    //         {
    //             Values[index] = value;
    //         }
    //         else
    //         {
    //             Values.Add(value);
    //         }
    //     }
    // }
}