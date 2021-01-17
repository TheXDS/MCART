/*
Server.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene la implementación de servidor de MCART.

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
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using TheXDS.MCART.Events;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.Networking.Legacy.Common;

namespace TheXDS.MCART.Networking.Legacy.Server
{
    /// <summary>
    /// Controla conexiones entrantes y ejecuta protocolos sobre los clientes
    /// que se conecten al servidor.
    /// </summary>
    public class Server<TClient> : Disposable, IServer where TClient : Client
    {
        private readonly CancellationTokenSource _cancellation = new CancellationTokenSource();
        private readonly HashSet<TClient> _clients = new HashSet<TClient>();
        private readonly HashSet<Task> _clientThreads = new HashSet<Task>();
        private readonly TcpListener _listener;
        private Task? _aliveTask;

        [DebuggerStepThrough]
        private static void CheckProtocolType(IProtocol<TClient> protocol)
        {
            var tClient = protocol.GetType().BaseType?.GenericTypeArguments.FirstOrDefault(p => typeof(Client).IsAssignableFrom(p)) ?? typeof(Client);
            if (!typeof(TClient).IsAssignableFrom(tClient))
            {
                throw new InvalidTypeException();
            }
        }
        private static byte[] GetResponse(Task<byte[]> task)
        {
            try
            {
                return task.Result;
            }
            catch (AggregateException)
            {
                return Array.Empty<byte>();
            }
        }

        private void AttendClient(TClient client)
        {
            ClientConnected?.Invoke(this, client);
            if (Protocol.ClientWelcome(client))
            {
                ClientAccepted?.Invoke(this, client);
                _clients.Add(client);
                while (client.IsAlive)
                {
                    var r = GetResponse(client.RecieveAsync(_cancellation.Token));

                    if (!_cancellation.IsCancellationRequested)
                    {
                        if (r.Any())
                        {
                            Protocol.ClientAttendant(client, r);
                        }
                        else
                        {
                            ClientLost?.Invoke(this, client);
                            Protocol.ClientDisconnect(client);

                            /*
                             * HACK: Este bloque de código se asegura de romper
                             * el ciclo, ya que bajo algunas circunstancias
                             * extrañas relacionadas con la sincronización de
                             * hilos, el cliente no desecha correctamente 
                             */
                            client.Disconnect();
                            break;
                        }
                    }

                    if (!client.Disconnecting) continue;
                    ClientFarewell?.Invoke(this, client);
                    Protocol.ClientBye(client);
                    client.Disconnect();
                    ClientDisconnected?.Invoke(this, client);
                }
            }
            else if (client?.IsAlive ?? false)
            {
                ClientRejected?.Invoke(this, client);
                try { client?.Disconnect(); }
                catch { /* Silenciar excepción */ }
            }

            if (!(client is null) && _clients.Contains(client)) _clients.Remove(client);
        }
        private async Task BeAlive()
        {
            while (IsAlive)
            {
                var c = await GetClient();
                if (c is null) continue;
                _clientThreads.Add(Task.Run(() => AttendClient(Protocol.CreateClient(c) as TClient ?? throw new InvalidReturnValueException())));
            }
        }
        private async Task<TcpClient?> GetClient()
        {
            try
            {
                return await _listener.AcceptTcpClientAsync();
            }
            catch
            {
                // Devolver null. BeAlive() se encarga de manejar correctamente esto
                return null;
            }
        }
        private void Kill()
        {
            _listener.Stop();
            _aliveTask = null;
            _cancellation.Cancel();
            ServerStopped?.Invoke(this, DateTime.Now);
        }
        private void PurgeClients()
        {
            while (_clients.Count > 0)
            {
                try
                {
                    _clients.FirstOrDefault()?.Disconnect();
                }
                catch { /* Silenciar la excepción */ }
            }
        }

        /// <inheritdoc/>
        protected override void OnDispose()
        {
            if (IsAlive) Stop();
        }

        /// <summary>
        /// Lista de objetos <typeparamref name="TClient"/> conectados a
        /// este servidor.
        /// </summary>
        public IEnumerable<TClient> Clients => _clients;

        /// <summary>
        /// Obtiene o establece un valor que indica si este
        /// <see cref="Server" /> está activo (vivo).
        /// </summary>
        /// <value><see langword="true" /> si está vivo; sino, <see langword="false" />.</value>
        public bool IsAlive
        {
            get => _aliveTask is { };
            set
            {
                if (_aliveTask is null && value) Start();
                if (IsAlive && !value) Stop();
            }
        }

        /// <summary>
        /// Dirección IP a la cual este servidor escucha.
        /// </summary>
        public IPEndPoint ListeningEndPoint { get; }

        /// <summary>
        /// Instancia de protocolos a utilizar para dar servicio a los
        /// clientes que se conecten a este servidor.
        /// </summary>
        public IProtocol<TClient> Protocol { get; }

        IProtocol IServer.Protocol => Protocol;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Server{T}" />.
        /// </summary>
        /// <param name="protocol">
        /// Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// Se produce si <paramref name="protocol" /> es <see langword="null" />.
        /// </exception>
        public Server(IProtocol<TClient> protocol) : this(protocol, null)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Server{T}" />.
        /// </summary>
        /// <param name="protocol">
        /// Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <param name="port">Puerto local a escuchar.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// Se produce si <paramref name="protocol" /> es <see langword="null" />.
        /// </exception>
        public Server(IProtocol<TClient> protocol, int port) : this(protocol, new IPEndPoint(IPAddress.Any, port))
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Server{T}" />.
        /// </summary>
        /// <param name="protocol">
        /// Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <param name="ep">
        /// <see cref="IPEndPoint" /> local a escuchar. Si se establece en
        /// <see langword="null" />, se escuchará al puerto predeterminado del
        /// protocolo (indicado por el atributo <see cref="PortAttribute" />) o
        /// <see cref="DefaultPort" /> de todas las direcciones IP del servidor.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="protocol" /> es <see langword="null" />.
        /// </exception>
        public Server(IProtocol<TClient> protocol, IPEndPoint? ep)
        {
            CheckProtocolType(protocol);

            Protocol = protocol ?? throw new ArgumentNullException(nameof(protocol));
            if (Protocol is IServerProtocol<TClient> p) p.MyServer = this;
            ListeningEndPoint = ep ?? new IPEndPoint(IPAddress.Any, Protocol.GetAttr<PortAttribute>()?.Value ?? DefaultPort);
            _listener = new TcpListener(ListeningEndPoint);
        }

        /// <summary>
        /// Ocurre cuando el protocolo ha aceptado al nuevo cliente.
        /// </summary>
        public event EventHandler<ValueEventArgs<Client>>? ClientAccepted;

        /// <summary>
        /// Ocurre cuando un nuevo cliente se conecta al servidor.
        /// </summary>
        public event EventHandler<ValueEventArgs<Client>>? ClientConnected;

        /// <summary>
        /// Ocurre cuando un cliente se desconecta correctamente.
        /// </summary>
        public event EventHandler<ValueEventArgs<Client>>? ClientDisconnected;

        /// <summary>
        /// Ocurre cuando el protocolo despide a un cliente.
        /// </summary>
        public event EventHandler<ValueEventArgs<Client>>? ClientFarewell;

        /// <summary>
        /// Ocurre cuando un cliente se desconecta inesperadamente.
        /// </summary>
        public event EventHandler<ValueEventArgs<Client?>>? ClientLost;

        /// <summary>
        /// Ocurre cuando el protocolo rechaza al nuevo cliente.
        /// </summary>
        public event EventHandler<ValueEventArgs<Client?>>? ClientRejected;

        /// <summary>
        /// Ocurre cuando el servidor es iniciado.
        /// </summary>
        public event EventHandler<ValueEventArgs<DateTime>>? ServerStarted;

        /// <summary>
        /// Ocurre cuando el servidor es detenido.
        /// </summary>
        public event EventHandler<ValueEventArgs<DateTime>>? ServerStopped;

        /// <summary>
        /// Crea un hilo de ejecución que da servicio a los clientes
        /// </summary>
        public Task Start()
        {
            if (IsAlive) return _aliveTask!;
            _listener.Start();
            ServerStarted?.Invoke(this, DateTime.Now);
            return _aliveTask = BeAlive();
        }

        /// <summary>
        /// Detiene al servidor.
        /// </summary>
        public void Stop()
        {
            Kill();
            Task.WhenAll(_clientThreads).Wait(DisconnectionTimeout);
            PurgeClients();
        }

        /// <summary>
        /// Detiene al servidor de forma asíncrona.
        /// </summary>
        /// <returns>
        /// Un <see cref="Task" /> que puede utilizarse para monitorear el
        /// estado de esta tarea.
        /// </returns>
        public async Task StopAsync()
        {
            Kill();
            await Task.WhenAny(Task.WhenAll(_clientThreads), Task.Delay(DisconnectionTimeout));
            PurgeClients();
        }

        #region Helpers

        /// <summary>
        /// Envía un mensaje a todos los clientes.
        /// </summary>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        public void Broadcast(byte[] data)
        {
            Broadcast(data, null);
        }

        /// <summary>
        /// Envía un mensaje a todos los clientes, excepto el especificado.
        /// </summary>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        /// <param name="client">Cliente que envía los datos.</param>
        public void Broadcast(byte[] data, TClient? client)
        {
            Multicast(data, c => c?.IsNot(client) ?? true);
        }

        /// <summary>
        /// Envía un mensaje a todos los clientes que satisfacen la
        /// condición especificada por <paramref name="condition" />.
        /// </summary>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        /// <param name="condition">
        /// Condición que determina a los clientes que recibirán el mensaje.
        /// </param>
        public void Multicast(byte[] data, Predicate<TClient> condition)
        {
            foreach (var j in Clients)
                if (condition(j))
                    j.Send(data);
        }

        /// <summary>
        /// Envía un mensaje a todos los clientes.
        /// </summary>
        /// <returns>
        /// Un <see cref="Task" /> que representa esta tarea.
        /// </returns>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        public Task BroadcastAsync(byte[] data)
        {
            return BroadcastAsync(data, null);
        }

        /// <summary>
        /// Envía un mensaje a todos los clientes, excepto el especificado.
        /// </summary>
        /// <returns>
        /// Un <see cref="Task" /> que representa esta tarea.
        /// </returns>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        /// <param name="client">Cliente que envía los datos.</param>
        public Task BroadcastAsync(byte[] data, TClient? client)
        {
            return MulticastAsync(data, c => c?.IsNot(client) ?? true);
        }

        /// <summary>
        /// Envía un mensaje a todos los clientes que satisfacen la
        /// condición especificada por <paramref name="condition" />.
        /// </summary>
        /// <returns>
        /// Un <see cref="Task" /> que representa esta tarea.
        /// </returns>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        /// <param name="condition">
        /// Condición que determina a los clientes que recibirán el mensaje.
        /// </param>
        public Task MulticastAsync(byte[] data, Predicate<TClient> condition)
        {
            var w = new HashSet<Task>();
            foreach (var j in Clients)
                if (condition(j))
                    w.Add(j.SendAsync(data));
            return Task.WhenAll(w);
        }

        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            return Protocol.GetType().NameOf();
        }
    }

    /// <summary>
    /// Controla conexiones entrantes y ejecuta protocolos sobre los clientes
    /// que se conecten al servidor.
    /// </summary>
    public class Server : Server<Client>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Server" />.
        /// </summary>
        /// <param name="protocol">
        /// Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="protocol" /> es <see langword="null" />.
        /// </exception>
        public Server(IProtocol<Client> protocol) : base(protocol)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Server" />.
        /// </summary>
        /// <param name="protocol">
        /// Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <param name="port">Puerto local a escuchar.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// Se produce si <paramref name="protocol" /> es <see langword="null" />.
        /// </exception>
        public Server(IProtocol<Client> protocol, int port) : base(protocol, port)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Server" />.
        /// </summary>
        /// <param name="protocol">
        /// Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <param name="ep">
        /// <see cref="IPEndPoint" /> local a escuchar. Si se establece en
        /// <see langword="null" />, se escuchará al puerto predeterminado del
        /// protocolo (indicado por el atributo <see cref="PortAttribute" />) o
        /// <see cref="DefaultPort" /> de todas las direcciones IP del servidor.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="protocol" /> es <see langword="null" />.
        /// </exception>
        public Server(IProtocol<Client> protocol, IPEndPoint? ep) : base(protocol, ep)
        {
        }
    }
}