/*
CryptoPasswordGenerator.cs

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
using System.Security;
using System.Security.Cryptography;

namespace TheXDS.MCART.Security.Password
{
    /// <inheritdoc />
    /// <summary>
    /// Generador de contraseñas seguras que utiliza todos los caracteres
    /// de UTF-16, tanto caracteres definidos como no definidos.
    /// </summary>
    public sealed class CryptoPasswordGenerator : IPasswordGenerator
    {
        /// <inheritdoc />
        /// <summary>
        /// Genera una contraseña utilizando este generador.
        /// </summary>
        /// <returns>
        /// Una contraseña generada por este
        /// <see cref="T:TheXDS.MCART.Security.Password.IPasswordGenerator" />.
        /// </returns>
        public SecureString Generate()
        {
            return Generate(512);
        }

        /// <inheritdoc />
        /// <summary>
        /// Genera una contraseña utilizando este generador.
        /// </summary>
        /// <paramref name="length">
        /// Longitud deseada de la contraseña a generar.
        /// </paramref>
        /// <returns>
        /// Una contraseña generada por este
        /// <see cref="T:TheXDS.MCART.Security.Password.IPasswordGenerator" />.
        /// </returns>
        public SecureString Generate(int length)
        {
            var s = new SecureString();
            var b = new byte[length * 2];
            using (var r = new RNGCryptoServiceProvider()) r.GetBytes(b);
            for (var j = 0; j < length; j += 2)
                s.AppendChar(BitConverter.ToChar(b, j));
            s.MakeReadOnly();
            return s;
        }

        /// <summary>
        /// Convierte implícitamente un <see cref="CryptoPasswordGenerator"/> en
        /// un <see cref="SecureString"/>.
        /// </summary>
        /// <param name="pwGen">Generador de contraseñas a convertir.</param>
        public static implicit operator SecureString(CryptoPasswordGenerator pwGen)
        {
            return pwGen.Generate();
        }

        /// <summary>
        /// Convierte implícitamente un <see cref="CryptoPasswordGenerator"/> en
        /// un <see cref="SecureString"/>.
        /// </summary>
        /// <param name="pwGen">Generador de contraseñas a convertir.</param>
        public static implicit operator Func<int, SecureString>(CryptoPasswordGenerator pwGen)
        {
            return pwGen.Generate;
        }
    }
}