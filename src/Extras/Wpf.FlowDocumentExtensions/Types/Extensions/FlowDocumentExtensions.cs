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
/// Extensiones de tipo Fluent para manipular objetos <see cref="FlowDocument" />
/// </summary>
public static partial class FlowDocumentExtensions
{
    /// <summary>
    /// Imprime un <see cref="FlowDocument"/> por medio del cuadro de
    /// diálogo de impresión del sistema operativo.
    /// </summary>
    /// <param name="fd">
    /// <see cref="FlowDocument"/> a imprimir.
    /// </param>
        /// <param name="title">
    /// Título del documento a imprimir.
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
    /// Imprime directamente un <see cref="FlowDocument"/> sin pasar
    /// por el cuadro de diálogo de impresión del sistema operativo.
    /// </summary>
    /// <param name="fd">
    /// <see cref="FlowDocument"/> a imprimir.
    /// </param>
    /// <param name="title">
    /// Título del documento a imprimir.
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
    /// Agrega una nueva tabla al <see cref="FlowDocument"/> especificado.
    /// </summary>
    /// <param name="document">
    /// Documento al cual agregar la nueva tabla.
    /// </param>
    /// <param name="columnWidths">
    /// Anchos de columna a establecer.
    /// </param>
    /// <returns>
    /// Una referencia a la nueva tabla creada.
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
    /// Agrega una nueva tabla al <see cref="FlowDocument"/> especificado.
    /// </summary>
    /// <param name="document">
    /// Documento al cual agregar la nueva tabla.
    /// </param>
    /// <param name="columns">
    /// Columnas a agregar.
    /// </param>
    /// <returns>
    /// Una referencia a la nueva tabla creada.
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
    /// marca el final del contexto de un elemento, devolviendo a su
    /// padre.
    /// </summary>
    /// <typeparam name="T">Tipo de padre a devolver.</typeparam>
    /// <param name="block">
    /// bloque del cual obtener al padre.
    /// </param>
    /// <returns>
    /// El padre del elemento especificado.
    /// </returns>
    /// <exception cref="InvalidCastException">
    /// Se produce si el padre del elemento no es del tipo
    /// <typeparamref name="T"/>.
    /// </exception>
    public static T Done<T>(this FrameworkContentElement block) where T : class
    {
        return block.Parent as T ?? throw new InvalidCastException();
    }

    /// <summary>
    /// Construye una tabla a partir de una enumeración de datos y una colección de descriptores de columnas.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de elementos a incluir en la tabla.
    /// </typeparam>
    /// <param name="fd">
    /// <see cref="FlowDocument"/> en el cual se agregará la tabla creada.
    /// </param>
    /// <param name="columns">
    /// Colección de columnas a incluir en la tabla.
    /// </param>
    /// <param name="data">
    /// Enumeración de datos a incluir en la tabla.
    /// </param>
    /// <returns>
    /// <paramref name="fd"/>, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static Table MakeTable<T>(this FlowDocument fd, IEnumerable<IColumnBuilder<T>> columns, IEnumerable<T> data) => fd.MakeTable(columns, data, null);

    /// <summary>
    /// Construye una tabla a partir de una enumeración de datos y una colección de descriptores de columnas.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de elementos a incluir en la tabla.
    /// </typeparam>
    /// <param name="fd">
    /// <see cref="FlowDocument"/> en el cual se agregará la tabla creada.
    /// </param>
    /// <param name="columns">
    /// Colección de columnas a incluir en la tabla.
    /// </param>
    /// <param name="data">
    /// Enumeración de datos a incluir en la tabla.
    /// </param>
    /// <param name="headersStyle">
    /// Estilo opcional a aplicar a los encabezados de la tabla.
    /// </param>
    /// <returns>
    /// <paramref name="fd"/>, lo que permite utilizar esta función
    /// con sintaxis Fluent.
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
    /// Construye una nueva tabla con los encabezados especificados.
    /// </summary>
    /// <param name="fd">
    /// <see cref="FlowDocument"/> en el cual se agregará la tabla creada.
    /// </param>
    /// <param name="headers">
    /// Encabezados a agregar a la tabla.
    /// </param>
    /// <returns>
    /// Una referencia a un nuevo <see cref="TableRowGroup"/> dentro de
    /// la tabla creada.
    /// </returns>
    public static TableRowGroup MakeTable(this FlowDocument fd, IEnumerable<string> headers) => fd.MakeTable(headers, null);

    /// <summary>
    /// Construye una nueva tabla con los encabezados especificados.
    /// </summary>
    /// <param name="fd">
    /// <see cref="FlowDocument"/> en el cual se agregará la tabla creada.
    /// </param>
    /// <param name="headers">
    /// Encabezados a agregar a la tabla.
    /// </param>
    /// <param name="headersStyle">
    /// Estilo opcional a aplicar a los encabezados de la tabla.
    /// </param>
    /// <returns>
    /// Una referencia a un nuevo <see cref="TableRowGroup"/> dentro de
    /// la tabla creada.
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
    /// Crea un nuevo párrafo.
    /// </summary>
    /// <param name="fd">
    /// <see cref="FlowDocument"/> en el cual se agregará el nuevo párrafo.
    /// </param>
    /// <returns>
    /// Una referencia al nuevo
    /// <see cref="System.Windows.Documents.Paragraph"/> que ha sido agregado
    /// al documento.
    /// </returns>
    public static Paragraph Paragraph(this FlowDocument fd)
    {
        Paragraph p = new();
        fd.Blocks.Add(p);
        return p;
    }

    /// <summary>
    /// Crea un nuevo párrafo.
    /// </summary>
    /// <param name="fd">
    /// <see cref="FlowDocument"/> en el cual se agregará el nuevo párrafo.
    /// </param>
    /// <param name="content">Contenido del nuevo párrafo</param>
    /// <returns>
    /// <paramref name="fd"/>, lo que permite el uso de sintaxis Fluent.
    /// </returns>
    public static FlowDocument Paragraph(this FlowDocument fd, string content)
    {
        return fd.Paragraph(content, TextAlignment.Left);
    }

    /// <summary>
    /// Crea un nuevo párrafo.
    /// </summary>
    /// <param name="fd">
    /// <see cref="FlowDocument"/> en el cual se agregará el nuevo párrafo.
    /// </param>
    /// <param name="content">Contenido del nuevo párrafo</param>
    /// <param name="alignment">Alineación de texto del nuevo párrafo.</param>
    /// <returns>
    /// <paramref name="fd"/>, lo que permite el uso de sintaxis Fluent.
    /// </returns>
    public static FlowDocument Paragraph(this FlowDocument fd, string content, TextAlignment alignment)
    {
        foreach (string text in content.Replace("\n\r", "\n").Split('\n'))
            fd.Blocks.Add(new Paragraph { Inlines = { new Run { Text = text } }, TextAlignment = alignment });
        return fd;
    }

    /// <summary>
    /// Agrega un bloque de texto al párrafo.
    /// </summary>
    /// <param name="paragraph">Párrafo en el cual agregar el nuevo texto.</param>
    /// <param name="text">Texto a agregar.</param>
    /// <returns>
    /// <paramref name="paragraph"/>, lo que permite el uso de sintaxis Fluent.
    /// </returns>
    public static Paragraph Run(this Paragraph paragraph, string text)
    {
        paragraph.Inlines.Add(new Run(text));
        return paragraph;
    }

    /// <summary>
    /// Establece la alineación del párrafo.
    /// </summary>
    /// <param name="paragraph">Párrafo al cual establecerle la alineación de texto.</param>
    /// <param name="alignment">Alineación de texto a utilizar en el párrafo.</param>
    /// <returns>
    /// <paramref name="paragraph"/>, lo que permite el uso de sintaxis Fluent.
    /// </returns>
    public static Paragraph Style(this Paragraph paragraph, TextAlignment alignment)
    {
        paragraph.TextAlignment = alignment;
        return paragraph;
    }

    /// <summary>
    /// Establece el peso de la fuente del párrafo.
    /// </summary>
    /// <param name="paragraph">Párrafo al cual establecerle el peso de la fuente.</param>
    /// <param name="weight">Peso de la fuente en el párrafo.</param>
    /// <returns>
    /// <paramref name="paragraph"/>, lo que permite el uso de sintaxis Fluent.
    /// </returns>
    public static Paragraph Style(this Paragraph paragraph, FontWeight weight)
    {
        paragraph.FontWeight = weight;
        return paragraph;
    }

    /// <summary>
    /// Establece los valores de alineación y peso de la fuente al párrafo.
    /// </summary>
    /// <param name="paragraph">Párrafo al cual aplicar los parámetros de estilo.</param>
    /// <param name="alignment">Alineación de texto a utilizar en el párrafo.</param>
    /// <param name="weight">Peso de la fuente en el párrafo.</param>
    /// <returns>
    /// <paramref name="paragraph"/>, lo que permite el uso de sintaxis Fluent.
    /// </returns>
    public static Paragraph Style(this Paragraph paragraph, TextAlignment alignment, FontWeight weight)
    {
        paragraph.TextAlignment = alignment;
        paragraph.FontWeight = weight;
        return paragraph;
    }

    /// <summary>
    /// Agrega un bloque de texto al conjunto de bloques de texto especificados.
    /// </summary>
    /// <param name="blocks">Colección de bloques de texto en el cual agregar un nuevo bloque de texto.</param>
    /// <param name="text">Texto a agregar.</param>
    /// <returns>
    /// Una referencia al nuevo
    /// <see cref="System.Windows.Documents.Paragraph"/> que ha sido agregado
    /// a la colección de bloques de texto.
    /// </returns>
    public static Paragraph Text(this BlockCollection blocks, string text)
    {
        Paragraph p = new(new Run(text));
        blocks.Add(p);
        return p;
    }

    /// <summary>
    /// Agrega un bloque de texto simple al documento.
    /// </summary>
    /// <param name="fd">Documento en el cual agregar el nuevo bloque de texto.</param>
    /// <param name="text">Texto a agregar.</param>
    /// <returns>
    /// <paramref name="fd"/>, lo que permite el uso de sintaxis Fluent.
    /// </returns>
    public static FlowDocument Text(this FlowDocument fd, string text)
    {
        return fd.Text(text, TextAlignment.Left);
    }

    /// <summary>
    /// Agrega un bloque de texto simple al documento.
    /// </summary>
    /// <param name="fd">Documento en el cual agregar el nuevo bloque de texto.</param>
    /// <param name="text">Texto a agregar.</param>
    /// <param name="alignment">Alineación del texto a utilizar.</param>
    /// <returns>
    /// <paramref name="fd"/>, lo que permite el uso de sintaxis Fluent.
    /// </returns>
    public static FlowDocument Text(this FlowDocument fd, string text, TextAlignment alignment)
    {
        fd.Blocks.Add(new Paragraph { Inlines = { new Run { Text = text } }, TextAlignment = alignment });
        return fd;
    }

    /// <summary>
    /// Agrega un texto con formato de título al documento.
    /// </summary>
    /// <param name="fd">Documento en el cual agregar el nuevo bloque de texto.</param>
    /// <param name="text">Texto de título a agregar.</param>
    /// <returns>
    /// <paramref name="fd"/>, lo que permite el uso de sintaxis Fluent.
    /// </returns>
    public static FlowDocument Title(this FlowDocument fd, string text)
    {
        return fd.Title(text, 0, TextAlignment.Left);
    }

    /// <summary>
    /// Agrega un texto con formato de título al documento.
    /// </summary>
    /// <param name="fd">Documento en el cual agregar el nuevo bloque de texto.</param>
    /// <param name="text">Texto de título a agregar.</param>
    /// <param name="level">Nivel de título a agregar.</param>
    /// <returns>
    /// <paramref name="fd"/>, lo que permite el uso de sintaxis Fluent.
    /// </returns>
    public static FlowDocument Title(this FlowDocument fd, string text, byte level)
    {
        return fd.Title(text, level, TextAlignment.Left);
    }

    /// <summary>
    /// Agrega un texto con formato de título al documento.
    /// </summary>
    /// <param name="fd">Documento en el cual agregar el nuevo bloque de texto.</param>
    /// <param name="text">Texto de título a agregar.</param>
    /// <param name="alignment">Alineación de texto a utilizar</param>
    /// <returns>
    /// <paramref name="fd"/>, lo que permite el uso de sintaxis Fluent.
    /// </returns>
    public static FlowDocument Title(this FlowDocument fd, string text, TextAlignment alignment)
    {
        return fd.Title(text, 0, alignment);
    }

    /// <summary>
    /// Agrega un texto con formato de título al documento.
    /// </summary>
    /// <param name="fd">Documento en el cual agregar el nuevo bloque de texto.</param>
    /// <param name="text">Texto de título a agregar.</param>
    /// <param name="level">Nivel de título a agregar.</param>
    /// <param name="alignment">Alineación de texto a utilizar</param>
    /// <returns>
    /// <paramref name="fd"/>, lo que permite el uso de sintaxis Fluent.
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
    /// Genera un objeto <see cref="Table" /> a partir de la vista actual de un
    /// <see cref="ListView" />.
    /// </summary>
    /// <param name="listView"><see cref="ListView" /> a procesar.</param>
    /// <returns>
    /// Un <see cref="Table" /> con el contenido de la vista activa del <see cref="ListView" />.
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
