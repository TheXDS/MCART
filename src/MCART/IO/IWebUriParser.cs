﻿/*
IWebUriParser.cs

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
using System.Net;
using System.Threading.Tasks;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.IO
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que permita
    /// interpretar un <see cref="Uri"/> y obtener una respuesta desde un
    /// servicio web.
    /// </summary>
    public interface IWebUriParser : IStreamUriParser
    {
        /// <summary>
        /// Obtiene una respuesta Web a partir del <see cref="Uri"/>
        /// especificado.
        /// </summary>
        /// <param name="uri">Dirección web a resolver.</param>
        /// <returns>
        /// La respuesta enviada por un servidor web.
        /// </returns>
        WebResponse GetResponse(Uri uri);

        /// <summary>
        /// Obtiene una respuesta Web a partir del <see cref="Uri"/>
        /// especificado de forma asíncrona.
        /// </summary>
        /// <param name="uri">Dirección web a resolver.</param>
        /// <returns>
        /// La respuesta enviada por un servidor web.
        /// </returns>
        Task<WebResponse> GetResponseAsync(Uri uri);
    }
}