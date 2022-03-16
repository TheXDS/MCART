/*
DelegateExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene numerosas extensiones para el tipo System.DateTime del
CLR, supliéndolo de nueva funcionalidad previamente no existente, o de
invocación compleja.

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

namespace TheXDS.MCART.Types.Factory;
using System;
using TheXDS.MCART.Attributes;

/// <summary>
/// Contiene extensiones para objetos de tipo <see cref="Delegate"/>.
/// </summary>
public static class DelegateExtensions
{
    /// <summary>
    /// Obtiene un nombre descriptivo para un <see cref="Delegate"/>.
    /// </summary>
    /// <param name="d">
    /// <see cref="Delegate"/> para el cual obtener un nombre
    /// descriptivo.
    /// </param>
    /// <returns>
    /// Un nombre descriptivo para un <see cref="Delegate"/>, o el
    /// nombre del método representado por el delegado si este no 
    /// contiene un atributo <see cref="NameAttribute"/>.
    /// </returns>
    public static string NameOf(this Delegate d)
    {
        return d.GetAttr<NameAttribute>()?.Value ?? d.Method.NameOf();
    }
}
