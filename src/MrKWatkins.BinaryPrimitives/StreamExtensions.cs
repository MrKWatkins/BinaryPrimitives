namespace MrKWatkins.BinaryPrimitives;

public static class StreamExtensions
{
    public static byte ReadByteOrThrow(this Stream stream)
    {
        var @byte = stream.ReadByte();
        if (@byte == -1)
        {
            throw new EndOfStreamException();
        }

        return (byte)@byte;
    }

    [MustUseReturnValue]
    public static ushort ReadWordOrThrow(this Stream stream, Endian endian = Endian.Little)
    {
        var byte0 = stream.ReadByteOrThrow();
        var byte1 = stream.ReadByteOrThrow();

        return endian.ToWord(byte0, byte1);
    }

    public static void WriteWord(this Stream stream, ushort value, Endian endian = Endian.Little)
    {
        var (msb, lsb) = value.ToBytes();

        if (endian == Endian.Little)
        {
            stream.WriteByte(lsb);
            stream.WriteByte(msb);
        }
        else
        {
            stream.WriteByte(msb);
            stream.WriteByte(lsb);
        }
    }
}