/*
Unpacker.cs

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

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Misc;

namespace TheXDS.MCART.Resources;

/// <summary>
/// <see cref="AssemblyUnpacker{T}"/> que expone directamente los
/// <see cref="Stream"/> de los recursos incrustados de un ensamblado.
/// </summary>
/// <param name="assembly">
/// <see cref="Assembly"/> desde donde se extraerán los recursos
/// incrustados.
/// </param>
/// <param name="path">
/// Ruta (en formato de espacio de nombre) donde se ubicarán los
/// recursos incrustados.
/// </param>
[RequiresUnreferencedCode(AttributeErrorMessages.ClassScansForTypes)]
[RequiresDynamicCode(AttributeErrorMessages.ClassCallsDynamicCode)]
public class Unpacker(Assembly assembly, string path) : AssemblyUnpacker<Stream>(assembly, path)
{

    /// <summary>
    /// Initializes a new instance of the <see cref="Unpacker"/>,
    /// buscando los recursos a extraer en el ensamblado que declara al tipo
    /// especificado, además usando el mismo como la referencia de ruta (en
    /// formato de espacio de nombre) para buscar los recursos incrustados.
    /// </summary>
    /// <param name="resReference">
    /// Tipo a tomar como referencia de la ubicación de los recursos.
    /// </param>
    public Unpacker(Type resReference) : this(resReference.Assembly, resReference.FullName ?? resReference.ToString()) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Unpacker"/>,
    /// buscando los recursos a extraer en el ensamblado desde el cual se crea
    /// esta instancia.
    /// </summary>
    /// <param name="path">
    /// Ruta (en formato de espacio de nombre) donde se ubicarán los
    /// recursos incrustados.
    /// </param>
    public Unpacker(string path) : this(Assembly.GetCallingAssembly(), path) { }

    /// <summary>
    /// Obtiene un recurso identificable.
    /// </summary>
    /// <param name="id">Identificador del recurso.</param>
    /// <returns>
    /// Un <see cref="Stream"/> desde donde se podrá leer el recurso
    /// incrustado.
    /// </returns>
    public override Stream Unpack(string id) => UnpackStream(id) ?? throw new MissingResourceException(id);

    /// <summary>
    /// Extrae un recurso comprimido utilizando el compresor con el
    /// identificador especificado.
    /// </summary>
    /// <param name="id">Identificador del recurso.</param>
    /// <param name="compressor">
    /// <see cref="ICompressorGetter"/> a utilizar para extraer al recurso.
    /// </param>
    /// <returns>
    /// Un <see cref="Stream"/> desde donde se podrá leer el recurso
    /// incrustado sin comprimir.
    /// </returns>
    public override Stream Unpack(string id, ICompressorGetter? compressor) => UnpackStream(id, compressor);
}
