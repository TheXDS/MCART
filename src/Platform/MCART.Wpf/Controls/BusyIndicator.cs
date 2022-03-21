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
using System.Windows.Media;
using System.Windows.Shapes;
using TheXDS.MCART.Controls.Base;
using TheXDS.MCART.Helpers;
using static TheXDS.MCART.Helpers.WpfUtils;

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
    }

    private static bool ChkAngle(object value)
    {
        return value is double v && v.IsBetween(0, 360);
    }

    /// <inheritdoc/>
    protected override void OnLayoutUpdate(double radius, double thickness)
    {
        if (path is null) return;
        path.Data = GetCircleArc(radius, ArcAngle, thickness);
    }
}
