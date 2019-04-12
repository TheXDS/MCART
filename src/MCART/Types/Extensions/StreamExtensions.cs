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

using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Extensiones de la clase <see cref="Stream"/>.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Destruye el contenido del <see cref="Stream"/>.
        /// </summary>
        /// <param name="fs">
        /// <see cref="Stream"/> a destruir.
        /// </param>
        [Dangerous] [Sugar] public static void Destroy(this Stream fs) => fs.SetLength(0);
        /// <summary>
        /// Salta la cantidad especificada de bytes en la secuencia desde la
        /// posición actual.
        /// </summary>
        /// <param name="fs"><see cref="Stream"/> del cual este método es
        /// una extensión.</param>
        /// <param name="bytesToSkip">Bytes a saltar.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Se produce si <paramref name="bytesToSkip"/> es menor a cero, o si
        /// se extiende fuera de la secuencia.
        /// </exception>
        [Sugar]
        public static void Skip(this Stream fs, int bytesToSkip)
        {
            if (bytesToSkip < 0 || (fs.Position + bytesToSkip) > fs.Length)
                throw new ArgumentOutOfRangeException(nameof(bytesToSkip));
            fs.Seek(bytesToSkip, SeekOrigin.Current);
        }
        /// <summary>
        /// Lee una cadena desde la secuencia y avanza la posición de lectura
        /// hasta después del último carácter Unicode leído.
        /// </summary>
        /// <returns>La cadena que ha sido leída.</returns>
        /// <param name="fs"><see cref="Stream"/> del cual este método es
        /// una extensión.</param>
        /// <param name="count">Cantidad de caracteres a leer.</param>
        [Sugar]
        public static string ReadString(this Stream fs, int count) => ReadString(fs, count, Encoding.Unicode);
        /// <summary>
        /// Lee una cadena desde la secuencia y avanza la posición de lectura
        /// hasta después del último carácter leído.
        /// </summary>
        /// <returns>La cadena que ha sido leída.</returns>
        /// <param name="fs"><see cref="Stream"/> del cual este método es
        /// una extensión.</param>
        /// <param name="count">Cantidad de caracteres a leer.</param>
        /// <param name="encoding"><see cref="Encoding"/> a utilizar.</param>
        public static string ReadString(this Stream fs, int count, Encoding encoding)
        {
            var retVal = new ExtendedList<char>();
            var br = new BinaryReader(fs, encoding);
            while (retVal.Count < count) retVal.Add(br.ReadChar());
            return new string(retVal.ToArray());
        }
        /// <summary>
        /// Obtiene la cantidad de bytes restantes desde la posición actual.
        /// </summary>
        /// <param name="fs"><see cref="Stream"/> del cual este método es
        /// una extensión.</param>
        /// <returns>
        /// La cantidad de bytes restantes desde la posición actual.
        /// </returns>
        [Sugar] public static long RemainingBytes(this Stream fs) => fs.Length - fs.Position;
        /// <summary>
        /// Lee asíncronamente una cadena desde la secuencia y avanza la posición
        /// de lectura hasta después del último carácter Unicode leído.
        /// </summary>
        /// <returns>La cadena que ha sido leída.</returns>
        /// <param name="fs"><see cref="Stream"/> del cual este método es
        /// una extensión.</param>
        /// <param name="count">Cantidad de caracteres a leer.</param>
        [Sugar] public static async Task<string> ReadStringAsync(this Stream fs, int count) => await Task.Run(() => ReadString(fs, count));
        /// <summary>
        /// Lee asíncronamente una cadena desde la secuencia y avanza la posición
        /// de lectura hasta después del último carácter leído.
        /// </summary>
        /// <returns>La cadena que ha sido leída.</returns>
        /// <param name="fs">
        /// <see cref="Stream"/> del cual este método es una extensión.
        /// </param>
        /// <param name="count">Cantidad de caracteres a leer.</param>
        /// <param name="encoding"><see cref="Encoding"/> a utilizar.</param>
        public static async Task<string> ReadStringAsync(this Stream fs, int count, Encoding encoding) => await Task.Run(() => ReadString(fs, count, encoding));
        /// <summary>
        /// Lee asíncronamente una cadena desde la posición actual hasta el
        /// final de la secuencia.
        /// </summary>
        /// <param name="fs">
        /// <see cref="Stream"/> del cual este método es una extensión.
        /// </param>
        /// <returns>La cadena que ha sido leída.</returns>
        [Sugar] public static async Task<string> ReadStringToEndAsync(this Stream fs) => await ReadStringToAsync(fs, fs.Length);
        /// <summary>
        /// Lee asíncronamente una cadena desde la posición actual hasta 
        /// alcanzar la posición especificada.
        /// </summary>
        /// <param name="fs">
        /// <see cref="Stream"/> del cual este método es una extensión.
        /// </param>
        /// <param name="pos">
        /// Posición hasta la cual se leerá la cadena.
        /// </param>
        /// <returns>La cadena que ha sido leída.</returns>
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
        /// Escribe un conjunto de objetos <see cref="byte"/> en el 
        /// <see cref="Stream"/>.
        /// </summary>
        /// <param name="fs">
        /// <see cref="Stream"/> del cual este método es una extensión.
        /// </param>
        /// <param name="bytes">Colección de objetos <see cref="byte"/> a
        /// escribir en el <see cref="Stream"/>.</param>
        [Sugar] public static void WriteBytes(this Stream fs, params byte[] bytes) => fs.Write(bytes, 0, bytes.Length);
        /// <summary>
        /// Escribe un conjunto de colecciones <see cref="byte"/> en el 
        /// <see cref="Stream"/>.
        /// </summary>
        /// <param name="fs">
        /// <see cref="Stream"/> del cual este método es una extensión.
        /// </param>
        /// <param name="bytes">Colecciones de <see cref="byte"/> a escribir en
        /// el <see cref="Stream"/>.</param>
        public static void WriteSeveralBytes(this Stream fs, params byte[][] bytes)
        {
            foreach (var x in bytes) fs.Write(x, 0, x.Length);
        }
    }
}