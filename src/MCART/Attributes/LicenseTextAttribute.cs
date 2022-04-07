/*
LicenseTextAttribute.cs

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
using TheXDS.MCART.Resources;
using static System.AttributeTargets;
using static TheXDS.MCART.Misc.Internals;

/// <summary>
/// Establece el texto de licencia a asociar con el elemento.
/// </summary>
[AttributeUsage(Class | Module | Assembly)]
public sealed class LicenseTextAttribute : LicenseAttributeBase
{
    private readonly string _title;

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="LicenseTextAttribute" />.
    /// </summary>
    /// <param name="title">Título de la licencia</param>
    /// <param name="licenseText">Texto de la licencia.</param>
    public LicenseTextAttribute(string title, string licenseText) 
        : base(licenseText)
    {
        _title = EmptyChecked(title, nameof(title));
        EmptyCheck(licenseText, nameof(licenseText));
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="LicenseTextAttribute" />.
    /// </summary>
    /// <param name="licenseText">Texto de la licencia.</param>
    public LicenseTextAttribute(string licenseText)
        : this(EmptyChecked(licenseText, nameof(licenseText)).Split('\n', 2)[0].Trim(), licenseText)
    {
    }

    /// <summary>
    /// Obtiene una licencia asociada a este atributo.
    /// </summary>
    /// <param name="context">
    /// Objeto del cual se ha extraído este atributo.
    /// </param>
    /// <returns>
    /// Una licencia asociada a este atributo.
    /// </returns>
    public override License GetLicense(object context)
    {
        return new TextLicense(_title, Value!);
    }
}
