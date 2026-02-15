using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for reading and writing <see cref="short" /> values from byte containers.
/// </summary>
public static class Int16Extensions
{
    private const int ConcreteTypePriority = 2;
    private const int ReadOnlyPriority = 1;

    /// <summary>
    /// Reads a little-endian <see cref="short" /> from a read-only span of bytes.
    /// </summary>
    /// <param name="bytes">The read-only span of bytes.</param>
    /// <returns>The <see cref="short" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short GetInt16(this ReadOnlySpan<byte> bytes) => MemoryMarshal.Read<short>(bytes);

    /// <summary>
    /// Reads a <see cref="short" /> from a read-only span of bytes using the specified endianness.
    /// </summary>
    /// <param name="bytes">The read-only span of bytes.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="short" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short GetInt16(this ReadOnlySpan<byte> bytes, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetInt16()
            : System.Buffers.Binary.BinaryPrimitives.ReadInt16BigEndian(bytes);

    /// <summary>
    /// Reads a little-endian <see cref="short" /> from a list of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The <see cref="short" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short GetInt16(this IList<byte> bytes, int index) => (short)(bytes[index] | bytes[index + 1] << 8);

    /// <summary>
    /// Reads a <see cref="short" /> from a list of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="short" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short GetInt16(this IList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetInt16(index)
            : (short)(bytes[index + 1] | bytes[index] << 8);

    /// <summary>
    /// Reads a little-endian <see cref="short" /> from a read-only list of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The read-only list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The <see cref="short" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short GetInt16(this IReadOnlyList<byte> bytes, int index) => (short)(bytes[index] | bytes[index + 1] << 8);

    /// <summary>
    /// Reads a <see cref="short" /> from a read-only list of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The read-only list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="short" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short GetInt16(this IReadOnlyList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetInt16(index)
            : (short)(bytes[index + 1] | bytes[index] << 8);

    /// <summary>
    /// Reads a little-endian <see cref="short" /> from a <see cref="List{T}" /> of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The <see cref="short" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short GetInt16(this List<byte> bytes, int index) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetInt16();

    /// <summary>
    /// Reads a <see cref="short" /> from a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="short" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short GetInt16(this List<byte> bytes, int index, Endian endian) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetInt16(endian);

    /// <summary>
    /// Writes a little-endian <see cref="short" /> to a list of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to write to.</param>
    /// <param name="value">The value to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetInt16(this IList<byte> bytes, int index, short value)
    {
        bytes[index] = (byte)(value & 0x00FF);
        bytes[index + 1] = (byte)((value & 0xFF00) >> 8);
    }

    /// <summary>
    /// Writes a <see cref="short" /> to a list of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to write to.</param>
    /// <param name="value">The value to write.</param>
    /// <param name="endian">The endianness to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetInt16(this IList<byte> bytes, int index, short value, Endian endian)
    {
        if (endian == Endian.Little)
        {
            bytes.SetInt16(index, value);
        }
        else
        {
            bytes[index + 1] = (byte)(value & 0x00FF);
            bytes[index] = (byte)((value & 0xFF00) >> 8);
        }
    }
}