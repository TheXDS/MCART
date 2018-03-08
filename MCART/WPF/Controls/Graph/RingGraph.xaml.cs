/*
RingGraph.cs

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

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Converters;
using System.Windows.Data;
using System.Windows.Shapes;

namespace TheXDS.MCART.Controls
{
    public partial class RingGraph : UserControl
    {
        static DependencyPropertyKey TotalPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(Total), typeof(double), typeof(RingGraph),
            new PropertyMetadata(0.0));
        /**
        Internamente, las propiedades de FontSize de txtTotal y el Margin
        del ViewBox que lo contiene, causan que la propiedad StrokeThickness
        de los elementos a dibujar representen valores porcentuales.
        <summary>
        Identifica a la propiedad de dependencia
        <see cref="RingThickness"/>.
        </summary>
        **/
        public static DependencyProperty RingThicknessProperty = DependencyProperty.Register(
                nameof(RingThickness), typeof(double), typeof(RingGraph),
                new PropertyMetadata(30.0), p => ((double)p).IsBetween(0.0, 100.0));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="SubLevelsShown"/>.
        /// </summary>
        public static DependencyProperty SubLevelsShownProperty = DependencyProperty.Register(
                nameof(SubLevelsShown), typeof(int), typeof(RingGraph),
                new PropertyMetadata(1, (d, e) => ((RingGraph)d).Redraw()), v => (int)v > 0);
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Title"/>.
        /// </summary>
        public static DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title), typeof(string), typeof(RingGraph),
            new PropertyMetadata(string.Empty));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="TitleFontSize"/>.
        /// </summary>
        public static DependencyProperty TitleFontSizeProperty = DependencyProperty.Register(
            nameof(IGraph.TitleFontSize), typeof(double), typeof(RingGraph),
            new PropertyMetadata(16.0));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="ToolTipFormat"/>.
        /// </summary>
        public static DependencyProperty ToolTipFormatProperty = DependencyProperty.Register(
                nameof(ToolTipFormat), typeof(string), typeof(RingGraph),
                new PropertyMetadata(string.Empty));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Total"/>.
        /// </summary>
        public static DependencyProperty TotalProperty = TotalPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="TotalVisible"/>.
        /// </summary>
        public static DependencyProperty TotalVisibleProperty = DependencyProperty.Register(
            nameof(TotalVisible), typeof(bool), typeof(RingGraph),
            new PropertyMetadata(true));
        /// <summary>
        /// Obtiene o establece el porcentaje de espacio ocupado por los datos
        /// desde el radio hasta el centro del gráfico, o hasta el espacio
        /// reservado para la etiqueta de total.
        /// </summary>
        public double RingThickness
        {
            get => (double)GetValue(RingThicknessProperty);
            set => SetValue(RingThicknessProperty, value);
        }
        /// <summary>
        /// Obtiene o establece la cantidad de sub-niveles a mostrar en este
        /// <see cref="ISliceGraph"/>.
        /// </summary>
        public int SubLevelsShown
        {
            get => (int)GetValue(SubLevelsShownProperty);
            set => SetValue(SubLevelsShownProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el título a mostrar de este
        /// <see cref="IGraph"/>.
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el tamaño de fuente a aplicar al título de este
        /// <see cref="IGraph"/>.
        /// </summary>
        public double TitleFontSize
        {
            get => (double)GetValue(TitleFontSizeProperty);
            set => SetValue(TitleFontSizeProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el formato opcional a aplicar a etiquetas
        /// flotantes para los <see cref="Slice"/> dibujados en este control.
        /// </summary>
        public string ToolTipFormat
        {
            get => (string)GetValue(ToolTipFormatProperty);
            set => SetValue(ToolTipFormatProperty, value);
        }
        /// <summary>
        /// Obtiene el total general de los datos de este
        /// <see cref="ISliceGraph"/>.
        /// </summary>
        public double Total => (double)GetValue(TotalProperty);
        /// <summary>
        /// Obtiene o establece un valor que determina si se mostrarán los
        /// totales de los puntos y el total general de los datos.
        /// </summary>
        public bool TotalVisible
        {
            get => (bool)GetValue(TotalVisibleProperty);
            set => SetValue(TotalVisibleProperty, value);
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="RingGraph"/>.
        /// </summary>
        public void Init()
        {
            InitializeComponent();
            txtTitle.SetBinding(VisibilityProperty,
                new Binding(nameof(txtTitle.Text))
                {
                    Source = txtTitle,
                    Converter = new StringVisibilityConverter()
                });
            txtTitle.SetBinding(TextBlock.TextProperty,
                new Binding(nameof(Title)) { Source = this });
            txtTitle.SetBinding(TextBlock.FontSizeProperty,
                new Binding(nameof(TitleFontSize)) { Source = this });
            Loaded += (sender, e) => Redraw();
        }
        /// <summary>
        /// Vuelve a dibujar todo el control.
        /// </summary>
        public void Redraw()
        {
            if (!Math.AreNotZero(ActualHeight, ActualWidth)) return;
            double s = (SubLevelsShown - 1) * RingThickness;
            grdRoot.Margin = new Thickness(s);
            vbxTotal.Margin = new Thickness(100 + s - RingThickness);
            grdRoot.Children.Clear();
            Grid g = new Grid();
            grdRoot.Children.Add(g);
            g.SetBinding(HeightProperty, new Binding(nameof(ActualHeight)) { Source = grdRoot });
            g.SetBinding(WidthProperty, new Binding(nameof(ActualWidth)) { Source = grdRoot });
            RdrwChild(Slices, g, 0, 360, pnlLabels.Items, out double tot);
            SetValue(TotalPropertyKey, tot);
            txtTotal.Text = tot.ToString();
        }
        /// <summary>
        /// Vuelve a dibujar únicamente a los hijos del <see cref="Slice"/>.
        /// </summary>
        /// <param name="r">
        /// <see cref="Slice"/> que ha realizado la solicitud.
        /// </param>
        public void DrawMyChildren(Slice r)
        {
            //HACK: Aún no es posible redibujar selectivamente los hijos de r.
            Redraw();
        }
        private void RdrwSeries(Slice j, double tot, double angle, double sze, Grid g, ItemCollection i, out TreeViewItem t)
        {
            Path p = new Path { Data = UI.GetCircleArc(((new[] { g.ActualWidth, g.Width }).Max() - RingThickness) / 2, angle, angle + sze, RingThickness) };
            p.SetBinding(Shape.StrokeProperty, new Binding(nameof(j.SliceBrush)) { Source = j });
            p.SetBinding(Shape.StrokeThicknessProperty, new Binding(nameof(RingThickness)) { Source = this });
            if (!ToolTipFormat.IsEmpty()) p.ToolTip = new ToolTip { Content = string.Format(ToolTipFormat, j.Name, j.Value) };
            CheckBox c = new CheckBox();
            c.SetBinding(ContentProperty, new Binding(nameof(j.Name)) { Source = j });
            c.SetBinding(BackgroundProperty, new Binding(nameof(j.SliceBrush)) { Source = j });
            t = new TreeViewItem { Header = c };
            i.Add(t);
            j.shape = p;
            j.label = c;
            j.parent = g;
            g.Children.Add(p);
        }
        private void RdrwChild(System.Collections.Generic.IList<Slice> slices, Grid g, double startAngle, double totalSize, ItemCollection labels, out double total) => RdrwChild(slices, g, startAngle, totalSize, labels, out total, 0);
        private void RdrwChild(System.Collections.Generic.IList<Slice> slices, Grid g, double startAngle, double totalSize, ItemCollection labels, out double total, int sublevel)
        {
            labels.Clear();
            colorizer?.Apply(slices);
            total = slices.GetTotal();
            double ang = startAngle;
            if (!grdRoot.Children.Contains(g))
            {
                grdRoot.Children.Add(g);
                grdRoot.UpdateLayout();
                g.UpdateLayout();
            }
            Grid subg = null;
            if (SubLevelsShown - 1 > sublevel)
            {
                double sz = RingThickness * (sublevel + 1);
                subg = new Grid { Margin = new Thickness(-sz) };
            }
            foreach (var k in slices)
            {
                if (g.Children.Contains(k.shape)) g.Children.Remove(k.shape);
                double sz = totalSize * (k.Value / total);
                RdrwSeries(k, total, ang, sz, g, labels, out TreeViewItem t);
                if (SubLevelsShown - 1 > sublevel)
                {
                    RdrwChild(
                        k.SubSlices,
                        subg,
                        ang,
                        sz,
                        t.Items,
                        out double tot,
                        sublevel + 1);
                    // TODO: en vez de _, dibujar una etiqueta con el total.
                }
                ang += sz;
            }
        }
    }
}