/*
StretchyWrapPanel.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     "Surfin Bird" (Original implementation) <https://stackoverflow.com/users/4267982/surfin-bird>
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace TheXDS.MCART.Controls
{
    /// <inheritdoc />
    /// <summary>
    ///     <see cref="Panel" /> que permite organizar los controles como una
    ///     línea justificada con sobreflujo, opcionalmente aplicando una
    ///     transformación proporcional a los mismos.
    /// </summary>
    /// <remarks>
    ///     Implementación de referencia original por "Surfin Bird".
    /// </remarks>
    public class StretchyWrapPanel : Panel
    {
        /* Cambios:
         * Streamlining del código
         * Escritura de documentación
         */

        private struct UvSize
        {
            internal UvSize(Orientation orientation, double width, double height)
            {
                U = V = 0d;
                _orientation = orientation;
                Width = width;
                Height = height;
            }

            internal UvSize(Orientation orientation)
            {
                U = V = 0d;
                _orientation = orientation;
            }

            internal double U;
            internal double V;
            private readonly Orientation _orientation;

            internal double Width
            {
                get => _orientation == Orientation.Horizontal ? U : V;
                private set
                {
                    if (_orientation == Orientation.Horizontal)
                        U = value;
                    else
                        V = value;
                }
            }

            internal double Height
            {
                get => _orientation == Orientation.Horizontal ? V : U;
                private set
                {
                    if (_orientation == Orientation.Horizontal)
                        V = value;
                    else
                        U = value;
                }
            }
        }

        #region Propiedades de dependencia

        /// <summary>
        ///     Identifica a la propiedad de dependencia <see cref="ItemHeight" />
        /// </summary>
        public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register(nameof(ItemHeight),
            typeof(double), typeof(StretchyWrapPanel),
            new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        ///     Obtiene o establece la altura de los elementos hijos de este <see cref="Panel" />.
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
        public double ItemHeight
        {
            get => (double)GetValue(ItemHeightProperty);
            set => SetValue(ItemHeightProperty, value);
        }

        /// <summary>
        ///     Identifica a la propiedad de dependencia <see cref="ItemWidth" />.
        /// </summary>
        public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register(nameof(ItemWidth),
            typeof(double), typeof(StretchyWrapPanel),
            new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        ///     Obtiene o establece la longitud a aplicar a los elementos hijos de este <see cref="Panel" />.
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
        public double ItemWidth
        {
            get => (double)GetValue(ItemWidthProperty);
            set => SetValue(ItemWidthProperty, value);
        }

        /// <summary>
        ///     Identifica a la propiedad de dependencia <see cref="Orientation" />
        /// </summary>
        public static readonly DependencyProperty OrientationProperty = StackPanel.OrientationProperty.AddOwner(
            typeof(StretchyWrapPanel),
            new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsMeasure,
                OnOrientationChanged));

        private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((StretchyWrapPanel)d)._orientation = (Orientation)e.NewValue;
        }

        /// <summary>
        ///     Obtiene o establece la orientación de la colocación de elementos en este <see cref="Panel" />.
        /// </summary>
        public Orientation Orientation
        {
            get => _orientation;
            set => SetValue(OrientationProperty, value);
        }

        /// <summary>
        ///     Identifica a la propiedad de dependencia <see cref="StretchProportionally" />.
        /// </summary>
        public static readonly DependencyProperty StretchProportionallyProperty = DependencyProperty.Register(
            nameof(StretchProportionally), typeof(bool),
            typeof(StretchyWrapPanel), new PropertyMetadata(true, OnStretchProportionallyChanged));

        private static void OnStretchProportionallyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((StretchyWrapPanel)o)._stretchProportionally = (bool)e.NewValue;
        }

        /// <summary>
        ///     Obtiene o establece un valor que indica si los controles hijos se alargarán proporcionalmente entre sí.
        /// </summary>
        public bool StretchProportionally
        {
            get => _stretchProportionally;
            set => SetValue(StretchProportionallyProperty, value);
        }
        
        #endregion

        private Orientation _orientation = Orientation.Horizontal;

        private bool _stretchProportionally = true;

        /// <inheritdoc />
        /// <summary>
        ///   Si se reemplaza en una clase derivada, coloca los elementos secundarios y determina un tamaño para una clase derivada <see cref="T:System.Windows.FrameworkElement" />.
        /// </summary>
        /// <param name="finalSize">
        ///   Área final dentro del elemento primario que este elemento debe usar para organizarse a sí mismo y a sus elementos secundarios.
        /// </param>
        /// <returns>Tamaño real usado.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            var firstInLine = 0;
            var itemWidth = ItemWidth;
            var itemHeight = ItemHeight;
            double accumulatedV = 0;
            var itemU = Orientation == Orientation.Horizontal ? itemWidth : itemHeight;
            var curLineSize = new UvSize(Orientation);
            var uvFinalSize = new UvSize(Orientation, finalSize.Width, finalSize.Height);
            var itemWidthSet = !double.IsNaN(itemWidth);
            var itemHeightSet = !double.IsNaN(itemHeight);
            var useItemU = Orientation == Orientation.Horizontal ? itemWidthSet : itemHeightSet;

            var children = InternalChildren;

            for (int i = 0, count = children.Count; i < count; i++)
            {
                var child = children[i];
                if (child == null) continue;

                var sz = new UvSize(Orientation, itemWidthSet ? itemWidth : child.DesiredSize.Width,
                    itemHeightSet ? itemHeight : child.DesiredSize.Height);
                if (curLineSize.U + sz.U > uvFinalSize.U)
                {
                    // Need to switch to another line
                    if (!useItemU && StretchProportionally)
                        ArrangeLineProportionally(accumulatedV, curLineSize.V, firstInLine, i, uvFinalSize.Width);
                    else
                        ArrangeLine(accumulatedV, curLineSize.V, firstInLine, i, true,
                            useItemU ? itemU : uvFinalSize.Width / System.Math.Max(1, i - firstInLine));

                    accumulatedV += curLineSize.V;
                    curLineSize = sz;

                    if (sz.U > uvFinalSize.U)
                    {
                        // The element is wider then the constraint - give it a separate line     
                        // Switch to next line which only contain one element
                        if (!useItemU && StretchProportionally)
                            ArrangeLineProportionally(accumulatedV, sz.V, i, ++i, uvFinalSize.Width);
                        else
                            ArrangeLine(accumulatedV, sz.V, i, ++i, true, useItemU ? itemU : uvFinalSize.Width);

                        accumulatedV += sz.V;
                        curLineSize = new UvSize(Orientation);
                    }

                    firstInLine = i;
                }
                else
                {
                    // Continue to accumulate a line
                    curLineSize.U += sz.U;
                    curLineSize.V = System.Math.Max(sz.V, curLineSize.V);
                }
            }

            // Arrange the last line, if any
            if (firstInLine < children.Count)
                if (!useItemU && StretchProportionally)
                    ArrangeLineProportionally(accumulatedV, curLineSize.V, firstInLine, children.Count,
                        uvFinalSize.Width);
                else
                    ArrangeLine(accumulatedV, curLineSize.V, firstInLine, children.Count, true,
                        useItemU ? itemU : uvFinalSize.Width / System.Math.Max(1, children.Count - firstInLine));

            return finalSize;
        }

        /// <inheritdoc />
        /// <summary>
        ///   Si se reemplaza en una clase derivada, mide el tamaño del diseño necesario para los elementos secundarios y determina un tamaño para la clase derivada <see cref="T:System.Windows.FrameworkElement" />.
        /// </summary>
        /// <param name="constraint">
        ///   Tamaño disponible que este elemento puede otorgar a los elementos secundarios.
        ///    Se puede usar infinito como valor para indicar que el elemento se ajustará a cualquier contenido disponible.
        /// </param>
        /// <returns>
        ///   Tamaño que este elemento determina que necesita durante el diseño, según sus cálculos de los tamaños de los elementos secundarios.
        /// </returns>
        protected override Size MeasureOverride(Size constraint)
        {
            var curLineSize = new UvSize(Orientation);
            var panelSize = new UvSize(Orientation);
            var uvConstraint = new UvSize(Orientation, constraint.Width, constraint.Height);
            var itemWidth = ItemWidth;
            var itemHeight = ItemHeight;
            var itemWidthSet = !double.IsNaN(itemWidth);
            var itemHeightSet = !double.IsNaN(itemHeight);

            var childConstraint = new Size(
                itemWidthSet ? itemWidth : constraint.Width,
                itemHeightSet ? itemHeight : constraint.Height);

            var children = InternalChildren;

            foreach(UIElement child in children)
            {
                if (child == null) continue;

                // Flow passes its own constraint to children
                child.Measure(childConstraint);

                // This is the size of the child in UV space
                var sz = new UvSize(Orientation,
                    itemWidthSet ? itemWidth : child.DesiredSize.Width,
                    itemHeightSet ? itemHeight : child.DesiredSize.Height);

                if (curLineSize.U + sz.U > uvConstraint.U)
                {
                    // Need to switch to another line
                    panelSize.U = System.Math.Max(curLineSize.U, panelSize.U);
                    panelSize.V += curLineSize.V;
                    curLineSize = sz;

                    if (!(sz.U > uvConstraint.U)) continue;
                    // The element is wider then the constraint - give it a separate line             
                    panelSize.U = System.Math.Max(sz.U, panelSize.U);
                    panelSize.V += sz.V;
                    curLineSize = new UvSize(Orientation);
                }
                else
                {
                    // Continue to accumulate a line
                    curLineSize.U += sz.U;
                    curLineSize.V = System.Math.Max(sz.V, curLineSize.V);
                }
            }

            // The last line size, if any should be added
            panelSize.U = System.Math.Max(curLineSize.U, panelSize.U);
            panelSize.V += curLineSize.V;

            // Go from UV space to W/H space
            return new Size(panelSize.Width, panelSize.Height);
        }

        private void ArrangeLine(double v, double lineV, int start, int end, bool useItemU, double itemU)
        {
            var u = 0d;
            var horizontal = Orientation == Orientation.Horizontal;
            var children = InternalChildren;

            for (var i = start; i < end; i++)
            {
                var child = children[i];
                if (child == null) continue;
                var childSize = new UvSize(Orientation, child.DesiredSize.Width, child.DesiredSize.Height);
                var layoutSlotU = useItemU ? itemU : childSize.U;
                child.Arrange(new Rect(horizontal ? u : v, horizontal ? v : u,
                    horizontal ? layoutSlotU : lineV, horizontal ? lineV : layoutSlotU));
                u += layoutSlotU;
            }
        }

        private void ArrangeLineProportionally(double v, double lineV, int start, int end, double limitU)
        {
            var u = 0d;
            var horizontal = Orientation == Orientation.Horizontal;
            var children = InternalChildren;

            var total = 0d;
            for (var i = start; i < end; i++)
                total += horizontal ? children[i].DesiredSize.Width : children[i].DesiredSize.Height;

            var uMultipler = total >0 ? limitU / total: 0;
            for (var i = start; i < end; i++)
            {
                var child = children[i];
                if (child == null) continue;
                var childSize = new UvSize(Orientation, child.DesiredSize.Width, child.DesiredSize.Height);
                var layoutSlotU = childSize.U * uMultipler;
                child.Arrange(new Rect(horizontal ? u : v, horizontal ? v : u,
                    horizontal ? layoutSlotU : lineV, horizontal ? lineV : layoutSlotU));
                u += layoutSlotU;
            }
        }
    }
}