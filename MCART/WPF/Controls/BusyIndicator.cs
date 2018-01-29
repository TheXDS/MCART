/*
BusyIndicator.cs

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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using static TheXDS.MCART.UI;

namespace TheXDS.MCART.Controls
{
    /// <summary>
    /// Control simple que indica al usuario que la aplicación está ocupada.
    /// </summary>
    public class BusyIndicator : UserControl
    {
        static Type T = typeof(BusyIndicator);
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Radius"/>.
        /// </summary>
        public static DependencyProperty RadiusProperty = DependencyProperty.Register(nameof(Radius), typeof(double), T, new PropertyMetadata(24.0, SetControlSize));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Starting"/>.
        /// </summary>
        public static DependencyProperty StartingProperty = DependencyProperty.Register(nameof(Starting), typeof(bool), T, new PropertyMetadata(false, Colorize));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Stroke"/>.
        /// </summary>
        public static DependencyProperty StrokeProperty = DependencyProperty.Register(nameof(Stroke), typeof(Brush), T, new PropertyMetadata(SystemColors.HighlightBrush, Colorize));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Stroke2"/>.
        /// </summary>
        public static DependencyProperty Stroke2Property = DependencyProperty.Register(nameof(Stroke2), typeof(Brush), T, new PropertyMetadata(SystemColors.ControlDarkBrush, Colorize));
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Thickness"/>.
        /// </summary>
        public static DependencyProperty ThicknessProperty = DependencyProperty.Register(nameof(Thickness), typeof(double), T, new PropertyMetadata(4.0, SetControlSize));
        static void Colorize(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BusyIndicator b = (BusyIndicator)d;
            b.pth.Stroke = b.Starting ? b.Stroke2 : b.Stroke;
            b.pth.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            b.pth.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, b.Starting ? b.spin2 : b.spin);
        }
        static void SetControlSize(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BusyIndicator)d).Width = (double)d.GetValue(RadiusProperty) * 2 + (double)d.GetValue(ThicknessProperty);
        }
        Path pth = new Path()
        {
            RenderTransform = new RotateTransform(),
            RenderTransformOrigin = new Point(0.5, 0.5)
        };
        DoubleAnimationUsingKeyFrames spin = new DoubleAnimationUsingKeyFrames()
        {
            RepeatBehavior = RepeatBehavior.Forever
        };
        DoubleAnimationUsingKeyFrames spin2 = new DoubleAnimationUsingKeyFrames()
        {
            RepeatBehavior = RepeatBehavior.Forever
        };
        void OnLoaded(object sender, RoutedEventArgs e) => pth.Data = GetCircleArc(Radius, 270, Thickness);
        /// <summary>
        /// Obtiene o establece el <see cref="Brush"/> a aplicar al control.
        /// </summary>
        public Brush Stroke
        {
            get => (Brush)GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el <see cref="Brush"/> a aplicar al estado
        /// secundario de el control.
        /// </summary>
        public Brush Stroke2
        {
            get => (Brush)GetValue(Stroke2Property);
            set => SetValue(Stroke2Property, value);
        }
        /// <summary>
        /// Obtiene o establece el grosor de los elementos de este control.
        /// </summary>
        public double Thickness
        {
            get => (double)GetValue(ThicknessProperty);
            set => SetValue(ThicknessProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el radio de este control.
        /// </summary>
        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }
        /// <summary>
        /// Obtiene o establece un valor que indica si el control se dibujará
        /// en su estado secundario.
        /// </summary>
        public bool Starting
        {
            get => (bool)GetValue(StartingProperty);
            set => SetValue(StartingProperty, value);
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="BusyIndicator"/>.
        /// </summary>
        public BusyIndicator()
        {
            SetBinding(HeightProperty, new Binding(nameof(Width)) { Source = this });
            Loaded += OnLoaded;
            SizeChanged += OnLoaded;
            pth.SetBinding(Shape.StrokeThicknessProperty, new Binding(nameof(Thickness)) { Source = this });
            SetControlSize(this, new DependencyPropertyChangedEventArgs());
            spin.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 1)),
                Value = 360.0
            });
            spin2.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 3)),
                Value = -360.0
            });
            Colorize(this, new DependencyPropertyChangedEventArgs());
            Content = pth;
        }
    }
}