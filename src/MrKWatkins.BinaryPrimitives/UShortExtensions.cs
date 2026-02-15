using System.Runtime.CompilerServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for <see cref="ushort" /> (word) values.
/// </summary>
public static class UShortExtensions
{
    /// <param name="value">The word value.</param>
    extension(ushort value)
    {
        /// <summary>
        /// Gets the value of the bit at the specified index.
        /// </summary>
        /// <param name="index">The zero-based bit index.</param>
        /// <returns><see langword="true" /> if the bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool GetBit(int index) => (value & (1 << index)) != 0;

        /// <summary>
        /// Gets the most significant byte of a word.
        /// </summary>
        /// <returns>The most significant byte.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte MostSignificantByte() => (byte)(value >> 8);

        /// <summary>
        /// Gets the least significant byte of a word.
        /// </summary>
        /// <returns>The least significant byte.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte LeastSignificantByte() => (byte)value;

        /// <summary>
        /// Decomposes a word into its most and least significant bytes.
        /// </summary>
        /// <returns>A tuple of the most significant byte and least significant byte.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (byte Msb, byte Lsb) ToBytes() => (value.MostSignificantByte(), value.LeastSignificantByte());

        /// <summary>
        /// Gets the sign bit (bit 15) of a word.
        /// </summary>
        /// <returns><see langword="true" /> if the sign bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SignBit() => (value & 0b10000000_00000000) != 0;

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
    }
}