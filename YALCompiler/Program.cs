using CommandLine;
using System.Threading.Tasks;

namespace YALCompiler;
public partial class Program
{
    static async Task Main(string[] args)
    {
        string code = "";

        CommandLine.Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {
                if (!File.Exists(o.InputFilePath))
                {
                    Console.WriteLine("Input file does not exist.");
                    return;
                }

                Transpiler transpiler = new(o.InputFilePath, o.OutputFilePath);
                transpiler.Transpile();

                code = transpiler.CompiledCode;
            });

        ESPSimulator s = new(code);

        Console.WriteLine("Running puppeteer");
        await s.Run();
    }
}