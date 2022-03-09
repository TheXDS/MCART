/*
LocalizedDescriptionAttribute.cs

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

namespace TheXDS.MCART.Attributes;
using System;
using System.Resources;
using static System.AttributeTargets;
using static Misc.Internals;

/// <summary>
/// Establece un nombre personalizado localizado para describir este elemento.
/// </summary>
[AttributeUsage(All)]
[Serializable]
public sealed class LocalizedDescriptionAttribute : System.ComponentModel.DescriptionAttribute
{
    private readonly string _stringId;
    private readonly ResourceManager _res;

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="LocalizedDescriptionAttribute" />.
    /// </summary>
    /// <param name="stringId">Id de la cadena a localizar.</param>
    /// <param name="resourceType">Tipo que contiene la información de localización a utilizar.</param>
    public LocalizedDescriptionAttribute(string stringId, Type resourceType)
    {
        _stringId = EmptyChecked(stringId, nameof(stringId));
        _res = new ResourceManager(NullChecked(resourceType, nameof(resourceType)));
    }

    /// <inheritdoc/>
    public override string Description => _res.GetString(_stringId) ?? $"[[{_stringId}]]";
}
