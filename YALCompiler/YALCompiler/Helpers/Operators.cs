namespace YALCompiler.Helpers;

public static class Operators
{
    private static readonly Dictionary<Types.ValueType, TypeMasks> TypeMaskMatching = new ()
    {
        { Types.ValueType.int8, TypeMasks.IntUint },
        { Types.ValueType.int16, TypeMasks.IntUint },
        { Types.ValueType.int32, TypeMasks.IntUint },
        { Types.ValueType.int64, TypeMasks.IntUint },
        { Types.ValueType.uint8, TypeMasks.IntUint },
        { Types.ValueType.uint16, TypeMasks.IntUint },
        { Types.ValueType.uint32, TypeMasks.IntUint },
        { Types.ValueType.uint64, TypeMasks.IntUint },
        { Types.ValueType.@char, TypeMasks.Char },
        { Types.ValueType.float32, TypeMasks.Float },        
        { Types.ValueType.float64, TypeMasks.Float },
        { Types.ValueType.@string, TypeMasks.String },
        { Types.ValueType.@bool, TypeMasks.Bool },
    };
    public enum PredicateOperator
    {
        And = 0x1 ^ TypeMasks.Bool,
        Or = 0x2 ^ TypeMasks.Bool,
        LessThan = 0x3 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        LessThanOrEqual = 0x4 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        GreaterThan = 0x5 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        GreaterThanOrEqual = 0x6 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        Equals = 0x3 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float ^ TypeMasks.String ^ TypeMasks.Bool,
        NotEquals = 0x3 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float ^ TypeMasks.String ^ TypeMasks.Bool,
    }

    public enum ExpressionOperator
    {
        Multiplication = 0x1 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        Division = 0x2 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        Modulo = 0x3 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        Addition = 0x4 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float ^ TypeMasks.String,
        Subtraction = 0x5 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        LeftShift = 0x6 ^ TypeMasks.IntUint ^ TypeMasks.Char,
        RightShift = 0x7 ^ TypeMasks.IntUint ^ TypeMasks.Char,
        BitwiseAnd = 0x8 ^ TypeMasks.IntUint ^ TypeMasks.Char,
        BitwiseXor = 0x9 ^ TypeMasks.IntUint ^ TypeMasks.Char,
        BitwiseOr = 0xA ^ TypeMasks.IntUint ^ TypeMasks.Char,
        BitwiseNot = 0xB ^ TypeMasks.IntUint ^ TypeMasks.Char,
        PreIncrement = 0xC ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PreDecrement = 0xD ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PostIncrement = 0xE ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PostDecrement = 0xF ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
    }

    public enum AssignmentOperator
    {
        Equals = 0x1 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float ^ TypeMasks.String ^ TypeMasks.Bool,
        AdditionAssignment = 0x2 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float ^ TypeMasks.String,
        SubtractionAssignment = 0x3 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        MultiplicationAssignment = 0x4 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        DivisionAssignment = 0x5 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        ModuloAssignment = 0x6 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PreIncrement = 0x7 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PreDecrement = 0x8 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PostIncrement = 0x9 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PostDecrement = 0xA ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
    }
    
    public enum TypeMasks
    {
        IntUint = 0x1 << 31,
        @Char = 0x1 << 30,
        @Float = 0x1 << 29,
        @String = 0x1 << 28,
        @Bool = 0x1 << 27,
    }
    
    public static bool CheckOperationIsValid(Types.ValueType type, ExpressionOperator @operator)
    {
        return ((int)TypeMaskMatching[type] & (int)@operator) != 0;
    }
    
    public static bool CheckOperationIsValid(Types.ValueType type, AssignmentOperator @operator)
    {
        return ((int)TypeMaskMatching[type] & (int)@operator) != 0;
    }
    
    public static bool CheckOperationIsValid(Types.ValueType type, PredicateOperator @operator)
    {
        return ((int)TypeMaskMatching[type] & (int)@operator) != 0;
    }
}