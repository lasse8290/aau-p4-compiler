using System.Collections;
using System.Reflection;
using YALCompiler.DataTypes;



public class Options {
    public ASTNode Exclude(Func<ASTNode, string> func) {
        return new SignedFloat(5);
    }
}

public class CAssert
{
    public static void Equivalent_Wrapper(object expected, object actual, Func<Options, ASTNode> options) {
        Equivalent(expected, actual);
    }

    public static void Equivalent(object expected, object actual)
    {
        // If both objects are null or the same instance, they are equal
        if (object.ReferenceEquals(expected, actual))
        {
            return;
        }

        // If either object is null, they are not equal
        if (expected == null || actual == null)
        {
            throw new Exception($"Descending {(expected == null ? expected.GetType().Name : actual.GetType().Name)} is null");
        }

        // If the objects are of different types, they are not equal
        if (expected.GetType() != actual.GetType())
        {
            throw new Exception($"Expected type {expected.GetType().Name} but got {actual.GetType().Name}");
        }


        if (expected.GetType().IsPrimitive || expected is string || expected.GetType().IsEnum)
        {
            ComparePrimitiveOrStringOrEnum(expected, actual);
        }
        else if (expected is IEnumerable)
        {
            CompareCollections(expected, actual);
        }
        else {
            CompareProperties(expected, actual);
        }

        return;
    }

    private static void ComparePrimitiveOrStringOrEnum(object expected, object actual) {
        if (!expected.Equals(actual))
        {
            throw new Exception($"Expected: {expected} but got: {actual}");
        }
    }

    private static void CompareCollections(object expected, object actual) {
        var enumerable1 = (IEnumerable)expected;
        var enumerable2 = (IEnumerable)actual;

        if (enumerable1.Cast<object>().Count() != enumerable2.Cast<object>().Count())
        {
            throw new Exception("One collection has more objects than the other");
        }

        var enumerator1 = ((IEnumerable)expected).GetEnumerator();
        var enumerator2 = ((IEnumerable)actual).GetEnumerator();


        while (enumerator1.MoveNext() && enumerator2.MoveNext())
        {
            Equivalent(enumerator1.Current, enumerator2.Current);
        }
    }

    private static void CompareProperties(object expected, object actual) {
        var properties = expected.GetType().GetProperties();

        foreach (var property in properties)
        {
            if (property.Name == nameof(ASTNode.LineNumber) || property.Name == nameof(ASTNode.Parent)) continue;

            var expectedvalue = property.GetValue(expected);
            var actualvalue = property.GetValue(actual);

            Equivalent(expectedvalue, actualvalue);
        }
    }
}