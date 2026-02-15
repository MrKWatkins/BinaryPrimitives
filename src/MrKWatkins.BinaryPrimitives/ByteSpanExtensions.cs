using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for reading and writing primitive values from spans of bytes.
/// </summary>
public static class ByteSpanExtensions
{
    /// <param name="bytes">The span of bytes.</param>
    extension(Span<byte> bytes)
    {
        /// <summary>
        /// Reads a little-endian word from a span of bytes.
        /// </summary>
        /// <returns>The word value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetWord() => MemoryMarshal.Read<ushort>(bytes);

        /// <summary>
        /// Reads a word from a span of bytes using the specified endianness.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The word value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetWord(Endian endian) =>
            endian == Endian.Little
                ? bytes.GetWord()
                : System.Buffers.Binary.BinaryPrimitives.ReadUInt16BigEndian(bytes);

        /// <summary>
        /// Writes a little-endian word to a span of bytes.
        /// </summary>
        /// <param name="value">The word value to write.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetWord(ushort value) => MemoryMarshal.Write(bytes, value);

        /// <summary>
        /// Writes a word to a span of bytes using the specified endianness.
        /// </summary>
        /// <param name="value">The word value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetWord(ushort value, Endian endian)
        {
            if (endian == Endian.Little)
            {
                bytes.SetWord(value);
            }
            else
            {
                System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes, value);
            }
        }

        /// <summary>
        /// Reads a little-endian <see cref="short" /> from a span of bytes.
        /// </summary>
        /// <returns>The <see cref="short" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short GetInt16() => MemoryMarshal.Read<short>(bytes);

        /// <summary>
        /// Reads a <see cref="short" /> from a span of bytes using the specified endianness.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="short" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short GetInt16(Endian endian) =>
            endian == Endian.Little
                ? bytes.GetInt16()
                : System.Buffers.Binary.BinaryPrimitives.ReadInt16BigEndian(bytes);

        /// <summary>
        /// Writes a little-endian <see cref="short" /> to a span of bytes.
        /// </summary>
        /// <param name="value">The value to write.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt16(short value) => Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(bytes), value);

        /// <summary>
        /// Writes a <see cref="short" /> to a span of bytes using the specified endianness.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt16(short value, Endian endian)
        {
            if (endian == Endian.Little)
            {
                System.Buffers.Binary.BinaryPrimitives.WriteInt16LittleEndian(bytes, value);
            }
            else
            {
                System.Buffers.Binary.BinaryPrimitives.WriteInt16BigEndian(bytes, value);
            }
        }

        /// <summary>
        /// Reads a little-endian unsigned 24-bit integer from a span of bytes.
        /// </summary>
        /// <returns>The 24-bit value stored in an <see cref="int" />.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetUInt24() => bytes[0] | bytes[1] << 8 | bytes[2] << 16;

        /// <summary>
        /// Reads an unsigned 24-bit integer from a span of bytes using the specified endianness.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The 24-bit value stored in an <see cref="int" />.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetUInt24(Endian endian) =>
            endian == Endian.Little
                ? bytes.GetUInt24()
                : bytes[0] << 16 | bytes[1] << 8 | bytes[2];

        /// <summary>
        /// Writes a little-endian unsigned 24-bit integer to a span of bytes.
        /// </summary>
        /// <param name="value">The 24-bit value to write. Only the lower 24 bits are used.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt24(int value)
        {
            value &= 0xFFFFFF;
            bytes[0] = (byte)value;
            bytes[1] = (byte)(value >> 8);
            bytes[2] = (byte)(value >> 16);
        }

        /// <summary>
        /// Writes an unsigned 24-bit integer to a span of bytes using the specified endianness.
        /// </summary>
        /// <param name="value">The 24-bit value to write. Only the lower 24 bits are used.</param>
        /// <param name="endian">The endianness to use.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt24(int value, Endian endian)
        {
            if (endian == Endian.Little)
            {
                bytes.SetUInt24(value);
            }
            else
            {
                bytes[0] = (byte)(value >> 16);
                bytes[1] = (byte)(value >> 8);
                bytes[2] = (byte)value;
            }
        }

        /// <summary>
        /// Reads a little-endian <see cref="int" /> from a span of bytes.
        /// </summary>
        /// <returns>The <see cref="int" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetInt32() => MemoryMarshal.Read<int>(bytes);

        /// <summary>
        /// Reads an <see cref="int" /> from a span of bytes using the specified endianness.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="int" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetInt32(Endian endian) =>
            endian == Endian.Little
                ? bytes.GetInt32()
                : System.Buffers.Binary.BinaryPrimitives.ReadInt32BigEndian(bytes);

        /// <summary>
        /// Writes a little-endian <see cref="int" /> to a span of bytes.
        /// </summary>
        /// <param name="value">The value to write.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt32(int value) => Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(bytes), value);

        /// <summary>
        /// Writes an <see cref="int" /> to a span of bytes using the specified endianness.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt32(int value, Endian endian)
        {
            if (endian == Endian.Little)
            {
                System.Buffers.Binary.BinaryPrimitives.WriteInt32LittleEndian(bytes, value);
            }
            else
            {
                System.Buffers.Binary.BinaryPrimitives.WriteInt32BigEndian(bytes, value);
            }
        }

        /// <summary>
        /// Reads a little-endian <see cref="uint" /> from a span of bytes.
        /// </summary>
        /// <returns>The <see cref="uint" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetUInt32() => MemoryMarshal.Read<uint>(bytes);

        /// <summary>
        /// Reads a <see cref="uint" /> from a span of bytes using the specified endianness.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="uint" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetUInt32(Endian endian) =>
            endian == Endian.Little
                ? bytes.GetUInt32()
                : System.Buffers.Binary.BinaryPrimitives.ReadUInt32BigEndian(bytes);

        /// <summary>
        /// Writes a little-endian <see cref="uint" /> to a span of bytes.
        /// </summary>
        /// <param name="value">The value to write.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt32(uint value) => Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(bytes), value);

        /// <summary>
        /// Writes a <see cref="uint" /> to a span of bytes using the specified endianness.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt32(uint value, Endian endian)
        {
            if (endian == Endian.Little)
            {
                System.Buffers.Binary.BinaryPrimitives.WriteUInt32LittleEndian(bytes, value);
            }
            else
            {
                System.Buffers.Binary.BinaryPrimitives.WriteUInt32BigEndian(bytes, value);
            }
        }

        /// <summary>
        /// Reads a little-endian <see cref="long" /> from a span of bytes.
        /// </summary>
        /// <returns>The <see cref="long" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long GetInt64() => MemoryMarshal.Read<long>(bytes);

        /// <summary>
        /// Reads a <see cref="long" /> from a span of bytes using the specified endianness.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="long" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long GetInt64(Endian endian) =>
            endian == Endian.Little
                ? bytes.GetInt64()
                : System.Buffers.Binary.BinaryPrimitives.ReadInt64BigEndian(bytes);

        /// <summary>
        /// Writes a little-endian <see cref="long" /> to a span of bytes.
        /// </summary>
        /// <param name="value">The value to write.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt64(long value) => Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(bytes), value);

        /// <summary>
        /// Writes a <see cref="long" /> to a span of bytes using the specified endianness.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt64(long value, Endian endian)
        {
            if (endian == Endian.Little)
            {
                System.Buffers.Binary.BinaryPrimitives.WriteInt64LittleEndian(bytes, value);
            }
            else
            {
                System.Buffers.Binary.BinaryPrimitives.WriteInt64BigEndian(bytes, value);
            }
        }

        /// <summary>
        /// Reads a little-endian <see cref="ulong" /> from a span of bytes.
        /// </summary>
        /// <returns>The <see cref="ulong" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetUInt64() => MemoryMarshal.Read<ulong>(bytes);

        /// <summary>
        /// Reads a <see cref="ulong" /> from a span of bytes using the specified endianness.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="ulong" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetUInt64(Endian endian) =>
            endian == Endian.Little
                ? bytes.GetUInt64()
                : System.Buffers.Binary.BinaryPrimitives.ReadUInt64BigEndian(bytes);

        /// <summary>
        /// Writes a little-endian <see cref="ulong" /> to a span of bytes.
        /// </summary>
        /// <param name="value">The value to write.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt64(ulong value) => Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(bytes), value);

        /// <summary>
        /// Writes a <see cref="ulong" /> to a span of bytes using the specified endianness.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt64(ulong value, Endian endian)
        {
            if (endian == Endian.Little)
            {
                System.Buffers.Binary.BinaryPrimitives.WriteUInt64LittleEndian(bytes, value);
            }
            else
            {
                System.Buffers.Binary.BinaryPrimitives.WriteUInt64BigEndian(bytes, value);
            }
        }
    }
}