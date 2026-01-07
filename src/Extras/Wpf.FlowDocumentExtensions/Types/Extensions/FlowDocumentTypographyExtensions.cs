/*
FlowDocumentTypographyExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using System.Windows;
using System.Windows.Documents;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Fluent type extensions for manipulating objects
/// <see cref="FlowDocument" /> focused on typographic settings.
/// </summary>
public static class FlowDocumentTypographyExtensions
{
    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">Index of the alternative annotation format.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyAnnotationAlternates<TElement>(this TElement element, int value)
        where TElement : TextElement
    {
        element.Typography.AnnotationAlternates = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// if set to <see langword="true" />, the spacing between uppercase glyphs will be globally adjusted to
    /// improve readability.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyCapitalSpacing<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.CapitalSpacing = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// If set to <see langword="true" />, the vertical position of the glyphs will be adjusted for better alignment
    /// with uppercase glyphs.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyCaseSensitiveForms<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.CaseSensitiveForms = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// If set to <see langword="true" />, custom glyphs will be used based on the context of the text being
    /// processed.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyContextualAlternates<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.ContextualAlternates = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// If set to <see langword="true" />, contextual ligatures will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyContextualLigatures<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.ContextualLigatures = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">Specifies the index of a contextual swash glyph form.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyContextualSwashes<TElement>(this TElement element, int value)
        where TElement : TextElement
    {
        element.Typography.ContextualSwashes = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// If set to <see langword="true" />, discretionary ligatures will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyDiscretionaryLigatures<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.DiscretionaryLigatures = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// If set to <see langword="true" />, standard Japanese font formats will be replaced with the
    /// corresponding preferred typographic formats.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyEastAsianExpertForms<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.EastAsianExpertForms = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Glyphs that will be used for a specific language or writing system for East Asia.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyEastAsianLanguage<TElement>(this TElement element, FontEastAsianLanguage value)
        where TElement : TextElement
    {
        element.Typography.EastAsianLanguage = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">Width of Latin characters in an Asian style font.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyEastAsianWidths<TElement>(this TElement element, FontEastAsianWidths value)
        where TElement : TextElement
    {
        element.Typography.EastAsianWidths = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">Uppercase letter style for a typography.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyFontCapitals<TElement>(this TElement element, FontCapitals value)
        where TElement : TextElement
    {
        element.Typography.Capitals = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">Numeral alignment to use.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyFontNumeralAlignment<TElement>(this TElement element, FontNumeralAlignment value) where TElement : TextElement
    {
        element.Typography.NumeralAlignment = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">Numeral style to use.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyFontNumeralStyle<TElement>(this TElement element, FontNumeralStyle value)
        where TElement : TextElement
    {
        element.Typography.NumeralStyle = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">Fraction representation style to use.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyFraction<TElement>(this TElement element, FontFraction value)
        where TElement : TextElement
    {
        element.Typography.Fraction = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">Flag indicating whether the use of standard forms will be enabled.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyHistoricalForms<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.HistoricalForms = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">Flag indicating whether the use of historical ligatures will be enabled.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyHistoricalLigatures<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.HistoricalLigatures = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">Flag indicating whether kerning will be enabled.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyKerning<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.Kerning = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether the standard glyphs used in mathematical notation
    /// with Greek symbols have been replaced by standard versions used in mathematical notation.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyMathematicalGreek<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.MathematicalGreek = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether the glyph to be used to represent the numeral
    /// digit zero (0) will be drawn in its slashed version.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographySlashedZero<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.SlashedZero = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">Flag indicating whether the use of standard ligatures will be enabled.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStandardLigatures<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StandardLigatures = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">Index of the standard form of typographic characters.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStandardSwashes<TElement>(this TElement element, int value)
        where TElement : TextElement
    {
        element.Typography.StandardSwashes = value;
        return element;
    }

    /// <summary>
    /// Manipulates the typographic information of the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of the <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">Stylistic alternative to use.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticAlternates<TElement>(this TElement element, int value)
        where TElement : TextElement
    {
        element.Typography.StylisticAlternates = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 1 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet1<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet1 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 10 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet10<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet10 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 11 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet11<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet11 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 12 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet12<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet12 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 13 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet13<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet13 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 14 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet14<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet14 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 15 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet15<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet15 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 16 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet16<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet16 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 17 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet17<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet17 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 18 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet18<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet18 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 19 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet19<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet19 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 2 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet2<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet2 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 20 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet20<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet20 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 3 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet3<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet3 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 4 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet4<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet4 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 5 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet5<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet5 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 6 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet6<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet6 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 7 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet7<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet7 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 8 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet8<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet8 = value;
        return element;
    }

    /// <summary>
    /// Enables the use of the stylistic set of fonts number 9 for
    /// the <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Flag indicating whether this typographic set will be enabled.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyStylisticSet9<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet9 = value;
        return element;
    }

    /// <summary>
    /// Sets the typographic variant to use for drawing the
    /// <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Type of the <see cref="TextElement" /> to manipulate.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="value">
    /// Typographic variant to use for drawing the
    /// <see cref="TextElement" />.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement TypographyVariants<TElement>(this TElement element, FontVariants value)
        where TElement : TextElement
    {
        element.Typography.Variants = value;
        return element;
    }
}