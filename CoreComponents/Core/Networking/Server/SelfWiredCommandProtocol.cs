/*
Protocol.cs

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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;

namespace TheXDS.MCART.Networking.Server
{
    /// <summary>
    ///     Clase base para crear protocolos simples con bytes de comandos 
    /// </summary>
    /// <typeparam name="TClient"></typeparam>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class SelfWiredCommandProtocol<TClient, TCommand, TResponse> : Protocol<TClient> where TClient: Client where TCommand : struct, Enum where TResponse: struct, Enum
    {
        /// <summary>
        ///     Describe la firma de un comando del protocolo.
        /// </summary>
        private delegate void DoCommand(BinaryReader br, TClient client, Server<TClient> server);
        private readonly Dictionary<TCommand, DoCommand> _commands = new Dictionary<TCommand, DoCommand>();

        private readonly TResponse? _errResponse;
        private readonly TResponse? _unkResponse;
        private readonly TResponse? _notMappedResponse;

        private static TCommand ReadCommand(BinaryReader br)
        {
            switch (Marshal.SizeOf<TCommand>())
            {
                case 1:
                    return (TCommand)Enum.ToObject(typeof(TCommand), br.ReadByte());
                case 2:
                    return (TCommand)Enum.ToObject(typeof(TCommand), br.ReadInt16());
                case 4:
                    return (TCommand)Enum.ToObject(typeof(TCommand), br.ReadInt32());
                case 8:
                    return (TCommand)Enum.ToObject(typeof(TCommand), br.ReadInt64());
                default:
                    throw new PlatformNotSupportedException();
            }
        }

        private static byte[] MakeCommand(TResponse command)
        {
            /* -= QUIRK =-
             * Recurrir a boxing no es ideal, pero es la única alternativa
             * disponible mientras C# no brinde un mejor soporte a los
             * argumentos de tipo genérico con restricción de Enum.
             */
            switch (Marshal.SizeOf<TResponse>())
            {
                case 1:
                    return BitConverter.GetBytes((byte)(object)command);
                case 2:
                    return BitConverter.GetBytes((short)(object)command);
                case 4:
                    return BitConverter.GetBytes((int)(object)command);
                case 8:
                    return BitConverter.GetBytes((long)(object)command);
                default:
                    throw new PlatformNotSupportedException();
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Protocolo de atención al cliente
        /// </summary>
        /// <param name="client">Cliente que será atendido.</param>
        /// <param name="server">Servidor que atiende al cliente.</param>
        /// <param name="data">Datos recibidos desde el cliente.</param>
        public override void ClientAttendant(TClient client, Server<TClient> server, byte[] data)
        {
            using (var br = new BinaryReader(new MemoryStream(data)))
            {
                var c = ReadCommand(br);

                if (!Enum.IsDefined(typeof(TCommand), br))
                    client.Send(MakeCommand(_unkResponse ?? _errResponse ?? throw new InvalidOperationException()));

                if (_commands.ContainsKey(c))
                {
                    try { _commands[c](br, client, server); }
                    catch { client.Send(MakeCommand(_errResponse ?? throw new InvalidOperationException())); }
                }
                else
                {
                    client.Send(MakeCommand(_notMappedResponse ?? _errResponse ?? throw new InvalidOperationException()));
                }
            }
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SelfWiredCommandProtocol{TClient,TCommand,TResponse}"/>
        /// </summary>
        protected SelfWiredCommandProtocol()
        {
            var vals = Enum.GetValues(typeof(TResponse)).OfType<TResponse?>().ToArray();

            _errResponse = vals.FirstOrDefault(p => p.HasAttr<ErrorResponseAttribute>());
            _unkResponse = vals.FirstOrDefault(p => p.HasAttr<UnknownResponseAttribute>());
            _notMappedResponse = vals.FirstOrDefault(p => p.HasAttr<NotMappedResponseAttribute>());

            var tCmdAttr = Objects.GetTypes<IValueAttribute<TCommand>>(true).FirstOrDefault() ??
                           throw new MissingTypeException(typeof(IValueAttribute<TCommand>));
            foreach (var j in GetType().GetMethods().WithSignature<DoCommand>())
            {
                var attr=j.Method.GetCustomAttributes(tCmdAttr,false).OfType<IValueAttribute<TCommand>>().FirstOrDefault();
                if (attr is null) continue;
                _commands.Add(attr.Value, j as DoCommand);
            }
        }
    }
}