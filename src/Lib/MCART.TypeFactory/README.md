# MCART.TypeFactory

## Overview
MCART.TypeFactory is a dynamic type generation library designed to simplify the creation and manipulation of types at runtime. It provides developers with a powerful and intuitive API to define classes, properties, methods, and more, enabling scenarios such as dynamic proxies, runtime code generation, and advanced reflection-based operations.

This library is ideal for developers who need to create flexible and dynamic solutions without the overhead of precompiled types, making it a valuable tool for frameworks, testing utilities, and runtime extensibility.

## Highlights
- **Dynamic Type Creation**: Easily define and instantiate new types at runtime.
- **Property and Method Generation**: Add properties, methods, and constructors dynamically.
- **Computed Properties**: Support for properties with custom logic.
- **Dynamic Assemblies**: Exposes dynamically generated assemblies for advanced use cases.
- **Extensibility**: Designed to integrate seamlessly with other MCART libraries and custom solutions.

## Getting Started
Here's a simple example demonstrating how to use MCART.TypeFactory to create a dynamic class with properties and methods:


```csharp
using TheXDS.MCART.Types;

class Program
{
    static void Main()
    {
        // Create a new TypeFactory instance
        var factory = new TypeFactory("DynamicNamespace");

        // Define a new class
        var typeBuilder = factory.NewClass("DynamicClass");

        // Add a property
        var nameProperty = typeBuilder.AddAutoProperty<string>("Name");

        // Add a computed property
        typeBuilder.AddComputedProperty<string>("Greeting", il =>
            il.LoadConstant("Hello, ")
              .LoadProperty(nameProperty)
              .Call<Func<string?, string?, string>>(string.Concat)
              .Return());

        // Create an instance of the dynamic class
        dynamic instance = typeBuilder.New();
        instance.Name = "World";

        // Access the computed property
        Console.WriteLine(instance.Greeting); // Output: Hello, World
    }
}

```

This example demonstrates how MCART.TypeFactory simplifies the creation of dynamic types, enabling powerful runtime capabilities with minimal effort.