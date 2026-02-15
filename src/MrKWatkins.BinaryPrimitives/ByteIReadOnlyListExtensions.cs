using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for <see cref="IReadOnlyList{T}" /> of <see cref="byte" />.
/// </summary>
public static class ByteIReadOnlyListExtensions
{
    private const int ReadOnlyPriority = 1;

    /// <param name="bytes">The read-only list of bytes.</param>
    extension(IReadOnlyList<byte> bytes)
    {
        /// <summary>
        /// Copies the contents of a read-only list to a span, starting at the specified offset in the destination.
        /// </summary>
        /// <param name="destination">The destination span.</param>
        /// <param name="start">The zero-based index in <paramref name="destination" /> to start copying to.</param>
        /// <exception cref="ArgumentException"><paramref name="destination" /> does not have enough space to copy <paramref name="bytes" />.</exception>
        public void CopyTo(Span<byte> destination, int start) => bytes.CopyTo(destination[start..]);

        /// <summary>
        /// Copies the contents of a read-only list to a span.
        /// </summary>
        /// <param name="destination">The destination span.</param>
        /// <exception cref="ArgumentException"><paramref name="destination" /> does not have enough space to copy <paramref name="bytes" />.</exception>
        public void CopyTo(Span<byte> destination)
        {
            if (bytes.Count > destination.Length)
            {
                throw new ArgumentException("Value does not have enough space to copy {nameof(bytes)}.", nameof(destination));
            }

            ref var reference = ref MemoryMarshal.GetReference(destination);
            foreach (var @byte in bytes)
            {
                reference = @byte;
                reference = ref Unsafe.Add(ref reference, sizeof(byte));
            }
        }


        /// <summary>
        /// Reads a little-endian <see cref="short" /> from a read-only list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="short" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ReadOnlyPriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short GetInt16(int index) => (short)(bytes[index] | bytes[index + 1] << 8);

        /// <summary>
        /// Reads a <see cref="short" /> from a read-only list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="short" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ReadOnlyPriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short GetInt16(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetInt16(index)
                : (short)(bytes[index + 1] | bytes[index] << 8);


        /// <summary>
        /// Reads a little-endian <see cref="int" /> from a read-only list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="int" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ReadOnlyPriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetInt32(int index) => bytes[index] | bytes[index + 1] << 8 | bytes[index + 2] << 16 | bytes[index + 3] << 24;

        /// <summary>
        /// Reads an <see cref="int" /> from a read-only list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="int" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ReadOnlyPriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetInt32(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetInt32(index)
                : bytes[index + 3] | bytes[index + 2] << 8 | bytes[index + 1] << 16 | bytes[index] << 24;

#pragma warning disable CS0675 // Bitwise-or operator used on a sign-extended operand




        /// <summary>
        /// Reads a little-endian <see cref="long" /> from a read-only list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="long" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ReadOnlyPriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long GetInt64(int index) =>
            bytes[index] | bytes[index + 1] << 8 | bytes[index + 2] << 16 | bytes[index + 3] << 24 |
            (long)bytes[index + 4] << 32 | (long)bytes[index + 5] << 40 | (long)bytes[index + 6] << 48 | (long)bytes[index + 7] << 56;

        /// <summary>
        /// Reads a <see cref="long" /> from a read-only list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="long" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ReadOnlyPriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long GetInt64(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetInt64(index)
                : bytes[index + 7] | bytes[index + 6] << 8 | bytes[index + 5] << 16 | bytes[index + 4] << 24 |
                  (long)bytes[index + 3] << 32 | (long)bytes[index + 2] << 40 | (long)bytes[index + 1] << 48 | (long)bytes[index] << 56;


#pragma warning restore CS0675


        /// <summary>
        /// Reads a little-endian unsigned 24-bit integer from a read-only list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The 24-bit value stored in an <see cref="int" />.</returns>
        [Pure]
        [OverloadResolutionPriority(ReadOnlyPriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetUInt24(int index) => bytes[index] | bytes[index + 1] << 8 | bytes[index + 2] << 16;

        /// <summary>
        /// Reads an unsigned 24-bit integer from a read-only list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The 24-bit value stored in an <see cref="int" />.</returns>
        [Pure]
        [OverloadResolutionPriority(ReadOnlyPriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetUInt24(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetUInt24(index)
                : bytes[index] << 16 | bytes[index + 1] << 8 | bytes[index + 2];


        /// <summary>
        /// Reads a little-endian <see cref="uint" /> from a read-only list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="uint" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ReadOnlyPriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetUInt32(int index) => (uint)(bytes[index] | bytes[index + 1] << 8 | bytes[index + 2] << 16 | bytes[index + 3] << 24);

        /// <summary>
        /// Reads a <see cref="uint" /> from a read-only list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="uint" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ReadOnlyPriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetUInt32(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetUInt32(index)
                : (uint)(bytes[index + 3] | bytes[index + 2] << 8 | bytes[index + 1] << 16 | bytes[index] << 24);

#pragma warning disable CS0675 // Bitwise-or operator used on a sign-extended operand



        /// <summary>
        /// Reads a little-endian <see cref="ulong" /> from a read-only list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="ulong" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ReadOnlyPriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetUInt64(int index) =>
            bytes[index] | (ulong)bytes[index + 1] << 8 | (ulong)bytes[index + 2] << 16 | (ulong)bytes[index + 3] << 24 |
            (ulong)bytes[index + 4] << 32 | (ulong)bytes[index + 5] << 40 | (ulong)bytes[index + 6] << 48 | (ulong)bytes[index + 7] << 56;

        /// <summary>
        /// Reads a <see cref="ulong" /> from a read-only list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="ulong" /> value.</returns>
        [Pure]
        [OverloadResolutionPriority(ReadOnlyPriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetUInt64(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetUInt64(index)
                : bytes[index + 7] | (ulong)bytes[index + 6] << 8 | (ulong)bytes[index + 5] << 16 | (ulong)bytes[index + 4] << 24 |
                  (ulong)bytes[index + 3] << 32 | (ulong)bytes[index + 2] << 40 | (ulong)bytes[index + 1] << 48 | (ulong)bytes[index] << 56;


#pragma warning restore CS0675


        /// <summary>
        /// Reads a little-endian word from a read-only list of bytes at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The word value.</returns>
        [Pure]
        [OverloadResolutionPriority(ReadOnlyPriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetWord(int index) => (ushort)(bytes[index] | bytes[index + 1] << 8);

        /// <summary>
        /// Reads a word from a read-only list of bytes at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The word value.</returns>
        [Pure]
        [OverloadResolutionPriority(ReadOnlyPriority)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetWord(int index, Endian endian) =>
            endian == Endian.Little
                ? bytes.GetWord(index)
                : (ushort)(bytes[index + 1] | bytes[index] << 8);
    }
}