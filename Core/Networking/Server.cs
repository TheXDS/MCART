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

//Incluir implementaciones predeterminadas de interfaces / clases abstractas
#define IncludeDefaultImplementations

#endregion

// Espacios de nombres importados
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MCART.Networking.Server
{
    /// <summary>
    /// Esta clase abstracta determina una serie de funciones a heredar por
    /// una clase que provea de protocolos a un servidor.
    /// </summary>
    public abstract class Protocol<TClient> where TClient : Client
    {
        /// <summary>
        /// Atiende al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public abstract void ClientAttendant(TClient client, Server<TClient> server, byte[] data);
        /// <summary>
        /// Protocolo de bienvenida del cliente.
        /// </summary>
        /// <returns><c>true</c> si el cliente fue aceptado por el protocolo; de lo contrario, <c>false</c>.</returns>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        public virtual bool ClientWelcome(TClient client, Server<TClient> server) { return true; }
        /// <summary>
        /// Protocolo de desconexión del cliente.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        public virtual void ClientBye(TClient client, Server<TClient> server) { }
        /// <summary>
        /// Protocolo de desconexión inesperada del cliente.
        /// </summary>
        /// <param name="client">Cliente que se ha desconectado.</param>
        /// <param name="server">Servidor que atiendía al cliente.</param>
        public virtual void ClientDisconnect(TClient client, Server<TClient> server) { }
    }

    /// <summary>
    /// Controla conexiones entrantes y ejecuta protocolos sobre los clientes que se conecten al servidor.
    /// </summary>
    public class Server<TClient> where TClient : Client
    {

        /// <summary>
        /// Puerto predeterminado para las conexiones entrantes.
        /// </summary>
        public const int defaultPort = 51220;

        /// <summary>
        /// Tiempo de espera antes de realizar una desconexión forzada al
        /// cerrar el servidor.
        /// </summary>
        const int disconnectionTimeout = 15000;

        /// <summary>
        /// Lista de objetos <see cref="Client"/>conectados a este servidor.
        /// </summary>
        internal List<TClient> clients = new List<TClient>();

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
        /// Instancia de protocolos a utilizar para dar servicio a los
        /// clientes que se conecten a este servidor.
        /// </summary>
        internal Protocol<TClient> prot;

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
        /// Crea un hilo de ejecución que da servicio a los clientes
        /// </summary>
        public async Task Serve()
        {
            if (_isAlive)
#if PreferExceptions
                    throw new InvalidOperationException("El servidor ya se ha iniciado");
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
        /// Attends the client.
        /// </summary>
        /// <returns>The client.</returns>
        /// <param name="client">Client.</param>
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

    /// <summary>
    /// Representa un cliente conectado al servidor.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Búfer predeterminado para recepción.
        /// </summary>
        public const ushort bufferSize = 1024;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Client"/>.
        /// </summary>
        /// <param name="tcpClient">
        /// Conexión con el cliente.
        /// </param>
        /// <param name="server">
        /// Instancia de servidor que dará servicio a este objeto.
        /// </param>
        internal Client(TcpClient tcpClient, Server<Client> server)
        {
            TcpClient = tcpClient;
            Server = server;
        }

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
        /// Gets a value indicating whether this <see cref="T:LoginLibrary.Server.Client"/> is alive.
        /// </summary>
        /// <value><c>true</c> if is alive; otherwise, <c>false</c>.</value>
        public bool IsAlive => (bool)TcpClient?.Connected;

        /// <summary>
        /// Obtiene un valor que indica si hay datos disponibles para leer.
        /// </summary>
        /// <value><c>true</c> si hay datos disponibles; sino, <c>false</c>.</value>
        public bool DataAvailable => (bool)TcpClient?.GetStream()?.DataAvailable;

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
        public async Task SendAsync(byte[] data)
        {
            await TcpClient?.GetStream().WriteAsync(data, 0, data.Length);
        }

        /// <summary>
        /// Envía un mensaje al cliente de forma asíncrona.
        /// </summary>
        /// <param name="data">Mensaje a enviar.</param>
        /// <param name="cancellationToken">Token de cancelación de tarea.</param>
        public async Task SendAsync(byte[] data, CancellationToken cancellationToken)
        {
            await TcpClient?.GetStream().WriteAsync(data, 0, data.Length, cancellationToken);
        }

        /// <summary>
        /// Devuelve los datos que el cliente envía.
        /// </summary>
        public byte[] Recieve()
        {
            List<byte> outp = new List<byte>();
            do
            {
                byte[] buff = new byte[bufferSize];
                TcpClient?.GetStream().Read(buff, 0, bufferSize);
                outp.AddRange(buff);
            } while ((bool)TcpClient?.GetStream()?.DataAvailable);
            return outp.ToArray();
        }

        /// <summary>
        /// Devuelve los datos recibidos una vez que el cliente los envía.
        /// </summary>
        public async Task<byte[]> RecieveAsync()
        {
            List<byte> outp = new List<byte>();
            do
            {
                byte[] buff = new byte[bufferSize];
                await TcpClient?.GetStream().ReadAsync(buff, 0, bufferSize);
                outp.AddRange(buff);
            } while ((bool)TcpClient?.GetStream()?.DataAvailable);
            return outp.ToArray();
        }

        /// <summary>
        /// Devuelve los datos recibidos una vez que el cliente los envía.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación de tarea.</param>
        public async Task<byte[]> RecieveAsync(CancellationToken cancellationToken)
        {
            List<byte> outp = new List<byte>();
            do
            {
                byte[] buff = new byte[bufferSize];
                await TcpClient?.GetStream().ReadAsync(buff, 0, bufferSize, cancellationToken);
                outp.AddRange(buff);
            } while ((bool)TcpClient?.GetStream()?.DataAvailable);
            return outp.ToArray();
        }

        /// <summary>
        /// Disconnect this instance.
        /// </summary>
        public void Disconnect()
        {
            Server.prot.ClientBye(this, Server);
            TcpClient.Close();
        }
    }

    /// <summary>
    /// Representa un cliente conectado al servidor.
    /// </summary>
    public class Client<T> : Client
    {
        /// <summary>
        /// Contiene un objeto de estado personalizado asociado a esta instancia, para utilizarse como el usuario lo desee.
        /// </summary>
        public T userObj;
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MCART.Networking.Server.Client`1"/> class.
        /// </summary>
        /// <param name="tcpClient">Tcp client.</param>
        /// <param name="server">Server.</param>
        public Client(TcpClient tcpClient, Server<Client> server) : base(tcpClient, server) { }
    }

#if IncludeDefaultImplementations
    namespace Protocols
    {
        /// <summary>
        /// Protocolo simple de eco.
        /// </summary>
        /// <remarks>Este protocolo utiliza TCP/IP, no IGMP.</remarks>
        public class EchoProtocol : Protocol<Client>
        {
            /// <summary>
            /// Protocolo de atención normal.
            /// </summary>
            public override void ClientAttendant(Client client, Server<Client> server, byte[] data)
            {
                client.Send(data);
            }
        }
        /// <summary>
        /// Protocolo simple de chat.
        /// </summary>
        public class LightChat : Protocol<Client<string>>
        {
            /// <summary>
            /// Comandos para el protocolo <see cref="LightChat"/>.
            /// </summary>
            public enum Command : byte
            {
                /// <summary>
                /// No realizar ninguna acción.
                /// </summary>
                Nothing,
                /// <summary>
                /// Iniciar sesión.
                /// </summary>
                Login,
                /// <summary>
                /// Cerrar sesión.
                /// </summary>
                Logout,
                /// <summary>
                /// Devolver una lista de los usuarios conectados.
                /// </summary>
                List,
                /// <summary>
                /// Enviar un mensaje.
                /// </summary>
                Say,
                /// <summary>
                /// Enviar un mensaje a un usuario.
                /// </summary>
                SayTo

            }
            /// <summary>
            /// Valores de retorno del servidor.
            /// </summary>
            public enum RetVals : byte
            {
                /// <summary>
                /// Operación finalizada correctamente.
                /// </summary>
                OK,
                /// <summary>
                /// Mensaje.
                /// </summary>
                Msg,
                /// <summary>
                /// Error en la operación.
                /// </summary>
                Err
            }

            /// <summary>
            /// Lista de usuarios registrados para este protocolo
            /// </summary>
            public Dictionary<string, byte[]> Users = new Dictionary<string, byte[]>();

            /// <summary>
            /// Atiende al cliente
            /// </summary>
            /// <param name="client">Cliente que será atendido.</param>
            /// <param name="server">Servidor que atiende al cliente.</param>
            /// <param name="data">Datos recibidos desde el cliente.</param>
            public override void ClientAttendant(Client<string> client, Server<Client<string>> server, byte[] data)
            {
                using (var ms = new System.IO.MemoryStream(data))
                {
                    switch ((Command)ms.ReadByte())
                    {
                        case Command.Login:
                            /*
                             * Descripción de estructura de comando:
                             * Offset | Tamaño | Descripción
                             * -------+--------+------------
                             * 0x0001 | 1 byte | Longitud de campo de nombre de usuario.
                             * 0x0002 | 0x0001 | Bytes Unicode que representan  el nombre de usuario.
                             * 0x00nn | ->EOS  | Hash de contraseña.
                            */
                            byte usrlngth = (byte)ms.ReadByte();
                            byte[] dt = new byte[usrlngth];
                            ms.Read(dt, 1, usrlngth);
                            string usr = System.Text.Encoding.Unicode.GetString(dt);

                            if (Users.ContainsKey(usr))
                            {

                            }
                            else client.Send(new byte[] { (byte)RetVals.Err });


                            break;
                        default:
                            // Comando desconocido. Devolver error.
                            client.Send(new byte[] { (byte)RetVals.Err });
                            break;
                    }
                }
            }
            /// <summary>
            /// Realiza funciones de bienvenida al cliente que se acaba de
            /// conectar a este servidor.
            /// </summary>
            /// <param name="client">Cliente que acaba de conectarse.</param>
            /// <param name="server">
            /// Instancia del servidor al cual el cliente se ha conectado.
            /// </param>
            /// <returns>
            /// <c>true</c> para indicar que el cliente ha sido aceptado por el
            /// protocolo, <c>false</c> para indicar lo contrario.
            /// </returns>
            public override bool ClientWelcome(Client<string> client, Server<Client<string>> server)
            {

                return true;
            }
        }
    }
#endif
}