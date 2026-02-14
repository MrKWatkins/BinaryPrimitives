# AGENTS.md

This file provides guidance to AI agents when working with code in this repository.

## Project Overview

C# library providing extension methods for reading and writing bytes as primitive types, with endianness support. Published as the `MrKWatkins.BinaryPrimitives` NuGet package.

## Build & Test Commands

All commands run from the `src/` directory:

```bash
dotnet build src/BinaryPrimitives.sln # Build all projects.

dotnet test --solution src/BinaryPrimitives.sln  # Run all tests.
dotnet test --solution src/BinaryPrimitives.sln --configuration Release -- --coverage --coverage-output coverage.xml --coverage-output-format cobertura # Run all tests with code coverage.
dotnet test --project src/MrKWatkins.BinaryPrimitives.Tests  # Run a single test project.
dotnet test --project src/MrKWatkins.BinaryPrimitives.Tests --filter "FullyQualifiedName~ByteExtensionsTests"  # Run a single test class.
dotnet test --project src/MrKWatkins.BinaryPrimitives.Tests --filter "FullyQualifiedName~GetBit"  # Run specific test methods.

dotnet format src/BinaryPrimitives.sln  # Format the source code.
```

The test project uses the NUnit runner with Microsoft Testing Platform (`EnableNUnitRunner`), so test executables can also be run directly.

## Architecture

The library is a single project with extension methods organized one class per primitive type:

- **`Endian`** enum (Little, Big) controls byte ordering for multi-byte operations.
- **Type-specific extensions** (`ByteExtensions`, `WordExtensions`, `UInt24Extensions`, `Int32Extensions`, `UInt32Extensions`, `Int64Extensions`, `UInt64Extensions`) provide read/write operations for each primitive size.
- **Container-specific extensions** (`ReadOnlyListExtensions`, `ReadOnlyMemoryExtensions`, `StreamExtensions`) adapt operations for different byte container types. (byte[], Span, IReadOnlyList, Stream, ReadOnlyMemory)
- **`ReadOnlyListStream`** wraps IReadOnlyList<byte> as a read-only seekable Stream.
- **`BoolExtensions`** provides `ToBitChar()` conversion.

Multi-byte read/write methods follow the pattern: read from container with optional `Endian` parameter (defaulting to Little).

## Code Conventions

- `[Pure]` and `[MethodImpl(MethodImplOptions.AggressiveInlining)]` on performance-critical extension methods.
- `Unsafe.ReadUnaligned`/`WriteUnaligned` for efficient byte-to-primitive conversions.
- `OverloadResolutionPriority` attributes to handle IList vs IReadOnlyList vs concrete type overloads.
- Global usings configured in Directory.Build.props: `System.Diagnostics.CodeAnalysis`, `System.Diagnostics.Contracts`, `PureAttribute`, `JetBrains.Annotations`.
- Warnings are errors; CA1707 (underscores in names) is suppressed in test projects only.

## Testing Conventions

- NUnit 4 with `MrKWatkins.Assertions`. (`.Should().Equal()`, `.Should().Throw<>()`, `AssertThat.Invoking()`)
- Global usings for `MrKWatkins.Assertions` and `NUnit.Framework` in test projects.
- One test class per implementation class; test methods use `[TestCase]` for parameterized tests.
- Test names should match the method they're testing.
- If a method is overloaded then test names should distinguish overloads by their parameter types, separated by an underscore, e.g. `GetInt32_Span`, `GetInt32_ReadOnlySpan`.
- Extra conditions can be appended to the end of test names to distinguish them, e.g. `GetBits_InvalidRange`. The happy path should never have extra conditions.
- InternalsVisibleTo is automatically configured for test projects.
