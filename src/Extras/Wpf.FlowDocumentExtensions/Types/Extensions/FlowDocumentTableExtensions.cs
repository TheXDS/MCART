/*
FlowDocumentTableExtensions.cs

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
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.FlowDocumentExtensions.Types.Extensions;

/// <summary>
/// Fluent type extensions for manipulating
/// <see cref="FlowDocument" /> objects focused on table creation.
/// </summary>
public static class FlowDocumentTableExtensions
{
    /// <summary>
    /// Adds an empty table cell to the current row.
    /// </summary>
    /// <param name="row">
    /// Table row to which the cell will be added.
    /// </param>
    /// <returns>
    /// The cell that has been added.
    /// </returns>
    public static TableCell AddCell(this TableRow row)
    {
        TableCell? c = new();
        row.Cells.Add(c);
        return c;
    }

    /// <summary>
    /// Adds an empty table cell to the current row.
    /// </summary>
    /// <param name="row">
    /// Table row to which the cell will be added.
    /// </param>
    /// <param name="columnSpan">
    /// Number of columns that the new cell can occupy.
    /// </param>
    /// <returns>
    /// The cell that has been added.
    /// </returns>
    public static TableCell AddCell(this TableRow row, int columnSpan)
    {
        TableCell? c = new() { ColumnSpan = columnSpan };
        row.Cells.Add(c);
        return c;
    }

    /// <summary>
    /// Adds an empty table cell to the current row.
    /// </summary>
    /// <param name="row">
    /// Table row to which the cell will be added.
    /// </param>
    /// <param name="rowSpan">
    /// Number of rows that the new cell can occupy.
    /// </param>
    /// <param name="columnSpan">
    /// Number of columns that the new cell can occupy.
    /// </param>
    /// <returns>
    /// The cell that has been added.
    /// </returns>
    public static TableCell AddCell(this TableRow row, int rowSpan, int columnSpan)
    {
        TableCell? c = new() { RowSpan = rowSpan, ColumnSpan = columnSpan };
        row.Cells.Add(c);
        return c;
    }

    /// <summary>
    /// Adds a simple cell with text to a row.
    /// </summary>
    /// <param name="row">Row to which to add the new cell.</param>
    /// <param name="text">Text of the cell.</param>
    /// <returns>
    /// <paramref name="row"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static TableCell AddCell(this TableRow row, string text)
    {
        return row.AddCell(text, FontWeights.Normal);
    }

    /// <summary>
    /// Adds a simple cell with text to a row.
    /// </summary>
    /// <param name="row">Row to which to add the new cell.</param>
    /// <param name="text">Text of the cell.</param>
    /// <param name="alignment">
    /// Horizontal text alignment within the cell.
    /// </param>
    /// <returns>
    /// <paramref name="row"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static TableCell AddCell(this TableRow row, string text, TextAlignment alignment)
    {
        return row.Cells.Push(new TableCell(new Paragraph(new Run(text)) { TextAlignment = alignment }));
    }

    /// <summary>
    /// Adds a simple cell with text to a row.
    /// </summary>
    /// <param name="row">Row to which to add the new cell.</param>
    /// <param name="text">Text of the cell.</param>
    /// <param name="weight">
    /// Font weight to use within the cell.
    /// </param>
    /// <returns>
    /// <paramref name="row"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static TableCell AddCell(this TableRow row, string text, FontWeight weight)
    {
        return row.Cells.Push(new TableCell(new Paragraph(new Run(text) { FontWeight = weight })));
    }

    /// <summary>
    /// Adds a simple cell with text to a row.
    /// </summary>
    /// <param name="row">Row to which to add the new cell.</param>
    /// <param name="text">Text of the cell.</param>
    /// <param name="alignment">
    /// Horizontal text alignment within the cell.
    /// </param>
    /// <param name="weight">
    /// Font weight to use within the cell.
    /// </param>
    /// <returns>
    /// <paramref name="row"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static TableCell AddCell(this TableRow row, string text, TextAlignment alignment, FontWeight weight)
    {
        return row.Cells.Push(new TableCell(new Paragraph(new Run(text) { FontWeight = weight }) { TextAlignment = alignment }));
    }

    /// <summary>
    /// Adds a new row group to the table.
    /// </summary>
    /// <param name="table">
    /// Table to which to add the new row group.
    /// </param>
    /// <returns>
    /// The new row group that has been added to the table.
    /// </returns>
    public static TableRowGroup AddGroup(this Table table)
    {
        TableRowGroup? rowGroup = new();
        table.RowGroups.Add(rowGroup);
        return rowGroup;
    }

    /// <summary>
    /// Adds a new row group to the table.
    /// </summary>
    /// <param name="table">
    /// Table to which to add the new row group.
    /// </param>
    /// <param name="newGroup">
    /// New instance of the row group to add to the table.
    /// </param>
    /// <returns>
    /// <paramref name="newGroup"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static TableRowGroup AddGroup(this Table table, TableRowGroup newGroup)
    {
        table.RowGroups.Add(newGroup);
        return newGroup;
    }

    /// <summary>
    /// Adds a new row to the row group.
    /// </summary>
    /// <param name="group">
    /// Row group to which to add the new row.
    /// </param>
    /// <returns>
    /// A reference to the new row created within the row group
    /// of the table.
    /// </returns>
    public static TableRow AddRow(this TableRowGroup group)
    {
        TableRow? row = new();
        group.Rows.Add(row);
        return row;
    }

    /// <summary>
    /// Adds a new row to the row group.
    /// </summary>
    /// <param name="rg">
    /// Row group to which to add the new row.
    /// </param>
    /// <param name="values">Values to add to the row.</param>
    /// <returns>
    /// A reference to the new row created within the row group
    /// of the table.
    /// </returns>
    public static TableRowGroup AddRow(this TableRowGroup rg, IEnumerable<string> values)
    {
        List<string>? lst = values.ToList();

        TableRow? row = new();
        foreach (string? j in lst) row.Cells.Add(j);
        rg.Rows.Add(row);
        return rg;
    }

    /// <summary>
    /// Adds a new row to the table.
    /// </summary>
    /// <param name="tbl">
    /// The table to which the new row will be added.
    /// </param>
    /// <param name="values">Values to add to the row.</param>
    /// <returns>
    /// A reference to the newly created row within a new group
    /// of rows in the table.
    /// </returns>
    public static TableRow AddRow(this Table tbl, IEnumerable<string> values)
    {
        List<string>? lst = values.ToList();

        if (lst.Count > tbl.Columns.Count) throw new ArgumentOutOfRangeException(nameof(values));

        TableRowGroup? rg = new();
        TableRow? row = new();
        foreach (string? j in lst) row.Cells.Add(new TableCell(new Paragraph(new Run(j))));
        rg.Rows.Add(row);

        tbl.RowGroups.Add(rg);

        return row;
    }

    /// <summary>
    /// Adds a new row to the row group.
    /// </summary>
    /// <param name="rg">
    /// The row group to which the new row will be added.
    /// </param>
    /// <param name="values">Values to add to the row.</param>
    /// <returns>
    /// A reference to the newly created row within the row group
    /// of the table.
    /// </returns>
    public static TableRowGroup AddRow(this TableRowGroup rg, params string[] values)
    {
        return rg.AddRow(values.ToList());
    }

    /// <summary>
    /// Adds a new row to the row group.
    /// </summary>
    /// <param name="rg">
    /// The row group to which the new row will be added.
    /// </param>
    /// <param name="cells">Cells to add to the row.</param>
    /// <returns>
    /// A reference to the newly created row within the row group
    /// of the table.
    /// </returns>
    public static TableRowGroup AddRow(this TableRowGroup rg, IEnumerable<TableCell> cells)
    {
        TableRow? tr = new();
        foreach (TableCell? j in cells) tr.Cells.Add(j);

        rg.Rows.Add(tr);
        return rg;
    }

    /// <summary>
    /// Adds a new row to the row group.
    /// </summary>
    /// <param name="rg">
    /// The row group to which the new row will be added.
    /// </param>
    /// <param name="cells">Cells to add to the row.</param>
    /// <returns>
    /// A reference to the newly created row within the row group
    /// of the table.
    /// </returns>
    public static TableRowGroup AddRow(this TableRowGroup rg, params TableCell[] cells)
    {
        return rg.AddRow(cells.ToList());
    }

    /// <summary>
    /// Adds a new row to the row group.
    /// </summary>
    /// <param name="rg">
    /// The row group to which the new row will be added.
    /// </param>
    /// <param name="values">Values to add to the row.</param>
    /// <returns>
    /// A reference to the newly created row within the row group
    /// of the table.
    /// </returns>
    public static TableRowGroup AddRow(this TableRowGroup rg, IEnumerable<Block> values)
    {
        List<Block>? lst = values.ToList();

        TableRow? row = new();
        foreach (Block? j in lst) row.Cells.Add(new TableCell(j));
        rg.Rows.Add(row);
        return rg;
    }

    /// <summary>
    /// Adds a new row to the row group.
    /// </summary>
    /// <param name="rg">
    /// The row group to which the new row will be added.
    /// </param>
    /// <param name="values">Values to add to the row.</param>
    /// <returns>
    /// A reference to the newly created row within the row group
    /// of the table.
    /// </returns>
    public static TableRowGroup AddRow(this TableRowGroup rg, params Block[] values)
    {
        return rg.AddRow(values.ToList());
    }

    /// <summary>
    /// Sets a border for a table cell.
    /// </summary>
    /// <param name="element">Cell to process.</param>
    /// <param name="brush"><see cref="Brush" /> to use for drawing the border.</param>
    /// <param name="thickness">Thickness of the border.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TableCell Border(this TableCell element, Brush brush, Thickness thickness)
    {
        element.BorderBrush = brush;
        element.BorderThickness = thickness;
        return element;
    }

    /// <summary>
    /// Sets a border for all cells in a <see cref="TableRow" />.
    /// </summary>
    /// <param name="element"><see cref="TableRow" /> to process.</param>
    /// <param name="brush"><see cref="Brush" /> to use for drawing the border.</param>
    /// <param name="thickness">Thickness of the border.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TableRow Borders(this TableRow element, Brush brush, Thickness thickness)
    {
        foreach (TableCell? j in element.Cells)
            j.Border(brush, thickness);
        return element;
    }

    /// <summary>
    /// Sets a border for all cells in a <see cref="TableRowGroup" />.
    /// </summary>
    /// <param name="element"><see cref="TableRowGroup" /> to process.</param>
    /// <param name="brush"><see cref="Brush" /> to use for drawing the border.</param>
    /// <param name="thickness">Thickness of the border.</param>
    /// <returns>
    /// <paramref name="element" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TableRowGroup Borders(this TableRowGroup element, Brush brush, Thickness thickness)
    {
        foreach (TableCell? j in element.Rows.SelectMany(p => p.Cells))
            j.Border(brush, thickness);
        return element;
    }

    /// <summary>
    /// Centers all content blocks in a row.
    /// </summary>
    /// <param name="row">
    /// The row to which the operation should be applied.
    /// </param>
    /// <returns>
    /// <paramref name="row" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TableRow CenterAll(this TableRow row)
    {
        foreach (Block? j in row.Cells.SelectMany(p => p.Blocks)) j.Center();
        return row;
    }

    /// <summary>
    /// Centers all content blocks in a row group.
    /// </summary>
    /// <param name="rowGroup">
    /// The row group to which the operation should be applied.
    /// </param>
    /// <returns>
    /// <paramref name="rowGroup" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TableRowGroup CenterAll(this TableRowGroup rowGroup)
    {
        foreach (Block? j in rowGroup.Rows.SelectMany(o => o.Cells.SelectMany(p => p.Blocks))) j.Center();
        return rowGroup;
    }

    /// <summary>
    /// Sets a background color for the specified column of the table.
    /// </summary>
    /// <param name="table"><see cref="Table" /> to process.</param>
    /// <param name="column">Index of the column.</param>
    /// <param name="brush"><see cref="Brush" /> to apply when drawing the table.</param>
    /// <returns>
    /// <paramref name="table" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static Table ColumnBackground(this Table table, int column, Brush brush)
    {
        table.Columns[column].Background = brush;
        return table;
    }

    /// <summary>
    /// Sets a column span value for the specified cell.
    /// </summary>
    /// <param name="cell">
    /// Cell to span.
    /// </param>
    /// <param name="span">
    /// Number of columns that the cell spans.
    /// </param>
    /// <returns>
    /// <paramref name="cell" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TableCell ColumnSpan(this TableCell cell, int span)
    {
        cell.ColumnSpan = span;
        return cell;
    }

    /// <summary>
    /// Sets the width of a column for a <see cref="Table" />.
    /// </summary>
    /// <param name="table"><see cref="Table" /> to process.</param>
    /// <param name="column">Index of the column.</param>
    /// <param name="width">Column width to apply.</param>
    /// <returns>
    /// <paramref name="table" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static Table ColumnWidth(this Table table, int column, GridLength width)
    {
        table.Columns[column].Width = width;
        return table;
    }

    /// <summary>
    /// Sets the column widths for a <see cref="Table" />.
    /// </summary>
    /// <param name="table"><see cref="Table" /> to process.</param>
    /// <param name="lengths">Column widths to apply.</param>
    /// <returns>
    /// <paramref name="table" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static Table ColumnWidths(this Table table, IEnumerable<double> lengths)
    {
        return table.ColumnWidths(lengths.Select(p => new GridLength(p)));
    }

    /// <summary>
    /// Sets the column widths for a <see cref="Table" />.
    /// </summary>
    /// <param name="table"><see cref="Table" /> to process.</param>
    /// <param name="lengths">Column widths to apply.</param>
    /// <returns>
    /// <paramref name="table" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static Table ColumnWidths(this Table table, IEnumerable<GridLength> lengths)
    {
        using (List<TableColumn>.Enumerator a = table.Columns.ToList().GetEnumerator())
        using (IEnumerator<GridLength>? b = lengths.GetEnumerator())
        {
            while (a.MoveNext() && b.MoveNext())
                if (a.Current is not null)
                    a.Current.Width = b.Current;
        }

        return table;
    }

    /// <summary>
    /// Marks the end of the context of a table row, returning to
    /// its parent group in a way that is compatible with Fluent syntax.
    /// </summary>
    /// <param name="row">
    /// Row for which to end the Fluent syntax context.
    /// </param>
    /// <returns>
    /// The row group to which this row belongs.
    /// </returns>
    public static TableRowGroup Done(this TableRow row)
    {
        return (TableRowGroup)row.Parent;
    }

    /// <summary>
    /// Marks the end of the context of a table row group,
    /// returning to its parent table in a way that is compatible
    /// with Fluent syntax.
    /// </summary>
    /// <param name="rowGroup">
    /// Row group for which to end the Fluent syntax context.
    /// </param>
    /// <returns>
    /// The table to which this row group belongs.
    /// </returns>
    public static Table Done(this TableRowGroup rowGroup)
    {
        return (Table)rowGroup.Parent;
    }

    /// <summary>
    /// Marks the end of the context of a cell, returning to its parent row
    /// in a way that is compatible with Fluent syntax.
    /// </summary>
    /// <param name="cell">
    /// Cell for which to end the Fluent syntax context.
    /// </param>
    /// <returns>
    /// The row to which this cell belongs.
    /// </returns>
    public static TableRow Done(this TableCell cell)
    {
        return (TableRow)cell.Parent;
    }

    /// <summary>
    /// Applies a style to a cell.
    /// </summary>
    /// <param name="cell">Cell to style.</param>
    /// <param name="style">Style to apply to the cell.</param>
    /// <returns>
    /// <paramref name="cell" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TableCell ApplyStyle(this TableCell cell, ICellStyle? style)
    {
        return cell.ApplyStyle(style, false);
    }

    /// <summary>
    /// Applies a style to a cell.
    /// </summary>
    /// <param name="cell">Cell to style.</param>
    /// <param name="style">Style to apply to the cell.</param>
    /// <param name="odd">
    /// Flag indicating whether it is an odd row or not.
    /// </param>
    /// <returns>
    /// <paramref name="cell" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TableCell ApplyStyle(this TableCell cell, ICellStyle? style, bool odd)
    {
        static void DoIf<T>(T? value, Action<T> op)
        {
            if (value is not null) op.Invoke(value);
        }

        if (style is not null)
        {
            DoIf(style.Background, p => cell.Background = (odd ? style.OddBackground : null) ?? p);
            DoIf(style.Foreground, p => cell.Foreground = p);
            DoIf(style.BorderBrush, p => cell.BorderBrush = p);
            DoIf(style.BorderThickness, p => cell.BorderThickness = p!.Value);
            cell.TextAlignment = style.Alignment;
        }
        return cell;
    }

    /// <summary>
    /// Adds a new cell with the specified textual content.
    /// </summary>
    /// <param name="cells">
    /// Collection of cells in which to add a new cell with the
    /// specified content.
    /// </param>
    /// <param name="content">
    /// Textual content to include in the cell.
    /// </param>
    /// <returns>
    /// A reference to the newly created cell.
    /// </returns>
    public static TableCell Add(this TableCellCollection cells, string content)
    {
        return cells.Add(new Run(content));
    }

    /// <summary>
    /// Adds a new cell with the specified content.
    /// </summary>
    /// <param name="cells">
    /// Collection of cells in which to add a new cell with the
    /// specified content.
    /// </param>
    /// <param name="content">
    /// Content to include in the cell.
    /// </param>
    /// <returns>
    /// A reference to the newly created cell.
    /// </returns>
    public static TableCell Add(this TableCellCollection cells, Inline content)
    {
        TableCell? c = new(new Paragraph(content));
        cells.Add(c);
        return c;
    }

    /// <summary>
    /// Indicates that a cell occupies a specified number of rows.
    /// </summary>
    /// <param name="cell">Cell to process.</param>
    /// <param name="span">Number of rows that this cell should occupy.</param>
    /// <returns>
    /// <paramref name="cell" />, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static TableCell RowSpan(this TableCell cell, int span)
    {
        cell.RowSpan = span;
        return cell;
    }

    /// <summary>
    /// Sets the content of the cell to the specified text.
    /// </summary>
    /// <param name="cell">Cell to process.</param>
    /// <param name="text">Text of the cell.</param>
    /// <returns>
    /// A <see cref="Paragraph"/> element that represents the textual
    /// content of the cell.
    /// </returns>
    public static Paragraph Text(this TableCell cell, string text)
    {
        Paragraph p = new(new Run(text));
        cell.Blocks.Add(p);
        return p;
    }
}
