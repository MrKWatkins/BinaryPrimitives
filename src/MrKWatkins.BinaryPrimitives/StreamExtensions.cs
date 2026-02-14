namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for reading and writing primitive values from a <see cref="Stream" />.
/// </summary>
public static class StreamExtensions
{
    /// <summary>
    /// Reads a single byte from the stream, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
    /// </summary>
    /// <param name="stream">The stream to read from.</param>
    /// <returns>The byte read from the stream.</returns>
    /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
    public static byte ReadByteOrThrow(this Stream stream)
    {
        var @byte = stream.ReadByte();
        if (@byte == -1)
        {
            throw new EndOfStreamException();
        }

        return (byte)@byte;
    }

    /// <summary>
    /// Reads a word from the stream, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
    /// </summary>
    /// <param name="stream">The stream to read from.</param>
    /// <param name="endian">The endianness to use.</param>
    /// <returns>The word value read from the stream.</returns>
    /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
    [MustUseReturnValue]
    public static ushort ReadWordOrThrow(this Stream stream, Endian endian = Endian.Little)
    {
        var byte0 = stream.ReadByteOrThrow();
        var byte1 = stream.ReadByteOrThrow();

        return endian.ToWord(byte0, byte1);
    }

    /// <summary>
    /// Writes a word to the stream.
    /// </summary>
    /// <param name="stream">The stream to write to.</param>
    /// <param name="value">The word value to write.</param>
    /// <param name="endian">The endianness to use.</param>
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
