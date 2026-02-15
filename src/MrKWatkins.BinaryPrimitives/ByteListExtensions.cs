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
        /// Reads a little-endian word from a <see cref="List{T}" /> of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The word value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetWord(int index) =>
            CollectionsMarshal.AsSpan(bytes)[index..].GetWord();

        /// <summary>
        /// Reads a word from a <see cref="List{T}" /> of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The word value.</returns>
        [Pure]
        [OverloadResolutionPriority(ConcreteTypePriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetWord(int index, Endian endian) =>
            CollectionsMarshal.AsSpan(bytes)[index..].GetWord(endian);

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
    }
}