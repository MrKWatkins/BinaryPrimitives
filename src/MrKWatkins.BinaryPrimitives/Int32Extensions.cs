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

}