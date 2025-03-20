/*
AssemblyUnpacker.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using System.Reflection;
using TheXDS.MCART.Exceptions;

namespace TheXDS.MCART.Resources;

/// <summary>
/// Clase base que permite definir un <see cref="IUnpacker{T}"/> que extrae
/// recursos incrustados desde un <see cref="Assembly"/>.
/// </summary>
/// <typeparam name="T">Tipo de recursos a extraer.</typeparam>
/// <param name="assembly">
/// <see cref="Assembly"/> desde donde se extraerán los recursos
/// incrustados.
/// </param>
/// <param name="path">
/// Ruta (en formato de espacio de nombre) donde se ubicarán los
/// recursos incrustados.
/// </param>
public abstract partial class AssemblyUnpacker<T>(Assembly assembly, string path) : IUnpacker<T>
{
    private readonly string _path = path ?? throw new ArgumentNullException(nameof(path));
    private readonly Assembly _assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));

    /// <summary>
    /// Obtiene un <see cref="Stream"/> desde el cual leer un recurso
    /// incrustado.
    /// </summary>
    /// <param name="id">Identificador del recurso incrustado.</param>
    /// <returns>
    /// Un <see cref="Stream"/> desde el cual leer un recurso incrustado.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="id"/> es una cadena vacía o
    /// <see langword="null"/>.
    /// </exception>
    protected Stream? UnpackStream(string id)
    {
        UnpackStream_Contract(id);
        return _assembly.GetManifestResourceStream($"{_path}.{id}");
    }

    /// <summary>
    /// Obtiene un <see cref="Stream"/> desde el cual extraer un recurso
    /// incrustado comprimido.
    /// </summary>
    /// <param name="id">Identificador del recurso incrustado.</param>
    /// <param name="compressor">
    /// <see cref="ICompressorGetter"/> a utilizar para extraer al recurso,
    /// o <see langword="null"/> para no utilizar un extractor de 
    /// descompresión.
    /// </param>
    /// <returns>
    /// Un <see cref="Stream"/> desde el cual leer un recurso incrustado
    /// sin comprimir.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="id"/> es una cadena vacía o
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="MissingResourceException">
    /// Se produce si no se ha encontrado un recurso incrustado con el ID
    /// especificado en la ruta definida para este extractor de recursos.
    /// </exception>
    protected Stream UnpackStream(string id, ICompressorGetter? compressor)
    {
        UnpackStream_Contract(id);
        ICompressorGetter? c = compressor ?? new NullCompressorGetter();
        return c.GetCompressor(_assembly?.GetManifestResourceStream($"{_path}.{id}{c.Extension}") ?? throw new MissingResourceException(id));
    }

    /// <summary>
    /// Obtiene un recurso identificable.
    /// </summary>
    /// <param name="id">Identificador del recurso.</param>
    /// <returns>Un recurso de tipo <typeparamref name="T" />.</returns>
    public abstract T Unpack(string id);

    /// <summary>
    /// Extrae un recurso comprimido utilizando el compresor con el
    /// identificador especificado.
    /// </summary>
    /// <param name="id">Identificador del recurso.</param>
    /// <param name="compressor">
    /// <see cref="ICompressorGetter" /> a utilizar para extraer al recurso.
    /// </param>
    /// <returns>
    /// Un recurso sin comprimir de tipo <typeparamref name="T" />.
    /// </returns>
    public abstract T Unpack(string id, ICompressorGetter compressor);

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
    public virtual bool TryUnpack(string id, out T result)
    {
        return AssemblyUnpacker<T>.InternalTryUnpack(() => Unpack(id), out result);
    }

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
    public virtual bool TryUnpack(string id, ICompressorGetter compressor, out T result)
    {
        return AssemblyUnpacker<T>.InternalTryUnpack(() => Unpack(id, compressor), out result);
    }

    private static bool InternalTryUnpack(Func<T> function, out T result)
    {
        try
        {
            result = function();
            return true;
        }
        catch
        {
            result = default!;
            return false;
        }
    }
}
