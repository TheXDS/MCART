﻿/*
IServerProtocol.cs

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

namespace TheXDS.MCART.Networking.Legacy.Server
{
    /// <summary>
    /// Define una serie de métodos a implementar por una clase que deba
    /// exponer un <see cref="Server"/> asociado.
    /// </summary>
    internal interface IServerProtocol : IServerProtocol<Client>
    {
        /// <summary>
        /// Obtiene o establece la instancia de servidor asociada a este
        /// objeto.
        /// </summary>
        new Server MyServer
        { 
            get => (Server)((IServerProtocol<Client>)this).MyServer;
            set=> ((IServerProtocol<Client>)this).MyServer = value;
        }
    }

    /// <summary>
    /// Define una serie de métodos a implementar por una clase que deba
    /// exponer un <see cref="Server"/> asociado.
    /// </summary>
    internal interface IServerProtocol<T> where T : Client
    {
        /// <summary>
        /// Obtiene o establece la instancia de servidor asociada a este
        /// objeto.
        /// </summary>
        Server<T> MyServer { get; set; }
    }
}