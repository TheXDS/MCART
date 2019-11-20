/*
LightGraph.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

using TheXDS.MCART.Types.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Converters;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using MC = TheXDS.MCART.Resources.Colors;
using ISt = TheXDS.MCART.Resources.InternalStrings;
using TheXDS.MCART.Math;
using TheXDS.MCART.Types;

// ReSharper disable UnusedMember.Global

namespace TheXDS.MCART.Controls
{
    /// <inheritdoc />
    /// <summary>
    ///     Control de gráficos de histograma ligero.
    /// </summary>
    [Obsolete(ISt.LegacyComponent)]
    public class LightGraph : UserControl
    {
        #region Propiedades de dependencia
        private static readonly Type T = typeof(LightGraph);

        private static readonly SolidColorBrush G2Default = MC.Pick().Brush();

        /// <summary>
        /// Identifica la propiedad de dependencia <see cref="Frozen"/>.
        /// </summary>
        public static DependencyProperty FrozenProperty = DependencyProperty.Register(nameof(Frozen), typeof(bool), T, new PropertyMetadata(false));
        /// <summary>
        /// Identifica la propiedad de dependencia <see cref="GraphTitle"/>.
        /// </summary>
        public static DependencyProperty GraphTitleProperty = DependencyProperty.Register(nameof(GraphTitle), typeof(string), T, new PropertyMetadata(string.Empty));
        /// <summary>
        /// Identifica la propiedad de dependencia <see cref="GraphStroke"/>.
        /// </summary>
        public static DependencyProperty GraphStrokeProperty = DependencyProperty.Register(nameof(GraphStroke), typeof(Brush), T, new PropertyMetadata(SystemColors.HighlightBrush));
        /// <summary>
        /// Identifica la propiedad de dependencia 
        /// <see cref="Graph2Stroke"/>.
        /// </summary>
        public static DependencyProperty Graph2StrokeProperty = DependencyProperty.Register(nameof(Graph2Stroke), typeof(Brush), T, new PropertyMetadata(G2Default));
        /// <summary>
        /// Identifica la propiedad de dependencia 
        /// <see cref="GraphThickness"/>.
        /// </summary>
        public static DependencyProperty GraphThicknessProperty = DependencyProperty.Register(nameof(GraphThickness), typeof(double), T, new PropertyMetadata((double)2));
        /// <summary>
        /// Identifica la propiedad de dependencia 
        /// <see cref="GraphDrawMode"/>.
        /// </summary>
        public static DependencyProperty GraphDrawModeProperty = DependencyProperty.Register(nameof(GraphDrawMode), typeof(EnumGraphDrawMode), T, new PropertyMetadata(EnumGraphDrawMode.Histogram, (d, e) =>
        {
            ((LightGraph)d).RefreshGraph1();
            ((LightGraph)d).RefreshGraph2();
        }));
        /// <summary>
        /// Identifica la propiedad de dependencia 
        /// <see cref="XLabel"/>.
        /// </summary>
        public static DependencyProperty XLabelProperty = DependencyProperty.Register(nameof(XLabel), typeof(string), T, new PropertyMetadata(string.Empty));
        /// <summary>
        /// Identifica la propiedad de dependencia 
        /// <see cref="XPeriod"/>.
        /// </summary>
        public static DependencyProperty xPeriodProperty = DependencyProperty.Register(nameof(XPeriod), typeof(short), T, new PropertyMetadata((short)1, (d, e) => ((LightGraph)d).PlotAxis()), a => (short)a >= 1);
        /// <summary>
        /// Identifica la propiedad de dependencia 
        /// <see cref="SpotLabels"/>.
        /// </summary>
        public static DependencyProperty SpotLabelsProperty = DependencyProperty.Register(
            nameof(SpotLabels), 
            typeof(SpotLabelsDrawMode), 
            T, new PropertyMetadata(
                SpotLabelsDrawMode.DarkBg & SpotLabelsDrawMode.YValues, 
                (d, e) => ((LightGraph)d).RefreshBothSpots()));
        /// <summary>
        /// Identifica la propiedad de dependencia <see cref="SpotPeriod"/>.
        /// </summary>
        public static DependencyProperty SpotPeriodProperty = DependencyProperty.Register(nameof(SpotPeriod), typeof(short), T, new PropertyMetadata(Convert.ToInt16(1), (d, e) => ((LightGraph)d).RefreshBothSpots()), a => (short)a > 0);
        /// <summary>
        /// Identifica la propiedad de dependencia <see cref="YLabel"/>.
        /// </summary>
        public static DependencyProperty YLabelProperty = DependencyProperty.Register(nameof(YLabel), typeof(string), T, new PropertyMetadata(string.Empty));
        /// <summary>
        /// Identifica la propiedad de dependencia <see cref="YMin"/>.
        /// </summary>
        public static DependencyProperty YMinProperty = DependencyProperty.Register(nameof(YMin), typeof(double), T, new PropertyMetadata(double.NaN, (dd, e) =>
        {
            var d = (LightGraph)dd;
            d.YMn.Text = string.Empty;
            if (d.Graph.Count > 0)
            {
                if (double.IsNaN((double)e.NewValue)) d.YMn.Text = d.Graph.Min().ToString();
                else d.YMn.Text = ((double)e.NewValue).ToString();
                d.RefreshGraph1();
            }
        }));
        /// <summary>
        /// Identifica la propiedad de dependencia <see cref="YMax"/>.
        /// </summary>
        public static DependencyProperty YMaxProperty = DependencyProperty.Register(nameof(YMax), typeof(double), T, new PropertyMetadata(double.NaN, (dd, e) =>
        {
            var d = (LightGraph)dd;
            d.YMx.Text = string.Empty;
            if (d.Graph.Count > 0)
            {
                if (double.IsNaN((double)e.NewValue)) d.YMn.Text = d.Graph.Max().ToString();
                else d.YMn.Text = ((double)e.NewValue).ToString();
                d.RefreshGraph1();
            }
        }));
        /// <summary>
        /// Identifica la propiedad de dependencia <see cref="Y2Label"/>.
        /// </summary>
        public static DependencyProperty Y2LabelProperty = DependencyProperty.Register(nameof(Y2Label), typeof(string), T, new PropertyMetadata(string.Empty));
        /// <summary>
        /// Identifica la propiedad de dependencia <see cref="Y2Min"/>.
        /// </summary>
        public static DependencyProperty Y2MinProperty = DependencyProperty.Register(nameof(Y2Min), typeof(double), T, new PropertyMetadata(double.NaN, (dd, e) =>
        {
            var d = (LightGraph)dd;
            d.Y2Mn.Text = string.Empty;
            if (d.Graph2.Count > 0)
            {
                if (double.IsNaN((double)e.NewValue)) d.Y2Mn.Text = d.Graph2.Min().ToString();
                else d.Y2Mn.Text = ((double)e.NewValue).ToString();
                d.RefreshGraph2();
            }
        }));
        /// <summary>
        /// Identifica la propiedad de dependencia <see cref="Y2Max"/>.
        /// </summary>
        public static DependencyProperty Y2MaxProperty = DependencyProperty.Register(nameof(Y2Max), typeof(double), T, new PropertyMetadata(double.NaN, (dd, e) =>
        {
            var d = (LightGraph)dd;
            d.Y2Mx.Text = string.Empty;
            if (d.Graph2.Count > 0)
            {
                if (double.IsNaN((double)e.NewValue)) d.Y2Mn.Text = d.Graph2.Max().ToString();
                else d.Y2Mn.Text = ((double)e.NewValue).ToString();
                d.RefreshGraph2();
            }
        }));
        #endregion
        #region Miembros privados
        TextBlock LblTitle = new TextBlock
        {
            FontSize = 16,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        Grid GrdXLbls = new Grid();
        TextBlock XLbl = new TextBlock { HorizontalAlignment = HorizontalAlignment.Center };
        CheckBox YLbl = new CheckBox
        {
            Visibility = Visibility.Collapsed,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            IsThreeState = true,
            IsChecked = true
        };
        TextBlock YMx = new TextBlock
        {
            VerticalAlignment = VerticalAlignment.Bottom,
            HorizontalAlignment = HorizontalAlignment.Right
        };
        TextBlock YMn = new TextBlock
        {
            VerticalAlignment = VerticalAlignment.Bottom,
            HorizontalAlignment = HorizontalAlignment.Left
        };
        CheckBox Y2Lbl = new CheckBox
        {
            Visibility = Visibility.Collapsed,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            IsThreeState = true,
            IsChecked = true
        };
        TextBlock Y2Mx = new TextBlock { HorizontalAlignment = HorizontalAlignment.Right };
        TextBlock Y2Mn = new TextBlock { HorizontalAlignment = HorizontalAlignment.Left };
        Grid GrdGraphBG = new Grid();
        Grid GrdGraph = new Grid();
        Polyline Grp1 = new Polyline { Stroke = SystemColors.HighlightBrush };
        Polyline Grp2 = new Polyline { Stroke = G2Default };
        Grid GrdGraphFG = new Grid();
        Grid GrdGraphFG2 = new Grid();
        #endregion
        #region Propiedades
        /// <summary>
        /// Congela o descongela la actualización del control.
        /// </summary>
        /// <remarks>
        /// Mientras el control esté congelado, los redibujos no funcionarán,
        /// lo que puede causar que el gráfico se visualice incorrectamente.
        /// </remarks>
        public bool Frozen
        {
            get => (bool)GetValue(FrozenProperty);
            set => SetValue(FrozenProperty, value);
        }
        /// <summary>
        /// Obtiene o establece los elementos de la gráfica.
        /// </summary>
        /// <remarks>
        /// Esta no es una propiedad de dependencia, debido a algunas
        /// complicaciones de eventos.
        /// </remarks>
        public ListEx<double> Graph { get; set; } = new ListEx<double>();
        /// <summary>
        /// Obtiene o establece los elementos de la gráfica secundaria.
        /// </summary>
        /// <remarks>
        /// Esta no es una propiedad de dependencia, debido a algunas 
        /// complicaciones de eventos.
        /// </remarks>
        public ListEx<double> Graph2 { get; set; } = new ListEx<double>();
        /// <summary>
        /// Determina el modo de dibujo del gráfico.
        /// </summary>
        public EnumGraphDrawMode GraphDrawMode
        {
            get => (EnumGraphDrawMode)GetValue(GraphDrawModeProperty);
            set => SetValue(GraphDrawModeProperty, value);
        }
        /// <summary>
        /// Ajusta el espacio a la derecha del gráfico, en unidades de puntos.
        /// </summary>
        public short GraphPadding { get; set; } = 1;
        /// <summary>
        /// Obtiene o establece el <see cref="Brush"/> del gráfico.
        /// </summary>
        public Brush GraphStroke
        {
            get => (Brush)GetValue(GraphStrokeProperty);
            set => SetValue(GraphStrokeProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el <see cref="Brush"/> del gráfico secundario.
        /// </summary>
        public Brush Graph2Stroke
        {
            get => (Brush)GetValue(Graph2StrokeProperty);
            set => SetValue(Graph2StrokeProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el grosor de la línea del gráfico.
        /// </summary>
        public double GraphThickness
        {
            get => (double)GetValue(GraphThicknessProperty);
            set => SetValue(GraphThicknessProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el título principal del gráfico.
        /// </summary>
        /// <returns>
        /// Un <see cref="string"/> con el título establecido para el control.
        /// </returns>
        public string GraphTitle
        {
            get => (string)GetValue(GraphTitleProperty);
            set => SetValue(GraphTitleProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el modo de dibujo de las etiquetas dentro del gráfico
        /// </summary>
        /// <returns></returns>
        public SpotLabelsDrawMode SpotLabels
        {
            get => (SpotLabelsDrawMode)GetValue(SpotLabelsProperty);
            set => SetValue(SpotLabelsProperty, value);
        }
        /// <summary>
        /// Obtiene o establece la cantidad de pasos para dibujar etiquetas en 
        /// los puntos del gráfico.
        /// </summary>
        public short SpotPeriod
        {
            get => (short)GetValue(SpotPeriodProperty);
            set => SetValue(SpotPeriodProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el título del eje X del gráfico.
        /// </summary>
        /// <returns>
        /// Un <see cref="string"/> con el nombre actualmente establecido para
        /// el eje X del gráfico.
        /// </returns>
        public string XLabel
        {
            get => (string)GetValue(XLabelProperty);
            set => SetValue(XLabelProperty, value);
        }
        /// <summary>
        /// Obtiene o establece las etiquetas del eje X.
        /// </summary>
        /// <remarks>
        /// Esta no es una propiedad de dependencia, debido a algunas 
        /// complicaciones de eventos.
        /// </remarks>
        public ListEx<string> XLabels { get; set; } = new ListEx<string>();
        /// <summary>
        /// Obtiene o establece el período de la rejilla mayor del eje X.
        /// </summary>
        /// <returns>
        /// La cantidad de pasos de la rejilla necesarios para un paso mayor.
        /// </returns>
        public short XPeriod
        {
            get => (short)GetValue(xPeriodProperty);
            set => SetValue(xPeriodProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el título del eje Y del gráfico.
        /// </summary>
        public string YLabel
        {
            get => (string)GetValue(YLabelProperty);
            set => SetValue(YLabelProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el mínimo del eje Y del gráfico.
        /// </summary>
        public double YMin
        {
            get => (double)GetValue(YMinProperty);
            set => SetValue(YMinProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el máximo del eje Y del gráfico.
        /// </summary>
        public double YMax
        {
            get => (double)GetValue(YMaxProperty);
            set => SetValue(YMaxProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el título del eje Y del gráfico secundario.
        /// </summary>
        public string Y2Label
        {
            get => (string)GetValue(Y2LabelProperty);
            set => SetValue(Y2LabelProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el mínimo del eje Y del gráfico secundario.
        /// </summary>
        public double Y2Min
        {
            get => (double)GetValue(Y2MinProperty);
            set => SetValue(Y2MinProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el máximo del eje Y del gráfico secundario.
        /// </summary>
        public double Y2Max
        {
            get => (double)GetValue(Y2MaxProperty);
            set => SetValue(Y2MaxProperty, value);
        }
        #endregion
        #region Métodos internos
        bool AmIValid() => Algebra.ArePositive(ActualHeight, ActualWidth);
        void ClearGrp(Polyline grp, TextBlock mx, TextBlock mn)
        {
            if (!AmIValid()) return;
            grp.Points.Clear();
            grp.Fill = null;
            mx.Text = string.Empty;
            mn.Text = string.Empty;
        }
        void ClearLabels(Grid g)
        {
            if (!AmIValid()) return;
            g.Children.Clear();
        }
        void PlotAxis()
        {
            if (!AmIValid()) return;
            var l = GrdGraphBG.ActualWidth / ((XLabels.Count - 1) + GraphPadding);
            if (l == 0) return;
            GrdGraphBG.Children.Clear();
            if (XLabels.Count > 0)
            {
                for (double j = 0; j <= GrdGraphBG.ActualWidth; j += l)
                {
                    var k = new Line()
                    {
                        X1 = j,
                        X2 = j,
                        Y1 = 0,
                        Y2 = GrdGraphBG.ActualHeight,
                        Stroke = Foreground
                    };
                    k.StrokeThickness = (System.Math.Round(j / l) % XPeriod == 0 && XPeriod > 1) ? 2 : 0.5;
                    GrdGraphBG.Children.Add(k);
                }
            }
        }
        void PlotLabels()
        {
            if (!AmIValid()) return;
            var l = GrdGraphBG.ActualWidth / ((XLabels.Count - 1) + GraphPadding);
            if (l == 0) return;
            GrdXLbls.Children.Clear();
            if (XLabels.Count > 0)
            {
                for (double j = 0; j <= GrdGraphBG.ActualWidth - (GraphPadding * l) + 1; j += (l * XPeriod))
                {
                    if (System.Math.Round(j / l) < XLabels.Count)
                    {
                        var k = new TextBlock()
                        {
                            Text = XLabels[(int)(j / l)],
                            Margin = new Thickness(j, 0, 0, 0)
                        };
                        GrdXLbls.Children.Add(k);
                    }
                }
            }
        }
        void PlotGrp(Polyline grp, ListEx<double> lst, TextBlock mx, TextBlock mn, double max, double min, CheckBox chk)
        {
            if (!AmIValid()) return;
            ClearGrp(grp, mx, mn);
            if (lst.Count == 0) return;
            if (double.IsNaN(max)) max = lst.Max();
            mx.Text = max.ToString();
            if (double.IsNaN(min)) min = lst.Min();
            mn.Text = min.ToString();
            var l = GrdGraph.ActualWidth / ((lst.Count - 1) + GraphPadding); //Unidades de gráfica
            double k = 0; //step en unidades de gráfica
            var a = 0; //step simple
            foreach (var j in lst.ToPercent(min, max))
            {
                if (j.IsValid())
                {
                    var p = new System.Windows.Point(k, (GrdGraph.ActualHeight - j * GrdGraph.ActualHeight));
                    grp.Points.Add(p);
                    if ((GraphDrawMode & EnumGraphDrawMode.Bars) != 0) grp.Points.Add(new System.Windows.Point(p.X + l, p.Y));
                }
                k += l;
                a += 1;
            }
            if ((GraphDrawMode & EnumGraphDrawMode.Filled) != 0)
            {
                if ((GraphDrawMode & EnumGraphDrawMode.Bars) != 0)
                    grp.Points.Add(new System.Windows.Point(k, GrdGraph.ActualHeight + GraphThickness * 2));
                else
                    grp.Points.Add(new System.Windows.Point(k - l, GrdGraph.ActualHeight + GraphThickness * 2));
                grp.Points.Add(new System.Windows.Point(0, GrdGraph.ActualHeight + GraphThickness * 2));
                var _with2 = ((SolidColorBrush)grp.Stroke).Color;
                var x = System.Windows.Media.Color.FromArgb((byte)(_with2.A / 2), _with2.R, _with2.G, _with2.B);
                grp.Fill = new SolidColorBrush(x);
            }
        }
        void PlotSpotLabels(Polyline Ps, ListEx<double> grp, Grid g)
        {
            if (!AmIValid()) return;
            g.Children.Clear();
            var mde = SpotLabels;
            if (grp.Count > 1 && Convert.ToBoolean(mde))
            {
                double mi = 0;
                double ma = 0;
                var j = 0;
                byte drop = 0;
                var tot = grp.Sum();
                ma = double.IsNaN(YMax) ? grp.Max() : YMax;
                mi = double.IsNaN(YMin) ? grp.Min() : YMin;
                foreach (var p in Ps.Points)
                {
                    if (Algebra.AreValid(p.X, p.Y) && drop == 0)
                    {
                        var lb = new TextBlock()
                        {
                            Foreground = ((mde & SpotLabelsDrawMode.GraphColor) != 0 ? Ps.Stroke : Foreground),
                            Background = (mde & SpotLabelsDrawMode.DarkBg) != 0 ? Brushes.Black : null,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top
                        };
                        var _with3 = new StringBuilder();
                        if ((mde & SpotLabelsDrawMode.YValues) != 0) _with3.Append(grp[j]);
                        if ((mde & SpotLabelsDrawMode.Percent) != 0) _with3.Append($"{(grp[j] / tot):0.0%}");
                        lb.Text = _with3.ToString();
                        lb.Margin = new Thickness(p.X, p.Y, 0, 0);
                        g.Children.Add(lb);
                    }
                    if ((GraphDrawMode & EnumGraphDrawMode.Bars) != 0)
                    {
                        drop = (byte)(1 - drop);
                        if (drop == 1)
                            j -= 1;
                    }
                    j += 1;
                    if (j >= grp.Count) break;
                }
            }
        }
        void RefreshBase()
        {
            if (Frozen) return;
            PlotAxis();
            PlotLabels();
        }
        void RefreshGraph1()
        {
            if (Frozen) return;
            switch (YLbl.IsChecked)
            {
                case true:
                    PlotGrp(Grp1, Graph, YMx, YMn, YMax, YMin, YLbl);
                    ClearLabels(GrdGraphFG);
                    break;
                case false:
                    ClearGrp(Grp1, YMx, YMn);
                    ClearLabels(GrdGraphFG);
                    break;
                default:
                    PlotGrp(Grp1, Graph, YMx, YMn, YMax, YMin, YLbl);
                    PlotSpotLabels(Grp1, Graph, GrdGraphFG);
                    break;
            }
        }
        void RefreshGraph2()
        {
            if (Frozen) return;
            switch (Y2Lbl.IsChecked)
            {
                case true:
                    PlotGrp(Grp2, Graph2, Y2Mx, Y2Mn, Y2Max, Y2Min, Y2Lbl);
                    ClearLabels(GrdGraphFG2);
                    break;
                case false:
                    ClearGrp(Grp2, Y2Mx, Y2Mn);
                    ClearLabels(GrdGraphFG2);
                    break;
                default:
                    PlotGrp(Grp2, Graph2, Y2Mx, Y2Mn, Y2Max, Y2Min, Y2Lbl);
                    PlotSpotLabels(Grp2, Graph2, GrdGraphFG2);
                    break;
            }
        }
        void RefreshBothSpots()
        {
            if (Frozen) return;
            switch (YLbl.IsChecked)
            {
                case true:
                case false:
                    ClearLabels(GrdGraphFG);
                    break;
                default:
                    PlotSpotLabels(Grp1, Graph, GrdGraphFG);
                    break;
            }
            switch (Y2Lbl.IsChecked)
            {
                case true:
                case false:
                    ClearLabels(GrdGraphFG2);
                    break;
                default:
                    PlotSpotLabels(Grp2, Graph2, GrdGraphFG2);
                    break;
            }
        }
        #endregion
        #region Control de eventos
        void Rfrsh(object sender, EventArgs e)
        {
            if (sender.Is(Graph) || sender.Is(YLbl)) RefreshGraph1();
            else if (sender.Is(Graph2) || sender.Is(Y2Lbl)) RefreshGraph2();
            else if (sender.Is(XLabels)) RefreshBase();
        }
        void Root_Change(object sender, EventArgs e)
        {
            RefreshBase();
            RefreshGraph1();
            RefreshGraph2();
        }
        #endregion
        #region Métodos públicos
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="LightGraph"/>.
        /// </summary>
        public LightGraph()
        {
            // Inicializar controles...
            var pnlroot = new DockPanel();
            var lba = new TextBlock { LayoutTransform = new RotateTransform(-90) };
            var rct1 = new Rectangle();
            var dp1 = new DockPanel();
            var gr1 = new Grid { LayoutTransform = new RotateTransform(-90) };
            var gr2 = new Grid { LayoutTransform = new RotateTransform(-90) };
            var gr3 = new Grid();
            var gr4 = new Grid();
            DockPanel.SetDock(LblTitle, Dock.Top);
            DockPanel.SetDock(dp1, Dock.Bottom);
            DockPanel.SetDock(gr1, Dock.Left);
            DockPanel.SetDock(gr2, Dock.Right);
            DockPanel.SetDock(lba, Dock.Left);
            Grid.SetRow(XLbl, 1);

            // Establecer bindings...
            YLbl.SetBinding(BackgroundProperty, new Binding(nameof(Grp1.Stroke)) { Source = Grp1 });
            YLbl.SetBinding(ForegroundProperty, new Binding(nameof(Foreground)) { Source = this });
            YLbl.SetBinding(ToggleButton.IsThreeStateProperty, new Binding(nameof(SpotLabels))
            {
                Source = this,
                Converter = new BoolFlagConverter<SpotLabelsDrawMode>()
            });
            YLbl.SetBinding(ContentProperty, new Binding(nameof(YLabel)) { Source = this });
            YLbl.SetBinding(VisibilityProperty, new Binding(nameof(YLabel))
            {
                Source = this,
                Converter = new StringVisibilityConverter()
            });
            Y2Lbl.SetBinding(BackgroundProperty, new Binding(nameof(Grp2.Stroke)) { Source = Grp2 });
            Y2Lbl.SetBinding(ForegroundProperty, new Binding(nameof(Foreground)) { Source = this });
            Y2Lbl.SetBinding(ToggleButton.IsThreeStateProperty, new Binding(nameof(SpotLabels))
            {
                Source = this,
                Converter = new BoolFlagConverter<SpotLabelsDrawMode>()
            });
            Y2Lbl.SetBinding(ContentProperty, new Binding(nameof(Y2Label)) { Source = this });
            Y2Lbl.SetBinding(VisibilityProperty, new Binding(nameof(Y2Label))
            {
                Source = this,
                Converter = new StringVisibilityConverter()
            });
            Grp1.SetBinding(Shape.StrokeProperty, new Binding(nameof(GraphStroke)) { Source = this });
            Grp1.SetBinding(Shape.StrokeThicknessProperty, new Binding(nameof(GraphThickness)) { Source = this });
            Grp2.SetBinding(Shape.StrokeProperty, new Binding(nameof(Graph2Stroke)) { Source = this });
            Grp2.SetBinding(Shape.StrokeThicknessProperty, new Binding(nameof(GraphThickness)) { Source = this });
            XLbl.SetBinding(TextBlock.TextProperty, new Binding(nameof(XLabel)) { Source = this });
            LblTitle.SetBinding(TextBlock.TextProperty, new Binding(nameof(GraphTitle)) { Source = this });

            // Crear diseño de control...
            dp1.Children.Add(lba);
            gr4.RowDefinitions.Add(new RowDefinition());
            gr4.RowDefinitions.Add(new RowDefinition());
            gr4.Children.Add(GrdXLbls);
            gr4.Children.Add(XLbl);
            dp1.Children.Add(gr4);
            var _with4 = gr1.Children;
            _with4.Add(YLbl);
            _with4.Add(YMx);
            _with4.Add(YMn);
            var _with5 = gr2.Children;
            _with5.Add(Y2Lbl);
            _with5.Add(Y2Mx);
            _with5.Add(Y2Mn);
            GrdGraph.Children.Add(Grp1);
            GrdGraph.Children.Add(Grp2);
            var _with6 = gr3.Children;
            _with6.Add(GrdGraphBG);
            _with6.Add(GrdGraph);
            _with6.Add(GrdGraphFG);
            _with6.Add(GrdGraphFG2);
            var _with7 = pnlroot.Children;
            _with7.Add(LblTitle);
            _with7.Add(dp1);
            _with7.Add(gr1);
            _with7.Add(gr2);
            _with7.Add(gr3);
            Content = pnlroot;

            // Conectar eventos...
            Graph.AddedItem += Rfrsh;
            Graph.RemovedItem += Rfrsh;
            Graph.ListCleared += Rfrsh;
            Graph.ListUpdated += Rfrsh;
            Graph2.AddedItem += Rfrsh;
            Graph2.RemovedItem += Rfrsh;
            Graph2.ListCleared += Rfrsh;
            Graph2.ListUpdated += Rfrsh;
            XLabels.AddedItem += Rfrsh;
            XLabels.RemovedItem += Rfrsh;
            XLabels.ListCleared += Rfrsh;
            XLabels.ListUpdated += Rfrsh;
            YLbl.Checked += Rfrsh;
            YLbl.Unchecked += Rfrsh;
            YLbl.Indeterminate += Rfrsh;
            Y2Lbl.Checked += Rfrsh;
            Y2Lbl.Unchecked += Rfrsh;
            Y2Lbl.Indeterminate += Rfrsh;
            SizeChanged += Root_Change;
            Loaded += Root_Change;
        }
        /// <summary>
        /// Obliga al control a volver a dibujarse.
        /// </summary>
        /// <remarks>
        /// Este método omite el valor de la propiedad <see cref="Frozen"/>.
        /// </remarks>
        public void Redraw() => Root_Change(this, EventArgs.Empty);
        #endregion
    }
}