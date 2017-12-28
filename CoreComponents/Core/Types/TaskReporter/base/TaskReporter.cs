//
//  TaskReporter.cs
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
        Extensions.Timer Tmr = new Extensions.Timer();
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
        /// operación, <c>false</c> en caso contrario.
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
            get => Tmr is null ? null : (TimeSpan?)TimeSpan.FromMilliseconds(Tmr.Interval);
            set
            {
                if (value.HasValue)
                {
                    if (Tmr is null) Tmr = new Extensions.Timer();
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
        /// <summary>
        /// Genera el evento <see cref="Begun"/>. 
        /// </summary>
        public abstract void Begin();
        /// <summary>
        /// Genera el evento <see cref="Begun"/> para una tarea que no puede
        /// detenerse.
        /// </summary>
        public abstract void BeginNonStop();
        /// <summary>
        /// Genera el evento <see cref="Ended"/> 
        /// </summary>
        public abstract void End();
        /// <summary>
        /// Genera el evento <see cref="Error"/> 
        /// </summary>
        /// <param name="ex">
        /// Parámetro opcional. <see cref="Exception"/> que ha causado la 
        /// finalización de la tarea.
        /// </param>
        public abstract void EndWithError(Exception ex = null);
        /// <summary>
        /// Genera el evento <see cref="Reporting"/> desde una tarea. 
        /// </summary>
        /// <param name="e">Argumentos del evento.</param>
        public abstract void Report(ProgressEventArgs e);
        /// <summary>
        /// Genera el evento <see cref="Reporting"/> desde una tarea. 
        /// </summary>
        /// <param name="helpText">
        /// Texto de ayuda sobre el progreso de la tarea.
        /// </param>
        public abstract void Report(string helpText);
        /// <summary>
        /// Genera el evento <see cref="Reporting"/> desde una tarea. 
        /// </summary>
        /// <param name="progress">Porcentaje de progreso de la tarea.</param>
        /// <param name="helpText">
        /// Texto de ayuda sobre el progreso de la tarea.
        /// </param>
        public abstract void Report(float? progress = default(float?), string helpText = null);
        /// <summary>
        /// Genera el evento <see cref="Stopped"/> al interrumpir una tarea.
        /// </summary>
        /// <param name="e">Argumentos del evento.</param>
        public abstract void Stop(ProgressEventArgs e);
        /// <summary>
        /// Genera el evento <see cref="Stopped"/> al interrumpir una tarea.
        /// </summary>
        /// <param name="progress">Porcentaje de progreso de la tarea.</param>
        /// <param name="helpText">
        /// Texto de ayuda sobre el progreso de la tarea.
        /// </param>
        public abstract void Stop(float? progress = default(float?), string helpText = null);
        /// <summary>
        /// Genera el evento <see cref="Stopped"/> al interrumpir una tarea.
        /// </summary>
        /// <param name="helpText">
        /// Texto de ayuda sobre el progreso de la tarea.
        /// </param>
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
        /// <summary>
        /// Indica que una tarea se ha iniciado. Genera el evento 
        /// <see cref="Begun"/>.
        /// </summary>
        public void Begin(TimeSpan timeout, bool genTOutEx = false)
        {
            BeginSub(timeout, genTOutEx);
            Begin();
        }
        /// <summary>
        /// Indica que una tarea que no se puede detener ha iniciado. Genera el
        /// evento <see cref="Begun"/>.
        /// </summary>
        public void BeginNonStop(TimeSpan timeout, bool genTOutEx = false)
        {
            BeginSub(timeout, genTOutEx);
            BeginNonStop();
        }
        /// <summary>
        /// Ejecuta un ciclo determinado por el delegado
        /// <paramref name="forAct"/>.
        /// </summary>
        /// <param name="cEnd">Valor final del contador.</param>
        /// <param name="forAct">Acción a ejecutar.</param>
        /// <param name="message">
        /// Parámetro opcional. Mensaje a mostrar.
        /// </param>
        /// <param name="nonStop">
        /// Parámetro opcional. Si es <c>true</c>, el ciclo no podrá ser
        /// interrumpido. De forma predeterminada, se asume <c>false</c>.
        /// </param>
        /// <param name="onCancel">
        /// Parámetro opcional. Acción a ejecutar en caso de cancelar el ciclo.
        /// </param>
        /// <param name="onError">
        /// Parámetro opcional. Acción a ejecutar en caso de generarse un error
        /// durante la ejecución del ciclo.
        /// </param>
        /// <remarks>
        /// De forma predeterminada, el ciclo iniciará el contador en 0, y
        /// realizará incrementos de 1 por cada paso.
        /// </remarks>
        public async Task For(int cEnd, ForAction forAct, string message = null, bool nonStop = false, Action onCancel = null, Action onError = null)
        {
            await For(0, cEnd, 1, forAct, message, nonStop, onCancel, onError, this);
        }
        /// <summary>
        /// Ejecuta un ciclo determinado por el delegado
        /// <paramref name="forAct"/>.
        /// </summary>
        /// <param name="cStart">Valor inicial del contador.</param>
        /// <param name="cEnd">Valor final del contador.</param>
        /// <param name="forAct">Acción a ejecutar.</param>
        /// <param name="message">
        /// Parámetro opcional. Mensaje a mostrar.
        /// </param>
        /// <param name="nonStop">
        /// Parámetro opcional. Si es <c>true</c>, el ciclo no podrá ser
        /// interrumpido. De forma predeterminada, se asume <c>false</c>.
        /// </param>
        /// <param name="onCancel">
        /// Parámetro opcional. Acción a ejecutar en caso de cancelar el ciclo.
        /// </param>
        /// <param name="onError">
        /// Parámetro opcional. Acción a ejecutar en caso de generarse un error
        /// durante la ejecución del ciclo.
        /// </param>
        /// <remarks>
        /// De forma predeterminada, el ciclo realizará incrementos de 1 por 
        /// cada paso.
        /// </remarks>
        public async Task For(int cStart, int cEnd, ForAction forAct, string message = null, bool nonStop = false, Action onCancel = null, Action onError = null)
        {
            await For(cStart, cEnd, 1, forAct, message, nonStop, onCancel, onError, this);
        }
        /// <summary>
        /// Ejecuta un ciclo determinado por el delegado 
        /// <paramref name="forAct"/>.
        /// </summary>
        /// <param name="cStart">Valor inicial del contador.</param>
        /// <param name="cEnd">Valor final del contador.</param>
        /// <param name="cStep">Incrmento del contador por cada paso.</param>
        /// <param name="forAct">Acción a ejecutar.</param>
        /// <param name="message">
        /// Parámetro opcional. Mensaje a mostrar.
        /// </param>
        /// <param name="nonStop">
        /// Parámetro opcional. Si es <c>true</c>, el ciclo no podrá ser
        /// interrumpido. De forma predeterminada, se asume <c>false</c>.
        /// </param>
        /// <param name="onCancel">
        /// Parámetro opcional. Acción a ejecutar en caso de cancelar el ciclo.
        /// </param>
        /// <param name="onError">
        /// Parámetro opcional. Acción a ejecutar en caso de generarse un error
        /// durante la ejecución del ciclo.
        /// </param>
        public async Task For(int cStart, int cEnd, int cStep, ForAction forAct, string message = null, bool nonStop = false, Action onCancel = null, Action onError = null)
        {
            await For(cStart, cEnd, cStep, forAct, message, nonStop, onCancel, onError, this);
        }
        /// <summary>
        /// Ejecuta un ciclo <c>For Each</c> determinado por el delegado
        /// <paramref name="forEachAct"/>.
        /// </summary>
        /// <typeparam name="T">Tipo de la colección del ciclo.</typeparam>
        /// <param name="collection">Colección del ciclo.</param>
        /// <param name="forEachAct">Acción a ejecutar.</param>
        /// <param name="nonStop">
        /// Parámetro opcional. Si es <c>true</c>, el ciclo no podrá ser
        /// interrumpido. De forma predeterminada, se asume <c>false</c>.
        /// </param>
        /// <param name="onCancel">
        /// Parámetro opcional. Acción a ejecutar en caso de cancelar el ciclo.
        /// </param>
        /// <param name="onError">
        /// Parámetro opcional. Acción a ejecutar en caso de generarse un error
        /// durante la ejecución del ciclo.
        /// </param>
        /// <param name="message">
        /// Parámetro opcional. Mensaje a mostrar.
        /// </param>
        public async Task ForEach<T>(IEnumerable<T> collection, ForEachAction<T> forEachAct, string message = null, bool nonStop = false, Action onCancel = null, Action onError = null)
        {
            await ForEach<T>(collection, forEachAct, message, nonStop, onCancel, onError, this);
        }
        /// <summary>
        /// Reinicia el contador de tiempo de espera durante una tarea.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Se produce cuando no se está ejecutando una tarea (el valor de
        /// <see cref="OnDuty"/> es <c>false</c>).</exception>
        public void ResetTimeout() { Tmr.Reset(); }
        #endregion
        #region Métodos estáticos públicos
        /// <summary>
        /// Ejecuta un ciclo determinado por el delegado 
        /// <paramref name="forAct"/>.
        /// </summary>
        /// <param name="cStart">Valor inicial del contador.</param>
        /// <param name="cEnd">Valor final del contador.</param>
        /// <param name="cStep">Incrmento del contador por cada paso.</param>
        /// <param name="forAct">Acción a ejecutar.</param>
        /// <param name="message">
        /// Parámetro opcional. Mensaje a mostrar.
        /// </param>
        /// <param name="nonStop">
        /// Parámetro opcional. Si es <c>true</c>, el ciclo no podrá ser
        /// interrumpido. De forma predeterminada, se asume <c>false</c>.
        /// </param>
        /// <param name="onCancel">
        /// Parámetro opcional. Acción a ejecutar en caso de cancelar el ciclo.
        /// </param>
        /// <param name="onError">
        /// Parámetro opcional. Acción a ejecutar en caso de generarse un error
        /// durante la ejecución del ciclo.
        /// </param>
        /// <param name="instance">
        /// Instancia de <see cref="ITaskReporter"/> a utilizar para reportar
        /// el estado de la tarea.
        /// </param>
        public static async Task For(
            int cStart,
            int cEnd,
            int cStep,
            ForAction forAct,
            string message,
            bool nonStop,
            Action onCancel,
            Action onError,
            ITaskReporter instance)
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
                            onCancel?.Invoke();
                            return;
                        }
                        instance.Report((j - cStart) / (cEnd - cStart), message);
                        forAct(j, instance);
                    }
                    instance.End();
                }
                catch (Exception ex)
                {
                    onError?.Invoke();
                    instance.EndWithError(ex);
                }
            });
        }
        /// <summary>
        /// Ejecuta un ciclo <c>For Each</c> determinado por el delegado
        /// <paramref name="forEachAct"/>.
        /// </summary>
        /// <typeparam name="T">Tipo de la colección del ciclo.</typeparam>
        /// <param name="collection">Colección del ciclo.</param>
        /// <param name="forEachAct">Acción a ejecutar.</param>
        /// <param name="nonStop">
        /// Parámetro opcional. Si es <c>true</c>, el ciclo no podrá ser
        /// interrumpido. De forma predeterminada, se asume <c>false</c>.
        /// </param>
        /// <param name="onCancel">
        /// Parámetro opcional. Acción a ejecutar en caso de cancelar el ciclo.
        /// </param>
        /// <param name="onError">
        /// Parámetro opcional. Acción a ejecutar en caso de generarse un error
        /// durante la ejecución del ciclo.
        /// </param>
        /// <param name="message">
        /// Parámetro opcional. Mensaje a mostrar.
        /// </param>
        /// <param name="instance">
        /// Instancia de <see cref="ITaskReporter"/> a utilizar para reportar
        /// el estado de la tarea.
        /// </param>
        public static async Task ForEach<T>(
            IEnumerable<T> collection,
            ForEachAction<T> forEachAct,
            string message,
            bool nonStop,
            Action onCancel,
            Action onError,
            ITaskReporter instance)
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
                    foreach (T j in collection)
                    {
                        if (!nonStop && instance.CancelPending)
                        {
                            instance.Stop();
                            onCancel?.Invoke();
                            return;
                        }
                        instance.Report(k / collection.Count(), message);
                        forEachAct(j, instance);
                        k++;
                    }
                    instance.End();
                }
                catch (Exception ex)
                {
                    onError?.Invoke();
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
        protected void RaiseBegun(object sender, BegunEventArgs e) => Begun?.Invoke(sender, e);
        /// <summary>
        /// Genera el evento <see cref="CancelRequested"/> desde una clase 
        /// derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        /// <param name="e">Parámetros del evento.</param>
        protected void RaiseCancelRequested(object sender, System.ComponentModel.CancelEventArgs e) => CancelRequested?.Invoke(sender, e);
        /// <summary>
        /// Genera el evento <see cref="Ended"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        protected void RaiseEnded(object sender) => Ended?.Invoke(sender, EventArgs.Empty);
        /// <summary>
        /// Genera el evento <see cref="Error"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        /// <param name="e">Parámetros del evento.</param>
        protected void RaiseError(object sender, Events.ExceptionEventArgs e) => Error?.Invoke(sender, e);
        /// <summary>
        /// Genera el evento <see cref="Reporting"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        /// <param name="e">Parámetros del evento</param>
        protected void RaiseReporting(object sender, ProgressEventArgs e) => Reporting?.Invoke(sender, e);
        /// <summary>
        /// Genera el evento <see cref="Stopped"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        /// <param name="e">Parámetros del evento.</param>
        protected void RaiseStopped(object sender, ProgressEventArgs e) => Stopped?.Invoke(sender, e);
        /// <summary>
        /// Genera el evento <see cref="TaskTimeout"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        /// <param name="e">Parámetros del evento.</param>
        protected void RaiseTimeout(object sender, ProgressEventArgs e) => TaskTimeout?.Invoke(sender, e);
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="TaskReporter"/>.
        /// </summary>
        protected TaskReporter() { Tmr.Elapsed += TmrElapsed; }
        #endregion
    }
}