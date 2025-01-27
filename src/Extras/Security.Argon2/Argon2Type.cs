/*
Argon2Type.cs

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

#pragma warning disable IDE0130

namespace TheXDS.MCART.Security;

/// <summary>
/// Enumerates the variants of the Argon2 algorithm that can be used to
/// derive keys.
/// </summary>
public enum Argon2Type : byte
{
    /// <summary>
    /// Argon2d algorithm. Provides resistance against GPU attacks.
    /// </summary>
    Argon2d,

    /// <summary>
    /// Argon2i algorithm. Provides resistance against Side-Channel attacks.
    /// </summary>
    Argon2i,

    /// <summary>
    /// Argon2id algorithm. Offers the advantages of both Argon2d and Argon2i
    /// simultaneously, at the cost of computational performance.
    /// </summary>
    Argon2id
}
