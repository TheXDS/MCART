//
//  Events.cs
//
//  This file is part of Morgan's CLR Advanced Runtime (MCART)
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.ComponentModel;
using System.Windows;
namespace TheXDS.MCART.Events
{
    /// <summary>
    /// Contiene información para los eventos cancelables que reporten el
    /// cambio de una propiedad de dependencia.
    /// </summary>
    public class DependencyPropertyChangingEventArgs : CancelEventArgs
    {
        /// <summary>
        /// Propiedad de dependencia que cambiará su valor.
        /// </summary>
        public readonly DependencyProperty Property;
        /// <summary>
        /// Valor original de la propiedad de dependencia.
        /// </summary>
        public readonly object OldValue;
        /// <summary>
        /// Nuevo valor de la propiedad de dependencia.
        /// </summary>
        public readonly object NewValue;
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="DependencyPropertyChangingEventArgs"/>.
        /// </summary>
        /// <param name="property">
        /// Propiedad de dependencia que cambiará su valor.
        /// </param>
        /// <param name="oldValue">
        /// Valor original de la propiedad de dependencia.
        /// </param>
        /// <param name="newValue">
        /// Nuevo valor de la propiedad de dependencia.
        /// </param>
        public DependencyPropertyChangingEventArgs(DependencyProperty property, object oldValue, object newValue)
        {
            Property = property;
            OldValue = oldValue;
            NewValue = newValue;
        }
        /// <summary>
        /// Convierte implícitamente este
        /// <see cref="DependencyPropertyChangingEventArgs"/> en un
        /// <see cref="DependencyPropertyChangedEventArgs"/>.
        /// </summary>
        /// <param name="x">Objeto a convertir.</param>
        /// <returns>
        /// Un <see cref="DependencyPropertyChangedEventArgs"/> con la misma
        /// información de evento del
        /// <see cref="DependencyPropertyChangingEventArgs"/> especificado.
        /// </returns>
        public static implicit operator DependencyPropertyChangedEventArgs(DependencyPropertyChangingEventArgs x) => new DependencyPropertyChangedEventArgs(x.Property, x.OldValue, x.NewValue);
    }
}