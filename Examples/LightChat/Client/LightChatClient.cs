﻿/*
LightChatClient.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using TheXDS.MCART.Annotations;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Networking.Client;

namespace TheXDS.LightChat
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class LightChatClient : SelfWiredCommandClient<Command, RetVal>
    {
        private readonly ITerminal _term;

        public LightChatClient(ITerminal terminal)
        {
            _term = terminal;
        }

        [Response(RetVal.Err)]
        [Response(RetVal.InvalidCommand)]
        [Response(RetVal.Unknown)]
        [Response(RetVal.InvalidInfo)]
        public static void DoErr(object instance, BinaryReader br)
        {
            Write(instance ?? throw new TamperException(), "El servidor ha encontrado un error.");
        }

        [Response(RetVal.InvalidLogin)]
        public static void ShowLoginErr(object instance, BinaryReader br)
        {
            var i = instance as LightChatClient ?? throw new TamperException();
            Write(i, "Inicio de sesión inválido.");
            i.CloseConnection();
        }

        [Response(RetVal.Msg)]
        public static void DoMsg(object instance, BinaryReader br)
        {
            Write(instance ?? throw new TamperException(), br.ReadString());
        }

        [Response(RetVal.NoLogin)]
        public static void DoNoLogin(object instance, BinaryReader br)
        {
            Write(instance ?? throw new TamperException(), "No has iniciado sesión.");
        }

        [Response(RetVal.Ok)]
        public static void DoOk(object instance, BinaryReader br)
        {
            if (br.PeekChar() < 0) return;
            var c = br.ReadInt32();
            Write(instance ?? throw new TamperException(), $"Hay otros {c} usuarios conectados:");
            for (var j = 0; j < c; j++) Write(instance, br.ReadString());
        }

        private static void Write([NotNull] object instance, string text)
        {
            (instance as LightChatClient)?._term.WriteLine(text);
        }

        public void Login(string user, IEnumerable<byte> password)
        {
            if (!IsAlive) return;

            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(user);
                bw.Write(password.ToArray());
                TalkToServer(MakeCommand(Command.Login, ms));
            }
        }

        public void Logout()
        {
            if (!IsAlive) return;
            TalkToServer(Command.Logout);
        }

        public void Say(string text)
        {
            if (!IsAlive) return;
            TalkToServer(MakeCommand(Command.Say,text));
        }

        public void SayTo(string dest, string text)
        {
            if (!IsAlive) return;

            TalkToServer(MakeCommand(Command.SayTo, new[]{dest, text}));
        }
    }
}