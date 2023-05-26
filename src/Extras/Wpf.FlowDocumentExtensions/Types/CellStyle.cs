/*
CellStyle.cs

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
using System.Windows.Media;

namespace TheXDS.MCART.FlowDocumentExtensions.Types;

/// <summary>
/// Estilo de elementos de WPF orientado a objetos
/// <see cref="System.Windows.Documents.TableCell"/>.
/// </summary>
public class CellStyle<T> : WpfStyle, ICellStyle
{
    private Brush? _oddBackground;

    /// <summary>
    /// <see cref="Brush"/> de fondo a aplicar al elemento para elementos impares.
    /// </summary>
    public Brush? OddBackground
    {
        get => _oddBackground ?? Background;
        set => _oddBackground = value;
    }

    /// <summary>
    /// Ancho de la celda.
    /// </summary>
    public GridLength? Width { get; set; }

    /// <summary>
    /// Alineación de texto a utilizar para colocar la información
    /// de la celda.
    /// </summary>
    public TextAlignment Alignment { get; set; }

    /// <summary>
    /// Obtiene o establece la función condicional para aplicar este
    /// estilo.
    /// </summary>
    public Func<T, bool>? StyleApplies { get; set; } = _ => true;
}
