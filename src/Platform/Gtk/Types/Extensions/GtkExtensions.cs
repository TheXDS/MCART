/*
GtkExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be
useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Windows.Input;
using Gtk;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Contiene una serie de extensiones básicas para simplificar el acceso a
    /// la API de Gtk#.
    /// </summary>
    public static class GtkExtensions
    {
        /// <summary>
        /// Conecta un botón con el <see cref="ICommand"/> especificado.
        /// </summary>
        /// <param name="button">Botón a configurar.</param>
        /// <param name="command">Comando a conectar.</param>
        /// <param name="parameter">Parámetro del ICommand.</param>
        [CLSCompliant(false)]
        public static void Bind(this Button button, ICommand command, object? parameter)
        {
            button.Clicked += (sender, args) =>
            {
                if (command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
            };
            
            command.CanExecuteChanged += (sender, args) => button.Sensitive = command.CanExecute(parameter);
        }

        /// <summary>
        /// Conecta un botón con el <see cref="ICommand"/> especificado.
        /// </summary>
        /// <param name="button">Botón a configurar.</param>
        /// <param name="command">Comando a conectar.</param>
        [CLSCompliant(false)]
        public static void Bind(this Button button, ICommand command)
        {
            Bind(button,command, null);
        }

        /// <summary>
        /// Conecta un botón con el <see cref="ICommand"/> especificado.
        /// </summary>
        /// <param name="button">Botón a configurar.</param>
        /// <param name="command">Comando a conectar.</param>
        /// <param name="parameter">Parámetro del ICommand.</param>
        [CLSCompliant(false)]
        public static void Bind(this Button button, Func<ICommand?> command, object? parameter)
        {
            button.Clicked += (sender, args) =>
            {
                if (command.Invoke() is {} c && c.CanExecute(parameter))
                {
                    c.Execute(parameter);
                }
            };
        }

        /// <summary>
        /// Conecta un botón con el <see cref="ICommand"/> especificado.
        /// </summary>
        /// <param name="button">Botón a configurar.</param>
        /// <param name="command">Comando a conectar.</param>
        [CLSCompliant(false)]
        public static void Bind( this Button button, Func<ICommand?> command)
        {
            Bind(button,command, null);
        }
    }
}