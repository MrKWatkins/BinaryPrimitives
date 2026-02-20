using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Represents a 24-bit unsigned integer.
/// </summary>
public readonly struct UInt24 :
    IBinaryInteger<UInt24>,
    IMinMaxValue<UInt24>,
    IUnsignedNumber<UInt24>
{
    internal const uint MaxValueUInt32 = 0x00FFFFFFu;

    private readonly uint _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="UInt24" /> struct from a raw <see cref="uint" /> value without validation.
    /// </summary>
    /// <param name="value">The raw value.</param>
    internal UInt24(uint value) => _value = value;

    private UInt24(uint value, bool @checked)
    {
        if (@checked && value > MaxValueUInt32)
        {
            throw new OverflowException("Value was either too large or too small for a UInt24.");
        }

        _value = value;
    }

    /// <summary>
    /// Represents the largest possible value of a <see cref="UInt24" />.
    /// </summary>
    public static UInt24 MaxValue => new(MaxValueUInt32);

    /// <summary>
    /// Represents the smallest possible value of a <see cref="UInt24" />.
    /// </summary>
    public static UInt24 MinValue => new(0u);

    /// <inheritdoc />
    public static UInt24 One => new(1u);

    /// <inheritdoc />
    public static int Radix => 2;

    /// <inheritdoc />
    public static UInt24 Zero => new(0u);

    /// <inheritdoc />
    public static UInt24 AdditiveIdentity => Zero;

    /// <inheritdoc />
    public static UInt24 MultiplicativeIdentity => One;

    #region IComparable

    /// <inheritdoc />
    [Pure]
    public int CompareTo(object? obj)
    {
        if (obj is null)
        {
            return 1;
        }

        if (obj is UInt24 other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException($"Object must be of type {nameof(UInt24)}.", nameof(obj));
    }

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(UInt24 other) => _value.CompareTo(other._value);

    #endregion

    #region IEquatable

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(UInt24 other) => _value == other._value;

    /// <inheritdoc />
    [Pure]
    public override bool Equals(object? obj) => obj is UInt24 other && Equals(other);

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode() => _value.GetHashCode();

    #endregion

    #region Formatting

    /// <inheritdoc />
    [Pure]
    public override string ToString() => _value.ToString(CultureInfo.CurrentCulture);

    /// <inheritdoc />
    [Pure]
    public string ToString(string? format, IFormatProvider? formatProvider) => _value.ToString(format, formatProvider);

    /// <inheritdoc />
    [Pure]
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider) =>
        _value.TryFormat(destination, out charsWritten, format, provider);

    /// <inheritdoc />
    [Pure]
    public bool TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider) =>
        _value.TryFormat(utf8Destination, out bytesWritten, format, provider);

    #endregion

    #region Parsing

    /// <inheritdoc />
    [Pure]
    public static UInt24 Parse(string s, IFormatProvider? provider) => Parse(s, NumberStyles.Integer, provider);

    /// <inheritdoc />
    [Pure]
    public static UInt24 Parse(string s, NumberStyles style, IFormatProvider? provider)
    {
        var value = uint.Parse(s, style, provider);
        return new UInt24(value, true);
    }

    /// <inheritdoc />
    [Pure]
    public static UInt24 Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => Parse(s, NumberStyles.Integer, provider);

    /// <inheritdoc />
    [Pure]
    public static UInt24 Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
    {
        var value = uint.Parse(s, style, provider);
        return new UInt24(value, true);
    }

    /// <inheritdoc />
    [Pure]
    public static UInt24 Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider) => Parse(utf8Text, NumberStyles.Integer, provider);

    /// <inheritdoc />
    [Pure]
    public static UInt24 Parse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider)
    {
        var value = uint.Parse(utf8Text, style, provider);
        return new UInt24(value, true);
    }

    /// <inheritdoc />
    public static bool TryParse(string? s, IFormatProvider? provider, out UInt24 result) =>
        TryParse(s, NumberStyles.Integer, provider, out result);

    /// <inheritdoc />
    public static bool TryParse(string? s, NumberStyles style, IFormatProvider? provider, out UInt24 result)
    {
        if (uint.TryParse(s, style, provider, out var value) && value <= MaxValueUInt32)
        {
            result = new UInt24(value);
            return true;
        }

        result = default;
        return false;
    }

    /// <inheritdoc />
    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out UInt24 result) =>
        TryParse(s, NumberStyles.Integer, provider, out result);

    /// <inheritdoc />
    public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out UInt24 result)
    {
        if (uint.TryParse(s, style, provider, out var value) && value <= MaxValueUInt32)
        {
            result = new UInt24(value);
            return true;
        }

        result = default;
        return false;
    }

    /// <inheritdoc />
    public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out UInt24 result) =>
        TryParse(utf8Text, NumberStyles.Integer, provider, out result);

    /// <inheritdoc />
    public static bool TryParse(ReadOnlySpan<byte> utf8Text, NumberStyles style, IFormatProvider? provider, out UInt24 result)
    {
        if (uint.TryParse(utf8Text, style, provider, out var value) && value <= MaxValueUInt32)
        {
            result = new UInt24(value);
            return true;
        }

        result = default;
        return false;
    }

    #endregion

    #region Arithmetic Operators

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator +(UInt24 left, UInt24 right) => new((left._value + right._value) & MaxValueUInt32);

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator checked +(UInt24 left, UInt24 right)
    {
        var result = left._value + right._value;
        if (result > MaxValueUInt32)
        {
            throw new OverflowException("UInt24 addition overflowed.");
        }

        return new UInt24(result);
    }

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator -(UInt24 left, UInt24 right) => new((left._value - right._value) & MaxValueUInt32);

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator checked -(UInt24 left, UInt24 right)
    {
        if (left._value < right._value)
        {
            throw new OverflowException("UInt24 subtraction overflowed.");
        }

        return new UInt24(left._value - right._value);
    }

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator *(UInt24 left, UInt24 right) => new((left._value * right._value) & MaxValueUInt32);

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator checked *(UInt24 left, UInt24 right)
    {
        var result = (ulong)left._value * right._value;
        if (result > MaxValueUInt32)
        {
            throw new OverflowException("UInt24 multiplication overflowed.");
        }

        return new UInt24((uint)result);
    }

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator /(UInt24 left, UInt24 right) => new(left._value / right._value);

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator %(UInt24 left, UInt24 right) => new(left._value % right._value);

    #endregion

    #region Increment/Decrement Operators

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator ++(UInt24 value) => new((value._value + 1u) & MaxValueUInt32);

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator checked ++(UInt24 value)
    {
        if (value._value >= MaxValueUInt32)
        {
            throw new OverflowException("UInt24 increment overflowed.");
        }

        return new UInt24(value._value + 1u);
    }

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator --(UInt24 value) => new((value._value - 1u) & MaxValueUInt32);

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator checked --(UInt24 value)
    {
        if (value._value == 0u)
        {
            throw new OverflowException("UInt24 decrement overflowed.");
        }

        return new UInt24(value._value - 1u);
    }

    #endregion

    #region Unary Operators

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator +(UInt24 value) => value;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator -(UInt24 value) => new((~value._value + 1u) & MaxValueUInt32);

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator checked -(UInt24 value)
    {
        if (value._value != 0u)
        {
            throw new OverflowException("UInt24 negation overflowed.");
        }

        return Zero;
    }

    #endregion

    #region Comparison Operators

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(UInt24 left, UInt24 right) => left._value == right._value;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(UInt24 left, UInt24 right) => left._value != right._value;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(UInt24 left, UInt24 right) => left._value < right._value;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(UInt24 left, UInt24 right) => left._value > right._value;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(UInt24 left, UInt24 right) => left._value <= right._value;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(UInt24 left, UInt24 right) => left._value >= right._value;

    #endregion

    #region Bitwise Operators

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator &(UInt24 left, UInt24 right) => new(left._value & right._value);

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator |(UInt24 left, UInt24 right) => new(left._value | right._value);

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator ^(UInt24 left, UInt24 right) => new(left._value ^ right._value);

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator ~(UInt24 value) => new(~value._value & MaxValueUInt32);

    #endregion

    #region Shift Operators

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator <<(UInt24 value, int shiftAmount) => new((value._value << shiftAmount) & MaxValueUInt32);

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator >>(UInt24 value, int shiftAmount) => new(value._value >> shiftAmount);

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 operator >>>(UInt24 value, int shiftAmount) => new(value._value >>> shiftAmount);

    #endregion

    #region Conversion Operators - Implicit TO UInt24

    /// <summary>
    /// Implicitly converts a <see cref="byte" /> to a <see cref="UInt24" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator UInt24(byte value) => new((uint)value);

    /// <summary>
    /// Implicitly converts a <see cref="ushort" /> to a <see cref="UInt24" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator UInt24(ushort value) => new((uint)value);

    #endregion

    #region Conversion Operators - Implicit FROM UInt24

    /// <summary>
    /// Implicitly converts a <see cref="UInt24" /> to a <see cref="uint" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator uint(UInt24 value) => value._value;

    /// <summary>
    /// Implicitly converts a <see cref="UInt24" /> to an <see cref="int" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator int(UInt24 value) => (int)value._value;

    /// <summary>
    /// Implicitly converts a <see cref="UInt24" /> to a <see cref="long" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator long(UInt24 value) => value._value;

    /// <summary>
    /// Implicitly converts a <see cref="UInt24" /> to a <see cref="ulong" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ulong(UInt24 value) => value._value;

    #endregion

    #region Conversion Operators - Explicit TO UInt24

    /// <summary>
    /// Explicitly converts an <see cref="sbyte" /> to a <see cref="UInt24" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <exception cref="OverflowException"><paramref name="value" /> is negative.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator UInt24(sbyte value) => new(checked((uint)value), false);

    /// <summary>
    /// Explicitly converts a <see cref="short" /> to a <see cref="UInt24" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <exception cref="OverflowException"><paramref name="value" /> is negative.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator UInt24(short value) => new(checked((uint)value), false);

    /// <summary>
    /// Explicitly converts an <see cref="int" /> to a <see cref="UInt24" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <exception cref="OverflowException"><paramref name="value" /> is negative or greater than <see cref="MaxValue" />.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator UInt24(int value) => new(checked((uint)value), true);

    /// <summary>
    /// Explicitly converts a <see cref="uint" /> to a <see cref="UInt24" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <exception cref="OverflowException"><paramref name="value" /> is greater than <see cref="MaxValue" />.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator UInt24(uint value) => new(value, true);

    /// <summary>
    /// Explicitly converts a <see cref="long" /> to a <see cref="UInt24" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <exception cref="OverflowException"><paramref name="value" /> is negative or greater than <see cref="MaxValue" />.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator UInt24(long value) => new(checked((uint)value), true);

    /// <summary>
    /// Explicitly converts a <see cref="ulong" /> to a <see cref="UInt24" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <exception cref="OverflowException"><paramref name="value" /> is greater than <see cref="MaxValue" />.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator UInt24(ulong value) => new(checked((uint)value), true);

    /// <summary>
    /// Explicitly converts a <see cref="nint" /> to a <see cref="UInt24" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <exception cref="OverflowException"><paramref name="value" /> is negative or greater than <see cref="MaxValue" />.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator UInt24(nint value) => new(checked((uint)value), true);

    /// <summary>
    /// Explicitly converts a <see cref="nuint" /> to a <see cref="UInt24" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <exception cref="OverflowException"><paramref name="value" /> is greater than <see cref="MaxValue" />.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator UInt24(nuint value) => new(checked((uint)value), true);

    /// <summary>
    /// Explicitly converts a <see cref="Half" /> to a <see cref="UInt24" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <exception cref="OverflowException"><paramref name="value" /> is negative, greater than <see cref="MaxValue" />, NaN or infinity.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator UInt24(Half value) => new(checked((uint)value), true);

    /// <summary>
    /// Explicitly converts a <see cref="float" /> to a <see cref="UInt24" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <exception cref="OverflowException"><paramref name="value" /> is negative, greater than <see cref="MaxValue" />, NaN or infinity.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator UInt24(float value) => new(checked((uint)value), true);

    /// <summary>
    /// Explicitly converts a <see cref="double" /> to a <see cref="UInt24" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <exception cref="OverflowException"><paramref name="value" /> is negative, greater than <see cref="MaxValue" />, NaN or infinity.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator UInt24(double value) => new(checked((uint)value), true);

    /// <summary>
    /// Explicitly converts a <see cref="decimal" /> to a <see cref="UInt24" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <exception cref="OverflowException"><paramref name="value" /> is negative or greater than <see cref="MaxValue" />.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator UInt24(decimal value) => new(checked((uint)value), true);

    #endregion

    #region Conversion Operators - Explicit FROM UInt24

    /// <summary>
    /// Explicitly converts a <see cref="UInt24" /> to a <see cref="byte" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <exception cref="OverflowException"><paramref name="value" /> is greater than <see cref="byte.MaxValue" />.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator byte(UInt24 value) => checked((byte)value._value);

    /// <summary>
    /// Explicitly converts a <see cref="UInt24" /> to an <see cref="sbyte" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <exception cref="OverflowException"><paramref name="value" /> is greater than <see cref="sbyte.MaxValue" />.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator sbyte(UInt24 value) => checked((sbyte)value._value);

    /// <summary>
    /// Explicitly converts a <see cref="UInt24" /> to a <see cref="short" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <exception cref="OverflowException"><paramref name="value" /> is greater than <see cref="short.MaxValue" />.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator short(UInt24 value) => checked((short)value._value);

    /// <summary>
    /// Explicitly converts a <see cref="UInt24" /> to a <see cref="ushort" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <exception cref="OverflowException"><paramref name="value" /> is greater than <see cref="ushort.MaxValue" />.</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator ushort(UInt24 value) => checked((ushort)value._value);

    #endregion

    #region INumberBase

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsCanonical(UInt24 value) => true;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsComplexNumber(UInt24 value) => false;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEvenInteger(UInt24 value) => (value._value & 1u) == 0u;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFinite(UInt24 value) => true;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsImaginaryNumber(UInt24 value) => false;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInfinity(UInt24 value) => false;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInteger(UInt24 value) => true;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNaN(UInt24 value) => false;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNegative(UInt24 value) => false;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNormal(UInt24 value) => value._value != 0u;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOddInteger(UInt24 value) => (value._value & 1u) != 0u;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPositive(UInt24 value) => true;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsRealNumber(UInt24 value) => true;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNegativeInfinity(UInt24 value) => false;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPositiveInfinity(UInt24 value) => false;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSubnormal(UInt24 value) => false;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero(UInt24 value) => value._value == 0u;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 MaxMagnitude(UInt24 x, UInt24 y) => Max(x, y);

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 MaxMagnitudeNumber(UInt24 x, UInt24 y) => Max(x, y);

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 MinMagnitude(UInt24 x, UInt24 y) => Min(x, y);

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 MinMagnitudeNumber(UInt24 x, UInt24 y) => Min(x, y);

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 Abs(UInt24 value) => value;

    /// <inheritdoc />
    static bool INumberBase<UInt24>.TryConvertFromChecked<TOther>(TOther value, out UInt24 result)
    {
        if (typeof(TOther) == typeof(uint))
        {
            var v = (uint)(object)value;
            if (v > MaxValueUInt32)
            {
                throw new OverflowException("Value was either too large or too small for a UInt24.");
            }

            result = new UInt24(v);
            return true;
        }

        if (typeof(TOther) == typeof(ulong))
        {
            var v = (ulong)(object)value;
            if (v > MaxValueUInt32)
            {
                throw new OverflowException("Value was either too large or too small for a UInt24.");
            }

            result = new UInt24((uint)v);
            return true;
        }

        if (typeof(TOther) == typeof(nuint))
        {
            var v = (nuint)(object)value;
            if (v > MaxValueUInt32)
            {
                throw new OverflowException("Value was either too large or too small for a UInt24.");
            }

            result = new UInt24((uint)v);
            return true;
        }

        result = default;
        return false;
    }

    /// <inheritdoc />
    static bool INumberBase<UInt24>.TryConvertFromSaturating<TOther>(TOther value, out UInt24 result)
    {
        if (typeof(TOther) == typeof(uint))
        {
            var v = (uint)(object)value;
            result = new UInt24(v > MaxValueUInt32 ? MaxValueUInt32 : v);
            return true;
        }

        if (typeof(TOther) == typeof(ulong))
        {
            var v = (ulong)(object)value;
            result = new UInt24(v > MaxValueUInt32 ? MaxValueUInt32 : (uint)v);
            return true;
        }

        if (typeof(TOther) == typeof(nuint))
        {
            var v = (nuint)(object)value;
            result = new UInt24(v > MaxValueUInt32 ? MaxValueUInt32 : (uint)v);
            return true;
        }

        result = default;
        return false;
    }

    /// <inheritdoc />
    static bool INumberBase<UInt24>.TryConvertFromTruncating<TOther>(TOther value, out UInt24 result)
    {
        if (typeof(TOther) == typeof(uint))
        {
            result = new UInt24((uint)(object)value & MaxValueUInt32);
            return true;
        }

        if (typeof(TOther) == typeof(ulong))
        {
            result = new UInt24((uint)((ulong)(object)value & MaxValueUInt32));
            return true;
        }

        if (typeof(TOther) == typeof(nuint))
        {
            result = new UInt24((uint)((nuint)(object)value & MaxValueUInt32));
            return true;
        }

        result = default;
        return false;
    }

    /// <inheritdoc />
    static bool INumberBase<UInt24>.TryConvertToChecked<TOther>(UInt24 value, [MaybeNullWhen(false)] out TOther result)
    {
        if (typeof(TOther) == typeof(byte))
        {
            result = (TOther)(object)checked((byte)value._value);
            return true;
        }

        if (typeof(TOther) == typeof(sbyte))
        {
            result = (TOther)(object)checked((sbyte)value._value);
            return true;
        }

        if (typeof(TOther) == typeof(short))
        {
            result = (TOther)(object)checked((short)value._value);
            return true;
        }

        if (typeof(TOther) == typeof(ushort))
        {
            result = (TOther)(object)checked((ushort)value._value);
            return true;
        }

        if (typeof(TOther) == typeof(int))
        {
            result = (TOther)(object)(int)value._value;
            return true;
        }

        if (typeof(TOther) == typeof(uint))
        {
            result = (TOther)(object)value._value;
            return true;
        }

        if (typeof(TOther) == typeof(long))
        {
            result = (TOther)(object)(long)value._value;
            return true;
        }

        if (typeof(TOther) == typeof(ulong))
        {
            result = (TOther)(object)(ulong)value._value;
            return true;
        }

        result = default;
        return false;
    }

    /// <inheritdoc />
    static bool INumberBase<UInt24>.TryConvertToSaturating<TOther>(UInt24 value, [MaybeNullWhen(false)] out TOther result)
    {
        if (typeof(TOther) == typeof(byte))
        {
            result = (TOther)(object)(value._value > byte.MaxValue ? byte.MaxValue : (byte)value._value);
            return true;
        }

        if (typeof(TOther) == typeof(sbyte))
        {
            result = (TOther)(object)(value._value > (uint)sbyte.MaxValue ? sbyte.MaxValue : (sbyte)value._value);
            return true;
        }

        if (typeof(TOther) == typeof(short))
        {
            result = (TOther)(object)(value._value > (uint)short.MaxValue ? short.MaxValue : (short)value._value);
            return true;
        }

        if (typeof(TOther) == typeof(ushort))
        {
            result = (TOther)(object)(value._value > ushort.MaxValue ? ushort.MaxValue : (ushort)value._value);
            return true;
        }

        if (typeof(TOther) == typeof(int))
        {
            result = (TOther)(object)(int)value._value;
            return true;
        }

        if (typeof(TOther) == typeof(uint))
        {
            result = (TOther)(object)value._value;
            return true;
        }

        if (typeof(TOther) == typeof(long))
        {
            result = (TOther)(object)(long)value._value;
            return true;
        }

        if (typeof(TOther) == typeof(ulong))
        {
            result = (TOther)(object)(ulong)value._value;
            return true;
        }

        result = default;
        return false;
    }

    /// <inheritdoc />
    static bool INumberBase<UInt24>.TryConvertToTruncating<TOther>(UInt24 value, [MaybeNullWhen(false)] out TOther result)
    {
        if (typeof(TOther) == typeof(byte))
        {
            result = (TOther)(object)(byte)value._value;
            return true;
        }

        if (typeof(TOther) == typeof(sbyte))
        {
            result = (TOther)(object)(sbyte)value._value;
            return true;
        }

        if (typeof(TOther) == typeof(short))
        {
            result = (TOther)(object)(short)value._value;
            return true;
        }

        if (typeof(TOther) == typeof(ushort))
        {
            result = (TOther)(object)(ushort)value._value;
            return true;
        }

        if (typeof(TOther) == typeof(int))
        {
            result = (TOther)(object)(int)value._value;
            return true;
        }

        if (typeof(TOther) == typeof(uint))
        {
            result = (TOther)(object)value._value;
            return true;
        }

        if (typeof(TOther) == typeof(long))
        {
            result = (TOther)(object)(long)value._value;
            return true;
        }

        if (typeof(TOther) == typeof(ulong))
        {
            result = (TOther)(object)(ulong)value._value;
            return true;
        }

        result = default;
        return false;
    }

    #endregion

    #region INumber

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 Clamp(UInt24 value, UInt24 min, UInt24 max) =>
        new(Math.Clamp(value._value, min._value, max._value));

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 Max(UInt24 x, UInt24 y) => new(Math.Max(x._value, y._value));

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 Min(UInt24 x, UInt24 y) => new(Math.Min(x._value, y._value));

    /// <inheritdoc />
    [Pure]
    public static int Sign(UInt24 value) => value._value == 0u ? 0 : 1;

    #endregion

    #region IBinaryNumber

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPow2(UInt24 value) => uint.IsPow2(value._value);

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 Log2(UInt24 value) => new((uint)uint.Log2(value._value));

    #endregion

    #region IBinaryInteger

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetByteCount() => 3;

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetShortestBitLength()
    {
        // Delegate to the uint value's calculation.
        return 32 - int.LeadingZeroCount((int)_value);
    }

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 LeadingZeroCount(UInt24 value) =>
        new((uint)(uint.LeadingZeroCount(value._value) - 8));

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 TrailingZeroCount(UInt24 value)
    {
        // If value is 0, TrailingZeroCount on uint returns 32, but for 24-bit we want 24.
        var count = uint.TrailingZeroCount(value._value);
        return new(count > 24u ? 24u : count);
    }

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 PopCount(UInt24 value) => new((uint)uint.PopCount(value._value));

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 RotateLeft(UInt24 value, int rotateAmount)
    {
        rotateAmount = ((rotateAmount % 24) + 24) % 24;
        return new(((value._value << rotateAmount) | (value._value >> (24 - rotateAmount))) & MaxValueUInt32);
    }

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 RotateRight(UInt24 value, int rotateAmount)
    {
        rotateAmount = ((rotateAmount % 24) + 24) % 24;
        return new(((value._value >> rotateAmount) | (value._value << (24 - rotateAmount))) & MaxValueUInt32);
    }

    /// <inheritdoc />
    public bool TryWriteBigEndian(Span<byte> destination, out int bytesWritten)
    {
        if (destination.Length < 3)
        {
            bytesWritten = 0;
            return false;
        }

        destination[0] = (byte)(_value >> 16);
        destination[1] = (byte)(_value >> 8);
        destination[2] = (byte)_value;
        bytesWritten = 3;
        return true;
    }

    /// <inheritdoc />
    public bool TryWriteLittleEndian(Span<byte> destination, out int bytesWritten)
    {
        if (destination.Length < 3)
        {
            bytesWritten = 0;
            return false;
        }

        destination[0] = (byte)_value;
        destination[1] = (byte)(_value >> 8);
        destination[2] = (byte)(_value >> 16);
        bytesWritten = 3;
        return true;
    }

    /// <inheritdoc />
    static bool IBinaryInteger<UInt24>.TryReadBigEndian(ReadOnlySpan<byte> source, bool isUnsigned, out UInt24 value)
    {
        if (source.Length == 0)
        {
            value = default;
            return false;
        }

        // If signed and high bit set, it's negative which is out of range for unsigned.
        if (!isUnsigned && (source[0] & 0x80) != 0)
        {
            value = default;
            return false;
        }

        uint result;

        if (source.Length >= 4)
        {
            // Check that any leading bytes beyond the 3 we need are all zero.
            for (var i = 0; i < source.Length - 3; i++)
            {
                if (source[i] != 0)
                {
                    value = default;
                    return false;
                }
            }

            var offset = source.Length - 3;
            result = (uint)source[offset] << 16 | (uint)source[offset + 1] << 8 | source[offset + 2];
        }
        else if (source.Length == 3)
        {
            result = (uint)source[0] << 16 | (uint)source[1] << 8 | source[2];
        }
        else if (source.Length == 2)
        {
            result = (uint)source[0] << 8 | source[1];
        }
        else
        {
            result = source[0];
        }

        if (result > MaxValueUInt32)
        {
            value = default;
            return false;
        }

        value = new UInt24(result);
        return true;
    }

    /// <inheritdoc />
    static bool IBinaryInteger<UInt24>.TryReadLittleEndian(ReadOnlySpan<byte> source, bool isUnsigned, out UInt24 value)
    {
        if (source.Length == 0)
        {
            value = default;
            return false;
        }

        // If signed and high bit of the last byte is set, it's negative.
        if (!isUnsigned && (source[^1] & 0x80) != 0)
        {
            value = default;
            return false;
        }

        uint result;

        if (source.Length >= 4)
        {
            // Check that any trailing bytes beyond the 3 we need are all zero.
            for (var i = 3; i < source.Length; i++)
            {
                if (source[i] != 0)
                {
                    value = default;
                    return false;
                }
            }

            result = source[0] | (uint)source[1] << 8 | (uint)source[2] << 16;
        }
        else if (source.Length == 3)
        {
            result = source[0] | (uint)source[1] << 8 | (uint)source[2] << 16;
        }
        else if (source.Length == 2)
        {
            result = source[0] | (uint)source[1] << 8;
        }
        else
        {
            result = source[0];
        }

        if (result > MaxValueUInt32)
        {
            value = default;
            return false;
        }

        value = new UInt24(result);
        return true;
    }

    #endregion

    #region CreateChecked/CreateSaturating/CreateTruncating

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 CreateChecked<TOther>(TOther value)
        where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(byte))
        {
            return new UInt24((uint)(byte)(object)value);
        }

        if (typeof(TOther) == typeof(ushort))
        {
            return new UInt24((uint)(ushort)(object)value);
        }

        if (typeof(TOther) == typeof(uint))
        {
            return new UInt24((uint)(object)value, true);
        }

        if (typeof(TOther) == typeof(ulong))
        {
            var v = (ulong)(object)value;
            if (v > MaxValueUInt32)
            {
                throw new OverflowException("Value was either too large or too small for a UInt24.");
            }

            return new UInt24((uint)v);
        }

        if (typeof(TOther) == typeof(sbyte))
        {
            return new UInt24(checked((uint)(sbyte)(object)value), false);
        }

        if (typeof(TOther) == typeof(short))
        {
            return new UInt24(checked((uint)(short)(object)value), false);
        }

        if (typeof(TOther) == typeof(int))
        {
            return new UInt24(checked((uint)(int)(object)value), true);
        }

        if (typeof(TOther) == typeof(long))
        {
            return new UInt24(checked((uint)(long)(object)value), true);
        }

        if (typeof(TOther) == typeof(Half))
        {
            return new UInt24(checked((uint)(Half)(object)value), true);
        }

        if (typeof(TOther) == typeof(float))
        {
            return new UInt24(checked((uint)(float)(object)value), true);
        }

        if (typeof(TOther) == typeof(double))
        {
            return new UInt24(checked((uint)(double)(object)value), true);
        }

        if (typeof(TOther) == typeof(decimal))
        {
            return new UInt24(checked((uint)(decimal)(object)value), true);
        }

        if (typeof(TOther) == typeof(UInt24))
        {
            return (UInt24)(object)value;
        }

        throw new NotSupportedException();
    }

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 CreateSaturating<TOther>(TOther value)
        where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(byte))
        {
            return new UInt24((uint)(byte)(object)value);
        }

        if (typeof(TOther) == typeof(ushort))
        {
            return new UInt24((uint)(ushort)(object)value);
        }

        if (typeof(TOther) == typeof(uint))
        {
            var v = (uint)(object)value;
            return new UInt24(v > MaxValueUInt32 ? MaxValueUInt32 : v);
        }

        if (typeof(TOther) == typeof(ulong))
        {
            var v = (ulong)(object)value;
            return new UInt24(v > MaxValueUInt32 ? MaxValueUInt32 : (uint)v);
        }

        if (typeof(TOther) == typeof(sbyte))
        {
            var v = (sbyte)(object)value;
            return new UInt24(v < 0 ? 0u : (uint)v);
        }

        if (typeof(TOther) == typeof(short))
        {
            var v = (short)(object)value;
            return new UInt24(v < 0 ? 0u : (uint)v);
        }

        if (typeof(TOther) == typeof(int))
        {
            var v = (int)(object)value;
            return new UInt24(v < 0 ? 0u : v > (int)MaxValueUInt32 ? MaxValueUInt32 : (uint)v);
        }

        if (typeof(TOther) == typeof(long))
        {
            var v = (long)(object)value;
            return new UInt24(v < 0L ? 0u : v > MaxValueUInt32 ? MaxValueUInt32 : (uint)v);
        }

        if (typeof(TOther) == typeof(Half))
        {
            var v = (Half)(object)value;
            if (Half.IsNaN(v) || v < Half.Zero)
            {
                return Zero;
            }

            return (float)v >= MaxValueUInt32 ? MaxValue : new UInt24((uint)(float)v);
        }

        if (typeof(TOther) == typeof(float))
        {
            var v = (float)(object)value;
            if (float.IsNaN(v) || v < 0f)
            {
                return Zero;
            }

            return v >= MaxValueUInt32 ? MaxValue : new UInt24((uint)v);
        }

        if (typeof(TOther) == typeof(double))
        {
            var v = (double)(object)value;
            if (double.IsNaN(v) || v < 0.0)
            {
                return Zero;
            }

            return v >= MaxValueUInt32 ? MaxValue : new UInt24((uint)v);
        }

        if (typeof(TOther) == typeof(decimal))
        {
            var v = (decimal)(object)value;
            if (v < 0m)
            {
                return Zero;
            }

            return v >= MaxValueUInt32 ? MaxValue : new UInt24((uint)v);
        }

        if (typeof(TOther) == typeof(UInt24))
        {
            return (UInt24)(object)value;
        }

        throw new NotSupportedException();
    }

    /// <inheritdoc />
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt24 CreateTruncating<TOther>(TOther value)
        where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(byte))
        {
            return new UInt24((uint)(byte)(object)value);
        }

        if (typeof(TOther) == typeof(ushort))
        {
            return new UInt24((uint)(ushort)(object)value);
        }

        if (typeof(TOther) == typeof(uint))
        {
            return new UInt24((uint)(object)value & MaxValueUInt32);
        }

        if (typeof(TOther) == typeof(ulong))
        {
            return new UInt24((uint)((ulong)(object)value & MaxValueUInt32));
        }

        if (typeof(TOther) == typeof(sbyte))
        {
            return new UInt24((uint)(sbyte)(object)value & MaxValueUInt32);
        }

        if (typeof(TOther) == typeof(short))
        {
            return new UInt24((uint)(short)(object)value & MaxValueUInt32);
        }

        if (typeof(TOther) == typeof(int))
        {
            return new UInt24((uint)(int)(object)value & MaxValueUInt32);
        }

        if (typeof(TOther) == typeof(long))
        {
            return new UInt24((uint)(long)(object)value & MaxValueUInt32);
        }

        if (typeof(TOther) == typeof(Half))
        {
            return new UInt24((uint)(float)(Half)(object)value & MaxValueUInt32);
        }

        if (typeof(TOther) == typeof(float))
        {
            return new UInt24((uint)(float)(object)value & MaxValueUInt32);
        }

        if (typeof(TOther) == typeof(double))
        {
            return new UInt24((uint)(double)(object)value & MaxValueUInt32);
        }

        if (typeof(TOther) == typeof(decimal))
        {
            return new UInt24((uint)(decimal)(object)value & MaxValueUInt32);
        }

        if (typeof(TOther) == typeof(UInt24))
        {
            return (UInt24)(object)value;
        }

        throw new NotSupportedException();
    }

    #endregion
}