/*
ServerAttribute.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

using System;
using TheXDS.MCART.Types.Extensions;
using static System.AttributeTargets;
#if NETFX_CORE
using System.Runtime.Serialization;
#endif


namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc cref="Attribute"/>
    /// <summary>
    ///     Atributo que define la ruta de un servidor.
    /// </summary>
    /// <remarks>
    ///     Es posible establecer este atributo más de una vez en un mismo elemento.
    /// </remarks>
    [AttributeUsage(All, AllowMultiple = true)]
#if NETFX_CORE
    [DataContract]
#else
    [Serializable]
#endif
    public sealed class ServerAttribute : Attribute//, IValueAttribute<(string, int)>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ServerAttribute" /> estableciendo el servidor y el puerto
        ///     al cual este atributo hará referencia.
        /// </summary>
        /// <param name="server">Nombre del servidor / Dirección IP.</param>
        /// <param name="port">Número de puerto del servidor.</param>
        /// <remarks>
        ///     Si se define un número de puerto en <paramref name="server" />, el
        ///     valor del parámetro <paramref name="port" /> tomará precedencia.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///     Se produce si el servidor es una ruta malformada.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Se produce si <paramref name="port" /> es inferior a 1, o superior
        ///     a 65535.
        /// </exception>
        public ServerAttribute(string server, int port)
        {
            if (server.IsEmpty()) throw new ArgumentNullException(nameof(server));
            if (!port.IsBetween(1, 65535)) throw new ArgumentOutOfRangeException(nameof(port));
            Server = server;
            Port = port;
        }

        /// <summary>
        ///     Obtiene el servidor.
        /// </summary>
        /// <value>
        ///     La ruta del servidor a la cual este atributo apunta.
        /// </value>
#if NETFX_CORE
        [DataMember]
#endif
        public string Server { get; }

        /// <summary>
        ///     Obtiene o establece el puerto de conexión del servidor.
        /// </summary>
        /// <value>
        ///     Un valor entre 1 y 65535 que establece el número de puerto a
        ///     apuntar.
        /// </value>
#if NETFX_CORE
        [DataMember]
#endif
        public int Port { get; }

        /// <summary>
        ///     Devuelve una cadena que representa al objeto actual.
        /// </summary>
        /// <returns>Una cadena que representa al objeto actual.</returns>
        public override string ToString()
        {
            return $"{Server}:{Port}";
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene el valor de este atributo.
        /// </summary>
        //public (string, int) Value => (Server, Port);
    }
}