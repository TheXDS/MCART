//
//  BusyIndicator.cs
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
using MCART.UI;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
namespace MCART.Controls
{
    class BusyIndicator : UserControl
    { 
        private static Type T = typeof(BusyIndicator);
        public static DependencyProperty ThicknessProperty = DependencyProperty.Register(
            nameof(Thickness), typeof(double), T,
            new PropertyMetadata(4.0, SetControlSize));
        public static DependencyProperty RadiusProperty = DependencyProperty.Register(
            nameof(Radius), typeof(double), T,
            new PropertyMetadata(24.0, SetControlSize));
        public static DependencyProperty FillProperty = DependencyProperty.Register(
            nameof(Fill), typeof(Brush), T,
            new PropertyMetadata(SystemColors.HighlightBrush, Colorize));
        public static DependencyProperty Fill2Property = DependencyProperty.Register(
            nameof(Fill2), typeof(Brush), T,
            new PropertyMetadata(SystemColors.GrayTextBrush, Colorize));
        public static DependencyProperty StartingProperty = DependencyProperty.Register(
            nameof(Starting), typeof(bool), T,
            new PropertyMetadata(false, Colorize));
        private static void Colorize(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BusyIndicator b = (BusyIndicator)d;
            b.pth.Stroke = b.Starting ? b.Fill2 : b.Fill2;
        }
        private static void SetControlSize(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BusyIndicator)d).Width = (double)d.GetValue(RadiusProperty) * 2 + (double)d.GetValue(ThicknessProperty);
        }
        private Path pth = new Path()
        {
            RenderTransform = new RotateTransform(),
            RenderTransformOrigin = new Point(0.5, 0.5)
        };
        private DoubleAnimationUsingKeyFrames spin = new DoubleAnimationUsingKeyFrames()
        {
            RepeatBehavior = RepeatBehavior.Forever
        };
        private DoubleAnimationUsingKeyFrames spin2 = new DoubleAnimationUsingKeyFrames()
        {
            RepeatBehavior = RepeatBehavior.Forever
        };
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            pth.Data = UITools.GetCircleArc(Radius, 270, Thickness);
        }
        public Brush Fill
        {
            get => (Brush)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }
        public Brush Fill2
        {
            get => (Brush)GetValue(Fill2Property);
            set => SetValue(Fill2Property, value);
        }
        public double Thickness
        {
            get => (double)GetValue(ThicknessProperty);
            set => SetValue(ThicknessProperty, value);
        }
        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }
        public bool Starting
        {
            get => (bool)GetValue(StartingProperty);
            set => SetValue(StartingProperty, value);
        }
        public BusyIndicator()
        {
            SetBinding(HeightProperty, new Binding(nameof(Width)) { Source = this });
            Loaded += OnLoaded;
            SizeChanged += OnLoaded;
            pth.SetBinding(Shape.StrokeThicknessProperty, new Binding(nameof(Thickness)) { Source = this });
            SetControlSize(this, new DependencyPropertyChangedEventArgs());
            Colorize(this, new DependencyPropertyChangedEventArgs());
            spin.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime=KeyTime.FromTimeSpan(new TimeSpan(0,0,1)),
                Value=360.0
            });
            spin2.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(new TimeSpan(0, 0, 3)),
                Value = -360.0
            });
            pth.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, spin);
            Content = pth;
        }
    }
}