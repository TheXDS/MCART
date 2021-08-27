/*
Strings.cs

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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TheXDS.MCART.Types.Extensions;
using System.Reflection;

namespace TheXDS.MCART.Resources.Strings
{
    /// <summary>
    /// Contiene cadenas de texto genéricas, además de funciones de
    /// composición de texto.
    /// </summary>
    public static class Composition
    {
        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="days"/>} días".
        /// </summary>
        /// <param name="days">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="days"/>} días".
        /// </returns>
        public static string Days(int days) => string.Format(Common.XDays, days);

        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="hours"/>} horas".
        /// </summary>
        /// <param name="hours">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="hours"/>} horas".
        /// </returns>
        public static string Hours(int hours) => string.Format(Common.XHours, hours);

        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="minutes"/>} minutos".
        /// </summary>
        /// <param name="minutes">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="minutes"/>} minutos".
        /// </returns>
        public static string Minutes(int minutes) => string.Format(Common.XMinutes, minutes);

        /// <summary>
        /// Devuelve una cadena con el texto "{<paramref name="seconds"/>} segundos".
        /// </summary>
        /// <param name="seconds">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "{<paramref name="seconds"/>} segundos".
        /// </returns>
        public static string Seconds(int seconds) => string.Format(Common.XSeconds, seconds);

        /// <summary>
        /// Vuelca toda la información pertinente de una excepción como un
        /// <see cref="string"/>.
        /// </summary>
        /// <param name="ex">
        /// Excepcíón de la cual obtener la información.
        /// </param>
        /// <param name="options">
        /// Opciones de presentación de la información.
        /// </param>
        /// <returns>
        /// Una cadena con toda la información de la excepción.
        /// </returns>
        public static string ExDump(Exception ex, ExDumpOptions options)
        {
            using MemoryStream ms = new();
            using (StreamWriter sw = new(ms, Encoding.UTF8, -1, true)) ExDump(sw, ex, options);
            ms.Seek(0, SeekOrigin.Begin);
            using StreamReader sr = new(ms, Encoding.UTF8);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// Vuelca toda la información pertinente de una excepción por medio de
        /// un <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="writer">
        /// Objeto en el cual escribir la información de la excepción.
        /// </param>
        /// <param name="ex">
        /// Excepcíón de la cual obtener la información.
        /// </param>
        /// <param name="options">
        /// Opciones de presentación de la información.
        /// </param>
        public static void ExDump(TextWriter writer, Exception ex, ExDumpOptions options)
        {
            const int textWidth = 80;
            bool WriteWith(Action<string> action, ExDumpOptions flag, string text)
            {
                if (options.HasFlag(flag))
                {
                    action(options.HasFlag(ExDumpOptions.TextWidthFormatted) ? string.Join('\n', text.TextWrap(textWidth).Select(p => p.TrimEnd(' '))) : text);
                    return true;
                }
                return false;
            }
            bool Write(ExDumpOptions flag, string text) => WriteWith(writer.Write, flag, text);
            bool WriteLn(ExDumpOptions flag, string text) => WriteWith(writer.WriteLine, flag, text);
            void Separator(char separator = '-')
            {
                if (!WriteLn(ExDumpOptions.TextWidthFormatted, new string(separator, textWidth))) writer.WriteLine();
            }

            Write(ExDumpOptions.Name, ex.GetType().NameOf());
            Write(ExDumpOptions.Source, string.Format(Errors.InX, ex.Source));
            Write(ExDumpOptions.HResult, $" (0x{ex.HResult.ToString("X").PadLeft(8, '0')})");
            Separator();
            if (Write(ExDumpOptions.Message, ex.Message)) Separator();
            if (Write(ExDumpOptions.Message, ex.StackTrace ?? Errors.NoStackInfo)) Separator();
            if (options.HasFlag(ExDumpOptions.Inner))
            {
                foreach (PropertyInfo j in ex.GetType().GetProperties())
                {
                    switch (j.GetValue(ex))
                    {
                        case IEnumerable<Exception?> exceptions:
                            foreach (Exception k in exceptions.NotNull())
                            {
                                Separator('=');
                                ExDump(writer, k, options & ~ExDumpOptions.LoadedAssemblies);
                            }
                            break;
                        case Exception inner:
                            Separator('=');
                            ExDump(writer, inner, options & ~ExDumpOptions.LoadedAssemblies);
                            break;
                    }
                }
            }

            if (options.HasFlag(ExDumpOptions.LoadedAssemblies))
            {
                Separator('=');
                foreach (Assembly k in AppDomain.CurrentDomain.GetAssemblies())
                {
                    try
                    {
                        writer.WriteLine();
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Devuelve una cadena con el texto "⚠ {<paramref name="text"/>}".
        /// </summary>
        /// <param name="text">Texto a formatear.</param>
        /// <returns>
        /// Una cadena con el texto "⚠ {<paramref name="text"/>}".
        /// </returns>
        public static string Warn(string text) => $"⚠ {text}";
    }
}