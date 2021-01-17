/*
IAsyncUnpacker.cs

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

using System.Threading.Tasks;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Define una serie de métodos a implementar por una clase que permita
    /// obtener y extraer recursos de forma asíncrona.
    /// </summary>
    /// <typeparam name="T">Tipo de recursos a obtener.</typeparam>
    public interface IAsyncUnpacker<T>
    {
        /// <summary>
        /// Obtiene un recurso identificable de forma asíncrona.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <returns>Un recurso de tipo <typeparamref name="T"/>.</returns>
        Task<T> UnpackAsync(string id);
        /// <summary>
        /// Extrae un recurso comprimido utilizando el compresor con el
        /// identificador especificado de forma asíncrona.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="compressorId">
        /// Identificador del compresor a utilizar para extraer al recurso.
        /// </param>
        /// <returns>
        /// Un recurso sin comprimir de tipo <typeparamref name="T"/>.
        /// </returns>
        Task<T> UnpackAsync(string id, string compressorId);
        /// <summary>
        /// Extrae un recurso comprimido utilizando el compresor con el
        /// identificador especificado de forma asíncrona.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="compressor">
        /// <see cref="ICompressorGetter"/> a utilizar para extraer al recurso.
        /// </param>
        /// <returns>
        /// Un recurso sin comprimir de tipo <typeparamref name="T"/>.
        /// </returns>
        Task<T> UnpackAsync(string id, ICompressorGetter compressor);
    }
}