// See https://aka.ms/new-console-template for more information

using System.Text;
using Antlr4.Runtime;
using StringTemplating;
using YALParser;

Console.WriteLine("Hello, World!");

IEnumerable<string> names = TemplateEngine.LoadTemplates("Templates", "c");

var tmp = new TemplateEngine("libs");
tmp["include"] = "#include <iostream>";

Console.WriteLine(tmp.ReplacePlaceholders());