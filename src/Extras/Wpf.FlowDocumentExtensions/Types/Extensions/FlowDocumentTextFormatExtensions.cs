/*
FlowDocumentTextFormatExtensions.cs

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

using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace TheXDS.MCART.FlowDocumentExtensions.Types.Extensions;

/// <summary>
/// Extensiones de tipo Fluent para manipular objetos
/// <see cref="FlowDocument" /> centradas en el formato de elementos de texto.
/// </summary>
public static class FlowDocumentTextFormatExtensions
{
    /// <summary>
    /// Establece un color de fondo para un <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> al cual aplicarle el color de fondo.</typeparam>
    /// <param name="element"><see cref="TextElement" /> al cual aplicarle el color de fondo.</param>
    /// <param name="value">Fondo a aplicar.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement Background<TElement>(this TElement element, Brush value) where TElement : TextElement
    {
        element.Background = value;
        return element;
    }

    /// <summary>
    /// Establece el formato de texto en negrita.
    /// </summary>
    /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement Bold<TElement>(this TElement element) where TElement : TextElement
    {
        element.FontWeight = FontWeights.Bold;
        return element;
    }

    /// <summary>
    /// Establece la alineación de texto en central.
    /// </summary>
    /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement Center<TElement>(this TElement element) where TElement : Block
    {
        element.TextAlignment = TextAlignment.Center;
        return element;
    }

    /// <summary>
    /// Establece un efecto de texto sobre un <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="effect">Efecto a aplicar al texto.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement Effect<TElement>(this TElement element, TextEffect effect) where TElement : TextElement
    {
        element.TextEffects.Add(effect);
        return element;
    }

    /// <summary>
    /// Establece un color principal para un <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> al cual aplicarle el color principal.</typeparam>
    /// <param name="element"><see cref="TextElement" /> al cual aplicarle el color principal.</param>
    /// <param name="value">Color principal a aplicar.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement Foreground<TElement>(this TElement element, Brush value) where TElement : TextElement
    {
        element.Foreground = value;
        return element;
    }

    /// <summary>
    /// Establece el formato de un <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="fontFamily">
    /// Familia de fuentes a utilizar.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement Format<TElement>(this TElement element, FontFamily fontFamily)
        where TElement : TextElement
    {
        element.FontFamily = fontFamily;
        return element;
    }

    /// <summary>
    /// Establece el formato de un <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="fontWeight">
    /// Densidad de la fuente.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement Format<TElement>(this TElement element, FontWeight fontWeight)
        where TElement : TextElement
    {
        element.FontWeight = fontWeight;
        return element;
    }

    /// <summary>
    /// Establece el formato de un <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="fontSize">Tamaño de la fuente.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement Format<TElement>(this TElement element, double fontSize) where TElement : TextElement
    {
        element.FontSize = fontSize;
        return element;
    }

    /// <summary>
    /// Establece el formato de un <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="fontStretch">
    /// Estiramiento de la fuente.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement Format<TElement>(this TElement element, FontStretch fontStretch)
        where TElement : TextElement
    {
        element.FontStretch = fontStretch;
        return element;
    }

    /// <summary>
    /// Establece el formato de un <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="fontStyle">Estilo de la fuente.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement Format<TElement>(this TElement element, FontStyle fontStyle) where TElement : TextElement
    {
        element.FontStyle = fontStyle;
        return element;
    }

    /// <summary>
    /// Establece el formato de un <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="alignment">Alineación de texto.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement Format<TElement>(this TElement element, TextAlignment alignment) where TElement : Block
    {
        element.TextAlignment = alignment;
        return element;
    }

    /// <summary>
    /// Establece la alineación del texto en justificada.
    /// </summary>
    /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement Justify<TElement>(this TElement element) where TElement : Block
    {
        element.TextAlignment = TextAlignment.Justify;
        return element;
    }

    /// <summary>
    /// Establece la alineación del texto en izquierda.
    /// </summary>
    /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement Left<TElement>(this TElement element) where TElement : Block
    {
        element.TextAlignment = TextAlignment.Left;
        return element;
    }

    /// <summary>
    /// Establece la alineación del texto en derecha.
    /// </summary>
    /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement Right<TElement>(this TElement element) where TElement : Block
    {
        element.TextAlignment = TextAlignment.Right;
        return element;
    }
}
