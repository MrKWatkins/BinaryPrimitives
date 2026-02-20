using System.Runtime.CompilerServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for <see cref="uint" />.
/// </summary>
public static class UInt32Extensions
{
    /// <param name="value">The uint value.</param>
    extension(uint value)
    {
        /// <summary>
        /// Gets the value of the bit at the specified index.
        /// </summary>
        /// <param name="index">The zero-based bit index. Must be in the range 0 to 31.</param>
        /// <returns><see langword="true" /> if the bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool GetBit(int index) => (value & (1u << index)) != 0;

        /// <summary>
        /// Gets the left-most bit (bit 31) of a uint.
        /// </summary>
        /// <returns><see langword="true" /> if the left-most bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool LeftMostBit() => value.SignBit();

        /// <summary>
        /// Returns a new uint with the bit at the specified index cleared.
        /// </summary>
        /// <param name="index">The zero-based bit index to reset.</param>
        /// <returns>A new uint with the specified bit cleared.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint ResetBit(int index) => value & ~(1u << index);

        /// <summary>
        /// Gets the right-most bit (bit 0) of a uint.
        /// </summary>
        /// <returns><see langword="true" /> if the right-most bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool RightMostBit() => value.GetBit(0);

        /// <summary>
        /// Returns a new uint with the bit at the specified index set.
        /// </summary>
        /// <param name="index">The zero-based bit index to set.</param>
        /// <returns>A new uint with the specified bit set.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint SetBit(int index) => value | (1u << index);

        /// <summary>
        /// Gets the sign bit (bit 31) of a uint.
        /// </summary>
        /// <returns><see langword="true" /> if the sign bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SignBit() => (value & 0x80000000u) != 0;

        /// <summary>
        /// Converts a uint to its binary string representation, e.g. <c>"0b00000001000000100000001100000100"</c>.
        /// </summary>
        /// <returns>A 34-character string prefixed with <c>"0b"</c> followed by 32 binary digits.</returns>
        [Pure]
        public string ToBinaryString() =>
            string.Create(34, value, (chars, v) =>
            {
                chars[0] = '0';
                chars[1] = 'b';
                BinaryStringHelper.WriteUInt32Chars(chars[2..], v);
            });
    }
}