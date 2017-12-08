//
//  ITaskReporter_Manual.cs
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

namespace MCART.Types.TaskReporter
{
    /// <summary>
    /// Define una serie de métodos a implementar por una clase que permita a
    /// una tarea reportar el progreso de una operación, generalmente cíclica.
    /// </summary>
    public partial interface ITaskReporter
    {
        /// <summary>
        /// Indica el progreso actual de la tarea.
        /// </summary>
        float? CurrentProgress { get; }
        /// <summary>
        /// Indica si hay una cancelación de tarea pendiente.
        /// </summary>
        bool CancelPending { get; }
        /// <summary>
        /// Indica si la tarea ha excedido el tiempo de espera concedido.
        /// </summary>
        bool TimedOut { get; }
        /// <summary>
        /// Indica si la tarea está en ejecución.
        /// </summary>
        bool OnDuty { get; }
        /// <summary>
        /// Momento de inicio de la tarea,
        /// </summary>
        DateTime TStart { get; }
        /// <summary>
        /// Indica la cantidad de tiempo restante para la tarea.
        /// </summary>
        TimeSpan? TimeLeft { get; }
        /// <summary>
        /// Indica la cantidad de tiempo concedida a la tarea.
        /// </summary>
        TimeSpan? Timeout { get; set; }
        /// <summary>
        /// Reporta el estado actual de esta tarea.
        /// </summary>
        /// <param name="e">
        /// <see cref="ProgressEventArgs"/> con la información del estado de
        /// esta tarea.
        /// </param>
        void Report(ProgressEventArgs e);
        /// <summary>
        /// Reporta el estado actual de esta tarea.
        /// </summary>
        /// <param name="helpText">
        /// Texto descriptivo del estado actual de esta tarea.
        /// </param>
        void Report(string helpText);
        /// <summary>
        /// Reporta el estado actual de esta tarea.
        /// </summary>
        /// <param name="progress">Progreso actual de esta tarea.</param>
        /// <param name="helpText">
        /// Texto descriptivo del estado actual de esta tarea.
        /// </param>
        void Report(float? progress = null, string helpText = null);
        /// <summary>
        /// Indica que una tarea ha dado inicio.
        /// </summary>
        void Begin();
        /// <summary>
        /// Indica que una tarea ha dado inicio.
        /// </summary>
        /// <param name="timeout">Tiempo concedido a la tarea.</param>
        /// <param name="genTOutEx">
        /// Indica si se generará una excepción al terminarse el tiempo de 
        /// espera.
        /// </param>
        void Begin(TimeSpan timeout, bool genTOutEx = false);
        /// <summary>
        /// Indica que una tarea que no se puede interrumpir ha dado inicio.
        /// </summary>
        void BeginNonStop();
        /// <summary>
        /// Indica que una tarea que no se puede interrumpir ha dado inicio.
        /// </summary>
        /// <param name="timeout">Tiempo concedido a la tarea.</param>
        /// <param name="genTOutEx">
        /// Indica si se generará una excepción al terminarse el tiempo de 
        /// espera.
        /// </param>
        void BeginNonStop(TimeSpan timeout, bool genTOutEx = false);
        /// <summary>
        /// Indica que una tarea ha finalizado.
        /// </summary>
        void End();
        /// <summary>
        /// Indica que esta tarea ha finalizado debido a un error.
        /// </summary>
        /// <param name="ex">
        /// <see cref="Exception"/> generada que ha causado la finalización de
        /// la tarea.
        /// </param>
        void EndWithError(Exception ex = null);
        /// <summary>
        /// Indica que esta tarea ha sido detenida antes de finalizar.
        /// </summary>
        /// <param name="e">
        /// <see cref="ProgressEventArgs"/> con la información del estado de
        /// esta tarea.
        /// </param>
        void Stop(ProgressEventArgs e);
        /// <summary>
        /// Indica que esta tarea ha sido detenida antes de finalizar.
        /// </summary>
        /// <param name="progress">Progreso actual de esta tarea.</param>
        /// <param name="helpText">
        /// Texto descriptivo del estado actual de esta tarea.
        /// </param>
        void Stop(float? progress = null, string helpText = null);
        /// <summary>
        /// Indica que esta tarea ha sido detenida antes de finalizar.
        /// </summary>
        /// <param name="helpText">
        /// Texto descriptivo del estado actual de esta tarea.
        /// </param>
        void Stop(string helpText);
        /// <summary>
        /// Reinicia el contador de tiempo de espera.
        /// </summary>
        void ResetTimeout();
    }
}