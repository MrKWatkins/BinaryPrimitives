# UInt24

[`UInt24`](API/MrKWatkins.BinaryPrimitives/UInt24/index.md) is a 24-bit unsigned integer value type provided by the library. It fills the gap
between [`ushort`](https://learn.microsoft.com/en-us/dotnet/api/system.uint16) (16-bit) and
[`uint`](https://learn.microsoft.com/en-us/dotnet/api/system.uint32) (32-bit), and is the type
returned by all `GetUInt24` methods and accepted by all `SetUInt24` methods.

## Range and Constants

A `UInt24` can represent integers from 0 to 16,777,215 (2<sup>24</sup> − 1).

| Constant                    | Value      |
| --------------------------- | ---------- |
| `UInt24.MinValue`           | 0          |
| `UInt24.MaxValue`           | 16,777,215 |
| `UInt24.Zero`               | 0          |
| `UInt24.One`                | 1          |
| `UInt24.AdditiveIdentity`   | 0          |
| `UInt24.MultiplicativeIdentity` | 1      |

## Numeric Interfaces

`UInt24` implements the standard .NET generic math interfaces, giving it full interoperability
with generic algorithms:

- [`IBinaryInteger<UInt24>`](https://learn.microsoft.com/en-us/dotnet/api/system.numerics.ibinaryinteger-1)
- [`IUnsignedNumber<UInt24>`](https://learn.microsoft.com/en-us/dotnet/api/system.numerics.iunsignednumber-1)
- [`IMinMaxValue<UInt24>`](https://learn.microsoft.com/en-us/dotnet/api/system.numerics.iminmaxvalue-1)

## Conversions

### Implicit Widening Conversions

The following conversions are implicit (no cast required) because the target type can always
represent every `UInt24` value:

| Direction          | Notes                              |
| ------------------ | ---------------------------------- |
| [`byte`](https://learn.microsoft.com/en-us/dotnet/api/system.byte) → `UInt24`  | 0..255 always fits                 |
| [`ushort`](https://learn.microsoft.com/en-us/dotnet/api/system.uint16) → `UInt24`| 0..65,535 always fits              |
| `UInt24` → [`uint`](https://learn.microsoft.com/en-us/dotnet/api/system.uint32)  | Always safe, no data loss          |
| `UInt24` → [`int`](https://learn.microsoft.com/en-us/dotnet/api/system.int32)   | Always safe; result is non-negative|
| `UInt24` → [`long`](https://learn.microsoft.com/en-us/dotnet/api/system.int64)  | Always safe                        |
| `UInt24` → [`ulong`](https://learn.microsoft.com/en-us/dotnet/api/system.uint64) | Always safe                        |

```csharp
UInt24 a = (byte)255;      // implicit from byte
UInt24 b = (ushort)65535;  // implicit from ushort

uint  u = (UInt24)100;     // implicit to uint
int   i = (UInt24)100;     // implicit to int
long  l = (UInt24)100;     // implicit to long
ulong ul = (UInt24)100;    // implicit to ulong
```

### Explicit Narrowing Conversions

Conversions that may overflow require an explicit cast. An
[`OverflowException`](https://learn.microsoft.com/en-us/dotnet/api/system.overflowexception) is
thrown in the cases shown:

| Direction             | Throws `OverflowException` if…                       |
| --------------------- | ---------------------------------------------------- |
| [`sbyte`](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte) → `UInt24`    | value is negative                                    |
| [`short`](https://learn.microsoft.com/en-us/dotnet/api/system.int16) → `UInt24`    | value is negative                                    |
| [`int`](https://learn.microsoft.com/en-us/dotnet/api/system.int32) → `UInt24`      | value is negative or > 16,777,215                    |
| [`uint`](https://learn.microsoft.com/en-us/dotnet/api/system.uint32) → `UInt24`     | value > 16,777,215                                   |
| [`long`](https://learn.microsoft.com/en-us/dotnet/api/system.int64) → `UInt24`     | value is negative or > 16,777,215                    |
| [`ulong`](https://learn.microsoft.com/en-us/dotnet/api/system.uint64) → `UInt24`    | value > 16,777,215                                   |
| [`nint`](https://learn.microsoft.com/en-us/dotnet/api/system.intptr) → `UInt24`     | value is negative or > 16,777,215                    |
| [`nuint`](https://learn.microsoft.com/en-us/dotnet/api/system.uintptr) → `UInt24`    | value > 16,777,215                                   |
| [`Half`](https://learn.microsoft.com/en-us/dotnet/api/system.half) → `UInt24`     | value is negative, NaN, or infinity                  |
| [`float`](https://learn.microsoft.com/en-us/dotnet/api/system.single) → `UInt24`    | value is negative, NaN, infinity, or > 16,777,215    |
| [`double`](https://learn.microsoft.com/en-us/dotnet/api/system.double) → `UInt24`   | value is negative, NaN, infinity, or > 16,777,215    |
| [`decimal`](https://learn.microsoft.com/en-us/dotnet/api/system.decimal) → `UInt24`  | value is negative or > 16,777,215                    |
| `UInt24` → [`byte`](https://learn.microsoft.com/en-us/dotnet/api/system.byte)     | value > 255                                          |
| `UInt24` → [`sbyte`](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte)    | value > 127                                          |
| `UInt24` → [`short`](https://learn.microsoft.com/en-us/dotnet/api/system.int16)    | value > 32,767                                       |
| `UInt24` → [`ushort`](https://learn.microsoft.com/en-us/dotnet/api/system.uint16)   | value > 65,535                                       |

```csharp
UInt24 x = (UInt24)16777215; // explicit from uint — OK
UInt24 y = (UInt24)16777216; // throws OverflowException

byte b = (byte)(UInt24)255;  // explicit to byte — OK
byte c = (byte)(UInt24)256;  // throws OverflowException
```

### Generic Math — `CreateChecked`, `CreateSaturating`, `CreateTruncating`

`UInt24` also supports the generic math conversion methods for use in generic algorithms:

```csharp
UInt24 a = UInt24.CreateChecked<int>(100);     // 100 — throws if out of range
UInt24 b = UInt24.CreateSaturating<int>(-1);   // 0   — clamps to MinValue
UInt24 c = UInt24.CreateSaturating<int>(20_000_000); // 16,777,215 — clamps to MaxValue
UInt24 d = UInt24.CreateTruncating<uint>(0xFFFFFFFF); // 0xFFFFFF — keeps low 24 bits
```

## Arithmetic

All standard arithmetic operators are supported. In unchecked contexts they wrap silently; in
checked contexts they throw
[`OverflowException`](https://learn.microsoft.com/en-us/dotnet/api/system.overflowexception) on
overflow.

```csharp
UInt24 a = UInt24.MaxValue;
UInt24 b = a + (UInt24)1;      // wraps to 0 (unchecked)

checked
{
    UInt24 c = UInt24.MaxValue + (UInt24)1; // throws OverflowException
}
```

## Parsing and Formatting

`UInt24` supports the same parsing and formatting API as the built-in integer types:

```csharp
UInt24 v = UInt24.Parse("12345", CultureInfo.InvariantCulture);
string s = v.ToString("X6", CultureInfo.InvariantCulture); // "00003039"

if (UInt24.TryParse("16777216", CultureInfo.InvariantCulture, out var result))
{
    // not reached — value exceeds MaxValue
}
```

## Reading and Writing

`UInt24` is used as the return type and parameter type for the 24-bit read/write extension
methods on all supported byte container types. An optional
[`Endian`](API/MrKWatkins.BinaryPrimitives/Endian/index.md) parameter (default
`Endian.Little`) controls byte ordering.

```csharp
byte[] data = new byte[3];

// Write little-endian (default)
data.SetUInt24(0, (UInt24)0x123456);
// data is now [0x56, 0x34, 0x12]

// Read it back
UInt24 le = data.GetUInt24(0);             // 0x123456
UInt24 be = data.GetUInt24(0, Endian.Big); // 0x563412

// Works on all container types
Span<byte> span = data;
UInt24 fromSpan = span.GetUInt24();

using var stream = new MemoryStream(data);
UInt24 fromStream = stream.ReadUInt24OrThrow();
```

See [Reading and Writing](reading-and-writing.md) for the full list of supported container types.
