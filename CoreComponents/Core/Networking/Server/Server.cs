//
//  Server.cs
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

// Preferir excepciones en lugar de continuar con código alternativo
//#define PreferExceptions

// Desactiva explícitamente la contante TRACE
//#undef TRACE

#endregion

using System;
using System.Collections.Generic;
#if TRACE
using System.Diagnostics;
#endif
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
#if TRACE
using St = MCART.Resources.Strings;
#endif

namespace MCART.Networking.Server
{
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
        public static implicit operator Server(Protocol<Client> p) => new Server(p);

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Server"/>.
        /// </summary>
        /// <param name="protocol">
        /// Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <param name="ep">
        /// <see cref="IPEndPoint"/> local a escuchar. Si se omite, se
        /// escuchará el puerto <see cref="Server{TClient}.defaultPort"/> de
        /// todas las direcciones IP del servidor.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="protocol"/> es <c>null</c>.
        /// </exception>
        public Server(Protocol<Client> protocol, IPEndPoint ep = null)
            : base(protocol, ep) { }
    }
    /// <summary>
    /// Controla conexiones entrantes y ejecuta protocolos sobre los clientes
    /// que se conecten al servidor.
    /// </summary>
    public class Server<TClient> where TClient : Client
    {
        /// <summary>
        /// Tiempo de espera en milisegundos antes de realizar una desconexión
        /// forzada al cerrar el servidor.
        /// </summary>
        const int disconnectionTimeout = 15000;

        /// <summary>
        /// Convierte implícitamente un <see cref="Protocol{TClient}"/> en un
        /// <see cref="Server{TClient}"/>.
        /// </summary>
        /// <param name="p">
        /// <see cref="Protocol{TClient}"/> a convertir.
        /// </param>
        public static implicit operator Server<TClient>(Protocol<TClient> p) => new Server<TClient>(p);

        /// <summary>
        /// Puerto predeterminado para las conexiones entrantes.
        /// </summary>
        public const int defaultPort = 51220;

        /// <summary>
        /// Lista de objetos <see cref="Client"/> conectados a este servidor.
        /// </summary>
        public List<TClient> Clients = new List<TClient>();

        /// <summary>
        /// Instancia de protocolos a utilizar para dar servicio a los
        /// clientes que se conecten a este servidor.
        /// </summary>
        public Protocol<TClient> Protocol;

        /// <summary>
        /// Número de puerto al que este servidor escucha.
        /// </summary>
        public readonly int ListeningPort;

        /// <summary>
        /// Dirección IP a la cual este servidor escucha.
        /// </summary>
        public readonly IPEndPoint ListeningEndPoint;

        /// <summary>
        /// Lista de hilos atendiendo a clientes.
        /// </summary>
        List<Task> clwaiter = new List<Task>();

        /// <summary>
        /// Escucha de conexiones entrantes.
        /// </summary>
        TcpListener conns;

        /// <summary>
        /// campo que determina si el servidor está escuchando conexiones y
        /// sirviendo a clientes (vivo)
        /// </summary>
        bool _isAlive;

#if TRACE
        /// <summary>
        /// Controla el evento <see cref="Logging"/>.
        /// </summary>
        /// <param name="sender">Objeto que ha generado el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        public delegate void LogEventHandler(object sender, Events.LoggingEventArgs e);

        /// <summary>
        /// Ocurre cuando el servidor desea reportar un cambio de estado, o 
        /// enviar un mensaje a la interfaz del servidor.
        /// </summary>
        public event LogEventHandler Logging;

        /// <summary>
        /// Escribe un mensaje de Log en la salida del servidor.
        /// </summary>
        /// <param name="msg">Mensaje a escribir en el Log.</param>
        public void Log(string msg) => Logging?.Invoke(this, new Events.LoggingEventArgs(msg));
#endif

        /// <summary>
        /// Encapsula <see cref="TcpListener.AcceptTcpClientAsync"/> para
        /// permitir cancelar la tarea cuando el servidor se detenga.
        /// </summary>
        /// <returns>
        /// El <see cref="TcpClient"/> conectado. Si el servidor se detiene, se
        /// devuelve <c>null</c>.
        /// </returns>
        async Task<TcpClient> GetClient()
        {
            //Necesario para poder detener el lambda
            //de espera sin matar al servidor.
            bool bewaiting = true;
            try
            {
                Task<TcpClient> t = conns.AcceptTcpClientAsync();
#if TRACE
               Log(St.ClientConnected);
#endif
                if (t == await Task.WhenAny(t, Task.Run(() => { while (_isAlive && bewaiting) ; })))
                {
                    //detener el lambda de espera...
                    bewaiting = false;
                    //devolver tarea...
                    return await t;
                }
            }
#if PreferExceptions
                catch { throw; }
#endif
            finally { bewaiting = false; } //detener el lambda de espera...
#if TRACE
            Log(St.XStopped(St.TheListener));
#endif
            //Devolver null. Serve() se encarga de manejar correctamente esto
            return null;
        }

        /// <summary>
        /// Atiende al cliente.
        /// </summary>
        /// <returns>Un <see cref="Task"/> que atiende al cliente.</returns>
        /// <param name="client">Cliente a atender.</param>
        async Task AttendClient(TClient client)
        {
            Clients.Add(client);
            if (Protocol.ClientWelcome(client, this))
            {
#if TRACE
                Log(St.XAccepted(St.TheClient));
#endif

                /*
				ClientBye será llamado por una función
				dentro de la clase Client, que será
				accesible por el código del usuario o la
				implementación del protocolo. La función
				ClientDisconnect será llamada cuando se
				detecte que la conexión ha finalizado sin
				llamar a la función de desconexión.
				*/
                while (client.IsAlive)
                    Protocol.ClientAttendant(client, this, await client.RecieveAsync());
#if TRACE
                Log(St.XDisconnected(St.TheClient));
#endif
            }
            else
            {
#if TRACE
                Log(St.XRejected(St.TheClient));
#endif
                if ((bool)client?.IsAlive) client.TcpClient.Close();
            }
            if ((bool)Clients?.Contains(client)) Clients.Remove(client);
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Server{TClient}"/>.
        /// </summary>
        /// <param name="protocol">
        /// Conjunto de protocolos a utilizar para este servidor.
        /// </param>
        /// <param name="ep">
        /// <see cref="IPEndPoint"/> local a escuchar. Si se omite, se
        /// escuchará el puerto <see cref="defaultPort"/> de todas las
        /// direcciones IP del servidor.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="protocol"/> es <c>null</c>.
        /// </exception>
        public Server(Protocol<TClient> protocol, IPEndPoint ep = null)
        {
            if (protocol.IsNull()) throw new ArgumentNullException(nameof(protocol));
            Protocol = protocol;
#if TRACE
            if (!Protocol.GetAttr<Attributes.BetaAttribute>().IsNull())
                Debug.Print(St.Warn(St.XIsBeta(Protocol.GetType().Name)));
#endif
            ListeningPort = Protocol.GetAttr<PortAttribute>()?.Value ?? defaultPort;
            ListeningEndPoint = ep ?? new IPEndPoint(IPAddress.Any, ListeningPort);
            conns = new TcpListener(ListeningEndPoint);
        }

        /// <summary>
        /// Crea un hilo de ejecución que da servicio a los clientes
        /// </summary>
        public async Task Start()
        {
            if (_isAlive)
            {
#if TRACE
                Logging?.Invoke(this, new Events.LoggingEventArgs(St.XAlreadyStarted(St.TheSrv)));
#endif
#if PreferExceptions
                throw new InvalidOperationException(St.XAlreadyStarted(St.TheSrv));
#else
                return;
#endif
            }
#if TRACE
            Logging?.Invoke(this, new Events.LoggingEventArgs($"{St.XStarted(St.TheSrv)} {conns.LocalEndpoint.ToString()}"));
#endif
            _isAlive = true;
            conns.Start();
            while (_isAlive)
            {
                TcpClient c = await GetClient();
                if (!c.IsNull())
                    clwaiter.Add(AttendClient(typeof(TClient).New(c, this) as TClient));
            }
        }

        /// <summary>
        /// Detiene al servidor.
        /// </summary>
        public async Task Stop()
        {
            _isAlive = false;
            await Task.WhenAny(
                Task.WhenAll(clwaiter),
                new Task(() => Thread.Sleep(disconnectionTimeout)));
            foreach (TClient j in Clients) j.TcpClient.Close();
            conns.Stop();
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si este
        /// <see cref="Server"/> está activo (vivo).
        /// </summary>
        /// <value><c>true</c> si está vivo; sino, <c>false</c>.</value>
        public bool IsAlive
        {
            get { return _isAlive; }
            set
            {
                if (!_isAlive && value) Start();
                _isAlive = value;
            }
        }
#region Helpers

        /// <summary>
        /// Envía un mensaje a todos los clientes, excepto el especificado.
        /// </summary>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        /// <param name="client">Parámetro opcional. 
        /// Cliente que envía los datos. Si se omite, el mensaje se enviará
        /// a todos los clientes conectados a esta instancia.
        /// </param>
        public void Broadcast(byte[] data, Client client = null)
        {
            Multicast(data, (j) => client.IsNot(j));
        }

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
            foreach (Client j in Clients) if (condition(j)) j.Send(data);
        }

        /// <summary>
        /// Envía un mensaje a todos los clientes, excepto el especificado.
        /// </summary>
        /// <returns>
        /// Un <see cref="Task"/> que representa esta tarea.
        /// </returns>
        /// <param name="data">Mensaje a enviar a los clientes.</param>
        /// <param name="client">Parámetro opcional. 
        /// Cliente que envía los datos. Si se omite, el mensaje se enviará
        /// a todos los clientes conectados a esta instancia.
        /// </param>
        public async Task BroadcastAsync(byte[] data, Client client = null)
        {
            await MulticastAsync(data, (j) => client.IsNot(j));
        }

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
        public async Task MulticastAsync(byte[] data, Predicate<Client> condition)
        {
            List<Task> w = new List<Task>();
            foreach (Client j in Clients)
            {
                if (condition(j)) w.Add(j.SendAsync(data));
            }
            await Task.WhenAll(w.ToArray());
        }
#endregion
    }
}