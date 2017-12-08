//
//  RandomExtensions.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
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
namespace MCART.Types.Extensions
{
    /// <summary>
    /// Extensiones para la clase <see cref="Random"/>
    /// </summary>
    public static class RandomExtensions
    {
        const string text = "0123456789" +
            "abcdefghijklmnopqrstuvwxyz" +
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        /// <summary>
        /// Obtiene una cadena de texto aleatorio.
        /// </summary>
        /// <param name="r">
        /// Instancia del objeto <see cref="Random"/> a utilizar.
        /// </param>
        /// <param name="length">Longitud de la cadena a generar.</param>
        /// <returns>
        /// Una cadena de texto aleatorio con la longitud especificada.
        /// </returns>
        public static string RndText(this Random r, uint length)
        {
            string x = string.Empty;
            while (x.Length < length) x += text[r.Next(0, text.Length)];
            return x;
        }
        /// <summary>
        /// Genera un nombre de archivo aleatorio que no se encuentre en uso en
        /// el directorio actual.
        /// </summary>
        /// <param name="r">
        /// Instancia del objeto <see cref="Random"/> a utilizar.
        /// </param>
        /// <param name="length">Longitud del nombre de archivo.</param>
        /// <returns>
        /// Un nombre de archivo aleatorio que no corresponde a ningún archivo
        /// en el directorio actual.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Se produce si <paramref name="length"/> es inferior a 0, o superior
        /// a el máximo admitido por el sistema operativo.
        /// </exception>
        public static string RndFileName(this Random r, byte length = 8)
        {            
            if (Environment.OSVersion.Platform==PlatformID.Win32NT && (length == 0 || length > 255))
                throw new ArgumentOutOfRangeException(nameof(length));
            int ac = 0;
            string x = string.Empty;
            do
            {
                ac++;
                if (ac > System.Math.Pow(text.Length, length)) throw new Exceptions.DirectoryIsFullException();
                x = RndText(r, length) + ".tmp";
            } while (System.IO.File.Exists(x));
            return x;
        }
    }
}
