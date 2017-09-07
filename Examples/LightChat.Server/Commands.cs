//
//  Commands.cs
//
//  Author:
//       César Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2017 César Morgan
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.IO;
using MCART;
using MCART.Networking.Server;

namespace LightChat
{
    public partial class LightChat
    {
        /// <summary>
        /// Describe la firma de un comando del protocolo
        /// <see cref="LightChat"/>.
        /// </summary>
        delegate void DoCommand(BinaryReader br, Client<string> client, Server<Client<string>> server);
        void DoLogin(BinaryReader br, Client<string> client, Server<Client<string>> server)
        {
            if (!client.userObj.IsEmpty()) client.Send(NewErr(ErrCodes.InvalidCommand));
            else
            {
                string usr = br.ReadString();
                if (Users.ContainsKey(usr) && Users[usr].CheckPw(br.ReadBytes(64)))
                {
                    if (!Users[usr].Banned)
                    {
                        client.userObj = usr;
                        server.Broadcast(NewMsg($"{client.userObj} ha iniciado sesión."), client);
                        client.Send(OkMsg());
                        client.Send(NewMsg("Has iniciado sesión."));
                    }
                    else client.Send(NewErr(ErrCodes.Banned));
                }
                else client.Send(NewErr(ErrCodes.InvalidLogin));
            }
        }
        void DoLogout(BinaryReader br, Client<string> client, Server<Client<string>> server)
        {
            if (client.userObj.IsEmpty()) client.Send(NewErr(ErrCodes.NoLogin));
            else
            {
                server.Broadcast(NewMsg($"{client.userObj} ha cerrado sesión."), client);
                client.Send(OkMsg());
                client.Send(NewMsg("Has cerrado sesión."));
                client.userObj = null;
                client.Disconnect();
            }
        }
        void DoList(BinaryReader br, Client<string> client, Server<Client<string>> server)
        {
            using (var os = new MemoryStream())
            {
                using (var bw = new BinaryWriter(os))
                {
                    bw.Write((byte)RetVal.Ok);
                    bw.Write(server.Clients.Count);
                    foreach (var j in server.Clients)
                        if (!j.userObj.IsEmpty() && j.IsNot(client))
                            bw.Write(j.userObj);
                    client.Send(os.ToArray());
                }
            }
        }
        void DoSay(BinaryReader br, Client<string> client, Server<Client<string>> server)
        {
            if (client.userObj.IsEmpty()) client.Send(NewErr(ErrCodes.NoLogin));
            else
            {
                string msg = br.ReadString();
                server.Broadcast(NewMsg($"{client.userObj} dice al grupo: {msg}"), client);
                client.Send(OkMsg());
                client.Send(NewMsg($"Dijiste: {msg}"));
            }
        }
        void DoSayTo(BinaryReader br, Client<string> client, Server<Client<string>> server)
        {
            if (client.userObj.IsEmpty()) client.Send(NewErr(ErrCodes.NoLogin));
            else
            {
                string dest = br.ReadString();
                string msg = br.ReadString();
                foreach (var j in server.Clients)
                {
                    if (j.userObj == dest)
                    {
                        j.Send(NewMsg($"{client.userObj} te dice: {msg}"));
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