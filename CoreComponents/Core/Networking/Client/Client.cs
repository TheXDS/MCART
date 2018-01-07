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

using System.Net.Sockets;
using System.Threading.Tasks;

#if BufferedIO
using System.Collections.Generic;
#endif

#if PreferExceptions
using System;
#endif

namespace TheXDS.MCART.Networking.Client
{
    /// <summary>
    /// Permite enviar y recibir información desde y hacia un servidor. Además,
    /// es una clase base para los controladores de protocolo del cliente.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Conexión al servidor
        /// </summary>
        protected readonly TcpClient connection = new TcpClient();
        /// <summary>
        /// Puerto predeterminado para las conexiones entrantes.
        /// </summary>
        public const ushort defaultPort = 51220;

#if BufferedIO

        /// <summary>
        /// Búfer predeterminado para recepción.
        /// </summary>
        public const ushort bufferSize = 256;

#endif

        /// <summary>
        /// Gets a value indicating whether this <see cref="Client"/> is alive.
        /// </summary>
        /// <value><c>true</c> if is alive; otherwise, <c>false</c>.</value>
        public bool IsAlive => (bool)connection?.Connected;
        /// <summary>
        /// Establece una conexión con el servidor de forma asíncrona.
        /// </summary>
        /// <returns>Un <see cref="Task"/> que representa la tarea.</returns>
        /// <param name="server">Servidor al cual conectarse.</param>
        /// <param name="port">
        /// Opcional. Puerto del servidor. Si se omite, se conectará al puerto
        /// predeterminado.
        /// </param>
        public async Task ConnectAsync(string server, ushort port = defaultPort)
        {
            try
            {
                await connection.ConnectAsync(server, port);
                AtConnect();
            }
#if PreferExceptions
            catch { throw; }
#else
			catch { ConnChk(); AtFail(); }
#endif
        }
        /// <summary>
        /// Establece una conexión con el servidor de forma asíncrona.
        /// </summary>
        /// <returns>Un <see cref="Task"/> que representa la tarea.</returns>
        /// <param name="server">Servidor al cual conectarse.</param>
        public async Task ConnectAsync(System.Net.IPEndPoint server)
        {
            try
            {
                await connection.ConnectAsync(server.Address, server.Port);
                AtConnect();
            }
#if PreferExceptions
            catch { throw; }
#else
			catch { ConnChk(); AtFail(); }
#endif
        }
        /// <summary>
        /// Establece una conexión con el servidor de forma asíncrona.
        /// </summary>
        /// <returns>Un <see cref="Task"/> que representa la tarea.</returns>
        /// <param name="server">Servidor al cual conectarse.</param>
        /// <param name="port">
        /// Opcional. Puerto del servidor. Si se omite, se conectará al puerto
        /// predeterminado.
        /// </param>
        public async Task ConnectAsync(System.Net.IPAddress server, ushort port = defaultPort)
        {
            try
            {
                await connection.ConnectAsync(server, port);
                AtConnect();
            }
#if PreferExceptions
            catch { throw; }
#else
			catch { ConnChk(); AtFail(); }
#endif
        }
        /// <summary>
        /// Establece una conexión con el servidor.
        /// </summary>
        /// <param name="server">Servidor al cual conectarse.</param>
        /// <param name="port">
        /// Opcional. Puerto del servidor. Si se omite, se conectará al puerto
        /// predeterminado.
        /// </param>
        public void Connect(string server, ushort port = defaultPort)
        {
            try
            {
                connection.Connect(server, port);
                AtConnect();
            }
#if PreferExceptions
            catch { throw; }
#else
			catch { ConnChk(); AtFail(); }
#endif
        }
        /// <summary>
        /// Establece una conexión con el servidor.
        /// </summary>
        /// <param name="server">Servidor al cual conectarse.</param>
        public void Connect(System.Net.IPEndPoint server)
        {
            try
            {
                connection.Connect(server);
                AtConnect();
            }
#if PreferExceptions
            catch { throw; }
#else
			catch { ConnChk(); AtFail(); }
#endif
        }
        /// <summary>
        /// Establece una conexión con el servidor.
        /// </summary>
        /// <param name="server">Servidor al cual conectarse.</param>
        /// <param name="port">
        /// Opcional. Puerto del servidor. Si se omite, se conectará al puerto
        /// predeterminado.
        /// </param>
        public void Connect(System.Net.IPAddress server, ushort port = defaultPort)
        {
            try
            {
                connection.Connect(server, port);
                AtConnect();
            }
#if PreferExceptions
            catch { throw; }
#else
			catch { ConnChk(); AtFail(); }
#endif
        }
        /// <summary>
        /// Desconecta correctamente a este cliente del servidor.
        /// </summary>
        public void Disconnect()
        {
            try { AtDisconnect(); }
            finally { ConnChk(); }
        }
        /// <summary>
        /// Envía un mensaje, y espera a que el servidor responda.
        /// </summary>
        /// <returns>Un mensaje enviado por el servidor.</returns>
        /// <param name="data">Mensaje a enviar al servidor.</param>
        public byte[] TalkToServer(byte[] data)
        {
            if (!(data?.Length > 0))
#if PreferExceptions
                throw new ArgumentNullException();
#else
				return new byte[] { };
#endif
            NetworkStream ns = connection?.GetStream();
            if (ns is null)
#if PreferExceptions
                throw new ArgumentNullException();
#else
				return new byte[] { };
#endif
#if BufferedIO
            int sze = data.Length;
            while (sze > 0)
            {
                ns.Write(data, data.Length - sze, (bufferSize < sze ? bufferSize : sze));
                sze -= bufferSize;
            }
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
			ns.Write(data, 0, data.Length);
			byte[] buff = new byte[(int)connection?.Available];
			ns.Read(buff,0, connection.Available);
			return buff;
#endif
        }
        /// <summary>
        /// Envía un mensaje, y espera a que el servidor responda de forma asíncrona.
        /// </summary>
        /// <returns>Un mensaje enviado por el servidor.</returns>
        /// <param name="data">Mensaje a enviar al servidor.</param>
        public async Task<byte[]> TalkToServerAsync(byte[] data)
        {
            if (!(data?.Length > 0))
#if PreferExceptions
                throw new ArgumentNullException();
#else
				return new byte[] { };
#endif
            NetworkStream ns = connection?.GetStream();
            if (ns is null)
#if PreferExceptions
                throw new ArgumentNullException();
#else
				return new byte[] { };
#endif
#if BufferedIO
            await ns.WriteAsync(data, 0, data.Length);
            int sze = data.Length;
            while (sze > 0)
            {
                ns.Write(data, data.Length - sze, (bufferSize < sze ? bufferSize : sze));
                sze -= bufferSize;
            }
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
			await ns.WriteAsync(data, 0, data.Length);
			byte[] buff = new byte[(int)connection?.Available];
			await ns.ReadAsync(buff,0, connection.Available);
			return buff;
#endif
        }
        /// <summary>
        /// Método invalidable que indica una serie de acciones a realizar al
        /// conectarse exitosamente con el servidor.
        /// </summary>
        /// <remarks>
        /// De forma predeterminada, no se realiza ninguna acción.
        /// </remarks>
        public virtual void AtConnect() { }
        /// <summary>
        /// Método invalidable que indica una serie de acciones a realizar al
        /// desconectarse del servidor.
        /// </summary>
        /// <remarks>
        /// De forma predeterminada, no se realiza ninguna acción.
        /// </remarks>
        public virtual void AtDisconnect() { }
        /// <summary>
        /// Método invalidable que indica una serie de acciones a realizar al no
        /// poder establecerse una conexión con el servidor.
        /// </summary>
        /// <remarks>
        /// De forma predeterminada, no se realiza ninguna acción.
        /// </remarks>
        public virtual void AtFail() { }
        /// <summary>
        /// Se asegura de cerrar la conexión.
        /// </summary>
        void ConnChk() { if (connection?.Connected ?? false) connection.Close(); }
        /// <summary>
        /// Realiza alguans tareas de limpieza antes de finalizar esta
        /// instancia de la clase <see cref="Client"/>.
        /// </summary>
        ~Client() { ConnChk(); }
    }
}