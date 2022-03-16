/*
PropertyBuildInfoExtensions.cs

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

namespace TheXDS.MCART.Types.Factory;
using System.Reflection;
using System.Reflection.Emit;
using static TheXDS.MCART.Types.TypeBuilderHelpers;
using Errors = Resources.TypeFactoryErrors;

/// <summary>
/// Contiene extensiones útiles para objetos de tipo
/// <see cref="PropertyBuildInfo"/>.
/// </summary>
public static class PropertyBuildInfoExtensions
{
    /// <summary>
    /// Crea un campo de almacenamiento para la propiedad y configura el
    /// método <see langword="get"/> de la propiedad para leer el valor del
    /// mismo.
    /// </summary>
    /// <param name="builder">
    /// <see cref="PropertyBuildInfo"/> en el cual se definirá el campo de
    /// almacenamiento.
    /// </param>
    /// <param name="field">
    /// <see cref="FieldBuilder"/> generado que repesenta al campo de
    /// almacenamiento.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="builder"/>, permitiendo el
    /// uso de sintaxis Fluent.
    /// </returns>
    public static PropertyBuildInfo WithBackingField(this PropertyBuildInfo builder, out FieldBuilder field)
    {
        if (builder.Field is not null) throw Errors.PropFieldAlreadyDefined();
        if ((builder.Getter?.ILOffset ?? 0) != 0) throw Errors.PropGetterAlreadyDefined();
        field = builder.TypeBuilder.DefineField(UndName(builder.Member.Name), builder.Member.PropertyType, FieldAttributes.Private | FieldAttributes.PrivateScope);
        builder.Getter?.LoadField(field).Return();
        return builder;
    }
}
