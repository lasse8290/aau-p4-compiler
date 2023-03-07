// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using StringTemplating;

Console.WriteLine("Hello, World!");

IEnumerable<string> names = Template.LoadTemplates("Templates", "txt");


var program = new Template("program");
program["include"] = "#include <stdio.h>";
program["setup_body"] = """printf(\Hello World\);""";

var declartion_assignment = new Template("declaration_assignment");

declartion_assignment.SetKeys(new List<Tuple<string, string>>() { 
    new("type", "int"),
    new("id", "i"),
    new("value", "0")
});

var ifStatement = new Template("if");
ifStatement.SetKeys(new List<Tuple<string, string>>() {
    new("condition", "i == 0"),
    new("body", """printf(\i is 0\);""")
});

program.SetKeys(new List<Tuple<string, Template>>() {
    new("loop_body", declartion_assignment),
    new("loop_body", ifStatement)
});

Console.WriteLine(program.ReplacePlaceholders(true));