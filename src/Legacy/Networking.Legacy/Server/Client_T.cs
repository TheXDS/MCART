/*
Client_T.cs

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

using System.Net.Sockets;

namespace TheXDS.MCART.Networking.Legacy.Server
{
    /// <summary>
    /// Representa un cliente que requiere datos de estado asociados que se ha
    /// conectado al servidor.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de datos asociados de cliente requeridos.
    /// </typeparam>
    public class Client<T> : Client
    {
        /// <summary>
        /// Contiene un objeto de estado personalizado asociado a esta
        /// instancia de la clase <see cref="Client{T}" />.
        /// </summary>
        public T ClientData { get; set; } = default!;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Client{T}" />.
        /// </summary>
        /// <param name="connection">
        /// <see cref="TcpClient" /> a utilizar para las comunicaciones con el
        /// cliente.
        /// </param>
        public Client(TcpClient connection) : base(connection)
        {
        }
    }
}