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
        [MustUseReturnValue]
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
            Span<byte> bytes = stackalloc byte[2];
            bytes.SetInt16(value, endian);
            stream.Write(bytes);
        }

        /// <summary>
        /// Reads a <see cref="ushort" /> from the stream, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="ushort" /> value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        [MustUseReturnValue]
        public ushort ReadUInt16OrThrow(Endian endian = Endian.Little) => stream.ReadExactly(2).GetUInt16(0, endian);

        /// <summary>
        /// Writes a <see cref="ushort" /> to the stream.
        /// </summary>
        /// <param name="value">The <see cref="ushort" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        public void WriteUInt16(ushort value, Endian endian = Endian.Little)
        {
            Span<byte> bytes = stackalloc byte[2];
            bytes.SetUInt16(value, endian);
            stream.Write(bytes);
        }

        /// <summary>
        /// Reads a <see cref="UInt24" /> from the stream, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="UInt24" /> value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        [MustUseReturnValue]
        public UInt24 ReadUInt24OrThrow(Endian endian = Endian.Little) => stream.ReadExactly(3).GetUInt24(0, endian);

        /// <summary>
        /// Writes a <see cref="UInt24" /> to the stream.
        /// </summary>
        /// <param name="value">The <see cref="UInt24" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        public void WriteUInt24(UInt24 value, Endian endian = Endian.Little)
        {
            Span<byte> bytes = stackalloc byte[3];
            bytes.SetUInt24(value, endian);
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
            Span<byte> bytes = stackalloc byte[4];
            bytes.SetInt32(value, endian);
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
            Span<byte> bytes = stackalloc byte[4];
            bytes.SetUInt32(value, endian);
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
            Span<byte> bytes = stackalloc byte[8];
            bytes.SetInt64(value, endian);
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
            Span<byte> bytes = stackalloc byte[8];
            bytes.SetUInt64(value, endian);
            stream.Write(bytes);
        }

        /// <summary>
        /// Reads all remaining bytes from the stream asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A byte array containing all remaining bytes from the stream.</returns>
        [MustUseReturnValue]
        public async ValueTask<byte[]> ReadAllBytesAsync(CancellationToken cancellationToken = default)
        {
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream, cancellationToken).ConfigureAwait(false);
            return memoryStream.ToArray();
        }

        /// <summary>
        /// Reads exactly <paramref name="length" /> bytes from the stream asynchronously, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <param name="length">The number of bytes to read.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A byte array containing the bytes read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached before <paramref name="length" /> bytes could be read.</exception>
        [MustUseReturnValue]
        public async ValueTask<byte[]> ReadExactlyAsync(int length, CancellationToken cancellationToken = default)
        {
            var result = new byte[length];
            await stream.ReadExactlyAsync(result, cancellationToken).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Reads a single byte from the stream asynchronously, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The byte read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        [MustUseReturnValue]
        public async ValueTask<byte> ReadByteOrThrowAsync(CancellationToken cancellationToken = default)
        {
            var buffer = new byte[1];
            await stream.ReadExactlyAsync(buffer, cancellationToken).ConfigureAwait(false);
            return buffer[0];
        }

        /// <summary>
        /// Reads a <see cref="short" /> from the stream asynchronously, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The <see cref="short" /> value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        [MustUseReturnValue]
        public async ValueTask<short> ReadInt16OrThrowAsync(Endian endian = Endian.Little, CancellationToken cancellationToken = default)
        {
            var buffer = await stream.ReadExactlyAsync(2, cancellationToken).ConfigureAwait(false);
            return buffer.GetInt16(0, endian);
        }

        /// <summary>
        /// Writes a <see cref="short" /> to the stream asynchronously.
        /// </summary>
        /// <param name="value">The <see cref="short" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        public async ValueTask WriteInt16Async(short value, Endian endian = Endian.Little, CancellationToken cancellationToken = default)
        {
            var bytes = new byte[2];
            bytes.SetInt16(0, value, endian);
            await stream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads a <see cref="ushort" /> from the stream asynchronously, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The <see cref="ushort" /> value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        [MustUseReturnValue]
        public async ValueTask<ushort> ReadUInt16OrThrowAsync(Endian endian = Endian.Little, CancellationToken cancellationToken = default)
        {
            var buffer = await stream.ReadExactlyAsync(2, cancellationToken).ConfigureAwait(false);
            return buffer.GetUInt16(0, endian);
        }

        /// <summary>
        /// Writes a <see cref="ushort" /> to the stream asynchronously.
        /// </summary>
        /// <param name="value">The <see cref="ushort" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        public async ValueTask WriteUInt16Async(ushort value, Endian endian = Endian.Little, CancellationToken cancellationToken = default)
        {
            var bytes = new byte[2];
            bytes.SetUInt16(0, value, endian);
            await stream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads a <see cref="UInt24" /> from the stream asynchronously, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The <see cref="UInt24" /> value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        [MustUseReturnValue]
        public async ValueTask<UInt24> ReadUInt24OrThrowAsync(Endian endian = Endian.Little, CancellationToken cancellationToken = default)
        {
            var buffer = await stream.ReadExactlyAsync(3, cancellationToken).ConfigureAwait(false);
            return buffer.GetUInt24(0, endian);
        }

        /// <summary>
        /// Writes a <see cref="UInt24" /> to the stream asynchronously.
        /// </summary>
        /// <param name="value">The <see cref="UInt24" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        public async ValueTask WriteUInt24Async(UInt24 value, Endian endian = Endian.Little, CancellationToken cancellationToken = default)
        {
            var bytes = new byte[3];
            bytes.SetUInt24(0, value, endian);
            await stream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads an <see cref="int" /> from the stream asynchronously, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The <see cref="int" /> value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        [MustUseReturnValue]
        public async ValueTask<int> ReadInt32OrThrowAsync(Endian endian = Endian.Little, CancellationToken cancellationToken = default)
        {
            var buffer = await stream.ReadExactlyAsync(4, cancellationToken).ConfigureAwait(false);
            return buffer.GetInt32(0, endian);
        }

        /// <summary>
        /// Writes an <see cref="int" /> to the stream asynchronously.
        /// </summary>
        /// <param name="value">The <see cref="int" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        public async ValueTask WriteInt32Async(int value, Endian endian = Endian.Little, CancellationToken cancellationToken = default)
        {
            var bytes = new byte[4];
            bytes.SetInt32(0, value, endian);
            await stream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads a <see cref="uint" /> from the stream asynchronously, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The <see cref="uint" /> value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        [MustUseReturnValue]
        public async ValueTask<uint> ReadUInt32OrThrowAsync(Endian endian = Endian.Little, CancellationToken cancellationToken = default)
        {
            var buffer = await stream.ReadExactlyAsync(4, cancellationToken).ConfigureAwait(false);
            return buffer.GetUInt32(0, endian);
        }

        /// <summary>
        /// Writes a <see cref="uint" /> to the stream asynchronously.
        /// </summary>
        /// <param name="value">The <see cref="uint" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        public async ValueTask WriteUInt32Async(uint value, Endian endian = Endian.Little, CancellationToken cancellationToken = default)
        {
            var bytes = new byte[4];
            bytes.SetUInt32(0, value, endian);
            await stream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads a <see cref="long" /> from the stream asynchronously, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The <see cref="long" /> value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        [MustUseReturnValue]
        public async ValueTask<long> ReadInt64OrThrowAsync(Endian endian = Endian.Little, CancellationToken cancellationToken = default)
        {
            var buffer = await stream.ReadExactlyAsync(8, cancellationToken).ConfigureAwait(false);
            return buffer.GetInt64(0, endian);
        }

        /// <summary>
        /// Writes a <see cref="long" /> to the stream asynchronously.
        /// </summary>
        /// <param name="value">The <see cref="long" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        public async ValueTask WriteInt64Async(long value, Endian endian = Endian.Little, CancellationToken cancellationToken = default)
        {
            var bytes = new byte[8];
            bytes.SetInt64(0, value, endian);
            await stream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Reads a <see cref="ulong" /> from the stream asynchronously, throwing <see cref="EndOfStreamException" /> if the end of the stream has been reached.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The <see cref="ulong" /> value read from the stream.</returns>
        /// <exception cref="EndOfStreamException">The end of the stream has been reached.</exception>
        [MustUseReturnValue]
        public async ValueTask<ulong> ReadUInt64OrThrowAsync(Endian endian = Endian.Little, CancellationToken cancellationToken = default)
        {
            var buffer = await stream.ReadExactlyAsync(8, cancellationToken).ConfigureAwait(false);
            return buffer.GetUInt64(0, endian);
        }

        /// <summary>
        /// Writes a <see cref="ulong" /> to the stream asynchronously.
        /// </summary>
        /// <param name="value">The <see cref="ulong" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        public async ValueTask WriteUInt64Async(ulong value, Endian endian = Endian.Little, CancellationToken cancellationToken = default)
        {
            var bytes = new byte[8];
            bytes.SetUInt64(0, value, endian);
            await stream.WriteAsync(bytes, cancellationToken).ConfigureAwait(false);
        }
    }
}