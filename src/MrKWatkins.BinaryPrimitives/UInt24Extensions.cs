using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for reading and writing unsigned 24-bit (3-byte) integer values from byte containers.
/// </summary>
public static class UInt24Extensions
{
    private const int ConcreteTypePriority = 2;

    [ExcludeFromCodeCoverage]
    static UInt24Extensions()
    {
        if (!BitConverter.IsLittleEndian)
        {
#pragma warning disable CA1065
            throw new InvalidOperationException("Only little endian processors are supported.");
#pragma warning restore CA1065
        }
    }

    /// <summary>
    /// Composes an unsigned 24-bit integer from three bytes.
    /// </summary>
    /// <param name="endian">The endianness to use.</param>
    /// <param name="msb">The most significant byte.</param>
    /// <param name="mid">The middle byte.</param>
    /// <param name="lsb">The least significant byte.</param>
    /// <returns>The composed 24-bit value stored in an <see cref="int" />.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToUInt24(this Endian endian, byte msb, byte mid, byte lsb)
    {
        if (endian == Endian.Little)
        {
            return msb | mid << 8 | lsb << 16;
        }

        return lsb | mid << 8 | msb << 16;
    }

    /// <summary>
    /// Reads a little-endian unsigned 24-bit integer from a <see cref="List{T}" /> of bytes at the specified index.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The 24-bit value stored in an <see cref="int" />.</returns>
    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetUInt24(this List<byte> bytes, int index) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetUInt24();

    /// <summary>
    /// Reads an unsigned 24-bit integer from a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The list of bytes.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The 24-bit value stored in an <see cref="int" />.</returns>
    [Pure]
    [OverloadResolutionPriority(ConcreteTypePriority)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetUInt24(this List<byte> bytes, int index, Endian endian) =>
        CollectionsMarshal.AsSpan(bytes)[index..].GetUInt24(endian);

}