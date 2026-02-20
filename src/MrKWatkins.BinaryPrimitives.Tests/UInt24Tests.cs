using System.Globalization;

namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class UInt24Tests
{
    #region Constants and Properties

    [Test]
    public void MinValue() => ((int)UInt24.MinValue).Should().Equal(0);

    [Test]
    public void MaxValue() => ((int)UInt24.MaxValue).Should().Equal(16777215);

    [Test]
    public void Zero() => ((int)UInt24.Zero).Should().Equal(0);

    [Test]
    public void One() => ((int)UInt24.One).Should().Equal(1);

    [Test]
    public void Radix() => UInt24.Radix.Should().Equal(2);

    [Test]
    public void AdditiveIdentity() => ((int)UInt24.AdditiveIdentity).Should().Equal(0);

    [Test]
    public void MultiplicativeIdentity() => ((int)UInt24.MultiplicativeIdentity).Should().Equal(1);

    #endregion

    #region Equality

    [TestCase(0, 0, true)]
    [TestCase(100, 100, true)]
    [TestCase(100, 200, false)]
    [TestCase(0, 1, false)]
    public void Equals_UInt24(int left, int right, bool expected)
        => ((UInt24)left).Equals((UInt24)right).Should().Equal(expected);

    [TestCase(100, true)]
    [TestCase(200, false)]
    public void Equals_Object(int other, bool expected)
        => ((UInt24)100).Equals((object)(UInt24)other).Should().Equal(expected);

    [Test]
    public void Equals_Object_Null()
        => ((UInt24)100).Equals(null).Should().Equal(false);

    [Test]
    public void Equals_Object_WrongType()
        => ((UInt24)100).Equals((object)100).Should().Equal(false);

    [TestCase(100, 100)]
    [TestCase(0, 0)]
    [TestCase(16777215, 16777215)]
    public void GetHashCode_EqualValues(int left, int right)
        => ((UInt24)left).GetHashCode().Should().Equal(((UInt24)right).GetHashCode());

    #endregion

    #region CompareTo

    [TestCase(100, 200, -1)]
    [TestCase(200, 200, 0)]
    [TestCase(200, 100, 1)]
    public void CompareTo_UInt24(int left, int right, int expectedSign)
        => Math.Sign(((UInt24)left).CompareTo((UInt24)right)).Should().Equal(expectedSign);

    [TestCase(100, 200, -1)]
    [TestCase(200, 200, 0)]
    [TestCase(200, 100, 1)]
    public void CompareTo_Object(int left, int right, int expectedSign)
        => Math.Sign(((UInt24)left).CompareTo((object)(UInt24)right)).Should().Equal(expectedSign);

    [Test]
    public void CompareTo_Object_Null()
        => ((UInt24)100).CompareTo(null).Should().Equal(1);

    [Test]
    public void CompareTo_Object_WrongType()
        => AssertThat.Invoking(() => ((UInt24)100).CompareTo((object)100)).Should().Throw<ArgumentException>();

    #endregion

    #region Comparison Operators

    [TestCase(100, 100, true)]
    [TestCase(100, 200, false)]
    public void EqualityOperator(int left, int right, bool expected)
        => ((UInt24)left == (UInt24)right).Should().Equal(expected);

    [TestCase(100, 100, false)]
    [TestCase(100, 200, true)]
    public void InequalityOperator(int left, int right, bool expected)
        => ((UInt24)left != (UInt24)right).Should().Equal(expected);

    [TestCase(100, 200, true)]
    [TestCase(200, 200, false)]
    [TestCase(200, 100, false)]
    public void LessThanOperator(int left, int right, bool expected)
        => ((UInt24)left < (UInt24)right).Should().Equal(expected);

    [TestCase(200, 100, true)]
    [TestCase(200, 200, false)]
    [TestCase(100, 200, false)]
    public void GreaterThanOperator(int left, int right, bool expected)
        => ((UInt24)left > (UInt24)right).Should().Equal(expected);

    [TestCase(100, 200, true)]
    [TestCase(200, 200, true)]
    [TestCase(200, 100, false)]
    public void LessThanOrEqualOperator(int left, int right, bool expected)
        => ((UInt24)left <= (UInt24)right).Should().Equal(expected);

    [TestCase(200, 100, true)]
    [TestCase(200, 200, true)]
    [TestCase(100, 200, false)]
    public void GreaterThanOrEqualOperator(int left, int right, bool expected)
        => ((UInt24)left >= (UInt24)right).Should().Equal(expected);

    #endregion

    #region Arithmetic Operators

    [TestCase(0, 1, 1)]
    [TestCase(100, 200, 300)]
    [TestCase(16777215, 1, 0)]
    [TestCase(16777215, 2, 1)]
    public void Addition(int left, int right, int expected)
        => ((int)((UInt24)left + (UInt24)right)).Should().Equal(expected);

    [TestCase(1, 0, 1)]
    [TestCase(300, 200, 100)]
    [TestCase(0, 1, 16777215)]
    public void Subtraction(int left, int right, int expected)
        => ((int)((UInt24)left - (UInt24)right)).Should().Equal(expected);

    [TestCase(100, 200, 20000)]
    [TestCase(5000, 5000, 8222784)]
    public void Multiplication(int left, int right, int expected)
        => ((int)((UInt24)left * (UInt24)right)).Should().Equal(expected);

    [TestCase(200, 100, 2)]
    [TestCase(10, 3, 3)]
    [TestCase(0, 1, 0)]
    public void Division(int left, int right, int expected)
        => ((int)((UInt24)left / (UInt24)right)).Should().Equal(expected);

    [TestCase(10, 3, 1)]
    [TestCase(200, 100, 0)]
    [TestCase(7, 4, 3)]
    public void Modulus(int left, int right, int expected)
        => ((int)((UInt24)left % (UInt24)right)).Should().Equal(expected);

    [TestCase(0, 1)]
    [TestCase(100, 101)]
    [TestCase(16777215, 0)]
    public void Increment(int value, int expected)
    {
        var v = (UInt24)value;
        v++;
        ((int)v).Should().Equal(expected);
    }

    [TestCase(1, 0)]
    [TestCase(100, 99)]
    [TestCase(0, 16777215)]
    public void Decrement(int value, int expected)
    {
        var v = (UInt24)value;
        v--;
        ((int)v).Should().Equal(expected);
    }

    [TestCase(0)]
    [TestCase(100)]
    [TestCase(16777215)]
    public void UnaryPlus(int value)
        => ((int)(+(UInt24)value)).Should().Equal(value);

    [TestCase(0, 0)]
    [TestCase(1, 16777215)]
    public void UnaryMinus(int value, int expected)
        => ((int)(-(UInt24)value)).Should().Equal(expected);

    #endregion

    #region Checked Arithmetic

    [Test]
    public void CheckedAddition()
        => AssertThat.Invoking(() => checked((UInt24)16777215 + (UInt24)1)).Should().Throw<OverflowException>();

    [TestCase(0, 1, 1)]
    [TestCase(100, 200, 300)]
    public void CheckedAddition_NoOverflow(int left, int right, int expected)
        => ((int)checked((UInt24)left + (UInt24)right)).Should().Equal(expected);

    [Test]
    public void CheckedSubtraction()
        => AssertThat.Invoking(() => checked((UInt24)0 - (UInt24)1)).Should().Throw<OverflowException>();

    [TestCase(300, 200, 100)]
    [TestCase(1, 0, 1)]
    public void CheckedSubtraction_NoOverflow(int left, int right, int expected)
        => ((int)checked((UInt24)left - (UInt24)right)).Should().Equal(expected);

    [Test]
    public void CheckedMultiplication()
        => AssertThat.Invoking(() => checked((UInt24)16777215 * (UInt24)2)).Should().Throw<OverflowException>();

    [TestCase(100, 200, 20000)]
    public void CheckedMultiplication_NoOverflow(int left, int right, int expected)
        => ((int)checked((UInt24)left * (UInt24)right)).Should().Equal(expected);

    [Test]
    public void CheckedIncrement()
    {
        var v = UInt24.MaxValue;
        AssertThat.Invoking(() => checked(v++)).Should().Throw<OverflowException>();
    }

    [Test]
    public void CheckedDecrement()
    {
        var v = UInt24.MinValue;
        AssertThat.Invoking(() => checked(v--)).Should().Throw<OverflowException>();
    }

    [Test]
    public void CheckedUnaryMinus()
        => AssertThat.Invoking(() => checked(-(UInt24)1)).Should().Throw<OverflowException>();

    [Test]
    public void CheckedUnaryMinus_Zero()
        => ((int)checked(-(UInt24)0)).Should().Equal(0);

    #endregion

    #region Bitwise Operators

    [TestCase(0xFF00FF, 0x00FF00, 0x000000)]
    [TestCase(0xFFFFFF, 0x00FF00, 0x00FF00)]
    public void BitwiseAnd(int left, int right, int expected)
        => ((int)((UInt24)left & (UInt24)right)).Should().Equal(expected);

    [TestCase(0xFF0000, 0x00FF00, 0xFFFF00)]
    [TestCase(0x000000, 0x00FF00, 0x00FF00)]
    public void BitwiseOr(int left, int right, int expected)
        => ((int)((UInt24)left | (UInt24)right)).Should().Equal(expected);

    [TestCase(0xFF00FF, 0xFF0000, 0x0000FF)]
    [TestCase(0xFFFFFF, 0x000000, 0xFFFFFF)]
    public void BitwiseXor(int left, int right, int expected)
        => ((int)((UInt24)left ^ (UInt24)right)).Should().Equal(expected);

    [TestCase(0x000000, 0xFFFFFF)]
    [TestCase(0xFF00FF, 0x00FF00)]
    [TestCase(0xFFFFFF, 0x000000)]
    public void BitwiseNot(int value, int expected)
        => ((int)(~(UInt24)value)).Should().Equal(expected);

    [TestCase(1, 1, 2)]
    [TestCase(1, 8, 256)]
    [TestCase(0x800000, 1, 0)]
    public void LeftShift(int value, int shift, int expected)
        => ((int)((UInt24)value << shift)).Should().Equal(expected);

    [TestCase(256, 1, 128)]
    [TestCase(256, 8, 1)]
    [TestCase(0xFFFFFF, 12, 0xFFF)]
    public void RightShift(int value, int shift, int expected)
        => ((int)((UInt24)value >> shift)).Should().Equal(expected);

    [TestCase(256, 1, 128)]
    [TestCase(0xFFFFFF, 12, 0xFFF)]
    public void UnsignedRightShift(int value, int shift, int expected)
        => ((int)((UInt24)value >>> shift)).Should().Equal(expected);

    #endregion

    #region Conversion Operators - Implicit TO UInt24

    [TestCase(0)]
    [TestCase(255)]
    public void ImplicitFromByte(int value)
    {
        UInt24 result = (byte)value;
        ((int)result).Should().Equal(value);
    }

    [TestCase(0)]
    [TestCase(65535)]
    public void ImplicitFromUInt16(int value)
    {
        UInt24 result = (ushort)value;
        ((int)result).Should().Equal(value);
    }

    #endregion

    #region Conversion Operators - Implicit FROM UInt24

    [TestCase(0)]
    [TestCase(16777215)]
    public void ImplicitToUInt32(int value)
    {
        uint result = (UInt24)value;
        result.Should().Equal((uint)value);
    }

    [TestCase(0)]
    [TestCase(16777215)]
    public void ImplicitToInt32(int value)
    {
        int result = (UInt24)value;
        result.Should().Equal(value);
    }

    [TestCase(0)]
    [TestCase(16777215)]
    public void ImplicitToInt64(int value)
    {
        long result = (UInt24)value;
        result.Should().Equal((long)value);
    }

    [TestCase(0)]
    [TestCase(16777215)]
    public void ImplicitToUInt64(int value)
    {
        ulong result = (UInt24)value;
        result.Should().Equal((ulong)value);
    }

    #endregion

    #region Conversion Operators - Explicit TO UInt24

    [Test]
    public void ExplicitFromInt32_Negative()
        => AssertThat.Invoking(() => _ = (UInt24)(-1)).Should().Throw<OverflowException>();

    [Test]
    public void ExplicitFromInt32_TooLarge()
        => AssertThat.Invoking(() => _ = (UInt24)16777216).Should().Throw<OverflowException>();

    [TestCase(0)]
    [TestCase(16777215)]
    public void ExplicitFromInt32(int value)
        => ((int)(UInt24)value).Should().Equal(value);

    [Test]
    public void ExplicitFromUInt32_TooLarge()
        => AssertThat.Invoking(() => _ = (UInt24)16777216u).Should().Throw<OverflowException>();

    [Test]
    public void ExplicitFromInt64_Negative()
        => AssertThat.Invoking(() => _ = (UInt24)(-1L)).Should().Throw<OverflowException>();

    [Test]
    public void ExplicitFromInt64_TooLarge()
        => AssertThat.Invoking(() => _ = (UInt24)16777216L).Should().Throw<OverflowException>();

    [Test]
    public void ExplicitFromSByte_Negative()
        => AssertThat.Invoking(() => _ = (UInt24)(sbyte)-1).Should().Throw<OverflowException>();

    [Test]
    public void ExplicitFromInt16_Negative()
        => AssertThat.Invoking(() => _ = (UInt24)(short)-1).Should().Throw<OverflowException>();

    #endregion

    #region Conversion Operators - Explicit FROM UInt24

    [Test]
    public void ExplicitToByte_TooLarge()
        => AssertThat.Invoking(() => _ = (byte)(UInt24)256).Should().Throw<OverflowException>();

    [TestCase(0)]
    [TestCase(255)]
    public void ExplicitToByte(int value)
        => ((byte)(UInt24)value).Should().Equal((byte)value);

    [Test]
    public void ExplicitToSByte_TooLarge()
        => AssertThat.Invoking(() => _ = (sbyte)(UInt24)128).Should().Throw<OverflowException>();

    [TestCase(0)]
    [TestCase(127)]
    public void ExplicitToSByte(int value)
        => ((sbyte)(UInt24)value).Should().Equal((sbyte)value);

    [Test]
    public void ExplicitToInt16_TooLarge()
        => AssertThat.Invoking(() => _ = (short)(UInt24)32768).Should().Throw<OverflowException>();

    [TestCase(0)]
    [TestCase(32767)]
    public void ExplicitToInt16(int value)
        => ((short)(UInt24)value).Should().Equal((short)value);

    [Test]
    public void ExplicitToUInt16_TooLarge()
        => AssertThat.Invoking(() => _ = (ushort)(UInt24)65536).Should().Throw<OverflowException>();

    [TestCase(0)]
    [TestCase(65535)]
    public void ExplicitToUInt16(int value)
        => ((ushort)(UInt24)value).Should().Equal((ushort)value);

    #endregion

    #region Formatting

    [TestCase(0, "0")]
    [TestCase(12345, "12345")]
    [TestCase(16777215, "16777215")]
    public void ToString_NoArgs(int value, string expected)
        => ((UInt24)value).ToString().Should().Equal(expected);

    [TestCase(255, "X4", "00FF")]
    [TestCase(16777215, "X6", "FFFFFF")]
    public void ToString_FormatProvider(int value, string format, string expected)
        => ((UInt24)value).ToString(format, CultureInfo.InvariantCulture).Should().Equal(expected);

    #endregion

    #region Parsing

    [TestCase("0", 0)]
    [TestCase("12345", 12345)]
    [TestCase("16777215", 16777215)]
    public void Parse_String(string input, int expected)
        => ((int)UInt24.Parse(input, CultureInfo.InvariantCulture)).Should().Equal(expected);

    [Test]
    public void Parse_String_Overflow()
        => AssertThat.Invoking(() => UInt24.Parse("16777216", CultureInfo.InvariantCulture)).Should().Throw<OverflowException>();

    [TestCase("0", true, 0)]
    [TestCase("12345", true, 12345)]
    [TestCase("16777215", true, 16777215)]
    [TestCase("16777216", false, 0)]
    [TestCase("abc", false, 0)]
    public void TryParse_String(string? input, bool expectedResult, int expectedValue)
    {
        UInt24.TryParse(input, CultureInfo.InvariantCulture, out var result).Should().Equal(expectedResult);
        ((int)result).Should().Equal(expectedValue);
    }

    #endregion

    #region INumber

    [TestCase(50, 0, 100, 50)]
    [TestCase(0, 10, 100, 10)]
    [TestCase(200, 10, 100, 100)]
    public void Clamp(int value, int min, int max, int expected)
        => ((int)UInt24.Clamp((UInt24)value, (UInt24)min, (UInt24)max)).Should().Equal(expected);

    [TestCase(100, 200, 200)]
    [TestCase(200, 100, 200)]
    [TestCase(100, 100, 100)]
    public void Max(int x, int y, int expected)
        => ((int)UInt24.Max((UInt24)x, (UInt24)y)).Should().Equal(expected);

    [TestCase(100, 200, 100)]
    [TestCase(200, 100, 100)]
    [TestCase(100, 100, 100)]
    public void Min(int x, int y, int expected)
        => ((int)UInt24.Min((UInt24)x, (UInt24)y)).Should().Equal(expected);

    [TestCase(0, 0)]
    [TestCase(1, 1)]
    [TestCase(16777215, 1)]
    public void Sign(int value, int expected)
        => UInt24.Sign((UInt24)value).Should().Equal(expected);

    [TestCase(0, 0)]
    [TestCase(100, 100)]
    [TestCase(16777215, 16777215)]
    public void Abs(int value, int expected)
        => ((int)UInt24.Abs((UInt24)value)).Should().Equal(expected);

    #endregion

    #region IBinaryNumber

    [TestCase(0, false)]
    [TestCase(1, true)]
    [TestCase(2, true)]
    [TestCase(3, false)]
    [TestCase(4, true)]
    [TestCase(256, true)]
    [TestCase(100, false)]
    public void IsPow2(int value, bool expected)
        => UInt24.IsPow2((UInt24)value).Should().Equal(expected);

    [TestCase(1, 0)]
    [TestCase(2, 1)]
    [TestCase(4, 2)]
    [TestCase(256, 8)]
    [TestCase(16777215, 23)]
    public void Log2(int value, int expected)
        => ((int)UInt24.Log2((UInt24)value)).Should().Equal(expected);

    #endregion

    #region IBinaryInteger

    [Test]
    public void GetByteCount()
        => ((UInt24)0).GetByteCount().Should().Equal(3);

    [TestCase(0, 24)]
    [TestCase(1, 23)]
    [TestCase(0xFF, 16)]
    [TestCase(0xFFFF, 8)]
    [TestCase(0xFFFFFF, 0)]
    public void LeadingZeroCount(int value, int expected)
        => ((int)UInt24.LeadingZeroCount((UInt24)value)).Should().Equal(expected);

    [TestCase(0, 24)]
    [TestCase(1, 0)]
    [TestCase(2, 1)]
    [TestCase(4, 2)]
    [TestCase(0x100, 8)]
    public void TrailingZeroCount(int value, int expected)
        => ((int)UInt24.TrailingZeroCount((UInt24)value)).Should().Equal(expected);

    [TestCase(0, 0)]
    [TestCase(1, 1)]
    [TestCase(0xFF, 8)]
    [TestCase(0xFFFFFF, 24)]
    [TestCase(0xAAAAAA, 12)]
    public void PopCount(int value, int expected)
        => ((int)UInt24.PopCount((UInt24)value)).Should().Equal(expected);

    [TestCase(1, 1, 2)]
    [TestCase(1, 8, 256)]
    [TestCase(0x800000, 1, 1)]
    [TestCase(0x000001, 24, 1)]
    public void RotateLeft(int value, int amount, int expected)
        => ((int)UInt24.RotateLeft((UInt24)value, amount)).Should().Equal(expected);

    [TestCase(1, 1, 0x800000)]
    [TestCase(2, 1, 1)]
    [TestCase(0x800000, 1, 0x400000)]
    [TestCase(0x000001, 24, 1)]
    public void RotateRight(int value, int amount, int expected)
        => ((int)UInt24.RotateRight((UInt24)value, amount)).Should().Equal(expected);

    [Test]
    public void TryWriteLittleEndian()
    {
        var value = (UInt24)0x123456;
        var buffer = new byte[3];
        value.TryWriteLittleEndian(buffer, out var bytesWritten).Should().Equal(true);
        bytesWritten.Should().Equal(3);
        buffer[0].Should().Equal((byte)0x56);
        buffer[1].Should().Equal((byte)0x34);
        buffer[2].Should().Equal((byte)0x12);
    }

    [Test]
    public void TryWriteLittleEndian_BufferTooSmall()
    {
        var value = (UInt24)0x123456;
        var buffer = new byte[2];
        value.TryWriteLittleEndian(buffer, out var bytesWritten).Should().Equal(false);
        bytesWritten.Should().Equal(0);
    }

    [Test]
    public void TryWriteBigEndian()
    {
        var value = (UInt24)0x123456;
        var buffer = new byte[3];
        value.TryWriteBigEndian(buffer, out var bytesWritten).Should().Equal(true);
        bytesWritten.Should().Equal(3);
        buffer[0].Should().Equal((byte)0x12);
        buffer[1].Should().Equal((byte)0x34);
        buffer[2].Should().Equal((byte)0x56);
    }

    [Test]
    public void TryWriteBigEndian_BufferTooSmall()
    {
        var value = (UInt24)0x123456;
        var buffer = new byte[2];
        value.TryWriteBigEndian(buffer, out var bytesWritten).Should().Equal(false);
        bytesWritten.Should().Equal(0);
    }

    #endregion

    #region INumberBase Boolean Methods

    [TestCase(0, true)]
    [TestCase(1, false)]
    [TestCase(16777215, false)]
    public void IsZero(int value, bool expected)
        => UInt24.IsZero((UInt24)value).Should().Equal(expected);

    [TestCase(0, true)]
    [TestCase(1, true)]
    [TestCase(16777215, true)]
    public void IsPositive(int value, bool expected)
        => UInt24.IsPositive((UInt24)value).Should().Equal(expected);

    [TestCase(0, false)]
    [TestCase(1, false)]
    [TestCase(16777215, false)]
    public void IsNegative(int value, bool expected)
        => UInt24.IsNegative((UInt24)value).Should().Equal(expected);

    [TestCase(0, true)]
    [TestCase(1, false)]
    [TestCase(2, true)]
    [TestCase(3, false)]
    [TestCase(16777214, true)]
    public void IsEvenInteger(int value, bool expected)
        => UInt24.IsEvenInteger((UInt24)value).Should().Equal(expected);

    [TestCase(0, false)]
    [TestCase(1, true)]
    [TestCase(2, false)]
    [TestCase(3, true)]
    [TestCase(16777215, true)]
    public void IsOddInteger(int value, bool expected)
        => UInt24.IsOddInteger((UInt24)value).Should().Equal(expected);

    #endregion
}