﻿#pragma warning disable CS1591

using System;
using System.IO;
using TheXDS.MCART.Networking.Legacy.Client;

namespace TheXDS.MCART.Examples.LightChat
{
    internal class Program
    {
        private static void Main()
        {
            var c = new LightChatClient();
            c.Connect("localhost", 51200);
            while (!c.Login()) Console.WriteLine("Invalid login.");
            Console.ReadKey();
            c.Logout();
            Console.ReadKey();
        }
    }

    public class LightChatClient : ManagedCommandClient<Command,RetVal>
    {
        private bool _logged;

        /// <summary>
        /// Inicializa la clase <see cref="LightChatClient"/>.
        /// </summary>
        static LightChatClient()
        {
            ScanTypeOnCtor = false;
        }

        public LightChatClient()
        {
            WireUp(RetVal.Msg, DisplayMessage);
        }

        private void DisplayMessage(RetVal response, BinaryReader br)
        {
            if (_logged) Console.WriteLine(br.ReadString());
        }

        public bool Login()
        {
            Console.Write("Login: ");
            var user = Console.ReadLine()!;

            Console.Write("Password: ");
            var pw = Console.ReadLine()!;

            if (Send(Command.Login, new[] { user, pw }, GetResponse) == RetVal.Ok)
            {
                Console.WriteLine("Has iniciado sesión");
                _logged = true;
                return true;
            };
            return false;
        }

        public void Logout()
        {
            Send(Command.Logout);
        }

        private RetVal GetResponse(RetVal response, BinaryReader br)
        {
            return response;
        }
    }
}
