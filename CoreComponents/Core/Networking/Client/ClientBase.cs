/*
ClientBase.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

En este archivo se define la clase base para todos los mecanismos de control
para clientes de red. Actualmente, MCART define 2 clases derivadas de ésta, una
que escucha activamente al servidor y otra que es un cliente pasivo.

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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using static TheXDS.MCART.Types.Extensions.StringExtensions;
using TcpClient = TheXDS.MCART.Types.Extensions.TcpClient;

#region Configuración de ReSharper

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable EventNeverSubscribedTo.Global

#endregion

namespace TheXDS.MCART.Networking.Client
{
    /// <summary>
    /// Clase base para implementar protocolos del lado del cliente.
    /// </summary>
    public abstract class ClientBase
    {
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ClientBase"/>.
        /// </summary>
        private protected ClientBase() { }

        /// <summary>
        ///     Obtiene de forma segura la instancia del
        ///     <see cref="NetworkStream"/> utilizada para la conexión con el
        ///     servidor remoto.
        /// </summary>
        /// <returns>
        ///     Un <see cref="NetworkStream"/> utilizado para la conexión con
        ///     el servidor remoto, o <see langword="null"/> si no existe una
        ///     conexión activa válida.
        /// </returns>
        protected NetworkStream NwStream()
        {
            try{return Connection?.GetStream();}
            catch{return null;}
        }

        /// <summary>
        /// Se produce cuando ocurre una conexión de manera satisfactoria.
        /// </summary>
        public event EventHandler<HostConnectionInfoEventArgs> Connected;

        /// <summary>
        /// Se produce si la conexión con el servidor ha fallado.
        /// </summary>
        public event EventHandler<ConnectionFailureEventArgs> ConnectionFailed;

        /// <summary>
        /// Conexión al servidor
        /// </summary>
        private protected readonly TcpClient Connection = new TcpClient();

        /// <summary>
        ///     Obtiene un valor que indica si la conexión con el servidor se
        ///     encuentra activa.
        /// </summary>
        /// <value>
        ///     <see langword="true"/> si la conexión se encuentra activa,
        ///     <see langword="false"/> en caso contrario.
        /// </value>
        public bool IsAlive => !(Connection?.Disposed ?? true) && !(NwStream() is null);

        /// <summary>
        ///     Método invalidable que es ejecutado inmediatamente después de
        ///     establecer una conexión con el servidor satisfactoriamente.
        /// </summary>
        protected abstract void PostConnection();

        private Thread _worker;

        #region Métodos de conexión

        /// <summary>
        /// Establece una conexión con el servidor de forma asíncrona.
        /// </summary>
        /// <returns>Un <see cref="Task"/> que representa la tarea.</returns>
        /// <param name="server">Servidor al cual conectarse.</param>
        public Task ConnectAsync(string server) => ConnectAsync(server, GetType().GetAttr<PortAttribute>()?.Value ?? Common.DefaultPort);
        /// <summary>
        /// Establece una conexión con el servidor de forma asíncrona.
        /// </summary>
        /// <returns>Un <see cref="Task"/> que representa la tarea.</returns>
        /// <param name="server">Servidor al cual conectarse.</param>
        public Task ConnectAsync(IPEndPoint server) => ConnectAsync(server.Address, server.Port);
        /// <summary>
        /// Establece una conexión con el servidor de forma asíncrona.
        /// </summary>
        /// <returns>Un <see cref="Task"/> que representa la tarea.</returns>
        /// <param name="server">Servidor al cual conectarse.</param>
        public Task ConnectAsync(IPAddress server) => ConnectAsync(server, GetType().GetAttr<PortAttribute>()?.Value ?? Common.DefaultPort);
        /// <summary>
        /// Establece una conexión con el servidor de forma asíncrona.
        /// </summary>
        /// <returns>Un <see cref="Task"/> que representa la tarea.</returns>
        /// <param name="server">Servidor al cual conectarse.</param>
        /// <param name="port">
        /// Opcional. Puerto del servidor. Si se omite, se conectará al puerto
        /// predeterminado.
        /// </param>
        public async Task ConnectAsync(IPAddress server, int port)
        {
            if (!port.IsBetween(1, 65535)) throw new ArgumentOutOfRangeException(nameof(port));
            try
            {
                await Connection.ConnectAsync(server, port);
                Connected?.Invoke(this, new HostConnectionInfoEventArgs(Connection));
                _worker = new Thread(PostConnection);
                _worker.Start();
            }
#if PreferExceptions
            catch { throw; }
#else
            catch (Exception ex)
            {
                CloseConnection();
                ConnectionFailed?.Invoke(this, new ConnectionFailureEventArgs(ex, server, port));
            }
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
        public async Task ConnectAsync(string server, int port)
        {
            if (server.IsEmpty()) throw new ArgumentNullException(nameof(server));
            if (!port.IsBetween(1, 65535)) throw new ArgumentOutOfRangeException(nameof(port));
            try
            {
                await Connection.ConnectAsync(server, port);
                Connected?.Invoke(this, new HostConnectionInfoEventArgs(server, Connection));
                _worker = new Thread(PostConnection);
                _worker.Start();
            }
#if PreferExceptions
            catch { throw; }
#else
            catch (Exception ex)
            {
                CloseConnection();
                ConnectionFailed?.Invoke(this, new ConnectionFailureEventArgs(ex, server, port));
            }
#endif
        }
        /// <summary>
        /// Establece una conexión con el servidor.
        /// </summary>
        /// <param name="server">Servidor al cual conectarse.</param>
        public void Connect(string server) => Connect(server, GetType().GetAttr<PortAttribute>()?.Value ?? Common.DefaultPort);
        /// <summary>
        /// Establece una conexión con el servidor.
        /// </summary>
        /// <param name="server">Servidor al cual conectarse.</param>
        public void Connect(IPEndPoint server) => Connect(server.Address, server.Port);
        /// <summary>
        /// Establece una conexión con el servidor.
        /// </summary>
        /// <param name="server">Servidor al cual conectarse.</param>
        public void Connect(IPAddress server) => Connect(server, GetType().GetAttr<PortAttribute>()?.Value ?? Common.DefaultPort);
        /// <summary>
        /// Establece una conexión con el servidor.
        /// </summary>
        /// <param name="server">Servidor al cual conectarse.</param>
        /// <param name="port">
        /// Opcional. Puerto del servidor. Si se omite, se conectará al puerto
        /// predeterminado.
        /// </param>
        public void Connect(IPAddress server, int port)
        {
            if (!port.IsBetween(1, 65535)) throw new ArgumentOutOfRangeException(nameof(port));
            try
            {
                Connection.Connect(server, port);
                Connected?.Invoke(this, new HostConnectionInfoEventArgs(Connection));
                _worker = new Thread(PostConnection);
                _worker.Start();
            }
#if PreferExceptions
            catch { throw; }
#else
            catch (Exception ex)
            {
                CloseConnection();
                ConnectionFailed?.Invoke(this, new ConnectionFailureEventArgs(ex, server, port));
            }
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
        public void Connect(string server, int port)
        {
            if (server.IsEmpty()) throw new ArgumentNullException(nameof(server));
            if (!port.IsBetween(1, 65535)) throw new ArgumentOutOfRangeException(nameof(port));
            try
            {
                Connection.Connect(server, port);
                Connected?.Invoke(this, new HostConnectionInfoEventArgs(server, Connection));
                _worker = new Thread(PostConnection);
                _worker.Start();
            }
#if PreferExceptions
            catch { throw; }
#else
            catch (Exception ex)
            {
                CloseConnection();
                ConnectionFailed?.Invoke(this, new ConnectionFailureEventArgs(ex, server, port));
            }
#endif
        }

        #endregion

        /// <summary>
        /// Se asegura de cerrar la conexión.
        /// </summary>
        public void CloseConnection()
        {
            if (!IsAlive) return;
            try
            {
                _worker.Abort();
                NwStream()?.Dispose();
                Connection?.Close();
            }
            catch { /* suprimir cualquier excepción */ }
        }
        /// <summary>
        /// Realiza algunas tareas de limpieza antes de finalizar esta
        /// instancia de la clase <see cref="ClientBase"/>.
        /// </summary>
        ~ClientBase() { CloseConnection(); }

        /// <summary>
        /// Obtiene un paquete completo de datos desde el servidor.
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        protected async Task<byte[]> GetDataAsync(NetworkStream ns)
        {
            var outp = new List<byte>();
            try
            {
                do
                {
                    var buff = new byte[Connection.ReceiveBufferSize];
                    var sze = await ns.ReadAsync(buff, 0, buff.Length);
                    if (sze < Connection.ReceiveBufferSize) Array.Resize(ref buff, sze);
                    outp.AddRange(buff);
                } while (ns.DataAvailable);
            }
            catch { outp.Clear(); }
            return outp.ToArray();
        }

        /// <summary>
        /// Obtiene un paquete completo de datos desde el servidor.
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        protected byte[] GetData(NetworkStream ns)
        {
            var outp = new List<byte>();
            try
            {
                do
                {
                    var buff = new byte[Connection.ReceiveBufferSize];
                    var sze = ns.Read(buff, 0, buff.Length);
                    if (sze < Connection.ReceiveBufferSize) Array.Resize(ref buff, sze);
                    outp.AddRange(buff);
                } while (ns.DataAvailable);
            }
            catch { outp.Clear(); }
            return outp.ToArray();
        }
    }
}