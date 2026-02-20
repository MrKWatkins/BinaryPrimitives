# BinaryPrimitives

[![Build Status](https://github.com/MrKWatkins/BinaryPrimitives/actions/workflows/build.yml/badge.svg)](https://github.com/MrKWatkins/BinaryPrimitives/actions/workflows/build.yml)
[![NuGet Version](https://img.shields.io/nuget/v/MrKWatkins.BinaryPrimitives)](https://www.nuget.org/packages/MrKWatkins.BinaryPrimitives)
[![NuGet Downloads](https://img.shields.io/nuget/dt/MrKWatkins.BinaryPrimitives)](https://www.nuget.org/packages/MrKWatkins.BinaryPrimitives)

> Extension methods on primitives types for various binary operations, such as reading and writing values from bytes and calculating parity.

The name is based on the [BinaryPrimitives](https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives) class in the .NET Framework which provides similar methods.

Full documentation is available at [mrkwatkins.github.io/BinaryPrimitives](https://mrkwatkins.github.io/BinaryPrimitives/). Source code and issue tracking are on [GitHub](https://github.com/MrKWatkins/BinaryPrimitives).

## Installation

```
dotnet add package MrKWatkins.BinaryPrimitives
```

## Usage

### Reading and Writing

Read and write multi-byte integers from `byte[]`, `Span<byte>`, `Stream`, and other byte
container types. All methods support an optional `Endian` parameter (defaulting to
`Endian.Little`):

```csharp
byte[] data = new byte[8];
data.SetInt32(0, 42);
data.SetUInt16(4, 1000, Endian.Big);

int    a = data.GetInt32(0);              // 42
ushort b = data.GetUInt16(4, Endian.Big); // 1000
```

Collections can have values appended directly:

```csharp
var buffer = new List<byte>();
buffer.AddUInt32(0xDEADBEEF);
buffer.AddInt16(-1, Endian.Big);
```

Streams support typed reading and writing, with async overloads for all methods:

```csharp
stream.WriteInt32(42);
int value = stream.ReadInt32OrThrow();
```

### Bit Operations

Get, set, and clear individual bits or ranges of bits on `byte`, `ushort`, `int`, `uint`,
`long`, and `ulong`:

```csharp
byte b = 0b00001010;
bool set   = b.GetBit(1);     // true
byte with0 = b.SetBit(0);     // 0b00001011
byte clear = b.ResetBit(1);   // 0b00001000
byte range = b.GetBits(1, 3); // bits 1..3, shifted to position 0
```

`byte` also provides nibble access, parity, and CPU flag detection:

```csharp
byte high = b.HighNibble(); // bits 4-7
bool even = b.Parity();     // true if an even number of bits are set
```

All supported integer types can be formatted as a binary string:

```csharp
string s = ((byte)0x5A).ToBinaryString(); // "0b01011010"
```

### Streams

`ReadOnlyListStream` wraps an `IReadOnlyList<byte>` as a seekable, read-only `Stream`:

```csharp
IReadOnlyList<byte> bytes = LoadBytes();
using var stream = new ReadOnlyListStream(bytes);
```

`PeekableStream` wraps any readable `Stream` to add one-byte lookahead without consuming the
byte:

```csharp
using var stream = new PeekableStream(inner);
if (stream.Peek() == 0xFF)
{
    stream.ReadByteOrThrow(); // consume it
}
```

## Licencing

Licensed under GPL v3.0.