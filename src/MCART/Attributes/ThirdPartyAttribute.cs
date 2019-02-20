﻿/*
TextAttribute.cs

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

using System;
using System.Windows;
using TheXDS.MCART.Component;
using static System.AttributeTargets;

namespace TheXDS.MCART.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Indica que un elemento proviene de orígenes de terceros.
    /// </summary>
    /// <remarks>
    ///     Es recomendable que al aplicar este atributo, también se aplique un
    ///     atributo que indique la licencia del mismo.
    /// </remarks>
    [AttributeUsage(Method | Class | Module | Assembly)]
    [Serializable]
    public sealed class ThirdPartyAttribute : Attribute
    {
    }
}