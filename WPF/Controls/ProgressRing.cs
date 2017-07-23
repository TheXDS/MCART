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
    public class ProgressRing : UserControl
    {
        private static Type T = typeof(ProgressRing);
        private static void Redraw(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressRing p = (ProgressRing)d;
            p.SetControlSize();
            p.Draw();
        }
        private static void Updt(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ProgressRing)d).Draw();
        }
        private static void Updt2(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressRing p = (ProgressRing)d;
            p.SetValue(IsIndeterminateProperty, !p.Value.IsBetween(p.Min, p.Max));
        }
        private static void TxtFmt(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProgressRing p = (ProgressRing)d;
            p.TxtPercent.Text = string.Format(p.TextFormat, p.Value);
        }
        private TextBlock TxtPercent = new TextBlock()
        {
            HorizontalAlignment=HorizontalAlignment.Center,
            VerticalAlignment=VerticalAlignment.Center
        };
        private Ellipse ellBg = new Ellipse();
        private Path pth = new Path()
        {
            RenderTransform = new RotateTransform(),
            RenderTransformOrigin = new Point(0.5, 0.5)
        };
        private DoubleAnimationUsingKeyFrames spin = new DoubleAnimationUsingKeyFrames()
        {
            RepeatBehavior = RepeatBehavior.Forever
        };
        private bool amIAnimated = false;
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Redraw(this, new DependencyPropertyChangedEventArgs());
        }
        private void Draw()
        {
            if (!pth.IsLoaded) return;
            RotateTransform x = (RotateTransform)pth.RenderTransform;
            if (!IsIndeterminate)
            {
                amIAnimated = false;
                x.BeginAnimation(RotateTransform.AngleProperty, null);
                pth.Data = UI.UI.GetCircleArc(Radius, (((Value - Min) / (Max - Min)) * 360).Clamp(359.999, 0), Thickness);
                TxtPercent.Text = string.Format(TextFormat, Value);
            }
            else if (!amIAnimated)
            {
                pth.Data = UI.UI.GetCircleArc(Radius, 90, Thickness);
                amIAnimated = true;
                x.BeginAnimation(RotateTransform.AngleProperty, spin);
                TxtPercent.Text = "...";
            }
        }
        private void SetControlSize()
        {
            Width = (double)GetValue(RadiusProperty) * 2 + (double)GetValue(ThicknessProperty);
        }
        public static DependencyProperty TextFormatProperty = DependencyProperty.Register(
            nameof(TextFormat), typeof(string), T,
            new PropertyMetadata("{0:0.0}%", TxtFmt));
        public static DependencyProperty ThicknessProperty = DependencyProperty.Register(
            nameof(Thickness), typeof(double), T,
            new PropertyMetadata(4.0, Redraw));
        public static DependencyProperty RadiusProperty = DependencyProperty.Register(
            nameof(Radius), typeof(double), T,
            new PropertyMetadata(24.0, Redraw));
        public static DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(double), T,
            new PropertyMetadata(0.0, Updt2));
        public static DependencyProperty MinProperty = DependencyProperty.Register(
            nameof(Min), typeof(double), T,
            new PropertyMetadata(0.0, Updt2));
        public static DependencyProperty MaxProperty = DependencyProperty.Register(
            nameof(Max), typeof(double), T,
            new PropertyMetadata(100.0, Updt2));
        public static DependencyProperty RingStrokeProperty = DependencyProperty.Register(
            nameof(RingStroke), typeof(Brush), T,
            new PropertyMetadata(SystemColors.ControlDarkBrush));
        public static DependencyProperty FillProperty = DependencyProperty.Register(
            nameof(Fill), typeof(Brush), T,
            new PropertyMetadata(SystemColors.HighlightBrush));
        public static DependencyProperty IsIndeterminateProperty = DependencyProperty.Register(
            nameof(IsIndeterminate), typeof(bool), T,
            new PropertyMetadata(false, Updt));
        public Brush RingStroke
        {
            get => (Brush)GetValue(RingStrokeProperty);
            set => SetValue(RingStrokeProperty, value);
        }
        public Brush Fill
        {
            get => (Brush)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }
        public string TextFormat
        {
            get => (string)GetValue(TextFormatProperty);
            set => SetValue(TextFormatProperty, value);
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
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        public double Min
        {
            get => (double)GetValue(MinProperty);
            set => SetValue(MinProperty, value);
        }
        public double Max
        {
            get => (double)GetValue(MaxProperty);
            set => SetValue(MaxProperty, value);
        }
        public bool IsIndeterminate
        {
            get => (bool)GetValue(IsIndeterminateProperty);
            set => SetValue(IsIndeterminateProperty, value);
        }
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
                KeyTime=KeyTime.FromTimeSpan(new TimeSpan(0,0,1)),
                Value=360.0
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