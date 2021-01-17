/*
UserData.cs

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
using System.Security;

namespace TheXDS.MCART.Security.Password
{
    /// <summary>
    /// Contiene información de creación de una credencial de seguridad,
    /// junto con datos sobre un indicio de contraseña y calidad evaluada
    /// de la misma.
    /// </summary>
    public struct UserData : ICredential
    {
        /// <summary>
        /// Obtiene una credencial vacía. Este campo es de solo lectura.
        /// </summary>
        public static readonly UserData Null = new UserData();

        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="UserData" />.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <param name="password">Contraseña.</param>
        public UserData(string username, SecureString password) : this(username, password, null, null)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="UserData" />.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <param name="password">Contraseña.</param>
        /// <param name="hint">Indicio de contraseña.</param>
        public UserData(string username, SecureString password, string hint) : this(username, password, hint, null)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="UserData"/>.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <param name="password">Contraseña.</param>
        /// <param name="quality">
        /// Calidad evaluada de la contraseña, expresado como un valor
        /// porcentual entre 0.0f y 1.0f.
        /// </param>
        public UserData(string username, SecureString password, float? quality) : this(username, password, null, quality)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la estructura
        /// <see cref="UserData"/>.
        /// </summary>
        /// <param name="username">Nombre de usuario.</param>
        /// <param name="password">Contraseña.</param>
        /// <param name="hint">Indicio de contraseña.</param>
        /// <param name="quality">
        /// Calidad evaluada de la contraseña, expresado como un valor
        /// porcentual entre 0.0f y 100.0f.
        /// </param>
        public UserData(string username, SecureString password, string? hint, float? quality)
        {
            if (quality.HasValue && !quality.Value.IsBetween(0f,100f))
                throw new ArgumentOutOfRangeException(nameof(quality));
            if (!password.IsReadOnly()) password.MakeReadOnly();
            Username = username;
            Password = password;
            Hint = hint;
            Quality = quality;
        }

        /// <summary>
        /// Obtiene el nombre de usuario de este <see cref="ICredential" />.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Obtiene la contraseña asociada a este <see cref="ICredential" />.
        /// </summary>
        public SecureString Password { get; }

        /// <summary>
        /// Obtiene un indicio de contraseña asociada a este <see cref="UserData"/>.
        /// </summary>
        public string? Hint { get; }

        /// <summary>
        /// Obtiene un valor que representa la calidad de la contraseña
        /// determinada por un <see cref="IPasswordEvaluator"/>, o
        /// <see langword="null"/> si la misma no ha sido evaluada.
        /// </summary>
        public float? Quality { get; }
    }
}