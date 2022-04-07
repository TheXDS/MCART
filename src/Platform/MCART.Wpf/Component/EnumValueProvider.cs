/*
EnumValueProvider.cs

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

namespace TheXDS.MCART.Component;
using System;
using System.Windows.Markup;

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
