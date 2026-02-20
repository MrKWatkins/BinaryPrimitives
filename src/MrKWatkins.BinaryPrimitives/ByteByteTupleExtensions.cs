using System.Runtime.CompilerServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for tuples of <see cref="byte" />.
/// </summary>
public static class ByteByteTupleExtensions
{
    /// <summary>
    /// Composes a <see cref="ushort" /> from a tuple of most and least significant bytes.
    /// </summary>
    /// <param name="bytes">A tuple of the most significant byte and least significant byte.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The composed <see cref="ushort" />.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort ToUInt16(this (byte Msb, byte Lsb) bytes, Endian endian = Endian.Little) => endian.ToUInt16(bytes.Msb, bytes.Lsb);
}