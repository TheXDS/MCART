/*
FlowDocumentFluent.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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
using System.Windows.Documents;

namespace TheXDS.MCART.Fluent
{
    /// <summary>
    ///     Extensiones de tipo Fluent para manipular objetos <see cref="FlowDocument" />
    /// </summary>
    public static class FlowDocumentFluentExtensions
    {
        /// <summary>
        ///     Agrega una celda simple con texto a una fila.
        /// </summary>
        /// <param name="row">Fila a la cual agregar la nueva celda.</param>
        /// <param name="text">Texto de la celda.</param>
        /// <returns>
        ///     <paramref name="row" />, lo que permite utilizar esta función
        ///     con sintáxis Fluent.
        /// </returns>
        public static TableRow AddCell(this TableRow row, string text)
        {
            return AddCell(row, text, FontWeights.Normal);
        }

        public static TableRow AddCell(this TableRow row, string text, FontWeight weight)
        {
            row.Cells.Add(new TableCell(new Paragraph(new Run(text) {FontWeight = weight})));
            return row;
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

        public static TableRowGroup AddRow(this TableRowGroup rg, IEnumerable<string> values)
        {
            var lst = values.ToList();

            var row = new TableRow();
            foreach (var j in lst) row.Cells.Add(new TableCell(new Paragraph(new Run(j))));
            rg.Rows.Add(row);
            return rg;
        }

        public static TableRowGroup AddRow(this TableRowGroup rg, params string[] values)
        {
            return AddRow(rg, values.ToList());
        }

        public static TableRowGroup AddRow(this TableRowGroup rg, IEnumerable<TableCell> cells)
        {
            var tr = new TableRow();
            foreach (var j in cells)
            {
                tr.Cells.Add(j);
            }

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

        public static Paragraph Paragraph(this FlowDocument fd)
        {
            var p = new Paragraph();
            fd.Blocks.Add(p);
            return p;
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

        public static FlowDocument Table(this FlowDocument fd, IDictionary<string, string> headers, IEnumerable data)
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

        public static FlowDocument Table<T>(this FlowDocument fd, IEnumerable<TableSelector<T>> columns,
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

        public static Table Table(this FlowDocument fd, IEnumerable<string> headers)
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
            return Title(fd, text, 0);
        }

        public static FlowDocument Title(this FlowDocument fd, string text, byte level)
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
                }
            });
            return fd;
        }

        public class TableSelector<T>
        {
            public TableSelector(string header, Func<T, string> selector)
            {
                Header = header;
                Selector = selector;
            }

            public string Header { get; }
            public Func<T, string> Selector { get; }
        }
    }
}