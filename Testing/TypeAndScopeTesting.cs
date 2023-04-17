using FluentAssertions;
using YALCompiler.DataTypes;

public class TypeAndScopeTesting : TestingHelper {
    
    [Theory]
    [InlineData(@"external <""my_library""> print1: in (string _string);", "print1")]
    [InlineData(@"external <""hej""> print2: in (string _string);", "print2")]
    public void External_Function_Exists_In_Symbol_Table(string input, string expected)
    {
        ExternalFunction actual = (ExternalFunction)Setup(input, nameof(YALGrammerParser.externalFunctionDeclaration));

        actual.Id.Should().Be(expected);
    }
    
}