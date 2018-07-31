/*
Commands.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene los comandos que este protocolo soporta.

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
using System.IO;
using TheXDS.MCART;
using TheXDS.MCART.Networking.Server;

namespace LightChat
{
    public partial class LightChat
    {
        #region Métodos de configuración de instancia

        /// <summary>
        ///     Describe la firma de un comando del protocolo
        ///     <see cref="LightChat" />.
        /// </summary>
        private delegate void DoCommand(BinaryReader br, Client<string> client, Server<Client<string>> server);

        [AttributeUsage(AttributeTargets.Method, Inherited = false)]
        private sealed class CommandAttribute : Attribute
        {
            public Command Value { get; }

            public CommandAttribute(Command value)
            {
                Value = value;
            }
        }

        private readonly Dictionary<Command, DoCommand> _commands = new Dictionary<Command, DoCommand>();

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:LightChat.LightChat" />.
        /// </summary>
        public LightChat()
        {
            foreach (var j in GetType().GetMethods().WithSignature<DoCommand>())
                if (j.HasAttr(out CommandAttribute c))
                    _commands.Add(c.Value, j as DoCommand);
        } 
        
        #endregion

        [Command(Command.List)]
        public void DoList(BinaryReader br, Client<string> client, Server<Client<string>> server)
        {
            using (var os = new MemoryStream())
            using (var bw = new BinaryWriter(os))
            {
                bw.Write((byte) RetVal.Ok);
                bw.Write(server.Clients.Count);
                foreach (var j in server.Clients)
                    if (!j.ClientData.IsEmpty() && j.IsNot(client))
                        bw.Write(j.ClientData);
                client.Send(os.ToArray());
            }
        }

        [Command(Command.Login)]
        public void DoLogin(BinaryReader br, Client<string> client, Server<Client<string>> server)
        {
            if (!client.ClientData.IsEmpty())
            {
                client.Send(NewErr(ErrCodes.InvalidCommand));
                return;
            }

            var usr = br.ReadString();
            if (Users.ContainsKey(usr) && Users[usr].CheckPw(br.ReadBytes(64)))
            {
                if (!Users[usr].Banned)
                {
                    client.ClientData = usr;
                    server.Broadcast(NewMsg($"{client.ClientData} ha iniciado sesión."), client);
                    client.Send(OkMsg());
                    client.Send(NewMsg("Has iniciado sesión."));
                }
                else
                {
                    client.Send(NewErr(ErrCodes.Banned));
                }
            }
            else
            {
                client.Send(NewErr(ErrCodes.InvalidLogin));
            }
        }

        [Command(Command.Logout)]
        public void DoLogout(BinaryReader br, Client<string> client, Server<Client<string>> server)
        {
            if (client.ClientData.IsEmpty())
            {
                client.Send(NewErr(ErrCodes.NoLogin));
            }
            else
            {
                server.Broadcast(NewMsg($"{client.ClientData} ha cerrado sesión."), client);
                client.Send(OkMsg());
                client.Send(NewMsg("Has cerrado sesión."));
                client.ClientData = null;
                client.Disconnect();
            }
        }

        [Command(Command.Say)]
        public void DoSay(BinaryReader br, Client<string> client, Server<Client<string>> server)
        {
            if (client.ClientData.IsEmpty())
            {
                client.Send(NewErr(ErrCodes.NoLogin));
            }
            else
            {
                var msg = br.ReadString();
                server.Broadcast(NewMsg($"{client.ClientData} dice al grupo: {msg}"), client);
                client.Send(OkMsg());
                client.Send(NewMsg($"Dijiste: {msg}"));
            }
        }

        [Command(Command.SayTo)]
        public void DoSayTo(BinaryReader br, Client<string> client, Server<Client<string>> server)
        {
            if (client.ClientData.IsEmpty())
            {
                client.Send(NewErr(ErrCodes.NoLogin));
            }
            else
            {
                var dest = br.ReadString();
                var msg = br.ReadString();
                foreach (var j in server.Clients)
                {
                    if (j.ClientData == dest)
                    {
                        j.Send(NewMsg($"{client.ClientData} te dice: {msg}"));
                        client.Send(OkMsg());
                        client.Send(NewMsg($"Dijiste a {dest}: {msg}"));
                        break;
                    }

                    client.Send(NewErr(ErrCodes.InvalidInfo));
                }
            }
        }
    }
}