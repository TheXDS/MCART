/*
ApplicationInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#nullable enable

using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TheXDS.MCART.Component
{
    /// <inheritdoc />
    /// <summary>
    ///     Expone la información de ensamblado de una aplicación de WPF.
    /// </summary>
    public class ApplicationInfo : IExposeInfo<UIElement?>
    {
        private readonly AssemblyInfo _infoExposer;

        private static UIElement InferIcon(Assembly asm)
        {
            var uri = new UriBuilder(asm.CodeBase ?? string.Empty);
            var path = Uri.UnescapeDataString(uri.Path);
            using var sysicon = System.Drawing.Icon.ExtractAssociatedIcon(path) ?? throw new Exception();
            return new Image
            {
                Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                    sysicon.Handle,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions())
            };
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ApplicationInfo" />.
        /// </summary>
        /// <param name="application">
        ///     Aplicación de la cual se mostrará la información.
        /// </param>
        public ApplicationInfo(Application application) 
            : this(application, null) { }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ApplicationInfo" />.
        /// </summary>
        /// <param name="application">
        ///     Aplicación de la cual se mostrará la información.
        /// </param>
        /// <param name="inferIcon">
        ///     <see langword="true"/> para intentar determinar el ícono de la
        ///     aplicación, <see langword="false"/> para no mostrar un ícono.
        /// </param>
        public ApplicationInfo(Application application, bool inferIcon)
            : this(application,inferIcon ? InferIcon(application.GetType().Assembly) : null) { }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ApplicationInfo"/>.
        /// </summary>
        /// <param name="application">
        ///     Aplicación de la cual se mostrará la información.
        /// </param>
        /// <param name="icon">Ícono a mostrar de la aplicación.</param>
        public ApplicationInfo(Application application, UIElement? icon)
            : this(application.GetType().Assembly, icon) { }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ApplicationInfo"/>
        /// </summary>
        /// <param name="assembly">
        ///     Ensamblado del cual se mostrará la información.
        /// </param>
        /// <param name="icon">Ícono a mostrar del ensamblado.</param>
        public ApplicationInfo(Assembly assembly, UIElement? icon)
        {
            _infoExposer = new AssemblyInfo(assembly);
            Icon = icon;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ApplicationInfo" />.
        /// </summary>
        /// <param name="assembly">
        ///     Ensamblado del cual se mostrará la información.
        /// </param>
        /// <param name="inferIcon">
        ///     <see langword="true"/> para intentar determinar el ícono del
        ///     ensamblado, <see langword="false"/> para no mostrar un ícono.
        /// </param>
        public ApplicationInfo(Assembly assembly, bool inferIcon)
            : this(assembly, inferIcon ? InferIcon(assembly) : null) { }


        /// <inheritdoc />
        /// <summary>
        ///     Obtiene el nombre del elemento.
        /// </summary>
        public string Name => _infoExposer.Name;

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene la descripción del elemento.
        /// </summary>
        public string Description => _infoExposer.Description ?? string.Empty;

        /// <summary>
        ///     Obtiene un ícono opcional a mostrar que describe al elemento.
        /// </summary>
        public UIElement? Icon { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Devuelve el autor del <see cref="IExposeInfo" />
        /// </summary>
        public string? Author => _infoExposer.Author;

        /// <inheritdoc />
        /// <summary>
        ///     Devuelve el Copyright del <see cref="IExposeInfo" />
        /// </summary>
        public string? Copyright => _infoExposer.Copyright;

        /// <inheritdoc />
        /// <summary>
        ///     Devuelve la licencia del <see cref="IExposeInfo" />
        /// </summary>
        public string? License => _infoExposer.License;

        /// <inheritdoc />
        /// <summary>
        ///     Devuelve la versión del <see cref="IExposeInfo" />
        /// </summary>
        public Version? Version => _infoExposer.Version;

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un valor que determina si este <see cref="IExposeInfo" />
        ///     contiene información de licencia.
        /// </summary>
        public bool HasLicense => _infoExposer.HasLicense;

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un valor que indica si este <see cref="IExposeInfo" />
        ///     cumple con el Common Language Standard (CLS)
        /// </summary>
        public bool ClsCompliant => _infoExposer.ClsCompliant;

        /// <summary>
        ///     Referencia al ensamblado del cual se expone la información.
        /// </summary>
        public Assembly Assembly => _infoExposer.Assembly;

        /// <summary>
        ///     Obtiene la versión informacional de este
        ///     <see cref="IExposeInfo"/>.
        /// </summary>
        public string? InformationalVersion => _infoExposer.InformationalVersion ?? Version?.ToString();
    }
}