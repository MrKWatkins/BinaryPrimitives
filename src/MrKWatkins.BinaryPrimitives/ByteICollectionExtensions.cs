namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for <see cref="ICollection{T}" /> of <see cref="byte" />.
/// </summary>
public static class ByteICollectionExtensions
{
    /// <param name="bytes">The byte collection to add to.</param>
    extension(ICollection<byte> bytes)
    {
        /// <summary>
        /// Adds a word to a byte collection.
        /// </summary>
        /// <param name="value">The word value to add.</param>
        /// <param name="endian">The endianness to use.</param>
        public void AddWord(ushort value, Endian endian = Endian.Little)
        {
            var (msb, lsb) = value.ToBytes();
            if (endian == Endian.Little)
            {
                bytes.Add(lsb);
                bytes.Add(msb);
            }
            else
            {
                bytes.Add(msb);
                bytes.Add(lsb);
            }
        }
    }
}