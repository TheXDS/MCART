/*
RtInfo.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Component;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;

[assembly: McartComponent(TheXDS.MCART.Resources.RtInfo.ComponentKind.Core)]

namespace TheXDS.MCART.Resources;

/// <summary>
/// Contiene métodos con funciones de identificación en información del
/// ensamblado de MCART.
/// </summary>
public class RtInfo : AssemblyInfo
{
    /// <summary>
    /// Enumera los tipos de componentes existentes para MCART.
    /// </summary>
    public enum ComponentKind
    {
        /// <summary>
        /// Ensamblado Core principal.
        /// </summary>
        Core,

        /// <summary>
        /// Librería auxiliar de plataforma.
        /// </summary>
        PlatformLibrary,

        /// <summary>
        /// Ensamblado objetivo de plataforma.
        /// </summary>
        PlatformTarget,

        /// <summary>
        /// Extensión opcional genérica.
        /// </summary>
        Extension,

        /// <summary>
        /// Ensamblado especial de instrumentación.
        /// </summary>
        Instrumentation,

        /// <summary>
        /// Herramienta de desarrollo.
        /// </summary>
        Tool
    }

    /// <summary>
    /// Comprueba si el objeto es compatible con esta versión de MCART
    /// comparando los números de versión establecidos en
    /// <see cref="MinMcartVersionAttribute"/> y
    /// <see cref="TargetMCARTVersionAttribute"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto a comprobar.</typeparam>
    /// <param name="obj">Objeto a comprobar.</param>
    /// <returns>
    /// <see langword="true"/> si el objeto es compatible con MCART,
    /// <see langword="false"/> si el objeto no es compatible, y
    /// <see langword="null"/> si no es posible verificar la
    /// compatibilidad.
    /// </returns>
    public static bool? RtSupport<T>(T obj) where T : notnull
    {
        return !obj.HasAttr(out TargetMCARTVersionAttribute? tt)
            ? null
            : !obj.HasAttr(out MinMcartVersionAttribute? mt) ? CoreRtVersion == tt?.Value : CoreRtVersion.IsBetween(mt!.Value, tt!.Value);
    }

    /// <summary>
    /// Comprueba si el ensamblado es compatible con esta versión de <see cref="MCART" />.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si el ensamblado es compatible con esta
    /// versión de <see cref="MCART" />, <see langword="false" /> si no lo
    /// es, y <see langword="null" /> si no se ha podido determinar la
    /// compatibilidad.
    /// </returns>
    /// <param name="asmbly">Ensamblado a comprobar.</param>
    public static bool? RtSupport(Assembly asmbly)
    {
        return RtSupport<Assembly>(asmbly);
    }

    /// <summary>
    /// Comprueba si el <see cref="Type" /> es compatible con esta versión de <see cref="MCART" />.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> si el <see cref="Type" /> es compatible con esta
    /// versión de <see cref="MCART" />, <see langword="false" /> si no lo
    /// es, y <see langword="null" /> si no se ha podido determinar la
    /// compatibilidad.
    /// </returns>
    /// <param name="type"><see cref="Type" /> a comprobar.</param>
    public static bool? RtSupport(Type type)
    {
        /* BUG: Problema al implementar RTSupport(Type)
         * Esta función debe reimplementarse completa debido a un
         * problema de boxing al intentar llamar a RTSupport<T>(T), ya que
         * .Net Framework podría pasar un objeto de tipo interno, 
         * System.Reflection.RuntimeType, el cual se encaja como Object al
         * intentar llamar a la función mencionada, causando que se llame a
         * la función HasAttr<T>(object, T) en lugar de HasAttr(Type, T),
         * lo cual no es la implementación intencionada.
         */
        return !type.HasAttr(out TargetMCARTVersionAttribute? tt)
            ? null
            : !type.HasAttr(out MinMcartVersionAttribute? mt) ? CoreRtVersion == tt?.Value : CoreRtVersion.IsBetween(mt!.Value, tt!.Value);
    }

    private static ComponentKind GetKind(Assembly mcartAssembly)
    {
        if (!mcartAssembly.HasAttrValue<McartComponentAttribute, ComponentKind>(out ComponentKind kind))
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
    /// Comprueba si el ensamblado especificado es MCART o uno de sus 
    /// componentes.
    /// </summary>
    /// <param name="assembly">
    /// Ensamblado a comprobar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si el ensamblado es MCART o uno de sus
    /// componentes, <see langword="false"/> en caso contrario.
    /// </returns>
    public static bool IsMcart(Assembly assembly)
    {
        return assembly.HasAttr<McartComponentAttribute>();
    }

    /// <summary>
    /// Obtiene la referencia del ensamblado principal de MCART
    /// </summary>
    /// <returns>
    /// El ensamblado principal de MCART.
    /// </returns>
    public static Assembly CoreRtAssembly => typeof(RtInfo).Assembly;

    /// <summary>
    /// Obtiene la versión del ensamblado de <see cref="MCART" />.
    /// </summary>
    /// <returns>
    /// Un <see cref="Version" /> con la información de versión de
    /// <see cref="MCART" />.
    /// </returns>
    public static Version CoreRtVersion => CoreRtAssembly.GetName().Version!;

    /// <summary>
    /// Obtiene el tipo de componente de MCART que este ensamblado es.
    /// </summary>
    public ComponentKind Kind { get; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="RtInfo" />.
    /// </summary>
    public RtInfo() : this(CoreRtAssembly)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="RtInfo"/>.
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
