namespace mxcd.util.sql
{
    internal enum ComparationType
    {

        //
        // Summary:
        // Node representing the arithmetic sum without overflow checking.        
        Add = 0,
        //
        // Summary:
        // Node representing the arithmetic sum with overflow check.
        AddChecked = 1,
        //
        // Summary:
        // Node representing a bitwise AND operation.
        And = 2,
        //
        // Summary:
        // Node representing a conditional short circuit AND operation.
        AndAlso = 3,
        //
        // Summary:
        // Node that represents obtaining the length of a one-dimensional array.
        ArrayLength = 4,
        //
        // Summary:
        // Node representing the indexing in a one-dimensional matrix.
        ArrayIndex = 5,
        //
        // Summary:
        // Node that represents the call to a method.
        Call = 6,
        //
        // Summary:
        // Node representing a combined use operation of null.
        Coalesce = 7,
        //
        // Summary:
        // Node that represents a conditional operation.
        Conditional = 8,
        //
        // Summary:
        // Node that represents an expression that has a constant value.
        Constant = 9,
        //
        // Summary:
        // Node that represents a conversion operation. If the operation is a conversion
        // numeric, an overflow automatically occurs if the converted value
        // does not fit the type of destination.
        Convert = 10,
        //
        // Summary:
        // Node that represents a conversion operation. If the operation is a conversion
        // numeric, an exception occurs if the converted value does not fit the type
        //     of destiny.
        ConvertChecked = 11,
        //
        // Summary:
        // Node representing the arithmetic division.
        Divide = 12,
        //
        // Summary:
        // Node that represents an equality comparison.
        Equal = 13,
        //
        // Summary:
        // Node that represents a bit-by-bit XOR operation.
        ExclusiveOr = 14,
        //
        // Summary:
        // Node that represents a numerical comparison "greater than".
        GreaterThan = 15,
        //
        // Summary:
        // Node that represents a numerical comparison "greater than or equal to".
        GreaterThanOrEqual = 16,
        //
        // Summary:
        // Node representing the application of a delegate or a lambda expression to a
        // list of argument expressions.
        Invoke = 17,
        //
        // Summary:
        // Node that represents a lambda expression.
        Lambda = 18,
        //
        // Summary:
        // Node that represents a bitwise left shift operation.
        LeftShift = 19,
        //
        // Summary:
        // Node that represents a numerical comparison "less than".
        LessThan = 20,
        //
        // Summary:
        // Node representing a numerical comparison "less than or equal to".
        LessThanOrEqual = 21,
        //
        // Summary:
        // Node representing the creation of a new System.Collections.IEnumerable object
        // and its initialization from a list of elements.
        ListInit = 22,
        //
        // Summary:
        // Node that represents the reading of a field or property.
        MemberAccess = 23,
        //
        // Summary:
        // Node that represents the creation of a new object and the initialization of one
        // or several of its members.
        MemberInit = 24,
        //
        // Summary:
        // Node representing an arithmetic rest operation.
        Module = 25,
        //
        // Summary:
        // Node representing arithmetic multiplication without overflow checking.
        Multiply = 26,
        //
        // Summary:
        // Node representing arithmetic multiplication with overflow checking.
        MultiplyChecked = 27,
        //
        // Summary:
        // Node representing an arithmetic denial operation.
        Negate = 28,
        //
        // Summary:
        // Node representing a unary operation +. The result of a unary operation
        // + predefined is simply the value of the operand, but the implementations
        // user-defined may have non-trivial results.
        UnaryPlus = 29,
        //
        // Summary:
        // Node representing an arithmetic denial operation with verification of
        // overflow.
        NegateChecked = 30,
        //
        // Summary:
        // Node that represents the call to a constructor to create a new object.
        New = 31,
        //
        // Summary:
        // Node that represents the creation of a new one-dimensional matrix and its initialization
        // from a list of items.
        NewArrayInit = 32,
        //
        // Summary:
        // Node that represents the creation of a new matrix where the
        // limits of each dimension.
        NewArrayBounds = 33,
        //
        // Summary:
        // Node that represents a bit-by-bit complement operation.
        Not = 34,
        //
        // Summary:
        // Node that represents a comparison of inequality.
        NotEqual = 35,
        //
        // Summary:
        // Node representing a bitwise OR operation.
        Or = 36,
        //
        // Summary:
        // Node representing a conditional short-circuit OR operation.
        OrElse = 37,
        //
        // Summary:
        // Node representing a reference to a parameter defined in the context of
        //     The expression.
        Parameter = 38,
        //
        // Summary:
        // Node representing the elevation of a number to a power.
        Power = 39,
        //
        // Summary:
        // Node that represents an expression that has a constant value of type System.Linq.Expressions.Expression.
        // A System.Linq.Expressions.ExpressionType.Quote node can contain references
        // to the parameters defined in the context of the expression it represents.
        Quote = 40,
        //
        // Summary:
        // Node representing a bitwise operation of right shift.
        RightShift = 41,
        //
        // Summary:
        // Node representing arithmetic subtraction without overflow checking.
        Subtract = 42,
        //
        // Summary:
        // Node representing arithmetic subtraction with overflow check.
        SubtractChecked = 43,
        //
        // Summary:
        // Node representing an explicit reference or boxing conversion where it is provided
        // the null value if the conversion fails.
        TypeAs = 44,
        //
        // Summary:
        // Node representing a type check.
        TypeIs = 45,
        ///
        /// Default value
        ///
        Default = -1
    }
}