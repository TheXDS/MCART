/*
BusyContainer.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     "Surfin Bird" (Original implementation) <https://stackoverflow.com/users/4267982/surfin-bird>
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
using System.Windows.Media.Effects;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Math;

namespace TheXDS.MCART.Controls;

/// <summary>
/// Container that blocks its content while the application is busy.
/// </summary>
public class BusyContainer : ContentControl
{
    private static readonly DependencyPropertyKey _currentBusyEffectPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(CurrentBusyEffect),
        typeof(Effect),
        typeof(BusyContainer),
        new PropertyMetadata(null, null, CoerceBusyEffect));
    private static readonly DependencyPropertyKey _currentBusyBackgroundPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(CurrentBusyBackground),
        typeof(Brush),
        typeof(BusyContainer),
        new PropertyMetadata(null, null, CoerceBusyBackground));

    /// <summary>
    /// Identifies the dependency property for <see cref="BusyBackground"/>.
    /// </summary>
    public static readonly DependencyProperty BusyBackgroundProperty = DependencyProperty.Register(
        nameof(BusyBackground),
        typeof(Brush),
        typeof(BusyContainer),
        new PropertyMetadata(SystemColors.AppWorkspaceBrush, OnChangeCurrentBusyBackground));

    /// <summary>
    /// Identifies the dependency property for <see cref="BusyContent"/>.
    /// </summary>
    public static readonly DependencyProperty BusyContentProperty = DependencyProperty.Register(
        nameof(BusyContent),
        typeof(object),
        typeof(BusyContainer),
        new PropertyMetadata(null));

    /// <summary>
    /// Identifies the dependency property for
    /// <see cref="BusyContentStringFormat"/>.
    /// </summary>
    public static readonly DependencyProperty BusyContentStringFormatProperty = DependencyProperty.Register(
        nameof(BusyContentStringFormat),
        typeof(string),
        typeof(BusyContainer),
        new PropertyMetadata(null));

    /// <summary>
    /// Identifies the dependency property for <see cref="BusyEffect"/>.
    /// </summary>
    public static readonly DependencyProperty BusyEffectProperty = DependencyProperty.Register(
        nameof(BusyEffect), 
        typeof(Effect),
        typeof(BusyContainer),
        new PropertyMetadata(new BlurEffect() { Radius = 5.0 }, OnChangeCurrentBusyEffect));

    /// <summary>
    /// Identifies the dependency property for <see cref="BusyOpacity"/>.
    /// </summary>
    public static readonly DependencyProperty BusyOpacityProperty = DependencyProperty.Register(
        nameof(BusyOpacity),
        typeof(double),
        typeof(BusyContainer),
        new PropertyMetadata(0.0, OnChangedBusyOpacity, CoerceBusyOpacity), ChkBusyOpacity);

    /// <summary>
    /// Identifies the dependency property for <see cref="IsBusy"/>.
    /// </summary>
    public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(
        nameof(IsBusy),
        typeof(bool),
        typeof(BusyContainer),
        new PropertyMetadata(false, OnChangeCurrentBusyEffect));

    /// <summary>
    /// Identifies the read-only dependency property
    /// <see cref="CurrentBusyEffect"/>.
    /// </summary>
    public static readonly DependencyProperty CurrentBusyEffectProperty = _currentBusyEffectPropertyKey.DependencyProperty;

    /// <summary>
    /// Identifies the read-only dependency property
    /// <see cref="CurrentBusyBackground"/>.
    /// </summary>
    public static readonly DependencyProperty CurrentBusyBackgroundProperty = _currentBusyBackgroundPropertyKey.DependencyProperty;

    /// <summary>
    /// Static constructor for <see cref="BusyContainer"/>.
    /// </summary>
    static BusyContainer()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyContainer), new FrameworkPropertyMetadata(typeof(BusyContainer)));
        HorizontalContentAlignmentProperty.OverrideMetadata(typeof(BusyContainer), new FrameworkPropertyMetadata(HorizontalAlignment.Stretch));
        VerticalContentAlignmentProperty.OverrideMetadata(typeof(BusyContainer), new FrameworkPropertyMetadata(VerticalAlignment.Stretch));
    }

    /// <summary>
    /// Gets or sets the content to display while the control is busy.
    /// </summary>
    public object BusyContent
    {
        get => GetValue(BusyContentProperty);
        set => SetValue(BusyContentProperty, value);
    }

    /// <summary>
    /// Gets or sets the format string for the busy content display.
    /// </summary>
    public string BusyContentStringFormat
    {
        get => (string)GetValue(BusyContentStringFormatProperty);
        set => SetValue(BusyContentStringFormatProperty, value);
    }

    /// <summary>
    /// Gets or sets the effect applied to content while busy.
    /// </summary>
    public Effect BusyEffect
    {
        get => (Effect)GetValue(BusyEffectProperty);
        set => SetValue(BusyEffectProperty, value);
    }

    /// <summary>
    /// Gets or sets the opacity of the background overlay shown when
    /// <see cref="IsBusy"/> is true.
    /// </summary>
    public double BusyOpacity
    {
        get => (double)GetValue(BusyOpacityProperty);
        set => SetValue(BusyOpacityProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Brush"/> for the background overlay
    /// displayed when <see cref="IsBusy"/> is true.
    /// </summary>
    public Brush BusyBackground
    {
        get => (Brush)GetValue(BusyBackgroundProperty);
        set => SetValue(BusyBackgroundProperty, value);
    }

    /// <summary>
    /// Gets the effect currently applied when the control is busy.
    /// </summary>
    public Effect CurrentBusyEffect => (Effect)GetValue(CurrentBusyEffectProperty);

    /// <summary>
    /// Gets the <see cref="Brush"/> currently applied when the control
    /// is busy.
    /// </summary>
    public Brush CurrentBusyBackground => (Brush)GetValue(CurrentBusyBackgroundProperty);

    /// <summary>
    /// Gets or sets a value indicating whether this container is busy.
    /// </summary>
    public bool IsBusy
    {
        get => (bool)GetValue(IsBusyProperty);
        set => SetValue(IsBusyProperty, value);
    }

    private static bool ChkBusyOpacity(object value)
    {
        return ((double)value).IsBetween(0, 1);
    }

    private static object CoerceBusyOpacity(DependencyObject d, object baseValue)
    {
        return ((double)baseValue).Clamp(0, 1);
    }

    private static object? CoerceBusyEffect(DependencyObject d, object baseValue)
    {
        BusyContainer? o = (BusyContainer)d;
        return o.IsBusy ? d.GetValue(BusyEffectProperty) : null;
    }

    private static object? CoerceBusyBackground(DependencyObject d, object baseValue)
    {
        BusyContainer? o = (BusyContainer)d;
        return o.IsBusy ? d.GetValue(BusyBackgroundProperty) : null;
    }

    private static void OnChangedBusyOpacity(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        d.CoerceValue(BusyOpacityProperty);
    }

    private static void OnChangeCurrentBusyEffect(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        d.CoerceValue(CurrentBusyEffectProperty);
    }

    private static void OnChangeCurrentBusyBackground(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        d.CoerceValue(CurrentBusyBackgroundProperty);
    }
}
