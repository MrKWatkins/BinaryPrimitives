using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for <see cref="IReadOnlyList{T}" /> of <see cref="byte" />.
/// </summary>
public static class ReadOnlyListExtensions
{
    /// <summary>
    /// Copies the contents of a read-only list to a span, starting at the specified offset in the destination.
    /// </summary>
    /// <param name="source">The source list to copy from.</param>
    /// <param name="destination">The destination span.</param>
    /// <param name="start">The zero-based index in <paramref name="destination" /> to start copying to.</param>
    /// <exception cref="ArgumentException"><paramref name="destination" /> does not have enough space to copy <paramref name="source" />.</exception>
    public static void CopyTo(this IReadOnlyList<byte> source, Span<byte> destination, int start) => source.CopyTo(destination[start..]);

    /// <summary>
    /// Copies the contents of a read-only list to a span.
    /// </summary>
    /// <param name="source">The source list to copy from.</param>
    /// <param name="destination">The destination span.</param>
    /// <exception cref="ArgumentException"><paramref name="destination" /> does not have enough space to copy <paramref name="source" />.</exception>
    public static void CopyTo(this IReadOnlyList<byte> source, Span<byte> destination)
    {
        if (source.Count > destination.Length)
        {
            throw new ArgumentException("Value does not have enough space to copy {nameof(source)}.", nameof(destination));
        }

        ref var reference = ref MemoryMarshal.GetReference(destination);
        foreach (var @byte in source)
        {
            reference = @byte;
            reference = ref Unsafe.Add(ref reference, sizeof(byte));
        }
    }
}
