/*
TypeFactory.cs

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
using System.Collections.Generic;
using System.Reflection.Emit;

namespace TheXDS.MCART.Types
{
    /// <summary>
    ///     Fábrica de tipos. Permite compilar nuevos tipos en Runtime.
    /// </summary>
    public abstract class TypeFactory
    {
        /// <summary>
        ///     Define la estructura de una propiedad construida.
        /// </summary>
        protected class PropertyBuilderInfo
        {
            /// <summary>
            ///     Obtiene propiedades importantes de la propiedad.
            /// </summary>
            [Flags]
            public enum PropertyFlags
            {
                /// <summary>
                ///     La propiedad puede ser leída.
                /// </summary>
                Readable = 1,
                /// <summary>
                ///     La propiedad puede ser escrita.
                /// </summary>
                Writtable = 2,
                /// <summary>
                ///     La propiedad puede ser leída o escrita.
                /// </summary>
                ReadWrite = Readable | Writtable,
                /// <summary>
                ///     La propiedad incorpora notificación de cambios.
                /// </summary>
                Notifies = 4 | Writtable,
                /// <summary>
                ///     La propiedad utiliza un campo como almacenamiento
                ///     del valor actual.
                /// </summary>
                BackingFiled = 8,
            }

            /// <summary>
            ///     Obtiene una referencia al objeto que construye la propiedad.
            /// </summary>
            public readonly PropertyBuilder Builder;

            /// <summary>
            ///     Obtiene una referencia al constructor del campo de almacenamiento de la propiedad.
            /// </summary>
            public readonly FieldBuilder? BackingField;

            /// <summary>
            ///     Obtiene una referencia al generador de IL del método que 
            ///     obtiene el valor de la propiedad.
            /// </summary>
            public readonly ILGenerator? Getter;

            /// <summary>
            ///     Obtiene una referencia al generador de IL del método que 
            ///     establece el valor de la propiedad.
            /// </summary>
            public readonly ILGenerator? Setter;

            /// <summary>
            ///     Obtiene una referencia al constructor de método que obtiene
            ///     el valor de la propiedad.
            /// </summary>
            public readonly MethodBuilder? GetterBuilder;

            /// <summary>
            ///     Obtiene una referencia al constructor de método que
            ///     establece el valor de la propiedad.
            /// </summary>
            public readonly MethodBuilder? SetterBuilder;

            /// <summary>
            ///     Obtiene las banderas definidas para esta propiedad.
            /// </summary>
            public readonly PropertyFlags Flags;

            /// <summary>
            ///     Inicializa una nueva instancia de la clase 
            ///     <see cref="PropertyBuilderInfo"/>.
            /// </summary>
            /// <param name="builder">
            ///     Constructor de propiedad.
            /// </param>
            /// <param name="backingField">
            ///     Constructor que representa al campo de almacenamiento.
            /// </param>
            private PropertyBuilderInfo(PropertyBuilder builder, FieldBuilder backingField)
            {
                Builder = builder;
                BackingField = backingField;
            }
        }

        private readonly HashSet<PropertyBuilder> _declaredProperties = new HashSet<PropertyBuilder>();
        private readonly HashSet<FieldBuilder> _declaredFields = new HashSet<FieldBuilder>();
    }
}
