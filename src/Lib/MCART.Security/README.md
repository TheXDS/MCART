# MCART.Security

## Overview
MCART.Security is a specialized library within the MCART ecosystem, designed to provide robust and efficient security-related utilities for .NET applications. Its primary goal is to simplify the implementation of common security patterns, cryptographic operations, and secure data handling, ensuring developers can focus on building secure applications without reinventing the wheel.

## Highlights
- **Encryption and Decryption**: Provides easy-to-use APIs for symmetric and asymmetric cryptography.
- **Hashing Utilities**: Includes support for generating and verifying cryptographic hashes.
- **Secure Data Storage**: Tools for securely storing sensitive information.
- **Authentication Helpers**: Simplifies the implementation of authentication mechanisms.
- **Extensibility**: Designed to be extended for custom security needs.
- **Cross-Platform**: Fully compatible with Windows, macOS, and Linux.

## Getting Started
Here’s a simple example demonstrating how to use MCART.Security to hash and verify a password:


```csharp
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.Security;

class Program
{
    static void Main()
    {
        string password = "SecurePassword123!".ToSecureString();
        
        // Hash the password
        byte[] hashedPassword = PasswordStorage.CreateHash<Pbkdf2Storage>(password);
        Console.WriteLine($"Hashed Password: {hashedPassword}");
        
        // Verify the password
        bool isValid = PasswordStorage.VerifyPassword(password, hashedPassword);
        Console.WriteLine($"Password is valid: {isValid}");
    }
}

```

This example showcases how MCART.Security simplifies secure password handling, ensuring best practices are followed with minimal effort.