using System.Runtime.CompilerServices;

namespace MrKWatkins.BinaryPrimitives;

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

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetUInt24(this byte[] bytes, int index) => Unsafe.ReadUnaligned<ushort>(ref bytes[index]) | bytes[index + 2] << 16;

    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetUInt24(this byte[] bytes, int index, Endian endian) =>
        endian == Endian.Little
            ? bytes.GetUInt24(index)
            : bytes[index] << 16 | bytes[index + 1] << 8 | bytes[index + 2];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetUInt24(this byte[] bytes, int index, int value)
    {
        value &= 0xFFFFFF;
        bytes[index] = (byte)value;
        bytes[index + 1] = (byte)(value >> 8);
        bytes[index + 2] = (byte)(value >> 16);
    }

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