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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TheXDS.MCART.Math;
using static TheXDS.MCART.WpfUi;

namespace TheXDS.MCART.Controls
{
    /// <summary>
    /// Control simple que indica al usuario que la aplicación está ocupada.
    /// </summary>
    public class BusyIndicator : Control
    {
        /// <summary>
        /// Enumera los distintos estados en los que este control puede ser
        /// presentado.
        /// </summary>
        public enum ControlState : byte
        {
            /// <summary>
            /// Estado predeterminado del control.
            /// </summary>
            Default,

            /// <summary>
            /// Mostrar una animación de espera.
            /// </summary>
            Waiting
        }

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Radius" />.
        /// </summary>
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(nameof(Radius),
            typeof(double), typeof(BusyIndicator),
            new FrameworkPropertyMetadata(24.0, FrameworkPropertyMetadataOptions.AffectsMeasure, SetControlSize, CoerceRadius), ChkDblValue);

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="State" />.
        /// </summary>
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(nameof(State),
            typeof(ControlState), typeof(BusyIndicator), new PropertyMetadata(ControlState.Default));

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Stroke" />.
        /// </summary>
        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(nameof(Stroke),
            typeof(Brush), typeof(BusyIndicator), new PropertyMetadata(SystemColors.HighlightBrush));

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Stroke2" />.
        /// </summary>
        public static readonly DependencyProperty Stroke2Property = DependencyProperty.Register(nameof(Stroke2),
            typeof(Brush), typeof(BusyIndicator), new PropertyMetadata(SystemColors.ControlDarkBrush));

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Thickness" />.
        /// </summary>
        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(nameof(Thickness),
            typeof(double), typeof(BusyIndicator),
            new FrameworkPropertyMetadata(4.0, FrameworkPropertyMetadataOptions.AffectsMeasure, SetControlSize, CoerceThickness), ChkDblValue);

        /// <summary>
        /// Inicializa la clase <see cref="BusyIndicator"/>
        /// </summary>
        static BusyIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyIndicator), new FrameworkPropertyMetadata(typeof(BusyIndicator)));
        }

        private static void SetControlSize(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not BusyIndicator { path: { } p }) return;

            d.CoerceValue(ThicknessProperty);
            d.CoerceValue(RadiusProperty);
            var r = (double)d.GetValue(RadiusProperty);
            var t = (double)d.GetValue(ThicknessProperty);
            p.Data = GetCircleArc(r, 270, t);
        }

        private static bool ChkDblValue(object value)
        {
            return ((double)value) >= 0;
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

        private Path path = null!;

        /// <summary>
        /// Obtiene o establece el radio de este control.
        /// </summary>
        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si el control se dibujará
        /// en su estado secundario.
        /// </summary>
        public ControlState State
        {
            get => (ControlState)GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        /// <summary>
        /// Obtiene o establece el <see cref="Brush" /> a aplicar al control.
        /// </summary>
        public Brush? Stroke
        {
            get => (Brush?)GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
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

        /// <summary>
        /// Obtiene o establece el grosor de los elementos de este control.
        /// </summary>
        public double Thickness
        {
            get => (double)GetValue(ThicknessProperty);
            set => SetValue(ThicknessProperty, value);
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            path = (Path)GetTemplateChild($"PART_{nameof(path)}");
            SetControlSize(this, new DependencyPropertyChangedEventArgs());
        }
    }
}