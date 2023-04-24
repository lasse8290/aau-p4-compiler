using CommandLine;

namespace YALCompiler;
public partial class Program
{
    static void Main(string[] args)
    {
        CommandLine.Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {
                if (!File.Exists(o.InputFilePath))
                {
                    Console.WriteLine("Input file does not exist.");
                    return;
                }
                
                Transpiler transpiler = new(o.InputFilePath, o.OutputFilePath);
                transpiler.Transpile();
            });
    }
}