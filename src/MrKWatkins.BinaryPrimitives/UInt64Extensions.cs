using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for reading and writing <see cref="ulong" /> values from byte containers.
/// </summary>
#pragma warning disable CS0675 // Bitwise-or operator used on a sign-extended operand
public static class UInt64Extensions
{
    private const int ConcreteTypePriority = 2;
    private const int ReadOnlyPriority = 1;

    /// <summary>
    /// Reads a little-endian <see cref="ulong" /> from a read-only list of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The read-only list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The <see cref="ulong" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetUInt64(this IReadOnlyList<byte> bytes, int index) =>
        bytes[index] | (ulong)bytes[index + 1] << 8 | (ulong)bytes[index + 2] << 16 | (ulong)bytes[index + 3] << 24 |
        (ulong)bytes[index + 4] << 32 | (ulong)bytes[index + 5] << 40 | (ulong)bytes[index + 6] << 48 | (ulong)bytes[index + 7] << 56;

    /// <summary>
    /// Reads a <see cref="ulong" /> from a read-only list of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The read-only list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="ulong" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ReadOnlyPriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetUInt64(this IReadOnlyList<byte> bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetUInt64(index)
            : bytes[index + 7] | (ulong)bytes[index + 6] << 8 | (ulong)bytes[index + 5] << 16 | (ulong)bytes[index + 4] << 24 |
              (ulong)bytes[index + 3] << 32 | (ulong)bytes[index + 2] << 40 | (ulong)bytes[index + 1] << 48 | (ulong)bytes[index] << 56;

    /// <summary>
    /// Reads a little-endian <see cref="ulong" /> from a <see cref="List{T}" /> of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The <see cref="ulong" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetUInt64(this List<byte> bytes, int index) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetUInt64();

    /// <summary>
    /// Reads a <see cref="ulong" /> from a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The <see cref="ulong" /> value.</returns>
    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetUInt64(this List<byte> bytes, int index, Endian endian) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetUInt64(endian);

}
#pragma warning restore CS0675 // Bitwise-or operator used on a sign-extended operand