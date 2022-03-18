/*
BusyIndicator.cs

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

namespace TheXDS.MCART.Controls;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Math;
using static TheXDS.MCART.Helpers.WpfUtils;

/// <summary>
/// Control simple que indica al usuario que la aplicación está ocupada.
/// </summary>
public class BusyIndicator : Control
{
    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Radius" />.
    /// </summary>
    public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(nameof(Radius),
        typeof(double), typeof(BusyIndicator),
        new FrameworkPropertyMetadata(24.0, FrameworkPropertyMetadataOptions.AffectsMeasure, SetControlSize, CoerceRadius), ChkDblValue);

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Radius" />.
    /// </summary>
    public static readonly DependencyProperty ArcAngleProperty = DependencyProperty.Register(nameof(ArcAngle),
        typeof(double), typeof(BusyIndicator),
        new FrameworkPropertyMetadata(270.0, FrameworkPropertyMetadataOptions.AffectsMeasure, SetControlSize), ChkAngle);

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Running" />.
    /// </summary>
    public static readonly DependencyProperty RunningProperty = DependencyProperty.Register(nameof(Running),
        typeof(bool), typeof(BusyIndicator), new PropertyMetadata(true));

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Stroke" />.
    /// </summary>
    public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(nameof(Stroke),
        typeof(Brush), typeof(BusyIndicator), new PropertyMetadata(SystemColors.HighlightBrush));

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Stroke2" />.
    /// </summary>
    public static readonly DependencyProperty Stroke2Property = DependencyProperty.Register(nameof(Stroke2),
        typeof(Brush), typeof(BusyIndicator), new PropertyMetadata(SystemColors.InactiveSelectionHighlightBrush));

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Thickness" />.
    /// </summary>
    public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(nameof(Thickness),
        typeof(double), typeof(BusyIndicator),
        new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsMeasure, SetControlSize, CoerceThickness), ChkDblValue);

    /// <summary>
    /// Inicializa la clase <see cref="BusyIndicator"/>
    /// </summary>
    static BusyIndicator()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyIndicator), new FrameworkPropertyMetadata(typeof(BusyIndicator)));
    }

    private Path? path = null;

    /// <summary>
    /// Obtiene o establece el radio de este control.
    /// </summary>
    public double Radius
    {
        get => (double)GetValue(RadiusProperty);
        set => SetValue(RadiusProperty, value);
    }

    /// <summary>
    /// Obtiene o establece el ángulo de cierre del arco de este control.
    /// </summary>
    public double ArcAngle
    {
        get => (double)GetValue(ArcAngleProperty);
        set => SetValue(ArcAngleProperty, value);
    }

    /// <summary>
    /// Obtiene o establece un valor que indica si el control se dibujará
    /// en su estado secundario.
    /// </summary>
    public bool Running
    {
        get => (bool)GetValue(RunningProperty);
        set => SetValue(RunningProperty, value);
    }

    /// <summary>
    /// Obtiene o establece el <see cref="Brush" /> a aplicar al control.
    /// </summary>
    public Brush? Stroke
    {
        get => (Brush?)GetValue(StrokeProperty);
        set => SetValue(StrokeProperty, value);
    }

    /// <summary>
    /// Obtiene o establece el <see cref="Brush" /> a aplicar al estado
    /// secundario de el control.
    /// </summary>
    public Brush? Stroke2
    {
        get => (Brush?)GetValue(Stroke2Property);
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

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        path = (Path)GetTemplateChild($"PART_{nameof(path)}");
        SetControlSize(this, new DependencyPropertyChangedEventArgs());
    }

    private static void SetControlSize(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not BusyIndicator { path: { } p }) return;
        d.CoerceValue(ThicknessProperty);
        d.CoerceValue(RadiusProperty);
        double r = (double)d.GetValue(RadiusProperty);
        double t = (double)d.GetValue(ThicknessProperty);
        d.SetValue(WidthProperty, r * 2);
        d.SetValue(HeightProperty, r * 2);
        p.Data = GetCircleArc(r, (double)d.GetValue(ArcAngleProperty), t);
    }

    private static bool ChkDblValue(object value)
    {
        return ((double)value) >= 0;
    }

    private static bool ChkAngle(object value)
    {
        return value is double v && v.IsBetween(0, 360);
    }

    private static object CoerceRadius(DependencyObject d, object baseValue)
    {
        if (d is not BusyIndicator) return baseValue;
        return ((double)baseValue).Clamp(0, double.MaxValue);
    }

    private static object CoerceThickness(DependencyObject d, object baseValue)
    {
        if (d is not BusyIndicator b) return baseValue;
        return ((double)baseValue).Clamp(0, b.Radius * 2);
    }
}
