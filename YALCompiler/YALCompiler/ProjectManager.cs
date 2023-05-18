using System.Diagnostics;
using System.Text;
using CliWrap;
using CliWrap.Buffered;
using ESPSimulation;

namespace YALCompiler;

public class ProjectManager {
    ProjectOptions _projectOptions;

    public static async Task<bool> IsDependenciesInstalled() {
        CommandResult x = await Cli.Wrap("pio").WithValidation(CommandResultValidation.None).ExecuteAsync();

        return true;
    }

    public ProjectManager(ProjectOptions projectOptions) {
        _projectOptions = projectOptions;
    }

    public async Task<bool> CreateProject() {
        Console.WriteLine("Creating project...");

        // Create project directory
        Directory.CreateDirectory(_projectOptions.ProjectDir);
        Directory.CreateDirectory(_projectOptions.BuildPath);
        Directory.CreateDirectory(Path.Join(_projectOptions.ProjectDir, "src"));

        // Create main.yal file
        File.Create(Path.Join(_projectOptions.ProjectDir, "src/main.yal"));

        Console.WriteLine("Initializing project...");
        
        await RunPIOCommand(
            $"project init --project-dir={_projectOptions.BuildPath} -b nodemcu-32s --project-option \"framework=arduino\"",
            "Could not initialize project.");

        Console.WriteLine("Project created successfully at: " + Path.GetFullPath(_projectOptions.ProjectDir));
        return true;
    }


    public async Task<bool> CleanProject() {

        if (!Directory.Exists(_projectOptions.ProjectDir) || !Directory.Exists(_projectOptions.SourcePath)) {
            TerminalExtension.LogError("Project directory does not exist or is malformed.");
            return false;
        }
        
        if (Directory.Exists(_projectOptions.BuildPath)) {
            Directory.Delete(_projectOptions.BuildPath, true);
        }
        
        Directory.CreateDirectory(_projectOptions.BuildPath);

        await RunPIOCommand(
            $"project init --project-dir={_projectOptions.BuildPath} -b nodemcu-32s --project-option \"framework=arduino\"",
            "Could not clean project."
        );

        File.Create(_projectOptions.SourcePath + "/main.yal");

        return true;
    }

    public async Task<bool> CompileProject() {

        foreach (string file in Directory.EnumerateFiles(_projectOptions.SourcePath)) {
            //TODO multiple files compilation
            if (file.Split('/').Last() != "main.yal") continue;
            
            Transpiler transpiler = new(file, _projectOptions.BuildPath + "/src/main.cpp");
            transpiler.Transpile();
            Console.WriteLine("Transpiled successfully.");
            return true;
        }

        return false;
    }

    public async Task<bool> RunProject() {
        if (!await CompileProject()) return false;

        if (_projectOptions.WokwiUrl != null) {
            
                ESPSimulator s;

                string code = File.ReadAllText(_projectOptions.BuildPath + "/src/main.cpp");

                //TODO proper duration
                s = new(code, 10, _projectOptions.WokwiUrl);

                Console.WriteLine("Running code...");
                s.Run().Wait();
                Console.WriteLine("Exited...");
        }
        else {
            await RunPIOCommand(
                $"run --project-dir={_projectOptions.BuildPath}",
                "Could not run project."
            );
        }

        return true;

    }

    async Task RunPIOCommand(string args, string? errorMsg) {
        try {
            BufferedCommandResult mm = await Cli.Wrap(_projectOptions.PIOCLIPath)
                .WithArguments(args)
                .WithValidation(CommandResultValidation.None)
                .ExecuteBufferedAsync();

            Console.WriteLine("==================PIO cli output==================");
            if (mm.StandardError != string.Empty) {
                TerminalExtension.LogError(mm.StandardError);
            }
            else if (mm.StandardOutput != string.Empty) {
                Console.WriteLine(mm.StandardOutput);
            }

            Console.WriteLine("====================================");
        }
        catch (Exception e) {
            Console.WriteLine(e);
            TerminalExtension.LogError(errorMsg);
            throw;
        }
    }
}