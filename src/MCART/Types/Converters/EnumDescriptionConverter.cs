/*
EnumDescriptionConverter.cs

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

using System;
using System.ComponentModel;
using System.Globalization;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types.Converters
{
    /// <summary>
    /// Convierte un valor de enumeración a su presentación amigable como una cadena.
    /// </summary>
    public class EnumDescriptionConverter : EnumConverter
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="EnumDescriptionConverter"/>.
        /// </summary>
        /// <param name="type">Tipo de enumeración a describir</param>
        public EnumDescriptionConverter(Type type) : base(type)
        {
        }

        /// <inheritdoc/>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return destinationType == typeof(string) && value is Enum e
                ? e.GetAttr<LocalizedDescriptionAttribute>()?.Description
                    ?? e.GetAttr<System.ComponentModel.DescriptionAttribute>()?.Description
                    ?? e.GetAttr<Attributes.DescriptionAttribute>()?.Value
                    ?? e.NameOf()
                : base.ConvertTo(context, culture, value, destinationType);
        }
    }
}