﻿/*
StretchyWrapPanel.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     "Surfin Bird" (Original implementation) <https://stackoverflow.com/users/4267982/surfin-bird>
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

using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Controls
{
    /// <summary>
    /// <see cref="Panel" /> que permite organizar los controles como una
    /// línea justificada con sobreflujo, opcionalmente aplicando una
    /// transformación proporcional a los mismos.
    /// </summary>
    /// <remarks>
    /// Implementación de referencia original por "Surfin Bird".
    /// </remarks>
    public class StretchyWrapPanel : Panel
    {
        /* Cambios:
         * Streamlining del código
         * Escritura de documentación
         * Corrección de errores
         */

        private struct UvSize
        {
            internal UvSize(Orientation orientation, double width, double height)
            {
                _u = _v = 0d;
                _orientation = orientation;
                Width = width;
                Height = height;
            }

            internal UvSize(Orientation orientation)
            {
                _u = _v = 0d;
                _orientation = orientation;
            }

            internal double _u;
            internal double _v;
            private readonly Orientation _orientation;

            internal double Width
            {
                get => _orientation == Orientation.Horizontal ? _u : _v;
                private set
                {
                    if (_orientation == Orientation.Horizontal)
                        _u = value;
                    else
                        _v = value;
                }
            }

            internal double Height
            {
                get => _orientation == Orientation.Horizontal ? _v : _u;
                private set
                {
                    if (_orientation == Orientation.Horizontal)
                        _v = value;
                    else
                        _u = value;
                }
            }
        }

        #region Propiedades de dependencia

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="ItemHeight" />
        /// </summary>
        public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register(nameof(ItemHeight),
            typeof(double), typeof(StretchyWrapPanel),
            new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Obtiene o establece la altura de los elementos hijos de este <see cref="Panel" />.
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
        public double ItemHeight
        {
            get => (double)GetValue(ItemHeightProperty);
            set => SetValue(ItemHeightProperty, value);
        }

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="ItemWidth" />.
        /// </summary>
        public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register(nameof(ItemWidth),
            typeof(double), typeof(StretchyWrapPanel),
            new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// Obtiene o establece la longitud a aplicar a los elementos hijos de este <see cref="Panel" />.
        /// </summary>
        [TypeConverter(typeof(LengthConverter))]
        public double ItemWidth
        {
            get => (double)GetValue(ItemWidthProperty);
            set => SetValue(ItemWidthProperty, value);
        }

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="Orientation" />
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
        /// Obtiene o establece la orientación de la colocación de elementos en este <see cref="Panel" />.
        /// </summary>
        public Orientation Orientation
        {
            get => _orientation;
            set => SetValue(OrientationProperty, value);
        }

        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="StretchProportionally" />.
        /// </summary>
        public static readonly DependencyProperty StretchProportionallyProperty = DependencyProperty.Register(
            nameof(StretchProportionally), typeof(bool),
            typeof(StretchyWrapPanel), new PropertyMetadata(true, OnStretchProportionallyChanged));

        private static void OnStretchProportionallyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((StretchyWrapPanel)o)._stretchProportionally = (bool)e.NewValue;
        }

        /// <summary>
        /// Obtiene o establece un valor que indica si los controles hijos se alargarán proporcionalmente entre sí.
        /// </summary>
        public bool StretchProportionally
        {
            get => _stretchProportionally;
            set => SetValue(StretchProportionallyProperty, value);
        }

        #endregion

        private Orientation _orientation = Orientation.Horizontal;

        private bool _stretchProportionally = true;

        /// <summary>
        ///   Si se reemplaza en una clase derivada, coloca los elementos secundarios y determina un tamaño para una clase derivada <see cref="FrameworkElement" />.
        /// </summary>
        /// <param name="finalSize">
        ///   Área final dentro del elemento primario que este elemento debe usar para organizarse a sí mismo y a sus elementos secundarios.
        /// </param>
        /// <returns>Tamaño real usado.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            int firstInLine = 0;
            double itemWidth = ItemWidth;
            double itemHeight = ItemHeight;
            double accumulatedV = 0;
            double itemU = Orientation == Orientation.Horizontal ? itemWidth : itemHeight;
            UvSize curLineSize = new(Orientation);
            UvSize uvFinalSize = new(Orientation, finalSize.Width, finalSize.Height);
            bool itemWidthSet = !double.IsNaN(itemWidth);
            bool itemHeightSet = !double.IsNaN(itemHeight);
            bool useItemU = Orientation == Orientation.Horizontal ? itemWidthSet : itemHeightSet;

            void DoArrange(double v, int c)
            {
                if (!useItemU && StretchProportionally)
                {
                    ArrangeLineProportionally(accumulatedV, v, firstInLine, c, uvFinalSize.Width);
                }
                else
                {
                    ArrangeLine(accumulatedV, v, firstInLine, c, useItemU ? itemU : uvFinalSize.Width / System.Math.Max(1, c - firstInLine));
                }
            }

            for (int i = 0, count = InternalChildren.Count; i < count; i++)
            {
                if (InternalChildren[i] is not UIElement child) continue;
                UvSize sz = new(Orientation, itemWidthSet ? itemWidth : child.DesiredSize.Width, itemHeightSet ? itemHeight : child.DesiredSize.Height);
                if (curLineSize._u + sz._u > uvFinalSize._u)
                {
                    DoArrange(curLineSize._v, i);
                    accumulatedV += curLineSize._v;
                    curLineSize = sz;
                    if (sz._u > uvFinalSize._u)
                    {
                        DoArrange(sz._v, i);
                        accumulatedV += sz._v;
                        curLineSize = new UvSize(Orientation);
                    }
                    firstInLine = i;
                }
                else
                {
                    curLineSize._u += sz._u;
                    curLineSize._v = System.Math.Max(sz._v, curLineSize._v);
                }
            }
            if (firstInLine < InternalChildren.Count)
            {
                DoArrange(curLineSize._v, InternalChildren.Count);
            }
            return finalSize;
        }

        /// <summary>
        ///   Si se reemplaza en una clase derivada, mide el tamaño del diseño necesario para los elementos secundarios y determina un tamaño para la clase derivada <see cref="FrameworkElement" />.
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
            UvSize curLineSize = new(Orientation);
            UvSize panelSize = new(Orientation);
            UvSize uvConstraint = new(Orientation, constraint.Width, constraint.Height);
            double itemWidth = ItemWidth;
            double itemHeight = ItemHeight;
            bool itemWidthSet = !double.IsNaN(itemWidth);
            bool itemHeightSet = !double.IsNaN(itemHeight);
            Size childConstraint = new( itemWidthSet ? itemWidth : constraint.Width, itemHeightSet ? itemHeight : constraint.Height);
            UIElementCollection? children = InternalChildren;
            foreach (UIElement? child in children)
            {
                if (child is null) continue;
                child.Measure(childConstraint);
                UvSize sz = new(Orientation, itemWidthSet ? itemWidth : child.DesiredSize.Width, itemHeightSet ? itemHeight : child.DesiredSize.Height);
                if (curLineSize._u + sz._u > uvConstraint._u)
                {
                    panelSize._u = System.Math.Max(curLineSize._u, panelSize._u);
                    panelSize._v += curLineSize._v;
                    curLineSize = sz;
                    if (!(sz._u > uvConstraint._u)) continue;
                    panelSize._u = System.Math.Max(sz._u, panelSize._u);
                    panelSize._v += sz._v;
                    curLineSize = new UvSize(Orientation);
                }
                else
                {
                    curLineSize._u += sz._u;
                    curLineSize._v = System.Math.Max(sz._v, curLineSize._v);
                }
            }
            panelSize._u = System.Math.Max(curLineSize._u, panelSize._u);
            panelSize._v += curLineSize._v;
            return new Size(panelSize.Width, panelSize.Height);
        }

        private void ArrangeLine(double v, double lineV, int start, int end, double itemU)
        {
            double u = 0d;
            bool horizontal = Orientation == Orientation.Horizontal;
            for (int i = start; i < end; i++)
            {
                if (InternalChildren[i] is not UIElement child) continue;
                child.Arrange(new Rect(horizontal ? u : v, horizontal ? v : u, horizontal ? itemU : lineV, horizontal ? lineV : itemU));
                u += itemU;
            }
        }

        private void ArrangeLineProportionally(double v, double lineV, int start, int end, double limitU)
        {
            bool horizontal = Orientation == Orientation.Horizontal;
            UIElement[] children = InternalChildren.OfType<UIElement>().ToArray()[start..(end - 1)];
            double total = horizontal ? children.Sum(p =>p.DesiredSize.Width) : children.Sum(p => p.DesiredSize.Height);
            double uMultipler = total > 0 ? limitU / total : 0;
            double u = 0d;

            foreach (UIElement j in children)
            {
                UvSize childSize = new(Orientation, j.DesiredSize.Width, j.DesiredSize.Height);
                double layoutSlotU = childSize._u * uMultipler;
                j.Arrange(new Rect(horizontal ? u : v, horizontal ? v : u,
                    horizontal ? layoutSlotU : lineV, horizontal ? lineV : layoutSlotU));
                u += layoutSlotU;
            }
        }
    }
}