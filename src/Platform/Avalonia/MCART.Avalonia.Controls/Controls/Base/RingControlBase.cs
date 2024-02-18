﻿/*
RingControlBase.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using TheXDS.MCART.Math;

namespace TheXDS.MCART.Controls.Base;

/// <summary>
/// Clase base que define una serie de miembros a implementar por un control gráfico de anillos.
/// </summary>
public abstract class RingControlBase : TemplatedControl
{
    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Radius" />.
    /// </summary>
    public static readonly StyledProperty<double> RadiusProperty = AvaloniaProperty.Register<RingControlBase, double>(nameof(Radius),
        defaultValue: 24.0,
        defaultBindingMode: Avalonia.Data.BindingMode.TwoWay,
        validate: ChkDblValue,
        coerce: CoerceRadius
        //notifying: UpdateFullLayout
        );

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Thickness" />.
    /// </summary>
    public static readonly StyledProperty<double> ThicknessProperty = AvaloniaProperty.Register<RingControlBase, double>(nameof(Thickness),
        defaultValue: 4.0,
        defaultBindingMode: Avalonia.Data.BindingMode.TwoWay,
        validate: ChkDblValue,
        coerce: CoerceThickness
        //notifying: UpdateThicknessLayout
        );

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Stroke" />.
    /// </summary>
    public static readonly StyledProperty<IBrush?> StrokeProperty = AvaloniaProperty.Register<RingControlBase, IBrush?>(nameof(Stroke),
        defaultValue: Brushes.RoyalBlue);

    /// <summary>
    /// Obtiene o establece el radio de este control.
    /// </summary>
    public double Radius
    {
        get => GetValue(RadiusProperty);
        set => SetValue(RadiusProperty, value);
    }

    /// <summary>
    /// Obtiene o establece el grosor de los elementos de este control.
    /// </summary>
    public double Thickness
    {
        get => GetValue(ThicknessProperty);
        set => SetValue(ThicknessProperty, value);
    }

    /// <summary>
    /// Obtiene o establece el <see cref="Brush" /> a aplicar al control.
    /// </summary>
    public IBrush? Stroke
    {
        get => GetValue(StrokeProperty);
        set => SetValue(StrokeProperty, value);
    }

    private static bool ChkDblValue(double value)
    {
        return value >= 0;
    }

    private static double CoerceRadius(AvaloniaObject d, double baseValue)
    {
        if (d is not RingControlBase) return baseValue;
        return baseValue.Clamp(0, double.MaxValue);
    }

    private static double CoerceThickness(AvaloniaObject d, double baseValue)
    {
        if (d is not RingControlBase b) return baseValue;
        return baseValue.Clamp(0, b.Radius * 2);
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
    protected static void UpdateLayout(AvaloniaObject d, bool e)
    {
        if (d is not RingControlBase p || !e) return;
        p.OnLayoutUpdate(d.GetValue(RadiusProperty), d.GetValue(ThicknessProperty));
    }

    /// <summary>
    /// Actualiza el estado visual completo del control.
    /// </summary>
    /// <param name="d">Objeto que es el origen del evento.</param>
    /// <param name="e">
    /// Argumentos de cambio de valor de la propiedad de dependencia.
    /// </param>
    protected static void UpdateFullLayout(AvaloniaObject d, bool e)
    {
        if (d is not RingControlBase || !e) return;
        d.CoerceValue(RadiusProperty);
        UpdateThicknessLayout(d, e);
        double r = d.GetValue(RadiusProperty);
        d.SetValue(WidthProperty, r * 2);
        d.SetValue(HeightProperty, r * 2);
    }

    /// <summary>
    /// Actualiza el estado de grosor de los anillos del control de anillo.
    /// </summary>
    /// <param name="d">Objeto que es el origen del evento.</param>
    /// <param name="e">
    /// Argumentos de cambio de valor de la propiedad de dependencia.
    /// </param>
    protected static void UpdateThicknessLayout(AvaloniaObject d, bool e)
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
        UpdateFullLayout(this, true);
    }

    /// <summary>
    /// Cuando se invalida en una clase base, actualiza las figuras que
    /// componen al control.
    /// </summary>
    /// <param name="radius">Radio del control.</param>
    /// <param name="thickness">Grosor de las líneas del control.</param>
    protected abstract void OnLayoutUpdate(double radius, double thickness);

    /// <inheritdoc/>
    public override void EndInit()
    {
        base.EndInit();
        SetControlSize();
    }
}
