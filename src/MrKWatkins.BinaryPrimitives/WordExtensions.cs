using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for <see cref="ushort" /> (word) values and reading/writing words from byte containers.
/// </summary>
public static class WordExtensions
{
    private const int ConcreteTypePriority = 2;
    private const int ReadOnlyPriority = 1;

    [ExcludeFromCodeCoverage]
    static WordExtensions()
    {
        if (!BitConverter.IsLittleEndian)
        {
#pragma warning disable CA1065
            throw new InvalidOperationException("Only little endian processors are supported.");
#pragma warning restore CA1065
        }
    }

    /// <summary>
    /// Gets the value of the bit at the specified index.
    /// </summary>
    /// <param name="value">The word value.</param>
    /// <param name="index">The zero-based bit index.</param>
    /// <returns><see langword="true" /> if the bit is set; <see langword="false" /> otherwise.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GetBit(this ushort value, int index) => (value & (1 << index)) != 0;

    /// <summary>
    /// Gets the most significant byte of a word.
    /// </summary>
    /// <param name="value">The word value.</param>
    /// <returns>The most significant byte.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte MostSignificantByte(this ushort value) => (byte)(value >> 8);

    /// <summary>
    /// Gets the least significant byte of a word.
    /// </summary>
    /// <param name="value">The word value.</param>
    /// <returns>The least significant byte.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte LeastSignificantByte(this ushort value) => (byte)value;

    /// <summary>
    /// Decomposes a word into its most and least significant bytes.
    /// </summary>
    /// <param name="value">The word value.</param>
    /// <returns>A tuple of the most significant byte and least significant byte.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (byte Msb, byte Lsb) ToBytes(this ushort value) => (value.MostSignificantByte(), value.LeastSignificantByte());

    /// <summary>
    /// Gets the sign bit (bit 15) of a word.
    /// </summary>
    /// <param name="value">The word value.</param>
    /// <returns><see langword="true" /> if the sign bit is set; <see langword="false" /> otherwise.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SignBit(this ushort value) => (value & 0b10000000_00000000) != 0;

    /// <summary>
    /// Composes a word from a tuple of most and least significant bytes.
    /// </summary>
    /// <param name="bytes">A tuple of the most significant byte and least significant byte.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The composed word.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort ToWord(this (byte Msb, byte Lsb) bytes, Endian endian = Endian.Little) => endian.ToWord(bytes.Msb, bytes.Lsb);

    /// <summary>
    /// Composes a word from two bytes.
    /// </summary>
    /// <param name="endian">The endianness to use.</param>
    /// <param name="msb">The most significant byte.</param>
    /// <param name="lsb">The least significant byte.</param>
    /// <returns>The composed word.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort ToWord(this Endian endian, byte msb, byte lsb) =>
        endian == Endian.Little
            ? (ushort)(msb | lsb << 8)
            : (ushort)(lsb | msb << 8);

    /// <summary>
    /// Adds a word to a byte collection.
    /// </summary>
    /// <param name="bytes">The byte collection to add to.</param>
    /// <param name="value">The word value to add.</param>
    /// <param name="endian">The endianness to use.</param>
    public static void AddWord(this ICollection<byte> bytes, ushort value, Endian endian = Endian.Little)
    {
        var (msb, lsb) = value.ToBytes();
        if (endian == Endian.Little)
        {
            bytes.Add(lsb);
            bytes.Add(msb);
        }
        else
        {
            bytes.Add(msb);
            bytes.Add(lsb);
        }
    }

    /// <summary>
    /// Reads a little-endian word from a byte array at the specified index.
    /// </summary>
    /// <param name="bytes">The byte array.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The word value.</returns>
    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this byte[] bytes, int index) => Unsafe.ReadUnaligned<ushort>(ref bytes[index]);

    /// <summary>
    /// Reads a word from a byte array at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The byte array.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The word value.</returns>
    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this byte[] bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetWord(index)
            : (ushort)(bytes[index + 1] | bytes[index] << 8);

    /// <summary>
    /// Reads a little-endian word from a span of bytes.
    /// </summary>
    /// <param name="bytes">The span of bytes.</param>
    /// <returns>The word value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this Span<byte> bytes) => MemoryMarshal.Read<ushort>(bytes);

    /// <summary>
    /// Reads a word from a span of bytes using the specified endianness.
    /// </summary>
    /// <param name="bytes">The span of bytes.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The word value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this Span<byte> bytes, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetWord()
            : System.Buffers.Binary.BinaryPrimitives.ReadUInt16BigEndian(bytes);

    /// <summary>
    /// Reads a little-endian word from a read-only span of bytes.
    /// </summary>
    /// <param name="bytes">The read-only span of bytes.</param>
    /// <returns>The word value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this ReadOnlySpan<byte> bytes) => MemoryMarshal.Read<ushort>(bytes);

    /// <summary>
    /// Reads a word from a read-only span of bytes using the specified endianness.
    /// </summary>
    /// <param name="bytes">The read-only span of bytes.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The word value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this ReadOnlySpan<byte> bytes, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetWord()
            : System.Buffers.Binary.BinaryPrimitives.ReadUInt16BigEndian(bytes);

    /// <summary>
    /// Reads a little-endian word from a list of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The word value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this IList<byte> bytes, int index) => (ushort)(bytes[index] | bytes[index + 1] << 8);

    /// <summary>
    /// Reads a word from a list of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The word value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this IList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetWord(index)
            : (ushort)(bytes[index + 1] | bytes[index] << 8);

    /// <summary>
    /// Reads a little-endian word from a read-only list of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The read-only list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The word value.</returns>
    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this IReadOnlyList<byte> bytes, int index) => (ushort)(bytes[index] | bytes[index + 1] << 8);

    /// <summary>
    /// Reads a word from a read-only list of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The read-only list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The word value.</returns>
    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this IReadOnlyList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetWord(index)
            : (ushort)(bytes[index + 1] | bytes[index] << 8);

    /// <summary>
    /// Reads a little-endian word from a <see cref="List{T}" /> of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The word value.</returns>
    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this List<byte> bytes, int index) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetWord();

    /// <summary>
    /// Reads a word from a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The word value.</returns>
    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this List<byte> bytes, int index, Endian endian) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetWord(endian);

    /// <summary>
    /// Writes a little-endian word to a byte array at the specified index.
    /// </summary>
    /// <param name="bytes">The byte array.</param>
    /// <param name="index">The zero-based index to write to.</param>
    /// <param name="value">The word value to write.</param>
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetWord(this byte[] bytes, int index, ushort value) => bytes.AsSpan(index).SetWord(value);

    /// <summary>
    /// Writes a word to a byte array at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The byte array.</param>
    /// <param name="index">The zero-based index to write to.</param>
    /// <param name="value">The word value to write.</param>
    /// <param name="endian">The endianness to use.</param>
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetWord(this byte[] bytes, int index, ushort value, Endian endian) => bytes.AsSpan(index).SetWord(value, endian);

    /// <summary>
    /// Writes a little-endian word to a span of bytes.
    /// </summary>
    /// <param name="bytes">The span of bytes.</param>
    /// <param name="value">The word value to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetWord(this Span<byte> bytes, ushort value) => MemoryMarshal.Write(bytes, value);

    /// <summary>
    /// Writes a word to a span of bytes using the specified endianness.
    /// </summary>
    /// <param name="bytes">The span of bytes.</param>
    /// <param name="value">The word value to write.</param>
    /// <param name="endian">The endianness to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetWord(this Span<byte> bytes, ushort value, Endian endian)
    {
        if (endian == Endian.Little)
        {
            bytes.SetWord(value);
        }
        else
        {
            System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes, value);
        }
    }

    /// <summary>
    /// Writes a little-endian word to a list of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to write to.</param>
    /// <param name="value">The word value to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetWord(this IList<byte> bytes, int index, ushort value)
    {
        var (msb, lsb) = value.ToBytes();
        bytes[index] = lsb;
        bytes[index + 1] = msb;
    }

    /// <summary>
    /// Writes a word to a list of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to write to.</param>
    /// <param name="value">The word value to write.</param>
    /// <param name="endian">The endianness to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetWord(this IList<byte> bytes, int index, ushort value, Endian endian)
    {
        var (msb, lsb) = value.ToBytes();
        if (endian == Endian.Little)
        {
            bytes.SetWord(index, value);
        }
        else
        {
            bytes[index] = msb;
            bytes[index + 1] = lsb;
        }
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
    public static bool DidAdditionOverflow(this ushort sum, ushort left, ushort right)
    {
        // See ByteExtensions.DidAdditionOverflow for an explanation.
        return ((sum ^ left) & (sum ^ right) & 0b10000000_00000000) != 0;
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
    public static bool DidSubtractionOverflow(this ushort difference, ushort left, ushort right)
    {
        // See ByteExtensions.DidSubtractionOverflow for an explanation.
        return ((left ^ right) & (difference ^ left) & 0b10000000_00000000) != 0;
    }

    /// <summary>
    /// Determines whether an addition produced a half carry, i.e. a carry from bit 11 to bit 12.
    /// </summary>
    /// <param name="sum">The result of the addition.</param>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true" /> if the addition produced a half carry; <see langword="false" /> otherwise.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DidAdditionHalfCarry(this ushort sum, ushort left, ushort right)
    {
        // See ByteExtensions.DidAdditionHalfCarry for an explanation. Half-carry for words is
        // the half carry in the high byte, i.e. bit 13.
        return ((left ^ right ^ sum) & 0b00010000_00000000) != 0;
    }

    /// <summary>
    /// Determines whether a subtraction produced a half borrow, i.e. a borrow from bit 12 to bit 11.
    /// </summary>
    /// <param name="difference">The result of the subtraction.</param>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns><see langword="true" /> if the subtraction produced a half borrow; <see langword="false" /> otherwise.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DidSubtractionHalfBorrow(this ushort difference, ushort left, ushort right)
    {
        // See ByteExtensions.DidSubtractionHalfBorrow for an explanation. Half-borrow for words is
        // the half carry in the high byte, i.e. bit 13.
        return ((left ^ right ^ difference) & 0b00010000_00000000) != 0;
    }
}
