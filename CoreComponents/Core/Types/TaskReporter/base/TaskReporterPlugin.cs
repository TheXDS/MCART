//
//  TaskReporterPlugin.cs
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

using TheXDS.MCART.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace TheXDS.MCART.Types.TaskReporter
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
        public event EventHandler<CancelEventArgs> CancelRequested;
        /// <summary>
        /// Se genera cuando se ha iniciado una tarea.
        /// </summary>
        public event EventHandler<BegunEventArgs> Begun;
        /// <summary>
        /// Se genera cuando una tarea desea reportar su estado.
        /// </summary>
        public event EventHandler<ProgressEventArgs> Reporting;
        /// <summary>
        /// Se genera cuando una tarea ha finalizado.
        /// </summary>
        public event EventHandler Ended;
        /// <summary>
        /// Se genera cuando una tarea ha sido cancelada.
        /// </summary>
        public event EventHandler<ProgressEventArgs> Stopped;
        /// <summary>
        /// Se genera cuando ocurre una excepción durante la ejecución de la
        /// tarea.
        /// </summary>
        public event EventHandler<ExceptionEventArgs> Error;
        /// <summary>
        /// Se genera cuando se ha agotado el tiempo de espera establecido para
        /// ejecutar la tarea.
        /// </summary>
        public event EventHandler<ProgressEventArgs> TaskTimeout;
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
            await TaskReporter.For(cStart, cEnd, cStep, forAct, message, nonStop, onCancel, onError, instance);
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
            await TaskReporter.ForEach<T>(collection, forEachAct, message, nonStop, onCancel, onError, instance);
        }
        #endregion
        #region Métodos para clases derivadas
        /// <summary>
        /// Establece el valor de la propiedad de sólo lectura 
        /// <see cref="CancelPending"/> desde una clase derivada.
        /// </summary>
        /// <param name="Value">
        /// Valor que se desea establecer en <see cref="CancelPending"/>.
        /// </param>
        protected void SetCancelPending(bool Value) { cp = Value; }
        /// <summary>
        /// Establece el valor de la propiedad de sólo lectura 
        /// <see cref="OnDuty"/> desde una clase derivada.
        /// </summary>
        /// <param name="Value">
        /// Valor que se desea establecer en <see cref="OnDuty"/>.
        /// </param>
        protected void SetOnDuty(bool Value) { od = Value; }
        /// <summary>
        /// Establece el valor de la propiedad de sólo lectura 
        /// <see cref="TStart"/> desde una clase derivada.
        /// </summary>
        /// <param name="t">
        /// Valor que se desea establecer en <see cref="TStart"/>.
        /// </param>
        protected void SetTimeStart(DateTime? t) { ts = t; }
        /// <summary>
        /// Establece el valor de la propiedad de sólo lectura 
        /// <see cref="CurrentProgress"/> desde una clase derivada.
        /// </summary>
        /// <param name="Value">
        /// Valor que se desea establecer en <see cref="CurrentProgress"/>.
        /// </param>
        protected void SetCurrentProgress(float? Value) { curp = Value; }
        /// <summary>
        /// Genera el evento <see cref="Begun"/> desde una clase
        /// derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        /// <param name="e">Parámetros del evento.</param>
        protected void RaiseBegun(object sender, BegunEventArgs e)
        {
            Begun?.Invoke(sender, e);
        }
        /// <summary>
        /// Genera el evento <see cref="CancelRequested"/> desde una clase
        /// derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        /// <param name="e">Parámetros del evento.</param>
        protected void RaiseCancelRequested(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CancelRequested?.Invoke(sender, e);
        }
        /// <summary>
        /// Genera el evento <see cref="Ended"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        protected void RaiseEnded(object sender)
        {
            Ended?.Invoke(sender, EventArgs.Empty);
        }
        /// <summary>
        /// Genera el evento <see cref="Error"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        /// <param name="e">Parámetros del evento.</param>
        protected void RaiseError(object sender, Events.ExceptionEventArgs e)
        {
            Error?.Invoke(sender, e);
        }
        /// <summary>
        /// Genera el evento <see cref="Reporting"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">Instancia del objeto que generará el evento.
        /// </param>
        /// <param name="e">Parámetros del evento.</param>
        protected void RaiseReporting(object sender, ProgressEventArgs e)
        {
            Reporting?.Invoke(sender, e);
        }
        /// <summary>
        /// Genera el evento <see cref="Stopped"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        /// <param name="e">Parámetros del evento.</param>
        protected void RaiseStopped(object sender, ProgressEventArgs e)
        {
            Stopped?.Invoke(sender, e);
        }
        /// <summary>
        /// Genera el evento <see cref="TaskTimeout"/> desde una clase derivada.
        /// </summary>
        /// <param name="sender">
        /// Instancia del objeto que generará el evento.
        /// </param>
        /// <param name="e">Parámetros del evento.</param>
        protected void RaiseTimeout(object sender, ProgressEventArgs e)
        {
            TaskTimeout?.Invoke(sender, e);
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="TaskReporterPlugin"/>.
        /// </summary>
        protected TaskReporterPlugin() { Tmr.Elapsed += TmrElapsed; }
        #endregion
    }
}