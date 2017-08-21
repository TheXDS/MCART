//
//  InteractionItem.cs
//
//  This file is part of MCART
//
//  Author:
//       César Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Morgan
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
using Gtk;

namespace MCART.PluginSupport
{
    /// <summary>
    /// Esta clase define a un elemento de interacción.
    /// </summary>
    public partial class InteractionItem
    {
        /// <summary>
        /// Obtiene un ícono asociado a este <see cref="InteractionItem"/>.
        /// </summary>
        /// <returns>
        /// Un <see cref="Image"/> con el ícono asociado a este objeto.
        /// Si no se ha establecido un ícono, se devuelve <c>null</c>.
        /// </returns>
        public readonly Image Icon;
        /// <summary>
        /// Crea una nueva entrada de interacción con el delegado
        /// <see cref="EventHandler"/> especificado.
        /// </summary>
        /// <param name="text">Nombre del comando.</param>
        /// <param name="description">
        /// Descripción larga del comando. útil para aplicar a Tooltips.
        /// </param>
        /// <param name="action">
        /// <see cref="EventHandler"/> que se utilizará para controlar la
        /// activación de este <see cref="InteractionItem"/>.
        /// </param>
        /// <param name="icon">
        /// <see cref="Image"/> con el ícono para este 
        /// <see cref="InteractionItem"/>.
        /// </param>
        public InteractionItem(EventHandler action, string text, string description, Image icon)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            Text = text ?? action.Method.Name;
            Description = description;
            Icon = icon;
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
        /// <param name="icon">
        /// <see cref="Image"/> con el ícono para este 
        /// <see cref="InteractionItem"/>.
        /// </param>
        public InteractionItem(EventHandler action, string text, Image icon)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            Text = text ?? action.Method.Name;
            Icon = icon;
        }
        /// <summary>
        /// Crea una nueva entrada de interacción con el delegado 
        /// <see cref="EventHandler"/> especificado.
        /// </summary>
        /// <param name="action">
        /// <see cref="EventHandler"/> que se utilizará para controlar la
        /// activación de este <see cref="InteractionItem"/>.
        /// </param>
        /// <param name="icon">
        /// <see cref="Image"/> con el ícono para este
        /// <see cref="InteractionItem"/>.
        /// </param>
        public InteractionItem(EventHandler action, Image icon)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            Text = action.Method.Name;
            Icon = icon;
        }
        /// <summary>
        /// Crea una nueva entrada de interacción con el delegado 
        /// <see cref="EventHandler"/> especificado.
        /// </summary>
        /// <param name="stockId">
        /// Identificador Stock de Gtk a asociar a este 
        /// <see cref="InteractionItem"/>.
        /// </param>
        /// <param name="action">
        /// <see cref="EventHandler"/> que se utilizará para controlar la
        /// activación de este <see cref="InteractionItem"/>.
        /// </param>
        public InteractionItem(string stockId, EventHandler action)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            if (StockManager.LookupItem(stockId, out StockItem s))
            {
                Text = s.Label;
                Icon = new Image(s.StockId, IconSize.Menu);
            }
            else Text = stockId;
        }
        /// <summary>
        /// Devuelve este <see cref="InteractionItem"/> como un
        /// <see cref="Button"/>.
        /// </summary>
        /// <returns>Un <see cref="Button"/> generado a partir de este
        /// <see cref="InteractionItem"/>.</returns>
        public Button AsButton()
        {
            Button k = new Button()
            {
                Label = Text,
                Visible = true
            };
            if (!Description.IsEmpty()) k.TooltipText = Description;
            if (!Icon.IsNull()) k.Image = Icon;
            k.Clicked += Action;
            return k;
        }
        /// <summary>
        /// Devuelve este <see cref="InteractionItem"/> como un
        /// <see cref="ImageMenuItem"/>.
        /// </summary>
        /// <returns>Un <see cref="MenuItem"/> generado a partir de este
        /// <see cref="InteractionItem"/>.</returns>
        public ImageMenuItem AsMenuItem()
        {
            ImageMenuItem k = new ImageMenuItem(Text)
            {
                Visible = true
            };
            if (!Description.IsEmpty())
                k.TooltipMarkup = Description;
            if (!Icon.IsNull()) k.Image = Icon;
            k.Activated += Action;
            return k;
        }
        /// <summary>
        /// Devuelve este <see cref="InteractionItem"/> como un
        /// <see cref="ToggleButton"/>.
        /// </summary>
        /// <returns>Un <see cref="ToggleButton"/> generado a partir de este
        /// <see cref="InteractionItem"/>.</returns>
        public ToggleButton AsToggleButton()
        {
            ToggleButton k = new ToggleButton(Text)
            {
                Visible = true
            };
            if (!Description.IsEmpty()) k.TooltipText = Description;
            if (!Icon.IsNull()) k.Image = Icon;
            k.Clicked += Action;
            return k;
        }
        /// <summary>
        /// Convierte implícitamente este <see cref="InteractionItem"/> en un
        /// <see cref="Button"/>.
        /// </summary>
        /// <param name="j"><see cref="InteractionItem"/> a convertir.</param>
        public static implicit operator Button(InteractionItem j) => j.AsButton();
        /// <summary>
        /// Convierte implícitamente este <see cref="InteractionItem"/> en un
        /// <see cref="ImageMenuItem"/>.
        /// </summary>
        /// <param name="j"><see cref="InteractionItem"/> a convertir.</param>
        public static implicit operator ImageMenuItem(InteractionItem j) => j.AsMenuItem();
        /// <summary>
        /// Convierte implícitamente este <see cref="InteractionItem"/> en un
        /// <see cref="ToggleButton"/>.
        /// </summary>
        /// <param name="j"><see cref="InteractionItem"/> a convertir.</param>
        public static implicit operator ToggleButton(InteractionItem j) => j.AsToggleButton();
    }
}