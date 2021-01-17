/*
IStreamUriParser.cs

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
using System.IO;
using System.Threading.Tasks;

namespace TheXDS.MCART.Types.Base
{
    /// <summary>
    /// Define una serie de miembros a implementar por un tipo que permita
    /// interpretar un <see cref="Uri"/> y obtener un <see cref="Stream"/>
    /// a partir del mismo.
    /// </summary>
    public interface IStreamUriParser
    {
        /// <summary>
        /// Obtiene un valor que determina si el <see cref="Stream"/>
        /// producido por este objeto requiere ser cargado por completo en
        /// un búffer de lectura en memoria.
        /// </summary>
        bool PreferFullTransfer { get; }

        /// <summary>
        /// Obtiene un <see cref="Stream"/> que enlaza al recurso
        /// solicitado, seleccionando el método más apropiado para obtener
        /// dicho flujo de datos.
        /// </summary>
        /// <param name="uri">
        /// <see cref="Uri"/> a abrir para lectura.
        /// </param>
        /// <returns>
        /// Un <see cref="Stream"/> desde el cual puede leerse el recurso
        /// apuntado por el <see cref="Uri"/> especificado.
        /// </returns>
        Stream? GetStream(Uri uri);

        /// <summary>
        /// Obtiene un <see cref="Stream"/> que enlaza al recurso
        /// solicitado, seleccionando el método más apropiado para obtener
        /// dicho flujo de datos.
        /// </summary>
        /// <param name="uri">
        /// <see cref="Uri"/> a abrir para lectura.
        /// </param>
        /// <returns>
        /// Un <see cref="Stream"/> desde el cual puede leerse el recurso
        /// apuntado por el <see cref="Uri"/> especificado.
        /// </returns>
        Task<Stream?> GetStreamAsync(Uri uri);

        /// <summary>
        /// Determina si este <see cref="IStreamUriParser"/> puede crear un
        /// <see cref="Stream"/> a partir del <see cref="Uri"/>
        /// especificado.
        /// </summary>
        /// <param name="uri">
        /// <see cref="Uri"/> a comprobar.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si este <see cref="IStreamUriParser"/>
        /// puede crear un <see cref="Stream"/> a partir del
        /// <see cref="Uri"/> especificado, <see langword="false"/> en caso
        /// contrario.
        /// </returns>
        bool Handles(Uri uri);

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
        Stream? Open(Uri uri);

        /// <summary>
        /// Abre el <see cref="Stream"/> desde el <see cref="Uri"/>
        /// especificado, y lo carga completamente en un nuevo
        /// <see cref="Stream"/> intermedio antes de devolverlo.
        /// </summary>
        /// <param name="uri">
        /// <see cref="Uri"/> a abrir para lectura.
        /// </param>
        /// <returns>
        /// Un <see cref="Stream"/> desde el cual puede leerse el recurso
        /// apuntado por el <see cref="Uri"/> especificado.
        /// </returns>
        Stream? OpenFullTransfer(Uri uri);

        /// <summary>
        /// Abre el <see cref="Stream"/> desde el <see cref="Uri"/>
        /// especificado, y lo carga completamente en un nuevo
        /// <see cref="Stream"/> intermedio de forma asíncrona antes de
        /// devolverlo.
        /// </summary>
        /// <param name="uri">
        /// <see cref="Uri"/> a abrir para lectura.
        /// </param>
        /// <returns>
        /// Un <see cref="Stream"/> desde el cual puede leerse el recurso
        /// apuntado por el <see cref="Uri"/> especificado.
        /// </returns>
        Task<Stream?> OpenFullTransferAsync(Uri uri);
    }
}