/*
IUnpacker.cs

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

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Define una serie de métodos a implementar por una clase que permita
    /// obtener y extraer recursos.
    /// </summary>
    /// <typeparam name="T">Tipo de recursos a obtener.</typeparam>
    public interface IUnpacker<T>
    {
        /// <summary>
        /// Obtiene un recurso identificable.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <returns>Un recurso de tipo <typeparamref name="T"/>.</returns>
        T Unpack(string id);

        /// <summary>
        /// Extrae un recurso comprimido utilizando el compresor con el
        /// identificador especificado.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="compressorId">
        /// Identificador del compresor a utilizar para extraer el recurso.
        /// </param>
        /// <returns>
        /// Un recurso sin comprimir de tipo <typeparamref name="T"/>.
        /// </returns>
        T Unpack(string id, string compressorId);

        /// <summary>
        /// Extrae un recurso comprimido utilizando el compresor con el
        /// identificador especificado.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="compressor">
        /// <see cref="ICompressorGetter"/> a utilizar para extraer el
        /// recurso.
        /// </param>
        /// <returns>
        /// Un recurso sin comprimir de tipo <typeparamref name="T"/>.
        /// </returns>
        T Unpack(string id, ICompressorGetter compressor);

        /// <summary>
        /// Intenta obtener un recurso identificable.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="result">
        /// Parámetro de salida. Un recurso de tipo 
        /// <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si el recurso se extrajo 
        /// satisfactoriamente, <see langword="false"/> en caso contrario.
        /// </returns>
        bool TryUnpack(string id, out T result);

        /// <summary>
        /// Intenta obtener un recurso identificable.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="result">
        /// Parámetro de salida. Un recurso de tipo 
        /// <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si el recurso se extrajo 
        /// satisfactoriamente, <see langword="false"/> en caso contrario.
        /// </returns>
        /// <param name="compressorId">
        /// Identificador del compresor a utilizar para extraer el recurso.
        /// </param>
        bool TryUnpack(string id, string compressorId, out T result);

        /// <summary>
        /// Intenta obtener un recurso identificable.
        /// </summary>
        /// <param name="id">Identificador del recurso.</param>
        /// <param name="result">
        /// Parámetro de salida. Un recurso de tipo 
        /// <typeparamref name="T"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si el recurso se extrajo 
        /// satisfactoriamente, <see langword="false"/> en caso contrario.
        /// </returns>
        /// <param name="compressor">
        /// <see cref="ICompressorGetter"/> a utilizar para extraer el
        /// recurso.
        /// </param>        
        bool TryUnpack(string id, ICompressorGetter compressor, out T result);
    }
}