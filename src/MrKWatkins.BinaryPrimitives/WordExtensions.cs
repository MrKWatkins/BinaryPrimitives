using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.BinaryPrimitives;

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

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GetBit(this ushort value, int index) => (value & (1 << index)) != 0;

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte MostSignificantByte(this ushort value) => (byte)(value >> 8);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte LeastSignificantByte(this ushort value) => (byte)value;

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (byte Msb, byte Lsb) ToBytes(this ushort value) => (value.MostSignificantByte(), value.LeastSignificantByte());

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool SignBit(this ushort value) => (value & 0b10000000_00000000) != 0;

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort ToWord(this (byte Msb, byte Lsb) bytes, Endian endian = Endian.Little) => endian.ToWord(bytes.Msb, bytes.Lsb);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort ToWord(this Endian endian, byte msb, byte lsb) =>
        endian == Endian.Little
            ? (ushort)(msb | lsb << 8)
            : (ushort)(lsb | msb << 8);

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

    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this byte[] bytes, int index) => Unsafe.ReadUnaligned<ushort>(ref bytes[index]);

    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this byte[] bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetWord(index)
            : (ushort)(bytes[index + 1] | bytes[index] << 8);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this Span<byte> bytes) => MemoryMarshal.Read<ushort>(bytes);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this Span<byte> bytes, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetWord()
            : System.Buffers.Binary.BinaryPrimitives.ReadUInt16BigEndian(bytes);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this ReadOnlySpan<byte> bytes) => MemoryMarshal.Read<ushort>(bytes);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this ReadOnlySpan<byte> bytes, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetWord()
            : System.Buffers.Binary.BinaryPrimitives.ReadUInt16BigEndian(bytes);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this IList<byte> bytes, int index) => (ushort)(bytes[index] | bytes[index + 1] << 8);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this IList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetWord(index)
            : (ushort)(bytes[index + 1] | bytes[index] << 8);

    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this IReadOnlyList<byte> bytes, int index) => (ushort)(bytes[index] | bytes[index + 1] << 8);

    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this IReadOnlyList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetWord(index)
            : (ushort)(bytes[index + 1] | bytes[index] << 8);

    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this List<byte> bytes, int index) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetWord();

    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort GetWord(this List<byte> bytes, int index, Endian endian) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetWord(endian);

    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetWord(this byte[] bytes, int index, ushort value) => bytes.AsSpan(index).SetWord(value);

    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetWord(this byte[] bytes, int index, ushort value, Endian endian) => bytes.AsSpan(index).SetWord(value, endian);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetWord(this Span<byte> bytes, ushort value) => MemoryMarshal.Write(bytes, value);

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetWord(this IList<byte> bytes, int index, ushort value)
    {
        var (msb, lsb) = value.ToBytes();
        bytes[index] = lsb;
        bytes[index + 1] = msb;
    }

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

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DidAdditionOverflow(this ushort sum, ushort left, ushort right)
    {
        // See ByteExtensions.DidAdditionOverflow for an explanation.
        return ((sum ^ left) & (sum ^ right) & 0b10000000_00000000) != 0;
    }

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DidSubtractionOverflow(this ushort difference, ushort left, ushort right)
    {
        // See ByteExtensions.DidSubtractionOverflow for an explanation.
        return ((left ^ right) & (difference ^ left) & 0b10000000_00000000) != 0;
    }

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DidAdditionHalfCarry(this ushort sum, ushort left, ushort right)
    {
        // See ByteExtensions.DidAdditionHalfCarry for an explanation. Half-carry for words is
        // the half carry in the high byte, i.e. bit 13.
        return ((left ^ right ^ sum) & 0b00010000_00000000) != 0;
    }

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool DidSubtractionHalfBorrow(this ushort difference, ushort left, ushort right)
    {
        // See ByteExtensions.DidSubtractionHalfBorrow for an explanation. Half-borrow for words is
        // the half carry in the high byte, i.e. bit 13.
        return ((left ^ right ^ difference) & 0b00010000_00000000) != 0;
    }
}