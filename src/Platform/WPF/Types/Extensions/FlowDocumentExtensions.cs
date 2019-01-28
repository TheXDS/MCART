/*
FlowDocumentExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

#region Configuración de ReSharper

// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

#endregion

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Extensiones de tipo Fluent para manipular objetos <see cref="FlowDocument" />
    /// </summary>
    public static class FlowDocumentExtensions
    {
        /// <summary>
        ///     Imprime un <see cref="FlowDocument"/> por medio del cuadro de
        ///     diálogo de impresión del sistema operativo.
        /// </summary>
        /// <param name="fd">
        ///     <see cref="FlowDocument"/> a imprimir.
        /// </param>
		/// <param name="title">
        ///     Título del documento a imprimir.
        /// </param>
        public static void Print(this FlowDocument fd, string title)
        {
            var dialog = new PrintDialog();
            if (!dialog.ShowDialog() ?? true) return;
            var sz = new Size(dialog.PrintableAreaWidth, dialog.PrintableAreaHeight);

            var paginator = (fd as IDocumentPaginatorSource).DocumentPaginator;
            paginator.PageSize = sz;
            dialog.PrintDocument(paginator, title);
        }

        public class TableSelector<T>
        {
            public string Header { get; }
            public Func<T, string> Selector { get; }

            public TableSelector(string header, Func<T, string> selector)
            {
                Header = header;
                Selector = selector;
            }
        }

        /// <summary>
        ///     Agrega una celda vacía de tabla a la fila actual.
        /// </summary>
        /// <param name="row">
        ///     Fila de tabla en la cual se agregará la celda.
        /// </param>
        /// <returns>
        ///     La celda que ha sido agregada.
        /// </returns>
        public static TableCell AddCell(this TableRow row)
        {
            var c = new TableCell();
            row.Cells.Add(c);
            return c;
        }

        /// <summary>
        ///     Agrega una celda vacía de tabla a la fila actual.
        /// </summary>
        /// <param name="row">
        ///     Fila de tabla en la cual se agregará la celda.
        /// </param>
        /// <param name="columnSpan">
        ///     Cantidad de columnas que la nueva celda podrá ocupar.
        /// </param>
        /// <returns>
        ///     La celda que ha sido agregada.
        /// </returns>
        public static TableCell AddCell(this TableRow row, int columnSpan)
        {
            var c = new TableCell {ColumnSpan = columnSpan};
            row.Cells.Add(c);
            return c;
        }

        /// <summary>
        ///     Agrega una celda vacía de tabla a la fila actual.
        /// </summary>
        /// <param name="row">
        ///     Fila de tabla en la cual se agregará la celda.
        /// </param>
        /// <param name="rowSpan">
        ///     Cantidad de filas que la nueva celda podrá ocupar.
        /// </param>
        /// <param name="columnSpan">
        ///     Cantidad de columnas que la nueva celda podrá ocupar.
        /// </param>
        /// <returns>
        ///     La celda que ha sido agregada.
        /// </returns>
        public static TableCell AddCell(this TableRow row, int rowSpan, int columnSpan)
        {
            var c = new TableCell {RowSpan = rowSpan, ColumnSpan = columnSpan};
            row.Cells.Add(c);
            return c;
        }

        /// <summary>
        ///     Agrega una celda simple con texto a una fila.
        /// </summary>
        /// <param name="row">Fila a la cual agregar la nueva celda.</param>
        /// <param name="text">Texto de la celda.</param>
        /// <returns>
        ///     <paramref name="row" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TableRow AddCell(this TableRow row, string text)
        {
            return AddCell(row, text, FontWeights.Normal);
        }

        /// <summary>
        ///     Agrega una celda simple con texto a una fila.
        /// </summary>
        /// <param name="row">Fila a la cual agregar la nueva celda.</param>
        /// <param name="text">Texto de la celda.</param>
        /// <param name="weight">
        ///     Densidad de la fuente a utilizar dentro de la celda.
        /// </param>
        /// <returns>
        ///     <paramref name="row" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TableRow AddCell(this TableRow row, string text, FontWeight weight)
        {
            row.Cells.Add(new TableCell(new Paragraph(new Run(text) {FontWeight = weight})));
            return row;
        }

        /// <summary>
        ///     Agrega un nuevo grupo de filas a la tabla.
        /// </summary>
        /// <param name="table">
        ///     Tabla a la cual agregar el nuevo grupo de filas.
        /// </param>
        /// <returns>
        ///     El nuevo grupo de filas que ha sido añadido a la tabla.
        /// </returns>
        public static TableRowGroup AddGroup(this Table table)
        {
            var rowGroup = new TableRowGroup();
            table.RowGroups.Add(rowGroup);
            return rowGroup;
        }

        public static TableRowGroup AddGroup(this Table table, TableRowGroup newGroup)
        {
            table.RowGroups.Add(newGroup);
            return newGroup;
        }

        public static TableRow AddRow(this TableRowGroup group)
        {
            var row = new TableRow();
            group.Rows.Add(row);
            return row;
        }

        public static TableRowGroup AddRow(this TableRowGroup rg, IEnumerable<string> values)
        {
            var lst = values.ToList();

            var row = new TableRow();
            foreach (var j in lst) row.Cells.Add(new TableCell(new Paragraph(new Run(j))));
            rg.Rows.Add(row);
            return rg;
        }

        public static Table AddRow(this Table tbl, IEnumerable<string> values)
        {
            var lst = values.ToList();

            if (lst.Count > tbl.Columns.Count) throw new ArgumentOutOfRangeException();

            var rg = new TableRowGroup();
            var row = new TableRow();
            foreach (var j in lst) row.Cells.Add(new TableCell(new Paragraph(new Run(j))));
            rg.Rows.Add(row);


            tbl.RowGroups.Add(rg);

            return tbl;
        }

        public static TableRowGroup AddRow(this TableRowGroup rg, params string[] values)
        {
            return AddRow(rg, values.ToList());
        }

        public static TableRowGroup AddRow(this TableRowGroup rg, IEnumerable<TableCell> cells)
        {
            var tr = new TableRow();
            foreach (var j in cells) tr.Cells.Add(j);

            rg.Rows.Add(tr);
            return rg;
        }

        public static TableRowGroup AddRow(this TableRowGroup rg, params TableCell[] cells)
        {
            return AddRow(rg, cells.ToList());
        }

        public static TableRowGroup AddRow(this TableRowGroup rg, IEnumerable<Block> values)
        {
            var lst = values.ToList();

            var row = new TableRow();
            foreach (var j in lst) row.Cells.Add(new TableCell(j));
            rg.Rows.Add(row);
            return rg;
        }

        public static TableRowGroup AddRow(this TableRowGroup rg, params Block[] values)
        {
            return AddRow(rg, values.ToList());
        }

        public static Table AddTable(this FlowDocument document, params GridLength[] columnWidths)
        {
            var t = new Table();
            foreach (var j in columnWidths)
                t.Columns.Add(new TableColumn {Width = j});
            document.Blocks.Add(t);
            return t;
        }

        public static Table AddTable(this FlowDocument document,
            IEnumerable<KeyValuePair<string, GridLength>> columnWidths)
        {
            var t = new Table();
            t.AddGroup().AddRow(columnWidths.Select(p =>
            {
                t.Columns.Add(new TableColumn {Width = p.Value});
                return p.Key;
            })).Bold();
            document.Blocks.Add(t);
            return t;
        }

        /// <summary>
        ///     Establece un color de fondo para un <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> al cual aplicarle el color de fondo.</typeparam>
        /// <param name="element"><see cref="TextElement" /> al cual aplicarle el color de fondo.</param>
        /// <param name="value">Fondo a aplicar.</param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement Background<TElement>(this TElement element, Brush value) where TElement : TextElement
        {
            element.Background = value;
            return element;
        }

        /// <summary>
        ///     Establece el formato de texto en negrita.
        /// </summary>
        /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement Bold<TElement>(this TElement element) where TElement : TextElement
        {
            element.FontWeight = FontWeights.Bold;
            return element;
        }

        /// <summary>
        ///     Establece un borde para una celda de una tabla.
        /// </summary>
        /// <param name="element">Celda a procesar.</param>
        /// <param name="brush"><see cref="Brush" /> a utilizar para dibujar el borde.</param>
        /// <param name="thickness">Grosor del borde.</param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TableCell Border(this TableCell element, Brush brush, Thickness thickness)
        {
            element.BorderBrush = brush;
            element.BorderThickness = thickness;
            return element;
        }

        /// <summary>
        ///     Establece un borde para todas las celdas de un <see cref="TableRow" />.
        /// </summary>
        /// <param name="element"><see cref="TableRow" /> a procesar.</param>
        /// <param name="brush"><see cref="Brush" /> a utilizar para dibujar el borde.</param>
        /// <param name="thickness">Grosor del borde.</param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TableRow Borders(this TableRow element, Brush brush, Thickness thickness)
        {
            foreach (var j in element.Cells)
                j.Border(brush, thickness);
            return element;
        }

        /// <summary>
        ///     Establece un borde para todas las celdas de un <see cref="TableRowGroup" />.
        /// </summary>
        /// <param name="element"><see cref="TableRowGroup" /> a procesar.</param>
        /// <param name="brush"><see cref="Brush" /> a utilizar para dibujar el borde.</param>
        /// <param name="thickness">Grosor del borde.</param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TableRowGroup Borders(this TableRowGroup element, Brush brush, Thickness thickness)
        {
            foreach (var j in element.Rows.SelectMany(p => p.Cells))
                j.Border(brush, thickness);
            return element;
        }

        /// <summary>
        ///     Establece la alineación de texto en central.
        /// </summary>
        /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement Center<TElement>(this TElement element) where TElement : Block
        {
            element.TextAlignment = TextAlignment.Center;
            return element;
        }

        public static TableRow CenterAll(this TableRow element)
        {
            foreach (var j in element.Cells.SelectMany(p => p.Blocks)) j.Center();
            return element;
        }

        public static TableRowGroup CenterAll(this TableRowGroup element)
        {
            foreach (var j in element.Rows.SelectMany(o => o.Cells.SelectMany(p => p.Blocks))) j.Center();
            return element;
        }

        /// <summary>
        ///     Establece un color de fondo para la columna especificada de la tabla.
        /// </summary>
        /// <param name="table"><see cref="System.Windows.Documents.Table" /> a procesar.</param>
        /// <param name="column">Índice de la columna.</param>
        /// <param name="brush"><see cref="Brush" /> a aplicar al dibujar la tabla.</param>
        /// <returns>
        ///     <paramref name="table" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static Table ColumnBackground(this Table table, int column, Brush brush)
        {
            table.Columns[column].Background = brush;
            return table;
        }

        public static TableCell ColumnSpan(this TableCell cell, int span)
        {
            cell.ColumnSpan = span;
            return cell;
        }

        /// <summary>
        ///     Establece el ancho de una columna para un <see cref="System.Windows.Documents.Table" />.
        /// </summary>
        /// <param name="table"><see cref="System.Windows.Documents.Table" /> a procesar.</param>
        /// <param name="column">Índice de la columna.</param>
        /// <param name="width">Ancho de columna a aplicar.</param>
        /// <returns>
        ///     <paramref name="table" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static Table ColumnWidth(this Table table, int column, GridLength width)
        {
            table.Columns[column].Width = width;
            return table;
        }

        /// <summary>
        ///     Establece los anchos de columna para un <see cref="System.Windows.Documents.Table" />.
        /// </summary>
        /// <param name="table"><see cref="System.Windows.Documents.Table" /> a procesar.</param>
        /// <param name="lengths">Anchos de columna a aplicar.</param>
        /// <returns>
        ///     <paramref name="table" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static Table ColumnWidths(this Table table, IEnumerable<double> lengths)
        {
            return ColumnWidths(table, lengths.Select(p => new GridLength(p)));
        }

        /// <summary>
        ///     Establece los anchos de columna para un <see cref="System.Windows.Documents.Table" />.
        /// </summary>
        /// <param name="table"><see cref="System.Windows.Documents.Table" /> a procesar.</param>
        /// <param name="lengths">Anchos de columna a aplicar.</param>
        /// <returns>
        ///     <paramref name="table" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static Table ColumnWidths(this Table table, IEnumerable<GridLength> lengths)
        {
            using (var a = table.Columns.ToList().GetEnumerator())
            using (var b = lengths.GetEnumerator())
            {
                while (a.MoveNext() && b.MoveNext())
                    if (!(a.Current is null))
                        a.Current.Width = b.Current;
            }

            return table;
        }

        /// <summary>
        ///     Marca el final del contexto de una fila de tabla, devolviendo a
        ///     su grupo padre de forma compatible con la sintaxis Fluent.
        /// </summary>
        /// <param name="row">
        ///     Fila para la cual finalizar el contexto de la sintaxis Fluent.
        /// </param>
        /// <returns>
        ///     El grupo de filas a las que esta fila pertenece.
        /// </returns>
        public static TableRowGroup Done(this TableRow row)
        {
            return row.Parent as TableRowGroup;
        }

        /// <summary>
        ///     Marca el final del contexto de un grupo de filas de tabla,
        ///     devolviendo a su tabla padre de forma compatible con la
        ///     sintaxis Fluent.
        /// </summary>
        /// <param name="rowGroup">
        ///     Grupo de filas para el cual finalizar el contexto de la
        ///     sintaxis Fluent.
        /// </param>
        /// <returns>
        ///     La tabla a las que este grupo de filas pertenece.
        /// </returns>
        public static Table Done(this TableRowGroup rowGroup)
        {
            return rowGroup.Parent as Table;
        }

        public static TableRow Done(this TableCell cell)
        {
            return cell.Parent as TableRow;
        }

        public static T Done<T>(this FrameworkContentElement block) where T : class
        {
            return block.Parent as T ?? throw new InvalidCastException();
        }

        /// <summary>
        ///     Establece un efecto de texto sobre un <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <param name="effect">Efecto a aplicar al texto.</param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement Effect<TElement>(this TElement element, TextEffect effect) where TElement : TextElement
        {
            element.TextEffects.Add(effect);
            return element;
        }

        /// <summary>
        ///     Establece un color principal para un <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> al cual aplicarle el color principal.</typeparam>
        /// <param name="element"><see cref="TextElement" /> al cual aplicarle el color principal.</param>
        /// <param name="value">Color principal a aplicar.</param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement Foreground<TElement>(this TElement element, Brush value) where TElement : TextElement
        {
            element.Foreground = value;
            return element;
        }

        /// <summary>
        ///     Establece el formato de un <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <param name="fontFamily">
        ///     Familia de fuentes a utilizar.
        /// </param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement Format<TElement>(this TElement element, FontFamily fontFamily)
            where TElement : TextElement
        {
            element.FontFamily = fontFamily;
            return element;
        }

        /// <summary>
        ///     Establece el formato de un <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <param name="fontWeight">
        ///     Densidad de la fuente.
        /// </param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement Format<TElement>(this TElement element, FontWeight fontWeight)
            where TElement : TextElement
        {
            element.FontWeight = fontWeight;
            return element;
        }

        /// <summary>
        ///     Establece el formato de un <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <param name="fontSize">Tamaño de la fuente.</param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement Format<TElement>(this TElement element, double fontSize) where TElement : TextElement
        {
            element.FontSize = fontSize;
            return element;
        }

        /// <summary>
        ///     Establece el formato de un <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <param name="fontStretch">
        ///     Estiramiento de la fuente.
        /// </param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement Format<TElement>(this TElement element, FontStretch fontStretch)
            where TElement : TextElement
        {
            element.FontStretch = fontStretch;
            return element;
        }

        /// <summary>
        ///     Establece el formato de un <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <param name="fontStyle">Estilo de la fuente.</param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement Format<TElement>(this TElement element, FontStyle fontStyle) where TElement : TextElement
        {
            element.FontStyle = fontStyle;
            return element;
        }

        /// <summary>
        ///     Establece el formato de un <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <param name="alignment">Alineación de texto.</param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement Format<TElement>(this TElement element, TextAlignment alignment) where TElement : Block
        {
            element.TextAlignment = alignment;
            return element;
        }

        /// <summary>
        ///     Establece la alineación del texto en justificada.
        /// </summary>
        /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement Justify<TElement>(this TElement element) where TElement : Block
        {
            element.TextAlignment = TextAlignment.Justify;
            return element;
        }

        /// <summary>
        ///     Establece la alineación del texto en izquierda.
        /// </summary>
        /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement Left<TElement>(this TElement element) where TElement : Block
        {
            element.TextAlignment = TextAlignment.Left;
            return element;
        }

        public static FlowDocument MakeTable(this FlowDocument fd, IDictionary<string, string> headers,
            IEnumerable data)
        {
            var tbl = new Table();

            var headersRow = new TableRow {FontWeight = FontWeights.Bold};
            foreach (var j in headers)
            {
                tbl.Columns.Add(new TableColumn());
                headersRow.Cells.Add(new TableCell(new Paragraph(new Run(j.Value))));
            }

            var rg = new TableRowGroup();
            rg.Rows.Add(headersRow);

            foreach (var j in data)
            {
                var row = new TableRow();
                foreach (var k in headers)
                {
                    var val = j.GetType().GetProperty(k.Key)?.GetMethod.Invoke(j, new object[] { }).ToString();
                    row.Cells.Add(new TableCell(new Paragraph(new Run(val))));
                }

                rg.Rows.Add(row);
            }

            tbl.RowGroups.Add(rg);
            fd.Blocks.Add(tbl);
            return fd;
        }

        public static FlowDocument MakeTable<T>(this FlowDocument fd, IEnumerable<TableSelector<T>> columns,
            IEnumerable<T> data)
        {
            var tbl = new Table();

            var headersRow = new TableRow {FontWeight = FontWeights.Bold};
            var namedSelectors = columns.ToList();
            foreach (var j in namedSelectors)
            {
                tbl.Columns.Add(new TableColumn());
                headersRow.Cells.Add(new TableCell(new Paragraph(new Run(j.Header))));
            }

            var rg = new TableRowGroup();
            rg.Rows.Add(headersRow);

            foreach (var j in data)
            {
                var row = new TableRow();
                foreach (var k in namedSelectors) row.Cells.Add(new TableCell(new Paragraph(new Run(k.Selector(j)))));
                rg.Rows.Add(row);
            }

            tbl.RowGroups.Add(rg);
            fd.Blocks.Add(tbl);
            return fd;
        }

        public static Table MakeTable(this FlowDocument fd, IEnumerable<string> headers)
        {
            var tbl = new Table();

            var headersRow = new TableRow {FontWeight = FontWeights.Bold};
            foreach (var j in headers)
            {
                tbl.Columns.Add(new TableColumn());
                headersRow.Cells.Add(new TableCell(new Paragraph(new Run(j))));
            }

            var rg = new TableRowGroup();
            rg.Rows.Add(headersRow);
            tbl.RowGroups.Add(rg);
            fd.Blocks.Add(tbl);
            return tbl;
        }

        public static Paragraph Paragraph(this FlowDocument fd)
        {
            var p = new Paragraph();
            fd.Blocks.Add(p);
            return p;
        }

        public static FlowDocument Paragraph(this FlowDocument fd, string content)
        {
            return Paragraph(fd, content, TextAlignment.Left);
        }

        public static FlowDocument Paragraph(this FlowDocument fd, string content, TextAlignment alignment)
        {
            foreach (var text in content.Replace("\n\r", "\n").Split('\n'))
                fd.Blocks.Add(new Paragraph {Inlines = {new Run {Text = text}}, TextAlignment = alignment});
            return fd;
        }

        /// <summary>
        ///     Establece la alineación del texto en derecha.
        /// </summary>
        /// <typeparam name="TElement">Tipo de <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
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
            var p = new Paragraph(new Run(text));
            blocks.Add(p);
            return p;
        }

        public static Paragraph Text(this TableCell cell, string text)
        {
            var p = new Paragraph(new Run(text));
            cell.Blocks.Add(p);
            return p;
        }

        public static FlowDocument Text(this FlowDocument fd, string text)
        {
            return Text(fd, text, TextAlignment.Left);
        }

        public static FlowDocument Text(this FlowDocument fd, string text, TextAlignment alignment)
        {
            fd.Blocks.Add(new Paragraph {Inlines = {new Run {Text = text}}, TextAlignment = alignment});
            return fd;
        }

        public static FlowDocument Title(this FlowDocument fd, string text)
        {
            return Title(fd, text, 0, TextAlignment.Left);
        }

        public static FlowDocument Title(this FlowDocument fd, string text, byte level)
        {
            return Title(fd, text, level, TextAlignment.Left);
        }

        public static FlowDocument Title(this FlowDocument fd, string text, TextAlignment alignment)
        {
            return Title(fd, text, 0, alignment);
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
                        FontSize = 36 - 6 * level
                    }
                },
                TextAlignment = alignment
            });
            return fd;
        }

        /// <summary>
        ///     Genera un objeto <see cref="System.Windows.Documents.Table" /> a partir de la vista actual de un
        ///     <see cref="ListView" />.
        /// </summary>
        /// <param name="listView"><see cref="ListView" /> a procesar.</param>
        /// <returns>
        ///     Un <see cref="System.Windows.Documents.Table" /> con el contenido de la vista activa del <see cref="ListView" />.
        /// </returns>
        public static Table ToDocumentTable(this ListView listView)
        {
            var cols = (listView.View as GridView)?.Columns ?? throw new ArgumentException();
            var table = new Table().ColumnWidths(cols.Select(p => p.Width));
            var data = table.AddGroup().AddRow(cols.Select(p => p.Header.ToString())).CenterAll().Bold().Done()
                .AddGroup();
            foreach (var j in listView.Items)
            {
                var row = data.AddRow();
                foreach (var k in ((GridView) listView.View).Columns)
                {
                    if (!(k.DisplayMemberBinding is Binding b)) continue;
                    var o = b.Path.Path.Split('.').Aggregate(j,
                        (current, i) => current?.GetType().GetProperty(i)?.GetMethod?.Invoke(j, new object[] { }));
                    if (!(o is null)) row.AddCell(o.ToString());
                }
            }

            return table;
        }

        /// <summary>
        ///     Manipula la información tipográfica del <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <param name="value">Índice de formato de anotación alternativa.</param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement TypographyAnnotationAlternates<TElement>(this TElement element, int value)
            where TElement : TextElement
        {
            element.Typography.AnnotationAlternates = value;
            return element;
        }

        /// <summary>
        ///     Manipula la información tipográfica del <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <param name="value">
        ///     si se establece en <see langword="true" />, se ajustará globalmente el espacio entre glifos en mayúsculas para
        ///     mejorar la legibilidad.
        /// </param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement TypographyCapitalSpacing<TElement>(this TElement element, bool value)
            where TElement : TextElement
        {
            element.Typography.CapitalSpacing = value;
            return element;
        }

        /// <summary>
        ///     Manipula la información tipográfica del <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <param name="value">
        ///     Si se establece en <see langword="true" />, se ajustará la posición vertical de los glifos para una mejor alineación
        ///     con los glifos en mayúsculas.
        /// </param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement TypographyCaseSensitiveForms<TElement>(this TElement element, bool value)
            where TElement : TextElement
        {
            element.Typography.CaseSensitiveForms = value;
            return element;
        }

        /// <summary>
        ///     Manipula la información tipográfica del <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <param name="value">
        ///     Si se establece en <see langword="true" /> se utilizarán glifos personalizados según el contexto del texto que se
        ///     procesa.
        /// </param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement TypographyContextualAlternates<TElement>(this TElement element, bool value)
            where TElement : TextElement
        {
            element.Typography.ContextualAlternates = value;
            return element;
        }

        /// <summary>
        ///     Manipula la información tipográfica del <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <param name="value">
        ///     Si se establece en <see langword="true" />, se habilitan las ligaduras contextuales.
        /// </param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement TypographyContextualLigatures<TElement>(this TElement element, bool value)
            where TElement : TextElement
        {
            element.Typography.ContextualLigatures = value;
            return element;
        }

        /// <summary>
        ///     Manipula la información tipográfica del <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <param name="value">Especifica el índice de un formulario de glifos floreados contextuales.</param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement TypographyContextualSwashes<TElement>(this TElement element, int value)
            where TElement : TextElement
        {
            element.Typography.ContextualSwashes = value;
            return element;
        }

        /// <summary>
        ///     Manipula la información tipográfica del <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <param name="value">
        ///     Si se establece en <see langword="true" />, se habilitan las ligaduras discrecionales.
        /// </param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement TypographyDiscretionaryLigatures<TElement>(this TElement element, bool value)
            where TElement : TextElement
        {
            element.Typography.DiscretionaryLigatures = value;
            return element;
        }

        /// <summary>
        ///     Manipula la información tipográfica del <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <param name="value">
        ///     Si se establece en <see langword="true" />, los formatos de fuente japonesa estándar se reemplazarán por los
        ///     correspondientes formatos tipográficos preferidos.
        /// </param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement TypographyEastAsianExpertForms<TElement>(this TElement element, bool value)
            where TElement : TextElement
        {
            element.Typography.EastAsianExpertForms = value;
            return element;
        }

        /// <summary>
        ///     Manipula la información tipográfica del <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <param name="value">
        ///     Glifos que se utilizarán para un idioma o sistema de escritura en específico para Asia oriental.
        /// </param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement TypographyEastAsianLanguage<TElement>(this TElement element, FontEastAsianLanguage value)
            where TElement : TextElement
        {
            element.Typography.EastAsianLanguage = value;
            return element;
        }

        /// <summary>
        ///     Manipula la información tipográfica del <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <param name="value">Ancho de los caracteres latinos en uan fuente de estilo asiático.</param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
        /// </returns>
        public static TElement TypographyEastAsianWidths<TElement>(this TElement element, FontEastAsianWidths value)
            where TElement : TextElement
        {
            element.Typography.EastAsianWidths = value;
            return element;
        }

        /// <summary>
        ///     Manipula la información tipográfica del <see cref="TextElement" />.
        /// </summary>
        /// <typeparam name="TElement">Tipo del <see cref="TextElement" /> a manipular.</typeparam>
        /// <param name="element"><see cref="TextElement" /> a manipular.</param>
        /// <param name="value">Estilo de letra mayúscula para una tipografía.</param>
        /// <returns>
        ///     <paramref name="element" />, lo que permite utilizar esta función
        ///     con sintaxis Fluent.
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
}