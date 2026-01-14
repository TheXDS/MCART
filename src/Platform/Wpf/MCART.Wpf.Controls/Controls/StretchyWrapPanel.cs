/*
StretchyWrapPanel.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     "Surfin Bird" (Original implementation) <https://stackoverflow.com/users/4267982/surfin-bird>
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using TheXDS.MCART.Math;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Controls;

/// <summary>
/// A <see cref="Panel"/> that arranges child elements as a justified
/// line with overflow, optionally applying proportional scaling.
/// </summary>
/// <remarks>
/// Original reference implementation by "Surfin Bird".
/// </remarks>
public class StretchyWrapPanel : Panel
{
    private struct UvSize
    {
        internal UvSize(Orientation orientation, StretchyWrapPanel host, UIElement element)
        {
            _u = _v = 0d;
            _orientation = orientation;
            Width = host.ItemWidth.OrIfInvalid(element.DesiredSize.Width);
            Height = host.ItemWidth.OrIfInvalid(element.DesiredSize.Height);
        }

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

    /// <summary>
    /// Identifies the dependency property for <see cref="ItemHeight"/>.
    /// </summary>
    public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register(nameof(ItemHeight),
        typeof(double), typeof(StretchyWrapPanel),
        new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure));

    /// <summary>
    /// Gets or sets the height of child items in this <see cref="Panel"/>.
    /// </summary>
    [TypeConverter(typeof(LengthConverter))]
    public double ItemHeight
    {
        get => (double)GetValue(ItemHeightProperty);
        set => SetValue(ItemHeightProperty, value);
    }

    /// <summary>
    /// Identifies the dependency property for <see cref="ItemWidth"/>.
    /// </summary>
    public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register(nameof(ItemWidth),
        typeof(double), typeof(StretchyWrapPanel),
        new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure));

    /// <summary>
    /// Gets or sets the width to apply to child items in this
    /// <see cref="Panel"/>.
    /// </summary>
    [TypeConverter(typeof(LengthConverter))]
    public double ItemWidth
    {
        get => (double)GetValue(ItemWidthProperty);
        set => SetValue(ItemWidthProperty, value);
    }

    /// <summary>
    /// Identifies the dependency property for <see cref="Orientation"/>.
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
    /// Gets or sets the orientation used to place child elements.
    /// </summary>
    public Orientation Orientation
    {
        get => _orientation;
        set => SetValue(OrientationProperty, value);
    }

    /// <summary>
    /// Identifies the dependency property for
    /// <see cref="StretchProportionally"/>.
    /// </summary>
    public static readonly DependencyProperty StretchProportionallyProperty = DependencyProperty.Register(
        nameof(StretchProportionally), typeof(bool),
        typeof(StretchyWrapPanel), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsArrange, OnStretchProportionallyChanged));

    private static void OnStretchProportionallyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
        ((StretchyWrapPanel)o)._stretchProportionally = (bool)e.NewValue;
    }

    /// <summary>
    /// Gets or sets whether child elements are stretched proportionally.
    /// </summary>
    public bool StretchProportionally
    {
        get => _stretchProportionally;
        set => SetValue(StretchProportionallyProperty, value);
    }

    private Orientation _orientation = Orientation.Horizontal;
    private bool _stretchProportionally = true;

    /// <summary>
    /// When overridden, positions child elements and determines a size
    /// for a derived <see cref="FrameworkElement"/>.
    /// </summary>
    /// <param name="finalSize">The final area within the parent element.
    /// </param>
    /// <returns>The actual size used.</returns>
    protected override Size ArrangeOverride(Size finalSize)
    {
        int i = 0;
        int firstInLine = 0;
        double accumulatedV = 0;
        UvSize curLineSize = new(Orientation);
        UvSize uvFinalSize = new(Orientation, finalSize.Width, finalSize.Height);
        (bool useItemU, double itemU) = GetUseItemU();
        void DoArrange(double v, int fil, int itm, double fnz)
        {
            if (!useItemU && StretchProportionally)
            {
                ArrangeLineProportionally(accumulatedV, v, fil, itm, uvFinalSize.Width);
            }
            else
            {
                ArrangeLine(accumulatedV, v, fil, itm, useItemU ? itemU : fnz);
            }
        }
        foreach (UIElement child in InternalChildren.NotNull())
        {
            UvSize sz = new(Orientation, this, child);
            if (curLineSize._u + sz._u > uvFinalSize._u)
            {
                if (sz._u > uvFinalSize._u)
                {
                    DoArrange(sz._v, i, ++i, uvFinalSize.Width);
                    accumulatedV += sz._v;
                    curLineSize = new UvSize(Orientation);
                }
                else
                {
                    DoArrange(curLineSize._v, firstInLine, i, uvFinalSize.Width / System.Math.Max(1, i - firstInLine));
                    accumulatedV += curLineSize._v;
                    curLineSize = sz;
                }
                firstInLine = i;
            }
            else
            {
                curLineSize._u += sz._u;
                curLineSize._v = System.Math.Max(sz._v, curLineSize._v);
            }
            i++;
        }
        if (firstInLine < InternalChildren.Count)
        {
            DoArrange(curLineSize._v, firstInLine, InternalChildren.Count, uvFinalSize.Width / System.Math.Max(1, InternalChildren.Count - firstInLine));
        }
        return finalSize;
    }

    /// <summary>
    /// When overridden, measures the size required for child elements
    /// and determines a size for the derived
    /// <see cref="FrameworkElement"/>.
    /// </summary>
    /// <param name="constraint">Available size for child elements. Use
    /// infinity to indicate unlimited space.</param>
    /// <returns>
    /// The size required during layout based on child measurements.
    /// </returns>
    protected override Size MeasureOverride(Size constraint)
    {
        UvSize curLineSize = new(Orientation);
        UvSize panelSize = new(Orientation);
        UvSize uvConstraint = new(Orientation, constraint.Width, constraint.Height);
        Size childConstraint = new(ItemWidth.OrIfInvalid(constraint.Width), ItemHeight.OrIfInvalid(constraint.Height));
        foreach (UIElement child in InternalChildren.NotNull())
        {
            child.Measure(childConstraint);
            UvSize sz = new(Orientation, this, child);
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

    private (bool, double) GetUseItemU()
    {
        bool useItemU;
        double itemU = 0.0;
        if (useItemU = !double.IsNaN(ItemHeight))
        {
            itemU = Orientation == Orientation.Horizontal
                ? ItemWidth
                : ItemHeight;
        }
        return (useItemU, itemU);
    }

    private void ArrangeLine(double v, double lineV, int start, int end, double itemU)
    {
        double u = 0d;
        bool horizontal = Orientation == Orientation.Horizontal;
        UIElement[] children = InternalChildren.Cast<UIElement>().ToArray()[start..end];
        foreach (UIElement child in children)
        {
            double layoutSlotU = itemU;
            child.Arrange(horizontal ? new Rect(u, v, layoutSlotU, lineV) : new Rect(v, u, lineV, layoutSlotU));
            u += layoutSlotU;
        }
    }

    private void ArrangeLineProportionally(double v, double lineV, int start, int end, double limitU)
    {
        double u = 0d;
        bool horizontal = Orientation == Orientation.Horizontal;
        UIElement[] children = InternalChildren.Cast<UIElement>().ToArray()[start..end];
        double total = horizontal ? children.Sum(p => p.DesiredSize.Width) : children.Sum(p=>p.DesiredSize.Height);
        double uMultiplier = total > 0 ? limitU / total : 0;
        foreach (UIElement child in children)
        {
            UvSize childSize = new(Orientation, child.DesiredSize.Width, child.DesiredSize.Height);
            double layoutSlotU = childSize._u * uMultiplier;
            child.Arrange(horizontal ? new Rect(u, v, layoutSlotU, lineV) : new Rect(v, u, lineV, layoutSlotU));
            u += layoutSlotU;
        }
    }
}
