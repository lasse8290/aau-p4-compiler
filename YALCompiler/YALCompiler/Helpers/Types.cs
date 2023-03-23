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
    
    public static Dictionary<ValueType, string> TypesInC = new()
    {
        { ValueType.@string, "char*" },
        { ValueType.@bool, "bool" },
        { ValueType.float64, "double" },
        { ValueType.float32, "float" },
        { ValueType.int64, "long" },
        { ValueType.int32, "int" },
        { ValueType.int16, "short" },
        { ValueType.int8, "char" },
        { ValueType.uint64, "unsigned long" },
        { ValueType.uint32, "unsigned int" },
        { ValueType.uint16, "unsigned short" },
        { ValueType.uint8, "unsigned char" },
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
            return CheckTypesAreAssignable(targetSingleType.Type, sourceSingleType.Type) &&
                   targetSingleType.IsArray == sourceSingleType.IsArray;
        }

        if (target is TupleType targetTupleType && source is SingleType sourceSingleType2)
        {
            return targetTupleType.Types.Count == 1 && 
                   CheckTypesAreAssignable(targetTupleType.Types[0].Type, sourceSingleType2.Type)
                   && targetTupleType.Types[0].IsArray == sourceSingleType2.IsArray;
        }
        
        if (target is SingleType targetSingleType2 && source is TupleType sourceTupleType)
        {
            return sourceTupleType.Types.Count == 1 && 
                   CheckTypesAreAssignable(targetSingleType2.Type, sourceTupleType.Types[0].Type)
                   && sourceTupleType.Types[0].IsArray == targetSingleType2.IsArray;
        }
        
        if (target is TupleType targetSingleType3 && source is TupleType sourceTupleType3)
        {
            return CheckTypesAreAssignable(
                targetSingleType3.Types.Select(t => t.Type).ToArray(), 
                sourceTupleType3.Types.Select(t => t.Type).ToArray())
                && targetSingleType3.Types.Select(t => t.IsArray).
                    SequenceEqual(sourceTupleType3.Types.Select(t => t.IsArray));
        }
        
        return false;
        
    }

    public static bool CheckCompoundExpressionTypesAreValid(YALType leftType, YALType rightType)
    {
        if (leftType is SingleType && rightType is SingleType)
        {
            return CheckTypesAreAssignable(leftType, rightType) || CheckTypesAreAssignable(rightType, leftType);
        }

        return false;
    }

    public static SingleType? GetLeastAssignableType(SingleType leftType, SingleType rightType)
    {
        return new SingleType(leftType.Type < rightType.Type ? leftType.Type : rightType.Type);
    }

    public static string? ToCType(this YALType? type)
    {
        if (type is SingleType singleType)
        {
            string? cType = null;
            TypesInC.TryGetValue(singleType.Type, out cType);
            return cType;
        }

        return null;
    }
}