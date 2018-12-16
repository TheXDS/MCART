/*
ActiveClient.cs

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
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using TheXDS.MCART.Annotations;

#region Configuración de ReSharper

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable EventNeverSubscribedTo.Global

#endregion

namespace TheXDS.MCART.Networking.Client
{
    /// <inheritdoc />
    /// <summary>
    /// Clase base para los protocolos cliente que pueden escuchar al servidor de forma activa.
    /// </summary>
    public abstract class ActiveClient : ClientBase
    {
        /// <summary>
        /// Se produce cuando se pierde la conexión con el servidor inesperadamente.
        /// </summary>
        public event EventHandler ConnectionLost;

        /// <summary>
        ///     Envía una solicitud al servidor.
        /// </summary>
        /// <param name="data">
        ///     Datos a enviar al servidor.
        /// </param>
        public void TalkToServer([CanBeNull]IEnumerable<byte> data)
        {
            var d = data?.ToArray();
            if (!(d?.Length > 0))
#if PreferExceptions
                throw new ArgumentNullException();
#else
                return;
#endif
            var ns = Connection?.GetStream() ?? throw new InvalidOperationException();
            ns.Write(d, 0, d.Length);
        }

        /// <summary>
        ///     Envía una solicitud al servidor de forma asíncrona.
        /// </summary>
        /// <param name="data">
        ///     Datos a enviar al servidor.
        /// </param>
        /// <returns>
        ///     Un objeto <see cref="Task"/> para monitorear la operación asíncrona.
        /// </returns>
        public Task TalkToServerAsync([CanBeNull]IEnumerable<byte> data)
        {
            var d = data?.ToArray();
            if (!(d?.Length > 0))
#if PreferExceptions
                throw new ArgumentNullException();
#else
#if Lite
                //HACK: Debido a quirks inusuales del framework, la propiedad Task.CompletedTask no es pública.
                return Task.Run(() => { });
#else
                return Task.CompletedTask;
#endif
#endif
            var ns = Connection?.GetStream() ?? throw new InvalidOperationException();
            return ns.WriteAsync(d, 0, d.Length);
        }

        /// <summary>
        /// Atiende una solicitud realizada por el servidor.
        /// </summary>
        /// <param name="data">Datos recibidos desde el servidor.</param>
        public abstract void AttendServer(byte[] data);

        /// <inheritdoc />
        /// <summary>
        ///     Inicia la escucha activa del servidor.
        /// </summary>
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
                    AttendServer(outp.ToArray());
                }
                catch { RaiseConnectionLost(); }
            }
        }

        /// <summary>
        /// Genera el evento <see cref="ConnectionLost"/>.
        /// </summary>
        protected void RaiseConnectionLost()=> ConnectionLost?.Invoke(this, EventArgs.Empty);
    }
}