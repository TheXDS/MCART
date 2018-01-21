//
//  EventArgs.cs
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

using System;

namespace TheXDS.MCART.Types.TaskReporter
{
    /// <summary>
    /// Contiene información del evento <see cref="TaskReporter.Reporting"/>.
    /// </summary>
    public class ProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Porcentaje de progreso actual de la tarea.
        /// </summary>
        public readonly float? Progress;
        /// <summary>
        /// Texto descriptivo del estado actual de la tarea.
        /// </summary>
        public readonly string HelpText;
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="ProgressEventArgs"/>.
        /// </summary>
        /// <param name="progress">
        /// Porecentaje de progreso actual de la tarea.
        /// </param>
        /// <param name="helpText">
        /// Texto descriptivo del estado actual de la tarea.
        /// </param>
        public ProgressEventArgs(
            float? progress = null,
            string helpText = null)
        {
            Progress = progress;
            HelpText = helpText;
        }
    }
    /// <summary>
    /// Contiene información del evento <see cref="TaskReporter.Reporting"/>.
    /// </summary>
    /// <typeparam name="T">Tipo de objeto relacionado a la tarea.</typeparam>
    public class ProgressEventArgs<T> : ProgressEventArgs
    {
        /// <summary>
        /// Objeto para el cual se está reportando el progreso de la tarea.
        /// </summary>
        public readonly T Obj;
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="ProgressEventArgs{T}"/>.
        /// </summary>
        /// <param name="progress">
        /// Porcentaje de progreso actual de la tarea.
        /// </param>
        /// <param name="helpText">
        /// Texto descriptivo del estado actual de la tarea.
        /// </param>
        /// <param name="obj">
        /// Objeto relacionado al estado actual de la tarea.
        /// </param>
        public ProgressEventArgs(
            float? progress = null,
            string helpText = null,
            T obj = default) : base(progress, helpText)
        {
            Obj = obj;
        }
    }
    /// <summary>
    /// Contiene información del evento <see cref="TaskReporter.Begun"/>.
    /// </summary>
    public class BegunEventArgs : EventArgs
    {
        /// <summary>
        /// Indica si la tarea podrá ser detenida.
        /// </summary>
        public readonly bool IsStoppable;
        /// <summary>
        /// Indica el momento de inicio de la tarea.
        /// </summary>
        public readonly DateTime StartTime;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="BegunEventArgs"/>.
        /// </summary>
        /// <param name="stoppable">
        /// Indica si la tarea podrá ser detenida.
        /// </param>
        /// <param name="startTime">
        /// Indica el momento de inicio de la tarea.
        /// </param>
        public BegunEventArgs(bool stoppable, DateTime startTime)
        {
            IsStoppable = stoppable;
            StartTime = startTime;
        }
    }
}