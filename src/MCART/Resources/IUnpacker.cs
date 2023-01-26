/*
IUnpacker.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace TheXDS.MCART.Resources;

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
