/*
FrameworkElementExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author:
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
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TheXDS.MCART.Attributes;

namespace TheXDS.MCART.Helpers;

/// <summary>
/// Contains extension methods for the <see cref="FrameworkElement"/>
/// class.
/// </summary>
public static class FrameworkElementExtensions
{
    /// <summary>
    /// Binds a dependency property on the element to a source
    /// <see cref="DependencyObject"/>.
    /// </summary>
    /// <param name="obj">Target element of the binding.</param>
    /// <param name="dp">Dependency property to bind.</param>
    /// <param name="source">Binding source object.</param>
    [Sugar]
    public static void Bind(this FrameworkElement obj, DependencyProperty dp, DependencyObject source)
    {
        obj.Bind(dp, (object)source, dp, BindingMode.TwoWay);
    }

    /// <summary>
    /// Binds a dependency property on the element to a source
    /// <see cref="DependencyObject"/> using the specified mode.
    /// </summary>
    /// <param name="obj">Target element of the binding.</param>
    /// <param name="dp">Dependency property to bind.</param>
    /// <param name="source">Binding source object.</param>
    /// <param name="mode">Binding mode to use.</param>
    [Sugar]
    public static void Bind(this FrameworkElement obj, DependencyProperty dp, DependencyObject source, BindingMode mode)
    {
        obj.Bind(dp, (object)source, dp, mode);
    }

    /// <summary>
    /// Binds a dependency property on the element to a source that
    /// implements <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    /// <param name="obj">Target element of the binding.</param>
    /// <param name="dp">Dependency property to bind.</param>
    /// <param name="source">Source implementing INotifyPropertyChanged.</param>
    [Sugar]
    public static void Bind(this FrameworkElement obj, DependencyProperty dp, INotifyPropertyChanged source)
    {
        obj.Bind(dp, (object)source, dp, BindingMode.TwoWay);
    }

    /// <summary>
    /// Binds a dependency property on the element to a source that
    /// implements <see cref="INotifyPropertyChanged"/>, with the
    /// specified binding mode.
    /// </summary>
    /// <param name="obj">Target element of the binding.</param>
    /// <param name="dp">Dependency property to bind.</param>
    /// <param name="source">Source implementing INotifyPropertyChanged.</param>
    /// <param name="mode">Binding mode to use.</param>
    [Sugar]
    public static void Bind(this FrameworkElement obj, DependencyProperty dp, INotifyPropertyChanged source, BindingMode mode)
    {
        obj.Bind(dp, (object)source, dp, mode);
    }

    /// <summary>
    /// Binds the specified target dependency property to a source
    /// dependency property on a <see cref="DependencyObject"/>.
    /// </summary>
    /// <param name="obj">Target element of the binding.</param>
    /// <param name="targetDp">Target dependency property.</param>
    /// <param name="source">Binding source object.</param>
    /// <param name="sourceDp">Source dependency property.</param>
    [Sugar]
    public static void Bind(this FrameworkElement obj, DependencyProperty targetDp, DependencyObject source, DependencyProperty sourceDp)
    {
        obj.Bind(targetDp, (object)source, sourceDp, BindingMode.TwoWay);
    }

    /// <summary>
    /// Binds the specified target dependency property to a source
    /// dependency property on a <see cref="DependencyObject"/>, using
    /// the provided binding mode.
    /// </summary>
    /// <param name="obj">Target element of the binding.</param>
    /// <param name="targetDp">Target dependency property.</param>
    /// <param name="source">Binding source object.</param>
    /// <param name="sourceDp">Source dependency property.</param>
    /// <param name="mode">Binding mode to use.</param>
    [Sugar]
    public static void Bind(this FrameworkElement obj, DependencyProperty targetDp, DependencyObject source, DependencyProperty sourceDp, BindingMode mode)
    {
        obj.Bind(targetDp, (object)source, sourceDp, mode);
    }

    /// <summary>
    /// Binds the specified target dependency property to a source
    /// property on an object that implements
    /// <see cref="INotifyPropertyChanged"/>, using two-way binding.
    /// </summary>
    /// <param name="obj">Target element of the binding.</param>
    /// <param name="targetDp">Target dependency property.</param>
    /// <param name="source">Source implementing INotifyPropertyChanged.</param>
    /// <param name="sourceDp">Source dependency property.</param>
    [Sugar]
    public static void Bind(this FrameworkElement obj, DependencyProperty targetDp, INotifyPropertyChanged source, DependencyProperty sourceDp)
    {
        obj.Bind(targetDp, (object)source, sourceDp, BindingMode.TwoWay);
    }

    /// <summary>
    /// Binds the specified target dependency property to a source
    /// property on an object that implements
    /// <see cref="INotifyPropertyChanged"/>, using the specified mode.
    /// </summary>
    /// <param name="obj">Target element of the binding.</param>
    /// <param name="targetDp">Target dependency property.</param>
    /// <param name="source">Source implementing INotifyPropertyChanged.</param>
    /// <param name="sourceDp">Source dependency property.</param>
    /// <param name="mode">Binding mode to use.</param>
    [Sugar]
    public static void Bind(this FrameworkElement obj, DependencyProperty targetDp, INotifyPropertyChanged source, DependencyProperty sourceDp, BindingMode mode)
    {
        obj.Bind(targetDp, (object)source, sourceDp, mode);
    }

    /// <summary>
    /// Binds a target dependency property on the element to a property
    /// on an arbitrary source object using the given binding mode.
    /// </summary>
    /// <param name="obj">Target element of the binding.</param>
    /// <param name="targetDp">Target dependency property.</param>
    /// <param name="source">Binding source object.</param>
    /// <param name="sourceDp">Source dependency property.</param>
    /// <param name="mode">Binding mode to use.</param>
    [Sugar]
    public static void Bind(this FrameworkElement obj, DependencyProperty targetDp, object source, DependencyProperty sourceDp, BindingMode mode)
    {
        obj.SetBinding(targetDp, new Binding
        {
            Path = new PropertyPath(sourceDp),
            Mode = mode,
            Source = source
        });
    }

    /// <summary>
    /// Creates a bitmap from a <see cref="FrameworkElement"/>.
    /// </summary>
    /// <param name="f">The <see cref="FrameworkElement"/> to render.</param>
    /// <returns>
    /// A <see cref="RenderTargetBitmap"/> containing a rendered image of
    /// <paramref name="f"/>.
    /// </returns>
    public static RenderTargetBitmap Render(this FrameworkElement f)
    {
        return f.Render(new Size((int)f.ActualWidth, (int)f.ActualHeight), (int)Windows.GetDpi().Width);
    }

    /// <summary>
    /// Creates a bitmap from a <see cref="FrameworkElement"/>, using the
    /// specified DPI value.
    /// </summary>
    /// <param name="f">The <see cref="FrameworkElement"/> to render.</param>
    /// <param name="dpi">Dots-per-inch value for the bitmap.</param>
    /// <returns>
    /// A <see cref="RenderTargetBitmap"/> containing a rendered image of
    /// <paramref name="f"/>.
    /// </returns>
    public static RenderTargetBitmap Render(this FrameworkElement f, int dpi)
    {
        return f.Render(new Size((int)f.ActualWidth, (int)f.ActualHeight), dpi);
    }

    /// <summary>
    /// Creates a bitmap of a <see cref="FrameworkElement"/> by setting
    /// the size at which the control will be drawn, so it does not need
    /// to be visible in the UI.
    /// </summary>
    /// <param name="f">The <see cref="FrameworkElement"/> to render.</param>
    /// <param name="inSize">Size used to measure the control.</param>
    /// <param name="outSize">Canvas size to render the control onto.</param>
    /// <param name="dpi">Dots-per-inch value for the bitmap.</param>
    /// <returns>
    /// A <see cref="RenderTargetBitmap"/> containing a rendered image of
    /// <paramref name="f"/>.
    /// </returns>
    public static RenderTargetBitmap Render(this FrameworkElement f, Size inSize, Size outSize, int dpi)
    {
        f.Measure(inSize);
        f.Arrange(new Rect(inSize));
        return f.Render(outSize, dpi);
    }
}
