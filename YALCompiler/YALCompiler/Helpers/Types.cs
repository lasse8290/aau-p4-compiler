namespace YALCompiler.Helpers;

public static class Types
{
    public static List<(ValueType, ValueType)> AssignableTypes = new()
    {
        // (target, source)
        ( ValueType.int16, ValueType.int8 ),
        ( ValueType.int16, ValueType.uint8 ),

        ( ValueType.int32, ValueType.int16 ),
        ( ValueType.int32, ValueType.int8 ),
        ( ValueType.int32, ValueType.uint16 ),
        ( ValueType.int32, ValueType.uint8 ),

        ( ValueType.int64, ValueType.int32 ),
        ( ValueType.int64, ValueType.int16 ),
        ( ValueType.int64, ValueType.int8 ),
        ( ValueType.int64, ValueType.uint32 ),
        ( ValueType.int64, ValueType.uint16 ),
        ( ValueType.int64, ValueType.uint8 ),

        ( ValueType.uint16, ValueType.uint8 ),

        ( ValueType.uint32, ValueType.uint16 ),
        ( ValueType.uint32, ValueType.uint8 ),

        ( ValueType.uint64, ValueType.uint32 ),
        ( ValueType.uint64, ValueType.uint16 ),
        ( ValueType.uint64, ValueType.uint8 ),

        ( ValueType.float32, ValueType.int8 ),
        ( ValueType.float32, ValueType.int16 ),
        ( ValueType.float32, ValueType.uint8 ),
        ( ValueType.float32, ValueType.uint16 ),

        ( ValueType.float64, ValueType.float32 ),
        ( ValueType.float64, ValueType.int8 ),
        ( ValueType.float64, ValueType.int16 ),
        ( ValueType.float64, ValueType.int32 ),
        ( ValueType.float64, ValueType.uint8 ),
        ( ValueType.float64, ValueType.uint16 ),
        ( ValueType.float64, ValueType.uint32 ),

        ( ValueType.@char, ValueType.uint8 ),
        ( ValueType.@string, ValueType.@char ),
    };
    
    public static Dictionary<ValueType, string> TypesInCPP = new()
    {
        { ValueType.@string, "string" },
        { ValueType.@bool, "bool" },
        { ValueType.float64, "double" },
        { ValueType.float32, "float" },
        { ValueType.int64, "int64_t" },
        { ValueType.int32, "int32_t" },
        { ValueType.int16, "int16_t" },
        { ValueType.int8, "int8_t" },
        { ValueType.uint64, "uint64_t" },
        { ValueType.uint32, "uint32_t" },
        { ValueType.uint16, "uint16_t" },
        { ValueType.uint8, "uint8_t" },
        { ValueType.@char, "char" },
    };

    public enum ValueType
    {
        @string,
        @bool,
        float64,
        float32,
        int64,
        int32,
        int16,
        int8,
        uint64,
        uint32,
        uint16,
        uint8,
        @char,
    }

    public static ValueType? Match(string type)
    {
        return ValueType.TryParse(type, out ValueType t) ? t : null;
    }
    
    public static bool CheckTypesAreAssignable((ValueType Type, bool isArray) target, (ValueType Type, bool isArray) source)
    {
        return target == source || (AssignableTypes.Contains((target.Type, source.Type)) && target.isArray == source.isArray);
    }
    
    public static bool CheckTypesAreAssignable((ValueType type, bool isArray)[] target, (ValueType type, bool isArray)[] source)
    {
        if (target.Length != source.Length)
        {
            return false;
        }

        for (int i = 0; i < target.Length; i++)
        {
            if (!CheckTypesAreAssignable(target[i], source[i]))
            {
                return false;
            }
        }

        return true;
    }
    
    public static bool CheckTypesAreAssignable(YALType? target, YALType? source)
    {
        if (target is null || source is null)
        {
            return false;
        }
        
        return CheckTypesAreAssignable(target.Types.ToArray(), source.Types.ToArray());

    }

    public static bool CheckCompoundExpressionTypesAreValid(YALType leftType, YALType rightType)
    {
        return CheckTypesAreAssignable(leftType, rightType) || CheckTypesAreAssignable(rightType, leftType);
    }
    
    public static YALType? GetLeastAssignableType(params YALType[] types)
    {
        YALType? type = null;
        ValueType? leastAssignableType = null;
        bool isArray = false;

        foreach (var t in types)
        {
            foreach ((ValueType valueType, bool typeIsArray) in t.Types)
            {
                if (valueType < leastAssignableType || typeIsArray || leastAssignableType is null)
                {
                    leastAssignableType = valueType;
                    isArray = typeIsArray;
                }
            }
        }

        if (leastAssignableType is ValueType vLeastAssignableType)
            type = new YALType(vLeastAssignableType, isArray);
        
        return type;
    }

    public static List<string> ToCPPType(this YALType? type)
    {
        var types = new List<string>();
        if (type is not null)
        {
            foreach (var t in type.Types)
            {
                string? cType = null;
                TypesInCPP.TryGetValue(t.Type, out cType);
                if (cType is not null)
                    types.Add(cType);
            }
        }
        return types;
    }
}