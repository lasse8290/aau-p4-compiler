namespace YALCompiler.DataTypes;

public class ExternalFunction: Function
{
    public string LibraryName { get; set; }
    public string FunctionName { get; set; }
}