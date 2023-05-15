using CommandLine;

namespace YALCompiler;

[Verb("compile", HelpText = "Compile a single YAL file.")]
public record CompileOptions {
    
    [Option('i', "input", Required = true, HelpText = "Path to the source file. It must be a (.YAL)")]
    public string InputFilePath { get; set; } = default!;

    [Option('o', "output", Required = false, HelpText = "Path for the destination file.")]
    public string? OutputFilePath { get; set; }

    //[Option('s', "simulator", Required = false, HelpText = "Run the program using a ESP32 Simulator.")]
    //public bool UseSimulator { get; set; } = default!;

    [Option('t', "timeout", Required = false, HelpText = "Timeout/Duration for the simulation in milliseconds.")]
    public int? Duration { get; set; }

    [Option('w', "wokwi", Required = false, HelpText = "URL to Wokwi project to be used")]
    public string WokwiUrl { get; set; } = string.Empty;
}