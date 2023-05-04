using CommandLine;

namespace YALCompiler;
public partial class Program
{
    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "Path to the source file. It must be a (.YAL)")]
        public string InputFilePath { get; set; } = default!;

        [Option('o', "output", Required = false, HelpText = "Path for the destination file.")]
        public string OutputFilePath { get; set; } = default!;

        [Option('s', "simulator", Required = false, HelpText = "Run the program using a ESP32 Simulator.")]
        public bool UseSimulator { get; set; } = default!;

        [Option('t', "timeout", Required = false, HelpText = "Timeout for the simulation in milliseconds.")]
        public int Timeout { get; set; } = 999999999;

        [Option('w', "wokwi", Required = false, HelpText = "URL to Wokwi project to be used")]
        public string WokwiURL { get; set; } = default!;
    }

}