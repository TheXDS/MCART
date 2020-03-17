/*
ICompressorGetter.cs

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

using System.IO;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Define una serie de métodos a implementar por una clase que permita
    /// obtener un <see cref="Stream"/> para extraer información comprimida
    /// desde otro <see cref="Stream"/>.
    /// </summary>
    public interface ICompressorGetter
    {
        /// <summary>
        /// Obtiene un <see cref="Stream"/> para extraer información comprimida
        /// desde <paramref name="inputStream"/>.
        /// </summary>
        /// <param name="inputStream">
        /// <see cref="Stream"/> que contiene la información a extraer.
        /// </param>
        /// <returns>
        /// Un <see cref="Stream"/> que puede utilizarse para extraer
        /// información comprimida desde <paramref name="inputStream"/>.
        /// </returns>
        Stream GetCompressor(Stream inputStream);

        /// <summary>
        /// Obtiene la extensión utilizada de forma predeterminada para un
        /// recurso comprimido utilizando este
        /// <see cref="ICompressorGetter"/>.
        /// </summary>
        string? Extension { get; }
    }
}