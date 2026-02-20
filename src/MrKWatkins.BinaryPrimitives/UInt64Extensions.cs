using System.Runtime.CompilerServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for <see cref="ulong" />.
/// </summary>
public static class UInt64Extensions
{
    /// <param name="value">The ulong value.</param>
    extension(ulong value)
    {
        /// <summary>
        /// Gets the value of the bit at the specified index.
        /// </summary>
        /// <param name="index">The zero-based bit index. Must be in the range 0 to 63.</param>
        /// <returns><see langword="true" /> if the bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool GetBit(int index) => (value & (1UL << index)) != 0;

        /// <summary>
        /// Gets the left-most bit (bit 63) of a ulong.
        /// </summary>
        /// <returns><see langword="true" /> if the left-most bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool LeftMostBit() => value.SignBit();

        /// <summary>
        /// Returns a new ulong with the bit at the specified index cleared.
        /// </summary>
        /// <param name="index">The zero-based bit index to reset.</param>
        /// <returns>A new ulong with the specified bit cleared.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong ResetBit(int index) => value & ~(1UL << index);

        /// <summary>
        /// Gets the right-most bit (bit 0) of a ulong.
        /// </summary>
        /// <returns><see langword="true" /> if the right-most bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool RightMostBit() => value.GetBit(0);

        /// <summary>
        /// Returns a new ulong with the bit at the specified index set.
        /// </summary>
        /// <param name="index">The zero-based bit index to set.</param>
        /// <returns>A new ulong with the specified bit set.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong SetBit(int index) => value | (1UL << index);

        /// <summary>
        /// Gets the sign bit (bit 63) of a ulong.
        /// </summary>
        /// <returns><see langword="true" /> if the sign bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SignBit() => (value & 0x8000000000000000UL) != 0;

        /// <summary>
        /// Converts a ulong to its binary string representation, e.g. <c>"0b0000000100000010000000110000010000000101000001100000011100001000"</c>.
        /// </summary>
        /// <returns>A 66-character string prefixed with <c>"0b"</c> followed by 64 binary digits.</returns>
        [Pure]
        public string ToBinaryString() =>
            string.Create(66, value, (chars, v) =>
            {
                chars[0] = '0';
                chars[1] = 'b';
                BinaryStringHelper.WriteUInt32Chars(chars[2..], (uint)(v >> 32));
                BinaryStringHelper.WriteUInt32Chars(chars[34..], (uint)v);
            });
    }
}