// BusyContainer.axaml.cs
//
// This file is part of Morgan's CLR Advanced Runtime (MCART)
//
// Author(s):
//      César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
// Released under the MIT License (MIT)
// Copyright © 2011 - 2025 César Andrés Morgan
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the “Software”), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using Avalonia.Media;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Math;

namespace TheXDS.MCART.Controls;

/// <summary>
/// Control that displays either normal or busy content.
/// </summary>
public class BusyContainer : TemplatedControl
{
    private static IBrush GetDefaultBusyBackground()
    {
        return (Application.Current?.TryGetResource("DefaultBackground", Application.Current.ActualThemeVariant,
            out var brush) ?? false) && brush is IBrush b
            ? b : Brushes.Black;
    }

    /// <summary>
    /// Identifies the <see cref="CurrentBusyEffect"/> direct property.
    /// </summary>
    public static readonly DirectProperty<BusyContainer, IEffect?> CurrentBusyEffectProperty =
        AvaloniaProperty.RegisterDirect<BusyContainer, IEffect?>(nameof(CurrentBusyEffect), b => b.IsBusy ? b.BusyEffect : null);

    /// <summary>
    /// Identifies the <see cref="BusyContent"/> styled property.
    /// </summary>
    public static readonly StyledProperty<object?> BusyContentProperty =
        AvaloniaProperty.Register<BusyContainer, object?>(nameof(BusyContent), new BusyIndicator());

    /// <summary>
    /// Identifies the <see cref="BusyContentTemplate"/> styled property.
    /// </summary>
    public static readonly StyledProperty<IDataTemplate?> BusyContentTemplateProperty =
        AvaloniaProperty.Register<BusyContainer, IDataTemplate?>(nameof(BusyContentTemplate));

    /// <summary>
    /// Identifies the <see cref="BusyBackground"/> styled property.
    /// </summary>
    public static readonly StyledProperty<IBrush?> BusyBackgroundProperty =
        AvaloniaProperty.Register<BusyContainer, IBrush?>(nameof(BusyBackground), GetDefaultBusyBackground());

    /// <summary>
    /// Identifies the <see cref="BusyOpacity"/> styled property.
    /// </summary>
    public static readonly StyledProperty<double> BusyOpacityProperty =
        AvaloniaProperty.Register<BusyContainer, double>(nameof(BusyOpacity), 0.5, validate: ChkBusyOpacity, coerce: CoerceBusyOpacity);

    /// <summary>
    /// Identifies the <see cref="BusyEffect"/> styled property.
    /// </summary>
    public static readonly StyledProperty<IEffect?> BusyEffectProperty =
        AvaloniaProperty.Register<BusyContainer, IEffect?>(nameof(BusyEffect), new BlurEffect());

    /// <summary>
    /// Identifies the <see cref="IsBusy"/> styled property.
    /// </summary>
    public static readonly StyledProperty<bool> IsBusyProperty =
        AvaloniaProperty.Register<BusyContainer, bool>(nameof(IsBusy));

    /// <summary>
    /// Identifies the <see cref="ContentTemplate"/> styled property.
    /// </summary>
    public static readonly StyledProperty<IDataTemplate?> ContentTemplateProperty =
        AvaloniaProperty.Register<BusyContainer, IDataTemplate?>(nameof(ContentTemplate));

    /// <summary>
    /// Identifies the <see cref="Content"/> styled property.
    /// </summary>
    public static readonly StyledProperty<object?> ContentProperty =
        AvaloniaProperty.Register<BusyContainer, object?>(nameof(Content));

    /// <summary>
    /// Identifies the <see cref="HorizontalContentAlignment"/> styled property.
    /// </summary>
    public static readonly StyledProperty<HorizontalAlignment> HorizontalContentAlignmentProperty =
        AvaloniaProperty.Register<BusyContainer, HorizontalAlignment>(nameof(HorizontalContentAlignment), HorizontalAlignment.Center);

    /// <summary>
    /// Identifies the <see cref="VerticalContentAlignment"/> styled property.
    /// </summary>
    public static readonly StyledProperty<VerticalAlignment> VerticalContentAlignmentProperty =
        AvaloniaProperty.Register<BusyContainer, VerticalAlignment>(nameof(VerticalContentAlignment), VerticalAlignment.Center);

    /// <summary>
    /// Gets or sets the desired horizontal alignment for the content being
    /// presented on this control. 
    /// </summary>
    public HorizontalAlignment HorizontalContentAlignment
    {
        get => GetValue(HorizontalContentAlignmentProperty);
        set => SetValue(HorizontalContentAlignmentProperty, value);
    }

    /// <summary>
    /// Gets or sets the desired vertical alignment for the content being
    /// presented on this control. 
    /// </summary>
    public VerticalAlignment VerticalContentAlignment
    {
        get => GetValue(VerticalContentAlignmentProperty);
        set => SetValue(VerticalContentAlignmentProperty, value);
    }

    /// <summary>
    /// Gets or sets the content being presented on this control.
    /// </summary>
    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    /// <summary>
    /// Gets or sets the template to be applied to the content being presented
    /// on this control.
    /// </summary>
    public IDataTemplate? ContentTemplate
    {
        get => GetValue(ContentTemplateProperty);
        set => SetValue(ContentTemplateProperty, value);
    }

    /// <summary>
    /// Gets or sets the content to be presented on this control when the
    /// <see cref="IsBusy"/> property is set to <see langword="true"/>.
    /// </summary>
    public object? BusyContent
    {
        get => GetValue(BusyContentProperty);
        set => SetValue(BusyContentProperty, value);
    }

    /// <summary>
    /// Gets or sets the template to be applied to the content being presented
    /// on this control when the <see cref="IsBusy"/> property is set to
    /// <see langword="true"/>.
    /// </summary>
    public IDataTemplate? BusyContentTemplate
    {
        get => GetValue(BusyContentTemplateProperty);
        set => SetValue(BusyContentTemplateProperty, value);
    }

    /// <summary>
    /// Gets or sets the background to be displayed on this control when the
    /// <see cref="IsBusy"/> property is set to <see langword="true"/>.
    /// </summary>
    public IBrush? BusyBackground
    {
        get => GetValue(BusyBackgroundProperty);
        set => SetValue(BusyBackgroundProperty, value);
    }

    /// <summary>
    /// Gets or sets the opacity of the overlay displayed on top of the content
    /// of this control when the <see cref="IsBusy"/> property is set to
    /// <see langword="true"/>.
    /// </summary>
    public double BusyOpacity
    {
        get => GetValue(BusyOpacityProperty);
        set => SetValue(BusyOpacityProperty, value);
    }

    /// <summary>
    /// Gets or sets an effect to be applied to the content being presented on
    /// this control when the <see cref="IsBusy"/> property is set to
    /// <see langword="true"/>.
    /// </summary>
    public IEffect? BusyEffect
    {
        get => GetValue(BusyEffectProperty);
        set => SetValue(BusyEffectProperty, value);
    }

    /// <summary>
    /// Gets or sets a value that indicates if the control should be displaying
    /// the main content or the "busy" content. 
    /// </summary>
    public bool IsBusy
    {
        get => GetValue(IsBusyProperty);
        set
        {
            SetValue(IsBusyProperty, value);
            if (value)
            {
                RaisePropertyChanged(CurrentBusyEffectProperty, null, CurrentBusyEffect);
            }
            else
            {
                RaisePropertyChanged(CurrentBusyEffectProperty, CurrentBusyEffect, null);
            }
        }
    }

    /// <summary>
    /// Gets a reference to the currently applied effect to the content of this control.
    /// </summary>
    public IEffect? CurrentBusyEffect => GetValue(CurrentBusyEffectProperty);

    private static double CoerceBusyOpacity(AvaloniaObject arg1, double arg2)
    {
        return arg2.Clamp(0.0, 1.0);
    }

    private static bool ChkBusyOpacity(double arg)
    {
        return arg.IsBetween(0.0, 1.0);
    }
}