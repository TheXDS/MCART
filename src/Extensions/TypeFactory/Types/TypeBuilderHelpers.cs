﻿/*
TypeBuilderHelpers.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using System.Reflection;
using TheXDS.MCART.Types.Extensions;
using static System.Reflection.MethodAttributes;

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Contiene funciones auxiliares para la construcción de tipos en Runtime.
    /// </summary>
    public static class TypeBuilderHelpers
    {
        internal static string UndName(string name)
        {
            if (name.IsEmpty()) throw new ArgumentNullException(name);
            return name.Length > 1
                ? $"_{name.Substring(0, 1).ToLower()}{name.Substring(1)}"
                : $"_{name.ToLower()}";
        }

        internal static string NoIfaceName(string name)
        {
            if (name.IsEmpty()) throw new ArgumentNullException(name);
            return name[0] != 'I' ? $"{name}Implementation" : name.Substring(1);
        }

        /// <summary>
        /// Obtiene un atributo de método a partir del valor de acceso
        /// especificado.
        /// </summary>
        /// <returns>
        /// Un atributo de método con el nivel de acceso especificado.
        /// </returns>
        /// <param name="access">Nivel de acceso deseado para el método.</param>
        public static MethodAttributes Access(MemberAccess access)
        {
            return access switch
            {
                MemberAccess.Private => Private,
                MemberAccess.Protected => Family,
                MemberAccess.Internal => MethodAttributes.Assembly,
                MemberAccess.Public => Public,
                _ => throw new NotImplementedException(),
            };
        }

        /// <summary>
        /// Infiere los atributos de tipo a utilizar de acuerdo al valor de
        ///  banderas de acceso a miembro.
        /// </summary>
        /// <param name="access">
        /// Banderas que indican el nivel ve acceso del tipo.
        /// </param>
        /// <returns>
        /// Un valor de atributos de tipo que incluye información de acceso.
        /// </returns>
        public static TypeAttributes TypeAccess(MemberAccess access)
        {
            return access switch
            {
                MemberAccess.Private => TypeAttributes.NestedPrivate,                                
                MemberAccess.Internal => TypeAttributes.NestedAssembly,
                MemberAccess.Public => TypeAttributes.Public,
                _ => throw new NotImplementedException(),
            };
        }

        /// <summary>
        /// Infiere el nivel de acceso para un tipo.
        /// </summary>
        /// <param name="type">
        /// Tipo a partir del cual inferir el nivel de acceso.
        /// </param>
        /// <returns>
        /// Banderas que indican el nivel ve acceso del tipo.
        /// </returns>
        public static MemberAccess InferAccess(Type type)
        {
            return type.IsPublic ? MemberAccess.Public : MemberAccess.Internal;            
        }

        /// <summary>
        /// Infiere todos los atributos del tipo especificado.
        /// </summary>
        /// <param name="type">
        /// Tipo para el cual inferir los atributos.
        /// </param>
        /// <returns>
        /// Un valor con banderas que indican los atributos del tipo.
        /// </returns>
        public static TypeAttributes InferAttributes(Type type)
        {
            var retVal = default(TypeAttributes);
            retVal |= type.IsClass ? TypeAttributes.Class : 0;
            if (type.IsNested)
            {
                retVal |= InferAccess(type) switch
                {
                    MemberAccess.Private => TypeAttributes.NestedPrivate,
                    MemberAccess.Internal => TypeAttributes.NestedAssembly,
                    MemberAccess.Public => TypeAttributes.NestedPublic,
                    _ => throw new NotImplementedException()
                };
            }
            else
            {
                retVal |= InferAccess(type) switch
                {
                    MemberAccess.Internal => TypeAttributes.NotPublic,
                    MemberAccess.Public => TypeAttributes.Public,
                    _ => throw new NotImplementedException()
                };
            }

            return retVal;
        }
    }
}