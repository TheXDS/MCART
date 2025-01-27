/*
ApplicationInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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
/// Expone la información de ensamblado de una aplicación de WPF.
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
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ApplicationInfo" />.
    /// </summary>
    /// <param name="application">
    /// Aplicación de la cual se mostrará la información.
    /// </param>
    public ApplicationInfo(Application application) : base(application, null) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ApplicationInfo" />.
    /// </summary>
    /// <param name="application">
    /// Aplicación de la cual se mostrará la información.
    /// </param>
    /// <param name="inferIcon">
    /// <see langword="true"/> para intentar determinar el ícono de la
    /// aplicación, <see langword="false"/> para no mostrar un ícono.
    /// </param>
    public ApplicationInfo(Application application, bool inferIcon) : this(application, inferIcon ? InferIcon(application.GetType().Assembly) : null) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ApplicationInfo"/>.
    /// </summary>
    /// <param name="application">
    /// Aplicación de la cual se mostrará la información.
    /// </param>
    /// <param name="icon">Ícono a mostrar de la aplicación.</param>
    public ApplicationInfo(Application application, UIElement? icon)
        : this(application.GetType().Assembly, icon) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ApplicationInfo"/>
    /// </summary>
    /// <param name="assembly">
    /// Ensamblado del cual se mostrará la información.
    /// </param>
    /// <param name="icon">Ícono a mostrar del ensamblado.</param>
    public ApplicationInfo(Assembly assembly, UIElement? icon) : base(assembly, GetIconFromOS(assembly))
    {
        Icon = icon;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ApplicationInfo" />.
    /// </summary>
    /// <param name="assembly">
    /// Ensamblado del cual se mostrará la información.
    /// </param>
    /// <param name="inferIcon">
    /// <see langword="true"/> para intentar determinar el ícono del
    /// ensamblado, <see langword="false"/> para no mostrar un ícono.
    /// </param>
    public ApplicationInfo(Assembly assembly, bool inferIcon) : this(assembly, inferIcon ? InferIcon(assembly) : null) { }

    /// <summary>
    /// Obtiene un ícono opcional a mostrar que describe al elemento.
    /// </summary>
    public new virtual UIElement? Icon { get; }
}
