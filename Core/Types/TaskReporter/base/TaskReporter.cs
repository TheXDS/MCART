﻿//
//  TaskReporter.cs
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
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace MCART.Types.TaskReporter
{
    /// <summary>
    /// Clase base para objetos que puedan utilizarse para reportar el progreso
    /// de una operación o tarea.
    /// </summary>
    public abstract class TaskReporter : ITaskReporter
    {
        #region Campos privados
        DateTime? ts;
        bool cp, od, ge;
        float? curp;
        Extensions.Timer Tmr;
        #endregion
        #region Propiedades públicas
        /// <summary>
        /// Obtiene un valor que indica si existe una solicitud para deterner la
        /// tarea actualmente en ejecución.
        /// </summary>
        /// <value>
        /// <c>true</c> si existe una solicitud de cancelación pendiente; de lo
        /// contrario, <c>false</c>.
        /// </value>
        public virtual bool CancelPending => cp;
        /// <summary>
        /// Obtiene un valor que indica si la operación debe detenerse debido a
        /// que se ha agotado el tiempo de espera de la misma.
        /// </summary>
        /// <value>
        /// <c>true</c> si se ha agotado el tiempo de espera para finalizar la
        /// operación; de lo contrario, <c>false</c>.
        /// </value>
        public bool TimedOut
        {
            get
            {
                if (ts.HasValue) return Tmr?.TimeLeft.Value.TotalMilliseconds <= 0;
                return false;
            }
        }
        /// <summary>
        /// Obtiene un valor que indica si se está ejecutando una tarea 
        /// actualmente.
        /// </summary>
        /// <value>
        /// <c>true</c> si hay una tarea en ejecución; de lo contrario, 
        /// <c>false</c>.
        /// </value>
        public virtual bool OnDuty => od;
        /// <summary>
        /// Obtiene el instante en el que dio inicio la tarea en ejecución.
        /// </summary>
        public DateTime TStart => ts ?? throw new InvalidOperationException();
        /// <summary>
        /// Obtiene la cantidad de tiempo disponible para finalizar la tarea.
        /// </summary>
        /// <value>El tiempo disponible para finalizar la tarea, o <c>null</c>
        /// en caso que no exista una restricción de tiempo.</value>
        public TimeSpan? TimeLeft => Tmr?.TimeLeft;
        /// <summary>
        /// Obtiene la cantidad de tiempo asignado para ejecutar la tarea.
        /// </summary>
        public TimeSpan? Timeout
        {
            get => Tmr.IsNull() ? null : (TimeSpan?)TimeSpan.FromMilliseconds(Tmr.Interval);
            set
            {
                if (value.HasValue)
                {
                    if (Tmr.IsNull()) Tmr = new Extensions.Timer();
                    Tmr.Interval = value.Value.TotalMilliseconds;
                }
                else { Tmr = null; }
            }
        }
        /// <summary>
        /// Obtiene un valor que indica el progreso actual reportardo por la
        /// tarea.
        /// </summary>
        /// <value>Un <see cref="float"/> que indica el progreso de la tarea, o
        /// <c>null</c> en caso que el progreso sea indeterminado.</value>
        public float? CurrentProgress => OnDuty ? curp : throw new InvalidOperationException();
        #endregion
        #region Eventos
        public event CancelRequestedEventHandler CancelRequested;
        public event BegunEventHandler Begun;
        public event ReportingEventHandler Reporting;
        public event EndedEventHandler Ended;
        public event StoppedEventHandler Stopped;
        public event ErrorEventHandler Error;
        public event TaskTimeoutEventHandler TaskTimeout;
        #endregion
        #region Métodos invalidables
        public abstract void Begin();
        public abstract void BeginNonStop();
        public abstract void End();
        public abstract void EndWithError(Exception ex = null);
        public abstract void Report(ProgressEventArgs e);
        public abstract void Report(string helpText);
        public abstract void Report(float? progress = default(float?), string helpText = null);
        public abstract void Stop(ProgressEventArgs e);
        public abstract void Stop(float? progress = default(float?), string helpText = null);
        public abstract void Stop(string helpText);
        #endregion
        #region Métodos privados
        void BeginSub(TimeSpan timeout, bool genTOutEx)
        {
            if (OnDuty) throw new InvalidOperationException();
            ge = genTOutEx;
            Tmr = new Extensions.Timer(timeout.TotalMilliseconds)
            {
                AutoReset = false,
                Enabled = true
            };
        }
        void TmrElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Tmr.Stop();
            if (OnDuty)
            {
                RaiseTimeout(this, null);
                if (ge) throw new TimeoutException();
            }
        }
        #endregion
        #region Métodos públicos
        public void Begin(TimeSpan timeout, bool genTOutEx = false)
        {
            BeginSub(timeout, genTOutEx);
            Begin();
        }
        public void BeginNonStop(TimeSpan timeout, bool genTOutEx = false)
        {
            BeginSub(timeout, genTOutEx);
            BeginNonStop();
        }
        public async Task For(int cEnd, ForAction forAct, string message = null, bool nonStop = false, Action onCancel = null, Action onError = null)
        {
            await For(0, cEnd, 1, forAct, message, nonStop, this, onCancel, onError);
        }
        public async Task For(int cStart, int cEnd, ForAction forAct, string message = null, bool nonStop = false, Action onCancel = null, Action onError = null)
        {
            await For(cStart, cEnd, 1, forAct, message, nonStop, this, onCancel, onError);
        }
        public async Task For(int cStart, int cEnd, int cStep, ForAction forAct, string message = null, bool nonStop = false, Action onCancel = null, Action onError = null)
        {
            await For(cStart, cEnd, cStep, forAct, message, nonStop, this, onCancel, onError);
        }
        public async Task ForEach<T>(IEnumerable<T> coll, ForEachAction<T> fEachAct, string message = null, bool nonStop = false, Action onCancel = null, Action onError = null)
        {
            await ForEach<T>(coll, fEachAct, message, nonStop, this, onCancel, onError);
        }
        public void ResetTimeout() { Tmr.Reset(); }
        #endregion
        #region Métodos estáticos públicos
        public static async Task For(
            int cStart,
            int cEnd,
            int cStep,
            ForAction forAct,
            string message,
            bool nonStop,
            ITaskReporter instance,
            Action onCancel,
            Action onError)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (nonStop) instance.BeginNonStop();
                    else instance.Begin();
                }
                catch { throw; }
                try
                {
                    for (int j = cStart; j <= cEnd; j += cStep)
                    {
                        if (!nonStop && instance.CancelPending)
                        {
                            instance.Stop();
                            if (!onCancel.IsNull()) onCancel.Invoke();
                            return;
                        }
                        instance.Report((j - cStart) / (cEnd - cStart), message);
                        forAct(j, instance);
                    }
                    instance.End();
                }
                catch (Exception ex)
                {
                    if (!onError.IsNull()) onError.Invoke();
                    instance.EndWithError(ex);
                }
            });
        }
        public static async Task ForEach<T>(
            IEnumerable<T> coll,
            ForEachAction<T> fEachAct,
            string message,
            bool nonStop,
            ITaskReporter instance,
            Action onCancel,
            Action onError)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (nonStop) instance.BeginNonStop();
                    else instance.Begin();
                }
                catch { throw; }
                try
                {
                    int k = 0;
                    foreach (T j in coll)
                    {
                        if (!nonStop && instance.CancelPending)
                        {
                            instance.Stop();
                            if (!onCancel.IsNull()) onCancel.Invoke();
                            return;
                        }
                        instance.Report(k / coll.Count(), message);
                        fEachAct(j, instance);
                        k++;
                    }
                    instance.End();
                }
                catch (Exception ex)
                {
                    if (!onError.IsNull()) onError.Invoke();
                    instance.EndWithError(ex);
                }
            });
        }
        #endregion
        #region Métodos para clases derivadas
        /// <summary>
        /// Establece el valor de la propiedad de sólo lectura
        /// <see cref="CancelPending"/> desde una clase derivada.
        /// </summary>
        /// <param name="Value">
        /// Valor a establecer en la propiedad <see cref="CancelPending"/>.
        /// </param>
        protected void SetCancelPending(bool Value) { cp = Value; }
        /// <summary>
        /// Establece el valor de la propiedad de sólo lectura
        /// <see cref="OnDuty"/> desde una clase derivada.
        /// </summary>
        /// <param name="Value">
        /// Valor a establecer en la propiedad <see cref="OnDuty"/>.
        /// </param>
        protected void SetOnDuty(bool Value) { od = Value; }
        /// <summary>
        /// Establece el valor de la propiedad de sólo lectura
        /// <see cref="CurrentProgress"/> desde una clase derivada.
        /// </summary>
        /// <param name="Value">
        /// Valor a establecer en la propiedad <see cref="CurrentProgress"/>.
        /// </param>
        protected void SetCurrentProgress(float? Value) { curp = Value; }
        /// <summary>
        /// Establece el valor de la propiedad de sólo lectura
        /// <see cref="TStart"/> desde una clase derivada.
        /// </summary>
        /// <param name="Value">
        /// Valor a establecer en la propiedad <see cref="TStart"/>.
        /// </param>
        protected void SetTimeStart(DateTime? Value) { ts = Value; }
        /// <summary>
        /// Genera el evento <see cref="Begun"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        /// <param name="e">Parámetros del evento.</param>
        protected void RaiseBegun(object sender, BegunEventArgs e) => Begun(sender, e);
        /// <summary>
        /// Genera el evento <see cref="CancelRequested"/> desde una clase 
        /// derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        /// <param name="e">Parámetros del evento.</param>
        protected void RaiseCancelRequested(object sender, System.ComponentModel.CancelEventArgs e) => CancelRequested(sender, e);
        /// <summary>
        /// Genera el evento <see cref="Ended"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        protected void RaiseEnded(object sender) => Ended(sender, EventArgs.Empty);
        /// <summary>
        /// Genera el evento <see cref="Error"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        /// <param name="e">Parámetros del evento.</param>
        protected void RaiseError(object sender, Events.ExceptionEventArgs e) => Error(sender, e);
        /// <summary>
        /// Genera el evento <see cref="Reporting"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        /// <param name="e">Parámetros del evento</param>
        protected void RaiseReporting(object sender, ProgressEventArgs e) => Reporting(sender, e);
        /// <summary>
        /// Genera el evento <see cref="Stopped"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        /// <param name="e">Parámetros del evento.</param>
        protected void RaiseStopped(object sender, ProgressEventArgs e) => Stopped(sender, e);
        /// <summary>
        /// Genera el evento <see cref="TaskTimeout"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        /// <param name="e">Parámetros del evento.</param>
        protected void RaiseTimeout(object sender, ProgressEventArgs e) => TaskTimeout(sender, e);
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="TaskReporter"/>.
        /// </summary>
        protected TaskReporter() { Tmr.Elapsed += TmrElapsed; }
        #endregion
    }
}