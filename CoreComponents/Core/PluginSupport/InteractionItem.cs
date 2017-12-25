//
//  InteractionItem.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
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
using System.Reflection;
using MCART.Attributes;

namespace MCART.PluginSupport
{
    /// <summary>
    /// Esta clase define a un elemento de interacción.
    /// </summary>
    public partial class InteractionItem
    {
        /// <summary>
        /// Obtiene un texto asociado a este <see cref="InteractionItem"/>.
        /// </summary>
        public readonly string Text;
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
        public readonly string Description;
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
        public readonly EventHandler Action;
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
        public InteractionItem(EventHandler action, string text, string description)
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

        internal InteractionItem(MethodInfo method, IPlugin parentInstance)
        {
            Action = (Delegate.CreateDelegate(typeof(EventHandler), parentInstance, method) as EventHandler) 
                ?? throw new Exceptions.InvalidMethodSignatureException(method);
            Text = method.GetAttr<NameAttribute>()?.Value ?? method.Name;
            Description = method.GetAttr<DescriptionAttribute>()?.Value;
        }
    }
}