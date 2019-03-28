﻿/*
AssemblyInfo.cs

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

#nullable enable

using System;
using System.Reflection;
using TheXDS.MCART.Annotations;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Misc;

namespace TheXDS.MCART.Component
{
    /// <inheritdoc />
    /// <summary>
    ///     Expone la información de identificación de un ensamblado.
    /// </summary>
    public class AssemblyInfo : IExposeInfo, IExposeAssembly
    {
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="AssemblyInfo"/>
        /// </summary>
        /// <param name="assembly">
        ///     Ensamblado del cual se mostrará la información.
        /// </param>
        public AssemblyInfo(Assembly assembly)
        {
            Assembly = assembly;
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="AssemblyInfo"/>
        /// </summary>
        public AssemblyInfo()
        {
            Assembly = Assembly.GetCallingAssembly();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Devuelve el nombre del <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        /// </summary>
        public string? Name => Assembly.GetAttr<NameAttribute>()?.Value ?? Assembly.GetAttr<AssemblyTitleAttribute>()?.Title;

        /// <inheritdoc />
        /// <summary>
        ///     Devuelve el Copyright del <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        /// </summary>
        public string? Copyright => Assembly.GetAttr<CopyrightAttribute>()?.Value ?? Assembly.GetAttr<AssemblyCopyrightAttribute>()?.Copyright;

        /// <inheritdoc />
        /// <summary>
        ///     Devuelve una descripción del <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        /// </summary>
        public string? Description => Assembly.GetAttr<DescriptionAttribute>()?.Value ?? Assembly.GetAttr<AssemblyDescriptionAttribute>()?.Description;

        /// <inheritdoc />
        /// <summary>
        ///     Devuelve el autor del <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        /// </summary>
        public string? Author => Assembly.GetAttr<AuthorAttribute>()?.Value ?? Assembly.GetAttr<AssemblyCompanyAttribute>()?.Company;

        /// <summary>
        ///     Devuelve la marca comercial del <see cref="Assembly" />
        /// </summary>
        public string? Trademark => Assembly.GetAttr<AssemblyTrademarkAttribute>()?.Trademark;

        /// <summary>
        ///     Devuelve el autor del <see cref="Assembly" />
        /// </summary>
        public string? Product => Assembly.GetAttr<AssemblyProductAttribute>()?.Product;

        /// <inheritdoc />
        /// <summary>
        ///     Devuelve la licencia del <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        /// </summary>
        public string? License => Internal.ReadLicense(Assembly);

        /// <inheritdoc />
        /// <summary>
        ///     Devuelve la versión del <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        /// </summary>
        public Version? Version => Assembly.GetName().Version;

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un valor que determina si este <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        ///     contiene información de licencia.
        /// </summary>
        public bool HasLicense => Internal.HasLicense(Assembly);

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene un valor que indica si este <see cref="T:TheXDS.MCART.Component.IExposeInfo" />
        ///     cumple con el Common Language Standard (CLS)
        /// </summary>
        public bool ClsCompliant => Assembly.HasAttr<CLSCompliantAttribute>();

        /// <inheritdoc />
        /// <summary>
        ///     Obtiene una referencia al ensamblado del cual se expone la información.
        /// </summary>
        public Assembly Assembly { get; }
    }
}