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

using System;
using System.ComponentModel;
using Gtk;

namespace MCART.Types.TaskReporter
{
    /// <summary>
    /// Clase base para los controles de Gtk que pueden utilizarse para
    /// mostrar el progreso de una tarea por medio de la interfaz 
    /// <see cref="ITaskReporter"/>.
    /// </summary>
    [ToolboxItem(true)]
    public abstract partial class TaskReporterControl : Widget
    {
        #region Campos privados
        bool CancelPendingProperty;
        bool OnDutyProperty;
        bool? StoppableProperty;
        DateTime TStartProperty;
        bool TimedOutProperty;
        float? CurrentProgressProperty;
        #endregion
        #region Propiedades
        /// <summary>
        /// Indica si hay pendiente una solicitud para cancelar la tarea.
        /// </summary>
        public bool CancelPending => CancelPendingProperty;
        /// <summary>
        /// Indica el progreso actual de una tarea.
        /// </summary>
        public float? CurrentProgress => CurrentProgressProperty;
        /// <summary>
        /// Indica si se está ejecutando una tarea actualmente.
        /// </summary>
        public bool OnDuty => OnDutyProperty;
        /// <summary>
        /// Indica si la tarea puede ser detenida.
        /// </summary>
        /// <returns>
        /// <c>true</c> si la tarea puede ser detenida, <c>false</c> en caso
        /// contrario.
        /// </returns>
        public bool? Stoppable => StoppableProperty;
        /// <summary>
        /// Indica si ya se ha agotado el tiempo de espera de la tarea.
        /// </summary>
        /// <returns>
        /// <c>true</c> si ya se ha agotado el tiempo de espera, <c>false</c>
        /// en caso contrario.
        /// </returns>
        public bool TimedOut => TimedOutProperty;
        /// <summary>
        /// Obtiene el momento de inicio de la tarea.
        /// </summary>
        public DateTime TStart => TStartProperty;
        #endregion
    }
}