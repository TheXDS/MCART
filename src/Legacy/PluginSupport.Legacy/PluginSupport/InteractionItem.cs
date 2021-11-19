/*
InteractionItem.cs

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
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.PluginSupport.Legacy
{
    /// <summary>
    /// Esta clase define a un elemento de interacción.
    /// </summary>
    public partial class InteractionItem
    {
        /// <summary>
        /// Obtiene un texto asociado a este <see cref="InteractionItem"/>.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Obtiene una descripción larga de este <see cref="InteractionItem"/>.
        /// </summary>
        /// <returns>
        /// Un <see cref="string"/> con el texto asociado a este 
        /// <see cref="InteractionItem"/>.
        /// </returns>
        /// <remarks>
        /// Esta propiedad puede ser utilizada mostrándose en un Tooltip, o un 
        /// menú descriptivo.
        /// </remarks>
        public string? Description { get; }

        /// <summary>
        /// Obtiene un delegado con la acción a realizar por este 
        /// <see cref="InteractionItem"/>.
        /// </summary>
        /// <remarks>
		/// Es posible utilizar un delegado <see cref="EventHandler"/>, una 
        /// expresión Lambda, la referencia directa a un método (C#) o un método
        /// con la palabra clave <c>AddressOf</c> (VB).
        /// Además, diferentes implementaciones de MCART podrían requerir de
        /// encapsulamiento para esta acción, como es el caso en los proyectos 
        /// de Windows Presentation Framework, para el cual se incluye una
        /// función de conversión entre <see cref="EventHandler"/> y 
        /// RoutedEventHandler.
		/// </remarks>
        public EventHandler Action { get; }

        /// <summary>
        /// Crea una nueva entrada de interacción con el delegado 
        /// <see cref="EventHandler"/> especificado.
        /// </summary>
        /// <param name="text">Nombre del comando.</param>
        /// <param name="description">
        /// Descripción larga del comando. Útil para aplicar a Tooltips.
        /// </param>
        /// <param name="action">
		/// <see cref="EventHandler"/> que se utilizará para controlar la 
        /// activación de este <see cref="InteractionItem"/>.
		/// </param>
        public InteractionItem(EventHandler action, string text, string? description)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            Text = text ?? action.Method.Name;
            Description = description;
        }

        /// <summary>
        /// Crea una nueva entrada de interacción con el delegado 
        /// <see cref="EventHandler"/> especificado.
        /// </summary>
        /// <param name="text">Nombre del comando.</param>
        /// <param name="action">
		/// <see cref="EventHandler"/> que se utilizará para controlar la
        /// activación de este <see cref="InteractionItem"/>.
		/// </param>
        public InteractionItem(EventHandler action, string text)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            Text = text ?? action.Method.Name;
            Description = action.GetAttr<DescriptionAttribute>()?.Value ?? null;
        }

        /// <summary>
        /// Crea una nueva entrada de interacción con el delegado 
        /// <see cref="EventHandler"/> especificado.
        /// </summary>
        /// <param name="action">
		/// <see cref="EventHandler"/> que se utilizará para controlar la 
        /// activación de este <see cref="InteractionItem"/>.
		/// </param>
        public InteractionItem(EventHandler action)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            Text = action.GetAttr<NameAttribute>()?.Value ?? action.Method.Name;
            Description = action.GetAttr<DescriptionAttribute>()?.Value;
        }

        /// <summary>
        /// Crea una una nueva entrada de interacción utilizando el método
        /// especificado asociado a una instancia de <see cref="IPlugin"/>.
        /// </summary>
        /// <param name="method">Método a llamar.</param>
        /// <param name="parentInstance">
        /// Instancia padre de este <see cref="InteractionItem"/>.
        /// </param>
        public InteractionItem(MethodInfo method, IPlugin parentInstance)
        {
            Action = Delegate.CreateDelegate(typeof(EventHandler), parentInstance, method, false) as EventHandler
                ?? throw new Exceptions.InvalidMethodSignatureException(method);
            Text = method.GetAttr<NameAttribute>()?.Value ?? method.Name;
            Description = method.GetAttr<DescriptionAttribute>()?.Value;
        }
    }
}