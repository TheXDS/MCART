/*
PassiveClient.cs

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
using System.Threading.Tasks;

namespace TheXDS.MCART.Networking.Client
{
    /// <inheritdoc />
    /// <summary>
    /// Clase base para los protocolos cliente que pueden escuchar al servidor de forma pasiva.
    /// </summary>
    public abstract class PassiveClient : ClientBase
    {
        /// <inheritdoc />
        /// <summary>
        ///     Este método se invalida para implementar
        ///     <see cref="ClientBase"/> correctamente.
        /// </summary>
        protected override void PostConnection()
        {
            /*
             *  Un protocolo pasivo no requiere de ninguna acción.
             */
        }

        /// <summary>
        ///     Envía una solicitud al servidor, y espera la respuesta del
        ///     mismo.
        /// </summary>
        /// <returns>
        ///     La respuesta obtenida del servidor luego de hacer la solicitud.
        /// </returns>
        public byte[] TalkToServer() => TalkToServer(new byte[0]);

        /// <summary>
        ///     Envía una solicitud al servidor, y espera la respuesta del
        ///     mismo.
        /// </summary>
        /// <param name="data">
        ///     Datos a enviar al servidor.
        /// </param>
        /// <returns>
        ///     La respuesta obtenida del servidor luego de hacer la solicitud.
        /// </returns>
        public byte[] TalkToServer(byte[] data)
        {
            var ns = Connection?.GetStream() ?? throw new InvalidOperationException();
            if (data?.Length > 0)
            {
                ns.Write(data, 0, data.Length);
            }

            var resp = new List<byte>();
            do
            {
                var buff = new byte[Connection.ReceiveBufferSize];
                var sze = ns.Read(buff, 0, buff.Length);
                if (sze < Connection.ReceiveBufferSize) Array.Resize(ref buff, sze);
                resp.AddRange(buff);

            } while (ns.DataAvailable);
            return resp.ToArray();
        }

        /// <summary>
        ///     Envía una solicitud al servidor, y espera la respuesta del
        ///     mismo de forma asíncrona.
        /// </summary>
        /// <returns>
        ///     La respuesta obtenida del servidor luego de hacer la solicitud.
        /// </returns>
        public Task<byte[]> TalkToServerAsync() => TalkToServerAsync(new byte[0]);

        /// <summary>
        ///     Envía una solicitud al servidor, y espera la respuesta del
        ///     mismo de forma asíncrona.
        /// </summary>
        /// <param name="data">
        ///     Datos a enviar al servidor.
        /// </param>
        /// <returns>
        ///     La respuesta obtenida del servidor luego de hacer la solicitud.
        /// </returns>
        public async Task<byte[]> TalkToServerAsync(byte[] data)
        {
            var ns = Connection?.GetStream() ?? throw new InvalidOperationException();
            if (data?.Length > 0)
            {
                await ns.WriteAsync(data, 0, data.Length);
            }

            var resp = new List<byte>();
            do
            {
                var buff = new byte[Connection.ReceiveBufferSize];
                var sze = await ns.ReadAsync(buff, 0, buff.Length);
                if (sze < Connection.ReceiveBufferSize) Array.Resize(ref buff, sze);
                resp.AddRange(buff);

            } while (ns.DataAvailable);
            return resp.ToArray();
        }
    }
}