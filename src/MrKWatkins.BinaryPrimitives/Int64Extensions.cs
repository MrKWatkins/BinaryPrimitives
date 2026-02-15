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

}
#pragma warning restore CS0675 // Bitwise-or operator used on a sign-extended operand