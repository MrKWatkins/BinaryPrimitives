using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for reading and writing <see cref="uint" /> values from byte containers.
/// </summary>
public static class UInt32Extensions
{
    private const int ConcreteTypePriority = 2;
    private const int ReadOnlyPriority = 1;

    /// <summary>
    /// Reads a little-endian <see cref="uint" /> from a read-only span of bytes.
    /// </summary>
    /// <param name="bytes">The read-only span of bytes.</param>
    /// <returns>The <see cref="uint" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this ReadOnlySpan<byte> bytes) => MemoryMarshal.Read<uint>(bytes);

    /// <summary>
    /// Reads a <see cref="uint" /> from a read-only span of bytes using the specified endianness.
    /// </summary>
    /// <param name="bytes">The read-only span of bytes.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="uint" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this ReadOnlySpan<byte> bytes, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetUInt32()
            : System.Buffers.Binary.BinaryPrimitives.ReadUInt32BigEndian(bytes);

    /// <summary>
    /// Reads a little-endian <see cref="uint" /> from a list of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The <see cref="uint" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this IList<byte> bytes, int index) => (uint)(bytes[index] | bytes[index + 1] << 8 | bytes[index + 2] << 16 | bytes[index + 3] << 24);

    /// <summary>
    /// Reads a <see cref="uint" /> from a list of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="uint" /> value.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this IList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetUInt32(index)
            : (uint)(bytes[index + 3] | bytes[index + 2] << 8 | bytes[index + 1] << 16 | bytes[index] << 24);

    /// <summary>
    /// Reads a little-endian <see cref="uint" /> from a read-only list of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The read-only list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The <see cref="uint" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this IReadOnlyList<byte> bytes, int index) => (uint)(bytes[index] | bytes[index + 1] << 8 | bytes[index + 2] << 16 | bytes[index + 3] << 24);

    /// <summary>
    /// Reads a <see cref="uint" /> from a read-only list of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The read-only list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="uint" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this IReadOnlyList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetUInt32(index)
            : (uint)(bytes[index + 3] | bytes[index + 2] << 8 | bytes[index + 1] << 16 | bytes[index] << 24);

    /// <summary>
    /// Reads a little-endian <see cref="uint" /> from a <see cref="List{T}" /> of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The <see cref="uint" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this List<byte> bytes, int index) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetUInt32();

    /// <summary>
    /// Reads a <see cref="uint" /> from a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="uint" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint GetUInt32(this List<byte> bytes, int index, Endian endian) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetUInt32(endian);

    /// <summary>
    /// Writes a little-endian <see cref="uint" /> to a list of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to write to.</param>
    /// <param name="value">The value to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetUInt32(this IList<byte> bytes, int index, uint value)
    {
        bytes[index] = (byte)(value & 0x000000FF);
        bytes[index + 1] = (byte)((value & 0x0000FF00) >> 8);
        bytes[index + 2] = (byte)((value & 0x00FF0000) >> 16);
        bytes[index + 3] = (byte)((value & 0xFF000000) >> 24);
    }

    /// <summary>
    /// Writes a <see cref="uint" /> to a list of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to write to.</param>
    /// <param name="value">The value to write.</param>
    /// <param name="endian">The endianness to use.</param>
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