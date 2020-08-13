/*
IServer.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene la implementación de servidor de MCART.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using System.Net;
using System.Threading.Tasks;
using TheXDS.MCART.Events;

namespace TheXDS.MCART.Networking.Legacy.Server
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que controla
    /// conexiones entrantes y ejecuta protocolos sobre los clientes que se
    /// conecten al servidor.
    /// </summary>
    public interface IServer : IDisposable
    {
        /// <summary>
        /// Obtiene o establece un valor que indica si este
        /// <see cref="IServer" /> está activo (vivo).
        /// </summary>
        /// <value><see langword="true" /> si está vivo; sino, <see langword="false" />.</value>
        bool IsAlive { get; set; }

        /// <summary>
        /// Dirección IP a la cual este servidor escucha.
        /// </summary>
        IPEndPoint ListeningEndPoint { get; }

        /// <summary>
        /// Instancia de protocolo a utilizar para dar servicio a los
        /// clientes que se conecten a este servidor.
        /// </summary>
        IProtocol Protocol { get; }

        /// <summary>
        /// Ocurre cuando el servidor es detenido.
        /// </summary>
        event EventHandler<ValueEventArgs<DateTime>>? ServerStopped;

        /// <summary>
        /// Ocurre cuando el servidor es iniciado.
        /// </summary>
        event EventHandler<ValueEventArgs<DateTime>>? ServerStarted;

        /// <summary>
        /// Ocurre cuando un nuevo cliente se conecta al servidor.
        /// </summary>
        event EventHandler<ValueEventArgs<Client>>? ClientConnected;

        /// <summary>
        /// Ocurre cuando el protocolo ha aceptado al nuevo cliente.
        /// </summary>
        event EventHandler<ValueEventArgs<Client>>? ClientAccepted;

        /// <summary>
        /// Ocurre cuando un cliente se desconecta correctamente.
        /// </summary>
        event EventHandler<ValueEventArgs<Client>>? ClientDisconnected;

        /// <summary>
        /// Ocurre cuando el protocolo despide a un cliente.
        /// </summary>
        event EventHandler<ValueEventArgs<Client>>? ClientFarewell;

        /// <summary>
        /// Ocurre cuando un cliente se desconecta inesperadamente.
        /// </summary>
        event EventHandler<ValueEventArgs<Client?>>? ClientLost;

        /// <summary>
        /// Ocurre cuando el protocolo rechaza al nuevo cliente.
        /// </summary>
        event EventHandler<ValueEventArgs<Client?>>? ClientRejected;

        /// <summary>
        /// Envía un mensaje a todos los clientes.
        /// </summary>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        void Broadcast(byte[] data);

        /// <summary>
        /// Envía un mensaje a todos los clientes.
        /// </summary>
        /// <returns>
        /// Un <see cref="Task" /> que representa esta tarea.
        /// </returns>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        Task BroadcastAsync(byte[] data);

        /// <summary>
        /// Crea un hilo de ejecución que da servicio a los clientes
        /// </summary>
        Task Start();
        
        /// <summary>
        /// Detiene al servidor.
        /// </summary>
        void Stop();
        
        /// <summary>
        /// Detiene al servidor de forma asíncrona.
        /// </summary>
        /// <returns>
        /// Un <see cref="Task" /> que puede utilizarse para monitorear el
        /// estado de esta tarea.
        /// </returns>
        Task StopAsync();
    }
}