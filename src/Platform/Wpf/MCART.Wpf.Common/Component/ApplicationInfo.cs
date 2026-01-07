/*
ApplicationInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
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

using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Component;

/// <summary>
/// Exposes assembly information of a WPF application.
/// </summary>
public class ApplicationInfo : ApplicationInfoBase<Application>, IExposeExtendedGuiInfo<UIElement?>
{
    private static UIElement? InferIcon(Assembly asm)
    {
        if (GetIconFromOS(asm) is not { } systemIcon) return null;
        using (systemIcon)
        {
            return new Image
            {
                Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                    systemIcon.Handle,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions())
            };
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationInfo"/> class.
    /// </summary>
    /// <param name="application">
    /// Application from which information will be displayed.
    /// </param>
    public ApplicationInfo(Application application) : base(application, null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationInfo"/> class.
    /// </summary>
    /// <param name="application">
    /// Application from which information will be displayed.
    /// </param>
    /// <param name="inferIcon">
    /// true to attempt to determine the app's icon, false to not display an icon.
    /// </param>
    public ApplicationInfo(Application application, bool inferIcon)
        : this(application, inferIcon ? InferIcon(application.GetType().Assembly) : null) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationInfo"/> class.
    /// </summary>
    /// <param name="application">
    /// Application from which information will be displayed.
    /// </param>
    /// <param name="icon">Icon to display for the application.</param>
    public ApplicationInfo(Application application, UIElement? icon)
        : this(application.GetType().Assembly, icon) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationInfo"/> class.
    /// </summary>
    /// <param name="assembly">
    /// Assembly from which information will be displayed.
    /// </param>
    /// <param name="icon">Icon to display for the assembly.</param>
    public ApplicationInfo(Assembly assembly, UIElement? icon)
        : base(assembly, GetIconFromOS(assembly))
    {
        Icon = icon;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationInfo"/> class.
    /// </summary>
    /// <param name="assembly">
    /// Assembly from which information will be displayed.
    /// </param>
    /// <param name="inferIcon">
    /// true to attempt to determine the assembly's icon, false to not display an icon.
    /// </param>
    public ApplicationInfo(Assembly assembly, bool inferIcon)
        : this(assembly, inferIcon ? InferIcon(assembly) : null) { }

    /// <summary>
    /// Gets an optional icon to display that describes the element.
    /// </summary>
    public new virtual UIElement? Icon { get; }
}
