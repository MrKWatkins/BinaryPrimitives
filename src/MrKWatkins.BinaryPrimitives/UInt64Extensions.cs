using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.BinaryPrimitives;

#pragma warning disable CS0675 // Bitwise-or operator used on a sign-extended operand
public static class UInt64Extensions
{
    private const int ConcreteTypePriority = 2;
    private const int ReadOnlyPriority = 1;

    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetUInt64(this byte[] bytes, int index) => Unsafe.ReadUnaligned<ulong>(ref bytes[index]);

    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetUInt64(this byte[] bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetUInt64(index)
            : bytes.AsSpan(index).GetUInt64(Endian.Big);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetUInt64(this Span<byte> bytes) => MemoryMarshal.Read<ulong>(bytes);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetUInt64(this Span<byte> bytes, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetUInt64()
            : System.Buffers.Binary.BinaryPrimitives.ReadUInt64BigEndian(bytes);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetUInt64(this ReadOnlySpan<byte> bytes) => MemoryMarshal.Read<ulong>(bytes);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetUInt64(this ReadOnlySpan<byte> bytes, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetUInt64()
            : System.Buffers.Binary.BinaryPrimitives.ReadUInt64BigEndian(bytes);

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetUInt64(this IList<byte> bytes, int index) =>
        bytes[index] | (ulong)bytes[index + 1] << 8 | (ulong)bytes[index + 2] << 16 | (ulong)bytes[index + 3] << 24 |
        (ulong)bytes[index + 4] << 32 | (ulong)bytes[index + 5] << 40 | (ulong)bytes[index + 6] << 48 | (ulong)bytes[index + 7] << 56;

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetUInt64(this IList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetUInt64(index)
            : bytes[index + 7] | (ulong)bytes[index + 6] << 8 | (ulong)bytes[index + 5] << 16 | (ulong)bytes[index + 4] << 24 |
              (ulong)bytes[index + 3] << 32 | (ulong)bytes[index + 2] << 40 | (ulong)bytes[index + 1] << 48 | (ulong)bytes[index] << 56;

    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetUInt64(this IReadOnlyList<byte> bytes, int index) =>
        bytes[index] | (ulong)bytes[index + 1] << 8 | (ulong)bytes[index + 2] << 16 | (ulong)bytes[index + 3] << 24 |
        (ulong)bytes[index + 4] << 32 | (ulong)bytes[index + 5] << 40 | (ulong)bytes[index + 6] << 48 | (ulong)bytes[index + 7] << 56;

    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetUInt64(this IReadOnlyList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetUInt64(index)
            : bytes[index + 7] | (ulong)bytes[index + 6] << 8 | (ulong)bytes[index + 5] << 16 | (ulong)bytes[index + 4] << 24 |
              (ulong)bytes[index + 3] << 32 | (ulong)bytes[index + 2] << 40 | (ulong)bytes[index + 1] << 48 | (ulong)bytes[index] << 56;

    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetUInt64(this List<byte> bytes, int index) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetUInt64();

    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetUInt64(this List<byte> bytes, int index, Endian endian) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetUInt64(endian);

    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetUInt64(this byte[] bytes, int index, ulong value) => bytes.AsSpan(index).SetUInt64(value);

    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetUInt64(this byte[] bytes, int index, ulong value, Endian endian) => bytes.AsSpan(index).SetUInt64(value, endian);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetUInt64(this Span<byte> bytes, ulong value) => Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(bytes), value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetUInt64(this Span<byte> bytes, ulong value, Endian endian)
    {
        if (endian == Endian.Little)
        {
            System.Buffers.Binary.BinaryPrimitives.WriteUInt64LittleEndian(bytes, value);
        }
        else
        {
            System.Buffers.Binary.BinaryPrimitives.WriteUInt64BigEndian(bytes, value);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetUInt64(this IList<byte> bytes, int index, ulong value)
    {
        bytes[index] = (byte)(value & 0x00000000000000FF);
        bytes[index + 1] = (byte)((value & 0x000000000000FF00) >> 8);
        bytes[index + 2] = (byte)((value & 0x0000000000FF0000) >> 16);
        bytes[index + 3] = (byte)((value & 0x00000000FF000000) >> 24);
        bytes[index + 4] = (byte)((value & 0x000000FF00000000) >> 32);
        bytes[index + 5] = (byte)((value & 0x0000FF0000000000) >> 40);
        bytes[index + 6] = (byte)((value & 0x00FF000000000000) >> 48);
        bytes[index + 7] = (byte)((value & 0xFF00000000000000) >> 56);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetUInt64(this IList<byte> bytes, int index, ulong value, Endian endian)
    {
        if (endian == Endian.Little)
        {
            bytes.SetUInt64(index, value);
        }
        else
        {
            bytes[index] = (byte)((value & 0xFF00000000000000) >> 56);
            bytes[index + 1] = (byte)((value & 0x00FF000000000000) >> 48);
            bytes[index + 2] = (byte)((value & 0x0000FF0000000000) >> 40);
            bytes[index + 3] = (byte)((value & 0x000000FF00000000) >> 32);
            bytes[index + 4] = (byte)((value & 0x00000000FF000000) >> 24);
            bytes[index + 5] = (byte)((value & 0x0000000000FF0000) >> 16);
            bytes[index + 6] = (byte)((value & 0x000000000000FF00) >> 8);
            bytes[index + 7] = (byte)(value & 0x00000000000000FF);
        }
    }
}
#pragma warning restore CS0675 // Bitwise-or operator used on a sign-extended operand