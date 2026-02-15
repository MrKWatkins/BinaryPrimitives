using System.Runtime.CompilerServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for <see cref="int" />.
/// </summary>
public static class IntExtensions
{
    /// <param name="value">The int value.</param>
    extension(int value)
    {
        /// <summary>
        /// Gets the value of the bit at the specified index.
        /// </summary>
        /// <param name="index">The zero-based bit index.</param>
        /// <returns><see langword="true" /> if the bit is set; <see langword="false" /> otherwise.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool GetBit(int index) => (value & (1 << index)) != 0;
    }
}