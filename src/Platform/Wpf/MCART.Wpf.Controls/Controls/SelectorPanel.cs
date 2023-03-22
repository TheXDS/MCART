/*
SelectorPanel.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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
using System.Windows.Input;
using System.Windows.Markup;
using TheXDS.MCART.Math;

namespace TheXDS.MCART.Controls;

/// <summary>
/// Control que permite seleccionar el contenido a mostrar basado en un valor
/// de índice.
/// </summary>
[ContentProperty(nameof(Items))]
public class SelectorPanel : ItemsControl
{
    static SelectorPanel()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectorPanel), new FrameworkPropertyMetadata(typeof(SelectorPanel)));
        IsTabStopProperty.OverrideMetadata(typeof(SelectorPanel), new FrameworkPropertyMetadata(false));
        KeyboardNavigation.DirectionalNavigationProperty.OverrideMetadata(typeof(SelectorPanel), new FrameworkPropertyMetadata(KeyboardNavigationMode.Contained));
    }

    private static readonly DependencyPropertyKey SelectedItemPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(SelectedItem),
        typeof(object),
        typeof(SelectorPanel),
        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsArrange));

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="SelectedIndex"/>.
    /// </summary>
    public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(
        nameof(SelectedIndex),
        typeof(int),
        typeof(SelectorPanel),
        new FrameworkPropertyMetadata(
            -1,
            FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsArrange,
            OnSelectedIndexChanged,
            OnCoerceSelectedIndex),
        OnValidateSelectedIndex);

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="SelectedItem"/>.
    /// </summary>
    public static readonly DependencyProperty SelectedItemProperty = SelectedItemPropertyKey.DependencyProperty;

    /// <summary>
    /// Obtiene o establece el índice del elemento a mostrar en este control.
    /// </summary>
    public int SelectedIndex
    {
        get => (int)GetValue(SelectedIndexProperty);
        set => SetValue(SelectedIndexProperty, value);
    }

    /// <summary>
    /// Obtiene una referencia al elemento mostrado actualmente en el control.
    /// </summary>
    public object? SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        protected set => SetValue(SelectedItemPropertyKey, value);
    }

    /// <inheritdoc/>
    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        OnSelectedIndexChanged(this, new(SelectedIndexProperty, SelectedIndex, SelectedIndex));
    }

    private static object OnCoerceSelectedIndex(DependencyObject d, object baseValue)
    {
        if (d is not SelectorPanel p || baseValue is not int v) return -1;
        return p.IsInitialized ? v.Clamp(-1, p.Items.Count - 1) : baseValue;
    }

    private static bool OnValidateSelectedIndex(object value)
    {
        return value is int v && v >= -1;
    }

    private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not SelectorPanel p || !p.IsInitialized) return;
        var v = (int)e.NewValue;
        p.SelectedItem = v >= 0 ? p.Items.GetItemAt(v.Clamp(0, p.Items.Count - 1)) : null;
    }
}
