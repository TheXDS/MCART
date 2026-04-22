# MCART Core Library

**MCART** (Morgan's CLR Advanced Runtime) Core is the foundational library providing essential types, extensions, utilities, and helpers for .NET 8+ development. It offers a comprehensive suite of functionality across collections, reflection, mathematics, and type manipulation, designed for modern .NET applications including cloud-native and edge computing scenarios.

- **Latest Version**: [![NuGet](https://img.shields.io/nuget/v/TheXDS.MCART)](https://www.nuget.org/packages/TheXDS.MCART/)
- **Platform**: .NET 8.0+
- **Language**: C# 12
- **License**: MIT

## Features Overview

MCART Core provides:

- **Rich Type System**: Geometric types, collections, ranges, and specialized containers
- **Extensive Extensions**: 40+ extension method classes for common .NET types
- **Mathematical Operations**: Algebra, geometry, statistics, and animation utilities
- **Reflection Utilities**: Advanced type inspection, member discovery, and metadata access
- **Event System**: 20+ specialized event argument types for data binding and notifications
- **Custom Attributes**: 30+ attributes for code documentation and metadata
- **Exception Hierarchy**: Domain-specific exceptions with value context
- **AOT & Trimming Support**: Full compatibility with .NET ahead-of-time compilation and code trimming

## Installation

### NuGet Package Manager
```powershell
Install-Package TheXDS.MCART
```

### .NET CLI
```bash
dotnet add package TheXDS.MCART
```

### Direct Package Reference
```xml
<PackageReference Include="TheXDS.MCART" Version="1.0.0" />
```

## Functional Groups

MCART Core is organized into several functional areas. Each group contains closely related types and utilities:

### 1. Type System & Collections

The type system provides specialized containers and data structures for common patterns:

**Key Types:**
- `Range<T>` – Generic value range with inclusive/exclusive bounds
- `Point`, `Point3D` – Geometric coordinate types
- `Size`, `Size3D` – Dimensional measurement types
- `AutoDictionary<TKey, TValue>` – Dictionary that auto-instantiates missing values
- `ListEx<T>` – Observable list with granular change events
- `OpenList<T>` – Flexible list implementation
- `NamedObject<T>` – Generic named object wrapper
- `Grouping<TKey, TElement>` – Grouping container

**Example: Working with Ranges**

```csharp
using TheXDS.MCART.Types;

// Create a range from 1 to 100
var range = new Range<int>(1, 100);

// Verify membership
bool contains50 = range.IsWithin(50);    // true
bool contains150 = range.IsWithin(150);  // false

// Get range statistics
int min = range.Minimum;
int max = range.Maximum;
```

**Example: Named Objects**

```csharp
using TheXDS.MCART.Types;

// Create a named object for user-friendly identification
var userRole = new NamedObject<int>("Administrator", 1);

Console.WriteLine(userRole.Name);  // "Administrator"
Console.WriteLine(userRole.Value); // 1
```

### 2. Collection & Enumeration Extensions

MCART provides comprehensive extension methods for working with collections and enumerables:

**Key Extension Classes:**
- `CollectionExtensions` – Generic collection operations
- `EnumerableExtensions` – LINQ-style enumerable utilities
- `ListExtensions` – List-specific operations
- `DictionaryExtensions` – Dictionary helpers
- `EnumeratorExtensions` – Enumerator manipulation

**Example: Collection Operations**

```csharp
using TheXDS.MCART.Types.Extensions;
using System.Linq;

var numbers = new[] { 1, 2, 3, 4, 5 };

// Check if collection contains any of a specific type
bool hasString = numbers.OfType<object>().IsAnyOf<string>();

// Safe collection operations
var safeList = new List<int> { 1, 2, 3 };
safeList.Locked(list => {
    // Operations on list in locked context
    list.Add(4);
});

// Auto dictionary - returns default value for missing keys
var autoDict = new AutoDictionary<string, int>();
int count = autoDict["missing"];  // Returns 0 (default for int)
```

### 3. Type & Reflection Utilities

Advanced reflection and type manipulation capabilities for runtime type inspection and manipulation:

**Key Helpers:**
- `TypeExtensions` – Type inspection and instantiation
- `ReflectionHelpers` – Metadata and member discovery
- `AssemblyExtensions` – Assembly information access
- `PropertyInfoExtensions`, `MethodInfoExtensions` – Member-level reflection
- `EnumExtensions` – Enum utilities and byte conversion

**Example: Reflection Operations**

```csharp
using System;
using System.ComponentModel.DataAnnotations;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.Helpers;

// Type instantiation
var type = typeof(Exception);
var instance = type.New();  // Calls parameterless constructor

// Get enum descriptions
public enum Status
{
    [Description("In Progress")]
    Active,
    [Description("Completed")]
    Inactive
}

// Read enum description attribute
var description = Status.Active.GetAttribute<DescriptionAttribute>()?.Description;  // "In Progress"
var memberName = Status.Active.NameOf();  // "Active"

// Get all public properties
var properties = typeof(MyClass).GetPublicProperties();
foreach (var prop in properties)
{
    Console.WriteLine($"{prop.Name}: {prop.PropertyType.Name}");
}
```

### 4. String & Parsing Extensions

Comprehensive string manipulation, validation, and parsing utilities:

**Key Extension Classes:**
- `StringExtensions` – String parsing, validation, and formatting
- `StringBuilderFluentExtensions` – Fluent StringBuilder API
- `EnumExtensions` – Enum-to-string conversions
- `ObjectExtensions` – Generic object-to-string operations

**Example: String Operations**

```csharp
using System.Text;
using TheXDS.MCART.Types.Extensions;

string text = "  Hello World  ";

// String validation
bool isEmpty = text.IsEmpty();
bool isNullOrWhiteSpace = string.IsNullOrWhiteSpace(text); // Less boilerplate than this

// Search with options
bool found = text.TokenSearch("*HELLO*", StringExtensions.SearchOptions.IgnoreCase);

// Fluent string building
var sb = new StringBuilder()
    .AppendLineIfNotNull("Line 1")
    .AppendAndWrap("A very long line 2 past 80 characters", 80);
```

### 5. Mathematical Operations

Comprehensive mathematical, geometric, and statistical utilities:

**Key Math Modules:**
- `Algebra` – Prime number checking, modular arithmetic, polynomial operations
- `Geometry` – Trigonometry, Bézier curves, point transformations, distance calculations
- `Statistics` – Data normalization, variance, distribution analysis
- `Series` – Mathematical sequences and sequence analysis
- `Tween` – Animation and interpolation functions


**Example: Algebraic Operations**

```csharp
using TheXDS.MCART.Math;

// Prime number checking
bool isPrime = Algebra.IsPrime(17);  // true
bool isPowerOfTwo = Algebra.IsTwoPow(1024); // true
bool arePositive = new[] { 1, 2, 3, 4 }.ArePositive(); // true
bool isWhole = (3.14).IsWhole(); // false
bool isValid = double.NaN.IsValid(); // false
```

**Example: Statistical Analysis**

```csharp
using TheXDS.MCART.Math;

var data = new[] { 1.0, 2.0, 3.0, 4.0, 5.0 };

// Calculate statistics
double mean = Statistics.MeanTendency(data);         // 3.0
double stdDev = Statistics.StandardDeviation(data);  // √2
```

### 6. Exception Hierarchy

Domain-specific exception types for precise error handling:

**Key Exception Types:**
- `OffendingException<T>` – Exception with offending value context
- `InvalidTypeException` – Type contract violations
- `NullItemException` – Null reference violations
- `EmptyCollectionException` – Empty collection errors
- `MissingTypeException`, `MissingResourceException` – Missing dependencies
- `InvalidArgumentException` – Invalid argument errors
- `TamperException` – Tamper detection

**Example: Exception Handling**

```csharp
using TheXDS.MCART.Exceptions;

try
{
    var collection = new List<string>();
    
    if (collection.Count == 0)
        throw new EmptyCollectionException(nameof(collection));
    
    var item = collection.FirstOrDefault() ?? throw new NullItemException(nameof(collection));
}
catch (OffendingException<string> ex)
{
    Console.WriteLine($"Offending value: {ex.OffendingValue}");
    Console.WriteLine($"Error: {ex.Message}");
}
```

### 7. Metadata & Attributes

Comprehensive attribute system for code documentation and technical metadata:

**Key Attribute Categories:**
- **Documentation Attributes**: `AuthorAttribute`, `CopyrightAttribute`, `DescriptionAttribute`
- **Version Attributes**: `VersionAttribute`, `BetaAttribute`, `UnstableAttribute`
- **License Attributes**: `SpdxLicenseAttribute`, `LicenseTextAttribute`, `EmbeddedLicenseAttribute`
- **Technical Attributes**: `ProtocolFormatAttribute`, `EndiannessAttribute`, `CompressorAttribute`

**Example: Using Attributes**

```csharp
using TheXDS.MCART.Attributes;
using System.Reflection;

[Author("John Doe")]
[Copyright(2026, "Acme Corp")]
[Description("Represents application configuration")]
[Version(1, 0)]
public class AppConfig
{
    [Description("Application name")]
    public string Name { get; set; }
}

// Read attributes
var type = typeof(AppConfig);
var author = type.GetAttribute<AuthorAttribute>();
var copyright = type.GetAttribute<CopyrightAttribute>();

Console.WriteLine($"Author: {author?.Author}");
Console.WriteLine($"Copyright: © {copyright?.Year} {copyright?.CompanyName}");
```

### 8. Helper Utilities

Static utility classes for common operations:

**Key Helper Classes:**
- `Common` – General-purpose utilities and type converters
- `CollectionHelpers` – Collection manipulation and bitwise operations
- `ReflectionHelpers` – Reflection-based operations
- `Objects` – Object copying and comparison utilities

**Example: Object Utilities**

```csharp
using TheXDS.MCART.Helpers;

// Shallow copy
var original = new { Name = "John", Age = 30 };
var copy = original.ShallowClone();

// Reference to itself (useful in VB's "With" block)
var obj = originalObject.Itself();

// Quick conversion to types
IEnumerable<Type> types = Objects.ToTypes("Abc", '@', 1); // { string, char, int }
```

### 10. Specialized Types

Additional utility types for specific scenarios:

**Concurrency & Flow Control:**
- `PauseToken`, `PauseTokenSource` – Asynchronous pause operations
- `TimerEx` – Enhanced timer with additional features
- `TcpClientEx` – Extended TCP client functionality

**Example: Pause Token for Async Operations**

```csharp
using TheXDS.MCART.Types;

var pauseSource = new PauseTokenSource();

async Task LongRunningOperation(PauseToken pauseToken)
{
    for (int i = 0; i < 100; i++)
    {
        // Pause if requested
        await pauseToken.WaitWhilePausedAsync();
        
        // Do work...
        await Task.Delay(100);
    }
}

// Usage
var task = LongRunningOperation(pauseSource.Token);

// Pause the operation
pauseSource.IsPaused = true;

// Resume the operation
pauseSource.IsPaused = false;

await task;
```

## Advanced Features

### Converters

Type conversion implementations for common scenarios:

```csharp
using TheXDS.MCART.Types.Converters;

// Range converters for type-specific ranges
var converter = new Int32RangeConverter();
var range = converter.ConvertFrom("1:100");  // Converts string to Range<int>

// Enum description converter
var enumConverter = new EnumDescriptionConverter(typeof(MyEnum));
```

### Disposable Patterns

Base classes implementing disposal patterns:

```csharp
using TheXDS.MCART.Types.Base;

// Synchronous disposal
public class MyService : Disposable
{
    protected override void OnDispose() => DisposeManagedResources();
    protected override void OnFinalize() => DisposeUnmanagedResources();
}

// Asynchronous disposal
public class MyAsyncService : AsyncDisposable
{
    protected override async ValueTask OnDisposeAsync() 
    {
        await CleanupResourcesAsync();
    }
}

// Usage
using (var service = new MyService())
{
    // Use service...
} // Automatically disposed

await using (var asyncService = new MyAsyncService())
{
    // Use async service...
} // Automatically disposed
```

## Common Use Cases

| Use Case | Key Classes | Example |
|----------|------------|---------|
| **2D/3D Graphics** | `Point`, `Size`, `Geometry` | Coordinate transformation, distance calculations |
| **Data Processing** | `Range<T>`, `Statistics`, `CollectionHelpers` | Data normalization, range validation |
| **UI Development** | `NamedObject`, `ListEx`, Event types | Observable collections, property binding |
| **Type-Safe Reflection** | `TypeExtensions`, `ReflectionHelpers` | Generic type discovery, dynamic instantiation |
| **Mathematical Computing** | `Algebra`, `Geometry`, `Statistics` | Prime checking, Bézier curves, variance calculation |
| **Async Operations** | `PauseToken`, `TaskExtensions` | Long-running operation control |
| **Configuration** | `AutoDictionary` | Type-safe configuration with defaults |

## Building & Testing

Build the entire MCART solution:

```bash
dotnet build src/MCART.slnx
```

Run unit tests:

```bash
dotnet test src/MCART.slnx
```

Generate code coverage report:

```bash
dotnet test src/MCART.slnx --collect:"XPlat Code Coverage" --results-directory:./Build/Tests
reportgenerator -reports:./Build/Tests/*/coverage.cobertura.xml -targetdir:./Build/Coverage/
```

## System Requirements

- **.NET**: 8.0 or later
- **C#**: 12.0 or later
- **Platforms**: Windows, Linux, macOS, ARM
- **Compatibility**: AOT compilable, trimming-compatible

## Architecture & Design

MCART Core follows modern .NET design principles:

- **Type Safety**: Generic, strongly-typed implementations
- **Extensibility**: Extension methods for seamless integration with existing code
- **Reflection-Driven**: Leverages reflection for generic operations while supporting AOT
- **Async-First**: Comprehensive async/await support
- **Contract-Based**: Code contracts for validation and diagnostics
- **Observable**: Event-driven architecture with granular notifications

## Contributing

Contributions are welcome! Please ensure:

- Code follows existing naming and style conventions
- All public members include XML documentation
- Unit tests are included for new functionality
- Tests pass locally before submitting

## License

MCART is released under the **MIT License**. See the [LICENSE](https://github.com/TheXDS/MCART/blob/master/LICENSE) file for details.

## Documentation

- [Full API Documentation](https://thexds.github.io/MCART/)
- [GitHub Repository](https://github.com/TheXDS/MCART)
- [NuGet Package](https://www.nuget.org/packages/TheXDS.MCART/)

## See Also

MCART provides additional specialized libraries:

- **MCART.Coloring** – Color manipulation and parsing
- **MCART.Mvvm** – MVVM utilities for UI frameworks
- **MCART.Security** – Cryptography and password management
- **MCART.TypeFactory** – Advanced type instantiation
- **MCART.Windows** – Windows-specific utilities
- **Platform-Specific**: WPF, Avalonia, WinForms extensions

## Support

For issues, questions, or suggestions, please visit the [GitHub Issues](https://github.com/TheXDS/MCART/issues) page.
