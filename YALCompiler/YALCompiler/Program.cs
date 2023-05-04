using CommandLine;
using ESPSimulation;

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

                if (o.UseSimulator) RunSimulator(transpiler.CompiledCode, o.Duration, o.WokwiURL);
            });
    }

    static void RunSimulator(string code, int? duration, string? wokwiURL = null)
    {
        ESPSimulator s;

        if (wokwiURL != null)
            s = new(code, duration, wokwiURL);
        else
            s = new(code, duration);

        Console.WriteLine("Running code...");
        s.Run().Wait();
        Console.WriteLine("Exited...");
    }
}