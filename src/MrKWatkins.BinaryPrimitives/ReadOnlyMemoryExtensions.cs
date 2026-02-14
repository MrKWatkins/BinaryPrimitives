using System.Buffers;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods that return <see cref="ReadOnlySequence{T}" /> instances.
/// </summary>
public static class ReadOnlySequenceExtensions
{
    /// <summary>
    /// Creates a <see cref="ReadOnlySequence{T}" /> that wraps an array, starting at the specified index. The sequence contains
    /// two segments covering the same underlying memory, allowing sequential reading that wraps around the starting position.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="array">The array to wrap.</param>
    /// <param name="startIndex">The zero-based index to start from.</param>
    /// <returns>A <see cref="ReadOnlySequence{T}" /> wrapping the array.</returns>
    [Pure]
    public static ReadOnlySequence<T> CreateWrappedSequence<T>(this T[] array, int startIndex = 0) => new ReadOnlyMemory<T>(array).CreateWrappedSequence(startIndex);

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