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
        /// Reads a little-endian <see cref="UInt24" /> from a span of bytes.
        /// </summary>
        /// <returns>The <see cref="UInt24" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt24 GetUInt24() => new((uint)(bytes[0] | bytes[1] << 8 | bytes[2] << 16));

        /// <summary>
        /// Reads a <see cref="UInt24" /> from a span of bytes using the specified endianness.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="UInt24" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt24 GetUInt24(Endian endian) =>
            endian == Endian.Little
                ? bytes.GetUInt24()
                : new((uint)(bytes[0] << 16 | bytes[1] << 8 | bytes[2]));

        /// <summary>
        /// Writes a little-endian <see cref="UInt24" /> to a span of bytes.
        /// </summary>
        /// <param name="value">The <see cref="UInt24" /> value to write.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt24(UInt24 value)
        {
            uint v = value;
            bytes[0] = (byte)v;
            bytes[1] = (byte)(v >> 8);
            bytes[2] = (byte)(v >> 16);
        }

        /// <summary>
        /// Writes a <see cref="UInt24" /> to a span of bytes using the specified endianness.
        /// </summary>
        /// <param name="value">The <see cref="UInt24" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt24(UInt24 value, Endian endian)
        {
            if (endian == Endian.Little)
            {
                bytes.SetUInt24(value);
            }
            else
            {
                uint v = value;
                bytes[0] = (byte)(v >> 16);
                bytes[1] = (byte)(v >> 8);
                bytes[2] = (byte)v;
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

        /// <summary>
        /// Reads a little-endian <see cref="ushort" /> from a span of bytes.
        /// </summary>
        /// <returns>The <see cref="ushort" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetUInt16() => MemoryMarshal.Read<ushort>(bytes);

        /// <summary>
        /// Reads a <see cref="ushort" /> from a span of bytes using the specified endianness.
        /// </summary>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="ushort" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetUInt16(Endian endian) =>
            endian == Endian.Little
                ? bytes.GetUInt16()
                : System.Buffers.Binary.BinaryPrimitives.ReadUInt16BigEndian(bytes);

        /// <summary>
        /// Writes a little-endian <see cref="ushort" /> to a span of bytes.
        /// </summary>
        /// <param name="value">The <see cref="ushort" /> value to write.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt16(ushort value) => MemoryMarshal.Write(bytes, value);

        /// <summary>
        /// Writes a <see cref="ushort" /> to a span of bytes using the specified endianness.
        /// </summary>
        /// <param name="value">The <see cref="ushort" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt16(ushort value, Endian endian)
        {
            if (endian == Endian.Little)
            {
                bytes.SetUInt16(value);
            }
            else
            {
                System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes, value);
            }
        }
    }
}