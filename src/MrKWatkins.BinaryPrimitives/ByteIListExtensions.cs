using System.Runtime.CompilerServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for reading and writing primitive values from lists of bytes.
/// </summary>
public static class ByteIListExtensions
{
    /// <param name="bytes">The list of bytes.</param>
    extension(IList<byte> bytes)
    {

        /// <summary>
        /// Reads a little-endian <see cref="short" /> from a list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="short" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short GetInt16(int index) => (short)(bytes[index] | bytes[index + 1] << 8);

        /// <summary>
        /// Reads a <see cref="short" /> from a list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="short" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short GetInt16(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetInt16(index)
                : (short)(bytes[index + 1] | bytes[index] << 8);

        /// <summary>
        /// Writes a little-endian <see cref="short" /> to a list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt16(int index, short value)
        {
            bytes[index] = (byte)(value & 0x00FF);
            bytes[index + 1] = (byte)((value & 0xFF00) >> 8);
        }

        /// <summary>
        /// Writes a <see cref="short" /> to a list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt16(int index, short value, Endian endian)
        {
            if (endian == Endian.Little)
            {
                bytes.SetInt16(index, value);
            }
            else
            {
                bytes[index + 1] = (byte)(value & 0x00FF);
                bytes[index] = (byte)((value & 0xFF00) >> 8);
            }
        }


        /// <summary>
        /// Reads a little-endian <see cref="int" /> from a list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="int" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetInt32(int index) => bytes[index] | bytes[index + 1] << 8 | bytes[index + 2] << 16 | bytes[index + 3] << 24;

        /// <summary>
        /// Reads an <see cref="int" /> from a list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="int" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetInt32(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetInt32(index)
                : bytes[index + 3] | bytes[index + 2] << 8 | bytes[index + 1] << 16 | bytes[index] << 24;

        /// <summary>
        /// Writes a little-endian <see cref="int" /> to a list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt32(int index, int value)
        {
            bytes[index] = (byte)(value & 0x000000FF);
            bytes[index + 1] = (byte)((value & 0x0000FF00) >> 8);
            bytes[index + 2] = (byte)((value & 0x00FF0000) >> 16);
            bytes[index + 3] = (byte)((value & 0xFF000000) >> 24);
        }

        /// <summary>
        /// Writes an <see cref="int" /> to a list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt32(int index, int value, Endian endian)
        {
            if (endian == Endian.Little)
            {
                bytes.SetInt32(index, value);
            }
            else
            {
                bytes[index + 3] = (byte)(value & 0x000000FF);
                bytes[index + 2] = (byte)((value & 0x0000FF00) >> 8);
                bytes[index + 1] = (byte)((value & 0x00FF0000) >> 16);
                bytes[index] = (byte)((value & 0xFF000000) >> 24);
            }
        }

#pragma warning disable CS0675 // Bitwise-or operator used on a sign-extended operand




        /// <summary>
        /// Reads a little-endian <see cref="long" /> from a list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="long" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long GetInt64(int index) =>
            bytes[index] | bytes[index + 1] << 8 | bytes[index + 2] << 16 | bytes[index + 3] << 24 |
            (long)bytes[index + 4] << 32 | (long)bytes[index + 5] << 40 | (long)bytes[index + 6] << 48 | (long)bytes[index + 7] << 56;

        /// <summary>
        /// Reads a <see cref="long" /> from a list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="long" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long GetInt64(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetInt64(index)
                : bytes[index + 7] | bytes[index + 6] << 8 | bytes[index + 5] << 16 | bytes[index + 4] << 24 |
                  (long)bytes[index + 3] << 32 | (long)bytes[index + 2] << 40 | (long)bytes[index + 1] << 48 | (long)bytes[index] << 56;

        /// <summary>
        /// Writes a little-endian <see cref="long" /> to a list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt64(int index, long value)
        {
            bytes[index] = (byte)(value & 0x00000000000000FF);
            bytes[index + 1] = (byte)((value & 0x000000000000FF00) >> 8);
            bytes[index + 2] = (byte)((value & 0x0000000000FF0000) >> 16);
            bytes[index + 3] = (byte)((value & 0x00000000FF000000) >> 24);
            bytes[index + 4] = (byte)((value & 0x000000FF00000000) >> 32);
            bytes[index + 5] = (byte)((value & 0x0000FF0000000000) >> 40);
            bytes[index + 6] = (byte)((value & 0x00FF000000000000) >> 48);
            bytes[index + 7] = (byte)(((ulong)value & 0xFF00000000000000) >> 56);
        }

        /// <summary>
        /// Writes a <see cref="long" /> to a list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt64(int index, long value, Endian endian)
        {
            if (endian == Endian.Little)
            {
                bytes.SetInt64(index, value);
            }
            else
            {
                bytes[index] = (byte)(((ulong)value & 0xFF00000000000000) >> 56);
                bytes[index + 1] = (byte)((value & 0x00FF000000000000) >> 48);
                bytes[index + 2] = (byte)((value & 0x0000FF0000000000) >> 40);
                bytes[index + 3] = (byte)((value & 0x000000FF00000000) >> 32);
                bytes[index + 4] = (byte)((value & 0x00000000FF000000) >> 24);
                bytes[index + 5] = (byte)((value & 0x0000000000FF0000) >> 16);
                bytes[index + 6] = (byte)((value & 0x000000000000FF00) >> 8);
                bytes[index + 7] = (byte)(value & 0x00000000000000FF);
            }
        }


        /// <summary>
        /// Reads a little-endian <see cref="UInt24" /> from a list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="UInt24" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt24 GetUInt24(int index) => new((uint)(bytes[index] | bytes[index + 1] << 8 | bytes[index + 2] << 16));

        /// <summary>
        /// Reads a <see cref="UInt24" /> from a list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="UInt24" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt24 GetUInt24(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetUInt24(index)
                : new((uint)(bytes[index] << 16 | bytes[index + 1] << 8 | bytes[index + 2]));

        /// <summary>
        /// Writes a little-endian <see cref="UInt24" /> to a list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The <see cref="UInt24" /> value to write.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt24(int index, UInt24 value)
        {
            uint v = value;
            bytes[index] = (byte)v;
            bytes[index + 1] = (byte)(v >> 8);
            bytes[index + 2] = (byte)(v >> 16);
        }

        /// <summary>
        /// Writes a <see cref="UInt24" /> to a list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The <see cref="UInt24" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt24(int index, UInt24 value, Endian endian)
        {
            if (endian == Endian.Little)
            {
                bytes.SetUInt24(index, value);
            }
            else
            {
                uint v = value;
                bytes[index] = (byte)(v >> 16);
                bytes[index + 1] = (byte)(v >> 8);
                bytes[index + 2] = (byte)v;
            }
        }


        /// <summary>
        /// Reads a little-endian <see cref="uint" /> from a list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="uint" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetUInt32(int index) => (uint)(bytes[index] | bytes[index + 1] << 8 | bytes[index + 2] << 16 | bytes[index + 3] << 24);

        /// <summary>
        /// Reads a <see cref="uint" /> from a list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="uint" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetUInt32(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetUInt32(index)
                : (uint)(bytes[index + 3] | bytes[index + 2] << 8 | bytes[index + 1] << 16 | bytes[index] << 24);

        /// <summary>
        /// Writes a little-endian <see cref="uint" /> to a list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt32(int index, uint value)
        {
            bytes[index] = (byte)(value & 0x000000FF);
            bytes[index + 1] = (byte)((value & 0x0000FF00) >> 8);
            bytes[index + 2] = (byte)((value & 0x00FF0000) >> 16);
            bytes[index + 3] = (byte)((value & 0xFF000000) >> 24);
        }

        /// <summary>
        /// Writes a <see cref="uint" /> to a list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt32(int index, uint value, Endian endian)
        {
            if (endian == Endian.Little)
            {
                bytes.SetUInt32(index, value);
            }
            else
            {
                bytes[index + 3] = (byte)(value & 0x000000FF);
                bytes[index + 2] = (byte)((value & 0x0000FF00) >> 8);
                bytes[index + 1] = (byte)((value & 0x00FF0000) >> 16);
                bytes[index] = (byte)((value & 0xFF000000) >> 24);
            }
        }


        /// <summary>
        /// Reads a little-endian <see cref="ulong" /> from a list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="ulong" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetUInt64(int index) =>
            bytes[index] | (ulong)bytes[index + 1] << 8 | (ulong)bytes[index + 2] << 16 | (ulong)bytes[index + 3] << 24 |
            (ulong)bytes[index + 4] << 32 | (ulong)bytes[index + 5] << 40 | (ulong)bytes[index + 6] << 48 | (ulong)bytes[index + 7] << 56;

        /// <summary>
        /// Reads a <see cref="ulong" /> from a list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="ulong" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetUInt64(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetUInt64(index)
                : bytes[index + 7] | (ulong)bytes[index + 6] << 8 | (ulong)bytes[index + 5] << 16 | (ulong)bytes[index + 4] << 24 |
                  (ulong)bytes[index + 3] << 32 | (ulong)bytes[index + 2] << 40 | (ulong)bytes[index + 1] << 48 | (ulong)bytes[index] << 56;

        /// <summary>
        /// Writes a little-endian <see cref="ulong" /> to a list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt64(int index, ulong value)
        {
            bytes[index] = (byte)(value & 0x00000000000000FF);
            bytes[index + 1] = (byte)((value & 0x000000000000FF00) >> 8);
            bytes[index + 2] = (byte)((value & 0x0000000000FF0000) >> 16);
            bytes[index + 3] = (byte)((value & 0x00000000FF000000) >> 24);
            bytes[index + 4] = (byte)((value & 0x000000FF00000000) >> 32);
            bytes[index + 5] = (byte)((value & 0x0000FF0000000000) >> 40);
            bytes[index + 6] = (byte)((value & 0x00FF000000000000) >> 48);
            bytes[index + 7] = (byte)((value & 0xFF00000000000000) >> 56);
        }

        /// <summary>
        /// Writes a <see cref="ulong" /> to a list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt64(int index, ulong value, Endian endian)
        {
            if (endian == Endian.Little)
            {
                bytes.SetUInt64(index, value);
            }
            else
            {
                bytes[index] = (byte)((value & 0xFF00000000000000) >> 56);
                bytes[index + 1] = (byte)((value & 0x00FF000000000000) >> 48);
                bytes[index + 2] = (byte)((value & 0x0000FF0000000000) >> 40);
                bytes[index + 3] = (byte)((value & 0x000000FF00000000) >> 32);
                bytes[index + 4] = (byte)((value & 0x00000000FF000000) >> 24);
                bytes[index + 5] = (byte)((value & 0x0000000000FF0000) >> 16);
                bytes[index + 6] = (byte)((value & 0x000000000000FF00) >> 8);
                bytes[index + 7] = (byte)(value & 0x00000000000000FF);
            }
        }


#pragma warning restore CS0675

        /// <summary>
        /// Reads a little-endian <see cref="ushort" /> from a list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="ushort" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetUInt16(int index) => (ushort)(bytes[index] | bytes[index + 1] << 8);

        /// <summary>
        /// Reads a <see cref="ushort" /> from a list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="ushort" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetUInt16(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetUInt16(index)
                : (ushort)(bytes[index + 1] | bytes[index] << 8);

        /// <summary>
        /// Writes a little-endian <see cref="ushort" /> to a list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The <see cref="ushort" /> value to write.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt16(int index, ushort value)
        {
            var (msb, lsb) = value.ToBytes();
            bytes[index] = lsb;
            bytes[index + 1] = msb;
        }

        /// <summary>
        /// Writes a <see cref="ushort" /> to a list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The <see cref="ushort" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt16(int index, ushort value, Endian endian)
        {
            var (msb, lsb) = value.ToBytes();
            if (endian == Endian.Little)
            {
                bytes.SetUInt16(index, value);
            }
            else
            {
                bytes[index] = msb;
                bytes[index + 1] = lsb;
            }
        }
    }
}