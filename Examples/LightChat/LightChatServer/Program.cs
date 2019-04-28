using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheXDS.MCART.Networking.Server;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Examples.LightChat
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new Server<Client<string>>(new LightChatProtocol(), 51200);
            s.Start();
            Console.ReadKey();
            s.Stop();
        }
    }

    public class LightChatProtocol : ManagedCommandProtocol<Client<string>, Command, RetVal>
    {
        public Dictionary<string, UserDetails> Users = new Dictionary<string, UserDetails>();

        /// <summary>
        ///     Inicializa la clase <see cref="LightChatProtocol"/>.
        /// </summary>
        static LightChatProtocol()
        {
            ScanTypeOnCtor = false;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="LightChatProtocol"/>.
        /// </summary>
        public LightChatProtocol()
        {
            WireUp(Command.Login, OnLogin);
            WireUp(Command.Logout, OnLogout);

            Users.Add("root", new UserDetails { Banned = false, DisplayName = "Administrador", Password = "root" });
            Users.Add("ban", new UserDetails { Banned = true, DisplayName = "Usuario baneado", Password = "ban" });
        }

        private async void OnLogout(Request request)
        {
            if (request.Client.ClientData.IsEmpty())
            {
                request.Respond(RetVal.NoLogin);
                return;
            }
            var u = request.Client.ClientData;
            request.Client.ClientData = null;
            await Task.WhenAll(
                request.BroadcastAsync(RetVal.Msg, $"{Users[u].DisplayName} ha cerrado sesión."),
                request.RespondAsync(RetVal.Msg, "Has cerrado sesión.")
            );
            request.Client.Bye();
        }

        private async void OnLogin(Request request)
        {
            if (!request.Client.ClientData.IsEmpty())
            {
                request.Failure();
                return;
            }
            var u = request.Reader.ReadString();
            if (!Users.TryGetValue(u, out var d)) request.Respond(RetVal.InvalidLogin);
            else if (request.Reader.ReadString() != d.Password) request.Respond(RetVal.InvalidLogin);
            else if (d.Banned) request.Respond(RetVal.Banned);
            else
            {
                request.Client.ClientData = u;
                await Task.WhenAll(
                    request.BroadcastAsync(RetVal.Msg, $"{d.DisplayName} ha iniciado sesión."),
                    request.RespondAsync(RetVal.Ok)
                );
            }
        }

        public override bool ClientWelcome(Client<string> client)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(MakeResponse(RetVal.Msg));
                bw.Write("Bienvenido al servidor de pruebas de LightChat. Por favor inicie sesión para continuar.");
                bw.Flush();

                client.Send(ms.ToArray());
            }
            return true;
        }
    }

    public struct UserDetails
    {
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public bool Banned { get; set; }
    }
}