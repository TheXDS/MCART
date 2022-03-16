/*
ColorExtensions.cs

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

namespace TheXDS.MCART.Types.Factory;
using TheXDS.MCART.Resources;

/// <summary>
/// Extensiones para todos los elementos de tipo <see cref="Color" />.
/// </summary>
public static partial class ColorExtensions
{
    /// <summary>
    /// Obtiene una versión opaca del color especificado.
    /// </summary>
    /// <param name="color">
    /// Color a partir del cual generar una versión opaca.
    /// </param>
    /// <returns>
    /// Un color equivalente a <paramref name="color"/>, pero con total
    /// opacidad del canal alfa.
    /// </returns>
    public static Color Opaque(in this Color color)
    {
        return color + Colors.Black;
    }

    /// <summary>
    /// Obtiene una versión transparente del color especificado.
    /// </summary>
    /// <param name="color">
    /// Color a partir del cual generar una versión transparente.
    /// </param>
    /// <returns>
    /// Un color equivalente a <paramref name="color"/>, pero con total
    /// transparencia del canal alfa.
    /// </returns>
    public static Color Transparent(in this Color color)
    {
        return color - Colors.Black;
    }

    /// <summary>
    /// Crea una copia del color especificado, ajustando el nivel de
    /// transparencia del mismo.
    /// </summary>
    /// <param name="color">Color base.</param>
    /// <param name="value">Nivel de transparencia a aplicar.</param>
    /// <returns>
    /// Una copia del color, con el nivel de transparencia especificado.
    /// </returns>
    public static Color WithAlpha(this Color color, in float value)
    {
        WithAlpha_Contract(value);
        Color c = color.Clone();
        c.ScA = value;
        return c;
    }
}
