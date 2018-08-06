/*
SelfWiredCommandClient.cs

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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;

namespace TheXDS.MCART.Networking.Client
{
    public abstract class SelfWiredCommandClient<TCommand, TResponse> : ActiveClient where TCommand : struct, Enum where TResponse : struct, Enum
    {
        private readonly TResponse? _errResponse;
        private readonly TResponse? _unkResponse;

        public event EventHandler ServerError;
        public event EventHandler UnknownCommandIssued;

        /// <summary>
        ///     Describe la firma de una respuesta del protocolo.
        /// </summary>
        public delegate void ResponseCallBack(BinaryReader br);
        private readonly Dictionary<TResponse, ResponseCallBack> _responses = new Dictionary<TResponse, ResponseCallBack>();

        public static TResponse ReadResponse(BinaryReader br)
        {
            switch (Marshal.SizeOf<TResponse>())
            {
                case 1:
                    return (TResponse)Enum.ToObject(typeof(TCommand), br.ReadByte());
                case 2:
                    return (TResponse)Enum.ToObject(typeof(TCommand), br.ReadInt16());
                case 4:
                    return (TResponse)Enum.ToObject(typeof(TCommand), br.ReadInt32());
                case 8:
                    return (TResponse)Enum.ToObject(typeof(TCommand), br.ReadInt64());
                default:
                    throw new PlatformNotSupportedException();
            }
        }

        public static byte[] MakeCommand(TCommand command)
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

        protected override async void PostConnection()
        {
            while (!(Connection?.Disposed ?? true) && Connection.GetStream() is NetworkStream ns)
            {
                try
                {
                    var outp = new List<byte>();
                    do
                    {
                        var buff = new byte[Connection.ReceiveBufferSize];
                        var sze = await ns.ReadAsync(buff, 0, buff.Length);
                        if (sze < Connection.ReceiveBufferSize) Array.Resize(ref buff, sze);
                        outp.AddRange(buff);
                    } while (ns.DataAvailable);


                    using (var ms = new MemoryStream(outp.ToArray()))
                    using (var br = new BinaryReader(ms))
                    {
                        if (_interrupts.TryDequeue(out var callback))
                        {
                            callback.Invoke(br);
                        }
                        else
                        {
                            var cmd = ReadResponse(br);
                            if (_errResponse.Equals(cmd)) ServerError?.Invoke(this, EventArgs.Empty);
                            if (_unkResponse.Equals(cmd)) UnknownCommandIssued?.Invoke(this, EventArgs.Empty);
                            if (_responses.ContainsKey(cmd))
                                _responses[cmd](br);
                            else AttendServer(outp.ToArray());
                        }
                    }
                }
                catch { RaiseConnectionLost(); }
            }
        }

        private readonly ConcurrentQueue<ResponseCallBack> _interrupts = new ConcurrentQueue<ResponseCallBack>();

        protected SelfWiredCommandClient()
        {
            var vals = Enum.GetValues(typeof(TResponse)).OfType<TResponse?>().ToArray();

            _errResponse = vals.FirstOrDefault(p => p.HasAttr<ErrorResponseAttribute>());
            _unkResponse = vals.FirstOrDefault(p => p.HasAttr<UnknownResponseAttribute>());

            var tCmdAttr = Objects.GetTypes<IValueAttribute<TResponse>>(true).FirstOrDefault() ??
                           throw new MissingTypeException(typeof(IValueAttribute<TResponse>));
            foreach (var j in
                GetType().GetMethods().WithSignature<ResponseCallBack>()
                    .Concat(this.PropertiesOf<ResponseCallBack>())
                    .Concat(this.FieldsOf<ResponseCallBack>()))
            {
                var attr = j.Method.GetCustomAttributes(tCmdAttr, false).OfType<IValueAttribute<TResponse>>().FirstOrDefault();
                if (attr is null) continue;
                _responses.Add(attr.Value, j as ResponseCallBack);
            }
        }


        public async Task TalkToServerAsync(TCommand command, byte[] data, ResponseCallBack callback)
        {
            if (!(data?.Length > 0))
#if PreferExceptions
                throw new ArgumentNullException();
#else
                return;
#endif
            var ns = Connection?.GetStream() ?? throw new InvalidOperationException();
            await ns.WriteAsync(data, 0, data.Length);
            _interrupts.Enqueue(callback);
        }
        public void TalkToServer(TCommand command, byte[] data, ResponseCallBack callback)
        {
            if (!(data?.Length > 0))
#if PreferExceptions
                throw new ArgumentNullException();
#else
                return;
#endif
            var ns = Connection?.GetStream() ?? throw new InvalidOperationException();
            ns.Write(data, 0, data.Length);
            _interrupts.Enqueue(callback);
        }
    }
}