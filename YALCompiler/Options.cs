using CommandLine;

namespace YALCompiler;
public partial class Program
{
    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "Path to the source file. It must be a (.YAL)")]
        public string InputFilePath { get; set; } = default!;

        [Option('o', "output", Required = true, HelpText = "Path for the destination file.")]
        public string OutputFilePath { get; set; } = default!;
    }

}