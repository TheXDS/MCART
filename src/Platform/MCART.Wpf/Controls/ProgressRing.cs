/*
ProgressRing.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.Controls;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using TheXDS.MCART.Controls.Base;
using TheXDS.MCART.Math;
using static TheXDS.MCART.Helpers.WpfUtils;

/// <summary>
/// Control que muestra un anillo de progreso.
/// </summary>
[ContentProperty(nameof(InnerContent))]
public class ProgressRing : RingControlBase
{
    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="RingBackground" />.
    /// </summary>
    public static readonly DependencyProperty RingBackgroundProperty = DependencyProperty.Register(
        nameof(RingBackground), typeof(Brush), typeof(ProgressRing),
        new FrameworkPropertyMetadata(SystemColors.InactiveSelectionHighlightBrush, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Identifica a la propiedad de dependencia 
    /// <see cref="IsIndeterminate"/>.
    /// </summary>
    public static readonly DependencyProperty IsIndeterminateProperty = DependencyProperty.Register(
        nameof(IsIndeterminate), typeof(bool), typeof(ProgressRing),
        new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, UpdateLayout));

    /// <summary>
    /// Identifica a la propiedad de dependencia 
    /// <see cref="InnerContent"/>.
    /// </summary>
    public static readonly DependencyProperty InnerContentProperty = DependencyProperty.Register(
        nameof(InnerContent), typeof(object), typeof(ProgressRing),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange));

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Sweep"/>.
    /// </summary>
    public static readonly DependencyProperty SweepProperty = DependencyProperty.Register(
        nameof(Sweep), typeof(SweepDirection), typeof(ProgressRing),
        new FrameworkPropertyMetadata(SweepDirection.Clockwise, FrameworkPropertyMetadataOptions.AffectsRender, UpdateLayout),
        typeof(SweepDirection).IsEnumDefined);

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Maximum" />.
    /// </summary>
    public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(nameof(Maximum),
        typeof(double), typeof(ProgressRing),
        new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.AffectsRender, UpdateLayout), IsDoubleValid);

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Minimum" />.
    /// </summary>
    public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(nameof(Minimum),
        typeof(double), typeof(ProgressRing),
        new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender, UpdateLayout), IsDoubleValid);

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Value" />.
    /// </summary>
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value),
        typeof(double), typeof(ProgressRing),
        new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender, UpdateLayout), IsDoubleValid);

    static ProgressRing()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressRing), new FrameworkPropertyMetadata(typeof(ProgressRing)));
        HorizontalContentAlignmentProperty.OverrideMetadata(typeof(ProgressRing), new FrameworkPropertyMetadata(HorizontalAlignment.Center));
        VerticalContentAlignmentProperty.OverrideMetadata(typeof(ProgressRing), new FrameworkPropertyMetadata(VerticalAlignment.Center));
    }

    private Path? path;
    private Ellipse? ringbg;

    /// <summary>
    /// Obtiene o establece el contenido interno a mostrar en el área interna del control.
    /// </summary>
    public object? InnerContent
    {
        get => GetValue(InnerContentProperty);
        set => SetValue(InnerContentProperty, value);
    }

    /// <summary>
    /// Obtiene o establece el <see cref="Brush"/> a utilizar para dibujar
    /// el relleno del anillo de este <see cref="ProgressRing"/>.
    /// </summary>
    public Brush RingBackground
    {
        get => (Brush)GetValue(RingBackgroundProperty);
        set => SetValue(RingBackgroundProperty, value);
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
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    /// <summary>
    /// Obtiene o establece el valor mínimo de este
    /// <see cref="ProgressRing"/>.
    /// </summary>
    public double Minimum
    {
        get => (double)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    /// <summary>
    /// Obtiene o establece el valor mínimo de este
    /// <see cref="ProgressRing"/>.
    /// </summary>
    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        path = (Path)GetTemplateChild($"PART_{nameof(path)}");
        ringbg = (Ellipse)GetTemplateChild($"PART_{nameof(ringbg)}");
        SetControlSize();
    }

    /// <inheritdoc/>
    protected override void OnLayoutUpdate(double radius, double thickness)
    {
        if (path is not null) path.Data = GetCircleArc(radius, GetAngle(), thickness);
        if (ringbg is not null)
        {
            ringbg.Height = ringbg.Width = radius * 2;
            ringbg.StrokeThickness = thickness;
        }
    }

    private double GetAngle()
    {
        return !IsIndeterminate ? (Value - Minimum) * 360 / (Maximum - Minimum) : 90;
    }

    private static bool IsDoubleValid(object o)
    {
        return o is double a && a.IsValid();
    }
}
