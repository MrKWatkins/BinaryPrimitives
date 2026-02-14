using System.Runtime.CompilerServices;

namespace MrKWatkins.BinaryPrimitives;

public static class BoolExtensions
{
    // TODO: Review performance of this with .NET 10.
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