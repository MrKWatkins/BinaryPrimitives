using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for reading primitive values from read-only spans of bytes.
/// </summary>
public static class ByteReadOnlySpanExtensions
{
    /// <param name="bytes">The read-only span of bytes.</param>
    extension(ReadOnlySpan<byte> bytes)
    {

        /// <summary>
        /// Reads a little-endian <see cref="short" /> from a read-only span of bytes.
        /// </summary>
        /// <returns>The <see cref="short" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short GetInt16() => MemoryMarshal.Read<short>(bytes);

        /// <summary>
        /// Reads a <see cref="short" /> from a read-only span of bytes using the specified endianness.
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
        /// Reads a little-endian <see cref="int" /> from a read-only span of bytes.
        /// </summary>
        /// <returns>The <see cref="int" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetInt32() => MemoryMarshal.Read<int>(bytes);

        /// <summary>
        /// Reads an <see cref="int" /> from a read-only span of bytes using the specified endianness.
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
        /// Reads a little-endian <see cref="long" /> from a read-only span of bytes.
        /// </summary>
        /// <returns>The <see cref="long" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long GetInt64() => MemoryMarshal.Read<long>(bytes);

        /// <summary>
        /// Reads a <see cref="long" /> from a read-only span of bytes using the specified endianness.
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
        /// Reads a little-endian unsigned 24-bit integer from a read-only span of bytes.
        /// </summary>
        /// <returns>The 24-bit value stored in an <see cref="int" />.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetUInt24() => bytes[0] | bytes[1] << 8 | bytes[2] << 16;

        /// <summary>
        /// Reads an unsigned 24-bit integer from a read-only span of bytes using the specified endianness.
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
        /// Reads a little-endian <see cref="uint" /> from a read-only span of bytes.
        /// </summary>
        /// <returns>The <see cref="uint" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetUInt32() => MemoryMarshal.Read<uint>(bytes);

        /// <summary>
        /// Reads a <see cref="uint" /> from a read-only span of bytes using the specified endianness.
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
        /// Reads a little-endian <see cref="ulong" /> from a read-only span of bytes.
        /// </summary>
        /// <returns>The <see cref="ulong" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetUInt64() => MemoryMarshal.Read<ulong>(bytes);

        /// <summary>
        /// Reads a <see cref="ulong" /> from a read-only span of bytes using the specified endianness.
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
        /// Reads a little-endian <see cref="ushort" /> from a read-only span of bytes.
        /// </summary>
        /// <returns>The <see cref="ushort" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetUInt16() => MemoryMarshal.Read<ushort>(bytes);

        /// <summary>
        /// Reads a <see cref="ushort" /> from a read-only span of bytes using the specified endianness.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="ushort" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetUInt16(Endian endian) =>
            endian == Endian.Little
                ? bytes.GetUInt16()
                : System.Buffers.Binary.BinaryPrimitives.ReadUInt16BigEndian(bytes);
    }
}