/*
FlowDocumentTableExtensions.cs

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
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.FlowDocumentExtensions.Types.Extensions;

/// <summary>
/// Extensiones de tipo Fluent para manipular objetos
/// <see cref="FlowDocument" /> centradas en la creación de tablas.
/// </summary>
public static class FlowDocumentTableExtensions
{
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

        if (lst.Count > tbl.Columns.Count) throw new ArgumentOutOfRangeException(nameof(values));

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
    /// Indica que una celda ocupa un determinado número de filas.
    /// </summary>
    /// <param name="cell">Celda a procesar.</param>
    /// <param name="span">Número de filas que esta celda debe ocupar.</param>
    /// <returns>
    /// <paramref name="cell" />, lo que permite utilizar esta función
    /// con sintaxis Fluent.
    /// </returns>
    public static TableCell RowSpan(this TableCell cell, int span)
    {
        cell.RowSpan = span;
        return cell;
    }

    /// <summary>
    /// Establece el contenido de la celda al texto especificado.
    /// </summary>
    /// <param name="cell">Celda a procesar.</param>
    /// <param name="text">Texo de la celda.</param>
    /// <returns>
    /// Un elemento <see cref="Paragraph"/> que representa el contenido textual
    /// de la celda.
    /// </returns>
    public static Paragraph Text(this TableCell cell, string text)
    {
        Paragraph p = new(new Run(text));
        cell.Blocks.Add(p);
        return p;
    }
}
