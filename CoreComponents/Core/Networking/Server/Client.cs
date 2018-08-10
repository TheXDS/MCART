﻿/*
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace TheXDS.MCART.Networking.Server
{
    /// <inheritdoc />
    /// <summary>
    ///     Representa a un cliente que no requiere datos de estado que se ha
    ///     conectado al servidor.
    /// </summary>
    public class Client
    {
        /// <inheritdoc />
        /// <summary>
        ///     Define los métodos a implementar por una clase que represente a un
        ///     cliente conectado al servidor.
        /// </summary>
        public bool Disconnecting { get; private set; }
    
        /// <summary>
        ///     Obtiene un valor que indica si hay datos disponibles para leer.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> si hay datos disponibles,
        ///     <see langword="false" /> en caso contrario.
        /// </value>
        public bool DataAvailable => TcpClient?.GetStream().DataAvailable ?? false;

        /// <summary>
        ///     Obtiene el <see cref="IPEndPoint" /> remoto del cliente.
        /// </summary>
        public IPEndPoint EndPoint => TcpClient.Client.RemoteEndPoint as IPEndPoint;

        /// <summary>
        ///     Indica si esta instancia de <see cref="Client" /> se encuentra
        ///     conectada a un servidor.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> si esta instancia de <see cref="Client" /> se
        ///     encuentra conectada a un servidor, <see langword="false" /> en caso
        ///     contrario.
        /// </value>
        public bool IsAlive => !(TcpClient?.GetStream() is null);

        /// <summary>
        ///     Obtiene la conexión <see cref="System.Net.Sockets.TcpClient" /> asociada a esta instancia.
        /// </summary>
        /// <value>My connection.</value>
        private TcpClient TcpClient { get; }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="Client" />.
        /// </summary>
        /// <param name="tcpClient">
        ///     <see cref="System.Net.Sockets.TcpClient" /> a utilizar para las
        ///     comunicaciones con el cliente.
        /// </param>
        public Client(TcpClient tcpClient)
        {
            TcpClient = tcpClient;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicia el proceso de desconexión del cliente.
        /// </summary>
        public void Bye()
        {
            Disconnecting = true;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Desconecta al cliente del servidor.
        /// </summary>
        public void Disconnect() => TcpClient.Close();

        /// <summary>
        ///     Devuelve los datos que el cliente envía.
        /// </summary>
        /// <returns>Un arreglo de <see cref="byte" /> con la información recibida desde el servidor.</returns>
        public byte[] Recieve()
        {
            var ns = TcpClient?.GetStream();
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
        ///     Devuelve los datos recibidos una vez que el cliente los envía.
        /// </summary>
        /// <returns>Un arreglo de <see cref="byte" /> con la información recibida desde el servidor.</returns>
        public async Task<byte[]> RecieveAsync()
        {
            var ns = TcpClient?.GetStream();
            if (ns is null)
#if PreferExceptions
				throw new ArgumentNullException();
#else
                return new byte[] { };
#endif

            var outp = new List<byte>();
            do
            {
                var buff = new byte[TcpClient.ReceiveBufferSize];
                var sze = await ns.ReadAsync(buff, 0, buff.Length);
                if (sze < TcpClient.ReceiveBufferSize) Array.Resize(ref buff, sze);
                outp.AddRange(buff);
            } while (ns.DataAvailable);

            return outp.ToArray();
        }

        /// <summary>
        ///     Devuelve los datos recibidos una vez que el cliente los envía.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación de tarea.</param>
        /// <returns>Un arreglo de <see cref="byte" /> con la información recibida desde el servidor.</returns>
        public async Task<byte[]> RecieveAsync(CancellationToken cancellationToken)
        {
            var ns = TcpClient?.GetStream();
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
        ///     Envía un mensaje al cliente.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        public void Send(byte[] data)
        {
            TcpClient?.GetStream().Write(data, 0, data.Length);
        }

        /// <summary>
        ///     Envía un mensaje al cliente.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        public void Send(IEnumerable<byte> data)
        {
            Send(data.ToArray());
        }

        /// <summary>
        ///     Envía un mensaje al cliente.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        public void Send(MemoryStream data)
        {
            Send(data.ToArray());
        }

        /// <summary>
        ///     Envía un mensaje al cliente.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        public void Send(Stream data)
        {
            using (var reader = new MemoryStream())
            {
                data.CopyTo(reader);
                Send(reader.ToArray());
            }
        }

        /// <summary>
        ///     Envía un mensaje al cliente de forma asíncrona.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        /// <returns>
        ///     Un <see cref="Task"/> que puede utilizarse para monitorear la
        ///     operación asíncrona.
        /// </returns>
        public Task SendAsync(IEnumerable<byte> data)
        {
            return SendAsync(data.ToArray());
        }

        /// <summary>
        ///     Envía un mensaje al cliente de forma asíncrona.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        /// <returns>
        ///     Un <see cref="Task"/> que puede utilizarse para monitorear la
        ///     operación asíncrona.
        /// </returns>
        public async Task SendAsync(Stream data)
        {
            using (var reader = new MemoryStream())
            {
                await data.CopyToAsync(reader);
                await SendAsync(reader.ToArray());
            }
        }

        /// <summary>
        ///     Envía un mensaje al cliente de forma asíncrona.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        /// <returns>
        ///     Un <see cref="Task"/> que puede utilizarse para monitorear la
        ///     operación asíncrona.
        /// </returns>
        public Task SendAsync(MemoryStream data)
        {
            return SendAsync(data.ToArray());
        }

        /// <summary>
        ///     Envía un mensaje al cliente de forma asíncrona.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        /// <returns>
        ///     Un <see cref="Task"/> que puede utilizarse para monitorear la
        ///     operación asíncrona.
        /// </returns>
        public Task SendAsync(byte[] data)
        {
            return TcpClient?.GetStream().WriteAsync(data, 0, data.Length);
        }

        /// <summary>
        ///     Envía un mensaje al cliente de forma asíncrona.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        /// <param name="cancellationToken">
        ///     Token de cancelación de tarea.
        /// </param>
        /// <returns>
        ///     Un <see cref="Task"/> que puede utilizarse para monitorear la
        ///     operación asíncrona.
        /// </returns>
        public Task SendAsync(byte[] data, CancellationToken cancellationToken)
        {
            return TcpClient?.GetStream().WriteAsync(data, 0, data.Length, cancellationToken);
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Representa un cliente que requiere datos de estado asociados que se ha
    ///     conectado al servidor.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo de datos asociados de cliente requeridos.
    /// </typeparam>
    public class Client<T> : Client
    {
        /// <summary>
        ///     Contiene un objeto de estado personalizado asociado a esta
        ///     instancia de la clase <see cref="Client{T}" />.
        /// </summary>
        public T ClientData { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Networking.Server.Client`1" />.
        /// </summary>
        /// <param name="tcpClient">
        ///     <see cref="T:System.Net.Sockets.TcpClient" /> a utilizar para las comunicaciones con el
        ///     cliente.
        /// </param>
        public Client(TcpClient tcpClient) : base(tcpClient)
        {
        }
    }
}