using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace MrKWatkins.BinaryPrimitives;

public static class ByteExtensions
{
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GetBit(this byte value, int index) => (value & (1 << index)) != 0;

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GetBit(this int value, int index) => (value & (1 << index)) != 0;

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

        byte mask = 0;
        for (var f = startInclusive; f <= endInclusive; f++)
        {
            mask |= (byte)(1 << f);
        }

        return mask;
    }

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SignBit(this byte value) => value.GetBit(7);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool LeftMostBit(this byte value) => value.GetBit(7);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool RightMostBit(this byte value) => value.GetBit(0);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte ResetBit(this byte value, int index) => (byte)(value & ~(1 << index));

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte SetBit(this byte value, int index) => (byte)(value | (1 << index));

    [Pure]
    public static byte SetBits(this byte original, byte value, int startInclusive, int endInclusive)
    {
        var bitsToPreserve = original & ~GetBitRangeMask(startInclusive, endInclusive);
        var bitsToSet = value & GetBitRangeMask(0, endInclusive - startInclusive);
        var bitsToSetInPosition = bitsToSet << startInclusive;

        return (byte)(bitsToPreserve | bitsToSetInPosition);
    }

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte LowNibble(this byte value) => (byte)(value & 0b00001111);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte HighNibble(this byte value) => (byte)((value & 0b11110000) >> 4);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte SetLowNibble(this byte value, byte nibble) => (byte)((value & 0b11110000) | (nibble & 0b00001111));

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte SetHighNibble(this byte value, byte nibble) => (byte)((value & 0b00001111) | (nibble << 4));

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Parity(this byte value) => (BitOperations.PopCount(value) & 1) == 0;

    [Pure]
    public static string ToBinaryString(this byte value) =>
        string.Create(10, value, (chars, @byte) =>
        {
            chars[0] = '0';
            chars[1] = 'b';

            // Copy the value to 8 ushort positions in the vector. Using ushorts because chars are 16-bit values.
            var vector = Vector128.Create((ushort)@byte);

            // Isolate one bit in each position.
            var masks = Vector128.Create((ushort)0b10000000, 0b01000000, 0b00100000, 0b00010000, 0b00001000, 0b00000100, 0b00000010, 0b00000001);
            var isolatedBits = Vector128.BitwiseAnd(vector, masks);

            // Now compare each position to its mask. This will give all ones if the original bit was a 1, and all zeroes if it was a 0.
            var compared = Vector128.Equals(isolatedBits, masks);

            // Shift everything right by 15. All ones will become just 1, and 0 will stay as 0.
            var shifted = Vector128.ShiftRightLogical(compared, 15);

            // Create a vector filled with ASCII 0s.
            var zeroes = Vector128.Create((ushort)'0');

            // Add our 1s or 0s from the ASCII 1s, giving us '0' for a 0 bit and '1' for a 1 bit.
            var result = Vector128.Add(zeroes, shifted);

            // Cast the chars to ushorts and copy the vector in.
            var castedChars = MemoryMarshal.Cast<char, ushort>(chars[2..]);
            result.CopyTo(castedChars);
        });

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte CopyBitsFrom(this byte input, byte toCopyFrom, byte mask)
    {
        var bits = toCopyFrom & mask;

        var inputWithoutBits = input & ~mask;

        return (byte)(inputWithoutBits | bits);
    }

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

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DidSubtractionHalfBorrow(this byte difference, byte left, byte right)
    {
        // The same formula for addition will work here too.
        return ((left ^ right ^ difference) & 0b00010000) != 0;
    }
}