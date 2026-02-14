using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.BinaryPrimitives;

public static class Int32Extensions
{
    private const int ConcreteTypePriority = 2;
    private const int ReadOnlyPriority = 1;

    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this byte[] bytes, int index) => Unsafe.ReadUnaligned<int>(ref bytes[index]);

    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this byte[] bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetInt32(index)
            : bytes.AsSpan(index).GetInt32(Endian.Big);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this Span<byte> bytes) => MemoryMarshal.Read<int>(bytes);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this Span<byte> bytes, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetInt32()
            : System.Buffers.Binary.BinaryPrimitives.ReadInt32BigEndian(bytes);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this ReadOnlySpan<byte> bytes) => MemoryMarshal.Read<int>(bytes);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this ReadOnlySpan<byte> bytes, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetInt32()
            : System.Buffers.Binary.BinaryPrimitives.ReadInt32BigEndian(bytes);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this IList<byte> bytes, int index) => bytes[index] | bytes[index + 1] << 8 | bytes[index + 2] << 16 | bytes[index + 3] << 24;

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this IList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetInt32(index)
            : bytes[index + 3] | bytes[index + 2] << 8 | bytes[index + 1] << 16 | bytes[index] << 24;

    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this IReadOnlyList<byte> bytes, int index) => bytes[index] | bytes[index + 1] << 8 | bytes[index + 2] << 16 | bytes[index + 3] << 24;

    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this IReadOnlyList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetInt32(index)
            : bytes[index + 3] | bytes[index + 2] << 8 | bytes[index + 1] << 16 | bytes[index] << 24;

    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this List<byte> bytes, int index) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetInt32();

    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this List<byte> bytes, int index, Endian endian) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetInt32(endian);

    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetInt32(this byte[] bytes, int index, int value) => bytes.AsSpan(index).SetInt32(value);

    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetInt32(this byte[] bytes, int index, int value, Endian endian) => bytes.AsSpan(index).SetInt32(value, endian);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetInt32(this Span<byte> bytes, int value) => Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(bytes), value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetInt32(this Span<byte> bytes, int value, Endian endian)
    {
        if (endian == Endian.Little)
        {
            System.Buffers.Binary.BinaryPrimitives.WriteInt32LittleEndian(bytes, value);
        }
        else
        {
            System.Buffers.Binary.BinaryPrimitives.WriteInt32BigEndian(bytes, value);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetInt32(this IList<byte> bytes, int index, int value)
    {
        bytes[index] = (byte)(value & 0x000000FF);
        bytes[index + 1] = (byte)((value & 0x0000FF00) >> 8);
        bytes[index + 2] = (byte)((value & 0x00FF0000) >> 16);
        bytes[index + 3] = (byte)((value & 0xFF000000) >> 24);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetInt32(this IList<byte> bytes, int index, int value, Endian endian)
    {
        if (endian == Endian.Little)
        {
            bytes.SetInt32(index, value);
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