/*
FloatConverterBase.cs

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

namespace TheXDS.MCART.ValueConverters.Base;
using TheXDS.MCART.Helpers;

/// <summary>
/// Clase base que incluye un método para obtener un <see cref="float"/>.
/// </summary>
public abstract class FloatConverterBase
{
    /// <summary>
    /// Convierte un valor a un <see cref="float"/>.
    /// </summary>
    /// <param name="value">
    /// Valor desde el cual obtener un <see cref="float"/>.
    /// </param>
    /// <returns>
    /// Un <see cref="float"/> obtenido a partir del valor brindado.
    /// </returns>
    protected static float GetFloat(object? value)
    {
        return value switch
        {
            float f => f,
            double d => (float)d,
            byte b => b / 255f,
            short s => s / 100f,
            int i => i,
            long l => l,
            sbyte sb => sb,
            ushort us => us / 100f,
            uint ui => ui,
            ulong ul=> ul,
            string str => float.TryParse(str, out float vv) ? vv : 0f,
            _ => (float?)Common.FindConverter<float>()?.ConvertTo(value, typeof(float)) ?? 0f
        };
    }
}
