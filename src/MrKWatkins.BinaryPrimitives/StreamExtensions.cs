namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for reading and writing primitive values from a <see cref="Stream" />.
/// </summary>
public static class StreamExtensions
{
    /// <param name="stream">The stream to read from.</param>
    extension(Stream stream)
    {
        /// <summary>
        /// Reads all remaining bytes from the stream.
        /// </summary>
        /// <returns>A byte array containing all remaining bytes from the stream.</returns>
        [MustUseReturnValue]
        public byte[] ReadAllBytes()
        {
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        /// <summary>
        /// Reads exactly <paramref name="length" /> bytes from the stream, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>A byte array containing the bytes read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached before <paramref name="length" /> bytes could be read.</exception>
        [MustUseReturnValue]
        public byte[] ReadExactly(int length)
        {
            var result = new byte[length];
            stream.ReadExactly(result);
            return result;
        }

        /// <summary>
        /// Reads a single byte from the stream, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <returns>The byte read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        public byte ReadByteOrThrow()
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
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The word value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        [MustUseReturnValue]
        public ushort ReadWordOrThrow(Endian endian = Endian.Little)
        {
            var byte0 = stream.ReadByteOrThrow();
            var byte1 = stream.ReadByteOrThrow();

            return endian.ToWord(byte0, byte1);
        }

        /// <summary>
        /// Writes a word to the stream.
        /// </summary>
        /// <param name="value">The word value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        public void WriteWord(ushort value, Endian endian = Endian.Little)
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
}