using System.Runtime.CompilerServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for reading and writing unsigned 24-bit (3-byte) integer values from byte containers.
/// </summary>
public static class UInt24Extensions
{
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
    /// Reads a little-endian unsigned 24-bit integer from a byte array at the specified index.
    /// </summary>
    /// <param name="bytes">The byte array.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <returns>The 24-bit value stored in an <see cref="int" />.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetUInt24(this byte[] bytes, int index) => Unsafe.ReadUnaligned<ushort>(ref bytes[index]) | bytes[index + 2] << 16;

    /// <summary>
    /// Reads an unsigned 24-bit integer from a byte array at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The byte array.</param>
    /// <param name="index">The zero-based index to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The 24-bit value stored in an <see cref="int" />.</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetUInt24(this byte[] bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetUInt24(index)
            : bytes[index] << 16 | bytes[index + 1] << 8 | bytes[index + 2];

    /// <summary>
    /// Writes a little-endian unsigned 24-bit integer to a byte array at the specified index.
    /// </summary>
    /// <param name="bytes">The byte array.</param>
    /// <param name="index">The zero-based index to write to.</param>
    /// <param name="value">The 24-bit value to write. Only the lower 24 bits are used.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetUInt24(this byte[] bytes, int index, int value)
    {
        value &= 0xFFFFFF;
        bytes[index] = (byte)value;
        bytes[index + 1] = (byte)(value >> 8);
        bytes[index + 2] = (byte)(value >> 16);
    }

    /// <summary>
    /// Writes an unsigned 24-bit integer to a byte array at the specified index using the specified endianness.
    /// </summary>
    /// <param name="bytes">The byte array.</param>
    /// <param name="index">The zero-based index to write to.</param>
    /// <param name="value">The 24-bit value to write. Only the lower 24 bits are used.</param>
    /// <param name="endian">The endianness to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetUInt24(this byte[] bytes, int index, int value, Endian endian)
    {
        if (endian == Endian.Little)
        {
            bytes.SetUInt24(index, value);
        }
        else
        {
            bytes[index] = (byte)(value >> 16);
            bytes[index + 1] = (byte)(value >> 8);
            bytes[index + 2] = (byte)value;
        }
    }
}