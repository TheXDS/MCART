//
//  Generators.cs
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
using System.Linq;
using MCART.Types.Extensions;
using St = MCART.Resources.Strings;

namespace MCART.Security.Password
{
    /// <summary>
    /// Contiene funciones que generan contraseñas.
    /// </summary>
	public static class Generators
    {
        /// <summary>
        /// Genera una contraseña.
        /// </summary>
        /// <returns>La contraseña.</returns>
        /// <param name="chars">Caracteres a utilizar.</param>
        /// <param name="l">Longitud de la contraseña.</param>
        static string GenPw(string chars, int l)
        {
            char[] x = chars.Shuffled().ToArray();
            string outp = string.Empty;
            Random r = new Random();
            while (outp.Length < l) outp += x[r.Next(0, x.Count())];
            return outp;
        }
        /// <summary>
        /// Genera una contraseña segura.
        /// </summary>
        /// <returns>
        /// Una contraseña que incluye mayúsculas, minúsculas, números y
        /// símbolos, todos disponibles en el teclado inglés.
        /// </returns>
        /// <param name="length">
        /// Opcional. Longitud de la contraseña a generar. Si se omite, se
        /// generará una contraseña de 16 caracteres.
        /// </param>
        public static string Safe(int length = 16) => GenPw(St.Chars, length);
        /// <summary>
        /// Genera una contraseña muy compleja.
        /// </summary>
        /// <returns>
        /// Una contraseña que incluye mayúsculas, minúsculas, números y
        /// símbolos, todos disponibles en el teclado inglés internacional.
        /// </returns>
        /// <param name="length">
        /// Opcional. Longitud de la contraseña a generar. Si se omite, se
        /// generará una contraseña de 128 caracteres.
        /// </param>
        public static string VeryComplex(int length = 128) => GenPw(St.MoreChars, length);
        /// <summary>
        /// Genera un número de pin.
        /// </summary>
        /// <returns>Un número de pin de la longitud especificada.</returns>
        /// <param name="length">
        /// Opcional. Longitud del número de pin a generar. Si se omite, se
        /// generará un número de pin de 4 dígitos.
        /// </param>
        public static string Pin(int length = 4) => GenPw(St.Numbers, length);
    }
}