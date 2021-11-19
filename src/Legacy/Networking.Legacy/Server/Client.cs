/*
Client.cs

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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using static TheXDS.MCART.Types.Extensions.TaskExtensions;

namespace TheXDS.MCART.Networking.Legacy.Server
{
    /// <summary>
    /// Representa a un cliente que no requiere datos de estado que se ha
    /// conectado al servidor.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Obtiene de forma segura la instancia del
        /// <see cref="NetworkStream"/> utilizada para la conexión con el
        /// cliente remoto.
        /// </summary>
        /// <returns>
        /// Un <see cref="NetworkStream"/> utilizado para la conexión con
        /// el cliente remoto, o <see langword="null"/> si no existe una
        /// conexión activa válida.
        /// </returns>
        protected NetworkStream? NwStream()
        {
            try
            {
                return Connection?.GetStream();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Define los métodos a implementar por una clase que represente a un
        /// cliente conectado al servidor.
        /// </summary>
        public bool Disconnecting { get; private set; }

        /// <summary>
        /// Obtiene un valor que indica si hay datos disponibles para leer.
        /// </summary>
        /// <value>
        /// <see langword="true" /> si hay datos disponibles,
        /// <see langword="false" /> en caso contrario.
        /// </value>
        public bool DataAvailable => NwStream()?.DataAvailable ?? false;

        /// <summary>
        /// Obtiene el <see cref="IPEndPoint" /> remoto del cliente.
        /// </summary>
        public IPEndPoint? EndPoint
        {
            get
            {
                try
                {
                    return Connection.Client.RemoteEndPoint as IPEndPoint;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Indica si esta instancia de <see cref="Client" /> se encuentra
        /// conectada a un servidor.
        /// </summary>
        /// <value>
        /// <see langword="true" /> si esta instancia de <see cref="Client" /> se
        /// encuentra conectada a un servidor, <see langword="false" /> en caso
        /// contrario.
        /// </value>
        public bool IsAlive => NwStream() is not null;

        /// <summary>
        /// Obtiene la conexión <see cref="TcpClient" /> asociada a esta instancia.
        /// </summary>
        /// <value>My connection.</value>
        private TcpClient Connection { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Client" />.
        /// </summary>
        /// <param name="connection">
        /// <see cref="TcpClient" /> a utilizar para las
        /// comunicaciones con el cliente.
        /// </param>
        public Client(TcpClient connection)
        {
            Connection = connection;
        }

        /// <summary>
        /// Inicia el proceso de desconexión del cliente.
        /// </summary>
        public void Bye()
        {
            Disconnecting = true;
        }

        /// <summary>
        /// Desconecta al cliente del servidor.
        /// </summary>
        public void Disconnect() => Connection.Close();

        /// <summary>
        /// Devuelve los datos que el cliente envía.
        /// </summary>
        /// <returns>Un arreglo de <see cref="byte" /> con la información recibida desde el servidor.</returns>
        public byte[] Recieve()
        {
            NetworkStream? ns = NwStream();
            if (ns is null)
#if PreferExceptions
				throw new ArgumentNullException();
#else
                return Array.Empty<byte>();
#endif
            using MemoryStream? ms = new();
            ns.CopyTo(ms);
            return ms.ToArray();
        }

        /// <summary>
        /// Obtiene un paquete completo de datos desde el servidor.
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        protected byte[] GetData(NetworkStream ns)
        {
            List<byte>? outp = new();
            do
            {
                byte[]? buff = new byte[Connection.ReceiveBufferSize];
                int sze = ns.Read(buff, 0, buff.Length);
                if (sze < Connection.ReceiveBufferSize) Array.Resize(ref buff, sze);
                outp.AddRange(buff);
            } while (ns.DataAvailable);
            return outp.ToArray();
        }

        /// <summary>
        /// Obtiene un paquete completo de datos desde el servidor.
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        protected async Task<byte[]> GetDataAsync(NetworkStream ns)
        {
            try
            {
                List<byte>? outp = new();
                do
                {
                    byte[]? buff = new byte[Connection.ReceiveBufferSize];
                    int sze = await ns.ReadAsync(buff, 0, buff.Length);
                    if (sze < Connection.ReceiveBufferSize) Array.Resize(ref buff, sze);
                    outp.AddRange(buff);
                } while (ns.DataAvailable);
                return outp.ToArray();
            }
            catch (ObjectDisposedException)
            {
                return Array.Empty<byte>();
            }
        }

        /// <summary>
        /// Devuelve los datos recibidos una vez que el cliente los envía.
        /// </summary>
        /// <returns>Un arreglo de <see cref="byte" /> con la información recibida desde el servidor.</returns>
        public async Task<byte[]> RecieveAsync()
        {
            NetworkStream? ns = NwStream();
            if (ns is null)
#if PreferExceptions
            	throw new ArgumentNullException();
#else
                return Array.Empty<byte>();
#endif
            return await GetDataAsync(ns);
        }

        /// <summary>
        /// Devuelve los datos recibidos una vez que el cliente los envía.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación de tarea.</param>
        /// <returns>Un arreglo de <see cref="byte" /> con la información recibida desde el servidor.</returns>
        public Task<byte[]> RecieveAsync(CancellationToken cancellationToken)
        {
            NetworkStream? ns = NwStream();
            if (ns is null) return Task.FromResult(Array.Empty<byte>());
            try
            {
                //using var ms = new MemoryStream();
                //await ns.CopyToAsync(ms, Connection.ReceiveBufferSize, cancellationToken);
                //return !cancellationToken.IsCancellationRequested ? ms.ToArray() : new byte[0];
                return GetDataAsync(ns).WithCancellation(cancellationToken);
            }
            catch
            {
                return Task.FromResult(Array.Empty<byte>());
            }
        }

        /// <summary>
        /// Envía un mensaje al cliente.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        public void Send(byte[] data)
        {
            NwStream()?.Write(data, 0, data.Length);
        }

        /// <summary>
        /// Envía un mensaje al cliente.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        public void Send(IEnumerable<byte> data)
        {
            Send(data.ToArray());
        }

        /// <summary>
        /// Envía un mensaje al cliente.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        public void Send(MemoryStream data)
        {
            Send(data.ToArray());
        }

        /// <summary>
        /// Envía un mensaje al cliente.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        public void Send(Stream data)
        {
            using MemoryStream? reader = new();
            data.CopyTo(reader);
            Send(reader.ToArray());
        }

        /// <summary>
        /// Envía un mensaje al cliente de forma asíncrona.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para monitorear la
        /// operación asíncrona.
        /// </returns>
        public Task SendAsync(IEnumerable<byte> data)
        {
            return SendAsync(data.ToArray());
        }

        /// <summary>
        /// Envía un mensaje al cliente de forma asíncrona.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para monitorear la
        /// operación asíncrona.
        /// </returns>
        public async Task SendAsync(Stream data)
        {
            using MemoryStream? reader = new();
            await data.CopyToAsync(reader);
            await SendAsync(reader.ToArray());
        }

        /// <summary>
        /// Envía un mensaje al cliente de forma asíncrona.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para monitorear la
        /// operación asíncrona.
        /// </returns>
        public Task SendAsync(MemoryStream data)
        {
            return SendAsync(data.ToArray());
        }

        /// <summary>
        /// Envía un mensaje al cliente de forma asíncrona.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para monitorear la
        /// operación asíncrona.
        /// </returns>
        public Task SendAsync(byte[] data)
        {
            return NwStream()?.WriteAsync(data, 0, data.Length) ?? Task.CompletedTask;
        }

        /// <summary>
        /// Envía un mensaje al cliente de forma asíncrona.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        /// <param name="cancellationToken">
        /// Token de cancelación de tarea.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para monitorear la
        /// operación asíncrona.
        /// </returns>
        public Task SendAsync(byte[] data, CancellationToken cancellationToken)
        {
            return NwStream()?.WriteAsync(data, 0, data.Length, cancellationToken) ?? Task.CompletedTask;
        }
    }
}