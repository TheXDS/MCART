//
//  Generators.cs
//
//  This file is part of Morgan's CLR Advanced Runtime (MCART)
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Linq;
using TheXDS.MCART.Types.Extensions;
using St = TheXDS.MCART.Resources.Strings;
using System.Security;

namespace TheXDS.MCART.Security.Password
{
    /// <summary>
    /// Contiene funciones que generan contraseñas.
    /// </summary>
	public static class Generators
    {
        /// <summary>
        /// Genera una contraseña.
        /// </summary>
        /// <param name="chars">Caracteres a utilizar.</param>
        /// <param name="l">Longitud de la contraseña.</param>
        /// <returns>
        /// Un <see cref="SecureString"/> con la contraseña generada.
        /// </returns>
        static SecureString GenPw(string chars, int l)
        {
            char[] x = chars.Shuffled().ToArray();
            var rnd = new Random();
            var retval = new SecureString();
            for (int j = 0; j < l; j++) retval.AppendChar(x[rnd.Next(0, x.Count())]);
            retval.MakeReadOnly();
            return retval;
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
        public static SecureString Safe(int length = 16) => GenPw(St.Chars, length);
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
        public static SecureString VeryComplex(int length = 128) => GenPw(St.MoreChars, length);
        /// <summary>
        /// Genera un número de pin.
        /// </summary>
        /// <returns>Un número de pin de la longitud especificada.</returns>
        /// <param name="length">
        /// Opcional. Longitud del número de pin a generar. Si se omite, se
        /// generará un número de pin de 4 dígitos.
        /// </param>
        public static SecureString Pin(int length = 4) => GenPw(St.Numbers, length);
        /// <summary>
        /// Genera una contraseña extremadamente segura, utilizando UTF-16
        /// </summary>
        /// <param name="length">
        /// Opcional. Longitud de la contraseña a generar. Si se omite, se
        /// generará una contraseña de 512 caracteres UTF-16.
        /// </param>
        /// <returns>
        /// Un <see cref="SecureString"/> con la contraseña generada.
        /// </returns>
        public static SecureString ExtremelyComplex(int length = 512)
        {
            var rnd = new Random();
            var retval = new SecureString();
            for (int j = 0; j < length; j++) retval.AppendChar((char)rnd.Next(0, 0x10000));
            return retval;
        }
    }
}