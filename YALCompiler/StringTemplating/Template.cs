using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringTemplating;

/// <summary>
/// A simple template engine that uses %%placeholder%% as placeholder
/// </summary>
public partial class Template {
    
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
    public string SelectedTemplateName { get; }

    private readonly string _selectedTemplateContent;

    private readonly Dictionary<string, string?> _replacementKeys;
    public string[] ReplacementKeys => _replacementKeys.Keys.ToArray();
    
    /// <summary>
    /// Set or get a replacement key
    /// </summary>
    /// <param name="key">the string key, should match a %%key%% </param>
    public string? this[string key] {
        get => _replacementKeys[key];
        set => _replacementKeys[key] = value;
    }
    
    
    private void ClearAllKeys() {
        foreach (string k in _replacementKeys.Keys) {
            this[k] = "";
        }
    }
    
    private void ClearKeys<T>(List<Tuple<string, T>> keys) {
        foreach (var k in keys) {

            if (!_replacementKeys.ContainsKey(k.Item1)) {
                string availableKeys = string.Join(", ", _replacementKeys.Keys);
                throw new KeyNotFoundException($"Key: {k.Item1} not found in template, availableKeys: {availableKeys}");
            }
            
            this[k.Item1] = "";
        }
    }

    /// <summary>
    ///  Replaces the given key placeholders in the selected template with the given values
    ///  Keys may be used multiple times to add multiple strings to the same key
    /// </summary>
    /// <param name="replacementKeys"> a tuple list of strings keys and string values</param>
    public void SetKeys(List<Tuple<string, string>> replacementKeys) {
        
        ClearKeys(replacementKeys);
        
        // replace all keys, some keys might be used multiple times for add multiple templates to same key
        foreach (Tuple<string, string> k in replacementKeys) {
            this[k.Item1] += k.Item2;
        }
    }
    
    /// <summary>
    /// Replaces the given key placeholders in the selected template with the given values
    /// Keys may be used multiple times to add multiple templates to the same key
    /// </summary>
    /// <param name="replacementKeys">a tuple list of  <code>string</code> keys and <code>Template</code> that will evaluate with ReplacePlaceholders  </param>
    public void SetKeys(List<Tuple<string, Template>> replacementKeys) {

        ClearKeys(replacementKeys);
        
        // replace all keys, some keys might be used multiple times for add multiple templates to same key
        foreach (Tuple<string, Template> k in replacementKeys) {
            this[k.Item1] += k.Item2.ReplacePlaceholders();
        }
    }
    
    /// <summary>
    /// Creates a new template engine instance with the given template name and loads the template content
    /// name should match a template name in the LoadedTemplates dictionary
    /// </summary>
    /// <param name="templateName">name of a loaded template</param>
    /// <exception cref="Exception">throws an exception if templates are not loaded or if templateName is not valid</exception>
    public Template(string templateName) {
        if (!Initialized) {
            throw new Exception("TemplateEngine not initialized, call LoadTemplates first");
        }
        
        if (!LoadedTemplates.ContainsKey(templateName)) {
            throw new Exception($"Template {templateName} not found");
        }
        
        SelectedTemplateName = templateName;
        _selectedTemplateContent = LoadedTemplates[templateName];
        _replacementKeys = GetReplacementKeys();
    } 

    /// <summary>
    /// Loads all templates in the given path with the given extension
    /// </summary>
    /// <param name="templatesPath">path to the folder containing templates</param>
    /// <param name="templatesExtension"></param>
    /// <returns>a IEnumerable of template names</returns>
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

    /// <summary>
    /// Returns a dictionary of all replacement keys in the selected template
    /// </summary>
    /// <returns></returns>
    private Dictionary<string, string?> GetReplacementKeys() {
        Dictionary<string, string?> replacementKeys = new();

        var regex = DelimiterRegex();
        MatchCollection matches = regex.Matches(_selectedTemplateContent);

        for (int index = 0; index < matches.Count; index++) {
            Match match = matches[index];
            replacementKeys.Add(match.Groups[1].Value, null);
        }

        return replacementKeys;
    }

    /// <summary>
    /// Replaces all placeholders in the selected template with the given values and returns the new content
    /// </summary>
    /// <returns> returns a string with all placeholders replaces</returns>
    /// <exception cref="Exception">throws if any keys are not set</exception>
    public string ReplacePlaceholders(bool ignoreMissingKeys = false) {

        string newContent = _selectedTemplateContent;
        
        foreach (string k in _replacementKeys.Keys) {
            if (!ignoreMissingKeys && _replacementKeys[k] == null) {
                throw new Exception($"Replacement key {k} not set");
            }

            newContent = newContent.Replace($"%%{k}%%", _replacementKeys[k]);
        }
        
        return newContent;
    }

    [GeneratedRegex("%%(.*?)%%")]
    private static partial Regex DelimiterRegex();
}