//
//  IPlugin.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace MCART.PluginSupport
{
    /// <summary>
	/// Define una interfaz básica para crear Plugins administrados por MCART
	/// </summary>
	public partial interface IPlugin
    {
        /// <summary>
        /// Se produce cuando un <see cref="IPlugin"/> solicita la actualización
        /// de su interfaz gráfica.
        /// </summary>
        event UIChangeRequestedEventHandler UIChangeRequested;
        /// <summary>
        /// Se produce cuando un <see cref="IPlugin"/> está por ser deshechado.
        /// </summary>
        event PluginFinalizingEventHandler PluginFinalizing;
        /// <summary>
        /// Se produce cuando un <see cref="IPlugin"/> ha sido desechado.
        /// </summary>
        event PluginFinalizedEventHandler PluginFinalized;
        /// <summary>
        /// Se produce cuando un <see cref="IPlugin"/> ha sido cargado.
        /// </summary>
        event PluginLoadedEventHandler PluginLoaded;
        /// <summary>
        /// Se produce cuando la carga de un <see cref="IPlugin"/> ha fallado.
        /// </summary>
        event PluginLoadFailedEventHandler PluginLoadFailed;
        /// <summary>
        /// Devuelve el nombre del <see cref="IPlugin"/>
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Devuelve la versión del <see cref="IPlugin"/>
        /// </summary>
        Version Version { get; }
        /// <summary>
        /// Devuelve una descripción del <see cref="IPlugin"/>
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Devuelve el autor del <see cref="IPlugin"/>
        /// </summary>
        string Author { get; }
        /// <summary>
        /// Devuelve el Copyright del <see cref="IPlugin"/>
        /// </summary>
        string Copyright { get; }
        /// <summary>
        /// Devuelve la licencia del <see cref="IPlugin"/>
        /// </summary>
        string License { get; }
        /// <summary>
        /// Determina la versión mínima de MCART necesaria para este <see cref="IPlugin"/>
        /// </summary>
        Version MinMCARTVersion { get; }
        /// <summary>
        /// Determina la versión mínima de MCART necesaria para este <see cref="IPlugin"/>
        /// </summary>
        /// <param name="minVersion">Versión mínima de MCART.</param>
        /// <returns>
        /// <c>True</c> si fue posible obtener información sobre la versión 
        /// mínima de MCART; de lo contrario, <c>false</c>.
        /// </returns>
        bool MinRTVersion(out Version minVersion);
        /// <summary>
        /// Determina la versión objetivo de MCART para este <see cref="IPlugin"/>
        /// </summary>
        Version TargetMCARTVersion { get; }
        /// <summary>
        /// Determina la versión objetivo de MCART necesaria para este <see cref="IPlugin"/>
        /// </summary>
        /// <param name="tgtVersion">Versión objetivo de MCART.</param>
        /// <returns>
        /// <c>True</c> si fue posible obtener información sobre la versión 
        /// objetivo de MCART; de lo contrario, <c>false</c>.
        /// </returns>
        bool TargetRTVersion(out Version tgtVersion);
        /// <summary>
        /// Devuelve <c>True</c> si el plugin es Beta
        /// </summary>
        /// <returns><c>True</c> si el plugin ha sido marcado como versión Beta; de lo contrario, <c>False</c></returns>
        bool IsBeta { get; }
        /// <summary>
        /// Determina si el plugin es inseguro
        /// </summary>
        /// <returns><c>True</c> si el plugin ha sido marcado como inseguro; de lo contrario, <c>False</c></returns>
        bool IsUnsafe { get; }
        /// <summary>
        /// Determina si el plugin es inseguro
        /// </summary>
        /// <returns><c>True</c> si el plugin ha sido marcado como inseguro; de lo contrario, <c>False</c></returns>
        bool IsUnstable { get; }
        /// <summary>
        /// Devuelve el nombre de las interfaces implementadas por este
        /// <see cref="IPlugin"/>.
        /// </summary>
        /// <returns>
        /// Un <see cref="ReadOnlyCollection{T}"/> con los nombre de las
        /// interfaces implementadas dentro de este <see cref="IPlugin"/>.
        /// </returns>
        IEnumerable<string> InterfaceNames { get; }
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
        ///<summary>
        ///Devuelve la referencia al ensamblado que contiene a este <see cref="IPlugin"/>.
        ///</summary>
        Assembly MyAssembly { get; }
        /// <summary>
        /// Devuelve una colección de opciones de interacción.
        /// </summary>
        /// <returns>
        /// Un <see cref="ReadOnlyCollection{InteractionItem}"/> con los
        /// elementos de interacción del <see cref="IPlugin"/>.
        /// </returns>
        /// <remarks>
        /// Si utiliza la implementación predeterminada de la interfaz 
        /// <see cref="IPlugin"/> incluída en MCART (<see cref="Plugin"/>),
        /// puede agregar nuevos elementos <see cref="InteractionItem"/> a la
        /// colección <see cref="Plugin.MyMenu"/>.
        /// </remarks>
        ReadOnlyCollection<InteractionItem> PluginInteractions { get; }
        /// <summary>
        /// Devuelve <c>true</c> si el <see cref="Plugin"/> contiene interacciones
        /// </summary>
        bool HasInteractions { get; }
        /// <summary>
        /// Obtiene o establece el objeto <see cref="Types.TaskReporter.ITaskReporter"/>
        /// asociado a este <see cref="IPlugin"/>
        /// </summary>
        /// <returns>
        /// El <see cref="Types.TaskReporter.ITaskReporter"/> asociado a este
        /// <see cref="IPlugin"/>. Si no se ha asociado ningún objeto, se
        /// devolverá <see cref="Types.TaskReporter.DummyTaskReporter"/>.
        /// </returns>
        Types.TaskReporter.ITaskReporter Reporter { get; set; }
        /// <summary>
        /// Contiene información adicional de este plugin
        /// </summary>
        /// <returns>Información determinada por el usuario relacionada a este plugin</returns>
        object Tag { get; set; }
        /// <summary>
        /// Devuelve la instancia de este <see cref="IPlugin"/> como un objeto de tipo <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">
		/// Interfaz a la cual se debe convertir esta instancia. El <see cref="IPlugin"/> debe implementar dicha 
		/// interfaz.
		/// </typeparam>
        /// <returns>Un objeto de tipo <typeparamref name="T"/>.</returns>
        /// <remarks>
        /// En Visual Basic, esta función puede considerarse como una variante más segura de <c>CType</c>, ya que 
		/// realiza múltiples comprobaciones de tipo antes de intentar la conversión.
        /// <list type="bullet">
        /// <item><description>
		/// Es conveniente utilizar esta función en conjunto con la propiedad <see cref="Interfaces"/>.
		/// </description></item>
        /// <item><description>
		/// Si necesita una referencia de tipo <see cref="IPlugin"/>, no haga <c>Plugin.CType(Of IPlugin)</c>, en su 
		/// lugar utilice <c>Itself</c>.</description></item>
        /// </list>
        /// </remarks>
        /// <exception cref="Exceptions.InterfaceExpectedException">
		/// Se produce si <typeparamref name="T"/> no es una interfaz.</exception>
        /// <exception cref="Exceptions.InterfaceNotImplementedException">
		/// Se produce si el <see cref="IPlugin"/> no implementa la interfaz especificada.</exception>
        T CType<T>() where T : class;
        /// <summary>
        /// Solicita a la aplicación que se actualize la interfaz de interacción del <see cref="IPlugin"/>
        /// </summary>
        void RequestUIChange();
    }
}