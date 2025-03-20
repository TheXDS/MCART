﻿/*
ColumnBuilder.cs

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

namespace TheXDS.MCART.FlowDocumentExtensions.Types;

/// <summary>
/// Class that describes a data selector for generating a
/// data column within a table.
/// </summary>
/// <typeparam name="TObject">
/// Type of object from which the column information will be extracted.
/// </typeparam>
/// <typeparam name="TItem">
/// Type of item to return for this column.
/// </typeparam>
/// <param name="header">
/// Title of the column.
/// </param>
/// <param name="selector">
/// Data selection function.
/// </param>
public class ColumnBuilder<TObject, TItem>(string header, Func<TObject, TItem> selector) : IColumnBuilder<TObject>
{
    /// <summary>
    /// Title of the column.
    /// </summary>
    public string Header { get; } = header;

    /// <summary>
    /// Content of the current row to be set in this column.
    /// </summary>
    /// <param name="obj">Object from which to extract the content.</param>
    /// <returns>
    /// The content in string format to place within the cell 
    /// corresponding to this column in the current row.
    /// </returns>
    public string Content(TObject obj) => Selector?.Invoke(obj)?.ToString() ?? obj?.ToString() ?? string.Empty;

    /// <summary>
    /// Gets a style to be used on the cell in this column of the current row.
    /// </summary>
    /// <param name="item"></param>
    /// <returns>
    /// A style to apply to the generated cell, or
    /// <see langword="null"/> if a valid style cannot be obtained.
    /// </returns>
    public ICellStyle? Style(object? item) => item is TItem tItem ? CurrentStyle(tItem) : null;

    /// <summary>
    /// Data selection function.
    /// </summary>
    public Func<TObject, TItem> Selector { get; } = selector;

    /// <summary>
    /// Gets a collection of styles registered for this column.
    /// </summary>
    public IList<CellStyle<TItem>> Styles { get; } = new List<CellStyle<TItem>>();

    /// <summary>
    /// Gets a style to be used for the current item.
    /// </summary>
    /// <param name="currentItem">
    /// Value against which to evaluate the styles.
    /// </param>
    /// <returns>
    /// A style that can be applied to the value, or
    /// <see langword="null"/> if no style can be applied.
    /// </returns>
    public CellStyle<TItem>? CurrentStyle(TItem currentItem) => Styles.FirstOrDefault(p => p.StyleApplies?.Invoke(currentItem) ?? true);
}
