﻿/*
IExposeGuid.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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

namespace TheXDS.MCART.Types.Base;
using System;
using System.Runtime.InteropServices;
using TheXDS.MCART.Resources;
using TheXDS.MCART.Types.Factory;

/// <summary>
/// Define una serie de métodos a implementar por un tipo que exponga
/// un <see cref="Guid"/> como una propiedad de instancia.
/// </summary>
public interface IExposeGuid
{
    /// <summary>
    /// Obtiene el <see cref="Guid"/> asociado a este objeto.
    /// </summary>
    Guid Guid
    {
        get
        {
            Type? t = GetType();
            GuidAttribute? g = t.GetAttr<GuidAttribute>() ?? throw Errors.MissingGuidAttr(t);
            return new Guid(g.Value);
        }
    }
}
