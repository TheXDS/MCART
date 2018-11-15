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

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Converters;
using System.Windows.Data;
using System.Windows.Shapes;
using TheXDS.MCART.Math;
using static TheXDS.MCART.Types.Extensions.StringExtensions;

namespace TheXDS.MCART.Controls
{
    public partial class RingGraph
    {
        private Binding totalBinding;


        #region Propiedades de dependencia

        private static readonly DependencyPropertyKey TotalPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(Total), typeof(double), typeof(RingGraph),
            new PropertyMetadata(0.0));

        /*
        Internamente, las propiedades de FontSize de txtTotal y el Margin
        del ViewBox que lo contiene, causan que la propiedad StrokeThickness
        de los elementos a dibujar representen valores porcentuales.
        */
        /// <summary>
        ///     Identifica a la propiedad de dependencia
        ///     <see cref="RingThickness"/>.
        /// </summary>
        public static readonly DependencyProperty RingThicknessProperty = DependencyProperty.Register(
            nameof(RingThickness), typeof(double), typeof(RingGraph),
            new PropertyMetadata(30.0), p => ((double)p).IsBetween(0.0, 100.0));

        /// <summary>
        ///     Identifica a la propiedad de dependencia <see cref="SubLevelsShown" />.
        /// </summary>
        public static readonly DependencyProperty SubLevelsShownProperty = DependencyProperty.Register(
            nameof(SubLevelsShown), typeof(int), typeof(RingGraph),
            new PropertyMetadata(1, (d, e) => ((RingGraph)d).Redraw()), v => (int)v > 0);

        /// <summary>
        ///     Identifica a la propiedad de dependencia <see cref="Title" />.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title), typeof(string), typeof(RingGraph),
            new PropertyMetadata(string.Empty));

        /// <summary>
        ///     Identifica a la propiedad de dependencia <see cref="TitleFontSize" />.
        /// </summary>
        public static readonly DependencyProperty TitleFontSizeProperty = DependencyProperty.Register(
            nameof(IGraph.TitleFontSize), typeof(double), typeof(RingGraph),
            new PropertyMetadata(16.0));

        /// <summary>
        ///     Identifica a la propiedad de dependencia <see cref="ToolTipFormat" />.
        /// </summary>
        public static readonly DependencyProperty ToolTipFormatProperty = DependencyProperty.Register(
            nameof(ToolTipFormat), typeof(string), typeof(RingGraph),
            new PropertyMetadata(string.Empty));

        /// <summary>
        ///     Identifica a la propiedad de dependencia <see cref="Total" />.
        /// </summary>
        public static readonly DependencyProperty TotalProperty = TotalPropertyKey.DependencyProperty;

        /// <summary>
        ///     Identifica a la propiedad de dependencia <see cref="TotalVisible" />.
        /// </summary>
        public static readonly DependencyProperty TotalVisibleProperty = DependencyProperty.Register(
            nameof(TotalVisible), typeof(bool), typeof(RingGraph),
            new PropertyMetadata(true));

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="TotalFormat"/>.
        /// </summary>
        public static readonly DependencyProperty TotalFormatProperty = DependencyProperty.Register(
                nameof(TotalFormat), typeof(string), typeof(RingGraph),
                new PropertyMetadata(null));

        #endregion

        #region Propiedades

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene o establece el formato a aplicar a la etiqueta de total del
        ///     <see cref="T:TheXDS.MCART.Controls.ISliceGraph" />.
        /// </summary>
        public string TotalFormat
        {
            get => (string)GetValue(TotalFormatProperty);
            set => SetValue(TotalFormatProperty, value);
        }

        /// <summary>
        ///     Obtiene o establece el porcentaje de espacio ocupado por los datos
        ///     desde el radio hasta el centro del gráfico, o hasta el espacio
        ///     reservado para la etiqueta de total.
        /// </summary>
        public double RingThickness
        {
            get => (double)GetValue(RingThicknessProperty);
            set => SetValue(RingThicknessProperty, value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene o establece la cantidad de sub-niveles a mostrar en este
        ///     <see cref="T:TheXDS.MCART.Controls.ISliceGraph" />.
        /// </summary>
        public int SubLevelsShown
        {
            get => (int)GetValue(SubLevelsShownProperty);
            set => SetValue(SubLevelsShownProperty, value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene o establece el título a mostrar de este
        ///     <see cref="T:TheXDS.MCART.Controls.IGraph" />.
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene o establece el tamaño de fuente a aplicar al título de este
        ///     <see cref="T:TheXDS.MCART.Controls.IGraph" />.
        /// </summary>
        public double TitleFontSize
        {
            get => (double)GetValue(TitleFontSizeProperty);
            set => SetValue(TitleFontSizeProperty, value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene o establece el formato opcional a aplicar a etiquetas
        ///     flotantes para los <see cref="T:TheXDS.MCART.Controls.Slice" /> dibujados en este control.
        /// </summary>
        public string ToolTipFormat
        {
            get => (string)GetValue(ToolTipFormatProperty);
            set => SetValue(ToolTipFormatProperty, value);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene el total general de los datos de este
        ///     <see cref="T:TheXDS.MCART.Controls.ISliceGraph" />.
        /// </summary>
        public double Total => (double)GetValue(TotalProperty);

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene o establece un valor que determina si se mostrarán los
        ///     totales de los puntos y el total general de los datos.
        /// </summary>
        public bool TotalVisible
        {
            get => (bool)GetValue(TotalVisibleProperty);
            set => SetValue(TotalVisibleProperty, value);
        }

        #endregion

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="T:TheXDS.MCART.Controls.RingGraph" />.
        /// </summary>
        public void Init()
        {
            InitializeComponent();
            totalBinding = new Binding(nameof(Total)) {Source = this};


            txtTitle.SetBinding(VisibilityProperty,
                new Binding(nameof(txtTitle.Text))
                {
                    Source = txtTitle,
                    Converter = new StringVisibilityConverter()
                });
            txtTitle.SetBinding(TextBlock.TextProperty, new Binding(nameof(Title)) {Source = this});
            txtTitle.SetBinding(TextBlock.FontSizeProperty, new Binding(nameof(TitleFontSize)) {Source = this});
            txtTotal.SetBinding(TextBlock.TextProperty, new Binding(nameof(Total)) {Source = this});


            Loaded += (sender, e) => Redraw();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Vuelve a dibujar el control.
        /// </summary>
        public void Redraw()
        {
            if (!Algebra.AreNotZero(ActualHeight, ActualWidth)) return;
            var s = (SubLevelsShown - 1) * RingThickness;
            grdRoot.Margin = new Thickness(s);
            vbxTotal.Margin = new Thickness(100 + s - RingThickness);
            grdRoot.Children.Clear();
            var g = new Grid();
            grdRoot.Children.Add(g);
            g.SetBinding(HeightProperty,
                new Binding(nameof(ActualHeight)) {Source = grdRoot, Mode = BindingMode.OneWay});
            g.SetBinding(WidthProperty, new Binding(nameof(ActualWidth)) {Source = grdRoot, Mode = BindingMode.OneWay});
            RdrwChild(Slices, g, 0, 360, pnlLabels.Items, out var tot);
            SetValue(TotalPropertyKey, tot);

            txtTotal.Text = tot.ToString();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Vuelve a dibujar únicamente a los hijos del <see cref="T:TheXDS.MCART.Controls.Slice" />.
        /// </summary>
        /// <param name="r">
        ///     <see cref="T:TheXDS.MCART.Controls.Slice" /> que ha realizado la solicitud.
        /// </param>
        public void DrawMyChildren(Slice r)
        {
            //HACK: Aún no es posible redibujar selectivamente los hijos de r.
            Redraw();
        }

        private void RdrwSeries(Slice j, double tot, double angle, double sze, Grid g, ItemCollection i, int currsl,
            out TreeViewItem t)
        {
            var ssz = (200 + currsl * (RingThickness * 2) - RingThickness) / 2;
            var p = new Path {Data = UI.GetCircleArc(ssz, angle, angle + sze, RingThickness)};
            p.SetBinding(Shape.StrokeProperty, new Binding(nameof(j.SliceBrush)) {Source = j});
            p.SetBinding(Shape.StrokeThicknessProperty, new Binding(nameof(RingThickness)) {Source = this});
            if (!ToolTipFormat.IsEmpty())
                p.ToolTip = new ToolTip {Content = string.Format(ToolTipFormat, j.Name, j.Value)};
            var c = new CheckBox();
            c.SetBinding(ContentProperty, new Binding(nameof(j.Name)) {Source = j});
            c.SetBinding(BackgroundProperty, new Binding(nameof(j.SliceBrush)) {Source = j});
            t = new TreeViewItem {Header = c};
            i.Add(t);
            j.shape = p;
            j.label = c;
            j.parent = g;
            g.Children.Add(p);
        }

        private void RdrwChild(IList<Slice> slices, Grid g, double startAngle, double totalSize, ItemCollection labels,
            out double total)
        {
            RdrwChild(slices, g, startAngle, totalSize, labels, out total, 0);
        }

        private void RdrwChild(IList<Slice> slices, Grid g, double startAngle, double totalSize, ItemCollection labels,
            out double total, int sublevel)
        {
            labels.Clear();
            _colorizer?.Apply(slices);
            total = slices.GetTotal();
            var ang = startAngle;
            if (!grdRoot.Children.Contains(g))
                grdRoot.Children.Add(g);
            grdRoot.Measure(Size.Empty);
            grdRoot.UpdateLayout();

            Grid subg = null;
            if (SubLevelsShown - 1 > sublevel)
            {
                var sz = RingThickness * (sublevel + 1);
                subg = new Grid {Margin = new Thickness(-sz)};
            }

            foreach (var k in slices)
            {
                if (g.Children.Contains(k.shape)) g.Children.Remove(k.shape);
                var sz = totalSize * (k.Value / total);
                RdrwSeries(k, total, ang, sz, g, labels, sublevel, out var t);
                if (SubLevelsShown - 1 > sublevel)
                {
                    RdrwChild(
                        k.SubSlices,
                        subg,
                        ang,
                        sz,
                        t.Items,
                        out var tot,
                        sublevel + 1);
                    // TODO: en vez de _, dibujar una etiqueta con el total.
                }

                ang += sz;
            }
        }
    }
}