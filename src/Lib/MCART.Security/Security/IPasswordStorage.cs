/*
IPasswordStorage.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Security;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Security;

/// <summary>
/// Defines a set of members that a type must implement to generate
/// hashes from passwords that can be stored securely.
/// </summary>
public interface IPasswordStorage
{
    /// <summary>
    /// Gets the algorithm name.
    /// </summary>
    string AlgId => GetType().Name.ChopEndAny("PasswordStorage", "Storage").ToUpperInvariant();

    /// <summary>
    /// Obtains configuration from the specified block, advancing the
    /// reader by the number of bytes required by this instance's
    /// configuration.
    /// </summary>
    /// <param name="reader">
    /// Object from which configuration values are read.
    /// </param>
    void ConfigureFrom(BinaryReader reader);

    /// <summary>
    /// Writes configuration values in binary format.
    /// </summary>
    /// <returns>
    /// A byte array that can be used to reconstruct the key‑derivation
    /// configuration for this instance.
    /// </returns>
    byte[] DumpSettings();

    /// <summary>
    /// Generates a binary blob that can be stored in a database.
    /// </summary>
    /// <param name="input">
    /// Password from which to derive a key.
    /// </param>
    /// <returns>
    /// A byte array containing the key derived from the specified
    /// password.
    /// </returns>
    byte[] Generate(byte[] input);

    /// <summary>
    /// Generates a binary blob that can be stored in a database.
    /// </summary>
    /// <param name="input">
    /// Password from which to derive a key.
    /// </param>
    /// <returns>
    /// A byte array containing the key derived from the specified
    /// password.
    /// </returns>
    byte[] Generate(SecureString input) => Generate(System.Text.Encoding.UTF8.GetBytes(input.Read()));

    /// <summary>
    /// Gets the number of key bytes this instance will generate.
    /// </summary>
    int KeyLength { get; }

    /// <summary>
    /// Gets an object containing the algorithm configuration.
    /// </summary>
    public object? Settings => null;
}
