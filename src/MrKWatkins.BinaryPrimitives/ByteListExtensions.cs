using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for reading primitive values from <see cref="List{T}" /> of <see cref="byte" />.
/// </summary>
public static class ByteListExtensions
{
    private const int ConcreteTypePriority = 2;

    /// <param name="bytes">The list of bytes.</param>
    extension(List<byte> bytes)
    {

        /// <summary>
        /// Reads a little-endian <see cref="short" /> from a <see cref="List{T}" /> of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="short" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short GetInt16(int index) =>
            CollectionsMarshal.AsSpan(bytes)[index..].GetInt16();

        /// <summary>
        /// Reads a <see cref="short" /> from a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="short" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short GetInt16(int index, Endian endian) =>
            CollectionsMarshal.AsSpan(bytes)[index..].GetInt16(endian);


        /// <summary>
        /// Reads a little-endian <see cref="int" /> from a <see cref="List{T}" /> of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="int" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetInt32(int index) =>
            CollectionsMarshal.AsSpan(bytes)[index..].GetInt32();

        /// <summary>
        /// Reads an <see cref="int" /> from a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="int" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetInt32(int index, Endian endian) =>
            CollectionsMarshal.AsSpan(bytes)[index..].GetInt32(endian);


        /// <summary>
        /// Reads a little-endian <see cref="long" /> from a <see cref="List{T}" /> of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="long" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long GetInt64(int index) =>
            CollectionsMarshal.AsSpan(bytes)[index..].GetInt64();

        /// <summary>
        /// Reads a <see cref="long" /> from a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="long" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long GetInt64(int index, Endian endian) =>
            CollectionsMarshal.AsSpan(bytes)[index..].GetInt64(endian);


        /// <summary>
        /// Reads a little-endian unsigned 24-bit integer from a <see cref="List{T}" /> of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The 24-bit value stored in an <see cref="int" />.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetUInt24(int index) =>
            CollectionsMarshal.AsSpan(bytes)[index..].GetUInt24();

        /// <summary>
        /// Reads an unsigned 24-bit integer from a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The 24-bit value stored in an <see cref="int" />.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetUInt24(int index, Endian endian) =>
            CollectionsMarshal.AsSpan(bytes)[index..].GetUInt24(endian);


        /// <summary>
        /// Reads a little-endian <see cref="uint" /> from a <see cref="List{T}" /> of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="uint" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetUInt32(int index) =>
            CollectionsMarshal.AsSpan(bytes)[index..].GetUInt32();

        /// <summary>
        /// Reads a <see cref="uint" /> from a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="uint" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetUInt32(int index, Endian endian) =>
            CollectionsMarshal.AsSpan(bytes)[index..].GetUInt32(endian);


        /// <summary>
        /// Reads a little-endian <see cref="ulong" /> from a <see cref="List{T}" /> of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="ulong" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetUInt64(int index) =>
            CollectionsMarshal.AsSpan(bytes)[index..].GetUInt64();

        /// <summary>
        /// Reads a <see cref="ulong" /> from a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="ulong" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetUInt64(int index, Endian endian) =>
            CollectionsMarshal.AsSpan(bytes)[index..].GetUInt64(endian);

        /// <summary>
        /// Reads a little-endian <see cref="ushort" /> from a <see cref="List{T}" /> of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="ushort" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetUInt16(int index) =>
            CollectionsMarshal.AsSpan(bytes)[index..].GetUInt16();

        /// <summary>
        /// Reads a <see cref="ushort" /> from a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="ushort" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetUInt16(int index, Endian endian) =>
            CollectionsMarshal.AsSpan(bytes)[index..].GetUInt16(endian);


        /// <summary>
        /// Writes a little-endian <see cref="short" /> to a <see cref="List{T}" /> of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt16(int index, short value) =>
            CollectionsMarshal.AsSpan(bytes)[index..].SetInt16(value);

        /// <summary>
        /// Writes a <see cref="short" /> to a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt16(int index, short value, Endian endian) =>
            CollectionsMarshal.AsSpan(bytes)[index..].SetInt16(value, endian);


        /// <summary>
        /// Writes a little-endian <see cref="int" /> to a <see cref="List{T}" /> of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt32(int index, int value) =>
            CollectionsMarshal.AsSpan(bytes)[index..].SetInt32(value);

        /// <summary>
        /// Writes an <see cref="int" /> to a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt32(int index, int value, Endian endian) =>
            CollectionsMarshal.AsSpan(bytes)[index..].SetInt32(value, endian);


        /// <summary>
        /// Writes a little-endian <see cref="long" /> to a <see cref="List{T}" /> of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt64(int index, long value) =>
            CollectionsMarshal.AsSpan(bytes)[index..].SetInt64(value);

        /// <summary>
        /// Writes a <see cref="long" /> to a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInt64(int index, long value, Endian endian) =>
            CollectionsMarshal.AsSpan(bytes)[index..].SetInt64(value, endian);


        /// <summary>
        /// Writes a little-endian unsigned 24-bit integer to a <see cref="List{T}" /> of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The 24-bit value to write. Only the lower 24 bits are used.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt24(int index, int value) =>
            CollectionsMarshal.AsSpan(bytes)[index..].SetUInt24(value);

        /// <summary>
        /// Writes an unsigned 24-bit integer to a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The 24-bit value to write. Only the lower 24 bits are used.</param>
        /// <param name="endian">The endianness to use.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt24(int index, int value, Endian endian) =>
            CollectionsMarshal.AsSpan(bytes)[index..].SetUInt24(value, endian);


        /// <summary>
        /// Writes a little-endian <see cref="uint" /> to a <see cref="List{T}" /> of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt32(int index, uint value) =>
            CollectionsMarshal.AsSpan(bytes)[index..].SetUInt32(value);

        /// <summary>
        /// Writes a <see cref="uint" /> to a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt32(int index, uint value, Endian endian) =>
            CollectionsMarshal.AsSpan(bytes)[index..].SetUInt32(value, endian);


        /// <summary>
        /// Writes a little-endian <see cref="ulong" /> to a <see cref="List{T}" /> of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt64(int index, ulong value) =>
            CollectionsMarshal.AsSpan(bytes)[index..].SetUInt64(value);

        /// <summary>
        /// Writes a <see cref="ulong" /> to a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt64(int index, ulong value, Endian endian) =>
            CollectionsMarshal.AsSpan(bytes)[index..].SetUInt64(value, endian);

        /// <summary>
        /// Writes a little-endian <see cref="ushort" /> to a <see cref="List{T}" /> of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The <see cref="ushort" /> value to write.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt16(int index, ushort value) =>
            CollectionsMarshal.AsSpan(bytes)[index..].SetUInt16(value);

        /// <summary>
        /// Writes a <see cref="ushort" /> to a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to write to.</param>
        /// <param name="value">The <see cref="ushort" /> value to write.</param>
        /// <param name="endian">The endianness to use.</param>
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetUInt16(int index, ushort value, Endian endian) =>
            CollectionsMarshal.AsSpan(bytes)[index..].SetUInt16(value, endian);
    }
}