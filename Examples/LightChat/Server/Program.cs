using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TheXDS.MCART;
using TheXDS.MCART.Networking.Server;

namespace TheXDS.LightChat
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var p = new LightChatProtocol();
            p.Users.Add("user1", new UserRegistry());
            p.Users.Add("user2", new UserRegistry());
            p.Users.Add("user3", new UserRegistry());
            p.Users.Add("user4", new UserRegistry());
            p.Users.Add("user5", new UserRegistry());

            var s =new Server<Client<string>>(p);
            s.ClientConnected += S_ClientConnected;
            s.ClientRejected += S_ClientRejected;
            s.ClientAccepted += S_ClientAccepted;
            s.ClientFarewell += S_ClientFarewell;
            s.ClientLost += S_ClientLost;

            s.Start();
            Console.ReadKey();
            s.Stop();
        }

        private static void S_ClientLost(object sender, MCART.Events.ValueEventArgs<Client> e)
        {
            Console.WriteLine($"Se ha perdido la conexión con el cliente {e.Value.EndPoint.Address}");
        }

        private static void S_ClientFarewell(object sender, MCART.Events.ValueEventArgs<Client> e)
        {
            Console.WriteLine($"El cliente {e.Value.EndPoint.Address} ha finalizado su sesión.");
        }

        private static void S_ClientAccepted(object sender, MCART.Events.ValueEventArgs<Client> e)
        {
            Console.WriteLine($"Se ha aceptado al cliente {e.Value.EndPoint.Address}");
        }

        private static void S_ClientRejected(object sender, MCART.Events.ValueEventArgs<Client> e)
        {
            Console.WriteLine($"Se ha rechazado al cliente {e.Value.EndPoint.Address}");
        }

        private static void S_ClientConnected(object sender, MCART.Events.ValueEventArgs<Client> e)
        {
            Console.WriteLine($"Conexión entrante desde {e.Value.EndPoint.Address}");
        }
    }
    
    /// <summary>
    /// Describe a un usuario registrado del protocolo 
    /// <see cref="LightChat"/>.
    /// </summary>
    internal class UserRegistry
    {
        byte[] _pwd;
        /// <summary>
        /// Establece el hash de contraseña del usuario.
        /// </summary>
        public byte[] Password { set => _pwd = value; }
        /// <summary>
        /// Indica si este usuario ha sido baneado.
        /// </summary>
        public bool Banned { get; set; }
        /// <summary>
        /// Comprueba la contraseña.
        /// </summary>
        /// <param name="pw">contraseña a comprobar.</param>
        /// <returns>
        /// <see langword="true"/> si la contraseña coincide, <see langword="false"/> en caso
        /// contrario.
        /// </returns>
        public bool CheckPw(byte[] pw) => _pwd == pw;
    }
    
    internal class LightChatProtocol : SelfWiredCommandProtocol<Client<string>, Command, RetVal>
    {
        /// <summary>
        /// Lista de usuarios registrados para este protocolo.
        /// </summary>
        public readonly Dictionary<string, UserRegistry> Users = new Dictionary<string, UserRegistry>();

        private static byte[] NewMsg(string msg)
        {
            using (var os = new MemoryStream())
            using (var bw = new BinaryWriter(os))
            {
                bw.Write(msg);
                return MakeResponse(RetVal.Msg).Concat(os.ToArray()).ToArray();
            }
        }
        
        [Command(Command.List)]
        public static void DoList(object instance, BinaryReader br, Client<string> client, Server<Client<string>> server)
        {
            using (var os = new MemoryStream())
            using (var bw = new BinaryWriter(os))
            {
                bw.Write((byte) RetVal.Ok);
                bw.Write(server.Clients.Count()-1);
                foreach (var j in server.Clients)
                    if (!j.ClientData.IsEmpty() && j.IsNot(client))
                        bw.Write(j.ClientData);
                client.Send(os.ToArray());
            }
        }
        
        [Command(Command.Login)]
        public static void DoLogin(object instance, BinaryReader br, Client<string> client, Server<Client<string>> server)
        {
            var p = (LightChatProtocol)instance;
            if (!client.ClientData.IsEmpty())
            {
                client.Send(MakeResponse(RetVal.InvalidCommand));
                return;
            }
            var usr = br.ReadString();
            if (p.Users.ContainsKey(usr) /*&& p.Users[usr].CheckPw(br.ReadBytes(64))*/)
            {
                if (!p.Users[usr].Banned)
                {
                    client.ClientData = usr;
                    server.Broadcast(NewMsg($"{client.ClientData} ha iniciado sesión."), client);
                    client.Send(NewMsg("Has iniciado sesión."));
                }
                else
                {
                    client.Send(MakeResponse(RetVal.Banned));
                }
            }
            else
            {
                client.Send(MakeResponse(RetVal.InvalidLogin));
            }
        }

        [Command(Command.Logout)]
        public static void DoLogout(object instance, BinaryReader br, Client<string> client, Server<Client<string>> server)
        {
            if (client.ClientData.IsEmpty())
            {
                client.Send(MakeResponse(RetVal.NoLogin));
                return;
            }

            server.Broadcast(NewMsg($"{client.ClientData} ha cerrado sesión."), client);
            client.Send(NewMsg("Has cerrado sesión."));
            client.ClientData = null;
            client.Bye();
        }

        [Command(Command.Say)]
        public static void DoSay(object instance, BinaryReader br, Client<string> client, Server<Client<string>> server)
        {
            if (client.ClientData.IsEmpty())
            {
                client.Send(MakeResponse(RetVal.NoLogin));
                return;
            }

            var msg = br.ReadString();
            server.Broadcast(NewMsg($"{client.ClientData} dice al grupo: {msg}"), client);
            client.Send(NewMsg($"Dijiste: {msg}"));
        }

        [Command(Command.SayTo)]
        public static void DoSayTo(object instance, BinaryReader br, Client<string> client, Server<Client<string>> server)
        {
            if (client.ClientData.IsEmpty())
            {
                client.Send(MakeResponse(RetVal.NoLogin));
                return;
            }

            var dest = br.ReadString();
            var msg = br.ReadString();
            foreach (var j in server.Clients)
            {
                if (j.ClientData == dest)
                {
                    j.Send(NewMsg($"{client.ClientData} te dice: {msg}"));
                    client.Send(NewMsg($"Dijiste a {dest}: {msg}"));
                    break;
                }
                client.Send(MakeResponse(RetVal.InvalidInfo));
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Protocolo de desconexión inesperada del cliente.
        /// </summary>
        /// <param name="client">Cliente que se ha desconectado.</param>
        public override void ClientDisconnect(Client<string> client)
        {
            Server.Broadcast(NewMsg($"{client.ClientData} se ha desconectado inesperadamente."),client);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Protocolo de desconexión del cliente.
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        public override void ClientBye(Client<string> client)
        {
            Server.Broadcast(NewMsg($"{client.ClientData} ha cerrado sesión."),client);
        }
    }
}