/*
PasswordGenerator.cs

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
using System.Linq;
using System.Security;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Security.Password
{
    /// <summary>
    /// Clase base para un generador de contraseña de caracteres aleatorios.
    /// </summary>
    public class PasswordGenerator : IPasswordGenerator
    {
        private readonly string _chars;
        private readonly int _length;

        /// <summary>
        /// Genera una contraseña aleatoria utilizando los parámetros
        /// especificados.
        /// </summary>
        /// <param name="validChars">
        /// Caracteres a incluir en la contraseña.
        /// </param>
        /// <param name="length">Longitud deseada de la contraseña.</param>
        /// <returns>
        /// Un <see cref="SecureString"/> que contiene la contraseña
        /// generada.
        /// </returns>
        public static SecureString GenerateRandomPassword(string validChars, int length)
        {
            char[]? x = validChars.Shuffled().ToArray();
            Random? rnd = new();
            SecureString? retval = new();
            for (int j = 0; j < length; j++) retval.AppendChar(x[rnd.Next(0, x.Length)]);
            retval.MakeReadOnly();
            return retval;
        }

        /// <summary>
        /// Genera una contraseña utilizando este generador.
        /// </summary>
        /// <returns>
        /// Una contraseña generada por este
        /// <see cref="IPasswordGenerator"/>.
        /// </returns>
        public SecureString Generate()
        {
            return GenerateRandomPassword(_chars, _length);
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PasswordGenerator"/>.
        /// </summary>
        /// <param name="chars">Caracteres a incluir en la contraseña generada.</param>
        /// <param name="length">Longitud deseada de la contraseña generada.</param>
        public PasswordGenerator(string chars, int length)
        {
            _chars = chars;
            _length = length;
        }

        /// <summary>
        /// Genera una contraseña utilizando este generador.
        /// </summary>
        /// <paramref name="length">
        /// Longitud deseada de la contraseña a generar.
        /// </paramref>
        /// <returns>
        /// Una contraseña generada por este
        /// <see cref="IPasswordGenerator" />.
        /// </returns>
        public SecureString Generate(int length)
        {
            return GenerateRandomPassword(_chars, length);
        }

        /// <summary>
        /// Convierte implícitamente un <see cref="PasswordGenerator"/> en
        /// un <see cref="SecureString"/>.
        /// </summary>
        /// <param name="pwGen">Generador de contraseñas a convertir.</param>
        public static implicit operator SecureString(PasswordGenerator pwGen)
        {
            return pwGen.Generate();
        }

        /// <summary>
        /// Convierte implícitamente un <see cref="PasswordGenerator"/> en
        /// un <see cref="SecureString"/>.
        /// </summary>
        /// <param name="pwGen">Generador de contraseñas a convertir.</param>
        public static implicit operator Func<int, SecureString>(PasswordGenerator pwGen)
        {
            return pwGen.Generate;
        }
    }
}