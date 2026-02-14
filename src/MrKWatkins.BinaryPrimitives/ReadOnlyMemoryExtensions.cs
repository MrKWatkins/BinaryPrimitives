using System.Buffers;

namespace MrKWatkins.BinaryPrimitives;

public static class ReadOnlyMemoryExtensions
{
    [Pure]
    public static ReadOnlySequence<T> CreateWrappedSequence<T>(this T[] array, int startIndex = 0) => new ReadOnlyMemory<T>(array).CreateWrappedSequence(startIndex);

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