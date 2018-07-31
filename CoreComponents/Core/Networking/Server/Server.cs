/*
Server.cs

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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using static TheXDS.MCART.Types.Extensions.TypeExtensions;
using St = TheXDS.MCART.Resources.Strings;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable UnusedMember.Global

namespace TheXDS.MCART.Networking.Server
{
    /// <inheritdoc />
    /// <summary>
    /// Controla conexiones entrantes y ejecuta protocolos sobre los clientes
    /// que se conecten al servidor.
    /// </summary>
    public class Server : Server<Client>
    {
        /// <summary>
        /// Convierte implícitamente un <see cref="Protocol{Client}"/> en un
        /// <see cref="Server"/>.
        /// </summary>
        /// <param name="p">
        /// <see cref="Protocol{Client}"/> a convertir.
        /// </param>
        /// <returns>
        /// Un <see cref="Server"/> que utiliza el protocolo especificado para
        /// atender a los clientes.
        /// </returns>
        public static implicit operator Server(Protocol<Client> p) => new Server(p);
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Networking.Server.Server" />.
        /// </summary>
        /// <param name="protocol">
        /// Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <param name="ep">
        /// <see cref="T:System.Net.IPEndPoint" /> local a escuchar. Si se omite, se
        /// escuchará el puerto <see cref="F:TheXDS.MCART.Networking.Server.Server`1.DefaultPort" /> de
        /// todas las direcciones IP del servidor.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// Se produce si <paramref name="protocol" /> es <see langword="null" />.
        /// </exception>
        public Server(Protocol<Client> protocol, IPEndPoint ep = null) : base(protocol, ep) { }
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Networking.Server.Server`1" />.
        /// </summary>
        /// <param name="protocol">
        /// Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <param name="port">Puerto local a escuchar.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// Se produce si <paramref name="protocol" /> es <see langword="null" />.
        /// </exception>
        public Server(Protocol<Client> protocol, int port) : base(protocol, port) { }
    }
    /// <summary>
    /// Controla conexiones entrantes y ejecuta protocolos sobre los clientes
    /// que se conecten al servidor.
    /// </summary>
    /// <typeparam name="TClient">
    /// Tipo de <see cref="Client"/> que este servidor atiende.
    /// </typeparam>
    public class Server<TClient> where TClient : Client
    {
        /// <summary>
        /// Convierte implícitamente un <see cref="Protocol{TClient}"/> en un
        /// <see cref="Server{TClient}"/>.
        /// </summary>
        /// <param name="p">
        /// <see cref="Protocol{TClient}"/> a convertir.
        /// </param>
        /// <returns>
        /// Un <see cref="Server{TClient}"/> que utiliza el protocolo 
        /// especificado para atender a los clientes.
        /// </returns>
        public static implicit operator Server<TClient>(Protocol<TClient> p) => new Server<TClient>(p);

        /// <summary>
        /// Tiempo de espera en milisegundos antes de realizar una desconexión
        /// forzada al cerrar el servidor.
        /// </summary>
        private const int DisconnectionTimeout = 15000;
        /// <summary>
        /// Puerto predeterminado para las conexiones entrantes.
        /// </summary>
        public const int DefaultPort = 51220;

        /// <summary>
        /// Lista de hilos atendiendo a clientes.
        /// </summary>
        private readonly List<Task> _clwaiter = new List<Task>();
        /// <summary>
        /// Escucha de conexiones entrantes.
        /// </summary>
        private readonly TcpListener _conns;
        /// <summary>
        /// campo que determina si el servidor está escuchando conexiones y
        /// sirviendo a clientes (vivo)
        /// </summary>
        private bool _isAlive;
        /// <summary>
        /// Lista de clientes conectados.
        /// </summary>
        private readonly List<TClient> _clients = new List<TClient>();

        /// <summary>
        /// Lista de objetos <see cref="Client"/> conectados a este servidor.
        /// </summary>
        public IReadOnlyCollection<TClient> Clients => _clients.AsReadOnly();
        /// <summary>
        /// Instancia de protocolos a utilizar para dar servicio a los
        /// clientes que se conecten a este servidor.
        /// </summary>
        public Protocol<TClient> Protocol { get; }
        /// <summary>
        /// Dirección IP a la cual este servidor escucha.
        /// </summary>
        public IPEndPoint ListeningEndPoint { get; }
        /// <summary>
        /// Obtiene o establece un valor que indica si este
        /// <see cref="Server"/> está activo (vivo).
        /// </summary>
        /// <value><see langword="true"/> si está vivo; sino, <see langword="false"/>.</value>
        public bool IsAlive
        {
            get => _isAlive;
            set { if (!_isAlive && value) Start(); }
        }

        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Networking.Server.Server`1" />.
        /// </summary>
        /// <param name="protocol">
        /// Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// Se produce si <paramref name="protocol" /> es <see langword="null" />.
        /// </exception>
        public Server(Protocol<TClient> protocol) : this(protocol, null) { }
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Networking.Server.Server`1" />.
        /// </summary>
        /// <param name="protocol">
        /// Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <param name="port">Puerto local a escuchar.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// Se produce si <paramref name="protocol" /> es <see langword="null" />.
        /// </exception>
        public Server(Protocol<TClient> protocol, int port) : this(protocol, new IPEndPoint(IPAddress.Any, port)) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Server{TClient}"/>.
        /// </summary>
        /// <param name="protocol">
        /// Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <param name="ep">
        /// <see cref="IPEndPoint"/> local a escuchar. Si se establece en
        /// <see langword="null"/>, se escuchará al puerto predeterminado del
        /// protocolo (indicado por el atributo <see cref="PortAttribute"/>) o
        /// <see cref="DefaultPort"/> de todas las direcciones IP del servidor.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="protocol"/> es <see langword="null"/>.
        /// </exception>
        public Server(Protocol<TClient> protocol, IPEndPoint ep)
        {
            Protocol = protocol ?? throw new ArgumentNullException(nameof(protocol));
#if DEBUG
            if (Protocol.HasAttr<Attributes.BetaAttribute>())
                Debug.Print(St.Warn(St.XIsBeta(Protocol.GetType().Name)));
#endif
            ListeningEndPoint = ep ?? new IPEndPoint(IPAddress.Any, Protocol.GetAttr<PortAttribute>()?.Value ?? DefaultPort);
            _conns = new TcpListener(ListeningEndPoint);
        }

        /// <summary>
        /// Encapsula <see cref="TcpListener.AcceptTcpClientAsync"/> para
        /// permitir cancelar la tarea cuando el servidor se detenga.
        /// </summary>
        /// <returns>
        /// El <see cref="TcpClient"/> conectado. Si el servidor se detiene, se
        /// devuelve <see langword="null"/>.
        /// </returns>
        private async Task<TcpClient> GetClient()
        {
            //Necesario para poder detener el lambda
            //de espera sin matar al servidor.
            var bewaiting = true;
            try
            {
                var t = _conns.AcceptTcpClientAsync();
                if (t == await Task.WhenAny(t, Task.Run(() => { while (_isAlive && bewaiting) ; })))
                    //devolver al cliente que se obtenga del hilo de aceptación...
                    return await t;
            }
#if PreferExceptions
				catch { throw; }
#endif
            finally { bewaiting = false; } //detener el lambda de espera...

            // Devolver null. BeAlive() se encarga de manejar correctamente esto
            return null;
        }
        /// <summary>
        /// Atiende al cliente.
        /// </summary>
        /// <returns>Un <see cref="Task"/> que atiende al cliente.</returns>
        /// <param name="client">Cliente a atender.</param>
        private async Task AttendClient(TClient client)
        {
            _clients.Add(client);
            if (Protocol.ClientWelcome(client, this))
            {
                /*
				ClientBye será llamado por una función dentro de la clase
				Client, que será accesible por el código del usuario o la
				implementación del protocolo. La función ClientDisconnect será
				llamada cuando se detecte que la conexión ha finalizado sin
				llamar a la función de desconexión.
				*/
                while (client.IsAlive)
                {
                    var ts = client.RecieveAsync();
                    var wdat = true;
                    if (Task.WaitAny(ts, Task.Run(() =>
                    {
                        while (_isAlive && wdat) ;
                    })) == 0)
                    {
                        if (ts.Result.Length != 0)
                            Protocol.ClientAttendant(client, this, await ts);
                        else
                        {
                            Protocol.ClientDisconnect(client,this);
                            //client.TcpClient.Close();
                        }
                    }
                    else
                        ts.Dispose();
                    
                    wdat = false;
                }
            }
            else if (client?.IsAlive ?? false)
            {
                try
                {
                    //client.TcpClient.GetStream().Close();
                    client.TcpClient.Close();
                }
                catch { /* Silenciar excepción */ }
            }
            if ((bool) _clients?.Contains(client)) _clients.Remove(client);
        }
        /// <summary>
        /// Hilo de escucha y atención del servidor.
        /// </summary>
        private async void BeAlive()
        {
            while (_isAlive)
            {
                var c = await GetClient();
                if (!(c is null))
                    _clwaiter.Add(AttendClient(typeof(TClient).New(c, this) as TClient));
            }
        }

        /// <summary>
        /// Crea un hilo de ejecución que da servicio a los clientes
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
            _conns.Start();

            BeAlive();
        }
        /// <summary>
        /// Detiene al servidor de forma asíncrona.
        /// </summary>
        /// <returns>
        /// Un <see cref="Task"/> que puede utilizarse para monitorear el
        /// estado de esta tarea.
        /// </returns>
        public async Task StopAsync()
        {
            _conns.Stop();
            _isAlive = false;
            await Task.WhenAny(
                Task.WhenAll(_clwaiter),
                new Task(() => Thread.Sleep(DisconnectionTimeout)));
            foreach (var j in Clients) j.TcpClient.Close();
        }
        /// <summary>
        /// Detiene al servidor.
        /// </summary>
        public void Stop()
        {
            _conns.Stop();
            _isAlive = false;
            Task.WhenAll(_clwaiter).Wait(DisconnectionTimeout);
            foreach (var j in Clients) j.TcpClient.Close();
        }

        #region Helpers
        /// <summary>
        /// Envía un mensaje a todos los clientes.
        /// </summary>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        public void Broadcast(byte[] data) => Broadcast(data, null);
        /// <summary>
        /// Envía un mensaje a todos los clientes, excepto el especificado.
        /// </summary>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        /// <param name="client">Cliente que envía los datos.</param>
        public void Broadcast(byte[] data, Client client) => Multicast(data, client.IsNot);
        /// <summary>
        /// Envía un mensaje a todos los clientes que satisfacen la
        /// condición especificada por <paramref name="condition"/>.
        /// </summary>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        /// <param name="condition">
        /// Condición que determina a los clientes que recibirán el mensaje.
        /// </param>
        public void Multicast(byte[] data, Predicate<Client> condition)
        {
            foreach (var j in Clients) if (condition(j)) j.Send(data);
        }
        /// <summary>
        /// Envía un mensaje a todos los clientes.
        /// </summary>
        /// <returns>
        /// Un <see cref="Task"/> que representa esta tarea.
        /// </returns>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        public Task BroadcastAsync(byte[] data) => BroadcastAsync(data, null);
        /// <summary>
        /// Envía un mensaje a todos los clientes, excepto el especificado.
        /// </summary>
        /// <returns>
        /// Un <see cref="Task"/> que representa esta tarea.
        /// </returns>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        /// <param name="client">Cliente que envía los datos.</param>
        public Task BroadcastAsync(byte[] data, Client client) => MulticastAsync(data, client.IsNot);
        /// <summary>
        /// Envía un mensaje a todos los clientes que satisfacen la
        /// condición especificada por <paramref name="condition"/>.
        /// </summary>
        /// <returns>
        /// Un <see cref="Task"/> que representa esta tarea.
        /// </returns>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        /// <param name="condition">
        /// Condición que determina a los clientes que recibirán el mensaje.
        /// </param>
        public Task MulticastAsync(byte[] data, Predicate<Client> condition)
        {
            var w = new HashSet<Task>();
            foreach (var j in Clients)
                if (condition(j)) w.Add(j.SendAsync(data));
            return Task.WhenAll(w);
        }
        #endregion
    }
}