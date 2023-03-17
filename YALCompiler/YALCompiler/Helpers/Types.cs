namespace YALCompiler.Helpers;

public static class Types
{
    public static List<(ValueType, ValueType)> AssignableTypes = new()
    {
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

        ( ValueType.float64, ValueType.int8 ),
        ( ValueType.float64, ValueType.int16 ),
        ( ValueType.float64, ValueType.int32 ),
        ( ValueType.float64, ValueType.uint8 ),
        ( ValueType.float64, ValueType.uint16 ),
        ( ValueType.float64, ValueType.uint32 ),

        ( ValueType.@char, ValueType.uint8 ),
        ( ValueType.@string, ValueType.@char ),
    };
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
    
    public static bool CheckTypesAreAssignable(ValueType target, ValueType source)
    {
        return target == source || AssignableTypes.Contains((target, source));
    }
    
    public static bool CheckTypesAreAssignable(ValueType[] target, ValueType[] source)
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
        
        if (target is SingleType targetSingleType && source is SingleType sourceSingleType)
        {
            return CheckTypesAreAssignable(targetSingleType.Type, sourceSingleType.Type);
        }

        if (target is TupleType targetTupleType && source is SingleType sourceSingleType2)
        {
            return targetTupleType.Types.Count == 1 && CheckTypesAreAssignable(targetTupleType.Types[0].Type, sourceSingleType2.Type);
        }
        
        if (target is SingleType targetSingleType2 && source is TupleType sourceTupleType)
        {
            return sourceTupleType.Types.Count == 1 && CheckTypesAreAssignable(targetSingleType2.Type, sourceTupleType.Types[0].Type);
        }
        
        if (target is TupleType targetSingleType3 && source is TupleType sourceTupleType3)
        {
            return CheckTypesAreAssignable(targetSingleType3.Types.Select(t => t.Type).ToArray(), 
                sourceTupleType3.Types.Select(t => t.Type).ToArray());
        }
        
        return false;
        
    }
}