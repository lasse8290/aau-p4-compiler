using FluentAssertions;

public class AcceptanceTesting
{
    public static TheoryData<string, List<string>, int> AcceptanceTestsData =>
       new() {
            { "test1.yal", new List<string> { "before", "between", "after" }, 1500 },
        };

    [Theory]
    [MemberData(nameof(AcceptanceTestsData))]
    public async Task AcceptanceTests(string filename, List<string> expectedOutput, int timeout)
    {
        Transpiler transpiler = new(Path.Combine(Environment.CurrentDirectory, $"src/{filename}"));
        transpiler.Transpile();

        string code = transpiler.CompiledCode;
        ESPSimulation s = new(code, timeout);
        await s.Run();
        s.Output.Should().BeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
    }
}