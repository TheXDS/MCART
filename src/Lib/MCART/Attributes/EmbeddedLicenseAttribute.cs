/*
EmbeddedLicenseAttribute.cs

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
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Extensions;
using static System.AttributeTargets;

namespace TheXDS.MCART.Attributes;

/// <summary>
/// Establece un archivo incrustado de licencia a asociar con el elemento.
/// </summary>
[AttributeUsage(Class | AttributeTargets.Module | AttributeTargets.Assembly)]
[Serializable]
public sealed class EmbeddedLicenseAttribute : LicenseAttributeBase
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="EmbeddedLicenseAttribute" />.
    /// </summary>
    /// <param name="value">
    /// Archivo incrustado de la licencia.
    /// </param>
    /// <param name="path">
    /// Ruta del archivo embebido de licencia dentro del ensamblado.
    /// </param>
    public EmbeddedLicenseAttribute(string value, string path) 
        : this(value, path, typeof(NullGetter))
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="EmbeddedLicenseAttribute" />.
    /// </summary>
    /// <param name="value">
    /// Archivo incrustado de la licencia.
    /// </param>
    /// <param name="path">
    /// Ruta del archivo embebido de licencia dentro del ensamblado.
    /// </param>
    /// <param name="compressorType">
    /// Compressor utilizado para extraer el recurso incrustado.
    /// </param>
    public EmbeddedLicenseAttribute(string value, string path, Type compressorType) 
        : base(value)
    {
        if (!compressorType.Implements<ICompressorGetter>())
        { 
            throw new InvalidTypeException(compressorType);
        }
        Path = path;
        CompressorType = compressorType;
    }

    /// <summary>
    /// Ruta del archivo embebido de licencia dentro del ensamblado.
    /// </summary>
    public string Path { get; }

    /// <summary>
    /// Compressor utilizado para extraer el recurso incrustado.
    /// </summary>
    public Type CompressorType { get; }

    /// <summary>
    /// Lee el contenido de la licencia embebida dentro del ensamblado.
    /// </summary>
    /// <param name="context">
    /// Objeto a partir del cual se ha obtenido este atributo.
    /// </param>
    /// <returns>
    /// El contenido de la licencia.
    /// </returns>
    public override License GetLicense(object context)
    {
        Assembly? origin = context switch
        {
            Assembly a   => a,
            Type t       => t.Assembly,
            MemberInfo m => m.DeclaringType?.Assembly!,
            null         => throw new ArgumentNullException(nameof(context)),
            _            => context.GetType().Assembly,
        };

        if (Value is null) return License.Unspecified;
        string? content = new StringUnpacker(origin, Path).Unpack(Value, CompressorType.New<ICompressorGetter>());
        return new TextLicense(content.Split('\n')[0].Trim(' ', '\n', '\r'), content);
    }
}
