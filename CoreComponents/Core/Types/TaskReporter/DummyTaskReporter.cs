//
//  DummyTaskReporter.cs
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
    /// <see cref="TaskReporter"/> que no implementa ningún medio de 
    /// interacción con el usuario.
    /// </summary>
    public class DummyTaskReporter : TaskReporter
    {
        /// <summary>
        /// Esta propiedad siempre devolverá <c>false</c>.
        /// </summary>
        public override bool CancelPending => false;
        /// <summary>
        /// Indica que una tarea que no se puede detener ha iniciado.
        /// </summary>
        public override void Begin()=>BeginNonStop();
        /// <summary>
        /// Indica que una tarea que no se puede detener ha iniciado.
        /// </summary>
        public override void BeginNonStop()
        {
            if (OnDuty) throw new InvalidOperationException();
            SetTimeStart(DateTime.Now);
            SetOnDuty(true);
            RaiseBegun(this, new BegunEventArgs(false, TStart));
        }
        /// <summary>
        /// Marca el final de una tarea.
        /// </summary>
        public override void End()
        {
            if (!OnDuty) throw new InvalidOperationException();
            SetOnDuty(false);
            SetTimeStart(null);
            RaiseEnded(this);
        }
        /// <summary>
        /// Indica que la tarea finalizó con un error.
        /// </summary>
        /// <param name="ex">
        /// <see cref="Exception"/> que causó la finalización de esta tarea.
        /// </param>
        public override void EndWithError(Exception ex = null)
        {
            if (!OnDuty) throw new InvalidOperationException();
            SetOnDuty(false);
            SetTimeStart(null);
            RaiseError(this, new Events.ExceptionEventArgs(ex));
        }
        /// <summary>
        /// Reporta el estado de la tarea actual.
        /// </summary>
        /// <param name="e">Información adicional del evento.</param>
        public override void Report(ProgressEventArgs e)
        {
            if (!OnDuty) throw new InvalidOperationException();
            RaiseReporting(this, e);
        }
        /// <summary>
        /// Reporta el estado de la tarea actual.
        /// </summary>
        /// <param name="helpText">Texto de ayuda sobre la tarea.</param>
        public override void Report(string helpText)
        {
            if (!OnDuty) throw new InvalidOperationException();
            RaiseReporting(this, new ProgressEventArgs(null, helpText));
        }
        /// <summary>
        /// Reporta el estado de la tarea actual.
        /// </summary>
        /// <param name="progress">
        /// <see cref="Nullable{T}"/> que representa el progreso actual de la
        /// tarea.
        /// </param>
        /// <param name="helpText">Texto de ayuda sobre la tarea.</param>
        public override void Report(float? progress = default(float?), string helpText = null)
        {
            if (!OnDuty) throw new InvalidOperationException();
            RaiseReporting(this, new ProgressEventArgs(progress, helpText));
        }
        /// <summary>
        /// Indica que la tarea actual ha sido detenida antes de finalizar.
        /// </summary>
        /// <param name="e">
        /// <see cref="ProgressEventArgs"/> con información del progreso de la
        /// tarea al momento de la detención.
        /// </param>
        public override void Stop(ProgressEventArgs e)
        {
            if (!OnDuty) throw new InvalidOperationException();
            SetOnDuty(false);
            SetTimeStart(null);
            RaiseStopped(this, e);
        }
        /// <summary>
        /// Indica que la tarea actual ha sido detenida antes de finalizar.
        /// </summary>
        /// <param name="progress">
        /// <see cref="Nullable{T}"/> que representa el progreso actual de la
        /// tarea.
        /// </param>
        /// <param name="helpText">Texto de ayuda sobre la tarea.</param>
        public override void Stop(float? progress = default(float?), string helpText = null)
        {
            if (!OnDuty) throw new InvalidOperationException();
            SetOnDuty(false);
            SetTimeStart(null);
            RaiseStopped(this, new ProgressEventArgs(progress, helpText));
        }
        /// <summary>
        /// Indica que la tarea actual ha sido detenida antes de finalizar.
        /// </summary>
        /// <param name="helpText">Texto de ayuda sobre la tarea.</param>
        public override void Stop(string helpText)
        {
            if (!OnDuty) throw new InvalidOperationException();
            SetOnDuty(false);
            SetTimeStart(null);
            RaiseStopped(this, new ProgressEventArgs(null, helpText));
        }
    }
}