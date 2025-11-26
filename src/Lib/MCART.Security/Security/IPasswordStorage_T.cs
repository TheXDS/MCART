/*
IPasswordStorage_T.cs

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

namespace TheXDS.MCART.Security;

/// <summary>
/// Defines a set of members to be implemented by a type that implements
/// <see cref="IPasswordStorage"/> and that exposes configuration data for
/// a key‑derivation algorithm.
/// </summary>
/// <typeparam name="T">
/// The type that holds the configuration values.  It is *recommended* to
/// use <see langword="record"/> or <see langword="record"/>
/// <see langword="struct"/> types with immutable
/// properties for safety.
/// </typeparam>
public interface IPasswordStorage<T> : IPasswordStorage where T : struct
{
    /// <summary>
    /// Gets a reference to the active configuration of this instance.
    /// </summary>
    new T Settings { get; set; }

    object IPasswordStorage.Settings => Settings;

    /// <summary>
    /// Configure this instance from a block of data that represents the
    /// serialized configuration.
    /// </summary>
    /// <param name="data">
    /// A read‑only span of bytes that contains the configuration block.
    /// </param>
    void ConfigureFrom(ReadOnlySpan<byte> data)
    {
        using var ms = new MemoryStream(data.ToArray());
        using var reader = new BinaryReader(ms);
        ConfigureFrom(reader);
    }
}
