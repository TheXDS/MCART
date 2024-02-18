/*
TextUnpacker.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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
/// <see cref="AssemblyUnpacker{T}" /> que
/// extrae recursos de texto.
/// </summary>
public class TextUnpacker : AssemblyUnpacker<string>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="AssemblyUnpacker{T}" />.
    /// </summary>
    /// <param name="assembly">
    /// <see cref="Assembly" /> desde donde se extraerán los recursos
    /// incrustados.
    /// </param>
    /// <param name="path">
    /// Ruta (en formato de espacio de nombre) donde se ubicarán los
    /// recursos incrustados.
    /// </param>
    public TextUnpacker(Assembly assembly, string path) : base(assembly, path)
    {
    }

    /// <summary>
    /// Obtiene un recurso identificable.
    /// </summary>
    /// <param name="id">Identificador del recurso.</param>
    /// <returns>
    /// Una cadena con el contenido total del recurso.
    /// </returns>
    public override string Unpack(string id)
    {
        using StreamReader? sr = new(UnpackStream(id) ?? throw new MissingResourceException(id));
        return sr.ReadToEnd();
    }

    /// <summary>
    /// Extrae un recurso comprimido utilizando el compresor con el
    /// identificador especificado.
    /// </summary>
    /// <param name="id">Identificador del recurso.</param>
    /// <param name="compressorId">
    /// Identificador del compresor a utilizar para extraer al recurso.
    /// </param>
    /// <returns>
    /// Un recurso sin comprimir como una cadena.
    /// </returns>
    public override string Unpack(string id, string compressorId)
    {
        using StreamReader? sr = new(UnpackStream(id, compressorId));
        return sr.ReadToEnd();
    }

    /// <summary>
    /// Extrae un recurso comprimido utilizando el compresor con el
    /// identificador especificado.
    /// </summary>
    /// <param name="id">Identificador del recurso.</param>
    /// <param name="compressor">
    /// <see cref="ICompressorGetter" /> a utilizar para extraer al
    /// recurso.
    /// </param>
    /// <returns>
    /// Un recurso sin comprimir como una cadena.
    /// </returns>
    public override string Unpack(string id, ICompressorGetter compressor)
    {
        using StreamReader? sr = new(UnpackStream(id, compressor));
        return sr.ReadToEnd();
    }
}
