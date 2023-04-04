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
    private record class ExDumpSection(ExDumpOptions Flag, Func<Exception, IEnumerable<string>> StringCallback)
    {
        public ExDumpSection(ExDumpOptions flag, Func<Exception, string> stringCallback) : this(flag, ex => new[] { stringCallback(ex) })
        {
        }

        public ExDumpSection(ExDumpOptions flag, Func<IEnumerable<string>> stringCallback) : this(flag, _ => stringCallback())
        {
        }
    }

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
    /// <param name="fixedTextWidth">
    /// Cuando el parámetro <paramref name="options"/> incluye la bandera
    /// <see cref="ExDumpOptions.TextWidthFormatted"/>, permite especificar el
    /// ancho a utilizar para aplicar formato al volcado de la excepción.
    /// </param>
    /// <returns>
    /// Una cadena con toda la información de la excepción.
    /// </returns>
    public static string ExDump(Exception ex, ExDumpOptions options, int fixedTextWidth = 80)
    {
        using MemoryStream ms = new();
        using (StreamWriter sw = new(ms, Encoding.UTF8, -1, true)) ExDump(sw, ex, options, fixedTextWidth);
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
    /// <param name="fixedTextWidth">
    /// Cuando el parámetro <paramref name="options"/> incluye la bandera
    /// <see cref="ExDumpOptions.TextWidthFormatted"/>, permite especificar el
    /// ancho a utilizar para aplicar formato al volcado de la excepción.
    /// </param>
    public static void ExDump(TextWriter writer, Exception ex, ExDumpOptions options, int fixedTextWidth = 80)
    {
        foreach (var j in ExDumpLines(ex, options, fixedTextWidth, 0, false))
        {
            writer.Write(j);
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

    private static IEnumerable<string> ExDumpLines(Exception ex, ExDumpOptions options, int fixedTextWidth, int padding, bool isInner)
    {
        #region Helpers

        string GetSeparator(char separator = '-') => $"{(options.HasFlag(ExDumpOptions.TextWidthFormatted) ? new string(separator, fixedTextWidth - padding) : null)}";
        string GetAsmInfo(Assembly asm) => $"{asm.FullName} {asm.GetAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? asm.GetName().Version?.ToString() ?? "1.0.0"}\n";

        #endregion

        #region Dumpers

        ExDumpSection Separator(ExDumpOptions flag = 0, char separator = '-') => new(flag, _ => GetSeparator(separator));
        ExDumpSection NewLine(ExDumpOptions flag = 0) => new(flag, _ => $"\n{new string(' ', padding)}");
        ExDumpSection Padding(ExDumpOptions flag = 0) => new(flag, _ => new string(' ', padding));
        ExDumpSection Constant(string text, ExDumpOptions flag = 0) => new(flag, _ => text);
        string DumpName(Exception e) => e.GetType().NameOf();
        string DumpSource(Exception e) => string.Format(Errors.InX, e.Source ?? "Unknown");
        string DumpHResult(Exception e) => $" (0x{e.HResult.ToString("X").PadLeft(8, '0')})";
        string DumpMessage(Exception e) => e.Message;
        string DumpStackTrace(Exception e) => e.StackTrace ?? Errors.NoStackInfo;
        IEnumerable<string> DumpLoadedAssemblies() => AppDomain.CurrentDomain.GetAssemblies().Select(GetAsmInfo);
        IEnumerable<string> DumpInner(Exception e)
        {
            foreach (PropertyInfo j in ex.GetType().GetProperties())
            {
                switch (j.GetValue(ex))
                {
                    case IEnumerable<Exception?> exceptions:
                        return exceptions.NotNull().SelectMany(DumpSingleInner);
                    case Exception inner:
                        return DumpSingleInner(inner);
                }
            }
            return Array.Empty<string>();
        }
        IEnumerable<string> DumpSingleInner(Exception e) => (!options.HasFlag(ExDumpOptions.TextWidthFormatted) 
            ? new[] { Environment.NewLine } 
            : Array.Empty<string>()).Concat(ExDumpLines(e, options & ~ExDumpOptions.LoadedAssemblies, fixedTextWidth, padding + 2, true));

        #endregion

        IEnumerable<ExDumpSection?> exDumpOptionRegistry = new ExDumpSection?[]
        {
            isInner ? NewLine(ExDumpOptions.TextWidthFormatted) : null,
            options.HasFlag(ExDumpOptions.TextWidthFormatted) ? null : Padding(),
            new(ExDumpOptions.Name, DumpName),
            new(ExDumpOptions.HResult, DumpHResult),
            new(ExDumpOptions.Source, DumpSource),
            NewLine(),
            Separator(ExDumpOptions.Message | ExDumpOptions.TextWidthFormatted, '='),
            NewLine(ExDumpOptions.TextWidthFormatted),
            new(ExDumpOptions.Message, DumpMessage),
            NewLine(),
            Separator(ExDumpOptions.StackTrace),
            NewLine(ExDumpOptions.TextWidthFormatted),
            new(ExDumpOptions.StackTrace, DumpStackTrace),
            NewLine(),
            new(ExDumpOptions.Inner, DumpInner),
            isInner ? null : NewLine(),
            Constant(Errors.LoadedAssemblies, ExDumpOptions.LoadedAssemblies),
            NewLine(ExDumpOptions.LoadedAssemblies),
            Separator(ExDumpOptions.LoadedAssemblies | ExDumpOptions.TextWidthFormatted),
            NewLine(ExDumpOptions.LoadedAssemblies | ExDumpOptions.TextWidthFormatted),
            new(ExDumpOptions.LoadedAssemblies, DumpLoadedAssemblies),
        };

        Func<string, string> textTransform = options.HasFlag(ExDumpOptions.TextWidthFormatted)
            ? str => string.Join('\n', str.TextWrap(fixedTextWidth))
            : str => str;

        return exDumpOptionRegistry.NotNull().Where(p => options.HasFlag(p.Flag)).SelectMany(p => p.StringCallback(ex)).Select(textTransform);        
    }
}
