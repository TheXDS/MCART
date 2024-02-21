/*
BusyIndicator.cs

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
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using TheXDS.MCART.Controls.Base;
using TheXDS.MCART.Helpers;
using static TheXDS.MCART.Misc.PrivateInternals;

namespace TheXDS.MCART.Controls;

/// <summary>
/// Control simple que indica al usuario que la aplicación está ocupada.
/// </summary>
[TemplatePart($"PART_{nameof(path)}", typeof(Avalonia.Controls.Shapes.Path))]
[PseudoClasses([":running"])]
public class BusyIndicator : RingControlBase
{
    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="ArcAngle" />.
    /// </summary>
    public static readonly StyledProperty<double> ArcAngleProperty = AvaloniaProperty.Register<BusyIndicator, double>(nameof(ArcAngle),
        defaultValue: 270.0,
        validate: ChkAngle
        //notifying: UpdateLayout
        );

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Running" />.
    /// </summary>
    public static readonly StyledProperty<bool> RunningProperty = AvaloniaProperty.Register<BusyIndicator, bool>(nameof(Running),
        defaultValue: true);

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="Stroke2" />.
    /// </summary>
    public static readonly StyledProperty<IBrush?> Stroke2Property = AvaloniaProperty.Register<BusyIndicator, IBrush?>(nameof(Stroke2),
        defaultValue: Brushes.Gray);

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="RingControlBase.Radius" />.
    /// </summary>
    public static readonly new StyledProperty<double> RadiusProperty = RingControlBase.RadiusProperty.AddOwner<BusyIndicator>();

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="RingControlBase.Thickness" />.
    /// </summary>
    public static readonly new StyledProperty<double> ThicknessProperty = RingControlBase.ThicknessProperty.AddOwner<BusyIndicator>();

    /// <summary>
    /// Identifica a la propiedad de dependencia <see cref="RingControlBase.Stroke" />.
    /// </summary>
    public static readonly new StyledProperty<IBrush?> StrokeProperty = RingControlBase.StrokeProperty.AddOwner<BusyIndicator>();

    static BusyIndicator()
    {
        WidthProperty.OverrideDefaultValue<BusyIndicator>(double.NaN);
        HeightProperty.OverrideDefaultValue<BusyIndicator>(double.NaN);
        HorizontalAlignmentProperty.OverrideDefaultValue<BusyIndicator>(HorizontalAlignment.Center);
        VerticalAlignmentProperty.OverrideDefaultValue<BusyIndicator>(VerticalAlignment.Center);
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="BusyIndicator"/>.
    /// </summary>
    public BusyIndicator()
    {
        UpdatePseudoClasses(Running);
    }

    private Avalonia.Controls.Shapes.Path? path = null;

    /// <summary>
    /// Obtiene o establece el ángulo de cierre del arco de este control.
    /// </summary>
    public double ArcAngle
    {
        get => GetValue(ArcAngleProperty);
        set => SetValue(ArcAngleProperty, value);
    }

    /// <summary>
    /// Obtiene o establece un valor que indica si el control se dibujará
    /// en su estado secundario.
    /// </summary>
    public bool Running
    {
        get => GetValue(RunningProperty);
        set => SetValue(RunningProperty, value);
    }

    /// <summary>
    /// Obtiene o establece el <see cref="Brush" /> a aplicar al estado
    /// secundario de el control.
    /// </summary>
    public IBrush? Stroke2
    {
        get => GetValue(Stroke2Property);
        set => SetValue(Stroke2Property, value);
    }

    /// <inheritdoc/>
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        path = e.NameScope.Get<Avalonia.Controls.Shapes.Path>($"PART_{nameof(path)}");
        SetControlSize();
    }
    
    /// <inheritdoc/>
    protected override void OnLayoutUpdate(double radius, double thickness)
    {
        if (path is null) return;
        path.Data = GetCircleArc(radius, ArcAngle, thickness);
    }

    private static bool ChkAngle(double value)
    {
        return value.IsBetween(0, 360);
    }

    private void UpdatePseudoClasses(bool running)
    {
        PseudoClasses.Set(":running", running);
    }
}
