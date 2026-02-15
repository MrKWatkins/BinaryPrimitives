using System.Buffers;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for arrays.
/// </summary>
public static class ArrayExtensions
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
}