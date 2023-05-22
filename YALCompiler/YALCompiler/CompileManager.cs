using ESPSimulation;

namespace YALCompiler; 

public class CompileManager {

    CompileOptions _options;
    
    public CompileManager(CompileOptions options) {
        _options = options;
        
 
    }

    public async Task<bool> Compile() {
        
        if (!File.Exists(_options.InputFilePath)) {
            TerminalExtension.LogError("Input file does not exist.");
            return false;
        }

        if (_options.OutputFilePath == null) {
            _options.OutputFilePath = Path.Join(Path.GetDirectoryName(_options.InputFilePath), $"{Path.GetFileNameWithoutExtension(_options.InputFilePath)}.ino");
        }

        Transpiler tp = new(_options.InputFilePath, _options.OutputFilePath);
        
        tp.Transpile();

        if (_options.WokwiUrl != null) {
            await RunSimulator(tp.CompiledCode, _options.Duration ??= 10000, _options.WokwiUrl);
        }
        
        return true;
    }
    
    public async Task<bool> RunSimulator(string code, int? duration, string? wokwiURL = null)
    {
        ESPSimulator s;

        if (wokwiURL != null)
            s = new(code, duration, wokwiURL);
        else
            s = new(code, duration);

        Console.WriteLine("Running code...");
        
        await s.Run();
        
        Console.WriteLine("Exited...");
        return true;
    }
}