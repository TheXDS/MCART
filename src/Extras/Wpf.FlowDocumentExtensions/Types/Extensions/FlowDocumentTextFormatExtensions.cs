/*
FlowDocumentTextFormatExtensions.cs

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

using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace TheXDS.MCART.FlowDocumentExtensions.Types.Extensions;

/// <summary>
/// Fluent type extensions for manipulating <see cref="FlowDocument" />
/// objects focused on text element formatting.
/// </summary>
public static class FlowDocumentTextFormatExtensions
{
    /// <summary>
    /// Sets a background color for a <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of <see cref="TextElement" /> to which the background color will be applied.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to which the background color will be applied.</param>
    /// <param name="value">Background to apply.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement Background<TElement>(this TElement element, Brush value) where TElement : TextElement
    {
        element.Background = value;
        return element;
    }

    /// <summary>
    /// Sets the text format to bold.
    /// </summary>
    /// <typeparam name="TElement">Type of <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement Bold<TElement>(this TElement element) where TElement : TextElement
    {
        element.FontWeight = FontWeights.Bold;
        return element;
    }

    /// <summary>
    /// Sets the text alignment to center.
    /// </summary>
    /// <typeparam name="TElement">Type of <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement Center<TElement>(this TElement element) where TElement : Block
    {
        element.TextAlignment = TextAlignment.Center;
        return element;
    }

    /// <summary>
    /// Sets a text effect on a <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="effect">Effect to apply to the text.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement Effect<TElement>(this TElement element, TextEffect effect) where TElement : TextElement
    {
        element.TextEffects.Add(effect);
        return element;
    }

    /// <summary>
    /// Sets a foreground color for a <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of <see cref="TextElement" /> to which the foreground color will be applied.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to which the foreground color will be applied.</param>
    /// <param name="value">Foreground color to apply.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement Foreground<TElement>(this TElement element, Brush value) where TElement : TextElement
    {
        element.Foreground = value;
        return element;
    }

    /// <summary>
    /// Sets the format of a <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="fontFamily">
    /// Font family to use.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement Format<TElement>(this TElement element, FontFamily fontFamily)
        where TElement : TextElement
    {
        element.FontFamily = fontFamily;
        return element;
    }

    /// <summary>
    /// Sets the format of a <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="fontWeight">
    /// Font weight.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement Format<TElement>(this TElement element, FontWeight fontWeight)
        where TElement : TextElement
    {
        element.FontWeight = fontWeight;
        return element;
    }

    /// <summary>
    /// Sets the format of a <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="fontSize">Font size.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement Format<TElement>(this TElement element, double fontSize) where TElement : TextElement
    {
        element.FontSize = fontSize;
        return element;
    }

    /// <summary>
    /// Sets the format of a <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="fontStretch">
    /// Font stretch.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement Format<TElement>(this TElement element, FontStretch fontStretch)
        where TElement : TextElement
    {
        element.FontStretch = fontStretch;
        return element;
    }

    /// <summary>
    /// Sets the format of a <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="fontStyle">Font style.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement Format<TElement>(this TElement element, FontStyle fontStyle) where TElement : TextElement
    {
        element.FontStyle = fontStyle;
        return element;
    }

    /// <summary>
    /// Sets the format of a <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Type of <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <param name="alignment">Text alignment.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement Format<TElement>(this TElement element, TextAlignment alignment) where TElement : Block
    {
        element.TextAlignment = alignment;
        return element;
    }

    /// <summary>
    /// Sets the text alignment to justified.
    /// </summary>
    /// <typeparam name="TElement">Type of <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement Justify<TElement>(this TElement element) where TElement : Block
    {
        element.TextAlignment = TextAlignment.Justify;
        return element;
    }

    /// <summary>
    /// Sets the text alignment to left.
    /// </summary>
    /// <typeparam name="TElement">Type of <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement Left<TElement>(this TElement element) where TElement : Block
    {
        element.TextAlignment = TextAlignment.Left;
        return element;
    }

    /// <summary>
    /// Sets the text alignment to right.
    /// </summary>
    /// <typeparam name="TElement">Type of <see cref="TextElement" /> to manipulate.</typeparam>
    /// <param name="element"><see cref="TextElement" /> to manipulate.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TElement Right<TElement>(this TElement element) where TElement : Block
    {
        element.TextAlignment = TextAlignment.Right;
        return element;
    }
}
