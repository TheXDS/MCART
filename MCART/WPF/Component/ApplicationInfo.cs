/*
ApplicationInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
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

// ReSharper disable UnusedMember.Global

using System;
using System.Windows;
using TheXDS.MCART.Annotations;

namespace TheXDS.MCART.Component
{
    /// <inheritdoc />
    /// <summary>
    ///     Expone la información de ensamblado de una aplicación de WPF.
    /// </summary>
    public class ApplicationInfo : IExposeInfo
    {
        private readonly AssemblyDataExposer _infoExposer;

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ApplicationInfo"/>.
        /// </summary>
        /// <param name="application">
        ///     Aplicación de la cual se mostrará la información.
        /// </param>
        public ApplicationInfo([NotNull] Application application)
        {
            _infoExposer = new AssemblyDataExposer(application.GetType().Assembly);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene el nombre del elemento.
        /// </summary>
        public string Name => _infoExposer.Name;

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene la descripción del elemento.
        /// </summary>
        public string Description => _infoExposer.Description;

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un ícono opcional a mostrar que describe al elemento.
        /// </summary>
        public UIElement Icon => _infoExposer.Icon;

        /// <inheritdoc />
        /// <summary>
        ///     Devuelve el autor del <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        /// </summary>
        public string Author => _infoExposer.Author;

        /// <inheritdoc />
        /// <summary>
        ///     Devuelve el Copyright del <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        /// </summary>
        public string Copyright => _infoExposer.Copyright;

        /// <inheritdoc />
        /// <summary>
        ///     Devuelve la licencia del <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        /// </summary>
        public string License => _infoExposer.License;

        /// <inheritdoc />
        /// <summary>
        ///     Devuelve la versión del <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        /// </summary>
        public Version Version => _infoExposer.Version;

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un valor que determina si este <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        ///     contiene información de licencia.
        /// </summary>
        public bool HasLicense => _infoExposer.HasLicense;

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un valor que indica si este <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        ///     cumple con el Common Language Standard (CLS)
        /// </summary>
        public bool ClsCompliant => _infoExposer.ClsCompliant;
    }
}