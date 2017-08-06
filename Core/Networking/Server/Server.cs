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

//Preferir excepciones en lugar de continuar con código alternativo
//#define PreferExceptions

#endregion

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MCART.Networking.Server
{
    /// <summary>
    /// Controla conexiones entrantes y ejecuta protocolos sobre los clientes que se conecten al servidor.
    /// </summary>
    public class Server<TClient> where TClient : Client
    {
        /// <summary>
        /// Tiempo de espera antes de realizar una desconexión forzada al
        /// cerrar el servidor.
        /// </summary>
        const int disconnectionTimeout = 15000;

        /// <summary>
        /// Puerto predeterminado para las conexiones entrantes.
        /// </summary>
        public const int defaultPort = 51220;

        /// <summary>
        /// Lista de objetos <see cref="Client"/>conectados a este servidor.
        /// </summary>
        internal List<TClient> clients = new List<TClient>();

        /// <summary>
        /// Instancia de protocolos a utilizar para dar servicio a los
        /// clientes que se conecten a este servidor.
        /// </summary>
        internal Protocol<TClient> prot;

        /// <summary>
        /// Escucha de conexiones entrantes.
        /// </summary>
        TcpListener conns;

        /// <summary>
        /// campo que determina si el servidor está escuchando conexiones y
        /// sirviendo a clientes (vivo)
        /// </summary>
        bool _isAlive;

        /// <summary>
        /// Encapsula <see cref="TcpListener.AcceptTcpClientAsync"/> para permitir cancelar la tarea cuando el servidor 
		/// se detenga.
        /// </summary>
        /// <returns>El <see cref="TcpClient"/> conectado. Si el servidor se detiene, se devuelve <c>null</c>.</returns>
        async Task<TcpClient> GetClient()
        {
            //Necesario para poder detener el lambda
            //de espera sin matar al servidor.
            bool bewaiting = true;
            try
            {
                Task<TcpClient> t = conns.AcceptTcpClientAsync();
                if (t == await Task.WhenAny(t, Task.Run(() => { while (_isAlive && bewaiting) { } })))
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
            clients.Add(client);
            if (prot.ClientWelcome(client, this))
            {
                /*
				 * ClientBye será llamado por una función
				 * dentro de la clase Client, que será
				 * accesible por el código del usuario o la
				 * implementación del protocolo. La función
				 * ClientDisconnect será llamada cuando se
				 * detecte que la conexión ha finalizado sin
				 * llamar a la función de desconexión.
				 */
                while (client.IsAlive)
                    prot.ClientAttendant(client, this, await client.RecieveAsync());
            }
            else
            {
                if ((bool)client?.IsAlive) client.TcpClient.Close();
            }
            if ((bool)clients?.Contains(client)) clients.Remove(client);
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Server"/>.
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
            if (ReferenceEquals(protocol, null)) throw new ArgumentNullException(nameof(protocol));
            prot = protocol;
            conns = new TcpListener(ep ?? new IPEndPoint(IPAddress.Any, defaultPort));
            Serve().Start();
        }

        /// <summary>
        /// Crea un hilo de ejecución que da servicio a los clientes
        /// </summary>
        public async Task Serve()
        {
            if (_isAlive)
#if PreferExceptions
                throw new InvalidOperationException(St.SrvAlreadyStarted(St.TheSrv));
#else
                return;
#endif
            _isAlive = true;
            List<Task> clwaiter = new List<Task>();
            conns.Start();
            while (_isAlive)
            {
                TcpClient c = await GetClient();
                if (c != null)
                {
                    Task t = AttendClient(Objects.New<TClient>(new object[] { c, this }));
                    clwaiter.Add(t);
                    t.Start();
                }
            }
            Task.WaitAll(clwaiter.ToArray(), disconnectionTimeout);
            foreach (TClient j in clients) j.TcpClient.Close();
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
                if (!_isAlive && value) Serve().Start();
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
            Multicast(data, (j) => !ReferenceEquals(client, j));
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
            foreach (Client j in clients) if (condition(j)) j.Send(data);
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
            await MulticastAsync(data, (j) => !ReferenceEquals(client, j));
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
            foreach (Client j in clients)
            {
                if (condition(j))
                {
                    Task k = j.SendAsync(data);
                    w.Add(k);
                    k.Start();
                }
            }
            await Task.WhenAll(w.ToArray());
        }
        #endregion
    }
}