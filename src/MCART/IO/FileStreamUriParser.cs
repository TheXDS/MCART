/*
FileStreamUriParser.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using System.Net;
using System.Threading.Tasks;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.IO
{
    /// <summary>
    /// Obtiene un <see cref="Stream"/> a partir de la ruta de archivo
    /// especificada por un <see cref="Uri"/>.
    /// </summary>
    public class FileStreamUriParser : SimpleStreamUriParser, IWebUriParser
    {
        /// <summary>
        /// Enumera los esquemas soportados por este
        /// <see cref="StreamUriParser"/>.
        /// </summary>
        protected override IEnumerable<string> SchemeList
        {
            get
            {
                yield return "file";
            }
        }

        /// <summary>
        /// Obtiene una respuesta Web a partir del <see cref="Uri"/>
        /// especificado.
        /// </summary>
        /// <param name="uri">Dirección web a resolver.</param>
        /// <returns>
        /// La respuesta enviada por un servidor web.
        /// </returns>
        public WebResponse GetResponse(Uri uri)
        {
            return WebRequest.Create(uri).GetResponse();
        }

        /// <summary>
        /// Obtiene una respuesta Web a partir del <see cref="Uri"/>
        /// especificado de forma asíncrona.
        /// </summary>
        /// <param name="uri">Dirección web a resolver.</param>
        /// <returns>
        /// La respuesta enviada por un servidor web.
        /// </returns>
        public Task<WebResponse> GetResponseAsync(Uri uri)
        {
            return WebRequest.Create(uri).GetResponseAsync();
        }

        /// <summary>
        /// Abre un <see cref="Stream"/> desde el <see cref="Uri"/>
        /// especificado.
        /// </summary>
        /// <param name="uri">
        /// <see cref="Uri"/> a abrir para lectura.
        /// </param>
        /// <returns>
        /// Un <see cref="Stream"/> desde el cual puede leerse el recurso
        /// apuntado por el <see cref="Uri"/> especificado.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        /// Se produce si el recurso apuntado por <paramref name="uri"/> no
        /// existe.
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        /// Se procude si no se tienen los permisos suficientes para
        /// realizar esta operación.
        /// </exception>
        /// <exception cref="IOException">
        /// Se produce si hay un problema de entrada/salida al abrir el
        /// recurso apuntado por <paramref name="uri"/>.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// Se produce si el directorio apuntado en la ruta de
        /// <paramref name="uri"/> no existe.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// Se produce si la longitud de la ruta de archivo excede los
        /// límites permitidos por el sistema operativo.
        /// </exception>
        public override Stream? Open(Uri uri)
        {
            if (uri.OriginalString.StartsWith("file://"))
            {
                try
                {
                    return WebRequest.Create(uri).GetResponse().GetResponseStream();
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                if (!File.Exists(uri.OriginalString)) return null;
                return new FileStream(uri.OriginalString, FileMode.Open);
            }
        }
    }
}