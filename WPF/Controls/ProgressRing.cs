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
        static Type T = typeof(ProgressRing);
        static void Redraw(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressRing p = (ProgressRing)d;
            p.SetControlSize();
            p.Draw();
        }
        static void Updt(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ProgressRing)d).Draw();
        }
        static void Updt2(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressRing p = (ProgressRing)d;
            p.SetValue(IsIndeterminateProperty, !p.Value.IsBetween(p.Min, p.Max));
        }
        static void TxtFmt(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressRing p = (ProgressRing)d;
            p.TxtPercent.Text = string.Format(p.TextFormat, p.Value);
        }
        TextBlock TxtPercent = new TextBlock()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        Ellipse ellBg = new Ellipse();
        Path pth = new Path()
        {
            RenderTransform = new RotateTransform(),
            RenderTransformOrigin = new Point(0.5, 0.5)
        };
        DoubleAnimationUsingKeyFrames spin = new DoubleAnimationUsingKeyFrames()
        {
            RepeatBehavior = RepeatBehavior.Forever
        };
        bool amIAnimated = false;
        void OnLoaded(object sender, RoutedEventArgs e)
        {
            Redraw(this, new DependencyPropertyChangedEventArgs());
        }
        void Draw()
        {
            if (!pth.IsLoaded) return;
            RotateTransform x = (RotateTransform)pth.RenderTransform;
            if (!IsIndeterminate)
            {
                amIAnimated = false;
                x.BeginAnimation(RotateTransform.AngleProperty, null);
                pth.Data = GetCircleArc(Radius, (((Value - Min) / (Max - Min)) * 360).Clamp(0, 359.999), Thickness);
                TxtPercent.Text = string.Format(TextFormat, Value);
            }
            else if (!amIAnimated)
            {
                pth.Data = GetCircleArc(Radius, 90, Thickness);
                amIAnimated = true;
                x.BeginAnimation(RotateTransform.AngleProperty, spin);
                TxtPercent.Text = "...";
            }
        }
        void SetControlSize()
        {
            Width = (double)GetValue(RadiusProperty) * 2 + (double)GetValue(ThicknessProperty);
        }
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Angle"/>.
        /// </summary>
        public static DependencyProperty AngleProperty = DependencyProperty.Register(
            nameof(Angle), typeof(float), T,
            new PropertyMetadata(0.0f), (a) => ((float)a).IsBetween(0.0f, 360.0f));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Sweep"/>.
        /// </summary>
        public static DependencyProperty SweepProperty = DependencyProperty.Register(
            nameof(Sweep), typeof(SweepDirection), T,
            new PropertyMetadata(SweepDirection.Clockwise, Updt2),
            (a) => typeof(SweepDirection).IsEnumDefined(a));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="TextFormat"/>.
        /// </summary>
        public static DependencyProperty TextFormatProperty = DependencyProperty.Register(
            nameof(TextFormat), typeof(string), T,
            new PropertyMetadata("{0:0.0}%", TxtFmt));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Thickness"/>.
        /// </summary>
        public static DependencyProperty ThicknessProperty = DependencyProperty.Register(
            nameof(Thickness), typeof(double), T,
            new PropertyMetadata(4.0, Redraw));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Radius"/>.
        /// </summary>
        public static DependencyProperty RadiusProperty = DependencyProperty.Register(
            nameof(Radius), typeof(double), T,
            new PropertyMetadata(24.0, Redraw));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Value"/>.
        /// </summary>
        public static DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(double), T,
            new PropertyMetadata(0.0, Updt2));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Min"/>.
        /// </summary>
        public static DependencyProperty MinProperty = DependencyProperty.Register(
            nameof(Min), typeof(double), T,
            new PropertyMetadata(0.0, Updt2));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Max"/>.
        /// </summary>
        public static DependencyProperty MaxProperty = DependencyProperty.Register(
            nameof(Max), typeof(double), T,
            new PropertyMetadata(100.0, Updt2));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="RingStroke"/>.
        /// </summary>
        public static DependencyProperty RingStrokeProperty = DependencyProperty.Register(
            nameof(RingStroke), typeof(Brush), T,
            new PropertyMetadata(SystemColors.ControlDarkBrush));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Fill"/>.
        /// </summary>
        public static DependencyProperty FillProperty = DependencyProperty.Register(
            nameof(Fill), typeof(Brush), T,
            new PropertyMetadata(SystemColors.HighlightBrush));
        /// <summary>
        /// Identifica a la propiedad de dependencia 
        /// <see cref="IsIndeterminate"/>.
        /// </summary>
        public static DependencyProperty IsIndeterminateProperty = DependencyProperty.Register(
            nameof(IsIndeterminate), typeof(bool), T,
            new PropertyMetadata(false, Updt));
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
        /// Obtiene o establece la dirección en la cual se rellenará este
        /// <see cref="ProgressRing"/>. 
        /// </summary>
        public SweepDirection Sweep
        {
            get => (SweepDirection)GetValue(SweepProperty);
            set => SetValue(SweepProperty, value);
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
        /// Obtiene o establece el <see cref="Brush"/> a utilizar para dibujar
        /// el relleno del anillo de este <see cref="ProgressRing"/>.
        /// </summary>
        public Brush Fill
        {
            get => (Brush)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
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
        /// Obtiene o establece el grosor del anillo de este
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public double Thickness
        {
            get => (double)GetValue(ThicknessProperty);
            set => SetValue(ThicknessProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el radio del anillo de este
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
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
        /// <summary>
        /// Obtiene o establece el valor mínimo de este
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public double Min
        {
            get => (double)GetValue(MinProperty);
            set => SetValue(MinProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el valor máximo de este
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public double Max
        {
            get => (double)GetValue(MaxProperty);
            set => SetValue(MaxProperty, value);
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
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="ProgressRing"/>.
        /// </summary>
        public ProgressRing()
        {
            SetBinding(HeightProperty, new Binding(nameof(Width)) { Source = this });
            ellBg.SetBinding(Shape.StrokeThicknessProperty, new Binding(nameof(Thickness)) { Source = this });
            ellBg.SetBinding(Shape.StrokeProperty, new Binding(nameof(RingStroke))
            {
                Source = this
            });
            pth.SetBinding(Shape.StrokeThicknessProperty, new Binding(nameof(Thickness)) { Source = this });
            pth.SetBinding(Shape.StrokeProperty, new Binding(nameof(Fill))
            {
                Source = this
            });
            spin.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 1)),
                Value = 360.0
            });
            Grid a = new Grid();
            a.Children.Add(ellBg);
            a.Children.Add(pth);
            a.Children.Add(TxtPercent);
            Content = a;
            Loaded += OnLoaded;
            SizeChanged += OnLoaded;
        }
    }
}