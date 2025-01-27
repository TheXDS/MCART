/*
BusyIndicator.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using TheXDS.MCART.Controls.Base;
using TheXDS.MCART.Helpers;
using static TheXDS.MCART.Misc.PrivateInternals;

namespace TheXDS.MCART.Controls;

/// <summary>
/// Control simple que indica al usuario que la aplicación está ocupada.
/// </summary>
public class BusyIndicator : RingControlBase
{
    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="ArcAngle" />.
    /// </summary>
    public static readonly DependencyProperty ArcAngleProperty = DependencyProperty.Register(nameof(ArcAngle),
        typeof(double), typeof(BusyIndicator),
        new FrameworkPropertyMetadata(270.0, FrameworkPropertyMetadataOptions.AffectsRender, UpdateLayout),
        ChkAngle);

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Running" />.
    /// </summary>
    public static readonly DependencyProperty RunningProperty = DependencyProperty.Register(nameof(Running),
        typeof(bool), typeof(BusyIndicator),
        new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Stroke2" />.
    /// </summary>
    public static readonly DependencyProperty Stroke2Property = DependencyProperty.Register(nameof(Stroke2),
        typeof(Brush), typeof(BusyIndicator),
        new FrameworkPropertyMetadata(SystemColors.InactiveSelectionHighlightBrush, FrameworkPropertyMetadataOptions.AffectsRender));

    static BusyIndicator()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyIndicator), new FrameworkPropertyMetadata(typeof(BusyIndicator)));
        WidthProperty.OverrideMetadata(typeof(BusyIndicator), new FrameworkPropertyMetadata(double.NaN));
        HeightProperty.OverrideMetadata(typeof(BusyIndicator), new FrameworkPropertyMetadata(double.NaN));
        HorizontalAlignmentProperty.OverrideMetadata(typeof(BusyIndicator), new FrameworkPropertyMetadata(HorizontalAlignment.Center));
        VerticalAlignmentProperty.OverrideMetadata(typeof(BusyIndicator), new FrameworkPropertyMetadata(VerticalAlignment.Center));
    }

    private Path? path = null;

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
    /// Obtiene o establece el <see cref="Brush" /> a aplicar al estado
    /// secundario de el control.
    /// </summary>
    public Brush? Stroke2
    {
        get => (Brush?)GetValue(Stroke2Property);
        set => SetValue(Stroke2Property, value);
    }

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        path = (Path)GetTemplateChild($"PART_{nameof(path)}");
        SetControlSize();
    }
    
    /// <inheritdoc/>
    protected override void OnLayoutUpdate(double radius, double thickness)
    {
        if (path is null) return;
        path.Data = GetCircleArc(radius, ArcAngle, thickness);
    }

    private static bool ChkAngle(object value)
    {
        return value is double v && v.IsBetween(0, 360);
    }
}
