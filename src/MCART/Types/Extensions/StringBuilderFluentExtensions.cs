/*
StringExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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

using System.Text;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Extensiones de la clase <see cref="StringBuilder"/>.
    /// </summary>
    public static class StringBuilderFluentExtensions
    {
        /// <summary>
        /// Concatena el texto especificado seguido del terminador de línea
        /// predeterminado al final de esta instancia solamente si la
        /// cadena no es <see langword="null"/>.
        /// </summary>
        /// <param name="sb">
        /// Instancia de <see cref="StringBuilder"/> sobre la cual realizar
        /// la operación.
        /// </param>
        /// <param name="text">
        /// Texto a concatenar. Si es <see langword="null"/>, no se
        /// realizará ninguna acción.
        /// </param>
        /// <returns>
        /// La misma instancia que <paramref name="sb"/>.
        /// </returns>
        public static StringBuilder AppendLineIfNotNull(this StringBuilder sb, string? text)
        {
            if (text is { }) sb.AppendLine(text);
            return sb;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static StringBuilder AppendAndWrap(this StringBuilder sb, string? text, int width)
        {
            foreach (var j in text?.TextWrap(width)?.NotNull()!)            
                sb.AppendLine(j);            

            return sb;
        }
    }
}        