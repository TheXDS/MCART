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

using System;
using System.ComponentModel;
using TheXDS.MCART.Events;

namespace TheXDS.MCART.Types.TaskReporter
{
    /// <summary>
    /// Define una serie de métodos a implementar por una clase que permita a
    /// una tarea reportar el progreso de una operación, generalmente cíclica.
    /// </summary>
    public partial interface ITaskReporter
    {
        /// <summary>
        /// Indica a la tarea que se ha solicitado que se detenga
        /// </summary>
        event EventHandler<CancelEventArgs> CancelRequested;
        /// <summary>
        /// Se produce cuando una tarea se ha iniciado
        /// </summary>
        event EventHandler<BegunEventArgs> Begun;
        /// <summary>
        /// Se produce cuando la tarea desea reportar su estado
        /// </summary>
        event EventHandler<ProgressEventArgs> Reporting;
        /// <summary>
        /// Se produce cuando una tarea finalizó correctamente
        /// </summary>
        event EventHandler Ended;
        /// <summary>
        /// Se produce cuando una tarea es cancelada
        /// </summary>
        event EventHandler<ProgressEventArgs> Stopped;
        /// <summary>
        /// Se produce cuando una tarea indica que finalizó con error
        /// </summary>
        event EventHandler<ExceptionEventArgs> Error;
        /// <summary>
        /// Se produce cuando se ha agotado el tiempo de espera para una tarea
        /// </summary>
        event EventHandler<ProgressEventArgs> TaskTimeout;
    }
}