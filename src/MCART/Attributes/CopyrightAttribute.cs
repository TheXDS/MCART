/*
CopyrightAttribute.cs

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
using static System.AttributeTargets;

namespace TheXDS.MCART.Attributes
{
    /// <summary>
    /// Establece la información de Copyright del elemento.
    /// </summary>
    [AttributeUsage(Method | Class | Module | Assembly)]
    [Serializable]
    public sealed class CopyrightAttribute : TextAttribute
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="DescriptionAttribute" />.
        /// </summary>
        /// <param name="copyright">Valor del atributo.</param>
        public CopyrightAttribute(string copyright) : base(GetCopyrightString(copyright))
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="DescriptionAttribute" />.
        /// </summary>
        /// <param name="year">Año de registro del Copyright.</param>
        /// <param name="holder">Poseedor del Copyright.</param>
#if CLSCompliance
        [CLSCompliant(false)]
#endif
        public CopyrightAttribute(ushort year, string holder) : this($"{year:0000} {holder}")
        {
        }
        
        private static string GetCopyrightString(string input)
        {
            return input.ToLowerInvariant().StartsWith("copyright ", true, null) ? input
                : $"Copyright © {input}";
        }
    }
}