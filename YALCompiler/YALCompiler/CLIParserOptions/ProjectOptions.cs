using CommandLine;

namespace YALCompiler; 

[Verb("project", HelpText = "Create a new project.")]
public record ProjectOptions {

    [Option('d', "directory", Required = false, HelpText = "Path to the project directory.")]
    public string ProjectDir { get; set; } = Directory.GetCurrentDirectory();

    [Option('p', "pio", Required = false, HelpText = "Path to the platformIO CLI executable.")]
    public string PIOCLIPath { get; set; } = "pio";

    public string BuildPath => ProjectDir + "/pioBuild";
    public string SourcePath => ProjectDir + "/src";
    
    
    [Option('i', "init", Required = false, HelpText = "Initialize a new project.", SetName = "init")]
    public bool InitProject { get; set; } = false;
    
    
    [Option('b', "board", Required = false, HelpText = "Board to be used for the project.")]
    public string Board { get; set; } = "nodemcu32s";
    
    [Option('c', "clean", Required = false, HelpText = "Clean the project.", SetName = "clean")]
    public bool CleanProject { get; set; } = false;
    
    [Option('C', "compile", Required = false, HelpText = "Compile the project.", SetName = "compile")]
    public bool CompileProject { get; set; } = false;
    
    
    
    [Option('r', "run", Required = false, HelpText = "Run the project.", SetName = "run")]
    public bool RunProject { get; set; }
    
    [Option('t', "timeout", Required = false, HelpText = "Timeout/Duration for the simulation in milliseconds.", SetName = "run")]
    public int? Duration { get; set; }

    [Option('w', "wokwi", Required = false, HelpText = "URL to Wokwi project to be used", SetName = "run")]
    public string? WokwiUrl { get; set; } = string.Empty;
    
    

}
