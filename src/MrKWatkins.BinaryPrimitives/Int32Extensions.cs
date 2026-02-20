using System.Runtime.CompilerServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for <see cref="int" />.
/// </summary>
public static class Int32Extensions
{
    /// <param name="value">The int value.</param>
    extension(int value)
    {
        /// <summary>
        /// Gets the value of the bit at the specified index.
        /// </summary>
        /// <param name="index">The zero-based bit index. Must be in the range 0 to 31.</param>
        /// <returns><see langword="true" /> if the bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool GetBit(int index) => (value & (1 << index)) != 0;

        /// <summary>
        /// Gets a range of bits from an int, shifted down to the least significant position.
        /// </summary>
        /// <param name="startInclusive">The zero-based start bit index, inclusive.</param>
        /// <param name="endInclusive">The zero-based end bit index, inclusive.</param>
        /// <returns>The extracted bits, shifted right so the start bit is at position 0.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startInclusive" /> or <paramref name="endInclusive" /> is not in the range 0 to 31, or <paramref name="endInclusive" /> is less than <paramref name="startInclusive" />.</exception>
        [Pure]
        public int GetBits(int startInclusive, int endInclusive)
        {
            var extractedBits = value & GetBitRangeMask(startInclusive, endInclusive);
            return extractedBits >> startInclusive;
        }

        /// <summary>
        /// Returns a new int with the bit at the specified index cleared.
        /// </summary>
        /// <param name="index">The zero-based bit index to reset.</param>
        /// <returns>A new int with the specified bit cleared.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ResetBit(int index) => value & ~(1 << index);

        /// <summary>
        /// Returns a new int with the bit at the specified index set.
        /// </summary>
        /// <param name="index">The zero-based bit index to set.</param>
        /// <returns>A new int with the specified bit set.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int SetBit(int index) => value | (1 << index);

        /// <summary>
        /// Sets a range of bits in an int to the specified value.
        /// </summary>
        /// <param name="bits">The value to set in the bit range.</param>
        /// <param name="startInclusive">The zero-based start bit index, inclusive.</param>
        /// <param name="endInclusive">The zero-based end bit index, inclusive.</param>
        /// <returns>A new int with the specified bit range replaced by <paramref name="bits" />.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startInclusive" /> or <paramref name="endInclusive" /> is not in the range 0 to 31, or <paramref name="endInclusive" /> is less than <paramref name="startInclusive" />.</exception>
        [Pure]
        public int SetBits(int bits, int startInclusive, int endInclusive)
        {
            var mask = GetBitRangeMask(startInclusive, endInclusive);
            var bitsToPreserve = value & ~mask;
            var bitsToSet = bits & GetBitRangeMask(0, endInclusive - startInclusive);
            var bitsToSetInPosition = bitsToSet << startInclusive;
            return bitsToPreserve | bitsToSetInPosition;
        }

        /// <summary>
        /// Gets the sign bit (bit 31) of an int.
        /// </summary>
        /// <returns><see langword="true" /> if the sign bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SignBit() => (value & unchecked((int)0x80000000)) != 0;

        /// <summary>
        /// Gets the left-most bit (bit 31) of an int.
        /// </summary>
        /// <returns><see langword="true" /> if the left-most bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool LeftMostBit() => value.SignBit();

        /// <summary>
        /// Gets the right-most bit (bit 0) of an int.
        /// </summary>
        /// <returns><see langword="true" /> if the right-most bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool RightMostBit() => value.GetBit(0);

        /// <summary>
        /// Converts an int to its binary string representation, e.g. <c>"0b00000000000000000000000001011010"</c>.
        /// </summary>
        /// <returns>A 34-character string prefixed with <c>"0b"</c> followed by 32 binary digits.</returns>
        [Pure]
        public string ToBinaryString() =>
            string.Create(34, value, (chars, v) =>
            {
                chars[0] = '0';
                chars[1] = 'b';
                BinaryStringHelper.WriteUInt32Chars(chars[2..], (uint)v);
            });
    }

    [Pure]
    private static int GetBitRangeMask(int startInclusive, int endInclusive)
    {
        if (startInclusive is < 0 or > 31)
        {
            throw new ArgumentOutOfRangeException(nameof(startInclusive), startInclusive, "Value must be in the range 0 to 31 inclusive.");
        }
        if (endInclusive is < 0 or > 31)
        {
            throw new ArgumentOutOfRangeException(nameof(endInclusive), endInclusive, "Value must be in the range 0 to 31 inclusive.");
        }
        if (endInclusive < startInclusive)
        {
            throw new ArgumentOutOfRangeException(nameof(endInclusive), endInclusive, $"Value must be greater than or equal to {nameof(endInclusive)} ({startInclusive}).");
        }

        return (int)(((1L << endInclusive) << 1) - (1L << startInclusive));
    }
}