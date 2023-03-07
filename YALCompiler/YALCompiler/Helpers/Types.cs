namespace YALCompiler.Helpers;

public static class Types
{
    public enum ValueType
    {
        int8,
        int16,
        int32,
        int64,
        uint8,
        uint16,
        uint32,
        uint64,
        float32,
        float64,
        @char,
        @string,
        @bool,
    }

    public static ValueType? Match(string type)
    {
        return ValueType.TryParse(type, out ValueType t) ? t : null;
    }
}