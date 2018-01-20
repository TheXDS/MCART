/*
Client.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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

using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace TheXDS.MCART.Networking.Server
{
    /// <summary>
    /// Representa a un cliente que no requiere datos de estado que se ha
    /// conectado al servidor.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Obtiene la conexión <see cref="System.Net.Sockets.TcpClient"/> asociada a esta instancia.
        /// </summary>
        /// <value>My connection.</value>
        internal TcpClient TcpClient { get; }
        /// <summary>
        /// Obtiene el objeto Server asociado a esta instancia.
        /// </summary>
        /// <value>The server.</value>
        public Server<Client> Server { get; }
        /// <summary>
        /// Indica si esta instancia de <see cref="Client"/> se encuentra
        /// conectada a un servidor.
        /// </summary>
        /// <value>
        /// <see langword="true"/> si esta instancia de <see cref="Client"/> se
        /// encuentra conectada a un servidor, <see langword="false"/> en caso
        /// contrario.
        /// </value>
        public bool IsAlive => TcpClient?.Connected ?? false;
        /// <summary>
        /// Obtiene un valor que indica si hay datos disponibles para leer.
        /// </summary>
        /// <value>
        /// <see langword="true"/> si hay datos disponibles,
        /// <see langword="false"/> en caso contrario.
        /// </value>
        public bool DataAvailable => TcpClient?.GetStream()?.DataAvailable ?? false;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Client"/>.
        /// </summary>
        /// <param name="tcpClient">
        /// <see cref="System.Net.Sockets.TcpClient"/> a utilizar para las 
        /// comunicaciones con el cliente.
        /// </param>
        /// <param name="server">
        /// <see cref="Networking.Server.Server{TClient}"/> que atenderá a este
        /// cliente.
        /// </param>
        public Client(TcpClient tcpClient, Server<Client> server)
        {
            TcpClient = tcpClient;
            Server = server;
        }
        /// <summary>
        /// Envía un mensaje al cliente.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        public void Send(byte[] data)
        {
            TcpClient?.GetStream().Write(data, 0, data.Length);
        }
        /// <summary>
        /// Envía un mensaje al cliente de forma asíncrona.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        /// <returns>
        /// Un <see cref="Task"/> que permite monitorizar el estado de la
        /// tarea.
        /// </returns>
        public async Task SendAsync(byte[] data)
        {
            await TcpClient?.GetStream().WriteAsync(data, 0, data.Length);
        }
        /// <summary>
        /// Envía un mensaje al cliente de forma asíncrona.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        /// <param name="cancellationToken">
        /// Token de cancelación de tarea.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que permite monitorizar el estado de la
        /// tarea.
        /// </returns>
        public async Task SendAsync(byte[] data, CancellationToken cancellationToken)
        {
            await TcpClient?.GetStream().WriteAsync(data, 0, data.Length, cancellationToken);
        }
        /// <summary>
        /// Devuelve los datos que el cliente envía.
        /// </summary>
        /// <returns>Un arreglo de <see cref="byte"/> con la información recibida desde el servidor.</returns>
        public byte[] Receive()
        {
            NetworkStream ns = TcpClient?.GetStream();
            if (ns is null)
#if PreferExceptions
				throw new ArgumentNullException();
#else
                return new byte[] { };
#endif
            using (var ms = new MemoryStream())
            {
                ns.CopyTo(ms);
                return ms.ToArray();
            }
        }
        /// <summary>
        /// Devuelve los datos recibidos una vez que el cliente los envía.
        /// </summary>
        /// <returns>Un arreglo de <see cref="byte"/> con la información recibida desde el servidor.</returns>
        public async Task<byte[]> RecieveAsync()
        {
            NetworkStream ns = TcpClient?.GetStream();
            if (ns is null)
#if PreferExceptions
				throw new ArgumentNullException();
#else
                return new byte[] { };
#endif
            using (var ms = new MemoryStream())
            {
                await ns.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
        /// <summary>
        /// Devuelve los datos recibidos una vez que el cliente los envía.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación de tarea.</param>
        /// <returns>Un arreglo de <see cref="byte"/> con la información recibida desde el servidor.</returns>
        public async Task<byte[]> RecieveAsync(CancellationToken cancellationToken)
        {
            NetworkStream ns = TcpClient?.GetStream();
            if (ns is null)
#if PreferExceptions
				throw new ArgumentNullException();
#else
                return new byte[] { };
#endif
            using (var ms = new MemoryStream())
            {
                await ns.CopyToAsync(ms, 81920, cancellationToken);
                return ms.ToArray();
            }
        }
        /// <summary>
        /// Desconecta al cliente del servidor.
        /// </summary>
        public void Disconnect()
        {
            Server.Protocol.ClientBye(this, Server);
            TcpClient.Close();
        }
    }

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
        /// instancia de la clase <see cref="Client{T}"/>.
        /// </summary>
        public T ClientData { get; set; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Client{T}"/>.
        /// </summary>
        /// <param name="tcpClient">
        /// <see cref="TcpClient"/> a utilizar para las comunicaciones con el 
        /// cliente.
        /// </param>
        /// <param name="server">
        /// <see cref="Server"/> que atenderá a este cliente.
        /// </param>
        public Client(TcpClient tcpClient, Server<Client> server) : base(tcpClient, server) { }
    }
}