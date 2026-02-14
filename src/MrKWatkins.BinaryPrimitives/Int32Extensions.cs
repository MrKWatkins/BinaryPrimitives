using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for reading and writing <see cref="int" /> values from byte containers.
/// </summary>
public static class Int32Extensions
{
    private const int ConcreteTypePriority = 2;
    private const int ReadOnlyPriority = 1;

    /// <summary>
    /// Reads a little-endian <see cref="int" /> from a byte array at the specified index.
    /// </summary>
    /// <param name="bytes">The byte array.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The <see cref="int" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this byte[] bytes, int index) => Unsafe.ReadUnaligned<int>(ref bytes[index]);

    /// <summary>
    /// Reads an <see cref="int" /> from a byte array at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The byte array.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="int" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this byte[] bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetInt32(index)
            : bytes.AsSpan(index).GetInt32(Endian.Big);

    /// <summary>
    /// Reads a little-endian <see cref="int" /> from a span of bytes.
    /// </summary>
    /// <param name="bytes">The span of bytes.</param>
    /// <returns>The <see cref="int" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this Span<byte> bytes) => MemoryMarshal.Read<int>(bytes);

    /// <summary>
    /// Reads an <see cref="int" /> from a span of bytes using the specified endianness.
    /// </summary>
    /// <param name="bytes">The span of bytes.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="int" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this Span<byte> bytes, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetInt32()
            : System.Buffers.Binary.BinaryPrimitives.ReadInt32BigEndian(bytes);

    /// <summary>
    /// Reads a little-endian <see cref="int" /> from a read-only span of bytes.
    /// </summary>
    /// <param name="bytes">The read-only span of bytes.</param>
    /// <returns>The <see cref="int" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this ReadOnlySpan<byte> bytes) => MemoryMarshal.Read<int>(bytes);

    /// <summary>
    /// Reads an <see cref="int" /> from a read-only span of bytes using the specified endianness.
    /// </summary>
    /// <param name="bytes">The read-only span of bytes.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="int" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this ReadOnlySpan<byte> bytes, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetInt32()
            : System.Buffers.Binary.BinaryPrimitives.ReadInt32BigEndian(bytes);

    /// <summary>
    /// Reads a little-endian <see cref="int" /> from a list of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The <see cref="int" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this IList<byte> bytes, int index) => bytes[index] | bytes[index + 1] << 8 | bytes[index + 2] << 16 | bytes[index + 3] << 24;

    /// <summary>
    /// Reads an <see cref="int" /> from a list of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="int" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this IList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetInt32(index)
            : bytes[index + 3] | bytes[index + 2] << 8 | bytes[index + 1] << 16 | bytes[index] << 24;

    /// <summary>
    /// Reads a little-endian <see cref="int" /> from a read-only list of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The read-only list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The <see cref="int" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this IReadOnlyList<byte> bytes, int index) => bytes[index] | bytes[index + 1] << 8 | bytes[index + 2] << 16 | bytes[index + 3] << 24;

    /// <summary>
    /// Reads an <see cref="int" /> from a read-only list of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The read-only list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="int" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this IReadOnlyList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetInt32(index)
            : bytes[index + 3] | bytes[index + 2] << 8 | bytes[index + 1] << 16 | bytes[index] << 24;

    /// <summary>
    /// Reads a little-endian <see cref="int" /> from a <see cref="List{T}" /> of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The <see cref="int" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this List<byte> bytes, int index) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetInt32();

    /// <summary>
    /// Reads an <see cref="int" /> from a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="int" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetInt32(this List<byte> bytes, int index, Endian endian) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetInt32(endian);

    /// <summary>
    /// Writes a little-endian <see cref="int" /> to a byte array at the specified index.
    /// </summary>
    /// <param name="bytes">The byte array.</param>
    /// <param name="index">The zero-based index to write to.</param>
    /// <param name="value">The value to write.</param>
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetInt32(this byte[] bytes, int index, int value) => bytes.AsSpan(index).SetInt32(value);

    /// <summary>
    /// Writes an <see cref="int" /> to a byte array at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The byte array.</param>
    /// <param name="index">The zero-based index to write to.</param>
    /// <param name="value">The value to write.</param>
    /// <param name="endian">The endianness to use.</param>
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetInt32(this byte[] bytes, int index, int value, Endian endian) => bytes.AsSpan(index).SetInt32(value, endian);

    /// <summary>
    /// Writes a little-endian <see cref="int" /> to a span of bytes.
    /// </summary>
    /// <param name="bytes">The span of bytes.</param>
    /// <param name="value">The value to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetInt32(this Span<byte> bytes, int value) => Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(bytes), value);

    /// <summary>
    /// Writes an <see cref="int" /> to a span of bytes using the specified endianness.
    /// </summary>
    /// <param name="bytes">The span of bytes.</param>
    /// <param name="value">The value to write.</param>
    /// <param name="endian">The endianness to use.</param>
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

    /// <summary>
    /// Writes a little-endian <see cref="int" /> to a list of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to write to.</param>
    /// <param name="value">The value to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetInt32(this IList<byte> bytes, int index, int value)
    {
        bytes[index] = (byte)(value & 0x000000FF);
        bytes[index + 1] = (byte)((value & 0x0000FF00) >> 8);
        bytes[index + 2] = (byte)((value & 0x00FF0000) >> 16);
        bytes[index + 3] = (byte)((value & 0xFF000000) >> 24);
    }

    /// <summary>
    /// Writes an <see cref="int" /> to a list of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to write to.</param>
    /// <param name="value">The value to write.</param>
    /// <param name="endian">The endianness to use.</param>
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
