﻿/*
IPlugin.cs

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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TheXDS.MCART.Component;

namespace TheXDS.MCART.PluginSupport.Legacy
{
    /// <summary>
    /// Define una interfaz básica para crear Plugins administrados por MCART.
    /// </summary>
    public interface IPlugin: IExposeInfo, IExposeAssembly
    {
        /// <summary>
        /// Se produce cuando un <see cref="IPlugin"/> solicita la
        /// actualización de su interfaz gráfica.
        /// </summary>
        event EventHandler<UiChangedEventArgs> UiChanged;

        /// <summary>
        /// Se produce cuando un <see cref="IPlugin"/> está por ser desechado.
        /// </summary>
        event EventHandler<PluginFinalizingEventArgs> PluginFinalizing;

        /// <summary>
        /// Determina la versión mínima de MCART necesaria para este
        /// <see cref="IPlugin"/>.
        /// </summary>
        Version? MinMcartVersion { get; }

        /// <summary>
        /// Determina la versión objetivo de MCART para este
        /// <see cref="IPlugin"/>.
        /// </summary>
        Version? TargetMcartVersion { get; }

        /// <summary>
        /// Devuelve <see langword="true"/> si el plugin es Beta.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si el plugin ha sido marcado como versión
        /// Beta, <see langword="false" /> en caso contrario.
        /// </returns>
        bool IsBeta { get; }

        /// <summary>
        /// Determina si el plugin contiene código no administrado.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si el plugin ha sido marcado para indicar 
        /// que contiene código no administrado, <see langword="false" /> en
        /// caso contrario.
        /// </returns>
        bool IsUnmanaged { get; }

        /// <summary>
        /// Determina si el plugin es inseguro.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> si el plugin ha sido marcado como inseguro,
        /// <see langword="false" /> en caso contrario.
        /// </returns>
        bool IsUnstable { get; }

        /// <summary>
        /// Devuelve la lista de interfaces que este <see cref="IPlugin"/>
        /// implementa.
        /// </summary>
        /// <returns>Una lista de las interfaces implementadas por este
        /// <see cref="IPlugin"/>.</returns>
        /// <remarks>
        /// Para que este <see cref="IPlugin"/> sea útil de alguna forma, se
        /// debe implementar una interfaz para la cual desee desarrollar un
        /// <see cref="Plugin"/>. Además, se ignoran las interfaces
        /// <see cref="IPlugin"/> y <see cref="IDisposable"/>.
        /// </remarks>
        IEnumerable<Type> Interfaces { get; }

        /// <summary>
        /// Devuelve una colección de opciones de interacción.
        /// </summary>
        /// <returns>
        /// Un <see cref="ReadOnlyCollection{InteractionItem}"/> con los
        /// elementos de interacción del <see cref="IPlugin"/>.
        /// </returns>
        /// <remarks>
        /// Si utiliza la implementación predeterminada de la interfaz 
        /// <see cref="IPlugin"/> incluida en MCART (<see cref="Plugin"/>),
        /// puede agregar nuevos elementos <see cref="InteractionItem"/> a la
        /// colección <see cref="Plugin.InteractionItems"/>.
        /// </remarks>
        ReadOnlyCollection<InteractionItem> PluginInteractions { get; }

        /// <summary>
        /// Devuelve <see langword="true"/> si el <see cref="Plugin"/> contiene
        /// interacciones.
        /// </summary>
        bool HasInteractions { get; }

        /// <summary>
        /// Contiene información adicional de este plugin.
        /// </summary>
        /// <returns>
        /// Información determinada por el usuario relacionada a este plugin.
        /// </returns>
        object? Tag { get; set; }

        /// <summary>
        /// Determina la versión mínima de MCART necesaria para este
        /// <see cref="IPlugin"/>.
        /// </summary>
        /// <param name="minVersion">
        /// Parámetro de salida. Devuelve la versión mínima de MCART requerida
        /// por este plugin.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si fue posible obtener información sobre la
        /// versión mínima de MCART, <see langword="false"/> en caso contrario.
        /// </returns>
        bool MinRtVersion(out Version? minVersion);

        /// <summary>
        /// Determina la versión objetivo de MCART necesaria para este
        /// <see cref="IPlugin"/>.
        /// </summary>
        /// <param name="tgtVersion">
        /// Parámetro de salida. Devuelve la versión objetivo de MCART requerida
        /// por este plugin.
        /// </param>
        /// <returns>
        /// <see langword="true"/> si fue posible obtener información sobre la
        /// versión objetivo de MCART, <see langword="false"/> en caso
        /// contrario.
        /// </returns>
        bool TargetRtVersion(out Version? tgtVersion);
    }
}