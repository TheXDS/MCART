/*
StringExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Types.Extensions
{
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
        [Dangerous]
        public static string Read(this SecureString value)
        {
            var valuePtr = IntPtr.Zero;
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
            var outp = new List<short>();
            var valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                for (var i = 0; i < value.Length * sz; i += sz) outp.Add(Marshal.ReadInt16(valuePtr, i));
                return outp.ToArray();
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
            var outp = new List<char>();
            var valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                for (var i = 0; i < value.Length * sz; i += sz) outp.Add((char) Marshal.ReadInt16(valuePtr, i));
                return outp.ToArray();
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
        public static byte[] ReadBytes(this SecureString value)
        {
            var outp = new List<byte>();
            var valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                for (var i = 0; i < value.Length * 2; i++) outp.Add(Marshal.ReadByte(valuePtr, i));
                return outp.ToArray();
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
        public static string ToBase64(this SecureString value)
        {
            return Convert.ToBase64String(ReadBytes(value));
        }
    }
}