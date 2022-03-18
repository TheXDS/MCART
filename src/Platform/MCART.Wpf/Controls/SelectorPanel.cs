/*
SelectorPanel.cs

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
using System.Windows.Input;
using System.Windows.Markup;
using TheXDS.MCART.Math;

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
