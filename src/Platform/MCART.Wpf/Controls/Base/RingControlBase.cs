/*
RingControlBase.cs

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

namespace TheXDS.MCART.Controls.Base;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TheXDS.MCART.Controls;
using TheXDS.MCART.Math;

/// <summary>
/// Clase base que define una serie de miembros a implementar por un control gráfico de anillos.
/// </summary>
public abstract class RingControlBase : Control
{
    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Radius" />.
    /// </summary>
    public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(nameof(Radius),
        typeof(double), typeof(RingControlBase),
        new FrameworkPropertyMetadata(24.0, FrameworkPropertyMetadataOptions.AffectsMeasure, UpdateFullLayout, CoerceRadius), ChkDblValue);

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Thickness" />.
    /// </summary>
    public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(nameof(Thickness),
        typeof(double), typeof(BusyIndicator),
        new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsMeasure, UpdateThicknessLayout, CoerceThickness), ChkDblValue);

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Stroke" />.
    /// </summary>
    public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(nameof(Stroke),
        typeof(Brush), typeof(BusyIndicator), new PropertyMetadata(SystemColors.HighlightBrush));

    /// <summary>
    /// Obtiene o establece el radio de este control.
    /// </summary>
    public double Radius
    {
        get => (double)GetValue(RadiusProperty);
        set => SetValue(RadiusProperty, value);
    }

    /// <summary>
    /// Obtiene o establece el grosor de los elementos de este control.
    /// </summary>
    public double Thickness
    {
        get => (double)GetValue(ThicknessProperty);
        set => SetValue(ThicknessProperty, value);
    }

    /// <summary>
    /// Obtiene o establece el <see cref="Brush" /> a aplicar al control.
    /// </summary>
    public Brush? Stroke
    {
        get => (Brush?)GetValue(StrokeProperty);
        set => SetValue(StrokeProperty, value);
    }

    ///// <summary>
    ///// Agrega al tipo especificado como un propietario para las propiedades de
    ///// dependencia definidas en esta clase base.
    ///// </summary>
    ///// <param name="ownerType">
    ///// Referencia al tipo que implementa esta clase base.
    ///// </param>
    //protected static void SetupProperties(Type ownerType)
    //{
    //    RadiusProperty.AddOwner(ownerType);
    //    ThicknessProperty.AddOwner(ownerType);
    //}

    private static bool ChkDblValue(object value)
    {
        return (double)value >= 0;
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

    /// <summary>
    /// Define un método que puede agregarse a los metadatos de la propiedad de
    /// dependencia para llamarse cuando el valor de una propiedad de
    /// dependencia cambie de valor.
    /// </summary>
    /// <param name="d">Objeto que es el origen del evento.</param>
    /// <param name="e">
    /// Argumentos de cambio de valor de la propiedad de dependencia.
    /// </param>
    protected static void UpdateLayout(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not RingControlBase p) return;
        p.OnLayoutUpdate((double)d.GetValue(RadiusProperty), (double)d.GetValue(ThicknessProperty));
    }

    protected static void UpdateFullLayout(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not RingControlBase) return;
        d.CoerceValue(RadiusProperty);
        UpdateThicknessLayout(d, e);
        double r = (double)d.GetValue(RadiusProperty);
        d.SetValue(WidthProperty, r * 2);
        d.SetValue(HeightProperty, r * 2);
    }

    protected static void UpdateThicknessLayout(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is null) return;
        d.CoerceValue(ThicknessProperty);
        UpdateLayout(d, e);        
    }

    /// <summary>
    /// Establece el tamaño del control.
    /// </summary>
    protected void SetControlSize()
    {
        UpdateFullLayout(this, new DependencyPropertyChangedEventArgs());
    }

    /// <summary>
    /// Cuando se invalida en una clase base, actualiza las figuras que
    /// componen al control.
    /// </summary>
    /// <param name="radius">Radio del control.</param>
    /// <param name="thickness">Grosor de las líneas del control.</param>
    protected abstract void OnLayoutUpdate(double radius, double thickness);

    /// <inheritdoc/>
    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        SetControlSize();
    }

}
