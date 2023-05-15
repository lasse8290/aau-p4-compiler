using CommandLine;
using ESPSimulation;

namespace YALCompiler;

public partial class Program {
    private static async Task Main(string[] args) {


        Parser.Default.ParseArguments<ProjectOptions, CompileOptions>(args).MapResult(
            (ProjectOptions opts) => {
                HandleProjectOptions(opts).Wait();
                return 0;
            },
            (CompileOptions opts) => {
                HandleCompileOptions(opts).Wait();
                return 0;
            },
            errs => {
                foreach (Error? e in errs) {
                    TerminalExtension.LogError(e.ToString());
                }
                return 0;
            });
    }

    private static async Task  HandleCompileOptions(CompileOptions opts) {
        
        CompileManager compileManager = new(opts);
        await compileManager.Compile();
    }


    private static async Task HandleProjectOptions(ProjectOptions projectOptions) {

        projectOptions.ProjectDir = "./test";
        
        var projectManager = new ProjectManager(projectOptions);

        if (projectOptions.InitProject) {

            bool x = await projectManager.CreateProject();
        }
        else if (projectOptions.CleanProject) {
            bool x = await projectManager.CleanProject();
        }
        else if (projectOptions.CompileProject) {
           bool x = await projectManager.CompileProject();
        }
        else if (projectOptions.RunProject) {
            bool x = await projectManager.RunProject();
        }
        else {
            TerminalExtension.LogError("No action specified.");
        }

    }
}

