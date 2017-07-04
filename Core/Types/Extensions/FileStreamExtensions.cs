//
//  FileStreamExtensions.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using MCART.Attributes;
namespace MCART.Types.Extensions
{
    /// <summary>
    /// Extensiones de la clase <see cref="FileStream"/>.
    /// </summary>
    public static class FileStreamExtensions
    {
        /// <summary>
        /// Destruye el contenido del <see cref="FileStream"/>.
        /// </summary>
        /// <param name="fs"><see cref="FileStream"/> del cual este método es
        /// una extensión.</param>
        [Unsafe]
        public static void Destroy(this FileStream fs)
        {
            try { fs.SetLength(0); }
            catch { throw; }
        }
        /// <summary>
        /// Salta la cantidad especificada de bytes en el archivo desde la
        /// posición actual.
        /// </summary>
        /// <param name="fs"><see cref="FileStream"/> del cual este método es
        /// una extensión.</param>
        /// <param name="bytesToSkip">Bytes a saltar.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// Se produce si <paramref name="bytesToSkip"/> es menor a cero, o si
        /// se extiende fuera del archivo.
        /// </exception>
        [Thunk]
        public static void Skip(this FileStream fs, int bytesToSkip)
        {
            if (bytesToSkip < 0 || (fs.Position + bytesToSkip) > fs.Length)
                throw new ArgumentOutOfRangeException(nameof(bytesToSkip));
            try { fs.Seek(bytesToSkip, SeekOrigin.Current); }
            catch { throw; }
        }
        /// <summary>
        /// Lee una cadena desde el archivo y avanza la posición de lectura
        /// hasta después del último carácter leído.
        /// </summary>
        /// <returns>La cadena que ha sido leída.</returns>
        /// <param name="fs"><see cref="FileStream"/> del cual este método es
        /// una extensión.</param>
        /// <param name="count">Cantidad de caracteres a leer.</param>
        /// <param name="encoding"><see cref="Encoding"/> a utilizar.</param>
        public static string ReadString(this FileStream fs, int count, Encoding encoding)
        {
            string outp = string.Empty;
            BinaryReader br = new BinaryReader(fs, encoding);
            try
            {
                while (outp.Length < count) outp += br.ReadChar();
                return outp;
            }
            catch { throw; }
        }
        /// <summary>
        /// Lee una cadena desde el archivo y avanza la posición de lectura
        /// hasta después del último carácter Unicode leído.
        /// </summary>
        /// <returns>La cadena que ha sido leída.</returns>
        /// <param name="fs"><see cref="FileStream"/> del cual este método es
        /// una extensión.</param>
        /// <param name="count">Cantidad de caracteres a leer.</param>
        [Thunk]
        public static string ReadString(this FileStream fs, int count)
        {
            try { return ReadString(fs, count, Encoding.Unicode); }
            catch { throw; }
        }
        /// <summary>
        /// Lee asíncronamente una cadena desde el archivo y avanza la posición
        /// de lectura hasta después del último carácter Unicode leído.
        /// </summary>
        /// <returns>La cadena que ha sido leída.</returns>
        /// <param name="fs"><see cref="FileStream"/> del cual este método es
        /// una extensión.</param>
        /// <param name="count">Cantidad de caracteres a leer.</param>
        [Thunk]
        public static async Task<string> ReadStringAsync(this FileStream fs, int count)
        {
            try { return await Task.Run(() => ReadString(fs, count)); }
            catch { throw; }
        }
        /// <summary>
        /// Lee asíncronamente una cadena desde el archivo y avanza la posición
        /// de lectura hasta después del último carácter leído.
        /// </summary>
        /// <returns>La cadena que ha sido leída.</returns>
        /// <param name="fs">
        /// <see cref="FileStream"/> del cual este método es una extensión.
        /// </param>
        /// <param name="count">Cantidad de caracteres a leer.</param>
        /// <param name="encoding"><see cref="Encoding"/> a utilizar.</param>
        [Thunk]
        public static async Task<string> ReadStringAsync(this FileStream fs, int count, Encoding encoding)
        {
            try { return await Task.Run(() => ReadString(fs, count, encoding)); }
            catch { throw; }
        }
        /// <summary>
        /// Lee asíncronamente una cadena desde la posición actual hasta el
        /// final del archivo.
        /// </summary>
        /// <param name="fs">
        /// <see cref="FileStream"/> del cual este método es una extensión.
        /// </param>
        /// <returns>La cadena que ha sido leída.</returns>
        [Thunk]
        public static async Task<string> ReadStringToEndAsync(this FileStream fs)
        {
            try { return await ReadStringToAsync(fs, fs.Length); }
            catch { throw; }
        }
        /// <summary>
        /// Lee asíncronamente una cadena desde la posición actual hasta 
        /// alcanzar la posición especificada.
        /// </summary>
        /// <param name="fs">
        /// <see cref="FileStream"/> del cual este método es una extensión.
        /// </param>
        /// <param name="pos">
        /// Posición hasta la cual se leerá la cadena.
        /// </param>
        /// <returns>La cadena que ha sido leída.</returns>
        public static async Task<string> ReadStringToAsync(this FileStream fs, long pos)
        {
            try
            {
                if (pos < fs.Position)
                {
                    long x = pos;
                    pos = fs.Position;
                    fs.Position = x;
                }
                byte[] bf = new byte[pos - fs.Position];
                await fs.ReadAsync(bf, 0, (int)(pos - fs.Position));
                return Encoding.UTF8.GetString(bf);
            }
            catch { throw; }
        }
        /// <summary>
        /// Escribe un conjunto de objetos <see cref="byte"/> en el 
        /// <see cref="FileStream"/>.
        /// </summary>
        /// <param name="fs">
        /// <see cref="FileStream"/> del cual este método es una extensión.
        /// </param>
        /// <param name="bytes">Colección de objetos <see cref="byte"/> a
        /// escribir en el <see cref="FileStream"/>.</param>
        [Thunk]
        public static void WriteBytes(this FileStream fs, params byte[] bytes)
        {
            try { fs.Write(bytes, 0, bytes.Length); }
            catch { throw; }
        }
        /// <summary>
        /// Escribe un conjunto de colecciones <see cref="byte"/> en el 
        /// <see cref="FileStream"/>.
        /// </summary>
        /// <param name="fs">
        /// <see cref="FileStream"/> del cual este método es una extensión.
        /// </param>
        /// <param name="bytes">Colecciones de <see cref="byte"/> a escribir en
        /// el <see cref="FileStream"/>.</param>
        [Thunk]
        public static void WriteSeveralBytes(this FileStream fs, params byte[][] bytes)
        {
            try { foreach (byte[] x in bytes) { fs.Write(x, 0, x.Length); } }
            catch { throw; }
        }
    }
}