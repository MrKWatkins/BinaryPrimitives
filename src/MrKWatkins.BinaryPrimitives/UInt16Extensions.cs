using System.Runtime.CompilerServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for <see cref="ushort" /> (word) values.
/// </summary>
public static class UInt16Extensions
{
    /// <param name="value">The word value.</param>
    extension(ushort value)
    {
        /// <summary>
        /// Determines whether an addition produced a half carry, i.e. a carry from bit 11 to bit 12.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns><see langword="true" /> if the addition produced a half carry; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool DidAdditionHalfCarry(ushort left, ushort right)
        {
            // See ByteExtensions.DidAdditionHalfCarry for an explanation. Half-carry for words is
            // the half carry in the high byte, i.e. bit 13.
            return ((left ^ right ^ value) & 0b00010000_00000000) != 0;
        }

        /// <summary>
        /// Determines whether a signed addition overflowed by examining the sum and its operands.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns><see langword="true" /> if the addition overflowed; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool DidAdditionOverflow(ushort left, ushort right)
        {
            // See ByteExtensions.DidAdditionOverflow for an explanation.
            return ((value ^ left) & (value ^ right) & 0b10000000_00000000) != 0;
        }

        /// <summary>
        /// Determines whether a subtraction produced a half borrow, i.e. a borrow from bit 12 to bit 11.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns><see langword="true" /> if the subtraction produced a half borrow; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool DidSubtractionHalfBorrow(ushort left, ushort right)
        {
            // See ByteExtensions.DidSubtractionHalfBorrow for an explanation. Half-borrow for words is
            // the half carry in the high byte, i.e. bit 13.
            return ((left ^ right ^ value) & 0b00010000_00000000) != 0;
        }

        /// <summary>
        /// Determines whether a signed subtraction overflowed by examining the difference and its operands.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns><see langword="true" /> if the subtraction overflowed; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool DidSubtractionOverflow(ushort left, ushort right)
        {
            // See ByteExtensions.DidSubtractionOverflow for an explanation.
            return ((left ^ right) & (value ^ left) & 0b10000000_00000000) != 0;
        }

        /// <summary>
        /// Gets the value of the bit at the specified index.
        /// </summary>
        /// <param name="index">The zero-based bit index. Must be in the range 0 to 15.</param>
        /// <returns><see langword="true" /> if the bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool GetBit(int index) => (value & (1 << index)) != 0;

        /// <summary>
        /// Gets the least significant byte of a word.
        /// </summary>
        /// <returns>The least significant byte.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte LeastSignificantByte() => (byte)value;

        /// <summary>
        /// Gets the left-most bit (bit 15) of a word.
        /// </summary>
        /// <returns><see langword="true" /> if the left-most bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool LeftMostBit() => value.SignBit();

        /// <summary>
        /// Gets the most significant byte of a word.
        /// </summary>
        /// <returns>The most significant byte.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte MostSignificantByte() => (byte)(value >> 8);

        /// <summary>
        /// Returns a new word with the bit at the specified index cleared.
        /// </summary>
        /// <param name="index">The zero-based bit index to reset.</param>
        /// <returns>A new word with the specified bit cleared.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort ResetBit(int index) => (ushort)(value & ~(1 << index));

        /// <summary>
        /// Gets the right-most bit (bit 0) of a word.
        /// </summary>
        /// <returns><see langword="true" /> if the right-most bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool RightMostBit() => value.GetBit(0);

        /// <summary>
        /// Returns a new word with the bit at the specified index set.
        /// </summary>
        /// <param name="index">The zero-based bit index to set.</param>
        /// <returns>A new word with the specified bit set.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort SetBit(int index) => (ushort)(value | (1 << index));

        /// <summary>
        /// Gets the sign bit (bit 15) of a word.
        /// </summary>
        /// <returns><see langword="true" /> if the sign bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SignBit() => (value & 0b10000000_00000000) != 0;

        /// <summary>
        /// Converts a word to its binary string representation, e.g. <c>"0b0001001000110100"</c>.
        /// </summary>
        /// <returns>An 18-character string prefixed with <c>"0b"</c> followed by 16 binary digits.</returns>
        [Pure]
        public string ToBinaryString() =>
            string.Create(18, value, (chars, v) =>
            {
                chars[0] = '0';
                chars[1] = 'b';
                BinaryStringHelper.WriteWordChars(chars[2..], v);
            });

        /// <summary>
        /// Decomposes a word into its most and least significant bytes.
        /// </summary>
        /// <returns>A tuple of the most significant byte and least significant byte.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (byte Msb, byte Lsb) ToBytes() => (value.MostSignificantByte(), value.LeastSignificantByte());
    }
}