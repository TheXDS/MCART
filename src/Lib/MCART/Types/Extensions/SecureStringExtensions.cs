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
/// Extensiones de la clase <see cref="SecureString" />.
/// </summary>
public static class SecureStringExtensions
{
    /// <summary>
    /// Convierte un <see cref="SecureString" /> en un
    /// <see cref="string" />.
    /// </summary>
    /// <param name="value">
    /// <see cref="SecureString" /> a convertir.
    /// </param>
    /// <returns>Un <see cref="string" /> de código administrado.</returns>
    /// <remarks>
    /// El uso de este método NO ESTÁ RECOMENDADO, ya que la conversión al
    /// tipo <see cref="string" /> vence el propósito original de
    /// <see cref="SecureString" />, y se provee como una
    /// alternativa sencilla, en casos en los que el programa no dependa de
    /// que la confidencialidad de una cadena en particular se deba
    /// mantener durante la ejecución.
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
    /// Convierte un <see cref="SecureString" /> en un
    /// arreglo de <see cref="short" />.
    /// </summary>
    /// <param name="value">
    /// <see cref="SecureString" /> a convertir.
    /// </param>
    /// <returns>
    /// Un arreglo de <see cref="short" /> de código administrado.
    /// </returns>
    public static short[] ReadInt16(this SecureString value)
    {
        const int sz = sizeof(short);
        List<short>? output = new();
        IntPtr valuePtr = IntPtr.Zero;
        try
        {
            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
            for (int i = 0; i < value.Length * sz; i += sz) output.Add(Marshal.ReadInt16(valuePtr, i));
            return output.ToArray();
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
        }
    }

    /// <summary>
    /// Convierte un <see cref="SecureString" /> en un
    /// arreglo de <see cref="char" />.
    /// </summary>
    /// <param name="value">
    /// <see cref="SecureString" /> a convertir.
    /// </param>
    /// <returns>
    /// Un arreglo de <see cref="char" /> de código administrado.
    /// </returns>
    public static char[] ReadChars(this SecureString value)
    {
        const int sz = sizeof(char);
        List<char>? output = new();
        IntPtr valuePtr = IntPtr.Zero;
        try
        {
            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
            for (int i = 0; i < value.Length * sz; i += sz) output.Add((char)Marshal.ReadInt16(valuePtr, i));
            return output.ToArray();
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
        }
    }

    /// <summary>
    /// Convierte un <see cref="SecureString" /> en un
    /// arreglo de <see cref="byte" />.
    /// </summary>
    /// <param name="value">
    /// <see cref="SecureString" /> a convertir.
    /// </param>
    /// <returns>
    /// Un arreglo de <see cref="byte" /> de código administrado.
    /// </returns>
    /// <remarks>
    /// El arreglo de bytes leídos corresponderá a una cadena UTF-16.
    /// </remarks>
    public static byte[] ReadBytes(this SecureString value)
    {
        List<byte>? output = new();
        IntPtr valuePtr = IntPtr.Zero;
        try
        {
            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
            for (int i = 0; i < value.Length * 2; i++) output.Add(Marshal.ReadByte(valuePtr, i));
            return output.ToArray();
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
        }
    }

    /// <summary>
    /// Convierte un <see cref="SecureString"/> en un valor de tipo
    /// <see cref="string"/> en formato base64.
    /// </summary>
    /// <see cref="SecureString" /> a convertir.
    /// <returns>
    /// Un valor de tipo <see cref="string"/> en formato base64.
    /// </returns>
    /// <remarks>
    /// El arreglo de bytes leídos previo a la conversión a Base64
    /// corresponderá a una cadena UTF-16.
    /// </remarks>
    public static string ToBase64(this SecureString value)
    {
        return Convert.ToBase64String(ReadBytes(value));
    }
}
