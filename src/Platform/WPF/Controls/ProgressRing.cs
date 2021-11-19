/*
ProgressRing.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Math;
using TheXDS.MCART.Types.Extensions;
using static TheXDS.MCART.WpfUi;

namespace TheXDS.MCART.Controls
{
    /// <summary>
    /// Control que presenta visualmente el progreso de una operación.
    /// </summary>
    public partial class ProgressRing : UserControl
    {
        #region Miembros estáticos
        private static void Updt(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ProgressRing)d).Draw();
        }

        private static void Updt2(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressRing? p = (ProgressRing)d;
            p.SetValue(IsIndeterminateProperty, !p.Value.IsBetween(p.Minimum, p.Maximum));
            p.BgDraw();
            p.Draw();
        }

        private static void Updt3(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressRing? p = (ProgressRing)d;
            p.BgDraw();
            p.Draw();
        }

        private static void TxtFmt(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressRing? p = (ProgressRing)d;
            p._txtPercent.Text = string.Format(p.TextFormat, p.Value);
        }
        #endregion

        #region Propiedades de dependencia
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Angle"/>.
        /// </summary>
        public static DependencyProperty AngleProperty = DependencyProperty.Register(
            nameof(Angle), typeof(float), typeof(ProgressRing),
            new PropertyMetadata(0f, Updt3), a => ((float)a).IsBetween(0f, 360f));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Fill"/>.
        /// </summary>
        public static DependencyProperty FillProperty = DependencyProperty.Register(
            nameof(Fill), typeof(Brush), typeof(ProgressRing),
            new PropertyMetadata(SystemColors.HighlightBrush));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="FullAngle"/>.
        /// </summary>
        public static DependencyProperty FullAngleProperty = DependencyProperty.Register(
                nameof(FullAngle), typeof(float), typeof(ProgressRing),
                new PropertyMetadata(360f, Updt3), a => ((float)a).IsBetween(0f, 360f));
        /// <summary>
        /// Identifica a la propiedad de dependencia 
        /// <see cref="IsIndeterminate"/>.
        /// </summary>
        public static DependencyProperty IsIndeterminateProperty = DependencyProperty.Register(
            nameof(IsIndeterminate), typeof(bool), typeof(ProgressRing),
            new PropertyMetadata(false, Updt3));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Maximum"/>.
        /// </summary>
        public static DependencyProperty MaxProperty = DependencyProperty.Register(
            nameof(Maximum), typeof(double), typeof(ProgressRing),
            new PropertyMetadata(100.0, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
            {
                ProgressRing? p = (ProgressRing)d;
                double v = (double)e.NewValue;
                if (double.IsNaN(v)) throw new ArgumentException();
                if (v < p.Minimum) throw new ArgumentOutOfRangeException(nameof(v));
                if (!double.IsNaN(p.Redline) && p.Redline > v)
                    p.Redline = v;
                Updt2(p, e);
            }));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Minimum"/>.
        /// </summary>
        public static DependencyProperty MinProperty = DependencyProperty.Register(
            nameof(Minimum), typeof(double), typeof(ProgressRing),
            new PropertyMetadata(0.0, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
            {
                ProgressRing? p = (ProgressRing)d;
                double v = (double)e.NewValue;
                if (double.IsNaN(v)) throw new ArgumentException();
                if (v > p.Maximum) throw new ArgumentOutOfRangeException(nameof(v));
                Updt2(p, e);
            }));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Redline"/>.
        /// </summary>
        public static DependencyProperty RedlineProperty = DependencyProperty.Register(
            nameof(Redline), typeof(double), typeof(ProgressRing),
            new PropertyMetadata(double.NaN, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
            {
                ProgressRing? p = (ProgressRing)d;
                double v = (double)e.NewValue;
                if (!(double.IsNaN(v) || v.IsBetween(p.Minimum, p.Maximum)))
                    throw new ArgumentOutOfRangeException();
                Updt3(p, e);
            }));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="RedlineBrush"/>.
        /// </summary>
        public static DependencyProperty RedlineBrushProperty = DependencyProperty.Register(
                nameof(RedlineBrush), typeof(Brush), typeof(ProgressRing),
                new PropertyMetadata(new Types.Color(255, 0, 0, 128).Brush()));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="RingStroke"/>.
        /// </summary>
        public static DependencyProperty RingStrokeProperty = DependencyProperty.Register(
            nameof(RingStroke), typeof(Brush), typeof(ProgressRing),
            new PropertyMetadata(SystemColors.ControlDarkBrush));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Sweep"/>.
        /// </summary>
        public static DependencyProperty SweepProperty = DependencyProperty.Register(
            nameof(Sweep), typeof(SweepDirection), typeof(ProgressRing),
            new PropertyMetadata(SweepDirection.Clockwise, Updt),
            (a) => typeof(SweepDirection).IsEnumDefined(a));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="TextFormat"/>.
        /// </summary>
        public static DependencyProperty TextFormatProperty = DependencyProperty.Register(
            nameof(TextFormat), typeof(string), typeof(ProgressRing),
            new PropertyMetadata("{0:0.0}%", TxtFmt));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="TextSize"/>.
        /// </summary>
        public static DependencyProperty TextSizeProperty = DependencyProperty.Register(
                nameof(TextSize), typeof(double), typeof(ProgressRing),
                new PropertyMetadata(16.0));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Thickness"/>.
        /// </summary>
        public static DependencyProperty ThicknessProperty = DependencyProperty.Register(
            nameof(Thickness), typeof(double), typeof(ProgressRing),
            new PropertyMetadata(4.0, Updt3));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Value"/>.
        /// </summary>
        public static DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(double), typeof(ProgressRing),
            new PropertyMetadata(0.0, Updt2));
        #endregion

        #region Campos privados
        private bool _amIAnimated = false;
        private readonly Path _ellBg = new();
        private readonly Path _redlinePath = new() { Stroke = new Types.Color(255, 0, 0, 192).Brush() };
        private readonly Path _pth = new()
        {
            RenderTransform = new RotateTransform(),
            RenderTransformOrigin = new Point(0.5, 0.5)
        };
        private readonly DoubleAnimationUsingKeyFrames _spin = new()
        {
            RepeatBehavior = RepeatBehavior.Forever
        };
        private readonly TextBlock _txtPercent = new()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        #endregion

        #region Propiedades
        /// <summary>
        /// Obtiene o establece el ángulo desde el que se empezará a dibujar el
        /// relleno de este <see cref="ProgressRing"/>. 
        /// </summary>
        public float Angle
        {
            get => (float)GetValue(AngleProperty);
            set => SetValue(AngleProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el <see cref="Brush"/> a utilizar para dibujar
        /// el relleno del anillo de este <see cref="ProgressRing"/>.
        /// </summary>
        public Brush Fill
        {
            get => (Brush)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el ángulo que indica un valor completo del
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public float FullAngle
        {
            get => (float)GetValue(FullAngleProperty);
            set => SetValue(FullAngleProperty, value);
        }
        /// <summary>
        /// Obtiene o establece un valor que indica si se mostrará un estado
        /// indeterminado en este <see cref="ProgressRing"/>.
        /// </summary>
        public bool IsIndeterminate
        {
            get => (bool)GetValue(IsIndeterminateProperty);
            set => SetValue(IsIndeterminateProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el valor máximo de este
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public double Maximum
        {
            get => (double)GetValue(MaxProperty);
            set => SetValue(MaxProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el valor mínimo de este
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public double Minimum
        {
            get => (double)GetValue(MinProperty);
            set => SetValue(MinProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el punto en el que inicia la línea de límite.
        /// </summary>
        public double Redline
        {
            get => (double)GetValue(RedlineProperty);
            set => SetValue(RedlineProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el color de la línea de límite.
        /// </summary>
        public Brush RedlineBrush
        {
            get => (Brush)GetValue(RedlineBrushProperty);
            set => SetValue(RedlineBrushProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el <see cref="Brush"/> a utilizar para dibujar
        /// el fondo del anillo de este <see cref="ProgressRing"/>.
        /// </summary>
        public Brush RingStroke
        {
            get => (Brush)GetValue(RingStrokeProperty);
            set => SetValue(RingStrokeProperty, value);
        }
        /// <summary>
        /// Obtiene o establece la dirección en la cual se rellenará este
        /// <see cref="ProgressRing"/>. 
        /// </summary>
        public SweepDirection Sweep
        {
            get => (SweepDirection)GetValue(SweepProperty);
            set => SetValue(SweepProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el formato de texto a aplicar al centro de este
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public string TextFormat
        {
            get => (string)GetValue(TextFormatProperty);
            set => SetValue(TextFormatProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el tamaño del texto de etiqueta del
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public double TextSize
        {
            get => (double)GetValue(TextSizeProperty);
            set => SetValue(TextSizeProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el grosor del anillo de este
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public double Thickness
        {
            get => (double)GetValue(ThicknessProperty);
            set => SetValue(ThicknessProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el valor a representar en este
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        #endregion

        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public ProgressRing()
        {
            _ellBg.SetBinding(Shape.StrokeThicknessProperty, new Binding(nameof(Thickness)) { Source = this });
            _ellBg.SetBinding(Shape.StrokeProperty, new Binding(nameof(RingStroke)) { Source = this });
            _redlinePath.SetBinding(Shape.StrokeThicknessProperty, new Binding(nameof(Thickness)) { Source = this });
            _redlinePath.SetBinding(Shape.StrokeProperty, new Binding(nameof(RedlineBrush)) { Source = this });
            _pth.SetBinding(Shape.StrokeThicknessProperty, new Binding(nameof(Thickness)) { Source = this });
            _pth.SetBinding(Shape.StrokeProperty, new Binding(nameof(Fill)) { Source = this });
            _spin.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 1)),
                Value = 360.0
            });
            Grid? a = new() { Width = 100 };
            a.SetBinding(HeightProperty, new Binding(nameof(Width)) { Source = a, Mode = BindingMode.TwoWay });
            a.Children.Add(_ellBg);
            a.Children.Add(_pth);
            a.Children.Add(_redlinePath);
            _txtPercent.SetBinding(TextBlock.FontSizeProperty, new Binding(nameof(TextSize)) { Source = this });
            a.Children.Add(_txtPercent);
            Content = new Viewbox() { Child = a };
            Loaded += OnLoaded;
        }

        #region Métodos privados
        private void BgDraw()
        {
            double radius = 50 - (Thickness / 2);
            double fullAngle = FullAngle.Clamp(0f, 359.999f);
            if (!IsIndeterminate)
                _ellBg.Data = GetCircleArc(radius, Angle, Angle + fullAngle, Thickness);
            else
                _ellBg.Data = GetCircleArc(radius, 0, 359.999, Thickness);

            if (!double.IsNaN(Redline))
            {
                _redlinePath.Data = GetCircleArc(radius, (((Redline - Minimum) / (Maximum - Minimum)) * FullAngle) + Angle, Angle + FullAngle, Thickness);
            }
            else
                _redlinePath.Data = null;
        }

        private void Draw()
        {
            double radius = 50 - (Thickness / 2);
            if (!_pth.IsLoaded) return;
            RotateTransform? x = (RotateTransform)_pth.RenderTransform;
            if (!IsIndeterminate)
            {
                _amIAnimated = false;
                x.BeginAnimation(RotateTransform.AngleProperty, null);
                switch (Sweep)
                {
                    case SweepDirection.Clockwise:
                        _pth.Data = GetCircleArc(radius, Angle, Angle + (((Value - Minimum) / (Maximum - Minimum)) * FullAngle).Clamp(0, 359.999), Thickness);
                        break;
                    case SweepDirection.CounterClockwise:
                        _pth.Data = GetCircleArc(radius, Angle + (((Maximum - (Value - Minimum)) / (Maximum - Minimum)) * FullAngle).Clamp(0, 359.999), Angle + FullAngle, Thickness);
                        break;
                    default:
                        break;
                }

                _txtPercent.Text = string.Format(TextFormat, Value);
            }
            else if (!_amIAnimated)
            {
                _pth.Data = GetCircleArc(radius, 90, Thickness);
                _amIAnimated = true;
                x.BeginAnimation(RotateTransform.AngleProperty, _spin);
                _txtPercent.Text = "...";
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            BgDraw();
            Draw();
        }
        #endregion
    }
}