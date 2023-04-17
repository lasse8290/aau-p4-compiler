public static class FluentAssertionsExtension
{
    public static void BeEquivalentTo(this FluentAssertions.Primitives.ObjectAssertions actual, object expected, string[] excludings)
    {
        actual.BeEquivalentTo(expected, options => options.RespectingRuntimeTypes().Excluding(ctx => excludings.Contains(ctx.Name)));
    }
}
