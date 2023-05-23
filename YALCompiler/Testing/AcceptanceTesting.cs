using ESPSimulation;
using FluentAssertions;

[Trait("Category", "AcceptanceTests")]
public class AcceptanceTesting
{
    public static TheoryData<string, List<string>, int> AcceptanceTestsData =>
       new() {
            { "test.yal", new List<string> { "before", "between", "after" }, 10000 },
        };

    [Theory]
    [MemberData(nameof(AcceptanceTestsData))]
    public async Task AcceptanceTests(string filename, List<string> expectedOutput, int timeout)
    {
        Transpiler transpiler = new(Path.Combine(Environment.CurrentDirectory, $"tests/{filename}"));
        transpiler.Transpile();

        string CompiledCode = transpiler.CompiledCode;
        ESPSimulator s = new(CompiledCode, timeout);
        await s.Run();

        s.Output.Should().BeEquivalentTo(expectedOutput, options => options.WithStrictOrdering());
    }
}