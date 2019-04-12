/*
TypeBuilderExtensions.cs

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

using System;
using System.Reflection;
using System.Reflection.Emit;
using static System.Reflection.Emit.OpCodes;
using static System.Reflection.MethodAttributes;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    ///     Contiene extensiones útiles para la generación de miembros por medio
    ///     de la clase <see cref="TypeBuilder"/>.
    /// </summary>
    public static class TypeBuilderExtensions
    {
        /// <summary>
        ///     Determina si el método es reemplazable.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> si el método es reemplazable
        ///     <see langword="false"/> en caso contrario, o
        ///     <see langword="null"/> si el método no existe en la clase base
        ///     del constructor de tipos.
        /// </returns>
        /// <param name="tb">
        ///     Constructor de tipo sobre el cual ejecutar la consulta.
        /// </param>
        /// <param name="method">Nombre del método a buscar.</param>
        /// <param name="args">Tipo de argumentos del método a buscar.</param>
        public static bool? Overridable(this TypeBuilder tb, string method, params Type[] args)
        {
            var bm = tb.BaseType?.GetMethod(method, args);
            if (bm is null) return null;
            return bm.IsVirtual || bm.IsAbstract;
        }

        /// <summary>
        ///     Agrega una propiedad automática al tipo.
        /// </summary>
        /// <param name="tb">
        ///     Constructor del tipo en el cual crear la nueva propiedad
        ///     automática.
        /// </param>
        /// <param name="name">Nombre de la nueva propiedad.</param>
        /// <param name="type">Tipo de la nueva propiedad.</param>
        /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
        /// <param name="writtable">
        ///     Si se establece en <see langword="true"/>, la propiedad podrá
        ///     ser escrita, es decir, tendrá un 'setter'.
        /// </param>
        /// <param name="virtual">
        ///     Si se establece en <see langword="true"/>, la propiedad será
        ///     definida como virtual, por lo que podrá ser reemplazada en una
        ///     clase derivada. 
        /// </param>
        /// <param name="prop">
        ///     Parámetro de salida. Referencia al constructor de propiedad
        ///     generado.
        /// </param>
        /// <param name="field">
        ///     parámetro de salida. Referencia al campo de almacenamiento de la
        ///     propiedad.
        /// </param>
        private static void AddAutoProp(this TypeBuilder tb, string name, Type type, MemberAccess access, bool writtable, bool @virtual, out PropertyBuilder prop, out FieldBuilder field)
        {
            var flags = Access(access) | SpecialName | HideBySig;
            if (tb.Overridable($"get_{name}") ?? @virtual) flags |= Virtual;

            field = tb.DefineField(Helpers.UndName(name), type, FieldAttributes.Private | (writtable ? 0 : FieldAttributes.InitOnly));
            prop = tb.DefineProperty(name, PropertyAttributes.HasDefault, type, null);
            var getM = tb.DefineMethod($"get_{name}", flags, type, null);
            var getIl = getM.GetILGenerator();
            getIl.Emit(Ldarg_0);
            getIl.Emit(Ldfld, field);
            getIl.Emit(Ret);
            prop.SetGetMethod(getM);

            if (writtable)
            {
                var setM = tb.DefineMethod($"set_{name}", flags, null, new[] { type });
                var setIl = setM.GetILGenerator();
                setIl.Emit(Ldarg_0);
                setIl.Emit(Ldarg_1);
                setIl.Emit(Stfld, field);
                setIl.Emit(Ret);
                prop.SetSetMethod(setM);
            }
        }

        private static void AddAutoProp(this TypeBuilder tb, string name, Type type, MemberAccess access, bool writtable, out PropertyBuilder prop, out FieldBuilder field)
        {
            AddAutoProp(tb, name, type, access, writtable, false, out prop, out field);
        }

        public static FieldBuilder AddAutoProp(this TypeBuilder tb, string name, Type type, MemberAccess access, bool writtable)
        {
            AddAutoProp(tb, name, type, access, writtable, out _, out var field);
            return field;
        }

        public static PropertyBuilder AddAutoProp(this TypeBuilder tb, string name, Type type, MemberAccess access)
        {
            AddAutoProp(tb, name, type, access, true, out var prop, out _);
            return prop;

        }

        /// <summary>
        ///     Obtiene un atributo de método a partir del valor de acceso
        ///     especificado.
        /// </summary>
        /// <returns>
        ///     Un atributo de método con el nivel de acceso especificado.
        /// </returns>
        /// <param name="access">Nivel de acceso deseado para el método.</param>
        private static MethodAttributes Access(MemberAccess access)
        {
            switch (access)
            {
                case MemberAccess.Private:
                    return MethodAttributes.Private;
                case MemberAccess.Protected:
                    return MethodAttributes.Family;
                case MemberAccess.Internal:
                    return MethodAttributes.Assembly;
                case MemberAccess.Public:
                    return MethodAttributes.Public;
            }
#pragma warning disable RECS0083 // Intencional.
            throw new NotImplementedException();
#pragma warning restore RECS0083
        }
    }
}