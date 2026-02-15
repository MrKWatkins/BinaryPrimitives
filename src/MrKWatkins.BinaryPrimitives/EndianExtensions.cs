using System.Runtime.CompilerServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for <see cref="Endian" />.
/// </summary>
public static class EndianExtensions
{
    /// <param name="endian">The endianness to use.</param>
    extension(Endian endian)
    {
        /// <summary>
        /// Composes a word from two bytes.
        /// </summary>
        /// <param name="msb">The most significant byte.</param>
        /// <param name="lsb">The least significant byte.</param>
        /// <returns>The composed word.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort ToWord(byte msb, byte lsb) =>
            endian == Endian.Little
                ? (ushort)(msb | lsb << 8)
                : (ushort)(lsb | msb << 8);

        /// <summary>
        /// Composes an unsigned 24-bit integer from three bytes.
        /// </summary>
        /// <param name="msb">The most significant byte.</param>
        /// <param name="mid">The middle byte.</param>
        /// <param name="lsb">The least significant byte.</param>
        /// <returns>The composed 24-bit value stored in an <see cref="int" />.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ToUInt24(byte msb, byte mid, byte lsb)
        {
            if (endian == Endian.Little)
            {
                return msb | mid << 8 | lsb << 16;
            }

            return lsb | mid << 8 | msb << 16;
        }
    }
}