/*
ScColor.cs

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

namespace TheXDS.MCART.Types.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using TheXDS.MCART.Types.Base;

/// <summary>
/// Estructura universal que describe un color en sus componentes alfa,
/// rojo, verde y azul.
/// </summary>
[ComplexType]
public class ScColor : IScColor
{
    /// <summary>
    /// Componente Alfa del color.
    /// </summary>
    public float ScA { get; set; }

    /// <summary>
    ///  Componente Azul del color.
    /// </summary>
    public float ScB { get; set; }

    /// <summary>
    ///  Componente Verde del color.
    /// </summary>
    public float ScG { get; set; }

    /// <summary>
    ///  Componente Rojo del color.
    /// </summary>
    public float ScR { get; set; }

    /// <summary>
    /// Convierte implcitamente un <see cref="Types.Color"/> en un
    /// <see cref="ScColor"/>.
    /// </summary>
    /// <param name="color">
    /// <see cref="Types.Color"/> a convertir.
    /// </param>
    public static implicit operator ScColor(Types.Color color)
    {
        return new ScColor
        {
            ScA = color.ScA,
            ScB = color.ScB,
            ScG = color.ScG,
            ScR = color.ScR,
        };
    }

    /// <summary>
    /// Convierte implcitamente un <see cref="ScColor"/> en un
    /// <see cref="Types.Color"/>.
    /// </summary>
    /// <param name="color">
    /// <see cref="ScColor"/> a convertir.
    /// </param>
    public static implicit operator Types.Color(ScColor color)
    {
        return new Types.Color(color.ScR, color.ScG, color.ScB, color.ScA);
    }
}
