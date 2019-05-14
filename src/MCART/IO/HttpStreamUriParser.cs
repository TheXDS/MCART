/*
FileStreamUriParser.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TheXDS.MCART.Networking;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.IO
{
    /// <summary>
    ///     Obtiene un <see cref="Stream"/> a partir de un <see cref="Uri"/>
    ///     que apunta a un recurso web.
    /// </summary>
    public class HttpStreamUriParser : SimpleStreamUriParser
    {
        /// <summary>
        ///     Enumera los protocolos soportados por este
        ///     <see cref="HttpStreamUriParser"/>.
        /// </summary>
        protected override IEnumerable<string> SchemeList
        {
            get
            {
                yield return "http";
                yield return "https";
            }
        }

        /// <summary>
        ///     Abre un <see cref="Stream"/> desde el <see cref="Uri"/>
        ///     especificado.
        /// </summary>
        /// <param name="uri">Dirección web a resolver.</param>
        /// <returns>
        ///     Un <see cref="Stream"/> que permite obtener el recurso apuntado
        ///     por el <see cref="Uri"/> especificado.
        /// </returns>
        public override Stream? Open(Uri uri)
        {
            var req = WebRequest.Create(uri);
            req.Timeout = 10000;
            return req.GetResponse()?.GetResponseStream();
        }

        /// <summary>
        ///     Obtiene un valor que indica si este objeto prefiere
        ///     transferencias completas a la hora de exponer un 
        ///     <see cref="Stream"/>.
        /// </summary>
        public override bool PreferFullTransfer => true;

        /// <summary>
        ///     Abre un <see cref="Stream"/> desde el <see cref="Uri"/>
        ///     especificado, haciendo una transferencia completa a la memoria
        ///     del equipo.
        /// </summary>
        /// <param name="uri">Dirección web a resolver.</param>
        /// <returns>
        ///     Un <see cref="Stream"/> que permite obtener el recurso apuntado
        ///     por el <see cref="Uri"/> especificado.
        /// </returns>
        public new async Task<MemoryStream?> OpenFullTransferAsync(Uri uri)
        {
            var ms = new MemoryStream();
            if (await DownloadHelper.DownloadHttpAsync(uri, ms) != HttpStatusCode.OK) return null;
            return ms;
        }
    }
}
