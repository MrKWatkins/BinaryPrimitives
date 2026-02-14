using System.Runtime.CompilerServices;

namespace MrKWatkins.BinaryPrimitives;

/// <summary>
/// Extension methods for <see cref="bool" />.
/// </summary>
public static class BoolExtensions
{
    // TODO: Review performance of this with .NET 10.
    /// <summary>
    /// Converts a <see cref="bool" /> to a bit character, i.e. <c>'1'</c> for <see langword="true" /> and <c>'0'</c> for <see langword="false" />.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The bit character.</returns>
    [Pure]
    public static char ToBitChar(this bool value)
    {
        // 10x quicker than the already stupidly quick value ? '1' : '0' due to no branching.

        // | Method                | Mean      | Error     | StdDev    | Ratio |
        // |---------------------- |----------:|----------:|----------:|------:|
        // | Ternary               | 33.307 ms | 0.0948 ms | 0.0887 ms |  1.00 |
        // | UnsafeShenanigans     |  3.798 ms | 0.0176 ms | 0.0165 ms |  0.11 |

        ref var @byte = ref Unsafe.As<bool, byte>(ref value);

        return (char)(@byte + '0');
    }
}