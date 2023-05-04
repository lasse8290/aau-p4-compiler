namespace YALCompiler.DataTypes;

public class FunctionCall : Expression
{
    public string Identifier { get; }
    public Function? Function { get; set; } = null;
    public List<Expression> InputParameters { get; set; } = new();
    public bool Await { get; set; }

    public FunctionCall(string identifier, bool await = false)
    {
        Identifier = identifier;
        Await = await;
    }
}