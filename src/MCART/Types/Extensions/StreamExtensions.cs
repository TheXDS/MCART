/*
StreamExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#nullable enable

using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using TheXDS.MCART.Attributes;
using System.Diagnostics;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones de la clase <see cref="Stream"/>.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        ///     Destruye el contenido del <see cref="Stream"/>.
        /// </summary>
        /// <param name="fs">
        ///     <see cref="Stream"/> a destruir.
        /// </param>
        [Dangerous, DebuggerStepThrough, Sugar] public static void Destroy(this Stream fs) => fs.SetLength(0);

        /// <summary>
        ///     Salta la cantidad especificada de bytes en la secuencia desde
        ///     la posición actual.
        /// </summary>
        /// <param name="fs">
        ///     <see cref="Stream"/> en el cual se saltará el cursor de
        ///     lectura.
        /// </param>
        /// <param name="bytesToSkip">Bytes a saltar.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Se produce si <paramref name="bytesToSkip"/> es menor a cero, o
        ///     si al saltar, el cursor se extiende fuera de la secuencia.
        /// </exception>
        [DebuggerStepThrough, Sugar]
        public static void Skip(this Stream fs, int bytesToSkip)
        {
            if (bytesToSkip < 0 || (fs.Position + bytesToSkip) > fs.Length)
                throw new ArgumentOutOfRangeException(nameof(bytesToSkip));
            fs.Seek(bytesToSkip, SeekOrigin.Current);
        }

        /// <summary>
        ///     Lee una cadena desde la secuencia y avanza la posición de
        ///     lectura hasta después del último carácter Unicode leído.
        /// </summary>
        /// <returns>La cadena que ha sido leída.</returns>
        /// <param name="fs">
        ///     <see cref="Stream"/> desde el cual leer la cadena.
        /// </param>
        /// <param name="count">Cantidad de caracteres a leer.</param>
        [DebuggerStepThrough, Sugar]
        public static string ReadString(this Stream fs, int count) => ReadString(fs, count, Encoding.Unicode);

        /// <summary>
        ///     Lee una cadena desde la secuencia y avanza la posición de
        ///     lectura hasta después del último carácter leído.
        /// </summary>
        /// <returns>La cadena que ha sido leída.</returns>
        /// <param name="fs">
        ///     <see cref="Stream"/> desde el cual leer la cadena.
        /// </param>        
        /// <param name="count">Cantidad de caracteres a leer.</param>
        /// <param name="encoding"><see cref="Encoding"/> a utilizar.</param>
        [DebuggerStepThrough]
        public static string ReadString(this Stream fs, int count, Encoding encoding)
        {
            var retVal = new ExtendedList<char>();
            using var br = new BinaryReader(fs, encoding);
            while (retVal.Count < count) retVal.Add(br.ReadChar());
            return new string(retVal.ToArray());
        }

        /// <summary>
        ///     Obtiene la cantidad de bytes restantes desde la posición
        ///     actual.
        /// </summary>
        /// <param name="fs">
        ///     <see cref="Stream"/> del cual se obtendrán los bytes restantes.
        /// </param>
        /// <returns>
        ///     La cantidad de bytes restantes desde la posición actual.
        /// </returns>
        [DebuggerStepThrough, Sugar] public static long RemainingBytes(this Stream fs) => fs.Length - fs.Position;

        /// <summary>
        ///     Lee asíncronamente una cadena desde la secuencia y avanza la
        ///     posición de lectura hasta después del último carácter Unicode
        ///     leído.
        /// </summary>
        /// <returns>La cadena que ha sido leída.</returns>
        /// <param name="fs">
        ///     <see cref="Stream"/> desde el cual leer la cadena.
        /// </param>        
        /// <param name="count">Cantidad de caracteres a leer.</param>
        [DebuggerStepThrough, Sugar] public static Task<string> ReadStringAsync(this Stream fs, int count) => Task.Run(() => ReadString(fs, count));

        /// <summary>
        ///     Lee asíncronamente una cadena desde la secuencia y avanza la
        ///     posición de lectura hasta después del último carácter leído.
        /// </summary>
        /// <returns>La cadena que ha sido leída.</returns>
        /// <param name="fs">
        ///     <see cref="Stream"/> desde el cual leer la cadena.
        /// </param>        
        /// <param name="count">Cantidad de caracteres a leer.</param>
        /// <param name="encoding"><see cref="Encoding"/> a utilizar.</param>
        [DebuggerStepThrough]
        public static Task<string> ReadStringAsync(this Stream fs, int count, Encoding encoding) => Task.Run(() => ReadString(fs, count, encoding));

        /// <summary>
        ///     Lee asíncronamente una cadena desde la posición actual hasta el
        ///     final de la secuencia.
        /// </summary>
        /// <param name="fs">
        ///     <see cref="Stream"/> desde el cual leer la cadena.
        /// </param>        
        /// <returns>La cadena que ha sido leída.</returns>
        [DebuggerStepThrough, Sugar] public static Task<string> ReadStringToEndAsync(this Stream fs) => ReadStringToAsync(fs, fs.Length);

        /// <summary>
        ///     Lee asíncronamente una cadena desde la posición actual hasta 
        ///     alcanzar la posición especificada.
        /// </summary>
        /// <param name="fs">
        ///     <see cref="Stream"/> desde el cual leer la cadena.
        /// </param>        
        /// <param name="pos">
        ///     Posición hasta la cual se leerá la cadena.
        /// </param>
        /// <returns>La cadena que ha sido leída.</returns>
        [DebuggerStepThrough]
        public static async Task<string> ReadStringToAsync(this Stream fs, long pos)
        {
            if (pos < fs.Position)
            {
                var x = pos;
                pos = fs.Position;
                fs.Position = x;
            }
            var bf = new byte[pos - fs.Position];
            await fs.ReadAsync(bf, 0, (int)(pos - fs.Position));
            return Encoding.UTF8.GetString(bf);
        }

        /// <summary>
        ///     Escribe un conjunto de <see cref="byte"/> en el 
        ///     <see cref="Stream"/>.
        /// </summary>
        /// <param name="fs">
        ///     <see cref="Stream"/> sobre el cual se escribirán los bytes.
        /// </param>
        /// <param name="bytes">
        ///     Colección de <see cref="byte"/> a escribir en el
        ///     <see cref="Stream"/>.
        /// </param>
        [DebuggerStepThrough, Sugar] public static void WriteBytes(this Stream fs, params byte[] bytes) => fs.Write(bytes, 0, bytes.Length);

        /// <summary>
        ///     Escribe un conjunto de secuencias de <see cref="byte"/> en el 
        ///     <see cref="Stream"/>.
        /// </summary>
        /// <param name="fs">
        ///     <see cref="Stream"/> sobre el cual se escribirán los bytes.
        /// </param>
        /// <param name="bytes">
        ///     Colecciones de <see cref="byte"/> a escribir en el
        ///     <see cref="Stream"/>.
        /// </param>
        [DebuggerStepThrough]
        public static void WriteSeveralBytes(this Stream fs, params byte[][] bytes)
        {
            foreach (var x in bytes) fs.Write(x, 0, x.Length);
        }
    }
}