/*
Credential.cs

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

using System.Security;

namespace TheXDS.MCART.Security.Password
{
    /// <summary>
    /// Estructura básica que representa una credencial de inicio de
    /// sesión.
    /// </summary>
    public struct Credential : ICredential
    {
        /// <summary>
        /// Crea una instancia de la estructura <see cref="Credential"/> a
        /// partir del objeto <see cref="ICredential"/> especificado.
        /// </summary>
        /// <param name="origin">
        /// <see cref="ICredential"/> a partir del cual crear el nuevo
        /// <see cref="Credential"/>.
        /// </param>
        /// <returns>
        /// Una nueva instancia de la clase <see cref="Credential"/> con los
        /// datos de credencial obtenidos a partir de
        /// <paramref name="origin"/>.
        /// </returns>
        public static Credential From(ICredential origin)
        {
            return new Credential(origin.Username, origin.Password);
        }

        /// <summary>
        /// Obtiene una credencial vacía. Este campo es de solo lectura.
        /// </summary>
        public static readonly Credential Null = new();

        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="Credential"/>.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <param name="password">Contraseña.</param>
        public Credential(string? username, SecureString password)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Obtiene el nombre de usuario de este <see cref="ICredential" />
        /// </summary>
        public string? Username { get; }

        /// <summary>
        /// Obtiene la contraseña asociada a este <see cref="ICredential" />.
        /// </summary>
        public SecureString Password { get; }
    }
}