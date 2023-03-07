using System.Text.RegularExpressions;

namespace StringTemplating;

/// <summary>
/// A simple template engine that uses %%placeholder%% as placeholder
/// </summary>
public class TemplateEngine {
    
    /// <summary>
    /// A dictionary that holds all loaded templates with their name as key and the content as value
    /// </summary>
    public static readonly Dictionary<string, string> LoadedTemplates = new();
    
    /// <summary>
    /// Checks if the templates have been loaded
    /// </summary>
    private static bool Initialized => LoadedTemplates.Keys.Count > 0;
    
    /// <summary>
    /// The name of the selected template
    /// </summary>
    public string SelectedTemplateName { get; init; }
    
    private string _selectedTemplateContent;

    private readonly Dictionary<string, string> _replacementKeys = new();

    /// <summary>
    /// Set or get a replacement key
    /// </summary>
    /// <param name="key">the string key, should match a %%key%% </param>
    public string this[string key] {
        get => _replacementKeys[key];
        set => _replacementKeys[key] = value;
    }
    
    public TemplateEngine(string templateName) {
        if (!Initialized) {
            throw new Exception("TemplateEngine not initialized, call LoadTemplates first");
        }
        
        SelectedTemplateName = templateName;
        _selectedTemplateContent = LoadedTemplates[templateName];
    }
    public static IEnumerable<string> LoadTemplates(string templatesPath, string templatesExtension) {
        Directory
            .EnumerateFiles(templatesPath, $"*.{templatesExtension}")
            .ToList()
            .ForEach(templatePath => {
                string templateName = Path.GetFileNameWithoutExtension(templatePath);
                string templateContent = File.ReadAllText(templatePath);
                LoadedTemplates.Add(templateName, templateContent);
            });

        return LoadedTemplates.Keys.ToList();
    }

    public Dictionary<string, string?> GetReplacementKeys() {
        Dictionary<string, string?> replacementKeys = new();

        var regex = new Regex(@"%%(.*?)%%");
        MatchCollection matches = regex.Matches(_selectedTemplateContent);

        for (int index = 0; index < matches.Count; index++) {
            Match match = matches[index];
            replacementKeys.Add(match.Groups[1].Value, null);
        }

        return replacementKeys;
    }

    public string ReplacePlaceholders() {

        foreach (string k in _replacementKeys.Keys) {
            if (_replacementKeys[k] == null) {
                throw new Exception($"Replacement key {k} not set");
            }

            _selectedTemplateContent = _selectedTemplateContent.Replace($"%%{k}%%", _replacementKeys[k]);
        }
        
        return _selectedTemplateContent;
    }
}