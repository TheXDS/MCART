/*
Argon2Settings.cs

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
/// Contains configuration values to be used for deriving passwords
/// using the Argon2 algorithm.
/// </summary>
/// <param name="Salt">Block of salt to be used for deriving the key.</param>
/// <param name="Iterations">
/// Argon2 iterations to be executed when deriving the key.
/// </param>
/// <param name="KbMemSize">
/// Amount of memory (in KB) to be used for deriving the key.
/// </param>
/// <param name="Parallelism">
/// Number of threads to be used when deriving a key.
/// </param>
/// <param name="Type">Variant of the Argon2 algorithm to be used.</param>
/// <param name="KeyLength">Number of bytes to derive.</param>
public readonly record struct Argon2Settings(
    byte[] Salt,
    int Iterations,
    int KbMemSize,
    short Parallelism,
    Argon2Type Type,
    int KeyLength);
