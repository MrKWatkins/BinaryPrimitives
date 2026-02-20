using System.Buffers;
using System.Runtime.CompilerServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for <see cref="ReadOnlyMemory{T}" />.
/// </summary>
public static class ReadOnlyMemoryExtensions
{
    /// <summary>
    /// Creates a <see cref="ReadOnlySequence{T}" /> that wraps a <see cref="ReadOnlyMemory{T}" />, starting at the specified index.
    /// The sequence contains two segments covering the same underlying memory, allowing sequential reading that wraps around the starting position.
    /// </summary>
    /// <typeparam name="T">The type of elements in the memory.</typeparam>
    /// <param name="memory">The memory to wrap.</param>
    /// <param name="startIndex">The zero-based index to start from.</param>
    /// <returns>A <see cref="ReadOnlySequence{T}" /> wrapping the memory.</returns>
    [Pure]
    public static ReadOnlySequence<T> CreateWrappedSequence<T>(this ReadOnlyMemory<T> memory, int startIndex = 0)
    {
        var start = new WrapSegment<T>(memory);
        var end = start.Append(memory);
        return new ReadOnlySequence<T>(start, startIndex, end, end.Memory.Length);
    }

    /// <param name="bytes">The read-only memory of bytes.</param>
    extension(ReadOnlyMemory<byte> bytes)
    {
        /// <summary>
        /// Reads a little-endian <see cref="short" /> from read-only memory at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="short" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short GetInt16(int index) => bytes.Span[index..].GetInt16();

        /// <summary>
        /// Reads a <see cref="short" /> from read-only memory at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="short" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short GetInt16(int index, Endian endian) => bytes.Span[index..].GetInt16(endian);


        /// <summary>
        /// Reads a little-endian <see cref="int" /> from read-only memory at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="int" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetInt32(int index) => bytes.Span[index..].GetInt32();

        /// <summary>
        /// Reads an <see cref="int" /> from read-only memory at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="int" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetInt32(int index, Endian endian) => bytes.Span[index..].GetInt32(endian);


        /// <summary>
        /// Reads a little-endian <see cref="long" /> from read-only memory at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="long" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long GetInt64(int index) => bytes.Span[index..].GetInt64();

        /// <summary>
        /// Reads a <see cref="long" /> from read-only memory at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="long" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long GetInt64(int index, Endian endian) => bytes.Span[index..].GetInt64(endian);


        /// <summary>
        /// Reads a little-endian <see cref="UInt24" /> from read-only memory at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="UInt24" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt24 GetUInt24(int index) => bytes.Span[index..].GetUInt24();

        /// <summary>
        /// Reads a <see cref="UInt24" /> from read-only memory at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="UInt24" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt24 GetUInt24(int index, Endian endian) => bytes.Span[index..].GetUInt24(endian);


        /// <summary>
        /// Reads a little-endian <see cref="uint" /> from read-only memory at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="uint" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetUInt32(int index) => bytes.Span[index..].GetUInt32();

        /// <summary>
        /// Reads a <see cref="uint" /> from read-only memory at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="uint" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetUInt32(int index, Endian endian) => bytes.Span[index..].GetUInt32(endian);


        /// <summary>
        /// Reads a little-endian <see cref="ulong" /> from read-only memory at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="ulong" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetUInt64(int index) => bytes.Span[index..].GetUInt64();

        /// <summary>
        /// Reads a <see cref="ulong" /> from read-only memory at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="ulong" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetUInt64(int index, Endian endian) => bytes.Span[index..].GetUInt64(endian);

        /// <summary>
        /// Reads a little-endian <see cref="ushort" /> from read-only memory at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <returns>The <see cref="ushort" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetUInt16(int index) => bytes.Span[index..].GetUInt16();

        /// <summary>
        /// Reads a <see cref="ushort" /> from read-only memory at the specified index using the specified endianness.
        /// </summary>
        /// <param name="index">The zero-based index to read from.</param>
        /// <param name="endian">The endianness to use.</param>
        /// <returns>The <see cref="ushort" /> value.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetUInt16(int index, Endian endian) => bytes.Span[index..].GetUInt16(endian);
    }

    private sealed class WrapSegment<T> : ReadOnlySequenceSegment<T>
    {
        public WrapSegment(ReadOnlyMemory<T> memory)
        {
            Memory = memory;
        }

        [MustUseReturnValue]
        public WrapSegment<T> Append(ReadOnlyMemory<T> memory)
        {
            var segment = new WrapSegment<T>(memory)
            {
                RunningIndex = RunningIndex + Memory.Length
            };

            Next = segment;
            return segment;
        }
    }
}