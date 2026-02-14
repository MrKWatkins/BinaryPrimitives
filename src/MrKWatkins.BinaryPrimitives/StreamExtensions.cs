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
        /// Reads a <see cref="short" /> from the stream, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="short" /> value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        [MustUseReturnValue]
        public short ReadInt16OrThrow(Endian endian = Endian.Little) => stream.ReadExactly(2).GetInt16(0, endian);

        /// <summary>
        /// Writes a <see cref="short" /> to the stream.
        /// </summary>
        /// <param name="value">The <see cref="short" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        public void WriteInt16(short value, Endian endian = Endian.Little)
        {
            var bytes = new byte[2];
            bytes.SetInt16(0, value, endian);
            stream.Write(bytes);
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

        /// <summary>
        /// Reads an unsigned 24-bit integer from the stream, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The 24-bit value stored in an <see cref="int" />.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        [MustUseReturnValue]
        public int ReadUInt24OrThrow(Endian endian = Endian.Little) => stream.ReadExactly(3).GetUInt24(0, endian);

        /// <summary>
        /// Writes an unsigned 24-bit integer to the stream.
        /// </summary>
        /// <param name="value">The 24-bit value to write. Only the lower 24 bits are used.</param>
        /// <param name="endian">The endianness to use.</param>
        public void WriteUInt24(int value, Endian endian = Endian.Little)
        {
            var bytes = new byte[3];
            bytes.SetUInt24(0, value, endian);
            stream.Write(bytes);
        }

        /// <summary>
        /// Reads an <see cref="int" /> from the stream, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="int" /> value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        [MustUseReturnValue]
        public int ReadInt32OrThrow(Endian endian = Endian.Little) => stream.ReadExactly(4).GetInt32(0, endian);

        /// <summary>
        /// Writes an <see cref="int" /> to the stream.
        /// </summary>
        /// <param name="value">The <see cref="int" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        public void WriteInt32(int value, Endian endian = Endian.Little)
        {
            var bytes = new byte[4];
            bytes.SetInt32(0, value, endian);
            stream.Write(bytes);
        }

        /// <summary>
        /// Reads a <see cref="uint" /> from the stream, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="uint" /> value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        [MustUseReturnValue]
        public uint ReadUInt32OrThrow(Endian endian = Endian.Little) => stream.ReadExactly(4).GetUInt32(0, endian);

        /// <summary>
        /// Writes a <see cref="uint" /> to the stream.
        /// </summary>
        /// <param name="value">The <see cref="uint" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        public void WriteUInt32(uint value, Endian endian = Endian.Little)
        {
            var bytes = new byte[4];
            bytes.SetUInt32(0, value, endian);
            stream.Write(bytes);
        }

        /// <summary>
        /// Reads a <see cref="long" /> from the stream, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="long" /> value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        [MustUseReturnValue]
        public long ReadInt64OrThrow(Endian endian = Endian.Little) => stream.ReadExactly(8).GetInt64(0, endian);

        /// <summary>
        /// Writes a <see cref="long" /> to the stream.
        /// </summary>
        /// <param name="value">The <see cref="long" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        public void WriteInt64(long value, Endian endian = Endian.Little)
        {
            var bytes = new byte[8];
            bytes.SetInt64(0, value, endian);
            stream.Write(bytes);
        }

        /// <summary>
        /// Reads a <see cref="ulong" /> from the stream, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="ulong" /> value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        [MustUseReturnValue]
        public ulong ReadUInt64OrThrow(Endian endian = Endian.Little) => stream.ReadExactly(8).GetUInt64(0, endian);

        /// <summary>
        /// Writes a <see cref="ulong" /> to the stream.
        /// </summary>
        /// <param name="value">The <see cref="ulong" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        public void WriteUInt64(ulong value, Endian endian = Endian.Little)
        {
            var bytes = new byte[8];
            bytes.SetUInt64(0, value, endian);
            stream.Write(bytes);
        }
    }
}