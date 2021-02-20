/*
ManagedCommandProtocol.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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
using System.Reflection;
using System.Threading.Tasks;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Events;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Networking.Legacy.Server
{
    /// <summary>
    /// Describe un protocolo de comandos administrado por MCART.
    /// </summary>
    /// <typeparam name="TClient">
    /// Tipo de clientes a atender por el servidor.
    /// </typeparam>
    /// <typeparam name="TCommand">
    /// <see cref="Enum"/> con los comandos aceptados.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// <see cref="Enum"/> con las respuestas generadas.
    /// </typeparam>
    public abstract class ManagedCommandProtocol<TClient, TCommand, TResult> : ServerProtocol<TClient> where TClient : Client where TCommand : struct, Enum where TResult : struct, Enum
    {
        /// <summary>
        /// Describe la solicitud realizada por un cliente que está siendo
        /// atendida por este protocolo.
        /// </summary>
        public class Request
        {
            private byte[] MkResp(TResult resp, bool withGuid = true)
            {
                var l = new List<byte>();

                if (Parent.AppendGuid)
                {
                    if (withGuid)
                    {
                        l.AddRange(BitConverter.GetBytes(true));
                        l.AddRange(CommandGuid.ToByteArray());
                    }
                    else
                    {
                        l.AddRange(BitConverter.GetBytes(false));
                    }
                }
                return l.Concat(_toResponse(resp)).ToArray();
            }

            internal Request(Guid comnandGuid, BinaryReader reader, TClient client, Server<TClient> server, TCommand command, ManagedCommandProtocol<TClient, TCommand, TResult> parent)
            {
                CommandGuid = comnandGuid;
                Reader = reader ?? throw new ArgumentNullException(nameof(reader));
                Client = client ?? throw new ArgumentNullException(nameof(client));
                Server = server ?? throw new ArgumentNullException(nameof(server));
                Command = command;
                Parent = parent;
            }

            /// <summary>
            /// Obtiene una referencia al servidor que atiende a este
            /// protocolo.
            /// </summary>
            public Server<TClient> Server { get; }

            /// <summary>
            /// Obtiene una referencia al cliente que ha realizado la
            /// solicitud.
            /// </summary>
            public TClient Client { get; }

            /// <summary>
            /// Obtiene el identificador único del comando enviado por el
            /// cliente.
            /// </summary>
            public Guid CommandGuid { get; }

            /// <summary>
            /// Obtiene el comando enviado por el cliente.
            /// </summary>
            public TCommand Command { get; }

            /// <summary>
            /// Obtiene un lector binario de datos para extraer información
            /// de la solicitud.
            /// </summary>
            public BinaryReader Reader { get; }

            /// <summary>
            /// Obtiene al protocolo padre de esta solicitud.
            /// </summary>
            public ManagedCommandProtocol<TClient, TCommand, TResult> Parent { get; }

            /// <summary>
            /// Obtiene el contenido del Payload de esta solicitud.
            /// </summary>
            /// <returns></returns>
            public byte[] GetPayload()
            {
                var ms = Reader.BaseStream as MemoryStream ?? throw new TamperException();
                var pos = ms.Position;
                var toEnd = (int)(ms.Length - pos);
                var data = Reader.ReadBytes(toEnd);
                ms.Position = pos;
                return data;
            }

            #region Helpers

            /// <summary>
            /// Responde con un error.
            /// </summary>
            public void Failure()
            {
                Respond(_errResponse ?? throw new InvalidOperationException());
            }

            /// <summary>
            /// Responde al cliente.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            public void Respond(TResult response)
            {
                Client.Send(MkResp(response));
            }

            /// <summary>
            /// Responde al cliente.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="rawData">
            /// Datos a enviar.
            /// </param>
            public void Respond(TResult response, IEnumerable<byte> rawData)
            {
                Client.Send(MkResp(response).Concat(rawData));
            }

            /// <summary>
            /// Responde al cliente.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="data">
            /// Datos a enviar.
            /// </param>
            public void Respond(TResult response, MemoryStream data)
            {
                Respond(response, data.ToArray());
            }

            /// <summary>
            /// Responde al cliente.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="dataStream">
            /// Datos a enviar.
            /// </param>
            public void Respond(TResult response, Stream dataStream)
            {
                if (!dataStream.CanRead) throw new InvalidOperationException();
                if (dataStream.CanSeek)
                {
                    using var sr = new BinaryReader(dataStream);
                    Respond(response, sr.ReadBytes((int)dataStream.Length));
                    return;
                }
                using var ms = new MemoryStream();
                dataStream.CopyTo(ms);
                Respond(response, ms);
            }

            /// <summary>
            /// Responde al cliente.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="data">
            /// Datos a enviar.
            /// </param>
            public void Respond(TResult response, IEnumerable<string> data)
            {
                using var ms = new MemoryStream();
                using var bw = new BinaryWriter(ms);
                foreach (var j in data) bw.Write(j);
                Respond(response, ms);
            }

            /// <summary>
            /// Responde al cliente.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="data">
            /// Datos a enviar.
            /// </param>
            public void Respond(TResult response, string data)
            {
                Respond(response, new[] { data });
            }

            /// <summary>
            /// Responde al cliente.
            /// </summary>
            /// <param name="client">
            /// Cliente al cual enviar una respuesta.
            /// </param>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            public void Send(TClient client, TResult response)
            {
                client.Send(MkResp(response, false));
            }

            /// <summary>
            /// Responde al cliente.
            /// </summary>
            /// <param name="client">
            /// Cliente al cual enviar una respuesta.
            /// </param>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="rawData">
            /// Datos a enviar.
            /// </param>
            public void Send(TClient client, TResult response, IEnumerable<byte> rawData)
            {
                client.Send(MkResp(response,false).Concat(rawData));
            }

            /// <summary>
            /// Responde al cliente.
            /// </summary>
            /// <param name="client">
            /// Cliente al cual enviar una respuesta.
            /// </param>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="data">
            /// Datos a enviar.
            /// </param>
            public void Send(TClient client, TResult response, MemoryStream data)
            {
                Send(client, response, data.ToArray());
            }

            /// <summary>
            /// Responde al cliente.
            /// </summary>
            /// <param name="client">
            /// Cliente al cual enviar una respuesta.
            /// </param>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="dataStream">
            /// Datos a enviar.
            /// </param>
            public void Send(TClient client, TResult response, Stream dataStream)
            {
                if (!dataStream.CanRead) throw new InvalidOperationException();
                if (dataStream.CanSeek)
                {
                    using var sr = new BinaryReader(dataStream);
                    Send(client, response, sr.ReadBytes((int)dataStream.Length));
                    return;
                }
                using var ms = new MemoryStream();
                dataStream.CopyTo(ms);
                Send(client, response, ms);
            }

            /// <summary>
            /// Responde al cliente.
            /// </summary>
            /// <param name="client">
            /// Cliente al cual enviar una respuesta.
            /// </param>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="data">
            /// Datos a enviar.
            /// </param>
            public void Send(TClient client, TResult response, IEnumerable<string> data)
            {
                using var ms = new MemoryStream();
                using var bw = new BinaryWriter(ms);
                foreach (var j in data) bw.Write(j);
                Send(client, response, ms);
            }

            /// <summary>
            /// Responde al cliente.
            /// </summary>
            /// <param name="client">
            /// Cliente al cual enviar una respuesta.
            /// </param>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="data">
            /// Datos a enviar.
            /// </param>
            public void Send(TClient client, TResult response, string data)
            {
                Send(client, response, new[] { data });
            }

            /// <summary>
            /// Envía una respuesta sin Guid a todos los demás clientes
            /// conectados al servidor.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            public void Broadcast(TResult response)
            {
                Server.Broadcast(MkResp(response, false), Client);
            }

            /// <summary>
            /// Envía una respuesta sin Guid a todos los demás clientes
            /// conectados al servidor.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="rawData">
            /// Datos a enviar.
            /// </param>
            public void Broadcast(TResult response, IEnumerable<byte> rawData)
            {
                Server.Broadcast(MkResp(response, false).Concat(rawData).ToArray(), Client);
            }

            /// <summary>
            /// Envía una respuesta sin Guid a todos los demás clientes
            /// conectados al servidor.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="data">
            /// Datos a enviar.
            /// </param>
            public void Broadcast(TResult response, MemoryStream data)
            {
                Broadcast(response,data.ToArray());
            }

            /// <summary>
            /// Envía una respuesta sin Guid a todos los demás clientes
            /// conectados al servidor.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="dataStream">
            /// Datos a enviar.
            /// </param>
            public void Broadcast(TResult response, Stream dataStream)
            {
                if (!dataStream.CanRead) throw new InvalidOperationException();
                if (dataStream.CanSeek)
                {
                    using var sr = new BinaryReader(dataStream);
                    Broadcast(response, sr.ReadBytes((int)dataStream.Length));
                    return;
                }
                using var ms = new MemoryStream();
                dataStream.CopyTo(ms);
                Broadcast(response, ms);
            }

            /// <summary>
            /// Envía una respuesta sin Guid a todos los demás clientes
            /// conectados al servidor.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="data">
            /// Datos a enviar.
            /// </param>
            public void Broadcast(TResult response, IEnumerable<string> data)
            {
                using var ms = new MemoryStream();
                using var bw = new BinaryWriter(ms);
                foreach (var j in data) bw.Write(j);
                Broadcast(response, ms);
            }

            /// <summary>
            /// Envía una respuesta sin Guid a todos los demás clientes
            /// conectados al servidor.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="data">
            /// Datos a enviar.
            /// </param>
            public void Broadcast(TResult response, string data)
            {
                Broadcast(response, new[] { data });
            }

            /// <summary>
            /// Envía una respuesta sin Guid a todos los clientes que
            /// cumplan con una condición.
            /// </summary>
            /// <param name="response">Respuesta a enviar.</param>
            /// <param name="condition">Condición a evaluar.</param>
            public void Multicast(TResult response, Predicate<TClient> condition)
            {
                Server.Multicast(MkResp(response, false), condition);
            }

            /// <summary>
            /// Envía una respuesta sin Guid a todos los clientes que
            /// cumplan con una condición.
            /// </summary>
            /// <param name="response">Respuesta a enviar.</param>
            /// <param name="rawData">Datos a enviar.</param>
            /// <param name="condition">Condición a evaluar.</param>
            public void Multicast(TResult response, IEnumerable<byte> rawData, Predicate<TClient> condition)
            {
                Server.Multicast(MkResp(response, false).Concat(rawData).ToArray(), condition);
            }

            /// <summary>
            /// Envía una respuesta sin Guid a todos los clientes que
            /// cumplan con una condición.
            /// </summary>
            /// <param name="response">Respuesta a enviar.</param>
            /// <param name="data">Datos a enviar.</param>
            /// <param name="condition">Condición a evaluar.</param>
            public void Multicast(TResult response, MemoryStream data, Predicate<TClient> condition)
            {
                Multicast(response, data.ToArray(), condition);
            }

            /// <summary>
            /// Envía una respuesta sin Guid a todos los clientes que
            /// cumplan con una condición.
            /// </summary>
            /// <param name="response">Respuesta a enviar.</param>
            /// <param name="dataStream">Datos a enviar.</param>
            /// <param name="condition">Condición a evaluar.</param>
            public void Multicast(TResult response, Stream dataStream, Predicate<TClient> condition)
            {
                if (!dataStream.CanRead) throw new InvalidOperationException();
                if (dataStream.CanSeek)
                {
                    using var sr = new BinaryReader(dataStream);
                    Multicast(response, sr.ReadBytes((int)dataStream.Length), condition);
                    return;
                }
                using var ms = new MemoryStream();
                dataStream.CopyTo(ms);
                Multicast(response, ms, condition);
            }

            /// <summary>
            /// Envía una respuesta sin Guid a todos los clientes que
            /// cumplan con una condición.
            /// </summary>
            /// <param name="response">Respuesta a enviar.</param>
            /// <param name="data">Datos a enviar.</param>
            /// <param name="condition">Condición a evaluar.</param>
            public void Multicast(TResult response, IEnumerable<string> data, Predicate<TClient> condition)
            {
                using var ms = new MemoryStream();
                using var bw = new BinaryWriter(ms);
                foreach (var j in data) bw.Write(j);
                Multicast(response, ms, condition);
            }

            /// <summary>
            /// Envía una respuesta sin Guid a todos los clientes que
            /// cumplan con una condición.
            /// </summary>
            /// <param name="response">Respuesta a enviar.</param>
            /// <param name="data">Datos a enviar.</param>
            /// <param name="condition">Condición a evaluar.</param>
            public void Multicast(TResult response, string data, Predicate<TClient> condition)
            {
                Multicast(response, new[] { data }, condition);
            }

            /// <summary>
            /// Responde con un error de forma asíncrona.
            /// </summary>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task FailureAsync()
            {
                return RespondAsync(_errResponse ?? throw new InvalidOperationException());
            }

            /// <summary>
            /// Responde al cliente de forma asíncrona.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task RespondAsync(TResult response)
            {
                return Client.SendAsync(MkResp(response));
            }

            /// <summary>
            /// Responde al cliente de forma asíncrona.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="rawData">
            /// Datos a enviar.
            /// </param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task RespondAsync(TResult response, IEnumerable<byte> rawData)
            {
                return Client.SendAsync(MkResp(response).Concat(rawData));
            }

            /// <summary>
            /// Responde al cliente de forma asíncrona.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="data">
            /// Datos a enviar.
            /// </param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task RespondAsync(TResult response, MemoryStream data)
            {
                return RespondAsync(response, data.ToArray());
            }

            /// <summary>
            /// Responde al cliente de forma asíncrona.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="dataStream">
            /// Datos a enviar.
            /// </param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task RespondAsync(TResult response, Stream dataStream)
            {
                if (!dataStream.CanRead) throw new InvalidOperationException();
                if (dataStream.CanSeek)
                {
                    using var sr = new BinaryReader(dataStream);
                    return RespondAsync(response, sr.ReadBytes((int)dataStream.Length));
                }
                using var ms = new MemoryStream();
                dataStream.CopyTo(ms);
                return RespondAsync(response, ms);
            }

            /// <summary>
            /// Responde al cliente de forma asíncrona.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="data">
            /// Datos a enviar.
            /// </param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task RespondAsync(TResult response, IEnumerable<string> data)
            {
                using var ms = new MemoryStream();
                using var bw = new BinaryWriter(ms);
                foreach (var j in data) bw.Write(j);
                return RespondAsync(response, ms);
            }

            /// <summary>
            /// Responde al cliente de forma asíncrona.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="data">
            /// Datos a enviar.
            /// </param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task RespondAsync(TResult response, string data)
            {
                return RespondAsync(response, new[] { data });
            }

            /// <summary>
            /// Responde al cliente de forma asíncrona.
            /// </summary>
            /// <param name="client">
            /// Cliente al cual enviar una respuesta.
            /// </param>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task SendAsync(TClient client,TResult response)
            {
                return client.SendAsync(MkResp(response, false));
            }

            /// <summary>
            /// Responde al cliente de forma asíncrona.
            /// </summary>
            /// <param name="client">
            /// Cliente al cual enviar una respuesta.
            /// </param>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="rawData">
            /// Datos a enviar.
            /// </param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task SendAsync(TClient client, TResult response, IEnumerable<byte> rawData)
            {
                return client.SendAsync(MkResp(response, false).Concat(rawData));
            }

            /// <summary>
            /// Responde al cliente de forma asíncrona.
            /// </summary>
            /// <param name="client">
            /// Cliente al cual enviar una respuesta.
            /// </param>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="data">
            /// Datos a enviar.
            /// </param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task SendAsync(TClient client, TResult response, MemoryStream data)
            {
                return SendAsync(client, response, data.ToArray());
            }

            /// <summary>
            /// Responde al cliente de forma asíncrona.
            /// </summary>
            /// <param name="client">
            /// Cliente al cual enviar una respuesta.
            /// </param>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="dataStream">
            /// Datos a enviar.
            /// </param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task SendAsync(TClient client, TResult response, Stream dataStream)
            {
                if (!dataStream.CanRead) throw new InvalidOperationException();
                if (dataStream.CanSeek)
                {
                    using var sr = new BinaryReader(dataStream);
                    return SendAsync(client, response, sr.ReadBytes((int)dataStream.Length));
                }
                using var ms = new MemoryStream();
                dataStream.CopyTo(ms);
                return SendAsync(client, response, ms);
            }

            /// <summary>
            /// Responde al cliente de forma asíncrona.
            /// </summary>
            /// <param name="client">
            /// Cliente al cual enviar una respuesta.
            /// </param>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="data">
            /// Datos a enviar.
            /// </param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task SendAsync(TClient client, TResult response, IEnumerable<string> data)
            {
                using var ms = new MemoryStream();
                using var bw = new BinaryWriter(ms);
                foreach (var j in data) bw.Write(j);
                return SendAsync(client, response, ms);
            }

            /// <summary>
            /// Responde al cliente de forma asíncrona.
            /// </summary>
            /// <param name="client">
            /// Cliente al cual enviar una respuesta.
            /// </param>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="data">
            /// Datos a enviar.
            /// </param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task SendAsync(TClient client, TResult response, string data)
            {
                return SendAsync(client, response, new[] { data });
            }

            /// <summary>
            /// Envía una respuesta sin Guid a todos los demás clientes
            /// conectados al servidor de forma asíncrona.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task BroadcastAsync(TResult response)
            {
                return Server.BroadcastAsync(MkResp(response, false), Client);
            }

            /// <summary>
            /// Envía una respuesta sin Guid a todos los demás clientes
            /// conectados al servidor de forma asíncrona.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="rawData">
            /// Datos a enviar.
            /// </param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task BroadcastAsync(TResult response, IEnumerable<byte> rawData)
            {
                return Server.BroadcastAsync(MkResp(response, false).Concat(rawData).ToArray(), Client);
            }

            /// <summary>
            /// Envía una respuesta sin Guid a todos los demás clientes
            /// conectados al servidor de forma asíncrona.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="data">
            /// Datos a enviar.
            /// </param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task BroadcastAsync(TResult response, MemoryStream data)
            {
                return BroadcastAsync(response, data.ToArray());
            }

            /// <summary>
            /// Envía una respuesta sin Guid a todos los demás clientes
            /// conectados al servidor de forma asíncrona.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="dataStream">
            /// Datos a enviar.
            /// </param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task BroadcastAsync(TResult response, Stream dataStream)
            {
                if (!dataStream.CanRead) throw new InvalidOperationException();
                if (dataStream.CanSeek)
                {
                    using var sr = new BinaryReader(dataStream);
                    return BroadcastAsync(response, sr.ReadBytes((int)dataStream.Length));
                }
                using var ms = new MemoryStream();
                dataStream.CopyTo(ms);
                return BroadcastAsync(response, ms);
            }

            /// <summary>
            /// Envía una respuesta sin Guid a todos los demás clientes
            /// conectados al servidor de forma asíncrona.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="data">
            /// Datos a enviar.
            /// </param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task BroadcastAsync(TResult response, IEnumerable<string> data)
            {
                using var ms = new MemoryStream();
                using var bw = new BinaryWriter(ms);
                foreach (var j in data) bw.Write(j);
                return BroadcastAsync(response, ms);
            }

            /// <summary>
            /// Envía una respuesta sin Guid a todos los demás clientes
            /// conectados al servidor de forma asíncrona.
            /// </summary>
            /// <param name="response">
            /// Respuesta a enviar.
            /// </param>
            /// <param name="data">
            /// Datos a enviar.
            /// </param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task BroadcastAsync(TResult response, string data)
            {
                return BroadcastAsync(response, new[] { data });
            }

            /// <summary>
            /// Envía una respuesta sin Guid de forma asíncrona a todos los
            /// clientes que cumplan con una condición.
            /// </summary>
            /// <param name="response">Respuesta a enviar.</param>
            /// <param name="condition">Condición a evaluar.</param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task MulticastAsync(TResult response, Predicate<TClient> condition)
            {
                return Server.MulticastAsync(MkResp(response, false), condition);
            }

            /// <summary>
            /// Envía una respuesta sin Guid de forma asíncrona a todos los
            /// clientes que cumplan con una condición.
            /// </summary>
            /// <param name="response">Respuesta a enviar.</param>
            /// <param name="rawData">Datos a enviar.</param>
            /// <param name="condition">Condición a evaluar.</param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task MulticastAsync(TResult response, IEnumerable<byte> rawData, Predicate<TClient> condition)
            {
                return Server.MulticastAsync(MkResp(response, false).Concat(rawData).ToArray(), condition);
            }

            /// <summary>
            /// Envía una respuesta sin Guid de forma asíncrona a todos los
            /// clientes que cumplan con una condición.
            /// </summary>
            /// <param name="response">Respuesta a enviar.</param>
            /// <param name="data">Datos a enviar.</param>
            /// <param name="condition">Condición a evaluar.</param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task MulticastAsync(TResult response, MemoryStream data, Predicate<TClient> condition)
            {
                return MulticastAsync(response, data.ToArray(), condition);
            }

            /// <summary>
            /// Envía una respuesta sin Guid de forma asíncrona a todos los
            /// clientes que cumplan con una condición.
            /// </summary>
            /// <param name="response">Respuesta a enviar.</param>
            /// <param name="dataStream">Datos a enviar.</param>
            /// <param name="condition">Condición a evaluar.</param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task MulticastAsync(TResult response, Stream dataStream, Predicate<TClient> condition)
            {
                if (!dataStream.CanRead) throw new InvalidOperationException();
                if (dataStream.CanSeek)
                {
                    using var sr = new BinaryReader(dataStream);
                    return MulticastAsync(response, sr.ReadBytes((int)dataStream.Length), condition);
                }
                using var ms = new MemoryStream();
                dataStream.CopyTo(ms);
                return MulticastAsync(response, ms, condition);
            }

            /// <summary>
            /// Envía una respuesta sin Guid de forma asíncrona a todos los
            /// clientes que cumplan con una condición.
            /// </summary>
            /// <param name="response">Respuesta a enviar.</param>
            /// <param name="data">Datos a enviar.</param>
            /// <param name="condition">Condición a evaluar.</param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task MulticastAsync(TResult response, IEnumerable<string> data, Predicate<TClient> condition)
            {
                using var ms = new MemoryStream();
                using var bw = new BinaryWriter(ms);
                foreach (var j in data) bw.Write(j);
                return MulticastAsync(response, ms, condition);
            }

            /// <summary>
            /// Envía una respuesta sin Guid de forma asíncrona a todos los
            /// clientes que cumplan con una condición.
            /// </summary>
            /// <param name="response">Respuesta a enviar.</param>
            /// <param name="data">Datos a enviar.</param>
            /// <param name="condition">Condición a evaluar.</param>
            /// <returns>
            /// Un <see cref="Task"/> que permite monitorear la operación.
            /// </returns>
            public Task MulticastAsync(TResult response, string data, Predicate<TClient> condition)
            {
                return MulticastAsync(response, new[] { data }, condition);
            }

            #endregion
        }

        /// <summary>
        /// Describe la firma de un comando del protocolo.
        /// </summary>
        public delegate void CommandCallback(Request request);

        /// <summary>
        /// Describe la firma de un comando del protocolo que puede
        /// ejecutarse asíncronamente.
        /// </summary>
        public delegate Task AsyncCommandCallback(Request request);

        private static readonly Func<TResult, byte[]> _toResponse;
        private static readonly TResult? _errResponse;
        private static readonly TResult? _notMappedResponse;
        private static readonly TResult? _unkResponse;
        private static readonly MethodInfo _readCmd;

        /// <summary>
        /// Inicializa la clase
        /// <see cref="ManagedCommandProtocol{TClient, TCommand, TResponse}"/>.
        /// </summary>
        static ManagedCommandProtocol()
        {
            _toResponse = EnumExtensions.ToBytes<TResult>();
            _readCmd = BinaryReaderExtensions.GetBinaryReadMethod(typeof(TCommand).GetEnumUnderlyingType())!;

            var vals = Enum.GetValues(typeof(TResult)).OfType<TResult>().ToArray();
            _errResponse = vals.FirstOrDefault(p => p.HasAttr<ErrorResponseAttribute>());
            _unkResponse = (TResult?)vals.FirstOrDefault(p => p.HasAttr<UnknownResponseAttribute>()) ?? _errResponse;
            _notMappedResponse = (TResult?)vals.FirstOrDefault(p => p.HasAttr<NotMappedResponseAttribute>()) ?? _unkResponse;
        }

        /// <summary>
        /// Activa o desactiva el escaneo del tipo de instancia a construir
        /// en busca de funciones de manejo de comandos del protocolo.
        /// </summary>
        public static bool ScanTypeOnCtor { get; set; } = true;

        /// <summary>
        /// Activa o desactiva el mapeo de funciones por convención de
        /// nombres para el manejo de comandos del protocolo.
        /// </summary>
        public static bool EnableMapByName { get; set; } = true;

        /// <summary>
        /// Activa o desactiva el salto de comandos mapeados, efectivamente
        /// causando el efecto inverso en el mecanismo de protección anti
        /// mapeo duplicado.
        /// </summary>
        public static bool SkipMapped { get; set; } = true;

        private static IEnumerable<CommandCallback> ScanType(ManagedCommandProtocol<TClient, TCommand, TResult> instance)
        {
            return instance.GetType().GetMethods().WithSignature<CommandCallback>()
                .Concat(instance.PropertiesOf<CommandCallback>())
                .Concat(instance.FieldsOf<CommandCallback>());
        }
        private static TCommand ReadCommand(BinaryReader br)
        {
            return (TCommand)Enum.ToObject(typeof(TCommand), _readCmd.Invoke(br, Array.Empty<object>())!);
        }

        private readonly Dictionary<TCommand, CommandCallback> _commands = new Dictionary<TCommand, CommandCallback>();

        /// <summary>
        /// Obtiene o establece un valor que indica si el protocolo
        /// adjuntará automáticamente el Guid de respuesta al comando
        /// enviado por un cliente.
        /// </summary>
        protected bool AppendGuid { get; set; } = true;

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ManagedCommandProtocol{TClient, TCommand, TResponse}"/>.
        /// </summary>
        protected ManagedCommandProtocol()
        {
            if (!ScanTypeOnCtor) return;
            foreach (var j in ScanType(this))
            {
                var attrs = j.Method.GetCustomAttributes(false).OfType<IValueAttribute<TCommand>>().ToList();
                if (attrs.Any()) // Mapeo por configuración
                {
                    foreach (var k in attrs)
                    {
                        if (_commands.ContainsKey(k.Value)) throw new DataAlreadyExistsException();
                        _commands.Add(k.Value, j);
                    }
                }
                else if (EnableMapByName) // Mapeo por convención
                {
                    if (!Enum.TryParse(j.Method.Name, out TCommand k)) continue;
                    WireUp(k, j);
                }
            }
            foreach(var l in WireUp())
            {
                WireUp(l.Key, l.Value);
            }
        }

        /// <summary>
        /// Cuando se invalida, permite obtener una colección con los
        /// métodos que serán llamados al recibir el comando especificado.
        /// </summary>
        /// <returns>
        /// Una enumeración de <see cref="KeyValuePair{TKey, TValue}"/>
        /// cuya llave es un comando y cuyo valor es el 
        /// <see cref="CommandCallback"/> a ejecutar al recibir dicho
        /// comando.
        /// </returns>
        protected virtual IEnumerable<KeyValuePair<TCommand, CommandCallback>> WireUp()
        {
            yield break;
        }

        /// <summary>
        /// Conecta manualmente un comando con un
        /// <see cref="CommandCallback"/> de atención.
        /// </summary>
        /// <param name="command">
        /// Comando a conectar.
        /// </param>
        /// <param name="action">
        /// Acción a ejecutar al recibir el comando.
        /// </param>
        public void WireUp(TCommand command, CommandCallback action)
        {
            if (_commands.ContainsKey(command))
            {
                if (SkipMapped) return;
                throw new DataAlreadyExistsException();
            }
            _commands.Add(command, action);
        }

        /// <summary>
        /// Conecta manualmente un comando con un
        /// <see cref="AsyncCommandCallback"/> de atención.
        /// </summary>
        /// <param name="command">
        /// Comando a conectar.
        /// </param>
        /// <param name="action">
        /// Acción asíncrona a ejecutar al recibir el comando.
        /// </param>
        public void WireUp(TCommand command, AsyncCommandCallback action)
        {
            if (_commands.ContainsKey(command))
            {
                if (SkipMapped) return;
                throw new DataAlreadyExistsException();
            }
            _commands.Add(command, async o => await action(o));
        }

        /// <summary>
        /// Retransmite todos los datos recibidos por el comando
        /// especificado.
        /// </summary>
        /// <param name="command">
        /// Comando a recibir para la retransmisión de datos.
        /// </param>
        /// <param name="requestResult">
        /// Resultado a enviar al cliente que inició la solicitud.
        /// </param>
        /// <param name="relayResult">
        /// Resultado a enviar a los demás clientes al retransmitir la
        /// información.
        /// </param>
        public void Relay(TCommand command, TResult requestResult, TResult relayResult)
        {
            WireUp(command, request => 
            {
                request.Respond(requestResult);
                var data = request.GetPayload();
                request.Broadcast(relayResult, data);
            });
        }
        
        /// <summary>
        /// Crea una respuesta a partir del valor
        /// <typeparamref name="TResult"/> especificado.
        /// </summary>
        /// <param name="resp">
        /// Valor a partir del cual crear la respuesta.
        /// </param>
        /// <returns>
        /// Un arreglo de bytes con la respuesta generada.
        /// </returns>
        public byte[] MakeResponse(TResult resp) => new byte[1].Concat(_toResponse(resp)).ToArray();

        /// <summary>
        /// Atiende al cliente.
        /// </summary>
        /// <param name="client">Cliente a atender.</param>
        /// <param name="data">bytes RAW de la solicitud.</param>
        public override sealed void ClientAttendant(TClient client, byte[] data)
        {
            using var ms = new MemoryStream(data);
            using var br = new BinaryReader(ms);
            try
            {
                var commandGuid = ms.Length > 16 ? br.ReadGuid() : Guid.NewGuid();
                var c = ReadCommand(br);

                if (_commands.ContainsKey(c))
                {
                    _commands[c](new Request(commandGuid, br, client, Server, c, this));
                }
                else
                {
                    NotMappedCommand?.Invoke(this, c);
                    if (!(_notMappedResponse is null)) client.Send(MakeResponse(_notMappedResponse.Value));
                    else if (!(_errResponse is null)) client.Send(MakeResponse(_errResponse.Value));
                }
            }
            catch (Exception ex)
            {
                ServerError?.Invoke(this, ex);
                if (!(_errResponse is null)) client.Send(MakeResponse(_errResponse.Value));
            }
        }

        /// <summary>
        /// Ocurre cuando un cliente envía un comando para el cual no existe un manejador.
        /// </summary>
        public event EventHandler<ValueEventArgs<TCommand>>? NotMappedCommand;

        /// <summary>
        /// Ocurre cuando el servidor encuentra un error.
        /// </summary>
        public event EventHandler<ExceptionEventArgs>? ServerError;
    }

    /// <summary>
    /// Describe un protocolo de comandos administrado por MCART.
    /// </summary>
    /// <typeparam name="TCommand">
    /// <see cref="Enum"/> con los comandos aceptados.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// <see cref="Enum"/> con las respuestas generadas.
    /// </typeparam>
    public abstract class ManagedCommandProtocol<TCommand, TResult> : ManagedCommandProtocol<Client, TCommand, TResult> where TCommand : struct, Enum where TResult : struct, Enum
    { }
}