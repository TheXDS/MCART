/*
FlowDocumentExtensions.cs

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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using TheXDS.MCART.Types.Extensions;

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
        System.Windows.Size sz = new(dialog.PrintableAreaWidth, dialog.PrintableAreaHeight);

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
        System.Windows.Size sz = new(dialog.PrintableAreaWidth, dialog.PrintableAreaHeight);
        DocumentPaginator? paginator = (fd as IDocumentPaginatorSource).DocumentPaginator;
        paginator.PageSize = sz;
        dialog.PrintDocument(paginator, title);
    }

    /// <summary>
    /// Agrega una celda vacía de tabla a la fila actual.
    /// </summary>
    /// <param name="row">
    /// Fila de tabla en la cual se agregará la celda.
    /// </param>
    /// <returns>
    /// La celda que ha sido agregada.
    /// </returns>
    public static TableCell AddCell(this TableRow row)
    {
        TableCell? c = new();
        row.Cells.Add(c);
        return c;
    }

    /// <summary>
    /// Agrega una celda vacía de tabla a la fila actual.
    /// </summary>
    /// <param name="row">
    /// Fila de tabla en la cual se agregará la celda.
    /// </param>
    /// <param name="columnSpan">
    /// Cantidad de columnas que la nueva celda podrá ocupar.
    /// </param>
    /// <returns>
    /// La celda que ha sido agregada.
    /// </returns>
    public static TableCell AddCell(this TableRow row, int columnSpan)
    {
        TableCell? c = new() { ColumnSpan = columnSpan };
        row.Cells.Add(c);
        return c;
    }

    /// <summary>
    /// Agrega una celda vacía de tabla a la fila actual.
    /// </summary>
    /// <param name="row">
    /// Fila de tabla en la cual se agregará la celda.
    /// </param>
    /// <param name="rowSpan">
    /// Cantidad de filas que la nueva celda podrá ocupar.
    /// </param>
    /// <param name="columnSpan">
    /// Cantidad de columnas que la nueva celda podrá ocupar.
    /// </param>
    /// <returns>
    /// La celda que ha sido agregada.
    /// </returns>
    public static TableCell AddCell(this TableRow row, int rowSpan, int columnSpan)
    {
        TableCell? c = new() { RowSpan = rowSpan, ColumnSpan = columnSpan };
        row.Cells.Add(c);
        return c;
    }

    /// <summary>
    /// Agrega una celda simple con texto a una fila.
    /// </summary>
    /// <param name="row">Fila a la cual agregar la nueva celda.</param>
    /// <param name="text">Texto de la celda.</param>
    /// <returns>
    /// <paramref name="row" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TableCell AddCell(this TableRow row, string text)
    {
        return row.AddCell(text, FontWeights.Normal);
    }

    /// <summary>
    /// Agrega una celda simple con texto a una fila.
    /// </summary>
    /// <param name="row">Fila a la cual agregar la nueva celda.</param>
    /// <param name="text">Texto de la celda.</param>
    /// <param name="alignment">
    /// Alineación horizontal de texto dentro de la celda.
    /// </param>
    /// <returns>
    /// <paramref name="row" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TableCell AddCell(this TableRow row, string text, TextAlignment alignment)
    {
        return row.Cells.Push(new TableCell(new Paragraph(new Run(text)) { TextAlignment = alignment }));
    }

    /// <summary>
    /// Agrega una celda simple con texto a una fila.
    /// </summary>
    /// <param name="row">Fila a la cual agregar la nueva celda.</param>
    /// <param name="text">Texto de la celda.</param>
    /// <param name="weight">
    /// Densidad de la fuente a utilizar dentro de la celda.
    /// </param>
    /// <returns>
    /// <paramref name="row" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TableCell AddCell(this TableRow row, string text, FontWeight weight)
    {
        return row.Cells.Push(new TableCell(new Paragraph(new Run(text) { FontWeight = weight })));
    }

    /// <summary>
    /// Agrega una celda simple con texto a una fila.
    /// </summary>
    /// <param name="row">Fila a la cual agregar la nueva celda.</param>
    /// <param name="text">Texto de la celda.</param>
    /// <param name="alignment">
    /// Alineación horizontal de texto dentro de la celda.
    /// </param>
    /// <param name="weight">
    /// Densidad de la fuente a utilizar dentro de la celda.
    /// </param>
    /// <returns>
    /// <paramref name="row" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TableCell AddCell(this TableRow row, string text, TextAlignment alignment, FontWeight weight)
    {
        return row.Cells.Push(new TableCell(new Paragraph(new Run(text) { FontWeight = weight }) { TextAlignment = alignment }));
    }

    /// <summary>
    /// Agrega un nuevo grupo de filas a la tabla.
    /// </summary>
    /// <param name="table">
    /// Tabla a la cual agregar el nuevo grupo de filas.
    /// </param>
    /// <returns>
    /// El nuevo grupo de filas que ha sido añadido a la tabla.
    /// </returns>
    public static TableRowGroup AddGroup(this Table table)
    {
        TableRowGroup? rowGroup = new();
        table.RowGroups.Add(rowGroup);
        return rowGroup;
    }

    /// <summary>
    /// Agrega un nuevo grupo de filas a la tabla.
    /// </summary>
    /// <param name="table">
    /// Tabla a la cual agregar el nuevo grupo de filas.
    /// </param>
    /// <param name="newGroup">
    /// Nueva instancia del grupo de filas a agregar a la tabla.
    /// </param>
    /// <returns>
    /// <paramref name="newGroup" />, lo que permite utilizar esta
    /// función con sintaxis Fluent.
    /// </returns>
    public static TableRowGroup AddGroup(this Table table, TableRowGroup newGroup)
    {
        table.RowGroups.Add(newGroup);
        return newGroup;
    }

    /// <summary>
    /// Agrega una nueva fila al grupo de filas.
    /// </summary>
    /// <param name="group">
    /// Grupo de filas al cual agregar la nueva fila.
    /// </param>
    /// <returns>
    /// Una referencia a la nueva fila creada dentro del grupo de filas
    /// de la tabla.
    /// </returns>
    public static TableRow AddRow(this TableRowGroup group)
    {
        TableRow? row = new();
        group.Rows.Add(row);
        return row;
    }

    /// <summary>
    /// Agrega una nueva fila al grupo de filas.
    /// </summary>
    /// <param name="rg">
    /// Grupo de filas al cual agregar la nueva fila.
    /// </param>
    /// <param name="values">Valores a agregar a la fila.</param>
    /// <returns>
    /// Una referencia a la nueva fila creada dentro del grupo de filas
    /// de la tabla.
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
    /// Agrega una nueva fila a la tabla.
    /// </summary>
    /// <param name="tbl">
    /// Tabla a la cual agregar la nueva fila.
    /// </param>
    /// <param name="values">Valores a agregar a la fila.</param>
    /// <returns>
    /// Una referencia a la nueva fila creada dentro de un nuevo grupo
    /// de filas de la tabla.
    /// </returns>
    public static TableRow AddRow(this Table tbl, IEnumerable<string> values)
    {
        List<string>? lst = values.ToList();

        if (lst.Count > tbl.Columns.Count) throw new ArgumentOutOfRangeException();

        TableRowGroup? rg = new();
        TableRow? row = new();
        foreach (string? j in lst) row.Cells.Add(new TableCell(new Paragraph(new Run(j))));
        rg.Rows.Add(row);

        tbl.RowGroups.Add(rg);

        return row;
    }

    /// <summary>
    /// Agrega una nueva fila al grupo de filas.
    /// </summary>
    /// <param name="rg">
    /// Grupo de filas al cual agregar la nueva fila.
    /// </param>
    /// <param name="values">Valores a agregar a la fila.</param>
    /// <returns>
    /// Una referencia a la nueva fila creada dentro del grupo de filas
    /// de la tabla.
    /// </returns>
    public static TableRowGroup AddRow(this TableRowGroup rg, params string[] values)
    {
        return rg.AddRow(values.ToList());
    }

    /// <summary>
    /// Agrega una nueva fila al grupo de filas.
    /// </summary>
    /// <param name="rg">
    /// Grupo de filas al cual agregar la nueva fila.
    /// </param>
    /// <param name="cells">Celdas a agregar a la fila.</param>
    /// <returns>
    /// Una referencia a la nueva fila creada dentro del grupo de filas
    /// de la tabla.
    /// </returns>
    public static TableRowGroup AddRow(this TableRowGroup rg, IEnumerable<TableCell> cells)
    {
        TableRow? tr = new();
        foreach (TableCell? j in cells) tr.Cells.Add(j);

        rg.Rows.Add(tr);
        return rg;
    }

    /// <summary>
    /// Agrega una nueva fila al grupo de filas.
    /// </summary>
    /// <param name="rg">
    /// Grupo de filas al cual agregar la nueva fila.
    /// </param>
    /// <param name="cells">Celdas a agregar a la fila.</param>
    /// <returns>
    /// Una referencia a la nueva fila creada dentro del grupo de filas
    /// de la tabla.
    /// </returns>
    public static TableRowGroup AddRow(this TableRowGroup rg, params TableCell[] cells)
    {
        return rg.AddRow(cells.ToList());
    }

    /// <summary>
    /// Agrega una nueva fila al grupo de filas.
    /// </summary>
    /// <param name="rg">
    /// Grupo de filas al cual agregar la nueva fila.
    /// </param>
    /// <param name="values">Valores a agregar a la fila.</param>
    /// <returns>
    /// Una referencia a la nueva fila creada dentro del grupo de filas
    /// de la tabla.
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
    /// Agrega una nueva fila al grupo de filas.
    /// </summary>
    /// <param name="rg">
    /// Grupo de filas al cual agregar la nueva fila.
    /// </param>
    /// <param name="values">Valores a agregar a la fila.</param>
    /// <returns>
    /// Una referencia a la nueva fila creada dentro del grupo de filas
    /// de la tabla.
    /// </returns>
    public static TableRowGroup AddRow(this TableRowGroup rg, params Block[] values)
    {
        return rg.AddRow(values.ToList());
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
    /// Establece un borde para una celda de una tabla.
    /// </summary>
    /// <param name="element">Celda a procesar.</param>
    /// <param name="brush"><see cref="Brush" /> a utilizar para dibujar el borde.</param>
    /// <param name="thickness">Grosor del borde.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TableCell Border(this TableCell element, Brush brush, Thickness thickness)
    {
        element.BorderBrush = brush;
        element.BorderThickness = thickness;
        return element;
    }

    /// <summary>
    /// Establece un borde para todas las celdas de un <see cref="TableRow" />.
    /// </summary>
    /// <param name="element"><see cref="TableRow" /> a procesar.</param>
    /// <param name="brush"><see cref="Brush" /> a utilizar para dibujar el borde.</param>
    /// <param name="thickness">Grosor del borde.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TableRow Borders(this TableRow element, Brush brush, Thickness thickness)
    {
        foreach (TableCell? j in element.Cells)
            j.Border(brush, thickness);
        return element;
    }

    /// <summary>
    /// Establece un borde para todas las celdas de un <see cref="TableRowGroup" />.
    /// </summary>
    /// <param name="element"><see cref="TableRowGroup" /> a procesar.</param>
    /// <param name="brush"><see cref="Brush" /> a utilizar para dibujar el borde.</param>
    /// <param name="thickness">Grosor del borde.</param>
    /// <returns>
    /// <paramref name="element" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TableRowGroup Borders(this TableRowGroup element, Brush brush, Thickness thickness)
    {
        foreach (TableCell? j in element.Rows.SelectMany(p => p.Cells))
            j.Border(brush, thickness);
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
    /// Centra todos los bloques de contenido de una fila.
    /// </summary>
    /// <param name="row">
    /// Fila a la cual se debe aplicar la operación.
    /// </param>
    /// <returns>
    /// <paramref name="row" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TableRow CenterAll(this TableRow row)
    {
        foreach (Block? j in row.Cells.SelectMany(p => p.Blocks)) j.Center();
        return row;
    }

    /// <summary>
    /// Centra todos los bloques de contenido de un grupo de filas.
    /// </summary>
    /// <param name="rowGroup">
    /// Grupo de filas a la cual se debe aplicar la operación.
    /// </param>
    /// <returns>
    /// <paramref name="rowGroup" />, lo que permite utilizar esta
    /// función con sintaxis Fluent.
    /// </returns>
    public static TableRowGroup CenterAll(this TableRowGroup rowGroup)
    {
        foreach (Block? j in rowGroup.Rows.SelectMany(o => o.Cells.SelectMany(p => p.Blocks))) j.Center();
        return rowGroup;
    }

    /// <summary>
    /// Establece un color de fondo para la columna especificada de la tabla.
    /// </summary>
    /// <param name="table"><see cref="Table" /> a procesar.</param>
    /// <param name="column">Índice de la columna.</param>
    /// <param name="brush"><see cref="Brush" /> a aplicar al dibujar la tabla.</param>
    /// <returns>
    /// <paramref name="table" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static Table ColumnBackground(this Table table, int column, Brush brush)
    {
        table.Columns[column].Background = brush;
        return table;
    }

    /// <summary>
    /// Establece un valor de combinación de columnas a la celda
    /// especificada.
    /// </summary>
    /// <param name="cell">
    /// Celda a combinar.
    /// </param>
    /// <param name="span">
    /// Cantidad de columnas que la celda abarca.
    /// </param>
    /// <returns>
    /// <paramref name="cell" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TableCell ColumnSpan(this TableCell cell, int span)
    {
        cell.ColumnSpan = span;
        return cell;
    }

    /// <summary>
    /// Establece el ancho de una columna para un <see cref="Table" />.
    /// </summary>
    /// <param name="table"><see cref="Table" /> a procesar.</param>
    /// <param name="column">Índice de la columna.</param>
    /// <param name="width">Ancho de columna a aplicar.</param>
    /// <returns>
    /// <paramref name="table" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static Table ColumnWidth(this Table table, int column, GridLength width)
    {
        table.Columns[column].Width = width;
        return table;
    }

    /// <summary>
    /// Establece los anchos de columna para un <see cref="Table" />.
    /// </summary>
    /// <param name="table"><see cref="Table" /> a procesar.</param>
    /// <param name="lengths">Anchos de columna a aplicar.</param>
    /// <returns>
    /// <paramref name="table" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static Table ColumnWidths(this Table table, IEnumerable<double> lengths)
    {
        return table.ColumnWidths(lengths.Select(p => new GridLength(p)));
    }

    /// <summary>
    /// Establece los anchos de columna para un <see cref="Table" />.
    /// </summary>
    /// <param name="table"><see cref="Table" /> a procesar.</param>
    /// <param name="lengths">Anchos de columna a aplicar.</param>
    /// <returns>
    /// <paramref name="table" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
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
    /// Marca el final del contexto de una fila de tabla, devolviendo a
    /// su grupo padre de forma compatible con la sintaxis Fluent.
    /// </summary>
    /// <param name="row">
    /// Fila para la cual finalizar el contexto de la sintaxis Fluent.
    /// </param>
    /// <returns>
    /// El grupo de filas al que esta fila pertenece.
    /// </returns>
    public static TableRowGroup Done(this TableRow row)
    {
        return (TableRowGroup)row.Parent;
    }

    /// <summary>
    /// Marca el final del contexto de un grupo de filas de tabla,
    /// devolviendo a su tabla padre de forma compatible con la
    /// sintaxis Fluent.
    /// </summary>
    /// <param name="rowGroup">
    /// Grupo de filas para el cual finalizar el contexto de la
    /// sintaxis Fluent.
    /// </param>
    /// <returns>
    /// La tabla a las que este grupo de filas pertenece.
    /// </returns>
    public static Table Done(this TableRowGroup rowGroup)
    {
        return (Table)rowGroup.Parent;
    }

    /// <summary>
    /// Marca el final del contexto de una celda, devolviendo a su fila
    /// padre de forma compatible con la sintaxis Fluent.
    /// </summary>
    /// <param name="cell">
    /// Celda para la cual finalizar el contexto de la sintaxis Fluent.
    /// </param>
    /// <returns>
    /// La fila a la que esta celda pertenece.
    /// </returns>
    public static TableRow Done(this TableCell cell)
    {
        return (TableRow)cell.Parent;
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
    /// Aplica un estilo a una celda.
    /// </summary>
    /// <param name="cell">Celda a estilizar.</param>
    /// <param name="style">Estilo a aplicar a la celda.</param>
    /// <returns>
    /// <paramref name="cell" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TableCell ApplyStyle(this TableCell cell, ICellStyle? style)
    {
        return cell.ApplyStyle(style, false);
    }

    /// <summary>
    /// Aplica un estilo a una celda.
    /// </summary>
    /// <param name="cell">Celda a estilizar.</param>
    /// <param name="style">Estilo a aplicar a la celda.</param>
    /// <param name="odd">
    /// Bandera que indica si se trata de una fila impar o no.
    /// </param>
    /// <returns>
    /// <paramref name="cell" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TableCell ApplyStyle(this TableCell cell, ICellStyle? style, bool odd)
    {
        if (style != null)
        {
            if (style.Background != null)
                cell.Background = (odd ? style.OddBackground : null) ?? style.Background;
            if (style.Foreground != null)
                cell.Foreground = style.Foreground;
            if (style.BorderBrush != null)
                cell.BorderBrush = style.BorderBrush;
            if (style.BorderThickness != null)
                cell.BorderThickness = style.BorderThickness.Value;

            cell.TextAlignment = style.Alignment;
        }
        return cell;
    }

    /// <summary>
    /// Agrega una nueva celda con el contenido textual especificado.
    /// </summary>
    /// <param name="cells">
    /// Colección de celdas en la cual agregar una nueva celda con el
    /// contenido especificado.
    /// </param>
    /// <param name="content">
    /// Contenido textual a incluir en la celda.
    /// </param>
    /// <returns>
    /// Una referencia a la nueva celda creada.
    /// </returns>
    public static TableCell Add(this TableCellCollection cells, string content)
    {
        return cells.Add(new Run(content));
    }

    /// <summary>
    /// Agrega una nueva celda con el contenido especificado.
    /// </summary>
    /// <param name="cells">
    /// Colección de celdas en la cual agregar una nueva celda con el
    /// contenido especificado.
    /// </param>
    /// <param name="content">
    /// Contenido a incluir en la celda.
    /// </param>
    /// <returns>
    /// Una referencia a la nueva celda creada.
    /// </returns>
    public static TableCell Add(this TableCellCollection cells, Inline content)
    {
        TableCell? c = new(new Paragraph(content));
        cells.Add(c);
        return c;
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

#nullable disable

#pragma warning disable CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente

    public static Paragraph Paragraph(this FlowDocument fd)
    {
        Paragraph p = new();
        fd.Blocks.Add(p);
        return p;
    }

    public static FlowDocument Paragraph(this FlowDocument fd, string content)
    {
        return fd.Paragraph(content, TextAlignment.Left);
    }

    public static FlowDocument Paragraph(this FlowDocument fd, string content, TextAlignment alignment)
    {
        foreach (string text in content.Replace("\n\r", "\n").Split('\n'))
            fd.Blocks.Add(new Paragraph { Inlines = { new Run { Text = text } }, TextAlignment = alignment });
        return fd;
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

    public static TableCell RowSpan(this TableCell cell, int span)
    {
        cell.RowSpan = span;
        return cell;
    }

    public static Paragraph Run(this Paragraph paragraph, string text)
    {
        paragraph.Inlines.Add(new Run(text));
        return paragraph;
    }

    public static Paragraph Style(this Paragraph paragraph, TextAlignment alignment)
    {
        paragraph.TextAlignment = alignment;
        return paragraph;
    }

    public static Paragraph Style(this Paragraph paragraph, FontWeight weight)
    {
        paragraph.FontWeight = weight;
        return paragraph;
    }

    public static Paragraph Style(this Paragraph paragraph, TextAlignment alignment, FontWeight weight)
    {
        paragraph.TextAlignment = alignment;
        paragraph.FontWeight = weight;
        return paragraph;
    }

    public static Paragraph Text(this BlockCollection blocks, string text)
    {
        Paragraph p = new(new Run(text));
        blocks.Add(p);
        return p;
    }

    public static Paragraph Text(this TableCell cell, string text)
    {
        Paragraph p = new(new Run(text));
        cell.Blocks.Add(p);
        return p;
    }

    public static FlowDocument Text(this FlowDocument fd, string text)
    {
        return fd.Text(text, TextAlignment.Left);
    }

    public static FlowDocument Text(this FlowDocument fd, string text, TextAlignment alignment)
    {
        fd.Blocks.Add(new Paragraph { Inlines = { new Run { Text = text } }, TextAlignment = alignment });
        return fd;
    }

    public static FlowDocument Title(this FlowDocument fd, string text)
    {
        return fd.Title(text, 0, TextAlignment.Left);
    }

    public static FlowDocument Title(this FlowDocument fd, string text, byte level)
    {
        return fd.Title(text, level, TextAlignment.Left);
    }

    public static FlowDocument Title(this FlowDocument fd, string text, TextAlignment alignment)
    {
        return fd.Title(text, 0, alignment);
    }

    public static FlowDocument Title(this FlowDocument fd, string text, byte level, TextAlignment alignment)
    {
        fd.Blocks.Add(new Paragraph
        {
            Inlines =
            {
                new Run
                {
                    Text = text,
                    FontSize = 36 - (6 * level)
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
        GridViewColumnCollection cols = (listView.View as GridView)?.Columns ?? throw new ArgumentException();
        Table table = new Table().ColumnWidths(cols.Select(p => p.Width));
        TableRowGroup data = table.AddGroup().AddRow(cols.Select(p => p.Header.ToString())).CenterAll().Bold().Done()
            .AddGroup();
        foreach (object j in listView.Items)
        {
            TableRow row = data.AddRow();
            foreach (GridViewColumn k in ((GridView)listView.View).Columns)
            {
                if (k.DisplayMemberBinding is not Binding b) continue;
                object o = b.Path.Path.Split('.').Aggregate(j,
                    (current, i) => current?.GetType().GetProperty(i)?.GetMethod?.Invoke(j, Array.Empty<object>()));
                if (o is not null) row.AddCell(o.ToString());
            }
        }

        return table;
    }

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

    public static TElement TypographyFontNumeralAlignment<TElement>(this TElement element,
        FontNumeralAlignment value) where TElement : TextElement
    {
        element.Typography.NumeralAlignment = value;
        return element;
    }

    public static TElement TypographyFontNumeralStyle<TElement>(this TElement element, FontNumeralStyle value)
        where TElement : TextElement
    {
        element.Typography.NumeralStyle = value;
        return element;
    }

    public static TElement TypographyFraction<TElement>(this TElement element, FontFraction value)
        where TElement : TextElement
    {
        element.Typography.Fraction = value;
        return element;
    }

    public static TElement TypographyHistoricalForms<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.HistoricalForms = value;
        return element;
    }

    public static TElement TypographyHistoricalLigatures<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.HistoricalLigatures = value;
        return element;
    }

    public static TElement TypographyKerning<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.Kerning = value;
        return element;
    }

    public static TElement TypographyMathematicalGreek<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.MathematicalGreek = value;
        return element;
    }

    public static TElement TypographySlashedZero<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.SlashedZero = value;
        return element;
    }

    public static TElement TypographyStandardLigatures<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StandardLigatures = value;
        return element;
    }

    public static TElement TypographyStandardSwashes<TElement>(this TElement element, int value)
        where TElement : TextElement
    {
        element.Typography.StandardSwashes = value;
        return element;
    }

    public static TElement TypographyStylisticAlternates<TElement>(this TElement element, int value)
        where TElement : TextElement
    {
        element.Typography.StylisticAlternates = value;
        return element;
    }

    public static TElement TypographyStylisticSet1<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet1 = value;
        return element;
    }

    public static TElement TypographyStylisticSet10<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet10 = value;
        return element;
    }

    public static TElement TypographyStylisticSet11<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet11 = value;
        return element;
    }

    public static TElement TypographyStylisticSet12<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet12 = value;
        return element;
    }

    public static TElement TypographyStylisticSet13<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet13 = value;
        return element;
    }

    public static TElement TypographyStylisticSet14<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet14 = value;
        return element;
    }

    public static TElement TypographyStylisticSet15<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet15 = value;
        return element;
    }

    public static TElement TypographyStylisticSet16<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet16 = value;
        return element;
    }

    public static TElement TypographyStylisticSet17<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet17 = value;
        return element;
    }

    public static TElement TypographyStylisticSet18<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet18 = value;
        return element;
    }

    public static TElement TypographyStylisticSet19<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet19 = value;
        return element;
    }

    public static TElement TypographyStylisticSet2<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet2 = value;
        return element;
    }

    public static TElement TypographyStylisticSet20<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet20 = value;
        return element;
    }

    public static TElement TypographyStylisticSet3<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet3 = value;
        return element;
    }

    public static TElement TypographyStylisticSet4<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet4 = value;
        return element;
    }

    public static TElement TypographyStylisticSet5<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet5 = value;
        return element;
    }

    public static TElement TypographyStylisticSet6<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet6 = value;
        return element;
    }

    public static TElement TypographyStylisticSet7<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet7 = value;
        return element;
    }

    public static TElement TypographyStylisticSet8<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet8 = value;
        return element;
    }

    public static TElement TypographyStylisticSet9<TElement>(this TElement element, bool value)
        where TElement : TextElement
    {
        element.Typography.StylisticSet9 = value;
        return element;
    }

    public static TElement TypographyVariants<TElement>(this TElement element, FontVariants value)
        where TElement : TextElement
    {
        element.Typography.Variants = value;
        return element;
    }
}