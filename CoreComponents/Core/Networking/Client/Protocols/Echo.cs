/*
Echo.cs

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

using System;
using System.Text;
using System.Threading.Tasks;
using TheXDS.MCART.Types.Extensions;

#if ExtrasBuiltIn
namespace TheXDS.MCART.Networking.Client.Protocols
{
    /// <inheritdoc />
    /// <summary>
    ///     Implementa el cliente pasivo del protocolo de Echo.
    /// </summary>
    /// <remarks>
    ///     Este protocolo utiliza TCP/IP, no IGMP.
    /// </remarks>
    [Port(7)]
    public class Echo : PassiveClient
    {
        /// <summary>
        /// Envía un paquete de echo al servidor, y devuelve su respuesta.
        /// </summary>
        /// <param name="str">Cadena a enviar al servidor.</param>
        /// <returns>
        ///     La respuesta del servidor, la cual para este protocolo debe ser
        ///     igual a la cadena enviada.
        /// </returns>
        public string Send(string str)
        {
            return Send(str, Encoding.Unicode);
        }

        /// <summary>
        /// Envía un paquete de echo al servidor, y devuelve su respuesta.
        /// </summary>
        /// <param name="str">Cadena a enviar al servidor.</param>
        /// <param name="encoding">
        ///     Codificación a utilizar para convertir la cadena a bytes.
        /// </param>
        /// <returns>
        ///     La respuesta del servidor, la cual para este protocolo debe ser
        ///     igual a la cadena enviada.
        /// </returns>
        public string Send(string str, Encoding encoding)
        {
            return encoding.GetString(TalkToServer(encoding.GetBytes(str)));
        }

        /// <summary>
        /// Comprueba el funcionamiento del protocolo Echo.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> si el protocolo de Echo funciona
        ///     correctamente, <see langword="false"/> en caso contrario.
        /// </returns>
        public bool Check()
        {
            var str = new Random().RndText(4096);
            return Send(str) == str;
        }

        /// <summary>
        ///     Envía un paquete de echo al servidor, y devuelve su respuesta
        ///     de forma asíncrona.
        /// </summary>
        /// <param name="str">Cadena a enviar al servidor.</param>
        /// <returns>
        ///     La respuesta del servidor, la cual para este protocolo debe ser
        ///     igual a la cadena enviada.
        /// </returns>
        public Task<string> SendAsync(string str)
        {
            return SendAsync(str, Encoding.Unicode);
        }

        /// <summary>
        ///     Envía un paquete de echo al servidor, y devuelve su respuesta
        ///     de forma asíncrona.
        /// </summary>
        /// <param name="str">Cadena a enviar al servidor.</param>
        /// <param name="encoding">
        ///     Codificación a utilizar para convertir la cadena a bytes.
        /// </param>
        /// <returns>
        ///     La respuesta del servidor, la cual para este protocolo debe ser
        ///     igual a la cadena enviada.
        /// </returns>
        public async Task<string> SendAsync(string str, Encoding encoding)
        {
            return encoding.GetString(await TalkToServerAsync(encoding.GetBytes(str)));
        }

        /// <summary>
        /// Comprueba el funcionamiento del protocolo Echo de forma asíncrona.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> si el protocolo de Echo funciona
        ///     correctamente, <see langword="false"/> en caso contrario.
        /// </returns>
        public async Task<bool> CheckAsync()
        {
            var str = new Random().RndText(4096);
            return await SendAsync(str) == str;
        }
    }
}
#endif