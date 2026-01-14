/*
RingControlBase.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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
using System.Windows.Controls;
using System.Windows.Media;
using TheXDS.MCART.Math;

namespace TheXDS.MCART.Controls.Base;

/// <summary>
/// Base class that defines members to implement for ring-style
/// graphical controls.
/// </summary>
public abstract class RingControlBase : Control
{
    /// <summary>
    /// Identifies the attached dependency property for
    /// <see cref="Radius"/>.
    /// </summary>
    public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(nameof(Radius),
        typeof(double), typeof(RingControlBase),
        new FrameworkPropertyMetadata(24.0, FrameworkPropertyMetadataOptions.AffectsMeasure, UpdateFullLayout, CoerceRadius), ChkDblValue);

    /// <summary>
    /// Identifies the attached dependency property for
    /// <see cref="Thickness"/>.
    /// </summary>
    public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(nameof(Thickness),
        typeof(double), typeof(RingControlBase),
        new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsMeasure, UpdateThicknessLayout, CoerceThickness), ChkDblValue);

    /// <summary>
    /// Identifies the dependency property for the control's
    /// <see cref="Stroke"/> brush.
    /// </summary>
    public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(nameof(Stroke),
        typeof(Brush), typeof(RingControlBase), new PropertyMetadata(SystemColors.HighlightBrush));

    /// <summary>
    /// Gets or sets the radius of this control.
    /// </summary>
    public double Radius
    {
        get => (double)GetValue(RadiusProperty);
        set => SetValue(RadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets the thickness of the control's visual elements.
    /// </summary>
    public double Thickness
    {
        get => (double)GetValue(ThicknessProperty);
        set => SetValue(ThicknessProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Brush"/> applied to the control.
    /// </summary>
    public Brush? Stroke
    {
        get => (Brush?)GetValue(StrokeProperty);
        set => SetValue(StrokeProperty, value);
    }

    private static bool ChkDblValue(object value)
    {
        return (double)value >= 0;
    }

    private static object CoerceRadius(DependencyObject d, object baseValue)
    {
        if (d is not RingControlBase) return baseValue;
        return ((double)baseValue).Clamp(0, double.MaxValue);
    }

    private static object CoerceThickness(DependencyObject d, object baseValue)
    {
        if (d is not RingControlBase b) return baseValue;
        return ((double)baseValue).Clamp(0, b.Radius * 2);
    }

    /// <summary>
    /// Update callback that can be attached to dependency property
    /// metadata to be invoked when a property value changes.
    /// </summary>
    /// <param name="d">Source object of the event.</param>
    /// <param name="_">Dependency property change arguments.</param>
    protected static void UpdateLayout(DependencyObject d, DependencyPropertyChangedEventArgs _)
    {
        if (d is not RingControlBase p) return;
        p.OnLayoutUpdate((double)d.GetValue(RadiusProperty), (double)d.GetValue(ThicknessProperty));
    }

    /// <summary>
    /// Updates the control's full visual state.
    /// </summary>
    /// <param name="d">Source object of the event.</param>
    /// <param name="e">Dependency property change arguments.</param>
    protected static void UpdateFullLayout(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not RingControlBase) return;
        d.CoerceValue(RadiusProperty);
        UpdateThicknessLayout(d, e);
        double r = (double)d.GetValue(RadiusProperty);
        d.SetValue(WidthProperty, r * 2);
        d.SetValue(HeightProperty, r * 2);
    }

    /// <summary>
    /// Updates the thickness-related visual state of the ring
    /// control.
    /// </summary>
    /// <param name="d">Source object of the event.</param>
    /// <param name="e">Dependency property change arguments.</param>
    protected static void UpdateThicknessLayout(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is null) return;
        d.CoerceValue(ThicknessProperty);
        UpdateLayout(d, e);        
    }

    /// <summary>
    /// Sets the control's computed size based on its radius.
    /// </summary>
    protected void SetControlSize()
    {
        UpdateFullLayout(this, new DependencyPropertyChangedEventArgs());
    }

    /// <summary>
    /// When invalidated by a base class, updates the shapes that make up
    /// the control.
    /// </summary>
    /// <param name="radius">Control radius.</param>
    /// <param name="thickness">Thickness of the control's lines.</param>
    protected abstract void OnLayoutUpdate(double radius, double thickness);

    /// <inheritdoc/>
    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        SetControlSize();
    }
}
