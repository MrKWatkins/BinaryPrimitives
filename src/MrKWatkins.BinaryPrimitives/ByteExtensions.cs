using System.Numerics;
using System.Runtime.CompilerServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for <see cref="byte" />.
/// </summary>
public static class ByteExtensions
{

    /// <summary>
    /// Copies bits from one byte to another using a mask.
    /// </summary>
    /// <param name="input">The byte to copy bits into.</param>
    /// <param name="toCopyFrom">The byte to copy bits from.</param>
    /// <param name="mask">A mask specifying which bits to copy. Set bits in the mask indicate positions to copy from <paramref name="toCopyFrom" />.</param>
    /// <returns>A new byte with masked bits from <paramref name="toCopyFrom" /> and unmasked bits from <paramref name="input" />.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte CopyBitsFrom(this byte input, byte toCopyFrom, byte mask)
    {
        var bits = toCopyFrom & mask;

        var inputWithoutBits = input & ~mask;

        return (byte)(inputWithoutBits | bits);
    }


    /// <summary>
    /// Determines whether an addition produced a half carry, i.e. a carry from bit 3 to bit 4.
    /// </summary>
    /// <param name="sum">The result of the addition.</param>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true" /> if the addition produced a half carry; <see langword="false" /> otherwise.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DidAdditionHalfCarry(this byte sum, byte left, byte right)
    {
        // Half carry means the addition of the low nibbles will overflow four bits. We cannot just detect a
        // 1 at the 5th bit because there might be a 0/1 combination in there already that would give 1. If there
        // was a 0/0 combination then it would give 0, and if it was 1/1 then it would also give 0. (And a 1 would
        // be carried to bit 6)
        // So:
        // * If we have 0/1 and bit 5 of sum is 0 then we have carried over as 0/1 gave 1 and then we added a carry
        //   to get 0. (With a 1 carrying over into bit 6)
        // * If we have a 0/0 or 1/1 and bit 5 of sum is 1 then we have carried over as 0/0 or 1/1 gave 0 and then
        //   we added a carry to get 1.
        //
        // Therefore we need to check if bit 5 of left and right is different, and then if the answer to that is
        // different from bit 5 of the result.
        return ((left ^ right ^ sum) & 0b00010000) != 0;
    }


    /// <summary>
    /// Determines whether a signed addition overflowed by examining the sum and its operands.
    /// </summary>
    /// <param name="sum">The result of the addition.</param>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true" /> if the addition overflowed; <see langword="false" /> otherwise.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DidAdditionOverflow(this byte sum, byte left, byte right)
    {
        // Hacker's Delight, second edition, page 28.
        // We have an overflow if and only if the following are both true:
        // 1. left and right have the same sign bit. If one is positive and the other is negative then an overflow is not possible.
        // 2. The sign bit of the sum is not the same as the sign bit of the values. This indicates that the sum wrapped around
        //    to the opposite sign.
        // Note that this still works even if the sum includes a 0 or 1 carry!

        // Start by doing sign-of(result) ^ sign-of(left), which will produce a sign-bit of 1 if they differ and 0 if they are the
        // same. Then do the same with sign-of(result) ^ sign-of(right) and combine both results with an AND. If we had a sign swap
        // compared to both inputs sign-of(left) == sign-of(right) and we must have overflowed. We get our sign-off operator by masking
        // with just the sign bit, which we can do once at the end.
        return ((sum ^ left) & (sum ^ right) & 0b10000000) != 0;
    }


    /// <summary>
    /// Determines whether a subtraction produced a half borrow, i.e. a borrow from bit 4 to bit 3.
    /// </summary>
    /// <param name="difference">The result of the subtraction.</param>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true" /> if the subtraction produced a half borrow; <see langword="false" /> otherwise.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DidSubtractionHalfBorrow(this byte difference, byte left, byte right)
    {
        // The same formula for addition will work here too.
        return ((left ^ right ^ difference) & 0b00010000) != 0;
    }


    /// <summary>
    /// Determines whether a signed subtraction overflowed by examining the difference and its operands.
    /// </summary>
    /// <param name="difference">The result of the subtraction.</param>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true" /> if the subtraction overflowed; <see langword="false" /> otherwise.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DidSubtractionOverflow(this byte difference, byte left, byte right)
    {
        // Similar to DidAdditionOverflow, however this time we have an overflow if and only if the following are true:
        // 1. left and right have opposite signs.
        // 2. The sign of the difference is opposite to left. (Or equivalently the same as right)
        // Again this still works even if the difference includes a 0 or 1 carry!
        return ((left ^ right) & (difference ^ left) & 0b10000000) != 0;
    }

    /// <summary>
    /// Gets the value of the bit at the specified index.
    /// </summary>
    /// <param name="value">The byte value.</param>
    /// <param name="index">The zero-based bit index. Must be in the range 0 to 7.</param>
    /// <returns><see langword="true" /> if the bit is set; <see langword="false" /> otherwise.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GetBit(this byte value, int index) => (value & (1 << index)) != 0;


    /// <summary>
    /// Gets a range of bits from a byte, shifted down to the least significant position.
    /// </summary>
    /// <param name="value">The byte value.</param>
    /// <param name="startInclusive">The zero-based start bit index, inclusive.</param>
    /// <param name="endInclusive">The zero-based end bit index, inclusive.</param>
    /// <returns>The extracted bits, shifted right so the start bit is at position 0.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="startInclusive" /> or <paramref name="endInclusive" /> is not in the range 0 to 7, or <paramref name="endInclusive" /> is less than <paramref name="startInclusive" />.</exception>
    [Pure]
    public static byte GetBits(this byte value, int startInclusive, int endInclusive)
    {
        var extractedBits = value & GetBitRangeMask(startInclusive, endInclusive);

        return (byte)(extractedBits >> startInclusive);
    }


    [Pure]
    private static byte GetBitRangeMask(int startInclusive, int endInclusive)
    {
        if (startInclusive is < 0 or > 7)
        {
            throw new ArgumentOutOfRangeException(nameof(startInclusive), startInclusive, "Value must be in the range 0 to 7 inclusive.");
        }
        if (endInclusive is < 0 or > 7)
        {
            throw new ArgumentOutOfRangeException(nameof(endInclusive), endInclusive, "Value must be in the range 0 to 7 inclusive.");
        }
        if (endInclusive < startInclusive)
        {
            throw new ArgumentOutOfRangeException(nameof(endInclusive), endInclusive, $"Value must be greater than or equal to {nameof(endInclusive)} ({startInclusive}).");
        }

        return (byte)((1 << (endInclusive + 1)) - (1 << startInclusive));
    }


    /// <summary>
    /// Gets the high nibble (bits 4-7) of a byte, shifted down to the least significant position.
    /// </summary>
    /// <param name="value">The byte value.</param>
    /// <returns>The high nibble.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte HighNibble(this byte value) => (byte)((value & 0b11110000) >> 4);


    /// <summary>
    /// Gets the left-most bit (bit 7) of a byte.
    /// </summary>
    /// <param name="value">The byte value.</param>
    /// <returns><see langword="true" /> if the left-most bit is set; <see langword="false" /> otherwise.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool LeftMostBit(this byte value) => value.GetBit(7);


    /// <summary>
    /// Gets the low nibble (bits 0-3) of a byte.
    /// </summary>
    /// <param name="value">The byte value.</param>
    /// <returns>The low nibble.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte LowNibble(this byte value) => (byte)(value & 0b00001111);


    /// <summary>
    /// Gets the parity of a byte, i.e. whether the number of set bits is even.
    /// </summary>
    /// <param name="value">The byte value.</param>
    /// <returns><see langword="true" /> if the number of set bits is even; <see langword="false" /> otherwise.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Parity(this byte value) => (BitOperations.PopCount(value) & 1) == 0;


    /// <summary>
    /// Returns a new byte with the bit at the specified index cleared.
    /// </summary>
    /// <param name="value">The byte value.</param>
    /// <param name="index">The zero-based bit index to reset.</param>
    /// <returns>A new byte with the specified bit cleared.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte ResetBit(this byte value, int index) => (byte)(value & ~(1 << index));


    /// <summary>
    /// Gets the right-most bit (bit 0) of a byte.
    /// </summary>
    /// <param name="value">The byte value.</param>
    /// <returns><see langword="true" /> if the right-most bit is set; <see langword="false" /> otherwise.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool RightMostBit(this byte value) => value.GetBit(0);


    /// <summary>
    /// Returns a new byte with the bit at the specified index set.
    /// </summary>
    /// <param name="value">The byte value.</param>
    /// <param name="index">The zero-based bit index to set.</param>
    /// <returns>A new byte with the specified bit set.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte SetBit(this byte value, int index) => (byte)(value | (1 << index));


    /// <summary>
    /// Sets a range of bits in a byte to the specified value.
    /// </summary>
    /// <param name="original">The original byte value.</param>
    /// <param name="value">The value to set in the bit range.</param>
    /// <param name="startInclusive">The zero-based start bit index, inclusive.</param>
    /// <param name="endInclusive">The zero-based end bit index, inclusive.</param>
    /// <returns>A new byte with the specified bit range replaced by <paramref name="value" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="startInclusive" /> or <paramref name="endInclusive" /> is not in the range 0 to 7, or <paramref name="endInclusive" /> is less than <paramref name="startInclusive" />.</exception>
    [Pure]
    public static byte SetBits(this byte original, byte value, int startInclusive, int endInclusive)
    {
        var bitsToPreserve = original & ~GetBitRangeMask(startInclusive, endInclusive);
        var bitsToSet = value & GetBitRangeMask(0, endInclusive - startInclusive);
        var bitsToSetInPosition = bitsToSet << startInclusive;

        return (byte)(bitsToPreserve | bitsToSetInPosition);
    }


    /// <summary>
    /// Returns a new byte with the high nibble (bits 4-7) set to the specified value.
    /// </summary>
    /// <param name="value">The byte value.</param>
    /// <param name="nibble">The nibble value to set. Only the lower 4 bits are used.</param>
    /// <returns>A new byte with the high nibble replaced.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte SetHighNibble(this byte value, byte nibble) => (byte)((value & 0b00001111) | (nibble << 4));


    /// <summary>
    /// Returns a new byte with the low nibble (bits 0-3) set to the specified value.
    /// </summary>
    /// <param name="value">The byte value.</param>
    /// <param name="nibble">The nibble value to set. Only the lower 4 bits are used.</param>
    /// <returns>A new byte with the low nibble replaced.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte SetLowNibble(this byte value, byte nibble) => (byte)((value & 0b11110000) | (nibble & 0b00001111));


    /// <summary>
    /// Gets the sign bit (bit 7) of a byte.
    /// </summary>
    /// <param name="value">The byte value.</param>
    /// <returns><see langword="true" /> if the sign bit is set; <see langword="false" /> otherwise.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SignBit(this byte value) => value.GetBit(7);


    /// <summary>
    /// Converts a byte to its binary string representation, e.g. <c>"0b01011010"</c>.
    /// </summary>
    /// <param name="value">The byte value.</param>
    /// <returns>A 10-character string prefixed with <c>"0b"</c> followed by 8 binary digits.</returns>
    [Pure]
    public static string ToBinaryString(this byte value) =>
        string.Create(10, value, (chars, @byte) =>
        {
            chars[0] = '0';
            chars[1] = 'b';
            BinaryStringHelper.WriteByteChars(chars[2..], @byte);
        });
}