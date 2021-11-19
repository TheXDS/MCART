/*
MethodBaseExtensions.cs

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

using System;
using System.Linq;
using System.Reflection;
using System.Text;
using TheXDS.MCART.Exceptions;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Contiene extensiones para la clase <see cref="MethodBase"/>.
    /// </summary>
    public static partial class MethodBaseExtensions
    {
        /// <summary>
        /// Obtiene un nombre completo para un método, incluyendo el tipo y
        /// el espacio de nombres donde el mismo ha sido definido.
        /// </summary>
        /// <param name="method">
        /// Método del cual obtener el nombre completo.
        /// </param>
        /// <returns>
        /// El nombre completo del método, incluyendo el tipo y el espacio
        /// de nombres donde el mismo ha sido definido.
        /// </returns>
        public static string FullName(this MethodBase method)
        {
            StringBuilder? s = new();
            s.Append(method.DeclaringType?.CSharpName().OrNull("{0}."));
            s.Append(method.Name);
            if (method.IsGenericMethod)
            {
                s.Append(string.Join(", ", method.GetGenericArguments().Select(Types.Extensions.TypeExtensions.CSharpName)).OrNull("<{0}>"));
            }
            s.Append($"({string.Join(", ", method.GetParameters().Select(q => q.ParameterType.CSharpName()))})");
            return s.ToString();
        }

        /// <summary>
        /// Determina si el método especificado ha sido invalidado en la
        /// instancia provista.
        /// </summary>
        /// <param name="method">
        /// Método a comprobar.
        /// </param>
        /// <param name="instance">
        /// Instancia en la cual se debe realizar la comprobación.
        /// Generalmente, este argumento debe ser <see langword="this"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si el método ha sido invalidado en la
        /// instancia especificada, <see langword="false"/> en caso 
        /// contrario.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se produce si <paramref name="method"/> o
        /// <paramref name="instance"/> son <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidTypeException">
        /// Se produce si la definición de <paramref name="method"/> no existe
        /// en el tipo de <paramref name="instance"/>.
        /// </exception>
        public static bool IsOverriden(this MethodBase method, object instance)
        {
            IsOverriden_Contract(method, instance);
            MethodInfo? m = instance.GetType().GetMethod(method.Name, GetBindingFlags(method), null, method.GetParameters().Select(p => p.ParameterType).ToArray(), null)
                ?? throw new TamperException(new MissingMethodException(instance.GetType().Name, method.Name));

            return method.DeclaringType != m.DeclaringType;
        }

        /// <summary>
        /// Infiere las <see cref="BindingFlags"/> utilizadas en la
        /// definición del método.
        /// </summary>
        /// <param name="method">
        /// Método para el cual inferir las <see cref="BindingFlags"/>.
        /// </param>
        /// <returns>
        /// Las <see cref="BindingFlags"/> inferidas basadas en las
        /// propiedades del método.
        /// </returns>
        public static BindingFlags GetBindingFlags(this MethodBase method)
        {
            BindingFlags retVal = BindingFlags.Default;

            void Test(MethodAttributes inFlag, BindingFlags orFlag, BindingFlags notFlags = BindingFlags.Default)
            {
                if (method.Attributes.HasFlag(inFlag))
                {
                    retVal |= orFlag;
                }
                else
                {
                    retVal |= notFlags;
                }
            }

            Test(MethodAttributes.Public, BindingFlags.Public);
            Test(MethodAttributes.Private, BindingFlags.NonPublic);
            Test(MethodAttributes.Family, BindingFlags.NonPublic);
            Test(MethodAttributes.Static, BindingFlags.Static, BindingFlags.Instance);

            return retVal;
        }
    }
}