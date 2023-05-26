/*
ProtocolFormatAttribute.cs

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

using System.Diagnostics;
using static System.AttributeTargets;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Attributes;

/// <summary>
/// Establece un formato de protocolo para abrir un vínculo por medio
/// del sistema operativo.
/// </summary>
[AttributeUsage(Property | Field)]
[Serializable]
public sealed class ProtocolFormatAttribute : Attribute, IValueAttribute<string>
{
    /// <summary>
    /// Establece un formato de protocolo para abrir un vínculo por medio del sistema operativo.
    /// </summary>
    /// <param name="format">Máscara a aplicar.</param>
    public ProtocolFormatAttribute(string format)
    {
        Format = EmptyChecked(format, nameof(format));
    }

    /// <summary>
    /// Formato de llamada de protocolo.
    /// </summary>
    public string Format { get; }

    /// <summary>
    /// Obtiene el valor de este atributo.
    /// </summary>
    string IValueAttribute<string>.Value => Format;

    /// <summary>
    /// Abre un url con este protocolo formateado.
    /// </summary>
    /// <param name="url">
    /// URL del recurso a abrir por medio del protocolo definido por
    /// este atributo.
    /// </param>
    /// <returns>
    /// Una instancia de la clas <see cref="Process"/> que representa al
    /// proceso del sistema operativo que ha sido cargado al abrir el 
    /// <paramref name="url"/> especificado.
    /// </returns>
    public Process? Open(string url)
    {
        return string.IsNullOrWhiteSpace(url) ? null : Process.Start(string.Format(Format, url));
    }
}
