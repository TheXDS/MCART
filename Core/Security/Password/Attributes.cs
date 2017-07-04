//
//  Attributes.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using MCART.Attributes;
using St = MCART.Resources.Strings;
namespace MCART.Security.Password
{
    /// <summary>
    /// Describe un nivel de ponderación.
    /// </summary>
    [AttributeUsage((AttributeTargets)71)]
    public sealed class PonderationAttribute : IntAttributeBase
    {
        /// <summary>
        /// Crea un nuevo atributo de <see cref="PonderationAttribute"/>.
        /// </summary>
        /// <param name="ponderation">
        /// Nivel de ponderación a aplicar al elemento.
        /// </param>
        public PonderationAttribute(PonderationLevel ponderation) : base((int)ponderation)
        {
            if (!typeof(PonderationLevel).IsEnumDefined(ponderation)) throw new
                ArgumentOutOfRangeException(
                    nameof(ponderation),
                    St.XCannotBeY(nameof(ponderation), ponderation.ToString()));
        }
    }
    /// <summary>
    /// Indica si un elemento se acitva o no de forma predeterminada.
    /// </summary>
    [AttributeUsage((AttributeTargets)71)]
    public sealed class DefaultEnableAttribute : BoolAttributeBase
    {
        /// <summary>
        /// Crea un nuevo atributo de <see cref="DefaultEnableAttribute"/>.
        /// </summary>
        /// <param name="attrValue">Valor predeterminado de activación.</param>
        public DefaultEnableAttribute(bool attrValue) : base(attrValue) { }
    }
    /// <summary>
    /// Indica que un elemento representará un puntaje extra en la evaluación.
    /// </summary>
    [AttributeUsage((AttributeTargets)71)]
    public sealed class ExtraPointsAttribute : Attribute { }
}