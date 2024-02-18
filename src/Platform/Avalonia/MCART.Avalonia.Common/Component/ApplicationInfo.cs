/*
ApplicationInfo.cs

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
using System.Reflection;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Component;

/// <summary>
/// Expone la información de ensamblado de una aplicación de WPF.
/// </summary>
public class ApplicationInfo : AssemblyInfo, IExposeExtendedGuiInfo<Image?>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ApplicationInfo" />.
    /// </summary>
    /// <param name="application">
    /// Aplicación de la cual se mostrará la información.
    /// </param>
    public ApplicationInfo(Application application) : this(application, null) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ApplicationInfo"/>.
    /// </summary>
    /// <param name="application">
    /// Aplicación de la cual se mostrará la información.
    /// </param>
    /// <param name="icon">Ícono a mostrar de la aplicación.</param>
    public ApplicationInfo(Application application, Image? icon)
        : this(application.GetType().Assembly, icon) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ApplicationInfo"/>
    /// </summary>
    /// <param name="assembly">
    /// Ensamblado del cual se mostrará la información.
    /// </param>
    /// <param name="icon">Ícono a mostrar del ensamblado.</param>
    public ApplicationInfo(Assembly assembly, Image? icon) : base(assembly)
    {
        Icon = icon;
    }

    /// <summary>
    /// Obtiene un ícono opcional a mostrar que describe al elemento.
    /// </summary>
    public virtual Image? Icon { get; }
}
