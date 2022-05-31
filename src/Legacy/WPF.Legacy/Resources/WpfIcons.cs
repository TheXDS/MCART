﻿/*
WpfIcons.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TheXDS.MCART.Resources
{
    /// <summary>
    /// Contiene íconos y otras imágenes para utilizar en aplicaciones de
    /// Windows Presentation Framework.
    /// </summary>
    public sealed class WpfIcons : McartIconLibrary<UIElement>
    {
        private static readonly XamlUnpacker _xaml = new(typeof(WpfIcons).Assembly, typeof(Icons).FullName!);

        /// <summary>
        /// Implementa el método de obtención del ícono basado en el nombre
        /// del ícono solicitado.
        /// </summary>
        /// <param name="id">
        /// Id del ícono solicitado.
        /// </param>
        /// <returns>
        /// El ícono solicitado.
        /// </returns>
        protected sealed override UIElement GetIcon([CallerMemberName] string? id = null!)
        {
            return _xaml.Unpack($"{id}_Xml", new DeflateGetter()) as UIElement ?? new TextBlock { Text = "?", FontSize = 256, Foreground = Brushes.DarkRed };
        }
    }
}