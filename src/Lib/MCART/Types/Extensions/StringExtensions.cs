/*
StringExtensions.cs

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

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Extensions for the <see cref="string" /> class.
/// </summary>
public static class StringExtensions
{
    private static (string match, Func<char, bool> test)[]? matchRules;

    /// <summary>
    /// Describes search options for the method
    /// <see cref="TokenSearch(string, string, SearchOptions)" />
    /// </summary>
    [Flags]
    public enum SearchOptions
    {
        /// <summary>
        /// Default search mode. Case will be ignored and the string will match the search terms if it contains at least one of the tokens.
        /// </summary>
        Default,

        /// <summary>
        /// Case-sensitive search mode. The string will match the search terms if it contains at least one of the tokens.
        /// </summary>
        CaseSensitive = 1,

        /// <summary>
        /// Strict search mode. Case will be ignored, and the string will match the search terms if it contains all specified tokens.
        /// </summary>
        IncludeAll = 2,

        /// <summary>
        /// Interpret tokens using the Like operator
        /// </summary>
        Wildcard = 4
    }

    /// <summary>
    /// Gets a capitalized string from the provided string.
    /// </summary>
    /// <param name="str">String to capitalize.</param>
    /// <param name="keepCasing">
    /// If set to <see langword="true"/>, maintains the casing of the rest of the characters in the string.
    /// </param>
    /// <returns>
    /// A new capitalized string from the provided string.
    /// </returns>
    public static string Capitalize(this string str, bool keepCasing = false)
    {
        return str[0].ToString().ToUpper() + (keepCasing ? str[1..] : str[1..].ToLower());
    }

    /// <summary>
    /// Removes an occurrence of a string from the ends of another.
    /// </summary>
    /// <param name="str">String to check.</param>
    /// <param name="toChop">Value to remove.</param>
    /// <returns>
    /// A string that does not start or end with
    /// <paramref name="toChop"/>.
    /// </returns>
    public static string Chop(this string str, string toChop)
    {
        return str.ChopStart(toChop).ChopEnd(toChop);
    }

    /// <summary>
    /// Removes an occurrence of any specified strings from the ends of another.
    /// </summary>
    /// <param name="str">String to check.</param>
    /// <param name="toChop">Values to remove.</param>
    /// <returns>
    /// A string that does not start or end with
    /// <paramref name="toChop"/>.
    /// </returns>
    public static string ChopAny(this string str, params string[] toChop)
    {
        foreach (string? j in toChop)
        {
            if (str.StartsWith(j)) return str[j.Length..];
            if (str.EndsWith(j)) return str[..^j.Length];
        }
        return str;
    }

    /// <summary>
    /// Removes an occurrence of a string from the end of another.
    /// </summary>
    /// <param name="str">String to check.</param>
    /// <param name="toChop">Value to remove.</param>
    /// <returns>
    /// A string that does not end with
    /// <paramref name="toChop"/>.
    /// </returns>
    public static string ChopEnd(this string str, string toChop)
    {
        return str.EndsWith(toChop) ? str[..^toChop.Length] : str;
    }

    /// <summary>
    /// Removes an occurrence of a string from the end of another.
    /// </summary>
    /// <param name="str">String to check.</param>
    /// <param name="toChop">Values to remove.</param>
    /// <returns>
    /// A string that does not end with <paramref name="toChop"/>.
    /// </returns>
    public static string ChopEndAny(this string str, params string[] toChop)
    {
        foreach (string? j in toChop)
        {
            if (str.EndsWith(j)) return str[..^j.Length];
        }
        return str;
    }

    /// <summary>
    /// Removes an occurrence of a string from the beginning of another.
    /// </summary>
    /// <param name="str">String to check.</param>
    /// <param name="toChop">Value to remove.</param>
    /// <returns>
    /// A string that does not start with <paramref name="toChop"/>.
    /// </returns>
    public static string ChopStart(this string str, string toChop)
    {
        return str.StartsWith(toChop) ? str[toChop.Length..] : str;
    }

    /// <summary>
    /// Removes an occurrence of any specified strings from the beginning of another.
    /// </summary>
    /// <param name="str">String to check.</param>
    /// <param name="toChop">Values to remove.</param>
    /// <returns>
    /// A string that does not start with <paramref name="toChop"/>.
    /// </returns>
    public static string ChopStartAny(this string str, params string[] toChop)
    {
        foreach (string? j in toChop)
        {
            if (str.StartsWith(j)) return str[j.Length..];
        }
        return str;
    }

    /// <summary>
    /// Truncates the length of a string to a maximum of
    /// <paramref name="length"/> characters.
    /// </summary>
    /// <param name="str">String to truncate.</param>
    /// <param name="length">Maximum length of the string.</param>
    /// <returns></returns>
    public static string Truncate(this string str, int length)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(length, 1);
        return
            length <= 3
            ? str.Length > length ? str[0..length] : str
            : str.Length > length
                ? $"{str[0..(length - 3)]}..."
                : str;
    }

    /// <summary>
    /// Wraps long text content into lines of up to 80 characters.
    /// </summary>
    /// <param name="str">String to wrap.</param>
    /// <returns>
    /// An array of strings with the original string's content wrapped into rows of up to 80 characters.
    /// </returns>
    public static string[] TextWrap(this string str) => str.TextWrap(80);

    /// <summary>
    /// Wraps long text content into lines.
    /// </summary>
    /// <param name="str">String to wrap.</param>
    /// <param name="width">
    /// Number of characters allowed per row. By default, it's 80 characters per column.
    /// </param>
    /// <returns>
    /// An array of strings with the original string's content wrapped into rows.
    /// </returns>
    public static string[] TextWrap(this string str, int width)
    {
        List<string> lines = [];
        foreach (string? word in str.Split(' '))
        {
            if ((lines.LastOrDefault() ?? string.Empty).Length + word.Length > width)
            {
                lines.AddRange(word.Split(width));
            }
            else
            {
                if (lines.Count == 0) lines.Add(string.Empty);
                lines[^1] += lines[^1].IsEmpty() && !word.IsEmpty() ? word : $" {word}";
            }
        }
        return [.. lines];
    }

    /// <summary>
    /// Determines if a string contains any of the specified characters.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the string contains any of the characters,
    /// <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="stringToCheck">String to check.</param>
    /// <param name="chars">Characters to search for.</param>
    [Sugar]
    public static bool ContainsAny(this string stringToCheck, params char[] chars)
    {
        return stringToCheck.ContainsAny(out _, chars);
    }

    /// <summary>
    /// Determines if a string contains any of the specified characters.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the string contains any of the characters,
    /// <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="stringToCheck">String to check.</param>
    /// <param name="argNum">
    /// Output parameter. If <paramref name="stringToCheck" /> contains any character specified in
    /// <paramref name="chars" />, the index of the contained argument will be returned;
    /// otherwise, -1 is returned.
    /// </param>
    /// <param name="chars">Characters to search for.</param>
    public static bool ContainsAny(this string stringToCheck, out int argNum, params char[] chars)
    {
        return stringToCheck.ContainsAny(chars, out argNum);
    }

    /// <summary>
    /// Determines if a string contains any of the specified characters.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the string contains any of the characters,
    /// <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="stringToCheck">String to check.</param>
    /// <param name="argNum">
    /// Output parameter. If <paramref name="stringToCheck" /> contains any character specified in
    /// <paramref name="chars" />, the index of the contained argument will be returned;
    /// otherwise, -1 is returned.
    /// </param>
    /// <param name="chars">Characters to search for.</param>
    public static bool ContainsAny(this string stringToCheck, IEnumerable<char> chars, out int argNum)
    {
        argNum = 0;
        foreach (char j in chars)
        {
            if (stringToCheck.Contains(j)) return true;
            argNum++;
        }
        argNum = -1;
        return false;
    }

    /// <summary>
    /// Determines if a string contains any of the specified strings.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the string contains any of the characters,
    /// <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="stringToCheck">String to check.</param>
    /// <param name="strings">Strings to search for.</param>
    [Sugar]
    public static bool ContainsAny(this string stringToCheck, params string[] strings)
    {
        return stringToCheck.ContainsAny(strings, out _);
    }

    /// <summary>
    /// Determines if a string contains any of the specified strings.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the string contains any of the characters,
    /// <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="stringToCheck">String to check.</param>
    /// <param name="strings">Strings to search for.</param>
    [Sugar]
    public static bool ContainsAny(this string stringToCheck, IEnumerable<string> strings)
    {
        return stringToCheck.ContainsAny(strings, out _);
    }

    /// <summary>
    /// Determines if a string contains any of the specified strings.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the string contains any of the characters,
    /// <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="stringToCheck">String to check.</param>
    /// <param name="argNum">
    /// Output parameter. If <paramref name="stringToCheck" /> contains any character specified in
    /// <paramref name="strings" />, the index of the contained argument will be returned;
    /// otherwise, -1 is returned.
    /// </param>
    /// <param name="strings">Strings to search for.</param>
    public static bool ContainsAny(this string stringToCheck, out int argNum, params string[] strings)
    {
        return stringToCheck.ContainsAny(strings, out argNum);
    }

    /// <summary>
    /// Determines if a string contains any of the specified strings.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the string contains any of the characters,
    /// <see langword="false" /> otherwise.
    /// </returns>
    /// <param name="stringToCheck">String to check.</param>
    /// <param name="argNum">
    /// Output parameter. If <paramref name="stringToCheck" /> contains any character specified in
    /// <paramref name="strings" />, the index of the contained argument will be returned;
    /// otherwise, -1 is returned.
    /// </param>
    /// <param name="strings">Strings to search for.</param>
    public static bool ContainsAny(this string stringToCheck, IEnumerable<string> strings, out int argNum)
    {
        argNum = 0;
        foreach (string? j in strings)
        {
            if (stringToCheck.Contains(j)) return true;
            argNum++;
        }
        argNum = -1;
        return false;
    }

    /// <summary>
    /// Checks if a string contains letters.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the string contains letters; otherwise,
    /// <see langword="false" />.
    /// </returns>
    /// <param name="stringToCheck">String to check.</param>
    public static bool ContainsLetters(this string stringToCheck)
    {
        return stringToCheck.ContainsAny((St.Constants.AlphaLc.ToUpperInvariant() + St.Constants.AlphaLc).ToCharArray());
    }

    /// <summary>
    /// Checks if a string contains letters.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the string contains letters; otherwise,
    /// <see langword="false" />.
    /// </returns>
    /// <param name="stringToCheck">String to check.</param>
    /// <param name="ucase">
    /// Optional. Specifies the type of check to perform. If set to
    /// <see langword="true" />, only uppercase characters are considered;
    /// if set to <see langword="false" />, only lowercase characters are considered.
    /// If omitted or set to <see langword="null" />, both cases are considered.
    /// </param>
    public static bool ContainsLetters(this string stringToCheck, bool ucase)
    {
        return stringToCheck.ContainsAny((ucase ? St.Constants.AlphaLc.ToUpperInvariant() : St.Constants.AlphaLc).ToCharArray());
    }

    /// <summary>
    /// Checks if a string contains numbers.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the string contains numbers; otherwise,
    /// <see langword="false" />.
    /// </returns>
    /// <param name="stringToCheck">String to check.</param>
    public static bool ContainsNumbers(this string stringToCheck)
    {
#if NativeNumbers
    return stringToCheck.ContainsAny(string
        .Join(null, Thread.CurrentThread.CurrentCulture.NumberFormat.NativeDigits).ToCharArray());
#else
        return stringToCheck.ContainsAny("0123456789".ToCharArray());
#endif
    }

    /// <summary>
    /// Checks if a name could be another specified name.
    /// </summary>
    /// <returns>
    /// A percentage value representing the probability that
    /// <paramref name="checkName" /> refers to the name
    /// <paramref name="actualName" />.
    /// </returns>
    /// <param name="checkName">Name to check.</param>
    /// <param name="actualName">Known actual name.</param>
    /// <exception cref="ArgumentNullException">
    /// Occurs when <paramref name="checkName" /> or
    /// <paramref name="actualName" /> are empty strings or <see langword="null" />.
    /// </exception>
    public static float CouldItBe(this string checkName, string actualName)
    {
        return checkName.CouldItBe(actualName, 0.75f);
    }

    /// <summary>
    /// Checks if a name could be another specified name.
    /// </summary>
    /// <returns>
    /// A value representing the probability that
    /// <paramref name="checkName" /> refers to the name
    /// <paramref name="actualName" />.
    /// </returns>
    /// <param name="checkName">Name to check.</param>
    /// <param name="actualName">Known actual name.</param>
    /// <param name="tolerance">
    /// Optional. A <see cref="float" /> between 0.0 and 1.0 that sets the
    /// minimum acceptable level of similarity. If not specified, it defaults to
    /// 75% (0.75).
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Occurs when <paramref name="tolerance" /> is not a value between
    /// <c>0.0f</c> and <c>1.0f</c>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Occurs when <paramref name="checkName" /> or
    /// <paramref name="actualName" /> are empty strings or <see langword="null" />.
    /// </exception>
    public static float CouldItBe(this string checkName, string actualName, float tolerance)
    {
        if (checkName.IsEmpty()) throw new ArgumentNullException(nameof(checkName));
        if (actualName.IsEmpty()) throw new ArgumentNullException(nameof(actualName));
        if (!tolerance.IsBetween(float.Epsilon, 1)) throw new ArgumentOutOfRangeException(nameof(tolerance));
        float n = 0f;
        int m = 0;
        foreach (string? j in checkName.Split(' '))
        {
            m++;
            n += actualName.Split(' ').Select(k => j.Likeness(k)).Where(l => l > tolerance).Sum();
        }
        return n / m;
    }

    /// <summary>
    /// Counts the characters that a string contains.
    /// </summary>
    /// <returns>
    /// An <see cref="int" /> with the total number of
    /// <paramref name="chars" /> that appear in <paramref name="stringToCheck" />.
    /// </returns>
    /// <param name="stringToCheck">String to check.</param>
    /// <param name="chars">Characters to count.</param>
    public static int CountChars(this string stringToCheck, params char[] chars)
    {
        return chars.Sum(j => stringToCheck.Count(a => a == j));
    }

    /// <summary>
    /// Counts the characters that a string contains.
    /// </summary>
    /// <returns>
    /// An <see cref="int" /> with the total number of
    /// characters of <paramref name="chars" /> that appear in
    /// <paramref name="stringToCheck" />.
    /// </returns>
    /// <param name="stringToCheck">String to check.</param>
    /// <param name="chars">Characters to count.</param>
    [Sugar]
    public static int CountChars(this string stringToCheck, string chars)
    {
        return stringToCheck.CountChars(chars.ToCharArray());
    }

    /// <summary>
    /// Determines if a string contains a binary value.
    /// </summary>
    /// <param name="str">String to check.</param>
    /// <returns>
    /// <see langword="true" /> if the string contains a value that can
    /// be interpreted as a binary number,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsBinary(this string str)
    {
        if (str.StartsWith("0b", true, CultureInfo.CurrentCulture)
            || str.StartsWith("&b", true, CultureInfo.CurrentCulture)) str = str[2..];
        return str.ToCharArray().All(j => "01".Contains(j));
    }

    /// <summary>
    /// Determines if a string is empty.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if the string is empty or null; otherwise, <see langword="false" />.
    /// </returns>
    /// <param name="stringToCheck">String to check.</param>
    [Sugar]
    public static bool IsEmpty([NotNullWhen(false)] this string? stringToCheck)
    {
        return string.IsNullOrWhiteSpace(stringToCheck);
    }

    /// <summary>
    /// Checks if the string has a basic alphanumeric format equal
    /// to the specified one.
    /// </summary>
    /// <param name="checkString"><see cref="string" /> to check.</param>
    /// <param name="format">
    /// Basic alphanumeric format to compare against.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the string's format is equal to
    /// the specified one, <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsFormattedAs(this string checkString, string format)
    {
        return checkString.IsFormattedAs(format, false);
    }

    /// <summary>
    /// Checks if the string has a basic alphanumeric format equal
    /// to the specified one.
    /// </summary>
    /// <param name="checkString"><see cref="string" /> to check.</param>
    /// <param name="format">
    /// Basic alphanumeric format to compare against.
    /// </param>
    /// <param name="checkCase">
    /// If set to <see langword="true" />, a case-sensitive evaluation
    /// of the string will be performed.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the string's format is equal to
    /// the specified one, <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsFormattedAs(this string checkString, string format, bool checkCase)
    {
        matchRules ??= [
            new("09", p => char.IsDigit(p)),
            new("Bb", p => "01".Contains(p)),
            new("Ff", p => byte.TryParse($"{p}", NumberStyles.HexNumber, null, out _)),
            new("AX", p => char.IsUpper(p)),
            new("ax", p => char.IsLower(p))
        ];
        string? chkS = checkCase ? checkString : checkString.ToUpperInvariant();
        string? fS = checkCase ? format : format.ToUpperInvariant();
        if (chkS.Length != fS.Length) return false;
        for (int j = 0; j < chkS.Length; j++)
        {
            char strChar = chkS[j];
            char fChar = fS[j];
            Func<char, bool>? rule = matchRules.SingleOrDefault(p => p.match.Contains(fChar)).test ?? (p => fChar == p);
            if (!rule(strChar)) return false;
        }
        return true;
    }

    /// <summary>
    /// Determines if a string contains a hexadecimal value.
    /// </summary>
    /// <param name="str">String to check.</param>
    /// <returns>
    /// <see langword="true" /> if the string contains a value that can
    /// be interpreted as a hexadecimal number,
    /// <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsHex(this string str)
    {
        if (str.StartsWith("0x") || str.StartsWith("&h", true, CultureInfo.CurrentCulture)) str = str[2..];
        return str.ToCharArray().All(j => @"0123456789abcdefABCDEF".Contains(j));
    }

    /// <summary>
    /// Gets a string that contains the specified number of characters
    /// from the left side of the string.
    /// </summary>
    /// <param name="string">
    /// Instance of <see cref="string" /> to process.
    /// </param>
    /// <param name="length">Length of characters to obtain.</param>
    /// <returns>
    /// A string that contains the specified number of characters from
    /// the left side of the string.
    /// </returns>
    public static string Left(this string @string, int length)
    {
        if (!length.IsBetween(0, @string.Length))
            throw new ArgumentOutOfRangeException(nameof(length));
        return @string[..length];
    }

    /// <summary>
    /// Calculates the percentage of similarity between two strings.
    /// </summary>
    /// <returns>The percentage of similarity between the two strings.</returns>
    /// <param name="ofString">String A to compare.</param>
    /// <param name="toString">String B to compare.</param>
    public static float Likeness(this string ofString, string toString)
    {
        return ofString.Likeness(toString, 3);
    }

    /// <summary>
    /// Calculates the percentage of similarity between two strings.
    /// </summary>
    /// <returns>The percentage of similarity between the two strings.</returns>
    /// <param name="ofString">String A to compare.</param>
    /// <param name="toString">String B to compare.</param>
    /// <param name="tolerance">
    /// Tolerance range for comparison. Represents the maximum allowed
    /// distance for each character that still makes the strings similar.
    /// </param>
    public static float Likeness(this string ofString, string toString, int tolerance)
    {
        int steps = 0, likes = 0;
        ofString = new string(' ', tolerance - 1) + ofString.ToUpper() + new string(' ', tolerance - 1);
        foreach (char c in toString.ToUpper())
            if (ofString.Substring(steps++, tolerance).Contains(c))
                likes++;
        return likes / (float)steps;
    }

    /// <summary>
    /// Ensures to return <see cref="string.Empty" /> if the string
    /// is empty or null.
    /// </summary>
    /// <param name="str">String to return.</param>
    /// <returns>
    /// The string, or <see cref="string.Empty" /> if the string is
    /// empty or null.
    /// </returns>
    public static string OrEmpty(this string? str)
    {
        return OrElse(str, string.Empty) ?? string.Empty;
    }

    /// <summary>
    /// Ensures to return <see cref="string.Empty" /> if the string
    /// is empty or null.
    /// </summary>
    /// <param name="str">String to return.</param>
    /// <param name="notEmptyFormat">
    /// Format to apply in case the string is not
    /// <see cref="string.Empty" />.
    /// </param>
    /// <returns>
    /// The string, or <see cref="string.Empty" /> if the string is
    /// empty or null.
    /// </returns>
    public static string OrEmpty(this string? str, string notEmptyFormat)
    {
        return !str.IsEmpty() ? string.Format(notEmptyFormat, str) : string.Empty;
    }

    /// <summary>
    /// Ensures to return <see langword="null" /> if the string
    /// is empty.
    /// </summary>
    /// <param name="str">String to return.</param>
    /// <returns>
    /// The string, or <see langword="null" /> if the string is
    /// empty.
    /// </returns>
    public static string? OrNull([NotNullIfNotNull(nameof(str))] this string? str)
    {
        return OrElse(str, null);
    }

    /// <summary>
    /// Ensures to return <see langword="null" /> if the string
    /// is empty.
    /// </summary>
    /// <param name="str">String to return.</param>
    /// <param name="notNullFormat">
    /// Format to apply in case the string is not
    /// <see langword="null" />.
    /// </param>
    /// <returns>
    /// The string, or <see langword="null" /> if the string is
    /// empty.
    /// </returns>
    public static string? OrNull(this string? str, string notNullFormat)
    {
        return !str.IsEmpty() ? string.Format(notNullFormat, str) : null;
    }

    /// <summary>
    /// Gets a string that contains the specified number of characters
    /// from the left side of the string.
    /// </summary>
    /// <param name="string">
    /// Instance of <see cref="string" /> to process.
    /// </param>
    /// <param name="length">Number of characters to get.</param>
    /// <returns>
    /// A string that contains the specified number of characters from
    /// the left side of the string.
    /// </returns>
    public static string Right(this string @string, int length)
    {
        if (!length.IsBetween(0, @string.Length))
            throw new ArgumentOutOfRangeException(nameof(length));
        return @string[length..];
    }

    /// <summary>
    /// Separates each character in a string with the specified
    /// <see cref="char" />.
    /// </summary>
    /// <param name="str">String to process.</param>
    /// <param name="separationChar">
    /// Character to use for separation.
    /// </param>
    /// <returns>
    /// A string whose characters have been separated by the
    /// specified <see cref="char" />.
    /// </returns>
    public static string Separate(this string str, char separationChar)
    {
        IEnumerable<char> InsertSpace()
        {
            System.Collections.IEnumerator? e = str.ToCharArray().GetEnumerator();
            e.Reset();
            if (!e.MoveNext()) yield break;
            while (true)
            {
                yield return (char)(e.Current ?? throw new TamperException());
                if (!e.MoveNext()) break;
                yield return separationChar;
            }
        }
        return new string([.. InsertSpace()]);
    }

    /// <summary>
    /// Separates each character in a string with a space.
    /// </summary>
    /// <param name="str">String to process.</param>
    /// <returns>
    /// A string whose characters have been separated by a space.
    /// </returns>
    public static string Spell(this string str)
    {
        return str.Separate(' ');
    }

    /// <summary>
    /// Splits a string based on the casing of its characters.
    /// </summary>
    /// <param name="name">String to split.</param>
    /// <returns>
    /// An enumeration of strings resulting from splitting by case.
    /// </returns>
    public static IEnumerable<string> SplitByCase(this string? name)
    {
        var ch = new List<char>();
        foreach (char c in name.NotNull())
        {
            if (char.IsUpper(c) && ch.Count != 0)
            {
                yield return new string([.. ch]);
                ch.Clear();
            }
            ch.Add(c);
        }
        if (ch.Count != 0) yield return new string([.. ch]);
    }

    /// <summary>
    /// Determines whether the string starts with any of the specified strings.
    /// </summary>
    /// <param name="str">String to check.</param>
    /// <param name="strings">
    /// Collection of initial strings to determine.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the string starts with any of
    /// the specified strings, otherwise <see langword="false" />.
    /// </returns>
    public static bool StartsWithAny(this string str, IEnumerable<string> strings)
    {
        return strings.Any(str.StartsWith);
    }

    /// <summary>
    /// Determines whether the string starts with any of the specified strings.
    /// </summary>
    /// <param name="str">String to check.</param>
    /// <param name="strings">
    /// Collection of initial strings to determine.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the string starts with any of
    /// the specified strings, otherwise <see langword="false" />.
    /// </returns>
    public static bool StartsWithAny(this string str, params string[] strings)
    {
        return strings.Any(str.StartsWith);
    }

    /// <summary>
    /// Determines whether the string starts with any of the specified strings,
    /// with case sensitivity control.
    /// </summary>
    /// <param name="str">String to check.</param>
    /// <param name="strings">
    /// Collection of initial strings to determine.
    /// </param>
    /// <param name="ignoreCase">
    /// If set to <see langword="true" />, the comparison is case-insensitive;
    /// if set to <see langword="false" />, the comparison is case-sensitive.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the string starts with any of
    /// the specified strings, otherwise <see langword="false" />.
    /// </returns>
    public static bool StartsWithAny(this string str, IEnumerable<string> strings, bool ignoreCase)
    {
        return strings.Any(p => str.StartsWith(p, ignoreCase, CultureInfo.CurrentCulture));
    }

    /// <summary>
    /// Determines whether the string starts with any of the specified strings,
    /// with case sensitivity and culture control.
    /// </summary>
    /// <param name="str">String to check.</param>
    /// <param name="strings">
    /// Collection of initial strings to determine.
    /// </param>
    /// <param name="ignoreCase">
    /// If set to <see langword="true" />, the comparison is case-insensitive;
    /// if set to <see langword="false" />, the comparison is case-sensitive.
    /// </param>
    /// <param name="culture">
    /// The culture to use for the comparison.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the string starts with any of
    /// the specified strings, otherwise <see langword="false" />.
    /// </returns>
    public static bool StartsWithAny(this string str, IEnumerable<string> strings, bool ignoreCase, CultureInfo culture)
    {
        return strings.Any(p => str.StartsWith(p, ignoreCase, culture));
    }

    /// <summary>
    /// Determines whether the string starts with any of the specified strings,
    /// using specified comparison options.
    /// </summary>
    /// <param name="str">String to check.</param>
    /// <param name="strings">
    /// Collection of initial strings to determine.
    /// </param>
    /// <param name="comparison">
    /// Specifies how the string comparison should be performed (case, culture).
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the string starts with any of
    /// the specified strings, otherwise <see langword="false" />.
    /// </returns>
    public static bool StartsWithAny(this string str, IEnumerable<string> strings, StringComparison comparison)
    {
        return strings.Any(p => str.StartsWith(p, comparison));
    }

    /// <summary>
    /// Performs a tokenized search on the specified string.
    /// </summary>
    /// <param name="str">
    /// The string in which to search.
    /// </param>
    /// <param name="searchTerms">
    /// Search terms.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the string matches any of the
    /// search terms, otherwise <see langword="false" />.
    /// </returns>
    public static bool TokenSearch(this string str, string searchTerms)
    {
        return str.TokenSearch(searchTerms, SearchOptions.Default);
    }

    /// <summary>
    /// Performs a tokenized search on the specified string.
    /// </summary>
    /// <param name="str">
    /// The string in which to search.
    /// </param>
    /// <param name="searchTerms">
    /// Search terms.
    /// </param>
    /// <param name="options">
    /// Search options.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the string matches any of the
    /// specified search terms and options, otherwise <see langword="false" />.
    /// </returns>
    public static bool TokenSearch(this string str, string searchTerms, SearchOptions options)
    {
        return str.TokenSearch(searchTerms, ' ', options);
    }

    /// <summary>
    /// Performs a tokenized search on the specified string.
    /// </summary>
    /// <param name="str">
    /// The string in which to search.
    /// </param>
    /// <param name="searchTerms">
    /// Search terms.
    /// </param>
    /// <param name="separator">
    /// Token separator.
    /// </param>
    /// <param name="options">
    /// Search options.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the string matches any of the
    /// specified search terms and options, otherwise <see langword="false" />.
    /// </returns>
    public static bool TokenSearch(this string str, string searchTerms, char separator, SearchOptions options)
    {
        (string? s,string? t)  = options.HasFlag(SearchOptions.CaseSensitive)
            ? (str, searchTerms)
            : (str.ToUpper(), searchTerms.ToUpper());
        string[]? terms = t.Split(separator);
        if (options.HasFlag(SearchOptions.Wildcard))
            return options.HasFlag(SearchOptions.IncludeAll)
                ? terms.All(j => Regex.IsMatch(s, WildcardToRegEx(j)))
                : terms.Any(j => Regex.IsMatch(s, WildcardToRegEx(j)));
        return options.HasFlag(SearchOptions.IncludeAll)
            ? terms.All(s.Contains)
            : terms.Any(s.Contains);
    }

    /// <summary>
    /// Converts a <see cref="string" /> to a <see cref="SecureString" />.
    /// </summary>
    /// <param name="string"><see cref="string" /> to convert.</param>
    /// <returns>
    /// A <see cref="SecureString" /> containing all the original characters
    /// of the provided string.
    /// </returns>
    public static SecureString ToSecureString(this string @string)
    {
        SecureString? retVal = new();
        foreach (char j in @string) retVal.AppendChar(j);
        return retVal;
    }

    /// <summary>
    /// Allows obtaining the content of a <see cref="string" /> as a
    /// <see cref="Stream" /> using the specified encoding.
    /// </summary>
    /// <param name="string">String to convert.</param>
    /// <param name="encoding">String encoding.</param>
    /// <returns>
    /// A <see cref="Stream" /> containing the content of the string.
    /// </returns>
    public static Stream ToStream(this string @string, Encoding encoding)
    {
        return new MemoryStream(encoding.GetBytes(@string));
    }

    /// <summary>
    /// Allows obtaining the content of a <see cref="string" /> as a
    /// <see cref="Stream" /> using UTF8 encoding.
    /// </summary>
    /// <param name="string">String to convert.</param>
    /// <returns>
    /// A <see cref="Stream" /> containing the content of the string.
    /// </returns>
    /// <remarks>
    /// Although strings in .NET Framework are UTF16, certain common functions
    /// prefer working with UTF8 encoded strings, so this method uses that encoding for conversion.
    /// </remarks>
    public static Stream ToStream(this string @string)
    {
        return @string.ToStream(Encoding.UTF8);
    }

    /// <summary>
    /// Converts this string into its base64 representation,
    /// specifying the <see cref="Encoding"/> to use for the conversion.
    /// </summary>
    /// <param name="string">String to convert.</param>
    /// <param name="encoding">String encoding.</param>
    /// <returns>
    /// A base64 formatted string containing the content of the original string.
    /// </returns>
    public static string ToBase64(this string @string, Encoding encoding)
    {
        return Convert.ToBase64String(encoding.GetBytes(@string));
    }

    /// <summary>
    /// Converts this string into its base64 representation
    /// using <see cref="Encoding.UTF8"/>.
    /// </summary>
    /// <param name="string">String to convert.</param>
    /// <returns>
    /// A base64 formatted string containing the content of the original string.
    /// </returns>
    public static string ToBase64(this string @string) => @string.ToBase64(Encoding.UTF8);

    /// <summary>
    /// Returns a new string without the specified characters.
    /// </summary>
    /// <param name="string">
    /// String to process.
    /// </param>
    /// <param name="chars">
    /// Characters to remove.
    /// </param>
    /// <returns>
    /// A string that does not contain any of the specified characters.
    /// </returns>
    public static string Without(this string @string, params char[] chars)
    {
        return @string.Without(chars.Select(p => p.ToString()).ToArray());
    }

    /// <summary>
    /// Returns a new string without the specified substrings.
    /// </summary>
    /// <param name="string">
    /// String to process.
    /// </param>
    /// <param name="strings">
    /// Substrings to remove.
    /// </param>
    /// <returns>
    /// A string that does not contain any of the specified substrings.
    /// </returns>
    public static string Without(this string @string, params string[] strings)
    {
        foreach (string? j in strings)
        {
            @string = @string.Replace(j, string.Empty);
        }
        return @string;
    }

    /// <summary>
    /// Converts a wildcard pattern to a regular expression pattern.
    /// </summary>
    /// <param name="value">Wildcard pattern.</param>
    /// <returns>Regular expression pattern.</returns>
    public static string WildcardToRegEx(string value)
    {
        return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
    }

    /// <summary>
    /// Returns the first non-empty string from the provided strings.
    /// </summary>
    /// <param name="source">Source string.</param>
    /// <param name="emptyRetVal">Value to return if all source strings are empty.</param>
    /// <returns>Non-empty string or default value.</returns>
    public static string? OrElse(string? source, string? emptyRetVal)
    {
        return !source.IsEmpty() ? source : emptyRetVal;
    }

    /// <summary>
    /// Splits this string into chunks of the specified size.
    /// </summary>
    /// <param name="str">String to split.</param>
    /// <param name="chunkSize">Chunk size.</param>
    /// <returns>Enumerable collection of string chunks.</returns>
    public static IEnumerable<string> Split(this string str, int chunkSize)
    {
        string Selector(int i)
        {
            int ch = i * chunkSize;
            return str.Substring(ch,
                ch + chunkSize > str.Length ? str.Length - ch : chunkSize);
        }
        return Enumerable.Range(0, (str.Length / chunkSize) + 1).Select(Selector);
    }
}
