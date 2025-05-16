/*
Strings.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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
/// Contains generic string literals, as well as text
/// composition functions.
/// </summary>
public static class Composition
{
    private record class ExDumpSection(ExDumpOptions Flag, Func<Exception, IEnumerable<string>> StringCallback)
    {
        public ExDumpSection(ExDumpOptions flag, Func<Exception, string> stringCallback) : this(flag, ex => [stringCallback(ex)])
        {
        }

        public ExDumpSection(ExDumpOptions flag, Func<IEnumerable<string>> stringCallback) : this(flag, _ => stringCallback())
        {
        }
    }

    /// <summary>
    /// Returns a string with the text "{<paramref name="days"/>} days".
    /// </summary>
    /// <param name="days">Text to format.</param>
    /// <returns>
    /// A string with the text "{<paramref name="days"/>} days".
    /// </returns>
    public static string Days(int days) => string.Format(Common.XDays, days);

    /// <summary>
    /// Returns a string with the text "{<paramref name="hours"/>} hours".
    /// </summary>
    /// <param name="hours">Text to format.</param>
    /// <returns>
    /// A string with the text "{<paramref name="hours"/>} hours".
    /// </returns>
    public static string Hours(int hours) => string.Format(Common.XHours, hours);

    /// <summary>
    /// Returns a string with the text "{<paramref name="minutes"/>} minutes".
    /// </summary>
    /// <param name="minutes">Text to format.</param>
    /// <returns>
    /// A string with the text "{<paramref name="minutes"/>} minutes".
    /// </returns>
    public static string Minutes(int minutes) => string.Format(Common.XMinutes, minutes);

    /// <summary>
    /// Returns a string with the text "{<paramref name="seconds"/>} seconds".
    /// </summary>
    /// <param name="seconds">Text to format.</param>
    /// <returns>
    /// A string with the text "{<paramref name="seconds"/>} seconds".
    /// </returns>
    public static string Seconds(int seconds) => string.Format(Common.XSeconds, seconds);

    /// <summary>
    /// Dumps all relevant information from an exception as a
    /// <see cref="string"/>.
    /// </summary>
    /// <param name="ex">
    /// Exception from which to obtain information.
    /// </param>
    /// <param name="options">
    /// Presentation options for the information.
    /// </param>
    /// <param name="fixedTextWidth">
    /// When the <paramref name="options"/> parameter includes the
    /// <see cref="ExDumpOptions.TextWidthFormatted"/> flag, it allows specifying the
    /// width to use to format the exception dump.
    /// </param>
    /// <returns>
    /// A string with all the exception information.
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
    /// Dumps all relevant information from an exception via a
    /// <see cref="TextWriter"/>.
    /// </summary>
    /// <param name="writer">
    /// Object on which to write the exception information.
    /// </param>
    /// <param name="ex">
    /// Exception from which to obtain information.
    /// </param>
    /// <param name="options">
    /// Presentation options for the information.
    /// </param>
    /// <param name="fixedTextWidth">
    /// When the <paramref name="options"/> parameter includes the
    /// <see cref="ExDumpOptions.TextWidthFormatted"/> flag, it allows specifying the
    /// width to use to format the exception dump.
    /// </param>
    public static void ExDump(TextWriter writer, Exception ex, ExDumpOptions options, int fixedTextWidth = 80)
    {
        foreach (var j in ExDumpLines(ex, options, fixedTextWidth, 0, false))
        {
            writer.Write(j);
        }
    }

    /// <summary>
    /// Returns a string with the text "⚠ {<paramref name="text"/>}".
    /// </summary>
    /// <param name="text">Text to format.</param>
    /// <returns>
    /// A string with the text "⚠ {<paramref name="text"/>}".
    /// </returns>
    public static string Warn(string text) => $"⚠ {text}";

    private static IEnumerable<string> ExDumpLines(Exception ex, ExDumpOptions options, int fixedTextWidth, int padding, bool isInner)
    {
        IEnumerable<ExDumpSection?> exDumpOptionRegistry =
        [
            isInner ? NewLine(padding, ExDumpOptions.TextWidthFormatted) : null,
            options.HasFlag(ExDumpOptions.TextWidthFormatted) ? null : Padding(padding),
            new(ExDumpOptions.Name, DumpName),
            new(ExDumpOptions.HResult, DumpHResult),
            new(ExDumpOptions.Source, DumpSource),
            NewLine(padding),
            Separator(options, padding, fixedTextWidth, ExDumpOptions.Message | ExDumpOptions.TextWidthFormatted, '='),
            NewLine(padding, ExDumpOptions.TextWidthFormatted),
            new(ExDumpOptions.Message, DumpMessage),
            NewLine(padding),
            Separator(options, padding, fixedTextWidth, ExDumpOptions.StackTrace),
            NewLine(padding, ExDumpOptions.TextWidthFormatted),
            new(ExDumpOptions.StackTrace, DumpStackTrace),
            NewLine(padding),
            new(ExDumpOptions.Inner, x => DumpInner(options, padding, fixedTextWidth, x)),
            isInner ? null : NewLine(padding),
            Constant(Errors.LoadedAssemblies, ExDumpOptions.LoadedAssemblies),
            NewLine(padding, ExDumpOptions.LoadedAssemblies),
            Separator(options, padding, fixedTextWidth, ExDumpOptions.LoadedAssemblies | ExDumpOptions.TextWidthFormatted),
            NewLine(padding, ExDumpOptions.LoadedAssemblies | ExDumpOptions.TextWidthFormatted),
            new(ExDumpOptions.LoadedAssemblies, DumpLoadedAssemblies),
        ];

        Func<string, string> textTransform = options.HasFlag(ExDumpOptions.TextWidthFormatted)
            ? str => string.Join('\n', str.TextWrap(fixedTextWidth))
            : str => str;

        return exDumpOptionRegistry.NotNull().Where(p => options.HasFlag(p.Flag)).SelectMany(p => p.StringCallback(ex)).Select(textTransform);        
    }

    private static string GetSeparator(ExDumpOptions options, int padding, int fixedTextWidth, char separator = '-') => $"{(options.HasFlag(ExDumpOptions.TextWidthFormatted) ? new string(separator, fixedTextWidth - padding) : null)}";
    
    private static string GetAsmInfo(Assembly asm) => $"{asm.FullName} {asm.GetAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? asm.GetName().Version?.ToString() ?? "1.0.0"}\n";
    
    private static ExDumpSection Separator(ExDumpOptions options, int padding, int fixedTextWidth, ExDumpOptions flag = 0, char separator = '-') => new(flag, _ => GetSeparator(options, padding, fixedTextWidth, separator));
    
    private static ExDumpSection NewLine(int padding, ExDumpOptions flag = 0) => new(flag, _ => $"\n{new string(' ', padding)}");
    
    private static ExDumpSection Padding(int padding, ExDumpOptions flag = 0) => new(flag, _ => new string(' ', padding));
    
    private static ExDumpSection Constant(string text, ExDumpOptions flag = 0) => new(flag, _ => text);
    
    private static string DumpName(Exception e) => e.GetType().NameOf();
    
    private static string DumpSource(Exception e) => string.Format(Errors.InX, e.Source ?? "Unknown");
    
    private static string DumpHResult(Exception e) => $" (0x{e.HResult.ToString("X").PadLeft(8, '0')})";
    
    private static string DumpMessage(Exception e) => e.Message;
    
    private static string DumpStackTrace(Exception e) => e.StackTrace ?? Errors.NoStackInfo;
    
    private static IEnumerable<string> DumpLoadedAssemblies() => AppDomain.CurrentDomain.GetAssemblies().Select(GetAsmInfo);

    private static IEnumerable<string> DumpInner(ExDumpOptions options, int padding, int fixedTextWidth, Exception e)
    {
        IEnumerable<string> DumpCollection(IEnumerable<Exception> exs) => exs.NotNull().SelectMany(ex => DumpSingleInner(options, padding, fixedTextWidth, ex));
        return e switch
        {
            AggregateException { InnerExceptions: { } innerExs } when innerExs.Count > 0 => DumpCollection(innerExs),
            ReflectionTypeLoadException { LoaderExceptions: { } exs } when exs.Length > 0 => DumpCollection(exs.NotNull()),
            { InnerException: { } inner } => DumpSingleInner(options, padding, fixedTextWidth, inner),
            _ => []
        };
    }
    
    private static IEnumerable<string> DumpSingleInner(ExDumpOptions options, int padding, int fixedTextWidth, Exception e) => (!options.HasFlag(ExDumpOptions.TextWidthFormatted)
        ? [Environment.NewLine]
        : Array.Empty<string>()).Concat(ExDumpLines(e, options & ~ExDumpOptions.LoadedAssemblies, fixedTextWidth, padding + 2, true));
}
