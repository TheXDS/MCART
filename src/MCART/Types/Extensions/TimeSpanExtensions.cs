/*
TimeSpanExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene numerosas extensiones para el tipo System.DateTime del
CLR, supliéndolo de nueva funcionalidad previamente no existente, o de
invocación compleja.

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

using System;
using System.Collections.Generic;
using System.Globalization;
using TheXDS.MCART.Resources;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Contiene extensiones útiles de la estructura <see cref="TimeSpan"/>.
    /// </summary>
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Humaniza un <see cref="TimeSpan"/> a un valor amigable.
        /// </summary>
        /// <param name="timeSpan">Valor a humanizar.</param>
        /// <returns>
        /// Una cadena amigable que indica los componentes de días, horas,
        /// minutos y segundos de <paramref name="timeSpan"/>.
        /// </returns>
        public static string Verbose(this in TimeSpan timeSpan)
        {
            var msjs = new HashSet<string>();
            if (timeSpan.Days > 0)
                msjs.Add(Strings.Days(timeSpan.Days));
            if (timeSpan.Hours > 0)
                msjs.Add(Strings.Hours(timeSpan.Hours));
            if (timeSpan.Minutes > 0)
                msjs.Add(Strings.Minutes(timeSpan.Minutes));
            if (timeSpan.Seconds > 0)
                msjs.Add(Strings.Seconds(timeSpan.Seconds));
            return string.Join(", ", msjs);
        }

        /// <summary>
        /// Convierte un <see cref="TimeSpan"/> en una representación de hora.
        /// </summary>
        /// <param name="timeSpan">Valor a convertir.</param>
        /// <returns>Una cadena que representa una hora.</returns>
        public static string AsTime(this in TimeSpan timeSpan)
        {
            return AsTime(timeSpan, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Convierte un <see cref="TimeSpan"/> en una representación de hora.
        /// </summary>
        /// <param name="timeSpan">Valor a convertir.</param>
        /// <param name="culture">Cultura a utilizar para el formato de hora.</param>
        /// <returns>Una cadena que representa una hora.</returns>
        public static string AsTime(this in TimeSpan timeSpan, in CultureInfo culture)
        {
            var f = $"{{0:{culture.DateTimeFormat.ShortTimePattern}}}";
            return string.Format(f, DateTime.MinValue.Add(timeSpan));
        }
    }
}