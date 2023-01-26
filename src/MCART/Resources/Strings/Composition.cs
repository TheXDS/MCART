/*
Strings.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Resources.Strings;

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
    /// Excepción de la cual obtener la información.
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
    /// Excepción de la cual obtener la información.
    /// </param>
    /// <param name="options">
    /// Opciones de presentación de la información.
    /// </param>
    public static void ExDump(TextWriter writer, Exception ex, ExDumpOptions options)
    {
        const int textWidth = 80;
        bool WriteWith(Action<string> action, ExDumpOptions flag, string text)
        {
            if (!options.HasFlag(flag)) return false;
            action(options.HasFlag(ExDumpOptions.TextWidthFormatted) ? string.Join('\n', text.TextWrap(textWidth).Select(p => p.TrimEnd(' '))) : text);
            return true;
        }
        bool Write(ExDumpOptions flag, string text) => WriteWith(writer.Write, flag, text);
        bool WriteLn(ExDumpOptions flag, string text) => WriteWith(writer.WriteLine, flag, text);
        void Separator(char separator = '-')
        {
            if (!WriteLn(ExDumpOptions.TextWidthFormatted, new(separator, textWidth))) writer.WriteLine();
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

        if (!options.HasFlag(ExDumpOptions.LoadedAssemblies)) return;
        Separator('=');
        foreach (Assembly k in AppDomain.CurrentDomain.GetAssemblies())
        {
            writer.WriteLine($"{k.FullName} {k.GetAttr<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? k.GetName().Version?.ToString() ?? "0.0.0"}");
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
