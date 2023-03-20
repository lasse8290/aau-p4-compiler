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
        And = 0b1 ^ TypeMasks.Bool,
        Or = 0b10 ^ TypeMasks.Bool,
        LessThan = 0b100 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        LessThanOrEqual = 0b1_000 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        GreaterThan = 0b10_000 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        GreaterThanOrEqual = 0b100_000 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        Equals = 0b1_000_000 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float ^ TypeMasks.String ^ TypeMasks.Bool,
        NotEquals = 0b10_000_000 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float ^ TypeMasks.String ^ TypeMasks.Bool,
    }

    public enum ExpressionOperator
    {
        Multiplication = 0b1 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        Division = 0b10 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        Modulo = 0b100 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        Addition = 0b1_000 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float ^ TypeMasks.String,
        Subtraction = 0b10_000 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        LeftShift = 0b100_000 ^ TypeMasks.IntUint ^ TypeMasks.Char,
        RightShift = 0b1_000_000 ^ TypeMasks.IntUint ^ TypeMasks.Char,
        BitwiseAnd = 0b10_000_000 ^ TypeMasks.IntUint ^ TypeMasks.Char,
        BitwiseXor = 0b100_000_000 ^ TypeMasks.IntUint ^ TypeMasks.Char,
        BitwiseOr = 0b1_000_000_000 ^ TypeMasks.IntUint ^ TypeMasks.Char,
        BitwiseNot = 0b10_000_000_000 ^ TypeMasks.IntUint ^ TypeMasks.Char,
        PreIncrement = 0b100_000_000_000 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PreDecrement = 0b1_000_000_000_000 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PostIncrement = 0b10_000_000_000_000 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PostDecrement = 0b100_000_000_000_000 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
    }

    public enum AssignmentOperator
    {
        Equals = 0b1 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float ^ TypeMasks.String ^ TypeMasks.Bool,
        AdditionAssignment = 0b10 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float ^ TypeMasks.String,
        SubtractionAssignment = 0b100 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        MultiplicationAssignment = 0b1_000 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        DivisionAssignment = 0b10_000 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        ModuloAssignment = 0b100_000 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PreIncrement = 0b1_000_000 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PreDecrement = 0b10_000_000 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PostIncrement = 0b100_000_000 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PostDecrement = 0b1_000_000_000 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
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