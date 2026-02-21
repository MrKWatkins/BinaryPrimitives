using System.Globalization;
using System.Numerics;
using System.Reflection;

namespace MrKWatkins.BinaryPrimitives.Tests;

public sealed class UInt24Tests
{

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
        var uint24Value = (UInt24)value;
        uint24Value++;
        ((int)uint24Value).Should().Equal(expected);
    }

    [TestCase(1, 0)]
    [TestCase(100, 99)]
    [TestCase(0, 16777215)]
    public void Decrement(int value, int expected)
    {
        var uint24Value = (UInt24)value;
        uint24Value--;
        ((int)uint24Value).Should().Equal(expected);
    }

    [TestCase(0)]
    [TestCase(100)]
    [TestCase(16777215)]
    public void UnaryPlus(int value)
        => ((int)+(UInt24)value).Should().Equal(value);

    [TestCase(0, 0)]
    [TestCase(1, 16777215)]
    public void UnaryMinus(int value, int expected)
        => ((int)-(UInt24)value).Should().Equal(expected);

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
    public void CheckedDivision_DivideByZero()
        => AssertThat.Invoking(() => checked((UInt24)100 / (UInt24)0)).Should().Throw<DivideByZeroException>();

    [TestCase(200, 100, 2)]
    [TestCase(10, 3, 3)]
    [TestCase(0, 1, 0)]
    public void CheckedDivision_NoOverflow(int left, int right, int expected)
        => ((int)checked((UInt24)left / (UInt24)right)).Should().Equal(expected);

    [Test]
    public void CheckedIncrement()
    {
        var uint24Value = UInt24.MaxValue;
        AssertThat.Invoking(() => checked(uint24Value++)).Should().Throw<OverflowException>();
    }

    [Test]
    public void CheckedIncrement_NoOverflow()
    {
        var uint24Value = (UInt24)100;
        uint24Value = checked(++uint24Value);
        ((int)uint24Value).Should().Equal(101);
    }

    [Test]
    public void CheckedDecrement()
    {
        var uint24Value = UInt24.MinValue;
        AssertThat.Invoking(() => checked(uint24Value--)).Should().Throw<OverflowException>();
    }

    [Test]
    public void CheckedDecrement_NoOverflow()
    {
        var uint24Value = (UInt24)100;
        uint24Value = checked(--uint24Value);
        ((int)uint24Value).Should().Equal(99);
    }

    [Test]
    public void CheckedUnaryMinus()
        => AssertThat.Invoking(() => checked(-(UInt24)1)).Should().Throw<OverflowException>();

    [Test]
    public void CheckedUnaryMinus_Zero()
        => ((int)checked(-(UInt24)0)).Should().Equal(0);

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
        => ((int)~(UInt24)value).Should().Equal(expected);

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

    [TestCase(0, "0")]
    [TestCase(12345, "12345")]
    [TestCase(16777215, "16777215")]
    public void ToString_NoArgs(int value, string expected)
        => ((UInt24)value).ToString().Should().Equal(expected);

    [TestCase(255, "X4", "00FF")]
    [TestCase(16777215, "X6", "FFFFFF")]
    public void ToString_FormatProvider(int value, string format, string expected)
        => ((UInt24)value).ToString(format, CultureInfo.InvariantCulture).Should().Equal(expected);

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

    [Test]
    public void IsCanonical()
        => UInt24.IsCanonical(UInt24.Zero).Should().Equal(true);

    [Test]
    public void IsComplexNumber()
        => UInt24.IsComplexNumber(UInt24.Zero).Should().Equal(false);

    [Test]
    public void IsFinite()
        => UInt24.IsFinite(UInt24.Zero).Should().Equal(true);

    [Test]
    public void IsImaginaryNumber()
        => UInt24.IsImaginaryNumber(UInt24.Zero).Should().Equal(false);

    [Test]
    public void IsInfinity()
        => UInt24.IsInfinity(UInt24.Zero).Should().Equal(false);

    [Test]
    public void IsInteger()
        => UInt24.IsInteger(UInt24.Zero).Should().Equal(true);

    [Test]
    public void IsNaN()
        => UInt24.IsNaN(UInt24.Zero).Should().Equal(false);

    [TestCase(0, false)]
    [TestCase(1, true)]
    [TestCase(16777215, true)]
    public void IsNormal(int value, bool expected)
        => UInt24.IsNormal((UInt24)value).Should().Equal(expected);

    [Test]
    public void IsRealNumber()
        => UInt24.IsRealNumber(UInt24.Zero).Should().Equal(true);

    [Test]
    public void IsNegativeInfinity()
        => UInt24.IsNegativeInfinity(UInt24.Zero).Should().Equal(false);

    [Test]
    public void IsPositiveInfinity()
        => UInt24.IsPositiveInfinity(UInt24.Zero).Should().Equal(false);

    [Test]
    public void IsSubnormal()
        => UInt24.IsSubnormal(UInt24.Zero).Should().Equal(false);

    [TestCase(100, 200, 200)]
    [TestCase(200, 100, 200)]
    [TestCase(100, 100, 100)]
    public void MaxMagnitude(int x, int y, int expected)
        => ((int)UInt24.MaxMagnitude((UInt24)x, (UInt24)y)).Should().Equal(expected);

    [TestCase(100, 200, 200)]
    [TestCase(200, 100, 200)]
    [TestCase(100, 100, 100)]
    public void MaxMagnitudeNumber(int x, int y, int expected)
        => ((int)UInt24.MaxMagnitudeNumber((UInt24)x, (UInt24)y)).Should().Equal(expected);

    [TestCase(100, 200, 100)]
    [TestCase(200, 100, 100)]
    [TestCase(100, 100, 100)]
    public void MinMagnitude(int x, int y, int expected)
        => ((int)UInt24.MinMagnitude((UInt24)x, (UInt24)y)).Should().Equal(expected);

    [TestCase(100, 200, 100)]
    [TestCase(200, 100, 100)]
    [TestCase(100, 100, 100)]
    public void MinMagnitudeNumber(int x, int y, int expected)
        => ((int)UInt24.MinMagnitudeNumber((UInt24)x, (UInt24)y)).Should().Equal(expected);

    [TestCase(0)]
    [TestCase(127)]
    public void ExplicitFromSByte(int value)
    {
        UInt24 result = (UInt24)(sbyte)value;
        ((int)result).Should().Equal(value);
    }

    [TestCase(0)]
    [TestCase(32767)]
    public void ExplicitFromInt16(int value)
    {
        UInt24 result = (UInt24)(short)value;
        ((int)result).Should().Equal(value);
    }

    [TestCase(0u)]
    [TestCase(16777215u)]
    public void ExplicitFromUInt32(uint value)
    {
        UInt24 result = (UInt24)value;
        ((uint)result).Should().Equal(value);
    }

    [TestCase(0L)]
    [TestCase(16777215L)]
    public void ExplicitFromInt64(long value)
    {
        UInt24 result = (UInt24)value;
        ((long)result).Should().Equal(value);
    }

    [TestCase(0UL)]
    [TestCase(16777215UL)]
    public void ExplicitFromUInt64(ulong value)
    {
        UInt24 result = (UInt24)value;
        ((ulong)result).Should().Equal(value);
    }

    [Test]
    public void ExplicitFromUInt64_TooLarge()
        => AssertThat.Invoking(() => _ = (UInt24)ulong.MaxValue).Should().Throw<OverflowException>();

    [TestCase(0)]
    [TestCase(16777215)]
    public void ExplicitFromNInt(int value)
    {
        UInt24 result = (UInt24)(nint)value;
        ((int)result).Should().Equal(value);
    }

    [Test]
    public void ExplicitFromNInt_Negative()
        => AssertThat.Invoking(() => _ = (UInt24)(nint)(-1)).Should().Throw<OverflowException>();

    [Test]
    public void ExplicitFromNInt_TooLarge()
        => AssertThat.Invoking(() => _ = (UInt24)(nint)16777216).Should().Throw<OverflowException>();

    [TestCase(0u)]
    [TestCase(16777215u)]
    public void ExplicitFromNUInt(uint value)
    {
        UInt24 result = (UInt24)(nuint)value;
        ((uint)result).Should().Equal(value);
    }

    [Test]
    public void ExplicitFromNUInt_TooLarge()
        => AssertThat.Invoking(() => _ = (UInt24)(nuint)16777216u).Should().Throw<OverflowException>();

    [Test]
    public void ExplicitFromHalf()
    {
        UInt24 result = (UInt24)Half.One;
        ((int)result).Should().Equal(1);
    }

    [Test]
    public void ExplicitFromHalf_Overflow()
        => AssertThat.Invoking(() => _ = (UInt24)Half.NaN).Should().Throw<OverflowException>();

    [Test]
    public void ExplicitFromSingle()
    {
        UInt24 result = (UInt24)5.0f;
        ((int)result).Should().Equal(5);
    }

    [Test]
    public void ExplicitFromSingle_Overflow()
        => AssertThat.Invoking(() => _ = (UInt24)float.MaxValue).Should().Throw<OverflowException>();

    [Test]
    public void ExplicitFromDouble()
    {
        UInt24 result = (UInt24)5.0;
        ((int)result).Should().Equal(5);
    }

    [Test]
    public void ExplicitFromDouble_Overflow()
        => AssertThat.Invoking(() => _ = (UInt24)double.MaxValue).Should().Throw<OverflowException>();

    [Test]
    public void ExplicitFromDecimal()
    {
        UInt24 result = (UInt24)5m;
        ((int)result).Should().Equal(5);
    }

    [Test]
    public void ExplicitFromDecimal_Overflow()
        => AssertThat.Invoking(() => _ = (UInt24)decimal.MaxValue).Should().Throw<OverflowException>();

    [TestCase("0", 0)]
    [TestCase("12345", 12345)]
    [TestCase("16777215", 16777215)]
    public void Parse_ReadOnlySpanChar(string input, int expected)
        => ((int)UInt24.Parse(input.AsSpan(), CultureInfo.InvariantCulture)).Should().Equal(expected);

    [Test]
    public void Parse_ReadOnlySpanChar_Overflow()
        => AssertThat.Invoking(() => UInt24.Parse("16777216".AsSpan(), CultureInfo.InvariantCulture)).Should().Throw<OverflowException>();

    [TestCase("0", 0)]
    [TestCase("12345", 12345)]
    [TestCase("16777215", 16777215)]
    public void Parse_ReadOnlySpanByte(string input, int expected)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(input);
        ((int)UInt24.Parse(bytes, CultureInfo.InvariantCulture)).Should().Equal(expected);
    }

    [Test]
    public void Parse_ReadOnlySpanByte_Overflow()
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes("16777216");
        AssertThat.Invoking(() => UInt24.Parse(bytes, CultureInfo.InvariantCulture)).Should().Throw<OverflowException>();
    }

    [TestCase("0", true, 0)]
    [TestCase("12345", true, 12345)]
    [TestCase("16777215", true, 16777215)]
    [TestCase("16777216", false, 0)]
    [TestCase("abc", false, 0)]
    public void TryParse_ReadOnlySpanChar(string input, bool expectedResult, int expectedValue)
    {
        UInt24.TryParse(input.AsSpan(), CultureInfo.InvariantCulture, out var result).Should().Equal(expectedResult);
        ((int)result).Should().Equal(expectedValue);
    }

    [TestCase("0", true, 0)]
    [TestCase("12345", true, 12345)]
    [TestCase("16777215", true, 16777215)]
    [TestCase("16777216", false, 0)]
    [TestCase("abc", false, 0)]
    public void TryParse_ReadOnlySpanByte(string input, bool expectedResult, int expectedValue)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(input);
        UInt24.TryParse(bytes, CultureInfo.InvariantCulture, out var result).Should().Equal(expectedResult);
        ((int)result).Should().Equal(expectedValue);
    }

    [Test]
    public void TryFormat_SpanChar()
    {
        var value = (UInt24)12345;
        Span<char> buffer = stackalloc char[10];
        value.TryFormat(buffer, out var charsWritten, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture).Should().Equal(true);
        charsWritten.Should().Equal(5);
        new string(buffer[..charsWritten]).Should().Equal("12345");
    }

    [Test]
    public void TryFormat_SpanChar_BufferTooSmall()
    {
        var value = (UInt24)12345;
        Span<char> buffer = stackalloc char[2];
        value.TryFormat(buffer, out var charsWritten, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture).Should().Equal(false);
        charsWritten.Should().Equal(0);
    }

    [Test]
    public void TryFormat_SpanByte()
    {
        var value = (UInt24)12345;
        Span<byte> buffer = stackalloc byte[10];
        value.TryFormat(buffer, out var bytesWritten, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture).Should().Equal(true);
        bytesWritten.Should().Equal(5);
        System.Text.Encoding.UTF8.GetString(buffer[..bytesWritten]).Should().Equal("12345");
    }

    [Test]
    public void TryFormat_SpanByte_BufferTooSmall()
    {
        var value = (UInt24)12345;
        Span<byte> buffer = stackalloc byte[2];
        value.TryFormat(buffer, out var bytesWritten, ReadOnlySpan<char>.Empty, CultureInfo.InvariantCulture).Should().Equal(false);
        bytesWritten.Should().Equal(0);
    }

    [TestCase(0, 0)]
    [TestCase(1, 1)]
    [TestCase(0xFF, 8)]
    [TestCase(0xFFFF, 16)]
    [TestCase(0xFFFFFF, 24)]
    public void GetShortestBitLength(int value, int expected)
        => ((UInt24)value).GetShortestBitLength().Should().Equal(expected);

    [Test]
    public void CreateChecked_Byte()
        => ((int)UInt24.CreateChecked<byte>(5)).Should().Equal(5);

    [Test]
    public void CreateChecked_UInt16()
        => ((int)UInt24.CreateChecked<ushort>(5)).Should().Equal(5);

    [Test]
    public void CreateChecked_UInt32()
        => ((int)UInt24.CreateChecked<uint>(5u)).Should().Equal(5);

    [Test]
    public void CreateChecked_UInt32_Overflow()
        => AssertThat.Invoking(() => UInt24.CreateChecked<uint>(uint.MaxValue)).Should().Throw<OverflowException>();

    [Test]
    public void CreateChecked_UInt64()
        => ((int)UInt24.CreateChecked<ulong>(5ul)).Should().Equal(5);

    [Test]
    public void CreateChecked_UInt64_Overflow()
        => AssertThat.Invoking(() => UInt24.CreateChecked<ulong>(ulong.MaxValue)).Should().Throw<OverflowException>();

    [Test]
    public void CreateChecked_SByte()
        => ((int)UInt24.CreateChecked<sbyte>(5)).Should().Equal(5);

    [Test]
    public void CreateChecked_Int16()
        => ((int)UInt24.CreateChecked<short>(5)).Should().Equal(5);

    [Test]
    public void CreateChecked_Int32()
        => ((int)UInt24.CreateChecked<int>(5)).Should().Equal(5);

    [Test]
    public void CreateChecked_Int64()
        => ((int)UInt24.CreateChecked<long>(5L)).Should().Equal(5);

    [Test]
    public void CreateChecked_Half()
        => ((int)UInt24.CreateChecked<Half>(Half.One)).Should().Equal(1);

    [Test]
    public void CreateChecked_Single()
        => ((int)UInt24.CreateChecked<float>(5.0f)).Should().Equal(5);

    [Test]
    public void CreateChecked_Double()
        => ((int)UInt24.CreateChecked<double>(5.0)).Should().Equal(5);

    [Test]
    public void CreateChecked_Decimal()
        => ((int)UInt24.CreateChecked<decimal>(5m)).Should().Equal(5);

    [Test]
    public void CreateChecked_UInt24()
        => ((int)UInt24.CreateChecked<UInt24>((UInt24)5)).Should().Equal(5);

    [Test]
    public void CreateChecked_NotSupported()
        => AssertThat.Invoking(() => UInt24.CreateChecked<Complex>(Complex.One)).Should().Throw<NotSupportedException>();

    [Test]
    public void CreateSaturating_Byte()
        => ((int)UInt24.CreateSaturating<byte>(5)).Should().Equal(5);

    [Test]
    public void CreateSaturating_UInt16()
        => ((int)UInt24.CreateSaturating<ushort>(5)).Should().Equal(5);

    [Test]
    public void CreateSaturating_UInt32()
        => ((int)UInt24.CreateSaturating<uint>(5u)).Should().Equal(5);

    [Test]
    public void CreateSaturating_UInt32_Saturates()
        => ((int)UInt24.CreateSaturating<uint>(uint.MaxValue)).Should().Equal(16777215);

    [Test]
    public void CreateSaturating_UInt64()
        => ((int)UInt24.CreateSaturating<ulong>(5ul)).Should().Equal(5);

    [Test]
    public void CreateSaturating_UInt64_Saturates()
        => ((int)UInt24.CreateSaturating<ulong>(ulong.MaxValue)).Should().Equal(16777215);

    [Test]
    public void CreateSaturating_SByte()
        => ((int)UInt24.CreateSaturating<sbyte>(5)).Should().Equal(5);

    [Test]
    public void CreateSaturating_SByte_Negative()
        => ((int)UInt24.CreateSaturating<sbyte>(-1)).Should().Equal(0);

    [Test]
    public void CreateSaturating_Int16()
        => ((int)UInt24.CreateSaturating<short>(5)).Should().Equal(5);

    [Test]
    public void CreateSaturating_Int16_Negative()
        => ((int)UInt24.CreateSaturating<short>(-1)).Should().Equal(0);

    [Test]
    public void CreateSaturating_Int32()
        => ((int)UInt24.CreateSaturating<int>(5)).Should().Equal(5);

    [Test]
    public void CreateSaturating_Int32_Negative()
        => ((int)UInt24.CreateSaturating<int>(-1)).Should().Equal(0);

    [Test]
    public void CreateSaturating_Int32_Saturates()
        => ((int)UInt24.CreateSaturating<int>(16777216)).Should().Equal(16777215);

    [Test]
    public void CreateSaturating_Int64()
        => ((int)UInt24.CreateSaturating<long>(5L)).Should().Equal(5);

    [Test]
    public void CreateSaturating_Int64_Negative()
        => ((int)UInt24.CreateSaturating<long>(-1L)).Should().Equal(0);

    [Test]
    public void CreateSaturating_Int64_Saturates()
        => ((int)UInt24.CreateSaturating<long>(16777216L)).Should().Equal(16777215);

    [Test]
    public void CreateSaturating_Half()
        => ((int)UInt24.CreateSaturating<Half>(Half.One)).Should().Equal(1);

    [Test]
    public void CreateSaturating_Half_NaN()
        => ((int)UInt24.CreateSaturating<Half>(Half.NaN)).Should().Equal(0);

    [Test]
    public void CreateSaturating_Half_Negative()
        => ((int)UInt24.CreateSaturating<Half>(-Half.One)).Should().Equal(0);

    [Test]
    public void CreateSaturating_Half_Saturates()
        => ((int)UInt24.CreateSaturating<Half>(Half.PositiveInfinity)).Should().Equal(16777215);

    [Test]
    public void CreateSaturating_Single()
        => ((int)UInt24.CreateSaturating<float>(5.0f)).Should().Equal(5);

    [Test]
    public void CreateSaturating_Single_NaN()
        => ((int)UInt24.CreateSaturating<float>(float.NaN)).Should().Equal(0);

    [Test]
    public void CreateSaturating_Single_Negative()
        => ((int)UInt24.CreateSaturating<float>(-1.0f)).Should().Equal(0);

    [Test]
    public void CreateSaturating_Single_Saturates()
        => ((int)UInt24.CreateSaturating<float>(float.MaxValue)).Should().Equal(16777215);

    [Test]
    public void CreateSaturating_Double()
        => ((int)UInt24.CreateSaturating<double>(5.0)).Should().Equal(5);

    [Test]
    public void CreateSaturating_Double_NaN()
        => ((int)UInt24.CreateSaturating<double>(double.NaN)).Should().Equal(0);

    [Test]
    public void CreateSaturating_Double_Negative()
        => ((int)UInt24.CreateSaturating<double>(-1.0)).Should().Equal(0);

    [Test]
    public void CreateSaturating_Double_Saturates()
        => ((int)UInt24.CreateSaturating<double>(double.MaxValue)).Should().Equal(16777215);

    [Test]
    public void CreateSaturating_Decimal()
        => ((int)UInt24.CreateSaturating<decimal>(5m)).Should().Equal(5);

    [Test]
    public void CreateSaturating_Decimal_Negative()
        => ((int)UInt24.CreateSaturating<decimal>(-1m)).Should().Equal(0);

    [Test]
    public void CreateSaturating_Decimal_Saturates()
        => ((int)UInt24.CreateSaturating<decimal>(16777216m)).Should().Equal(16777215);

    [Test]
    public void CreateSaturating_UInt24()
        => ((int)UInt24.CreateSaturating<UInt24>((UInt24)5)).Should().Equal(5);

    [Test]
    public void CreateSaturating_NotSupported()
        => AssertThat.Invoking(() => UInt24.CreateSaturating<Complex>(Complex.One)).Should().Throw<NotSupportedException>();

    [Test]
    public void CreateTruncating_Byte()
        => ((int)UInt24.CreateTruncating<byte>(5)).Should().Equal(5);

    [Test]
    public void CreateTruncating_UInt16()
        => ((int)UInt24.CreateTruncating<ushort>(5)).Should().Equal(5);

    [Test]
    public void CreateTruncating_UInt32()
        => ((int)UInt24.CreateTruncating<uint>(5u)).Should().Equal(5);

    [Test]
    public void CreateTruncating_UInt32_Truncates()
        => ((int)UInt24.CreateTruncating<uint>(0xFFFFFFFFu)).Should().Equal(0xFFFFFF);

    [Test]
    public void CreateTruncating_UInt64()
        => ((int)UInt24.CreateTruncating<ulong>(5ul)).Should().Equal(5);

    [Test]
    public void CreateTruncating_UInt64_Truncates()
        => ((int)UInt24.CreateTruncating<ulong>(0xFFFFFFFFFFFFFFFFul)).Should().Equal(0xFFFFFF);

    [Test]
    public void CreateTruncating_SByte()
        => ((int)UInt24.CreateTruncating<sbyte>(5)).Should().Equal(5);

    [Test]
    public void CreateTruncating_SByte_Negative()
        => ((int)UInt24.CreateTruncating<sbyte>(-1)).Should().Equal(0xFFFFFF);

    [Test]
    public void CreateTruncating_Int16()
        => ((int)UInt24.CreateTruncating<short>(5)).Should().Equal(5);

    [Test]
    public void CreateTruncating_Int16_Negative()
        => ((int)UInt24.CreateTruncating<short>(-1)).Should().Equal(0xFFFFFF);

    [Test]
    public void CreateTruncating_Int32()
        => ((int)UInt24.CreateTruncating<int>(5)).Should().Equal(5);

    [Test]
    public void CreateTruncating_Int32_Truncates()
        => ((int)UInt24.CreateTruncating<int>(-1)).Should().Equal(0xFFFFFF);

    [Test]
    public void CreateTruncating_Int64()
        => ((int)UInt24.CreateTruncating<long>(5L)).Should().Equal(5);

    [Test]
    public void CreateTruncating_Int64_Truncates()
        => ((int)UInt24.CreateTruncating<long>(-1L)).Should().Equal(0xFFFFFF);

    [Test]
    public void CreateTruncating_Half()
        => ((int)UInt24.CreateTruncating<Half>(Half.One)).Should().Equal(1);

    [Test]
    public void CreateTruncating_Single()
        => ((int)UInt24.CreateTruncating<float>(5.0f)).Should().Equal(5);

    [Test]
    public void CreateTruncating_Double()
        => ((int)UInt24.CreateTruncating<double>(5.0)).Should().Equal(5);

    [Test]
    public void CreateTruncating_Decimal()
        => ((int)UInt24.CreateTruncating<decimal>(5m)).Should().Equal(5);

    [Test]
    public void CreateTruncating_UInt24()
        => ((int)UInt24.CreateTruncating<UInt24>((UInt24)5)).Should().Equal(5);

    [Test]
    public void CreateTruncating_NotSupported()
        => AssertThat.Invoking(() => UInt24.CreateTruncating<Complex>(Complex.One)).Should().Throw<NotSupportedException>();

    [Test]
    public void TryReadBigEndian_EmptySource()
        => TryReadBigEndian<UInt24>([], isUnsigned: true, out _).Should().Equal(false);

    [Test]
    public void TryReadBigEndian_SignedNegative()
        => TryReadBigEndian<UInt24>([0x80], isUnsigned: false, out _).Should().Equal(false);

    [Test]
    public void TryReadBigEndian_LargeSpan_NonZeroLeadingByte()
        => TryReadBigEndian<UInt24>([0x01, 0x00, 0x00, 0x00], isUnsigned: true, out _).Should().Equal(false);

    [Test]
    public void TryReadBigEndian_LargeSpan()
    {
        TryReadBigEndian<UInt24>([0x00, 0x12, 0x34, 0x56], isUnsigned: true, out var result).Should().Equal(true);
        ((int)result).Should().Equal(0x123456);
    }

    [Test]
    public void TryReadBigEndian_ThreeBytes()
    {
        TryReadBigEndian<UInt24>([0x12, 0x34, 0x56], isUnsigned: true, out var result).Should().Equal(true);
        ((int)result).Should().Equal(0x123456);
    }

    [Test]
    public void TryReadBigEndian_TwoBytes()
    {
        TryReadBigEndian<UInt24>([0x12, 0x34], isUnsigned: true, out var result).Should().Equal(true);
        ((int)result).Should().Equal(0x1234);
    }

    [Test]
    public void TryReadBigEndian_OneByte()
    {
        TryReadBigEndian<UInt24>([0x12], isUnsigned: true, out var result).Should().Equal(true);
        ((int)result).Should().Equal(0x12);
    }

    [Test]
    public void TryReadLittleEndian_EmptySource()
        => TryReadLittleEndian<UInt24>([], isUnsigned: true, out _).Should().Equal(false);

    [Test]
    public void TryReadLittleEndian_SignedNegative()
        => TryReadLittleEndian<UInt24>([0x80], isUnsigned: false, out _).Should().Equal(false);

    [Test]
    public void TryReadLittleEndian_LargeSpan_NonZeroTrailingByte()
        => TryReadLittleEndian<UInt24>([0x00, 0x00, 0x00, 0x01], isUnsigned: true, out _).Should().Equal(false);

    [Test]
    public void TryReadLittleEndian_LargeSpan()
    {
        TryReadLittleEndian<UInt24>([0x56, 0x34, 0x12, 0x00], isUnsigned: true, out var result).Should().Equal(true);
        ((int)result).Should().Equal(0x123456);
    }

    [Test]
    public void TryReadLittleEndian_ThreeBytes()
    {
        TryReadLittleEndian<UInt24>([0x56, 0x34, 0x12], isUnsigned: true, out var result).Should().Equal(true);
        ((int)result).Should().Equal(0x123456);
    }

    [Test]
    public void TryReadLittleEndian_TwoBytes()
    {
        TryReadLittleEndian<UInt24>([0x34, 0x12], isUnsigned: true, out var result).Should().Equal(true);
        ((int)result).Should().Equal(0x1234);
    }

    [Test]
    public void TryReadLittleEndian_OneByte()
    {
        TryReadLittleEndian<UInt24>([0x12], isUnsigned: true, out var result).Should().Equal(true);
        ((int)result).Should().Equal(0x12);
    }

    // Static abstract interface members can only be invoked via a constrained type parameter.
    private static bool TryReadBigEndian<T>(ReadOnlySpan<byte> source, bool isUnsigned, out T result)
        where T : IBinaryInteger<T>
        => T.TryReadBigEndian(source, isUnsigned, out result);

    private static bool TryReadLittleEndian<T>(ReadOnlySpan<byte> source, bool isUnsigned, out T result)
        where T : IBinaryInteger<T>
        => T.TryReadLittleEndian(source, isUnsigned, out result);

    [Test]
    public void TryConvertFromChecked_UInt32()
    {
        TryConvertFromChecked<uint>(100u, out var result).Should().Equal(true);
        ((int)result).Should().Equal(100);
    }

    [Test]
    public void TryConvertFromChecked_UInt32_Overflow() =>
        AssertThat.Invoking(() => TryConvertFromChecked<uint>(uint.MaxValue, out _)).Should().Throw<OverflowException>();

    [Test]
    public void TryConvertFromChecked_UInt64()
    {
        TryConvertFromChecked<ulong>(100ul, out var result).Should().Equal(true);
        ((int)result).Should().Equal(100);
    }

    [Test]
    public void TryConvertFromChecked_UInt64_Overflow() =>
        AssertThat.Invoking(() => TryConvertFromChecked<ulong>(ulong.MaxValue, out _)).Should().Throw<OverflowException>();

    [Test]
    public void TryConvertFromChecked_NUInt()
    {
        TryConvertFromChecked<nuint>((nuint)100, out var result).Should().Equal(true);
        ((int)result).Should().Equal(100);
    }

    [Test]
    public void TryConvertFromChecked_NUInt_Overflow() =>
        AssertThat.Invoking(() => TryConvertFromChecked<nuint>((nuint)0x1000000, out _)).Should().Throw<OverflowException>();

    [Test]
    public void TryConvertFromChecked_Unsupported() =>
        TryConvertFromChecked<int>(42, out _).Should().Equal(false);

    [Test]
    public void TryConvertFromSaturating_UInt32()
    {
        TryConvertFromSaturating<uint>(100u, out var result).Should().Equal(true);
        ((int)result).Should().Equal(100);
    }

    [Test]
    public void TryConvertFromSaturating_UInt32_Saturates()
    {
        TryConvertFromSaturating<uint>(uint.MaxValue, out var result).Should().Equal(true);
        result.Should().Equal(UInt24.MaxValue);
    }

    [Test]
    public void TryConvertFromSaturating_UInt64()
    {
        TryConvertFromSaturating<ulong>(100ul, out var result).Should().Equal(true);
        ((int)result).Should().Equal(100);
    }

    [Test]
    public void TryConvertFromSaturating_UInt64_Saturates()
    {
        TryConvertFromSaturating<ulong>(ulong.MaxValue, out var result).Should().Equal(true);
        result.Should().Equal(UInt24.MaxValue);
    }

    [Test]
    public void TryConvertFromSaturating_NUInt()
    {
        TryConvertFromSaturating<nuint>((nuint)100, out var result).Should().Equal(true);
        ((int)result).Should().Equal(100);
    }

    [Test]
    public void TryConvertFromSaturating_NUInt_Saturates()
    {
        TryConvertFromSaturating<nuint>((nuint)0x1000000, out var result).Should().Equal(true);
        result.Should().Equal(UInt24.MaxValue);
    }

    [Test]
    public void TryConvertFromSaturating_Unsupported() =>
        TryConvertFromSaturating<int>(42, out _).Should().Equal(false);

    [Test]
    public void TryConvertFromTruncating_UInt32()
    {
        TryConvertFromTruncating<uint>(0x123456u, out var result).Should().Equal(true);
        ((int)result).Should().Equal(0x123456);
    }

    [Test]
    public void TryConvertFromTruncating_UInt32_Truncates()
    {
        TryConvertFromTruncating<uint>(0x01234567u, out var result).Should().Equal(true);
        ((int)result).Should().Equal(0x234567);
    }

    [Test]
    public void TryConvertFromTruncating_UInt64()
    {
        TryConvertFromTruncating<ulong>(0x123456ul, out var result).Should().Equal(true);
        ((int)result).Should().Equal(0x123456);
    }

    [Test]
    public void TryConvertFromTruncating_UInt64_Truncates()
    {
        TryConvertFromTruncating<ulong>(0x0123456789ABCDEFul, out var result).Should().Equal(true);
        ((int)result).Should().Equal(0xABCDEF);
    }

    [Test]
    public void TryConvertFromTruncating_NUInt()
    {
        TryConvertFromTruncating<nuint>((nuint)0x123456, out var result).Should().Equal(true);
        ((int)result).Should().Equal(0x123456);
    }

    [Test]
    public void TryConvertFromTruncating_NUInt_Truncates()
    {
        TryConvertFromTruncating<nuint>((nuint)0x01234567, out var result).Should().Equal(true);
        ((int)result).Should().Equal(0x234567);
    }

    [Test]
    public void TryConvertFromTruncating_Unsupported() =>
        TryConvertFromTruncating<int>(42, out _).Should().Equal(false);

    [Test]
    public void TryConvertToChecked_Byte()
    {
        TryConvertToChecked<byte>((UInt24)100, out var result).Should().Equal(true);
        result.Should().Equal((byte)100);
    }

    [Test]
    public void TryConvertToChecked_Byte_Overflow() =>
        AssertThat.Invoking(() => TryConvertToChecked<byte>((UInt24)256, out _)).Should().Throw<OverflowException>();

    [Test]
    public void TryConvertToChecked_SByte()
    {
        TryConvertToChecked<sbyte>((UInt24)100, out var result).Should().Equal(true);
        result.Should().Equal((sbyte)100);
    }

    [Test]
    public void TryConvertToChecked_SByte_Overflow() =>
        AssertThat.Invoking(() => TryConvertToChecked<sbyte>((UInt24)200, out _)).Should().Throw<OverflowException>();

    [Test]
    public void TryConvertToChecked_Int16()
    {
        TryConvertToChecked<short>((UInt24)1000, out var result).Should().Equal(true);
        result.Should().Equal((short)1000);
    }

    [Test]
    public void TryConvertToChecked_Int16_Overflow() =>
        AssertThat.Invoking(() => TryConvertToChecked<short>((UInt24)40000, out _)).Should().Throw<OverflowException>();

    [Test]
    public void TryConvertToChecked_UInt16()
    {
        TryConvertToChecked<ushort>((UInt24)1000, out var result).Should().Equal(true);
        result.Should().Equal((ushort)1000);
    }

    [Test]
    public void TryConvertToChecked_UInt16_Overflow() =>
        AssertThat.Invoking(() => TryConvertToChecked<ushort>((UInt24)70000, out _)).Should().Throw<OverflowException>();

    [Test]
    public void TryConvertToChecked_Int32()
    {
        TryConvertToChecked<int>((UInt24)12345, out var result).Should().Equal(true);
        result.Should().Equal(12345);
    }

    [Test]
    public void TryConvertToChecked_UInt32()
    {
        TryConvertToChecked<uint>((UInt24)12345, out var result).Should().Equal(true);
        result.Should().Equal(12345u);
    }

    [Test]
    public void TryConvertToChecked_Int64()
    {
        TryConvertToChecked<long>((UInt24)12345, out var result).Should().Equal(true);
        result.Should().Equal(12345L);
    }

    [Test]
    public void TryConvertToChecked_UInt64()
    {
        TryConvertToChecked<ulong>((UInt24)12345, out var result).Should().Equal(true);
        result.Should().Equal(12345ul);
    }

    [Test]
    public void TryConvertToChecked_Unsupported() =>
        TryConvertToChecked<float>((UInt24)100, out _).Should().Equal(false);

    [Test]
    public void TryConvertToSaturating_Byte()
    {
        TryConvertToSaturating<byte>((UInt24)100, out var result).Should().Equal(true);
        result.Should().Equal((byte)100);
    }

    [Test]
    public void TryConvertToSaturating_Byte_Saturates()
    {
        TryConvertToSaturating<byte>(UInt24.MaxValue, out var result).Should().Equal(true);
        result.Should().Equal(byte.MaxValue);
    }

    [Test]
    public void TryConvertToSaturating_SByte()
    {
        TryConvertToSaturating<sbyte>((UInt24)100, out var result).Should().Equal(true);
        result.Should().Equal((sbyte)100);
    }

    [Test]
    public void TryConvertToSaturating_SByte_Saturates()
    {
        TryConvertToSaturating<sbyte>(UInt24.MaxValue, out var result).Should().Equal(true);
        result.Should().Equal(sbyte.MaxValue);
    }

    [Test]
    public void TryConvertToSaturating_Int16()
    {
        TryConvertToSaturating<short>((UInt24)1000, out var result).Should().Equal(true);
        result.Should().Equal((short)1000);
    }

    [Test]
    public void TryConvertToSaturating_Int16_Saturates()
    {
        TryConvertToSaturating<short>(UInt24.MaxValue, out var result).Should().Equal(true);
        result.Should().Equal(short.MaxValue);
    }

    [Test]
    public void TryConvertToSaturating_UInt16()
    {
        TryConvertToSaturating<ushort>((UInt24)1000, out var result).Should().Equal(true);
        result.Should().Equal((ushort)1000);
    }

    [Test]
    public void TryConvertToSaturating_UInt16_Saturates()
    {
        TryConvertToSaturating<ushort>(UInt24.MaxValue, out var result).Should().Equal(true);
        result.Should().Equal(ushort.MaxValue);
    }

    [Test]
    public void TryConvertToSaturating_Int32()
    {
        TryConvertToSaturating<int>((UInt24)12345, out var result).Should().Equal(true);
        result.Should().Equal(12345);
    }

    [Test]
    public void TryConvertToSaturating_UInt32()
    {
        TryConvertToSaturating<uint>((UInt24)12345, out var result).Should().Equal(true);
        result.Should().Equal(12345u);
    }

    [Test]
    public void TryConvertToSaturating_Int64()
    {
        TryConvertToSaturating<long>((UInt24)12345, out var result).Should().Equal(true);
        result.Should().Equal(12345L);
    }

    [Test]
    public void TryConvertToSaturating_UInt64()
    {
        TryConvertToSaturating<ulong>((UInt24)12345, out var result).Should().Equal(true);
        result.Should().Equal(12345ul);
    }

    [Test]
    public void TryConvertToSaturating_Unsupported() =>
        TryConvertToSaturating<float>((UInt24)100, out _).Should().Equal(false);

    [Test]
    public void TryConvertToTruncating_Byte()
    {
        TryConvertToTruncating<byte>((UInt24)100, out var result).Should().Equal(true);
        result.Should().Equal((byte)100);
    }

    [Test]
    public void TryConvertToTruncating_Byte_Truncates()
    {
        TryConvertToTruncating<byte>((UInt24)0x1FF, out var result).Should().Equal(true);
        result.Should().Equal((byte)0xFF);
    }

    [Test]
    public void TryConvertToTruncating_SByte()
    {
        TryConvertToTruncating<sbyte>((UInt24)100, out var result).Should().Equal(true);
        result.Should().Equal((sbyte)100);
    }

    [Test]
    public void TryConvertToTruncating_SByte_Truncates()
    {
        TryConvertToTruncating<sbyte>((UInt24)0x1FF, out var result).Should().Equal(true);
        result.Should().Equal(unchecked((sbyte)0xFF));
    }

    [Test]
    public void TryConvertToTruncating_Int16()
    {
        TryConvertToTruncating<short>((UInt24)1000, out var result).Should().Equal(true);
        result.Should().Equal((short)1000);
    }

    [Test]
    public void TryConvertToTruncating_Int16_Truncates()
    {
        TryConvertToTruncating<short>((UInt24)0x1FFFF, out var result).Should().Equal(true);
        result.Should().Equal(unchecked((short)0xFFFF));
    }

    [Test]
    public void TryConvertToTruncating_UInt16()
    {
        TryConvertToTruncating<ushort>((UInt24)1000, out var result).Should().Equal(true);
        result.Should().Equal((ushort)1000);
    }

    [Test]
    public void TryConvertToTruncating_UInt16_Truncates()
    {
        TryConvertToTruncating<ushort>((UInt24)0x1FFFF, out var result).Should().Equal(true);
        result.Should().Equal((ushort)0xFFFF);
    }

    [Test]
    public void TryConvertToTruncating_Int32()
    {
        TryConvertToTruncating<int>((UInt24)12345, out var result).Should().Equal(true);
        result.Should().Equal(12345);
    }

    [Test]
    public void TryConvertToTruncating_UInt32()
    {
        TryConvertToTruncating<uint>((UInt24)12345, out var result).Should().Equal(true);
        result.Should().Equal(12345u);
    }

    [Test]
    public void TryConvertToTruncating_Int64()
    {
        TryConvertToTruncating<long>((UInt24)12345, out var result).Should().Equal(true);
        result.Should().Equal(12345L);
    }

    [Test]
    public void TryConvertToTruncating_UInt64()
    {
        TryConvertToTruncating<ulong>((UInt24)12345, out var result).Should().Equal(true);
        result.Should().Equal(12345ul);
    }

    [Test]
    public void TryConvertToTruncating_Unsupported() =>
        TryConvertToTruncating<float>((UInt24)100, out _).Should().Equal(false);

    // Explicit static interface implementations are non-public and cannot be called directly.
    // Reflection is used to invoke them, unwrapping TargetInvocationException so callers see
    // the original exception type.
    private static bool TryConvertFromChecked<TOther>(TOther value, out UInt24 result)
        where TOther : INumberBase<TOther>
    {
        var method = typeof(UInt24)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .Single(m => m.Name.EndsWith("TryConvertFromChecked", StringComparison.Ordinal) && m.IsGenericMethodDefinition)
            .MakeGenericMethod(typeof(TOther));
        var args = new object?[] { value, null };
        try
        {
            var success = (bool)method.Invoke(null, args)!;
            result = (UInt24)args[1]!;
            return success;
        }
        catch (TargetInvocationException ex)
        {
            result = default;
            throw ex.InnerException!;
        }
    }

    private static bool TryConvertFromSaturating<TOther>(TOther value, out UInt24 result)
        where TOther : INumberBase<TOther>
    {
        var method = typeof(UInt24)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .Single(m => m.Name.EndsWith("TryConvertFromSaturating", StringComparison.Ordinal) && m.IsGenericMethodDefinition)
            .MakeGenericMethod(typeof(TOther));
        var args = new object?[] { value, null };
        try
        {
            var success = (bool)method.Invoke(null, args)!;
            result = (UInt24)args[1]!;
            return success;
        }
        catch (TargetInvocationException ex)
        {
            result = default;
            throw ex.InnerException!;
        }
    }

    private static bool TryConvertFromTruncating<TOther>(TOther value, out UInt24 result)
        where TOther : INumberBase<TOther>
    {
        var method = typeof(UInt24)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .Single(m => m.Name.EndsWith("TryConvertFromTruncating", StringComparison.Ordinal) && m.IsGenericMethodDefinition)
            .MakeGenericMethod(typeof(TOther));
        var args = new object?[] { value, null };
        try
        {
            var success = (bool)method.Invoke(null, args)!;
            result = (UInt24)args[1]!;
            return success;
        }
        catch (TargetInvocationException ex)
        {
            result = default;
            throw ex.InnerException!;
        }
    }

    private static bool TryConvertToChecked<TOther>(UInt24 value, out TOther result)
    {
        var method = typeof(UInt24)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .Single(m => m.Name.EndsWith("TryConvertToChecked", StringComparison.Ordinal) && m.IsGenericMethodDefinition)
            .MakeGenericMethod(typeof(TOther));
        var args = new object?[] { value, null };
        try
        {
            var success = (bool)method.Invoke(null, args)!;
            result = args[1] is TOther t ? t : default!;
            return success;
        }
        catch (TargetInvocationException ex)
        {
            result = default!;
            throw ex.InnerException!;
        }
    }

    private static bool TryConvertToSaturating<TOther>(UInt24 value, out TOther result)
    {
        var method = typeof(UInt24)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .Single(m => m.Name.EndsWith("TryConvertToSaturating", StringComparison.Ordinal) && m.IsGenericMethodDefinition)
            .MakeGenericMethod(typeof(TOther));
        var args = new object?[] { value, null };
        try
        {
            var success = (bool)method.Invoke(null, args)!;
            result = args[1] is TOther t ? t : default!;
            return success;
        }
        catch (TargetInvocationException ex)
        {
            result = default!;
            throw ex.InnerException!;
        }
    }

    private static bool TryConvertToTruncating<TOther>(UInt24 value, out TOther result)
    {
        var method = typeof(UInt24)
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
            .Single(m => m.Name.EndsWith("TryConvertToTruncating", StringComparison.Ordinal) && m.IsGenericMethodDefinition)
            .MakeGenericMethod(typeof(TOther));
        var args = new object?[] { value, null };
        try
        {
            var success = (bool)method.Invoke(null, args)!;
            result = args[1] is TOther t ? t : default!;
            return success;
        }
        catch (TargetInvocationException ex)
        {
            result = default!;
            throw ex.InnerException!;
        }
    }

}