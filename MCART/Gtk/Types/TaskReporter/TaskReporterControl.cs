//
//  TaskReporterControl.cs
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

namespace MCART.Types.TaskReporter
{
    /// <summary>
    /// Clase base para los controles de Gtk que pueden utilizarse para
    /// mostrar el progreso de una tarea por medio de la interfaz 
    /// <see cref="ITaskReporter"/>.
    /// </summary>
    [System.ComponentModel.ToolboxItem(true)]
    public abstract partial class TaskReporterControl : Gtk.Box
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TaskReporterControl"/>.
        /// </summary>
        protected TaskReporterControl() : base(Gtk.Orientation.Horizontal, 1) { }
        /// <summary>
        /// Encapsula y simula al método Invoke que se encuentra disponible en
        /// controles de Win32 con la finalidad de compartir código.
        /// </summary>
        /// <param name="method">
        /// Método a ejecutar en el hilo que controla a Gtk.
        /// </param>
        /// <param name="args">Argumentos del método.</param>
        void Invoke(System.Delegate method, params object[] args)
        {
            Gtk.Application.Invoke(delegate { method.DynamicInvoke(args); });
        }
    }
}