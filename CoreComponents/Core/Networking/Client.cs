//
//  Client.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

#region Opciones de compilación

//Preferir excepciones en lugar de continuar con código alternativo
//#define PreferExceptions

//Incluir implementaciones predeterminadas de interfaces / clases abstractas
#define IncludeDefaultImplementations

#endregion

using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MCART.Networking.Client
{
    /// <summary>
    /// Clase base para los controladores de protocolo del cliente.
    /// </summary>
	public abstract class Client
    {
        /// <summary>
        /// Puerto predeterminado para las conexiones entrantes.
        /// </summary>
        public const ushort defaultPort = 51220;

        /// <summary>
        /// Búfer predeterminado para recepción.
        /// </summary>
        public const ushort bufferSize = 1024;

        /// <summary>
        /// Conexión al servidor
        /// </summary>
        protected readonly TcpClient connection = new TcpClient();

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
            catch { ConnChk(); }
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
            connection?.GetStream().Write(data, 0, data.Length);
            List<byte> outp = new List<byte>();
            do
            {
                byte[] buff = new byte[bufferSize];
                connection?.GetStream().Read(buff, 0, bufferSize);
                outp.AddRange(buff);
            } while ((bool)connection?.GetStream()?.DataAvailable);
            return outp.ToArray();
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
            await connection?.GetStream().WriteAsync(data, 0, data.Length);
            List<byte> outp = new List<byte>();
            do
            {
                byte[] buff = new byte[bufferSize];
                await connection?.GetStream().ReadAsync(buff, 0, bufferSize);
                outp.AddRange(buff);
            } while ((bool)connection?.GetStream()?.DataAvailable);
            return outp.ToArray();
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
        void ConnChk() { if ((bool)connection?.Connected) connection.Close(); }
        /// <summary>
        /// Realiza alguans tareas de limpieza antes de finalizar esta
        /// instalcia de la clase <see cref="Client"/>.
        /// </summary>
        ~Client() { ConnChk(); }
    }
}