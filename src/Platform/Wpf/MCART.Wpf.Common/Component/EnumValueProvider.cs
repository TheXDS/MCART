/*
EnumValueProvider.cs

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

using System.Windows.Markup;

namespace TheXDS.MCART.Component;

/// <summary>
/// Define una extensión de Markup XAML que permite obtener valores de
/// enumeración como una colección.
/// </summary>
public partial class EnumValueProvider : MarkupExtension
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="EnumValueProvider"/>.
    /// </summary>
    /// <param name="enumType">Tipo de enumeración a exponer.</param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="enumType"/> es
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Se produce si <paramref name="enumType"/> no es un tipo válido de
    /// enumeración.
    /// </exception>
    public EnumValueProvider(Type enumType)
    {
        EnumValueProvider_Contract(enumType);
        EnumType = enumType;
    }

    /// <summary>
    /// Obtiene una referencia al tipo de enumeración desde la cual se
    /// obtendrán los valores.
    /// </summary>
    public Type EnumType { get; }

    /// <inheritdoc/>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Enum.GetValues(EnumType);
    }
}
