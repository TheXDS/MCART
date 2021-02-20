/*
ClientBase.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

En este archivo se define la clase base para todos los mecanismos de control
para clientes de red. Actualmente, MCART define 2 clases derivadas de ésta, una
que escucha activamente al servidor y otra que es un cliente pasivo.

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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using TheXDS.MCART.Types;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Networking.Legacy.Client
{
    /// <summary>
    /// Clase base para implementar protocolos del lado del cliente.
    /// </summary>
    public abstract class ClientBase
    {
        private Task? _worker;

        /// <summary>
        /// Obtiene un valor que indica si la conexión con el servidor se
        /// encuentra activa.
        /// </summary>
        /// <value>
        /// <see langword="true" /> si la conexión se encuentra activa,
        /// <see langword="false" /> en caso contrario.
        /// </value>
        public bool IsAlive => !(Connection?.IsDisposed ?? true) && !(NwStream() is null);

        /// <summary>
        /// Conexión al servidor
        /// </summary>
        private protected TcpClientEx Connection { get; private set; } = new TcpClientEx();

        private int DefaultPort => GetType().GetAttr<PortAttribute>()?.Value ?? Common.DefaultPort;

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ClientBase" />.
        /// </summary>
        private protected ClientBase()
        {
        }

        /// <summary>
        /// Método invalidable que es ejecutado inmediatamente después de
        /// establecer una conexión con el servidor satisfactoriamente.
        /// </summary>
        protected abstract void PostConnection();

        /// <summary>
        /// Realiza algunas tareas de limpieza antes de finalizar esta
        /// instancia de la clase <see cref="ClientBase" />.
        /// </summary>
        ~ClientBase()
        {
            CloseConnection();
        }

        /// <summary>
        /// Se asegura de cerrar la conexión.
        /// </summary>
        public void CloseConnection()
        {
            CloseConnection(false);
        }

        /// <summary>
        /// Se asegura de cerrar la conexión.
        /// </summary>
        /// <param name="force">
        /// Ignora el estado de la conexión e intenta cerrarla de todas formas.
        /// </param>
        public void CloseConnection(in bool force)
        {
            if (!IsAlive && !force) return;
            try
            {
                if (_worker?.Status == TaskStatus.Running) _worker!.Dispose();
                NwStream()?.Dispose();
                Connection?.Close();
            }
            catch { /* suprimir cualquier excepción */ }
        }

        /// <summary>
        /// Establece una conexión con el servidor.
        /// </summary>
        /// <param name="server">Servidor al cual conectarse.</param>
        public bool Connect(string server)
        {
            return Connect(server, DefaultPort);
        }

        /// <summary>
        /// Establece una conexión con el servidor.
        /// </summary>
        /// <param name="server">Servidor al cual conectarse.</param>
        public bool Connect(IPEndPoint server)
        {
            return Connect(server.Address, server.Port);
        }

        /// <summary>
        /// Establece una conexión con el servidor.
        /// </summary>
        /// <param name="server">Servidor al cual conectarse.</param>
        public bool Connect(IPAddress server)
        {
            return Connect(server, DefaultPort);
        }

        /// <summary>
        /// Establece una conexión con el servidor.
        /// </summary>
        /// <param name="server">Servidor al cual conectarse.</param>
        /// <param name="port">
        /// Opcional. Puerto del servidor. Si se omite, se conectará al puerto
        /// predeterminado.
        /// </param>
        public bool Connect(IPAddress server, int port)
        {
            if (!port.IsBetween(1, 65535)) throw new ArgumentOutOfRangeException(nameof(port));
            return Connect(c => c.Connect(server, port),
                () => new HostConnectionInfoEventArgs(Connection),
                ex => new ConnectionFailureEventArgs(ex, server, port));
        }

        /// <summary>
        /// Establece una conexión con el servidor.
        /// </summary>
        /// <param name="server">Servidor al cual conectarse.</param>
        /// <param name="port">
        /// Opcional. Puerto del servidor. Si se omite, se conectará al puerto
        /// predeterminado.
        /// </param>
        public bool Connect(string server, int port)
        {
            if (server.IsEmpty()) throw new ArgumentNullException(nameof(server));
            if (!port.IsBetween(1, 65535)) throw new ArgumentOutOfRangeException(nameof(port));
            return Connect(c => c.Connect(server, port),
                () => new HostConnectionInfoEventArgs(server, Connection),
                ex => new ConnectionFailureEventArgs(ex, server, port));
        }

        private bool Connect(Action<TcpClientEx> connect, Func<HostConnectionInfoEventArgs> connected, Func<Exception, ConnectionFailureEventArgs> failure)
        {
            try
            {
                CloseConnection();
                Connection = new TcpClientEx();
                connect(Connection);
                Connected?.Invoke(this, connected());
                _worker = Task.Run(PostConnection);
                _worker.Start();
                return true;
            }
            catch (Exception ex)
            {
                CloseConnection();
                ConnectionFailed?.Invoke(this, failure(ex));
                return false;
            }
        }

        /// <summary>
        /// Establece una conexión con el servidor de forma asíncrona.
        /// </summary>
        /// <returns>Un <see cref="Task" /> que representa la tarea.</returns>
        /// <param name="server">Servidor al cual conectarse.</param>
        public Task<bool> ConnectAsync(string server)
        {
            return ConnectAsync(server, DefaultPort);
        }

        /// <summary>
        /// Establece una conexión con el servidor de forma asíncrona.
        /// </summary>
        /// <returns>Un <see cref="Task" /> que representa la tarea.</returns>
        /// <param name="server">Servidor al cual conectarse.</param>
        public Task<bool> ConnectAsync(IPEndPoint server)
        {
            return ConnectAsync(server.Address, server.Port);
        }

        /// <summary>
        /// Establece una conexión con el servidor de forma asíncrona.
        /// </summary>
        /// <returns>Un <see cref="Task" /> que representa la tarea.</returns>
        /// <param name="server">Servidor al cual conectarse.</param>
        public Task<bool> ConnectAsync(IPAddress server)
        {
            return ConnectAsync(server, DefaultPort);
        }

        /// <summary>
        /// Establece una conexión con el servidor de forma asíncrona.
        /// </summary>
        /// <returns>Un <see cref="Task" /> que representa la tarea.</returns>
        /// <param name="server">Servidor al cual conectarse.</param>
        /// <param name="port">
        /// Opcional. Puerto del servidor. Si se omite, se conectará al puerto
        /// predeterminado.
        /// </param>
        public Task<bool> ConnectAsync(IPAddress server, int port)
        {
            if (!port.IsBetween(1, 65535)) throw new ArgumentOutOfRangeException(nameof(port));
            return ConnectAsync(c => c.ConnectAsync(server, port),
                () => new HostConnectionInfoEventArgs(Connection),
                ex => new ConnectionFailureEventArgs(ex, server, port));
        }

        /// <summary>
        /// Establece una conexión con el servidor de forma asíncrona.
        /// </summary>
        /// <returns>Un <see cref="Task" /> que representa la tarea.</returns>
        /// <param name="server">Servidor al cual conectarse.</param>
        /// <param name="port">
        /// Opcional. Puerto del servidor. Si se omite, se conectará al puerto
        /// predeterminado.
        /// </param>
        public Task<bool> ConnectAsync(string server, int port)
        {
            if (server.IsEmpty()) throw new ArgumentNullException(nameof(server));
            if (!port.IsBetween(1, 65535)) throw new ArgumentOutOfRangeException(nameof(port));

            return ConnectAsync(c => c.ConnectAsync(server, port),
                () => new HostConnectionInfoEventArgs(server, Connection),
                ex => new ConnectionFailureEventArgs(ex, server, port));
        }

        private async Task<bool> ConnectAsync(Func<TcpClientEx, Task> connect,
            Func<HostConnectionInfoEventArgs> connected,
            Func<Exception, ConnectionFailureEventArgs> failure)
        {
            try
            {
                CloseConnection();
                Connection = new TcpClientEx();
                await connect(Connection);
                Connected?.Invoke(this, connected());
                _worker = Task.Run(PostConnection);
                _worker.Start();
                return true;
            }
            catch (Exception ex)
            {
                CloseConnection();
                ConnectionFailed?.Invoke(this, failure(ex));
                return false;
            }
        }

        /// <summary>
        /// Se produce cuando ocurre una conexión de manera satisfactoria.
        /// </summary>
        public event EventHandler<HostConnectionInfoEventArgs>? Connected;

        /// <summary>
        /// Se produce si la conexión con el servidor ha fallado.
        /// </summary>
        public event EventHandler<ConnectionFailureEventArgs>? ConnectionFailed;

        /// <summary>
        /// Obtiene un paquete completo de datos desde el servidor.
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        protected byte[] GetData(NetworkStream ns)
        {
            var outp = new System.Collections.Generic.List<byte>();
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
            catch
            {
                outp.Clear();
            }

            return outp.ToArray();
        }

        /// <summary>
        /// Obtiene un paquete completo de datos desde el servidor.
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        protected async Task<byte[]> GetDataAsync(NetworkStream ns)
        {
            var outp = new System.Collections.Generic.List<byte>();
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
            catch
            {
                outp.Clear();
            }

            return outp.ToArray();
        }

        /// <summary>
        /// Obtiene de forma segura la instancia del
        /// <see cref="NetworkStream" /> utilizada para la conexión con el
        /// servidor remoto.
        /// </summary>
        /// <returns>
        /// Un <see cref="NetworkStream" /> utilizado para la conexión con
        /// el servidor remoto, o <see langword="null" /> si no existe una
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
    }
}