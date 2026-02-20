namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for <see cref="ICollection{T}" /> of <see cref="byte" />.
/// </summary>
public static class ByteICollectionExtensions
{
    /// <param name="bytes">The byte collection to add to.</param>
    extension(ICollection<byte> bytes)
    {
        /// <summary>
        /// Adds a <see cref="ushort" /> to a byte collection.
        /// </summary>
        /// <param name="value">The <see cref="ushort" /> value to add.</param>
        /// <param name="endian">The endianness to use.</param>
        public void AddUInt16(ushort value, Endian endian = Endian.Little)
        {
            var (msb, lsb) = value.ToBytes();
            if (endian == Endian.Little)
            {
                bytes.Add(lsb);
                bytes.Add(msb);
            }
            else
            {
                bytes.Add(msb);
                bytes.Add(lsb);
            }
        }

        /// <summary>
        /// Adds a <see cref="short" /> to a byte collection.
        /// </summary>
        /// <param name="value">The <see cref="short" /> value to add.</param>
        /// <param name="endian">The endianness to use.</param>
        public void AddInt16(short value, Endian endian = Endian.Little)
        {
            Span<byte> buffer = stackalloc byte[2];
            buffer.SetInt16(value, endian);
            bytes.Add(buffer[0]);
            bytes.Add(buffer[1]);
        }

        /// <summary>
        /// Adds an unsigned 24-bit integer to a byte collection.
        /// </summary>
        /// <param name="value">The 24-bit value to add. Only the lower 24 bits are used.</param>
        /// <param name="endian">The endianness to use.</param>
        public void AddUInt24(int value, Endian endian = Endian.Little)
        {
            Span<byte> buffer = stackalloc byte[3];
            buffer.SetUInt24(value, endian);
            bytes.Add(buffer[0]);
            bytes.Add(buffer[1]);
            bytes.Add(buffer[2]);
        }

        /// <summary>
        /// Adds an <see cref="int" /> to a byte collection.
        /// </summary>
        /// <param name="value">The <see cref="int" /> value to add.</param>
        /// <param name="endian">The endianness to use.</param>
        public void AddInt32(int value, Endian endian = Endian.Little)
        {
            Span<byte> buffer = stackalloc byte[4];
            buffer.SetInt32(value, endian);
            bytes.Add(buffer[0]);
            bytes.Add(buffer[1]);
            bytes.Add(buffer[2]);
            bytes.Add(buffer[3]);
        }

        /// <summary>
        /// Adds a <see cref="uint" /> to a byte collection.
        /// </summary>
        /// <param name="value">The <see cref="uint" /> value to add.</param>
        /// <param name="endian">The endianness to use.</param>
        public void AddUInt32(uint value, Endian endian = Endian.Little)
        {
            Span<byte> buffer = stackalloc byte[4];
            buffer.SetUInt32(value, endian);
            bytes.Add(buffer[0]);
            bytes.Add(buffer[1]);
            bytes.Add(buffer[2]);
            bytes.Add(buffer[3]);
        }

        /// <summary>
        /// Adds a <see cref="long" /> to a byte collection.
        /// </summary>
        /// <param name="value">The <see cref="long" /> value to add.</param>
        /// <param name="endian">The endianness to use.</param>
        public void AddInt64(long value, Endian endian = Endian.Little)
        {
            Span<byte> buffer = stackalloc byte[8];
            buffer.SetInt64(value, endian);
            bytes.Add(buffer[0]);
            bytes.Add(buffer[1]);
            bytes.Add(buffer[2]);
            bytes.Add(buffer[3]);
            bytes.Add(buffer[4]);
            bytes.Add(buffer[5]);
            bytes.Add(buffer[6]);
            bytes.Add(buffer[7]);
        }

        /// <summary>
        /// Adds a <see cref="ulong" /> to a byte collection.
        /// </summary>
        /// <param name="value">The <see cref="ulong" /> value to add.</param>
        /// <param name="endian">The endianness to use.</param>
        public void AddUInt64(ulong value, Endian endian = Endian.Little)
        {
            Span<byte> buffer = stackalloc byte[8];
            buffer.SetUInt64(value, endian);
            bytes.Add(buffer[0]);
            bytes.Add(buffer[1]);
            bytes.Add(buffer[2]);
            bytes.Add(buffer[3]);
            bytes.Add(buffer[4]);
            bytes.Add(buffer[5]);
            bytes.Add(buffer[6]);
            bytes.Add(buffer[7]);
        }
    }
}