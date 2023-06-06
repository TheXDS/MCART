/*
FlowDocumentTypographyExtensions.cs

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

namespace TheXDS.MCART.FlowDocumentExtensions.Types.Extensions;

/// <summary>
/// Extensiones de tipo Fluent para manipular objetos
/// <see cref="FlowDocument" /> centradas en la configuración tipográfica.
/// </summary>
public static class FlowDocumentTypographyExtensions
{
    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">Índice de formato de anotación alternativa.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyAnnotationAlternates<TElement>(this TElement element, int value)
        where TElement : TextElement
    {
        element.Typography.AnnotationAlternates = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// si se establece en <see langword="true" />, se ajustará globalmente el espacio entre glifos en mayúsculas para
    /// mejorar la legibilidad.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyCapitalSpacing<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.CapitalSpacing = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Si se establece en <see langword="true" />, se ajustará la posición vertical de los glifos para una mejor alineación
    /// con los glifos en mayúsculas.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyCaseSensitiveForms<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.CaseSensitiveForms = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Si se establece en <see langword="true" /> se utilizarán glifos personalizados según el contexto del texto que se
    /// procesa.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyContextualAlternates<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.ContextualAlternates = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Si se establece en <see langword="true" />, se habilitan las ligaduras contextuales.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyContextualLigatures<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.ContextualLigatures = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">Especifica el índice de un formulario de glifos floreados contextuales.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyContextualSwashes<TElement>(this TElement element, int value)
        where TElement : TextElement
    {
        element.Typography.ContextualSwashes = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Si se establece en <see langword="true" />, se habilitan las ligaduras discrecionales.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyDiscretionaryLigatures<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.DiscretionaryLigatures = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Si se establece en <see langword="true" />, los formatos de fuente japonesa estándar se reemplazarán por los
    /// correspondientes formatos tipográficos preferidos.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyEastAsianExpertForms<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.EastAsianExpertForms = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Glifos que se utilizarán para un idioma o sistema de escritura en específico para Asia oriental.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyEastAsianLanguage<TElement>(this TElement element, FontEastAsianLanguage value)
        where TElement : TextElement
    {
        element.Typography.EastAsianLanguage = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">Ancho de los caracteres latinos en uan fuente de estilo asiático.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyEastAsianWidths<TElement>(this TElement element, FontEastAsianWidths value)
        where TElement : TextElement
    {
        element.Typography.EastAsianWidths = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">Estilo de letra mayúscula para una tipografía.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyFontCapitals<TElement>(this TElement element, FontCapitals value)
        where TElement : TextElement
    {
        element.Typography.Capitals = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">Alineación numeral a utilizar.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyFontNumeralAlignment<TElement>(this TElement element, FontNumeralAlignment value) where TElement : TextElement
    {
        element.Typography.NumeralAlignment = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">Estilo de numerales a utilizar.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyFontNumeralStyle<TElement>(this TElement element, FontNumeralStyle value)
        where TElement : TextElement
    {
        element.Typography.NumeralStyle = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">Estilo de representación de fracciones a utilizar.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyFraction<TElement>(this TElement element, FontFraction value)
        where TElement : TextElement
    {
        element.Typography.Fraction = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">Bandera que indica si el uso de formas estándar estará habilitado.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyHistoricalForms<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.HistoricalForms = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">Bandera que indica si el uso de ligaduras históricas estará habilitado.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyHistoricalLigatures<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.HistoricalLigatures = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">Bandera que indica si el interletraje estará habilitado.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyKerning<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.Kerning = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si los glifos estándar utilizados en notación
    /// matemática con simbolos griegos han sido reemplazados por versiones
    /// estándar utilizadas en notación matemática.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyMathematicalGreek<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.MathematicalGreek = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si el glifo a utilizar para representar al dígito
    /// numeral cero (0) será dibujado en su versión con una línea transversal.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographySlashedZero<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.SlashedZero = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">Bandera que indica si el uso de ligaduras estándar estará habilitado.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStandardLigatures<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StandardLigatures = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">Índice de la forma estándar de los caracteres tipográficos.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStandardSwashes<TElement>(this TElement element, int value)
        where TElement : TextElement
    {
        element.Typography.StandardSwashes = value;
        return element;
    }

    /// <summary>
    /// Manipula la información tipográfica del <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">Alternativa estilística a utilizar.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticAlternates<TElement>(this TElement element, int value)
        where TElement : TextElement
    {
        element.Typography.StylisticAlternates = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 1 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet1<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet1 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 10 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet10<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet10 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 11 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet11<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet11 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 12 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet12<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet12 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 13 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet13<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet13 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 14 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet14<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet14 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 15 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet15<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet15 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 16 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet16<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet16 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 17 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet17<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet17 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 18 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet18<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet18 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 19 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet19<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet19 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 2 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet2<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet2 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 20 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet20<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet20 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 3 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet3<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet3 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 4 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet4<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet4 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 5 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet5<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet5 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 6 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet6<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet6 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 7 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet7<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet7 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 8 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet8<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet8 = value;
        return element;
    }

    /// <summary>
    /// Habilita el uso del conjunto estilístico de tipografías número 9 para
    /// el <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Bandera que indica si este conjunto tipográfico será habilitado.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyStylisticSet9<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet9 = value;
        return element;
    }

    /// <summary>
    /// Establece la variante tipográfica a utilizar para dibujar el
    /// <see cref="TextElement" />.
    /// </summary>
    /// <typeparam name="TElement">
    /// Tipo del <see cref="TextElement" /> a manipular.
    /// </typeparam>
    /// <param name="element"><see cref="TextElement" /> a manipular.</param>
    /// <param name="value">
    /// Variante tipográfica a utilizar para dibujar el
    /// <see cref="TextElement" />.
    /// </param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TElement TypographyVariants<TElement>(this TElement element, FontVariants value)
        where TElement : TextElement
    {
        element.Typography.Variants = value;
        return element;
    }
}