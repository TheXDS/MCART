//
//  ProgressRing.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using static MCART.UI;

namespace MCART.Controls
{
    public partial class ProgressRing : UserControl
    {
        #region Miembros estáticos
        static void Updt(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ProgressRing)d).Draw();
        }
        static void Updt2(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressRing p = (ProgressRing)d;
            p.SetValue(IsIndeterminateProperty, !p.Value.IsBetween(p.Minimum, p.Maximum));
            p.BgDraw();
            p.Draw();
        }
        static void Updt3(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressRing p = (ProgressRing)d;
            p.BgDraw();
            p.Draw();
        }
        static void TxtFmt(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressRing p = (ProgressRing)d;
            p.TxtPercent.Text = string.Format(p.TextFormat, p.Value);
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
            new PropertyMetadata(false, Updt));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Maximum"/>.
        /// </summary>
        public static DependencyProperty MaxProperty = DependencyProperty.Register(
            nameof(Maximum), typeof(double), typeof(ProgressRing),
            new PropertyMetadata(100.0, Updt2));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Minimum"/>.
        /// </summary>
        public static DependencyProperty MinProperty = DependencyProperty.Register(
            nameof(Minimum), typeof(double), typeof(ProgressRing),
            new PropertyMetadata(0.0, Updt2));
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
            new PropertyMetadata(4.0, Updt));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Value"/>.
        /// </summary>
        public static DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(double), typeof(ProgressRing),
            new PropertyMetadata(0.0, Updt2));
        #endregion
        #region Campos privados
        bool amIAnimated = false;
        Path ellBg = new Path();
        Path pth = new Path()
        {
            RenderTransform = new RotateTransform(),
            RenderTransformOrigin = new Point(0.5, 0.5)
        };
        DoubleAnimationUsingKeyFrames spin = new DoubleAnimationUsingKeyFrames()
        {
            RepeatBehavior = RepeatBehavior.Forever
        };
        TextBlock TxtPercent = new TextBlock()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        #endregion
        #region Métodos privados
        void BgDraw()
        {
            double radius = 50 - Thickness / 2;
            if (!IsIndeterminate)
                ellBg.Data = GetCircleArc(radius, Angle, Angle + FullAngle.Clamp(0f, 359.999f), Thickness);
            else
                ellBg.Data = GetCircleArc(radius, 0, 359.999, Thickness);
        }
        void Draw()
        {
            double radius = 50 - Thickness / 2;
            if (!pth.IsLoaded) return;
            RotateTransform x = (RotateTransform)pth.RenderTransform;
            if (!IsIndeterminate)
            {
                amIAnimated = false;
                x.BeginAnimation(RotateTransform.AngleProperty, null);
                pth.Data = GetCircleArc(radius, Angle, Angle + (((Value - Minimum) / (Maximum - Minimum)) * FullAngle).Clamp(0, 359.999), Thickness);
                TxtPercent.Text = string.Format(TextFormat, Value);
            }
            else if (!amIAnimated)
            {
                pth.Data = GetCircleArc(radius, 90, Thickness);
                amIAnimated = true;
                x.BeginAnimation(RotateTransform.AngleProperty, spin);
                TxtPercent.Text = "...";
            }
        }
        void OnLoaded(object sender, RoutedEventArgs e)
        {
            BgDraw();
            Draw();
        }
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
            ellBg.SetBinding(Shape.StrokeThicknessProperty, new Binding(nameof(Thickness)) { Source = this });
            ellBg.SetBinding(Shape.StrokeProperty, new Binding(nameof(RingStroke)) { Source = this });
            pth.SetBinding(Shape.StrokeThicknessProperty, new Binding(nameof(Thickness)) { Source = this });
            pth.SetBinding(Shape.StrokeProperty, new Binding(nameof(Fill)) { Source = this });
            spin.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 1)),
                Value = 360.0
            });
            Grid a = new Grid { Width = 100 };
            a.SetBinding(HeightProperty, new Binding(nameof(Width)) { Source = a, Mode = BindingMode.TwoWay });
            a.Children.Add(ellBg);
            a.Children.Add(pth);
            TxtPercent.SetBinding(TextBlock.FontSizeProperty, new Binding(nameof(TextSize)) { Source = this });
            a.Children.Add(TxtPercent);
            Content = new Viewbox() { Child = a };
            Loaded += OnLoaded;
        }
    }
}