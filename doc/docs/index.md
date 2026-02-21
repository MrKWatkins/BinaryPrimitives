# Home

[![Build Status](https://github.com/MrKWatkins/BinaryPrimitives/actions/workflows/build.yml/badge.svg)](https://github.com/MrKWatkins/BinaryPrimitives/actions/workflows/build.yml)
[![NuGet Version](https://img.shields.io/nuget/v/MrKWatkins.BinaryPrimitives)](https://www.nuget.org/packages/MrKWatkins.BinaryPrimitives)
[![NuGet Downloads](https://img.shields.io/nuget/dt/MrKWatkins.BinaryPrimitives)](https://www.nuget.org/packages/MrKWatkins.BinaryPrimitives)

> Extension methods on primitive types for various binary operations, such as reading and writing values from bytes and calculating parity.

The name is based on the [BinaryPrimitives](https://learn.microsoft.com/en-us/dotnet/api/system.buffers.binary.binaryprimitives) class in the .NET Framework which provides similar methods.

## Reading and Writing

Extension methods on byte container types (`byte[]`, `Span<byte>`, `Stream`, and others) for
reading and writing multi-byte primitive values with configurable endianness.

[Read more](reading-and-writing.md)

## UInt24

A 24-bit unsigned integer value type (`UInt24`) bridging the gap between `ushort` (16-bit) and
`uint` (32-bit). Implements the full suite of .NET generic math interfaces and is returned by
all `GetUInt24` read methods.

[Read more](uint24.md)

## Bit Operations

Extension methods on integer types (`byte`, `ushort`, `int`, `uint`, `long`, `ulong`) for
common bit-level operations: individual bit access, bit range extraction and replacement, nibble
manipulation, parity, overflow detection, and binary string formatting.

[Read more](bit-operations.md)

## Streams

Two `Stream` implementations for byte-oriented work: `ReadOnlyListStream` wraps an
`IReadOnlyList<byte>` as a seekable read-only stream, and `PeekableStream` adds
peek-without-consuming to any readable stream.

[Read more](streams.md)

## Licencing

Licensed under GPL v3.0.
