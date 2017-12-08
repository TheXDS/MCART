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
using System.Windows.Forms;
using System.Drawing;

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
        /// <param name="Text">Nombre del comando.</param>
        /// <param name="Description">
        /// Descripción larga del comando. útil para aplicar a Tooltips.
        /// </param>
        /// <param name="Action">
        /// <see cref="EventHandler"/> que se utilizará para controlar la
        /// activación de este <see cref="InteractionItem"/>.
        /// </param>
        /// <param name="Icon">
        /// <see cref="Image"/> con el ícono para este 
        /// <see cref="InteractionItem"/>.
        /// </param>
        public InteractionItem(EventHandler Action, string Text, string Description, Image Icon)
        {
            this.Action = Action ?? throw new ArgumentNullException(nameof(Action));
            this.Text = Text ?? Action.Method.Name;
            this.Description = Description;
            this.Icon = Icon;
        }
        /// <summary>
        /// Crea una nueva entrada de interacción con el delegado 
        /// <see cref="EventHandler"/> especificado.
        /// </summary>
        /// <param name="Text">Nombre del comando.</param>
        /// <param name="Action">
        /// <see cref="EventHandler"/> que se utilizará para controlar la 
        /// activación de este <see cref="InteractionItem"/>.
        /// </param>
        /// <param name="Icon">
        /// <see cref="Image"/> con el ícono para este 
        /// <see cref="InteractionItem"/>.
        /// </param>
        public InteractionItem(EventHandler Action, string Text, Image Icon)
        {
            this.Action = Action ?? throw new ArgumentNullException(nameof(Action));
            this.Text = Text ?? Action.Method.Name;
            this.Icon = Icon;
        }
        /// <summary>
        /// Crea una nueva entrada de interacción con el delegado 
        /// <see cref="EventHandler"/> especificado.
        /// </summary>
        /// <param name="Action">
        /// <see cref="EventHandler"/> que se utilizará para controlar la
        /// activación de este <see cref="InteractionItem"/>.
        /// </param>
        /// <param name="Icon">
        /// <see cref="Image"/> con el ícono para este
        /// <see cref="InteractionItem"/>.
        /// </param>
        public InteractionItem(EventHandler Action, Image Icon)
        {
            this.Action = Action ?? throw new ArgumentNullException(nameof(Action));
            Text = Action.Method.Name;
            this.Icon = Icon;
        }
        /// <summary>
        /// Devuelve este <see cref="InteractionItem"/> como un
        /// <see cref="Button"/>.
        /// </summary>
        /// <returns>Un <see cref="Button"/> generado a partir de este
        /// <see cref="InteractionItem"/>.</returns>
        public Button AsButton() => AsButton<Button>();
        /// <summary>
        /// Devuelve este <see cref="InteractionItem"/> como un 
        /// <see cref="ButtonBase"/> de tipo <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Tipo de control a generar.</typeparam>
        /// <returns>Un <typeparamref name="T"/> generado a partir de este
        /// <see cref="InteractionItem"/>.</returns>
        public T AsButton<T>() where T : ButtonBase, new() => new T() { Image = Icon, Text = Text };   
        /// <summary>
        /// Devuelve este <see cref="InteractionItem"/> como un
        /// <see cref="MenuItem"/>.
        /// </summary>
        /// <returns>Un <see cref="MenuItem"/> generado a partir de este
        /// <see cref="InteractionItem"/>.</returns>
        public MenuItem AsMenuItem()=> new MenuItem(Text, Action);
        /// <summary>
        /// Devuelve este <see cref="InteractionItem"/> como un
        /// <see cref="ToolStripMenuItem"/>.
        /// </summary>
        /// <returns>
        /// Un <see cref="ToolStripMenuItem"/> generado a partir de este 
        /// <see cref="InteractionItem"/>.
        /// </returns>
        public ToolStripMenuItem AsToolStripMenuItem() => new ToolStripMenuItem(Text, Icon, Action) { ToolTipText = Description };            
        /// <summary>
        /// Convierte implícitamente este <see cref="InteractionItem"/> en un
        /// <see cref="Button"/>.
        /// </summary>
        /// <param name="j"><see cref="InteractionItem"/> a convertir.</param>
        public static implicit operator Button(InteractionItem j) => j.AsButton<Button>();
        /// <summary>
        /// Convierte implícitamente este <see cref="InteractionItem"/> en un
        /// <see cref="MenuItem"/>.
        /// </summary>
        /// <param name="j"><see cref="InteractionItem"/> a convertir.</param>
        public static implicit operator MenuItem(InteractionItem j) => j.AsMenuItem();
        /// <summary>
        /// Convierte implícitamente este <see cref="InteractionItem"/> en un
        /// <see cref="ToolStripMenuItem"/>.
        /// </summary>
        /// <param name="j"><see cref="InteractionItem"/> a convertir.</param>
        public static implicit operator ToolStripMenuItem(InteractionItem j)=>j.AsToolStripMenuItem();
    }
}