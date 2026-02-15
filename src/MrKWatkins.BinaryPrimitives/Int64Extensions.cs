using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for reading and writing <see cref="long" /> values from byte containers.
/// </summary>
#pragma warning disable CS0675 // Bitwise-or operator used on a sign-extended operand
public static class Int64Extensions
{
    private const int ConcreteTypePriority = 2;
    private const int ReadOnlyPriority = 1;

    /// <summary>
    /// Reads a little-endian <see cref="long" /> from a span of bytes.
    /// </summary>
    /// <param name="bytes">The span of bytes.</param>
    /// <returns>The <see cref="long" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long GetInt64(this Span<byte> bytes) => MemoryMarshal.Read<long>(bytes);

    /// <summary>
    /// Reads a <see cref="long" /> from a span of bytes using the specified endianness.
    /// </summary>
    /// <param name="bytes">The span of bytes.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="long" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long GetInt64(this Span<byte> bytes, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetInt64()
            : System.Buffers.Binary.BinaryPrimitives.ReadInt64BigEndian(bytes);

    /// <summary>
    /// Reads a little-endian <see cref="long" /> from a read-only span of bytes.
    /// </summary>
    /// <param name="bytes">The read-only span of bytes.</param>
    /// <returns>The <see cref="long" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long GetInt64(this ReadOnlySpan<byte> bytes) => MemoryMarshal.Read<long>(bytes);

    /// <summary>
    /// Reads a <see cref="long" /> from a read-only span of bytes using the specified endianness.
    /// </summary>
    /// <param name="bytes">The read-only span of bytes.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="long" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long GetInt64(this ReadOnlySpan<byte> bytes, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetInt64()
            : System.Buffers.Binary.BinaryPrimitives.ReadInt64BigEndian(bytes);

    /// <summary>
    /// Reads a little-endian <see cref="long" /> from a list of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The <see cref="long" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long GetInt64(this IList<byte> bytes, int index) =>
        bytes[index] | bytes[index + 1] << 8 | bytes[index + 2] << 16 | bytes[index + 3] << 24 |
        (long)bytes[index + 4] << 32 | (long)bytes[index + 5] << 40 | (long)bytes[index + 6] << 48 | (long)bytes[index + 7] << 56;

    /// <summary>
    /// Reads a <see cref="long" /> from a list of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="long" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long GetInt64(this IList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetInt64(index)
            : bytes[index + 7] | bytes[index + 6] << 8 | bytes[index + 5] << 16 | bytes[index + 4] << 24 |
              (long)bytes[index + 3] << 32 | (long)bytes[index + 2] << 40 | (long)bytes[index + 1] << 48 | (long)bytes[index] << 56;

    /// <summary>
    /// Reads a little-endian <see cref="long" /> from a read-only list of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The read-only list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The <see cref="long" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long GetInt64(this IReadOnlyList<byte> bytes, int index) =>
        bytes[index] | bytes[index + 1] << 8 | bytes[index + 2] << 16 | bytes[index + 3] << 24 |
        (long)bytes[index + 4] << 32 | (long)bytes[index + 5] << 40 | (long)bytes[index + 6] << 48 | (long)bytes[index + 7] << 56;

    /// <summary>
    /// Reads a <see cref="long" /> from a read-only list of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The read-only list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="long" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long GetInt64(this IReadOnlyList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetInt64(index)
            : bytes[index + 7] | bytes[index + 6] << 8 | bytes[index + 5] << 16 | bytes[index + 4] << 24 |
              (long)bytes[index + 3] << 32 | (long)bytes[index + 2] << 40 | (long)bytes[index + 1] << 48 | (long)bytes[index] << 56;

    /// <summary>
    /// Reads a little-endian <see cref="long" /> from a <see cref="List{T}" /> of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The <see cref="long" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long GetInt64(this List<byte> bytes, int index) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetInt64();

    /// <summary>
    /// Reads a <see cref="long" /> from a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="long" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long GetInt64(this List<byte> bytes, int index, Endian endian) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetInt64(endian);

    /// <summary>
    /// Writes a little-endian <see cref="long" /> to a span of bytes.
    /// </summary>
    /// <param name="bytes">The span of bytes.</param>
    /// <param name="value">The value to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetInt64(this Span<byte> bytes, long value) => Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(bytes), value);

    /// <summary>
    /// Writes a <see cref="long" /> to a span of bytes using the specified endianness.
    /// </summary>
    /// <param name="bytes">The span of bytes.</param>
    /// <param name="value">The value to write.</param>
    /// <param name="endian">The endianness to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetInt64(this Span<byte> bytes, long value, Endian endian)
    {
        if (endian == Endian.Little)
        {
            System.Buffers.Binary.BinaryPrimitives.WriteInt64LittleEndian(bytes, value);
        }
        else
        {
            System.Buffers.Binary.BinaryPrimitives.WriteInt64BigEndian(bytes, value);
        }
    }

    /// <summary>
    /// Writes a little-endian <see cref="long" /> to a list of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to write to.</param>
    /// <param name="value">The value to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetInt64(this IList<byte> bytes, int index, long value)
    {
        bytes[index] = (byte)(value & 0x00000000000000FF);
        bytes[index + 1] = (byte)((value & 0x000000000000FF00) >> 8);
        bytes[index + 2] = (byte)((value & 0x0000000000FF0000) >> 16);
        bytes[index + 3] = (byte)((value & 0x00000000FF000000) >> 24);
        bytes[index + 4] = (byte)((value & 0x000000FF00000000) >> 32);
        bytes[index + 5] = (byte)((value & 0x0000FF0000000000) >> 40);
        bytes[index + 6] = (byte)((value & 0x00FF000000000000) >> 48);
        bytes[index + 7] = (byte)(((ulong)value & 0xFF00000000000000) >> 56);
    }

    /// <summary>
    /// Writes a <see cref="long" /> to a list of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to write to.</param>
    /// <param name="value">The value to write.</param>
    /// <param name="endian">The endianness to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetInt64(this IList<byte> bytes, int index, long value, Endian endian)
    {
        if (endian == Endian.Little)
        {
            bytes.SetInt64(index, value);
        }
        else
        {
            bytes[index] = (byte)(((ulong)value & 0xFF00000000000000) >> 56);
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