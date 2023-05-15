using CommandLine;
using ESPSimulation;

namespace YALCompiler;

public partial class Program {
    private static async Task Main(string[] args) {

        /*
         Parser.Default.ParseArguments<DefaultOptions>(args).WithParsed(defaultOptions => {
            if (!File.Exists(defaultOptions.InputFilePath)) {
                Console.WriteLine("Input file does not exist.");
                return;
            }

            Transpiler transpiler = new(defaultOptions.InputFilePath, defaultOptions.OutputFilePath);
            transpiler.Transpile();

            if (defaultOptions.UseSimulator)
                RunSimulator(transpiler.CompiledCode, defaultOptions.Duration, defaultOptions.WokwiUrl);
        });
*/
        Parser.Default.ParseArguments<ProjectOptions, CompileOptions>(args).MapResult(
            (ProjectOptions opts) => {
                HandleProjectOptions(opts).Wait();
                return 0;
            },
            (CompileOptions opts) => {
                HandleCompileOptions(opts);
                return 0;
            },
            errs => 1);
        
        

    }

    private static void HandleCompileOptions(CompileOptions opts) { }

    private static async Task HandleProjectOptions(ProjectOptions projectOptions) {

        projectOptions.ProjectDir = "./test";
        
        var projectmanager = new ProjectManager(projectOptions);

        if (projectOptions.InitProject) {

            bool x = await projectmanager.CreateProject();
        }
        else if (projectOptions.CleanProject) {
            bool x = await projectmanager.CleanProject();
        }
        else if (projectOptions.CompileProject) {
           bool x = await projectmanager.CompileProject();
        }
        else if (projectOptions.RunProject) {
            bool x = await projectmanager.RunProject();
        }
        else {
            TerminalExtension.LogError("No action specified.");
        }

    }
}

