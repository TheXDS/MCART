//
//  Client.cs
// 
//  This file is part of Morgan's CLR Advanced Runtime (MCART)
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;
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
		/// Búfer predeterminado para recepción.
		/// </summary>
		public const ushort bufferSize = 256;

		/// <summary>
		/// Obtiene la conexión <see cref="System.Net.Sockets.TcpClient"/> asociada a esta instancia.
		/// </summary>
		/// <value>My connection.</value>
		internal readonly TcpClient TcpClient;

		/// <summary>
		/// Obtiene el objeto Server asociado a esta instancia.
		/// </summary>
		/// <value>The server.</value>
		public readonly Server<Client> Server;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Client"/> is alive.
		/// </summary>
		/// <value><c>true</c> if is alive; otherwise, <c>false</c>.</value>
		public bool IsAlive => (bool)TcpClient?.Connected;

		/// <summary>
		/// Obtiene un valor que indica si hay datos disponibles para leer.
		/// </summary>
		/// <value><c>true</c> si hay datos disponibles; sino, <c>false</c>.</value>
		public bool DataAvailable => (bool)TcpClient?.GetStream()?.DataAvailable;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Client"/>.
        /// </summary>
        /// <param name="tcpClient">
        /// <see cref="System.Net.Sockets.TcpClient"/> a utilizar para las comunicaciones con el 
        /// cliente.
        /// </param>
        /// <param name="server">
        /// <see cref="Networking.Server.Server{TClient}"/> que atenderá a este cliente.
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
		public byte[] Recieve()
		{
			NetworkStream ns = TcpClient?.GetStream();
			if (ns is null)
#if PreferExceptions
				throw new ArgumentNullException();
#else
				return new byte[] { };
#endif
#if BufferedIO
			int sze;
			List<byte> outp = new List<byte>();
			do
			{
				byte[] buff = new byte[bufferSize];
				sze = ns.Read(buff, 0, buff.Length);
				if (sze < bufferSize) System.Array.Resize(ref buff, sze);
				outp.AddRange(buff);
			} while (ns.DataAvailable);
			return outp.ToArray();
#else
			byte[] buff = new byte[TcpClient.Available];
			ns.Read(buff,0, TcpClient.Available);
			return buff;
#endif
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
#if BufferedIO
			int sze;
			List<byte> outp = new List<byte>();
			do
			{
				byte[] buff = new byte[bufferSize];
				sze = await ns.ReadAsync(buff, 0, buff.Length);
				if (sze < bufferSize) System.Array.Resize(ref buff, sze);
				outp.AddRange(buff);
			} while (ns.DataAvailable);
			return outp.ToArray();
#else
			byte[] buff = new byte[TcpClient.Available];
			await ns.ReadAsync(buff,0, TcpClient.Available);
			return buff;
#endif
		}

        /// <summary>
        /// Devuelve los datos recibidos una vez que el cliente los envía.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación de tarea.</param>
        /// <returns>Un arreglo de <see cref="byte"/> con la información recibida desde el servidor.</returns>
        public async Task<byte[]> RecieveAsync(CancellationToken cancellationToken)
		{
			List<byte> outp = new List<byte>();
			do
			{
				byte[] buff = new byte[bufferSize];
				await TcpClient?.GetStream().ReadAsync(buff, 0, bufferSize, cancellationToken);
				outp.AddRange(buff);
			} while (TcpClient?.GetStream()?.DataAvailable ?? false);
			return outp.ToArray();
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
		/// instancia, para utilizarse como el usuario lo desee.
		/// </summary>
		public T userObj;
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