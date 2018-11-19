/*
LightChatClient.cs

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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using TheXDS.MCART.Networking.Client;

namespace TheXDS.LightChat
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class LightChatClient : SelfWiredCommandClient<Command, RetVal>
    {
        private readonly MainWindow _wnd;

        public LightChatClient(MainWindow wnd)
        {
            _wnd = wnd;
        }

        [Response(RetVal.Err)]
        [Response(RetVal.InvalidCommand)]
        [Response(RetVal.InvalidInfo)]
        [Response(RetVal.InvalidLogin)]
        [Response(RetVal.Unknown)]
        public static void DoErr(object instance, BinaryReader br)
        {
            Write(instance as LightChatClient, "El servidor ha encontrado un error.");
        }

        [Response(RetVal.Msg)]
        public static void DoMsg(object instance, BinaryReader br)
        {
            Write(instance as LightChatClient, br.ReadString());
        }

        [Response(RetVal.NoLogin)]
        public static void DoNoLogin(object instance, BinaryReader br)
        {
            Write(instance as LightChatClient, "No has iniciado sesión.");
        }

        [Response(RetVal.Ok)]
        public static void DoOk(object instance, BinaryReader br)
        {
            if (br.PeekChar() < 0) return;
            var c = br.ReadInt32();
            Write(instance as LightChatClient, $"Hay otros {c} usuarios conectados:");
            for (var j = 0; j < c; j++) Write(instance as LightChatClient, br.ReadString());
        }

        private static void Write(LightChatClient instance, string text)
        {
            instance._wnd.Dispatcher.Invoke(() => { instance._wnd.TxtChat.Text += $"{text}\n"; });
        }

        public void Login(string user, IEnumerable<byte> password)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(user);
                bw.Write(password.ToArray());
                TalkToServer(Command.Login, ms);
            }
        }

        public void Logout()
        {
            TalkToServer(Command.Logout);
        }

        public void Say(string text)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(text);
                TalkToServer(Command.Say, ms);
            }
        }

        public void SayTo(string dest, string text)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(dest);
                bw.Write(text);
                TalkToServer(Command.SayTo, ms.ToArray());
            }
        }
    }
}