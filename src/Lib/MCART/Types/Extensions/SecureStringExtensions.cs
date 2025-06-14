/*
StringExtensions.cs

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

using System.Runtime.InteropServices;
using System.Security;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensions for the <see cref="SecureString" /> class.
/// </summary>
public static class SecureStringExtensions
{
    /// <summary>
    /// Converts a <see cref="SecureString" /> to a <see cref="string" />.
    /// </summary>
    /// <param name="value">
    /// The <see cref="SecureString" /> to convert.
    /// </param>
    /// <returns>A managed <see cref="string" />.</returns>
    /// <remarks>
    /// Using this method is NOT RECOMMENDED, as converting to a
    /// <see cref="string" /> defeats the original purpose of
    /// <see cref="SecureString" />. It's provided as a simple alternative
    /// in cases where the program doesn't rely on maintaining the
    /// confidentiality of a specific string during execution.
    /// </remarks>
    public static string Read(this SecureString value)
    {
        IntPtr valuePtr = IntPtr.Zero;
        try
        {
            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
            return Marshal.PtrToStringUni(valuePtr, value.Length);
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
        }
    }

    /// <summary>
    /// Converts a <see cref="SecureString" /> to an array of <see cref="short" />.
    /// </summary>
    /// <param name="value">
    /// The <see cref="SecureString" /> to convert.
    /// </param>
    /// <returns>A managed array of <see cref="short" />.</returns>
    public static short[] ReadInt16(this SecureString value)
    {
        const int sz = sizeof(short);
        List<short>? output = [];
        IntPtr valuePtr = IntPtr.Zero;
        try
        {
            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
            for (int i = 0; i < value.Length * sz; i += sz) output.Add(Marshal.ReadInt16(valuePtr, i));
            return [.. output];
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
        }
    }

    /// <summary>
    /// Converts a <see cref="SecureString" /> to an array of <see cref="char" />.
    /// </summary>
    /// <param name="value">
    /// The <see cref="SecureString" /> to convert.
    /// </param>
    /// <returns>A managed array of <see cref="char" />.</returns>
    public static char[] ReadChars(this SecureString value)
    {
        const int sz = sizeof(char);
        List<char>? output = [];
        IntPtr valuePtr = IntPtr.Zero;
        try
        {
            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
            for (int i = 0; i < value.Length * sz; i += sz) output.Add((char)Marshal.ReadInt16(valuePtr, i));
            return [.. output];
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
        }
    }

    /// <summary>
    /// Converts a <see cref="SecureString" /> to an array of <see cref="byte" />.
    /// </summary>
    /// <param name="value">
    /// The <see cref="SecureString" /> to convert.
    /// </param>
    /// <returns>A managed array of <see cref="byte" />.</returns>
    /// <remarks>
    /// The byte array read corresponds to a UTF-16 string.
    /// </remarks>
    public static byte[] ReadBytes(this SecureString value)
    {
        List<byte>? output = [];
        IntPtr valuePtr = IntPtr.Zero;
        try
        {
            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
            for (int i = 0; i < value.Length * 2; i++) output.Add(Marshal.ReadByte(valuePtr, i));
            return [.. output];
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
        }
    }

    /// <summary>
    /// Converts a <see cref="SecureString"/> to a Base64 formatted <see cref="string"/>.
    /// </summary>
    /// <param name="value">
    /// The <see cref="SecureString" /> to convert.
    /// </param>
    /// <returns>A Base64 formatted <see cref="string"/>.</returns>
    /// <remarks>
    /// The byte array read before conversion to Base64 corresponds to a UTF-16 string.
    /// </remarks>
    public static string ToBase64(this SecureString value)
    {
        return Convert.ToBase64String(ReadBytes(value));
    }
}
