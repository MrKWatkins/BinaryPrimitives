using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.BinaryPrimitives;

public static class UInt32Extensions
{
    private const int ConcreteTypePriority = 2;
    private const int ReadOnlyPriority = 1;

    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this byte[] bytes, int index) => Unsafe.ReadUnaligned<uint>(ref bytes[index]);

    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this byte[] bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetUInt32(index)
            : bytes.AsSpan(index).GetUInt32(Endian.Big);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this Span<byte> bytes) => MemoryMarshal.Read<uint>(bytes);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this Span<byte> bytes, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetUInt32()
            : System.Buffers.Binary.BinaryPrimitives.ReadUInt32BigEndian(bytes);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this ReadOnlySpan<byte> bytes) => MemoryMarshal.Read<uint>(bytes);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this ReadOnlySpan<byte> bytes, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetUInt32()
            : System.Buffers.Binary.BinaryPrimitives.ReadUInt32BigEndian(bytes);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this IList<byte> bytes, int index) => (uint)(bytes[index] | bytes[index + 1] << 8 | bytes[index + 2] << 16 | bytes[index + 3] << 24);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this IList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetUInt32(index)
            : (uint)(bytes[index + 3] | bytes[index + 2] << 8 | bytes[index + 1] << 16 | bytes[index] << 24);

    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this IReadOnlyList<byte> bytes, int index) => (uint)(bytes[index] | bytes[index + 1] << 8 | bytes[index + 2] << 16 | bytes[index + 3] << 24);

    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this IReadOnlyList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetUInt32(index)
            : (uint)(bytes[index + 3] | bytes[index + 2] << 8 | bytes[index + 1] << 16 | bytes[index] << 24);

    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this List<byte> bytes, int index) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetUInt32();

    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this List<byte> bytes, int index, Endian endian) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetUInt32(endian);

    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetUInt32(this byte[] bytes, int index, uint value) => bytes.AsSpan(index).SetUInt32(value);

    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetUInt32(this byte[] bytes, int index, uint value, Endian endian) => bytes.AsSpan(index).SetUInt32(value, endian);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetUInt32(this Span<byte> bytes, uint value) => Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(bytes), value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetUInt32(this Span<byte> bytes, uint value, Endian endian)
    {
        if (endian == Endian.Little)
        {
            System.Buffers.Binary.BinaryPrimitives.WriteUInt32LittleEndian(bytes, value);
        }
        else
        {
            System.Buffers.Binary.BinaryPrimitives.WriteUInt32BigEndian(bytes, value);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetUInt32(this IList<byte> bytes, int index, uint value)
    {
        bytes[index] = (byte)(value & 0x000000FF);
        bytes[index + 1] = (byte)((value & 0x0000FF00) >> 8);
        bytes[index + 2] = (byte)((value & 0x00FF0000) >> 16);
        bytes[index + 3] = (byte)((value & 0xFF000000) >> 24);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetUInt32(this IList<byte> bytes, int index, uint value, Endian endian)
    {
        if (endian == Endian.Little)
        {
            bytes.SetUInt32(index, value);
        }
        else
        {
            bytes[index + 3] = (byte)(value & 0x000000FF);
            bytes[index + 2] = (byte)((value & 0x0000FF00) >> 8);
            bytes[index + 1] = (byte)((value & 0x00FF0000) >> 16);
            bytes[index] = (byte)((value & 0xFF000000) >> 24);
        }
    }
}