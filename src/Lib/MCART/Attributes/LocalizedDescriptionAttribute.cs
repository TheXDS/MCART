/*
LocalizedDescriptionAttribute.cs

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

using System.Resources;
using static System.AttributeTargets;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Attributes;

/// <summary>
/// Establece un nombre personalizado localizado para describir este elemento.
/// </summary>
/// <remarks>
/// Inicializa una nueva instancia de la clase
/// <see cref="LocalizedDescriptionAttribute" />.
/// </remarks>
/// <param name="stringId">Id de la cadena a localizar.</param>
/// <param name="resourceType">Tipo que contiene la información de localización a utilizar.</param>
[AttributeUsage(All)]
[Serializable]
public sealed class LocalizedDescriptionAttribute(string stringId, Type resourceType) : System.ComponentModel.DescriptionAttribute, IValueAttribute<string>
{
    private readonly string _stringId = EmptyChecked(stringId);
    private readonly ResourceManager _res = new(NullChecked(resourceType));

    /// <inheritdoc/>
    public override string Description => _res.GetString(_stringId) ?? $"[[{_stringId}]]";

    string IValueAttribute<string>.Value => Description;
}
