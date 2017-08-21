//
//  Events.cs
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
using MCART.Events;

namespace MCART.Types.TaskReporter
{
    /// <summary>
    /// Indica a la tarea que se ha solicitado que se detenga
    /// </summary>
    /// <param name="sender">Objeto que ha iniciado el evento</param>
    /// <param name="e">Parámetros del evento</param>
    /// <remarks>Este evento debe ser implementado por la clase que contiene a la tarea</remarks>
    public delegate void CancelRequestedEventHandler(object sender, CancelEventArgs e);
    /// <summary>
    /// Se produce cuando una tarea se ha iniciado
    /// </summary>
    /// <param name="sender">Objeto que ha iniciado el evento</param>
    /// <param name="e">Parámetros del evento</param>
    public delegate void BegunEventHandler(object sender, BegunEventArgs e);
    /// <summary>
    /// Se produce cuando la tarea desea reportar su estado
    /// </summary>
    /// <param name="sender">Objeto que ha iniciado el evento</param>
    /// <param name="e">Parámetros del evento</param>
    public delegate void ReportingEventHandler(object sender, ProgressEventArgs e);
    /// <summary>
    /// Se produce cuando se ha agotado el tiempo de espera para una tarea
    /// </summary>
    /// <param name="sender">Objeto que ha iniciado el evento</param>
    /// <param name="e">Parámetros del evento</param>
    public delegate void TaskTimeoutEventHandler(object sender, ProgressEventArgs e);
    /// <summary>
    /// Se produce cuando una tarea es cancelada
    /// </summary>
    /// <param name="sender">Objeto que ha iniciado el evento</param>
    /// <param name="e">Parámetros del evento</param>
    public delegate void StoppedEventHandler(object sender, ProgressEventArgs e);
    /// <summary>
    /// Se produce cuando una tarea finalizó correctamente
    /// </summary>
    /// <param name="sender">Objeto que ha iniciado el evento</param>
    /// <param name="e">Parámetros del evento</param>
    public delegate void EndedEventHandler(object sender, EventArgs e);
    /// <summary>
    /// Se produce cuando una tarea indica que finalizó con error
    /// </summary>
    /// <param name="sender">Objeto que ha iniciado el evento</param>
    /// <param name="e">Parámetros del evento</param>
    public delegate void ErrorEventHandler(object sender, ExceptionEventArgs e);
    /// <summary>
    /// Define una serie de métodos a implementar por una clase que permita a
    /// una tarea reportar el progreso de una operación, generalmente cíclica.
    /// </summary>
    public partial interface ITaskReporter
    {
        /// <summary>
        /// Indica a la tarea que se ha solicitado que se detenga
        /// </summary>
        event CancelRequestedEventHandler CancelRequested;
        /// <summary>
        /// Se produce cuando una tarea se ha iniciado
        /// </summary>
        event BegunEventHandler Begun;
        /// <summary>
        /// Se produce cuando la tarea desea reportar su estado
        /// </summary>
        event ReportingEventHandler Reporting;
        /// <summary>
        /// Se produce cuando una tarea finalizó correctamente
        /// </summary>
        event EndedEventHandler Ended;
        /// <summary>
        /// Se produce cuando una tarea es cancelada
        /// </summary>
        event StoppedEventHandler Stopped;
        /// <summary>
        /// Se produce cuando una tarea indica que finalizó con error
        /// </summary>
        event ErrorEventHandler Error;
        /// <summary>
        /// Se produce cuando se ha agotado el tiempo de espera para una tarea
        /// </summary>
        event TaskTimeoutEventHandler TaskTimeout;
    }
}