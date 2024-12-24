/*
FlowDocumentExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using TheXDS.MCART.Math;

namespace TheXDS.MCART.FlowDocumentExtensions.Types.Extensions;

/// <summary>
/// Fluent type extensions for manipulating <see cref="FlowDocument" /> objects.
/// </summary>
public static partial class FlowDocumentExtensions
{
    /// <summary>
    /// Prints a <see cref="FlowDocument"/> using the operating system's print dialog.
    /// </summary>
    /// <param name="fd">
    /// <see cref="FlowDocument"/> to print.
    /// </param>
    /// <param name="title">
    /// Title of the document to print.
    /// </param>
    public static void Print(this FlowDocument fd, string title)
    {
        PrintDialog? dialog = new();
        if (!dialog.ShowDialog() ?? true) return;
        Size sz = new(dialog.PrintableAreaWidth, dialog.PrintableAreaHeight);

        DocumentPaginator? paginator = (fd as IDocumentPaginatorSource).DocumentPaginator;
        paginator.PageSize = sz;
        dialog.PrintDocument(paginator, title);
    }

    /// <summary>
    /// Directly prints a <see cref="FlowDocument"/> without going through
    /// the operating system's print dialog.
    /// </summary>
    /// <param name="fd">
    /// <see cref="FlowDocument"/> to print.
    /// </param>
    /// <param name="title">
    /// Title of the document to print.
    /// </param>
    public static void PrintDirect(this FlowDocument fd, string title)
    {
        PrintDialog? dialog = new();
        Size sz = new(dialog.PrintableAreaWidth, dialog.PrintableAreaHeight);
        DocumentPaginator? paginator = (fd as IDocumentPaginatorSource).DocumentPaginator;
        paginator.PageSize = sz;
        dialog.PrintDocument(paginator, title);
    }

    /// <summary>
    /// Adds a new table to the specified <see cref="FlowDocument"/>.
    /// </summary>
    /// <param name="document">
    /// Document to which the new table will be added.
    /// </param>
    /// <param name="columnWidths">
    /// Column widths to set.
    /// </param>
    /// <returns>
    /// A reference to the newly created table.
    /// </returns>
    public static Table AddTable(this FlowDocument document, params GridLength[] columnWidths)
    {
        Table? t = new();
        foreach (GridLength j in columnWidths)
            t.Columns.Add(new TableColumn { Width = j });
        document.Blocks.Add(t);
        return t;
    }

    /// <summary>
    /// Adds a new table to the specified <see cref="FlowDocument"/>.
    /// </summary>
    /// <param name="document">
    /// Document to which the new table will be added.
    /// </param>
    /// <param name="columns">
    /// Columns to add.
    /// </param>
    /// <returns>
    /// A reference to the newly created table.
    /// </returns>
    public static Table AddTable(this FlowDocument document, IEnumerable<KeyValuePair<string, GridLength>> columns)
    {
        Table? t = new();
        t.AddGroup().AddRow(columns.Select(p =>
        {
            t.Columns.Add(new TableColumn { Width = p.Value });
            return p.Key;
        })).Bold();
        document.Blocks.Add(t);
        return t;
    }

    /// <summary>
    /// Marks the end of the context of an element, returning to its
    /// parent.
    /// </summary>
    /// <typeparam name="T">Type of the parent to return.</typeparam>
    /// <param name="block">
    /// Block from which to obtain the parent.
    /// </param>
    /// <returns>
    /// The parent of the specified element.
    /// </returns>
    /// <exception cref="InvalidCastException">
    /// Thrown if the parent of the element is not of type
    /// <typeparamref name="T"/>.
    /// </exception>
    public static T Done<T>(this FrameworkContentElement block) where T : class
    {
        return block.Parent as T ?? throw new InvalidCastException();
    }

    /// <summary>
    /// Constructs a table from a data enumeration and a collection of column descriptors.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements to include in the table.
    /// </typeparam>
    /// <param name="fd">
    /// <see cref="FlowDocument"/> in which the created table will be added.
    /// </param>
    /// <param name="columns">
    /// Collection of columns to include in the table.
    /// </param>
    /// <param name="data">
    /// Enumeration of data to include in the table.
    /// </param>
    /// <returns>
    /// <paramref name="fd"/>, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static Table MakeTable<T>(this FlowDocument fd, IEnumerable<IColumnBuilder<T>> columns, IEnumerable<T> data) => fd.MakeTable(columns, data, null);

    /// <summary>
    /// Constructs a table from a data enumeration and a collection of column descriptors.
    /// </summary>
    /// <typeparam name="T">
    /// Type of elements to include in the table.
    /// </typeparam>
    /// <param name="fd">
    /// <see cref="FlowDocument"/> in which the created table will be added.
    /// </param>
    /// <param name="columns">
    /// Collection of columns to include in the table.
    /// </param>
    /// <param name="data">
    /// Enumeration of data to include in the table.
    /// </param>
    /// <param name="headersStyle">
    /// Optional style to apply to the table headers.
    /// </param>
    /// <returns>
    /// <paramref name="fd"/>, allowing this function to be used
    /// with Fluent syntax.
    /// </returns>
    public static Table MakeTable<T>(this FlowDocument fd, IEnumerable<IColumnBuilder<T>> columns, IEnumerable<T> data, ICellStyle? headersStyle)
    {
        Table? tbl = new();
        TableRowGroup? rg = new();

        TableRow? headersRow = new() { FontWeight = FontWeights.Bold };
        List<IColumnBuilder<T>>? c = columns.ToList();
        foreach (IColumnBuilder<T>? j in c)
        {
            tbl.Columns.Add(new TableColumn());
            headersRow.Cells.Add(j.Header).ApplyStyle(headersStyle);
        }
        rg.Rows.Add(headersRow);

        bool odd = true;
        foreach (T? j in data)
        {
            TableRow? row = new();
            foreach (IColumnBuilder<T>? k in c)
            {
                row.Cells.Add(k.Content(j)).ApplyStyle(k.Style(j));
            }
            rg.Rows.Add(row);
            odd = !odd;
        }

        tbl.RowGroups.Add(rg);
        fd.Blocks.Add(tbl);
        return tbl;
    }

    /// <summary>
    /// Constructs a new table with the specified headers.
    /// </summary>
    /// <param name="fd">
    /// <see cref="FlowDocument"/> in which the created table will be added.
    /// </param>
    /// <param name="headers">
    /// Headers to add to the table.
    /// </param>
    /// <returns>
    /// A reference to a new <see cref="TableRowGroup"/> within
    /// the created table.
    /// </returns>
    public static TableRowGroup MakeTable(this FlowDocument fd, IEnumerable<string> headers) => fd.MakeTable(headers, null);

    /// <summary>
    /// Constructs a new table with the specified headers.
    /// </summary>
    /// <param name="fd">
    /// <see cref="FlowDocument"/> in which the created table will be added.
    /// </param>
    /// <param name="headers">
    /// Headers to add to the table.
    /// </param>
    /// <param name="headersStyle">
    /// Optional style to apply to the table headers.
    /// </param>
    /// <returns>
    /// A reference to a new <see cref="TableRowGroup"/> within
    /// the created table.
    /// </returns>
    public static TableRowGroup MakeTable(this FlowDocument fd, IEnumerable<string> headers, ICellStyle? headersStyle)
    {
        Table? tbl = new();
        TableRowGroup? rg = new();

        TableRow? headersRow = new() { FontWeight = FontWeights.Bold };
        foreach (string? j in headers)
        {
            tbl.Columns.Add(new TableColumn());
            headersRow.Cells.Add(j).ApplyStyle(headersStyle);
        }

        rg.Rows.Add(headersRow);
        tbl.RowGroups.Add(rg);
        fd.Blocks.Add(tbl);
        return rg;
    }

    /// <summary>
    /// Creates a new paragraph.
    /// </summary>
    /// <param name="fd">
    /// <see cref="FlowDocument"/> in which the new paragraph will be added.
    /// </param>
    /// <returns>
    /// A reference to the new
    /// <see cref="System.Windows.Documents.Paragraph"/> that has been added
    /// to the document.
    /// </returns>
    public static Paragraph Paragraph(this FlowDocument fd)
    {
        Paragraph p = new();
        fd.Blocks.Add(p);
        return p;
    }

    /// <summary>
    /// Creates a new paragraph.
    /// </summary>
    /// <param name="fd">
    /// <see cref="FlowDocument"/> in which the new paragraph will be added.
    /// </param>
    /// <param name="content">Content of the new paragraph.</param>
    /// <returns>
    /// <paramref name="fd"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static FlowDocument Paragraph(this FlowDocument fd, string content)
    {
        return fd.Paragraph(content, TextAlignment.Left);
    }

    /// <summary>
    /// Creates a new paragraph.
    /// </summary>
    /// <param name="fd">
    /// <see cref="FlowDocument"/> in which the new paragraph will be added.
    /// </param>
    /// <param name="content">Content of the new paragraph.</param>
    /// <param name="alignment">Text alignment of the new paragraph.</param>
    /// <returns>
    /// <paramref name="fd"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static FlowDocument Paragraph(this FlowDocument fd, string content, TextAlignment alignment)
    {
        foreach (string text in content.Replace("\n\r", "\n").Split('\n'))
            fd.Blocks.Add(new Paragraph { Inlines = { new Run { Text = text } }, TextAlignment = alignment });
        return fd;
    }

    /// <summary>
    /// Adds a text block to the paragraph.
    /// </summary>
    /// <param name="paragraph">Paragraph to which to add the new text.</param>
    /// <param name="text">Text to add.</param>
    /// <returns>
    /// <paramref name="paragraph"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static Paragraph Run(this Paragraph paragraph, string text)
    {
        paragraph.Inlines.Add(new Run(text));
        return paragraph;
    }

    /// <summary>
    /// Sets the alignment of the paragraph.
    /// </summary>
    /// <param name="paragraph">Paragraph to set the text alignment for.</param>
    /// <param name="alignment">Text alignment to use in the paragraph.</param>
    /// <returns>
    /// <paramref name="paragraph"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static Paragraph Style(this Paragraph paragraph, TextAlignment alignment)
    {
        paragraph.TextAlignment = alignment;
        return paragraph;
    }

    /// <summary>
    /// Sets the font weight of the paragraph.
    /// </summary>
    /// <param name="paragraph">Paragraph to set the font weight for.</param>
    /// <param name="weight">Font weight in the paragraph.</param>
    /// <returns>
    /// <paramref name="paragraph"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static Paragraph Style(this Paragraph paragraph, FontWeight weight)
    {
        paragraph.FontWeight = weight;
        return paragraph;
    }

    /// <summary>
    /// Sets the alignment and font weight values for the paragraph.
    /// </summary>
    /// <param name="paragraph">Paragraph to apply the style parameters to.</param>
    /// <param name="alignment">Text alignment to use in the paragraph.</param>
    /// <param name="weight">Font weight in the paragraph.</param>
    /// <returns>
    /// <paramref name="paragraph"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static Paragraph Style(this Paragraph paragraph, TextAlignment alignment, FontWeight weight)
    {
        paragraph.TextAlignment = alignment;
        paragraph.FontWeight = weight;
        return paragraph;
    }

    /// <summary>
    /// Adds a text block to the specified collection of text blocks.
    /// </summary>
    /// <param name="blocks">Collection of text blocks to which to add a new text block.</param>
    /// <param name="text">Text to add.</param>
    /// <returns>
    /// A reference to the new
    /// <see cref="System.Windows.Documents.Paragraph"/> that has been added
    /// to the collection of text blocks.
    /// </returns>
    public static Paragraph Text(this BlockCollection blocks, string text)
    {
        Paragraph p = new(new Run(text));
        blocks.Add(p);
        return p;
    }

    /// <summary>
    /// Adds a simple text block to the document.
    /// </summary>
    /// <param name="fd">Document in which to add the new text block.</param>
    /// <param name="text">Text to add.</param>
    /// <returns>
    /// <paramref name="fd"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static FlowDocument Text(this FlowDocument fd, string text)
    {
        return fd.Text(text, TextAlignment.Left);
    }

    /// <summary>
    /// Adds a simple text block to the document.
    /// </summary>
    /// <param name="fd">Document in which to add the new text block.</param>
    /// <param name="text">Text to add.</param>
    /// <param name="alignment">Text alignment to use.</param>
    /// <returns>
    /// <paramref name="fd"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static FlowDocument Text(this FlowDocument fd, string text, TextAlignment alignment)
    {
        fd.Blocks.Add(new Paragraph { Inlines = { new Run { Text = text } }, TextAlignment = alignment });
        return fd;
    }

    /// <summary>
    /// Adds a title-formatted text to the document.
    /// </summary>
    /// <param name="fd">Document in which to add the new text block.</param>
    /// <param name="text">Title text to add.</param>
    /// <returns>
    /// <paramref name="fd"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static FlowDocument Title(this FlowDocument fd, string text)
    {
        return fd.Title(text, 0, TextAlignment.Left);
    }

    /// <summary>
    /// Adds a title-formatted text to the document.
    /// </summary>
    /// <param name="fd">Document in which to add the new text block.</param>
    /// <param name="text">Title text to add.</param>
    /// <param name="level">Title level to add.</param>
    /// <returns>
    /// <paramref name="fd"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static FlowDocument Title(this FlowDocument fd, string text, byte level)
    {
        return fd.Title(text, level, TextAlignment.Left);
    }

    /// <summary>
    /// Adds a title-formatted text to the document.
    /// </summary>
    /// <param name="fd">Document in which to add the new text block.</param>
    /// <param name="text">Title text to add.</param>
    /// <param name="alignment">Text alignment to use.</param>
    /// <returns>
    /// <paramref name="fd"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static FlowDocument Title(this FlowDocument fd, string text, TextAlignment alignment)
    {
        return fd.Title(text, 0, alignment);
    }

    /// <summary>
    /// Adds a title-formatted text to the document.
    /// </summary>
    /// <param name="fd">Document in which to add the new text block.</param>
    /// <param name="text">Title text to add.</param>
    /// <param name="level">Title level to add.</param>
    /// <param name="alignment">Text alignment to use.</param>
    /// <returns>
    /// <paramref name="fd"/>, allowing the use of Fluent syntax.
    /// </returns>
    public static FlowDocument Title(this FlowDocument fd, string text, byte level, TextAlignment alignment)
    {
        fd.Blocks.Add(new Paragraph
        {
            Inlines =
            {
                new Run
                {
                    Text = text,
                    FontSize = 36 - (6 * level).Clamp(30)
                }
            },
            TextAlignment = alignment
        });
        return fd;
    }

    /// <summary>
    /// Generates a <see cref="Table" /> object from the current view of a
    /// <see cref="ListView" />.
    /// </summary>
    /// <param name="listView"><see cref="ListView" /> to process.</param>
    /// <returns>
    /// A <see cref="Table" /> with the content of the active view of the <see cref="ListView" />.
    /// </returns>
    public static Table ToDocumentTable(this ListView listView)
    {
        GridViewColumnCollection cols = (listView.View as GridView)?.Columns ?? throw Resources.Errors.InvalidValue(nameof(listView));
        Table table = new Table().ColumnWidths(cols.Select(p => p.Width));
        TableRowGroup data = table.AddGroup().AddRow(cols.Select(p => p.Header.ToString())!).CenterAll().Bold().Done()
            .AddGroup();
        foreach (object j in listView.Items)
        {
            TableRow row = data.AddRow();
            foreach (GridViewColumn k in ((GridView)listView.View).Columns)
            {
                if (k.DisplayMemberBinding is not Binding b) continue;
                object o = b.Path.Path.Split('.').Aggregate(j,
                    (current, i) => current?.GetType().GetProperty(i)?.GetMethod?.Invoke(j, Array.Empty<object>())!);
                if (o is not null) row.AddCell(o.ToString()!);
            }
        }
        return table;
    }
}
