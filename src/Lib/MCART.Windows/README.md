# MCART.Windows

## Overview
MCART.Windows is a specialized library within the MCART ecosystem, designed to enhance the development of Windows-based applications. It provides a set of tools, utilities, and components tailored for the Windows platform, streamlining the creation of robust and feature-rich desktop applications.

This library is more often included as a transitive dependency in other MCART libraries, such as MCART.WPF, which is a wrapper around the Windows Presentation Foundation (WPF) framework. While it is not intended for direct use in most cases, it does include useful helpers for Windows-specific apps, and you don't have to reference a specific platform to use its features, like in cases where you need to implement a console application that uses Windows APIs and/or dialogs.

## Highlights
- **Windows-Specific Utilities**: Tools and helpers designed specifically for the Windows platform.
- **Extensibility**: Designed to be extended for custom Windows-specific solutions.
- **Access to WIndows-specific APIs**: Provides access to a few Windows-specific APIs and features, such as some of the common Windows dialogs (including the modern progress dialog as seen in Windows Vista and up), separate console, window management and more.

## Getting Started
```csharp
using TheXDS.MCART.Helpers;

class Program
{
    static void Main()
    {
        Console.WriteLine($"The system accent color is {Windows.GetAeroAccentColor()}");
    }
}
```