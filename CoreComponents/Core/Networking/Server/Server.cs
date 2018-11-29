/*
Server.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene la implementación de servidor de MCART.

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
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TheXDS.MCART.Events;
using TheXDS.MCART.Exceptions;
using static TheXDS.MCART.Networking.Common;

#region Configuración de ReSharper

// ReSharper disable EventNeverSubscribedTo.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable UnusedMember.Global

#endregion

namespace TheXDS.MCART.Networking.Server
{
    /// <inheritdoc />
    /// <summary>
    ///     Controla conexiones entrantes y ejecuta protocolos sobre los clientes
    ///     que se conecten al servidor.
    /// </summary>
    public class Server<TClient> : Server where TClient : Client
    {
        [DebuggerStepThrough]
        private static void CheckProtocolType(IProtocol protocol)
        {
            var tClient = protocol.GetType().BaseType?.GenericTypeArguments.FirstOrDefault(p => typeof(Client).IsAssignableFrom(p)) ?? typeof(Client);
            if (!typeof(TClient).IsAssignableFrom(tClient))
            {
                throw new InvalidTypeException();
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Networking.Server.Server`1" />.
        /// </summary>
        /// <param name="protocol">
        ///     Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     Se produce si <paramref name="protocol" /> es <see langword="null" />.
        /// </exception>
        [DebuggerStepThrough]
        public Server(IProtocol protocol) : base(protocol)
        {
            CheckProtocolType(protocol);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Networking.Server.Server`1" />.
        /// </summary>
        /// <param name="protocol">
        ///     Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <param name="port">Puerto local a escuchar.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     Se produce si <paramref name="protocol" /> es <see langword="null" />.
        /// </exception>
        [DebuggerStepThrough]
        public Server(IProtocol protocol, int port) : base(protocol, port)
        {
            CheckProtocolType(protocol);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Networking.Server.Server" />.
        /// </summary>
        /// <param name="protocol">
        ///     Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <param name="ep">
        ///     <see cref="T:System.Net.IPEndPoint" /> local a escuchar. Si se establece en
        ///     <see langword="null" />, se escuchará al puerto predeterminado del
        ///     protocolo (indicado por el atributo <see cref="T:TheXDS.MCART.Networking.PortAttribute" />) o
        ///     <see cref="F:TheXDS.MCART.Networking.Common.DefaultPort" /> de todas las direcciones IP del servidor.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     Se produce si <paramref name="protocol" /> es <see langword="null" />.
        /// </exception>
        [DebuggerStepThrough]
        public Server(IProtocol protocol, IPEndPoint ep) : base(protocol, ep)
        {
            CheckProtocolType(protocol);
        }

        /// <summary>
        ///     Lista de objetos <typeparamref name="TClient"/> conectados a
        ///     este servidor.
        /// </summary>
        public new IEnumerable<TClient> Clients => base.Clients.OfType<TClient>();
    }

    /// <summary>
    ///     Controla conexiones entrantes y ejecuta protocolos sobre los clientes
    ///     que se conecten al servidor.
    /// </summary>
    public class Server
    {
        /// <summary>
        ///     Lista de clientes conectados.
        /// </summary>
        private readonly HashSet<Client> _clients = new HashSet<Client>();

        /// <summary>
        ///     Lista de hilos atendiendo a clientes.
        /// </summary>
        private readonly HashSet<Task> _clientThreads = new HashSet<Task>();

        /// <summary>
        ///     Escucha de conexiones entrantes.
        /// </summary>
        private readonly TcpListener _listener;

        /// <summary>
        ///     campo que determina si el servidor está escuchando conexiones y
        ///     sirviendo a clientes (vivo)
        /// </summary>
        private bool _isAlive;

        /// <summary>
        ///     Lista de objetos <see cref="Client" /> conectados a este servidor.
        /// </summary>
        public IEnumerable<Client> Clients => _clients;

        /// <summary>
        ///     Obtiene o establece un valor que indica si este
        ///     <see cref="Server" /> está activo (vivo).
        /// </summary>
        /// <value><see langword="true" /> si está vivo; sino, <see langword="false" />.</value>
        public bool IsAlive
        {
            get => _isAlive;
            set
            {
                if (!_isAlive && value) Start();
                if (_isAlive && !value) Stop();
            }
        }

        /// <summary>
        ///     Dirección IP a la cual este servidor escucha.
        /// </summary>
        public IPEndPoint ListeningEndPoint { get; }

        /// <summary>
        ///     Instancia de protocolos a utilizar para dar servicio a los
        ///     clientes que se conecten a este servidor.
        /// </summary>
        public IProtocol Protocol { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Networking.Server.Server`1" />.
        /// </summary>
        /// <param name="protocol">
        ///     Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     Se produce si <paramref name="protocol" /> es <see langword="null" />.
        /// </exception>
        public Server(IProtocol protocol) : this(protocol, null)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Networking.Server.Server`1" />.
        /// </summary>
        /// <param name="protocol">
        ///     Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <param name="port">Puerto local a escuchar.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     Se produce si <paramref name="protocol" /> es <see langword="null" />.
        /// </exception>
        public Server(IProtocol protocol, int port) : this(protocol, new IPEndPoint(IPAddress.Any, port))
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="Server" />.
        /// </summary>
        /// <param name="protocol">
        ///     Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <param name="ep">
        ///     <see cref="IPEndPoint" /> local a escuchar. Si se establece en
        ///     <see langword="null" />, se escuchará al puerto predeterminado del
        ///     protocolo (indicado por el atributo <see cref="PortAttribute" />) o
        ///     <see cref="DefaultPort" /> de todas las direcciones IP del servidor.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Se produce si <paramref name="protocol" /> es <see langword="null" />.
        /// </exception>
        public Server(IProtocol protocol, IPEndPoint ep)
        {
            Protocol = protocol ?? throw new ArgumentNullException(nameof(protocol));
            if (Protocol is IServerProtocol p) p.MyServer = this;
            ListeningEndPoint = ep ?? new IPEndPoint(IPAddress.Any, Protocol.GetAttr<PortAttribute>()?.Value ?? DefaultPort);
            _listener = new TcpListener(ListeningEndPoint);
        }

        /// <summary>
        ///     Encapsula una tarea para devolver un arreglo de bytes vacío
        ///     cuando ocurra una excepción.
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private static byte[] GetResponse(Task<byte[]> task)
        {
            try
            {
                return task.Result;
            }
            catch (AggregateException)
            {
                return new byte[0];
            }
        }

        /// <summary>
        ///     Atiende al cliente.
        /// </summary>
        /// <returns>Un <see cref="Task" /> que atiende al cliente.</returns>
        /// <param name="client">Cliente a atender.</param>
        private void AttendClient(Client client)
        {
            ClientConnected?.Invoke(this, client);
            if (Protocol.ClientWelcome(client))
            {
                ClientAccepted?.Invoke(this, client);
                _clients.Add(client);
                while (client.IsAlive)
                {
                    var ts = client.RecieveAsync();
                    var waitData = true;
                    if (Task.WaitAny(ts, Task.Run(() =>
                    {
                        // ReSharper disable once AccessToModifiedClosure
                        // ReSharper disable once EmptyEmbeddedStatement
                        while (_isAlive && waitData) ;
                    })) == 0)
                    {
                        var r = GetResponse(ts);
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
                            waitData = false;
                            break;
                        }
                    }

                    waitData = false;
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
                try { client.Disconnect(); }
                catch { /* Silenciar excepción */ }
            }

            if (_clients.Contains(client)) _clients.Remove(client);
        }

        /// <summary>
        ///     Hilo de escucha y atención del servidor.
        /// </summary>
        private async void BeAlive()
        {
            while (_isAlive)
            {
                var c = await GetClient();
                if (c is null) continue;
                _clientThreads.Add(Task.Run(() => AttendClient(Protocol.CreateClient(c))));
            }
        }

        /// <summary>
        ///     Ocurre cuando el protocolo ha aceptado al nuevo cliente.
        /// </summary>
        public event EventHandler<ValueEventArgs<Client>> ClientAccepted;

        /// <summary>
        ///     Ocurre cuando un nuevo cliente se conecta al servidor.
        /// </summary>
        public event EventHandler<ValueEventArgs<Client>> ClientConnected;

        /// <summary>
        ///     Ocurre cuando un cliente se desconecta correctamente.
        /// </summary>
        public event EventHandler<ValueEventArgs<Client>> ClientDisconnected;

        /// <summary>
        ///     Ocurre cuando el protocolo despide a un cliente.
        /// </summary>
        public event EventHandler<ValueEventArgs<Client>> ClientFarewell;

        /// <summary>
        ///     Ocurre cuando un cliente se desconecta inesperadamente.
        /// </summary>
        public event EventHandler<ValueEventArgs<Client>> ClientLost;

        /// <summary>
        ///     Ocurre cuando el protocolo rechaza al nuevo cliente.
        /// </summary>
        public event EventHandler<ValueEventArgs<Client>> ClientRejected;

        /// <summary>
        ///     Encapsula <see cref="TcpListener.AcceptTcpClientAsync" /> para
        ///     permitir cancelar la tarea cuando el servidor se detenga.
        /// </summary>
        /// <returns>
        ///     El <see cref="TcpClient" /> conectado. Si el servidor se detiene, se
        ///     devuelve <see langword="null" />.
        /// </returns>
        private async Task<TcpClient> GetClient()
        {
            //Necesario para poder detener el lambda
            //de espera sin matar al servidor.
            var bewaiting = true;
            try
            {
                var t = _listener.AcceptTcpClientAsync();
                // ReSharper disable once AccessToModifiedClosure
                // ReSharper disable once EmptyEmbeddedStatement
                if (t == await Task.WhenAny(t, Task.Run(() =>
                    {
                        while (_isAlive && bewaiting) ;
                    })))
                    //devolver al cliente que se obtenga del hilo de aceptación...
                    return await t;
            }
#if PreferExceptions
				catch { throw; }
#endif
            finally
            {
                bewaiting = false;
            } //detener el lambda de espera...

            // Devolver null. BeAlive() se encarga de manejar correctamente esto
            return null;
        }

        /// <summary>
        ///     Ocurre cuando el servidor es iniciado.
        /// </summary>
        public event EventHandler<ValueEventArgs<DateTime>> ServerStarted;

        /// <summary>
        ///     Ocurre cuando el servidor es detenido.
        /// </summary>
        public event EventHandler<ValueEventArgs<DateTime>> ServerStopped;

        /// <summary>
        ///     Crea un hilo de ejecución que da servicio a los clientes
        /// </summary>
        public void Start()
        {
            if (_isAlive)
            {
#if PreferExceptions
				throw new InvalidOperationException(St.XAlreadyStarted(St.TheSrv));
#else
                return;
#endif
            }

            _isAlive = true;
            _listener.Start();
            ServerStarted?.Invoke(this, DateTime.Now);
            BeAlive();
        }

        /// <summary>
        ///     Detiene al servidor.
        /// </summary>
        public void Stop()
        {
            _listener.Stop();
            _isAlive = false;
            ServerStopped?.Invoke(this, DateTime.Now);
            Task.WhenAll(_clientThreads).Wait(DisconnectionTimeout);

            while (_clients.Count > 0)
            {
                try
                {
                    _clients.FirstOrDefault()?.Disconnect();
                }
                catch { /* Silenciar la excepción */ }
            }
        }

        /// <summary>
        ///     Detiene al servidor de forma asíncrona.
        /// </summary>
        /// <returns>
        ///     Un <see cref="Task" /> que puede utilizarse para monitorear el
        ///     estado de esta tarea.
        /// </returns>
        public async Task StopAsync()
        {
            _listener.Stop();
            _isAlive = false;
            ServerStopped?.Invoke(this, DateTime.Now);
            await Task.WhenAny(
                Task.WhenAll(_clientThreads),
                Task.Delay(DisconnectionTimeout));
            while (Clients.Any())
            {
                try
                {
                    Clients.FirstOrDefault()?.Disconnect();
                }
                catch { /* Silenciar la excepción */ }
            }
        }

        #region Helpers

        /// <summary>
        ///     Envía un mensaje a todos los clientes.
        /// </summary>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        public void Broadcast(byte[] data)
        {
            Broadcast(data, null);
        }

        /// <summary>
        ///     Envía un mensaje a todos los clientes, excepto el especificado.
        /// </summary>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        /// <param name="client">Cliente que envía los datos.</param>
        public void Broadcast(byte[] data, Client client)
        {
            Multicast(data, client.IsNot);
        }

        /// <summary>
        ///     Envía un mensaje a todos los clientes que satisfacen la
        ///     condición especificada por <paramref name="condition" />.
        /// </summary>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        /// <param name="condition">
        ///     Condición que determina a los clientes que recibirán el mensaje.
        /// </param>
        public void Multicast(byte[] data, Predicate<Client> condition)
        {
            foreach (var j in Clients)
                if (condition(j))
                    j.Send(data);
        }

        /// <summary>
        ///     Envía un mensaje a todos los clientes.
        /// </summary>
        /// <returns>
        ///     Un <see cref="Task" /> que representa esta tarea.
        /// </returns>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        public Task BroadcastAsync(byte[] data)
        {
            return BroadcastAsync(data, null);
        }

        /// <summary>
        ///     Envía un mensaje a todos los clientes, excepto el especificado.
        /// </summary>
        /// <returns>
        ///     Un <see cref="Task" /> que representa esta tarea.
        /// </returns>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        /// <param name="client">Cliente que envía los datos.</param>
        public Task BroadcastAsync(byte[] data, Client client)
        {
            return MulticastAsync(data, client.IsNot);
        }

        /// <summary>
        ///     Envía un mensaje a todos los clientes que satisfacen la
        ///     condición especificada por <paramref name="condition" />.
        /// </summary>
        /// <returns>
        ///     Un <see cref="Task" /> que representa esta tarea.
        /// </returns>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        /// <param name="condition">
        ///     Condición que determina a los clientes que recibirán el mensaje.
        /// </param>
        public Task MulticastAsync(byte[] data, Predicate<Client> condition)
        {
            var w = new HashSet<Task>();
            foreach (var j in Clients)
                if (condition(j))
                    w.Add(j.SendAsync(data));
            return Task.WhenAll(w);
        }

        #endregion
    }
}