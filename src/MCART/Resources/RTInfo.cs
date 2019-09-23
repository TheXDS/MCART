/*
RtInfo.cs

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
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Component;

[assembly: McartComponent(TheXDS.MCART.Resources.RtInfo.ComponentKind.Core)]

namespace TheXDS.MCART.Resources
{
    /// <inheritdoc />
    /// <summary>
    ///     Contiene métodos con funciones de identificación en información del
    ///     ensamblado de MCART.
    /// </summary>
    public class RtInfo : AssemblyInfo
    {
        /// <summary>
        ///     Enumera los tipos de componentes existentes para MCART.
        /// </summary>
        public enum ComponentKind
        {
            /// <summary>
            ///     Ensamblado Core principal.
            /// </summary>
            Core,

            /// <summary>
            ///     Librería auxiliar de plataforma.
            /// </summary>
            PlatformLibrary,

            /// <summary>
            ///     Ensamblado objetivo de plataforma.
            /// </summary>
            PlatformTarget,

            /// <summary>
            ///     Extensión opcional genérica.
            /// </summary>
            Extension,

            /// <summary>
            ///     Ensamblado especial de instrumentación.
            /// </summary>
            Instrumentation,

            /// <summary>
            ///     Herramienta de desarrollo.
            /// </summary>
            Tool
        }
        
        /// <summary>
        ///     Comprueba si el objeto es compatible con esta versión de MCART
        ///     comparando los números de versión establecidos en
        ///     <see cref="MinMcartVersionAttribute"/> y
        ///     <see cref="TargetMCARTVersionAttribute"/>.
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
        /// <param name="obj">Objeto a comprobar.</param>
        /// <returns>
        ///     <see langword="true"/> si el objeto es compatible con MCART,
        ///     <see langword="false"/> si el objeto no es compatible, y
        ///     <see langword="null"/> si no es posible verificar la
        ///     compatibilidad.
        /// </returns>
        public static bool? RtSupport<T>(T obj)
        {
            TargetMCARTVersionAttribute? tt = null;
            if (!obj?.HasAttr(out tt) ?? true) return null;
            if (!obj!.HasAttr(out MinMcartVersionAttribute? mt))
#if StrictMCARTVersioning
                return null;
#else
                return CoreRtVersion == tt?.Value;
#endif
            return CoreRtVersion.IsBetween(mt!.Value, tt!.Value);
        }

        /// <summary>
        ///     Comprueba si el ensamblado es compatible con esta versión de <see cref="MCART" />.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si el ensamblado es compatible con esta
        ///     versión de <see cref="MCART" />, <see langword="false" /> si no lo
        ///     es, y <see langword="null" /> si no se ha podido determinar la
        ///     compatibilidad.
        /// </returns>
        /// <param name="asmbly">Ensamblado a comprobar.</param>
        public static bool? RtSupport(Assembly asmbly)
        {
            return RtSupport<Assembly>(asmbly);
        }

        /// <summary>
        ///     Comprueba si el <see cref="Type" /> es compatible con esta versión de <see cref="MCART" />.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> si el <see cref="Type" /> es compatible con esta
        ///     versión de <see cref="MCART" />, <see langword="false" /> si no lo
        ///     es, y <see langword="null" /> si no se ha podido determinar la
        ///     compatibilidad.
        /// </returns>
        /// <param name="type"><see cref="Type" /> a comprobar.</param>
        public static bool? RtSupport(Type type)
        {
            /* HACK: Problema al implementar RTSupport(Type)
             * Esta función debe reimplementarse completa debido a un
             * problema de boxing al intentar llamar a RTSupport<T>(T), ya que
             * .Net Framework podría pasar un objeto de tipo interno, 
             * System.Reflection.RuntimeType, el cual se encaja como Object al
             * intentar llamar a la función mencionada, causando que se llame a
             * la función HasAttr<T>(object, T) en lugar de HasAttr(Type, T),
             * lo cual no es la implementación intencionada.
             */
            if (!type.HasAttr(out TargetMCARTVersionAttribute? tt)) return null;
            if (!type.HasAttr(out MinMcartVersionAttribute? mt))
#if StrictMCARTVersioning
                return null;
#else
                return CoreRtVersion == tt?.Value;
#endif
            return CoreRtVersion.IsBetween(mt!.Value, tt!.Value);
        }

        private static ComponentKind GetKind(Assembly mcartAssembly)
        {
            if (!mcartAssembly.HasAttrValue<McartComponentAttribute, ComponentKind>(out var kind))
                throw new InvalidOperationException();
            return kind;
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Name} {Version}";
        }

        /// <summary>
        ///     Comprueba si el ensamblado especificado es MCART o uno de sus 
        ///     componentes.
        /// </summary>
        /// <param name="assembly">
        ///     Ensamblado a comprobar.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> si el ensamblado es MCART o uno de sus
        ///     componentes, <see langword="false"/> en caso contrario.
        /// </returns>
        public bool IsMcart(Assembly assembly)
        {
            return assembly.HasAttr<McartComponentAttribute>();
        }

        /// <summary>
        ///     Obtiene la referencia del ensamblado principal de MCART
        /// </summary>
        /// <returns>
        ///     El ensamblado principal de MCART.
        /// </returns>
        public static Assembly CoreRtAssembly => typeof(RtInfo).Assembly;

        /// <summary>
        ///     Obtiene la versión del ensamblado de <see cref="MCART" />.
        /// </summary>
        /// <returns>
        ///     Un <see cref="Version" /> con la información de versión de
        ///     <see cref="MCART" />.
        /// </returns>
        public static Version CoreRtVersion => CoreRtAssembly.GetName().Version!;

        /// <summary>
        ///     Obtiene el tipo de componente de MCART que este ensamblado es.
        /// </summary>
        public ComponentKind Kind { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="RtInfo" />.
        /// </summary>
        public RtInfo() : this(CoreRtAssembly)
        {
        }

        /// <summary>
        ///     Inicializa una nueva instancia de la clase <see cref="RtInfo"/>.
        /// </summary>
        /// <param name="asm">Ensamblado de MCART a relacionar.</param>
        protected RtInfo(Assembly asm) : this(asm, GetKind(asm))
        {
        }

        private RtInfo(Assembly asm, ComponentKind kind) : base(asm)
        {
            Kind = kind;
        }
    }
}