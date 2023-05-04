namespace YALCompiler.Helpers;

public static class Operators
{
    private static readonly Dictionary<Types.ValueType, TypeMasks> TypeMaskMatching = new()
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
        Or = 0x1 << 1 ^ TypeMasks.Bool,
        LessThan = 0x1 << 2 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        LessThanOrEqual = 0x1 << 3 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        GreaterThan = 0x1 << 4 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        GreaterThanOrEqual = 0x1 << 5 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        Equals = 0x1 << 6 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float ^ TypeMasks.String ^ TypeMasks.Bool,
        NotEquals = 0x1 << 7 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float ^ TypeMasks.String ^ TypeMasks.Bool,
    }

    public enum ExpressionOperator
    {
        Multiplication = 0x1 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        Division = 0x1 << 1 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        Modulo = 0x1 << 2 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        Addition = 0x1 << 3 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float ^ TypeMasks.String,
        Subtraction = 0x1 << 4 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        LeftShift = 0x1 << 5 ^ TypeMasks.IntUint ^ TypeMasks.Char,
        RightShift = 0x1 << 6 ^ TypeMasks.IntUint ^ TypeMasks.Char,
        BitwiseAnd = 0x1 << 7 ^ TypeMasks.IntUint ^ TypeMasks.Char,
        BitwiseXor = 0x1 << 8 ^ TypeMasks.IntUint ^ TypeMasks.Char,
        BitwiseOr = 0x1 << 9 ^ TypeMasks.IntUint ^ TypeMasks.Char,
        BitwiseNot = 0x1 << 10 ^ TypeMasks.IntUint ^ TypeMasks.Char,
        PreIncrement = 0x1 << 11 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PreDecrement = 0x1 << 12 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PostIncrement = 0x1 << 13 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PostDecrement = 0x1 << 14 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
    }

    public enum AssignmentOperator
    {
        Equals = 0x1 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float ^ TypeMasks.String ^ TypeMasks.Bool,
        AdditionAssignment = 0x1 << 1 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float ^ TypeMasks.String,
        SubtractionAssignment = 0x1 << 2 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        MultiplicationAssignment = 0x1 << 3 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        DivisionAssignment = 0x1 << 4 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        ModuloAssignment = 0x1 << 5 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PreIncrement = 0x1 << 6 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PreDecrement = 0x1 << 7 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PostIncrement = 0x1 << 8 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        PostDecrement = 0x1 << 9 ^ TypeMasks.IntUint ^ TypeMasks.Char ^ TypeMasks.Float,
        BitwiseNot = 0x1 << 10 ^ TypeMasks.IntUint ^ TypeMasks.Char,
    }

    public enum TypeMasks
    {
        IntUint = 0x1 << 31,
        @Char = 0x1 << 30,
        @Float = 0x1 << 29,
        @String = 0x1 << 28,
        @Bool = 0x1 << 27,
    }

    public static bool CheckOperationIsValid(YALType type, ExpressionOperator @operator)
    {
        return type.Types is [{ IsArray: false } _] &&
               ((int)TypeMaskMatching[type.Types[0].Type] & (int)@operator) != 0;
    }

    public static bool CheckOperationIsValid(YALType type, AssignmentOperator @operator)
    {
        foreach (var valueTypeAndArrayFlag in type.Types)
        {
            if ((valueTypeAndArrayFlag.IsArray && @operator != AssignmentOperator.Equals) ||
                ((int)TypeMaskMatching[valueTypeAndArrayFlag.Type] & (int)@operator) == 0)
            {
                return false;
            }
        }

        return true;
    }

    public static bool CheckOperationIsValid(YALType type, PredicateOperator @operator)
    {
        return type.Types is [{ IsArray: false } _] &&
               ((int)TypeMaskMatching[type.Types[0].Type] & (int)@operator) != 0;
    }

    public static string ToStringValue(this ExpressionOperator @operator) => @operator switch {
        ExpressionOperator.Multiplication => "*",
        ExpressionOperator.Division => "/",
        ExpressionOperator.Modulo => "%",
        ExpressionOperator.Addition => "+",
        ExpressionOperator.Subtraction => "-",
        ExpressionOperator.LeftShift => "<<",
        ExpressionOperator.RightShift => ">>",
        ExpressionOperator.BitwiseAnd => "&",
        ExpressionOperator.BitwiseXor => "^",
        ExpressionOperator.BitwiseOr => "|",
        ExpressionOperator.BitwiseNot => "~",
        ExpressionOperator.PreIncrement => "++",
        ExpressionOperator.PreDecrement => "--",
        ExpressionOperator.PostIncrement => "++",
        ExpressionOperator.PostDecrement => "--",
        _ => throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null)
    };
    
    public static string ToStringValue(this AssignmentOperator @operator) => @operator switch {
        AssignmentOperator.Equals => "=",
        AssignmentOperator.AdditionAssignment => "+=",
        AssignmentOperator.SubtractionAssignment => "-=",
        AssignmentOperator.MultiplicationAssignment => "*=",
        AssignmentOperator.DivisionAssignment => "/=",
        AssignmentOperator.ModuloAssignment => "%=",
        AssignmentOperator.PreIncrement => "++",
        AssignmentOperator.PreDecrement => "--",
        AssignmentOperator.PostIncrement => "++",
        AssignmentOperator.PostDecrement => "--",
        AssignmentOperator.BitwiseNot => "~",
        _ => throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null)
    };
    
    public static string ToStringValue(this PredicateOperator @operator) => @operator switch {
        PredicateOperator.LessThan => "<",
        PredicateOperator.LessThanOrEqual => "<=",
        PredicateOperator.GreaterThan => ">",
        PredicateOperator.GreaterThanOrEqual => ">=",
        PredicateOperator.Equals => "==",
        PredicateOperator.NotEquals => "!=",
        _ => throw new ArgumentOutOfRangeException(nameof(@operator), @operator, null)
    };
}