//
//  TaskReporterPlugin.cs
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
    /// Implementa la interfaz <see cref="ITaskReporter"/> para crear plugins
    /// compatibles con MCART que permitan reportar el progreso de una
    /// operación o tarea.
    /// </summary>
    public abstract class TaskReporterPlugin : PluginSupport.Plugin, ITaskReporter
    {
        #region Campos privados
        DateTime? ts;
        bool cp, od, ge;
        float? curp;
        Extensions.Timer Tmr;
        #endregion
        #region Propiedades públicas
        /// <summary>
        /// Obtiene un valor que indica si hay pendiente una solicitud para 
        /// cancelar la tarea en ejecución.
        /// </summary>
        public bool CancelPending => cp;
        /// <summary>
        /// Obtiene un valor que indica si se ha agotado el tiempo de espera
        /// para ejecutar la tarea.
        /// </summary>
        public bool TimedOut => ts.HasValue ? Tmr?.TimeLeft.Value.TotalMilliseconds <= 0 : false;
        /// <summary>
        /// Obtiene un valor que indica si actualmente hay una tarea en
        /// ejecución supervisada por este <see cref="TaskReporterPlugin"/>.
        /// </summary>
        public bool OnDuty => od;
        /// <summary>
        /// Obtiene el instante en el cual se inició la tarea actualmente en
        /// ejecución.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Se produce si actualmenteno hay una tarea en ejecución.
        /// </exception>
        public DateTime TStart => ts ?? throw new InvalidOperationException();
        /// <summary>
        /// Obtiene la cantidad de tiempo restante antes de que se agote el
        /// tiempo de espera.
        /// </summary>
        public TimeSpan? TimeLeft => Tmr?.TimeLeft;
        /// <summary>
        /// Obtiene o establece el tiempo de espera de este 
        /// <see cref="TaskReporterPlugin"/>.
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
        /// Obtiene un valor que indica el progreso actual de la tarea
        /// actualmente en ejecución.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Se produce si actualmenteno hay una tarea en ejecución.
        /// </exception>
        public float? CurrentProgress => OnDuty ? curp : throw new InvalidOperationException();
        #endregion
        #region Eventos
        /// <summary>
        /// Se genera cuando este <see cref="TaskReporter"/> ha solicifado la
        /// detención de la tarea en ejecucion.
        /// </summary>
        public event CancelRequestedEventHandler CancelRequested;
        /// <summary>
        /// Se genera cuando se ha iniciado una tarea.
        /// </summary>
        public event BegunEventHandler Begun;
        /// <summary>
        /// Se genera cuando una tarea desea reportar su estado.
        /// </summary>
        public event ReportingEventHandler Reporting;
        /// <summary>
        /// Se genera cuando una tarea ha finalizado.
        /// </summary>
        public event EndedEventHandler Ended;
        /// <summary>
        /// Se genera cuando una tarea ha sido cancelada.
        /// </summary>
        public event StoppedEventHandler Stopped;
        /// <summary>
        /// Se genera cuando ocurre una excepción durante la ejecución de la
        /// tarea.
        /// </summary>
        public event ErrorEventHandler Error;
        /// <summary>
        /// Se genera cuando se ha agotado el tiempo de espera establecido para
        /// ejecutar la tarea.
        /// </summary>
        public event TaskTimeoutEventHandler TaskTimeout;
        #endregion
        #region Métodos invalidables
        public abstract void Begin();
        public abstract void BeginNonStop();
        public abstract void End();
        public abstract void EndWithError(Exception ex = null);
        public abstract void Report(ProgressEventArgs e);
        public abstract void Report(string helpText);
        public abstract void Report(float? progress = default(float?), string helptext = null);
        public abstract void Stop(ProgressEventArgs e);
        public abstract void Stop(float? Progress = default(float?), string helpText = null);
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
        /// Establece el valor de la propiedad de sólo lectura <see cref="CancelPending"/> desde una clase derivada
        /// </summary>
        /// <param name="Value">Valor que se desea establecer en <see cref="CancelPending"/></param>
        protected void SetCancelPending(bool Value) { cp = Value; }
        protected void SetOnDuty(bool Value) { od = Value; }
        protected void SetTimeStart(DateTime? t) { ts = t; }
        protected void SetCurrentProgress(float? Value) { curp = Value; }
        protected void RaiseBegun(object sender, BegunEventArgs e)
        {
            Begun(sender, e);
        }
        /// <summary>
        /// Genera el evento <see cref="CancelRequested"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">Instancia del objeto que generará el evento</param>
        /// <param name="e">Parámetros del evento</param>
        protected void RaiseCancelRequested(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CancelRequested(sender, e);
        }
        /// <summary>
        /// Genera el evento <see cref="Ended"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">Instancia del objeto que generará el evento</param>
        protected void RaiseEnded(object sender)
        {
            Ended(sender, EventArgs.Empty);
        }
        /// <summary>
        /// Genera el evento <see cref="Error"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">Instancia del objeto que generará el evento</param>
        /// <param name="e">Parámetros del evento</param>
        protected void RaiseError(object sender, Events.ExceptionEventArgs e)
        {
            Error(sender, e);
        }
        /// <summary>
        /// Genera el evento <see cref="Reporting"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">Instancia del objeto que generará el evento</param>
        /// <param name="e">Parámetros del evento</param>
        protected void RaiseReporting(object sender, ProgressEventArgs e)
        {
            Reporting(sender, e);
        }
        /// <summary>
        /// Genera el evento <see cref="Stopped"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">Instancia del objeto que generará el evento</param>
        /// <param name="e">Parámetros del evento</param>
        protected void RaiseStopped(object sender, ProgressEventArgs e)
        {
            Stopped(sender, e);
        }
        protected void RaiseTimeout(object sender, ProgressEventArgs e)
        {
            TaskTimeout(sender, e);
        }
        protected TaskReporterPlugin() { Tmr.Elapsed += TmrElapsed; }

        #endregion
    }
}