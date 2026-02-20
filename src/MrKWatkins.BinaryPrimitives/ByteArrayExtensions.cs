using System.Runtime.CompilerServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for reading and writing primitive values from byte arrays.
/// </summary>
public static class ByteArrayExtensions
{
    private const int ConcreteTypePriority = 2;

    /// <param name="bytes">The byte array.</param>
    extension(byte[] bytes)
    {

        /// <summary>
        /// Reads a little-endian <see cref="short" /> from a byte array at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="short" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short GetInt16(int index) => Unsafe.ReadUnaligned<short>(ref bytes[index]);

        /// <summary>
        /// Reads a <see cref="short" /> from a byte array at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="short" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short GetInt16(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetInt16(index)
                : bytes.AsSpan(index).GetInt16(Endian.Big);

        /// <summary>
        /// Writes a little-endian <see cref="short" /> to a byte array at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt16(int index, short value) => bytes.AsSpan(index).SetInt16(value);

        /// <summary>
        /// Writes a <see cref="short" /> to a byte array at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt16(int index, short value, Endian endian) => bytes.AsSpan(index).SetInt16(value, endian);


        /// <summary>
        /// Reads a little-endian <see cref="int" /> from a byte array at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="int" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetInt32(int index) => Unsafe.ReadUnaligned<int>(ref bytes[index]);

        /// <summary>
        /// Reads an <see cref="int" /> from a byte array at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="int" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetInt32(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetInt32(index)
                : bytes.AsSpan(index).GetInt32(Endian.Big);

        /// <summary>
        /// Writes a little-endian <see cref="int" /> to a byte array at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt32(int index, int value) => bytes.AsSpan(index).SetInt32(value);

        /// <summary>
        /// Writes an <see cref="int" /> to a byte array at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt32(int index, int value, Endian endian) => bytes.AsSpan(index).SetInt32(value, endian);


        /// <summary>
        /// Reads a little-endian <see cref="long" /> from a byte array at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="long" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long GetInt64(int index) => Unsafe.ReadUnaligned<long>(ref bytes[index]);

        /// <summary>
        /// Reads a <see cref="long" /> from a byte array at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="long" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long GetInt64(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetInt64(index)
                : bytes.AsSpan(index).GetInt64(Endian.Big);

        /// <summary>
        /// Writes a little-endian <see cref="long" /> to a byte array at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt64(int index, long value) => bytes.AsSpan(index).SetInt64(value);

        /// <summary>
        /// Writes a <see cref="long" /> to a byte array at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt64(int index, long value, Endian endian) => bytes.AsSpan(index).SetInt64(value, endian);


        /// <summary>
        /// Reads a little-endian <see cref="UInt24" /> from a byte array at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="UInt24" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt24 GetUInt24(int index) => new((uint)(Unsafe.ReadUnaligned<ushort>(ref bytes[index]) | bytes[index + 2] << 16));

        /// <summary>
        /// Reads a <see cref="UInt24" /> from a byte array at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="UInt24" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt24 GetUInt24(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetUInt24(index)
                : new((uint)(bytes[index] << 16 | bytes[index + 1] << 8 | bytes[index + 2]));

        /// <summary>
        /// Writes a little-endian <see cref="UInt24" /> to a byte array at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The <see cref="UInt24" /> value to write.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt24(int index, UInt24 value)
        {
            uint v = value;
            bytes[index] = (byte)v;
            bytes[index + 1] = (byte)(v >> 8);
            bytes[index + 2] = (byte)(v >> 16);
        }

        /// <summary>
        /// Writes a <see cref="UInt24" /> to a byte array at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The <see cref="UInt24" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
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
        /// Reads a little-endian <see cref="uint" /> from a byte array at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="uint" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetUInt32(int index) => Unsafe.ReadUnaligned<uint>(ref bytes[index]);

        /// <summary>
        /// Reads a <see cref="uint" /> from a byte array at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="uint" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetUInt32(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetUInt32(index)
                : bytes.AsSpan(index).GetUInt32(Endian.Big);

        /// <summary>
        /// Writes a little-endian <see cref="uint" /> to a byte array at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt32(int index, uint value) => bytes.AsSpan(index).SetUInt32(value);

        /// <summary>
        /// Writes a <see cref="uint" /> to a byte array at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt32(int index, uint value, Endian endian) => bytes.AsSpan(index).SetUInt32(value, endian);


        /// <summary>
        /// Reads a little-endian <see cref="ulong" /> from a byte array at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="ulong" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetUInt64(int index) => Unsafe.ReadUnaligned<ulong>(ref bytes[index]);

        /// <summary>
        /// Reads a <see cref="ulong" /> from a byte array at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="ulong" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetUInt64(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetUInt64(index)
                : bytes.AsSpan(index).GetUInt64(Endian.Big);

        /// <summary>
        /// Writes a little-endian <see cref="ulong" /> to a byte array at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt64(int index, ulong value) => bytes.AsSpan(index).SetUInt64(value);

        /// <summary>
        /// Writes a <see cref="ulong" /> to a byte array at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt64(int index, ulong value, Endian endian) => bytes.AsSpan(index).SetUInt64(value, endian);

        /// <summary>
        /// Reads a little-endian <see cref="ushort" /> from a byte array at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="ushort" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetUInt16(int index) => Unsafe.ReadUnaligned<ushort>(ref bytes[index]);

        /// <summary>
        /// Reads a <see cref="ushort" /> from a byte array at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="ushort" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetUInt16(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetUInt16(index)
                : (ushort)(bytes[index + 1] | bytes[index] << 8);

        /// <summary>
        /// Writes a little-endian <see cref="ushort" /> to a byte array at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The <see cref="ushort" /> value to write.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt16(int index, ushort value) => bytes.AsSpan(index).SetUInt16(value);

        /// <summary>
        /// Writes a <see cref="ushort" /> to a byte array at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The <see cref="ushort" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt16(int index, ushort value, Endian endian) => bytes.AsSpan(index).SetUInt16(value, endian);
    }
}