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
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using MCART.Events;
using St = MCART.Resources.Strings;

namespace MCART.Types.TaskReporter
{
    /// <summary>
    /// Definición parcial de una clase base que permite crear controles,
    /// Widgets o componentes para las distintas plataformas de MCART, que
    /// permite reportar visualmente el progreso de una tarea.
    /// </summary>
    public abstract partial class TaskReporterControl : ITaskReporter
    {
        #region Campos privados
        bool genEx;
        Extensions.Timer Tmr;
        #endregion
        #region Propiedades
        /// <summary>
        /// Indica el tiempo de espera disponible.
        /// </summary>
        /// <returns>
        /// El tiempo de espera disponible.
        /// </returns>
        public TimeSpan? TimeLeft => Tmr?.TimeLeft;
        /// <summary>
        /// Obtiene o establece el tiempo de espera total para la tarea actual.
        /// </summary>
        /// <returns>
        /// El tiempo de espera total establecido para la tarea actual.
        /// </returns>
        public TimeSpan? Timeout
        {
            get
            {
                if (Tmr.IsNull()) return null;
                return TimeSpan.FromMilliseconds(Tmr.Interval);
            }
            set
            {
                if (!OnDuty) throw new InvalidOperationException();
                Tmr = new Extensions.Timer()
                {
                    AutoReset = false,
                    Enabled = true
                };
                Tmr.Interval = value?.TotalMilliseconds ?? throw new ArgumentNullException(nameof(value));
                Tmr.Elapsed += Tmr_Elapsed;
            }
        }
        #endregion
        #region Eventos
        /// <summary>
        /// Se produce cuando se ha solicitado la detención de la tarea.
        /// </summary>
        public event CancelRequestedEventHandler CancelRequested;
        /// <summary>
        /// Se produce cuando una tarea se ha iniciado.
        /// </summary>
        public event BegunEventHandler Begun;
        /// <summary>
        /// Se produce cuando la tarea desea reportar su estado.
        /// </summary>
        public event ReportingEventHandler Reporting;
        /// <summary>
        /// Se produce cuando una tarea finalizó correctamente.
        /// </summary>
        public event EndedEventHandler Ended;
        /// <summary>
        /// Se produce cuando una tarea es cancelada.
        /// </summary>
        public event StoppedEventHandler Stopped;
        /// <summary>
        /// Se produce cuando una tarea indica que finalizó con error.
        /// </summary>
        public event ErrorEventHandler Error;
        /// <summary>
        /// Se produce cuando una tarea ha alcanzado el límite del tiempo de
        /// espera establecido.
        /// </summary>
        public event TaskTimeoutEventHandler TaskTimeout;
        #endregion
        #region Métodos privados
        void Bgn(bool ns)
        {
            if (OnDuty) throw new InvalidOperationException();
            CancelPendingProperty = false;
            TimedOutProperty = false;
            OnDutyProperty = true;
            TStartProperty = DateTime.Now;
            StoppableProperty = ns;
            OnBegin(true);
            Begun?.Invoke(this, new BegunEventArgs(ns, TStart));
        }
        void Bgn(TimeSpan timeout, bool genTOutEx, bool ns)
        {
            if (OnDuty) throw new InvalidOperationException();
            genEx = genTOutEx;
            Tmr = new Extensions.Timer(timeout.TotalMilliseconds) { AutoReset = false };
            Bgn(ns);
            Tmr.Elapsed += Tmr_Elapsed;
            Tmr.Start();
        }
        void Bsy(ProgressEventArgs e)
        {
            if (!OnDuty) throw new InvalidOperationException();
            OnBusy(e);
            Reporting?.Invoke(this, e);
        }
        void Rdy(string msg) => OnReady(msg ?? St.Rdy);
        void Tmr_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Tmr.Stop();
            Tmr.Elapsed -= Tmr_Elapsed;
            Tmr = null;
            RaiseCancelPending();
            TimedOutProperty = true;
            if (OnDuty)
            {
                TaskTimeout?.Invoke(this, null);
                try { OnReady(St.Timeout); }
                catch { throw; }
                if (genEx) throw new TimeoutException();
            }
        }
        #endregion
        #region Métodos protegidos
        /// <summary>
        /// Registra una solicitud para cancelar la tarea actual.
        /// </summary>
        protected void RaiseCancelPending()
        {
            if (!(bool)Stoppable) throw new InvalidOperationException();
            CancelEventArgs ev = new CancelEventArgs();
            CancelRequested?.Invoke(this, ev);
            CancelPendingProperty = true;
        }
        /// <summary>
        /// Modifica al control para pasar a un estado de 'Ocupado'
        /// </summary>
        /// <param name="stoppable">
        /// Indica si la tarea es detenible.
        /// </param>
        protected virtual void OnBegin(bool stoppable) { }
        /// <summary>
        /// Actualiza el estado del control basado en el estado de una tarea.
        /// </summary>
        /// <param name="e">
        /// Estado reportado por la tarea.
        /// </param>
        protected virtual void OnBusy(ProgressEventArgs e) { }
        /// <summary>
        /// Modifica al control para pasar a un estado de 'Listo'
        /// </summary>
        /// <param name="msg">
        /// Parámetro opcional. Mensaje a mostrar en el control. Es posible que
        /// algunos controles no implementen estados textuales, en cuyo caso el
        /// argumento será ignorado.
        /// </param>
        protected virtual void OnReady(string msg = null) { }
        #endregion
        #region Métodos públicos
        /// <summary>
        /// Indica que una tarea se ha iniciado. Genera el evento 
        /// <see cref="Begun"/>.
        /// </summary>
        public void Begin() => Bgn(true);
        /// <summary>
        /// Indica que una tarea se ha iniciado. Genera el evento 
        /// <see cref="Begun"/>.
        /// </summary>
        /// <param name="timeout">
        /// Indica el tiempo total de espera antes de generar el evento
        /// <see cref="TaskTimeout"/>.
        /// </param>
        /// <param name="genTOutEx">
        /// Parámetro opcional. Si es <c>true</c>, se generará un 
        /// <see cref="TimeoutException"/> al agotarse el tiempo de espera 
        /// especificado durante una tarea. Si se omite, o se establece en
        /// <c>false</c>, no se generará la excepción. El evento
        /// <see cref="TaskTimeout"/> se genera indistintamente de este valor.
        /// </param>
        /// <remarks>
        /// Para que una tarea pueda detenerse, ésta debe monitorear el valor
        /// de <see cref="CancelPending"/> por cada vuelta del ciclo, y tomar
        /// las acciones necesarias para finalizar. Opcionalmente puede
        /// manejarse la detención de la tarea por medio del evento
        /// <see cref="CancelRequested"/>.
        /// </remarks>
        public void Begin(TimeSpan timeout, bool genTOutEx = false) => Bgn(timeout, genTOutEx, true);
        /// <summary>
        /// Indica que una tarea que no se puede detener ha iniciado. Genera el
        /// evento <see cref="Begun"/>.
        /// </summary>
        public void BeginNonStop() => Bgn(false);
        /// <summary>
        /// Marca el inicio de una tarea que no se puede detener. Genera el
        /// evento <see cref="Begun"/>.
        /// </summary>
        /// <param name="timeout">
        /// Indica el tiempo total de espera antes de generar el evento 
        /// <see cref="TaskTimeout"/>.
        /// </param>
        /// <param name="genTOutEx">Parámetro opcional. un valor <c>true</c> provocará una excepción
        /// <see cref="TimeoutException"/> además del evento <see cref="TaskTimeout"/>. Si se omite, o se establece en
        /// <c>false</c>, no se generará la excepción.</param>
        /// <remarks>
        /// Al iniciar una tarea con este método, la tarea no se podrá
        /// detener ni se mostrará la interfaz para detenerla.
        /// </remarks>
        public void BeginNonStop(TimeSpan timeout, bool genTOutEx = false) => Bgn(timeout, genTOutEx, false);
        /// <summary>
        /// Marca el final de una tarea. Genera el evento <see cref="Ended"/>.
        /// </summary>
        public void End()
        {
            Rdy(St.Rdy);
            Ended?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Indica que la tarea finalizó con un error. Genera el evento 
        /// <see cref="Error"/>.
        /// </summary>
        /// <param name="ex">
        /// <see cref="Exception"/> que causó la finalización de esta tarea.
        /// </param>
        public void EndWithError(Exception ex = null)
        {
            Rdy(ex.Message);
            Error?.Invoke(this, new ExceptionEventArgs(ex));
        }
        /// <summary>
        /// Ejecuta un ciclo determinado por el delegado
        /// <paramref name="ForAct"/>.
        /// </summary>
        /// <param name="CEnd">Valor final del contador.</param>
        /// <param name="ForAct">Acción a ejecutar.</param>
        /// <param name="Message">
        /// Parámetro opcional. Mensaje a mostrar.
        /// </param>
        /// <param name="NonStop">
        /// Parámetro opcional. Si es <c>true</c>, el ciclo no podrá ser
        /// interrumpido. De forma predeterminada, se asume <c>false</c>.
        /// </param>
        /// <param name="OnCancel">
        /// Parámetro opcional. Acción a ejecutar en caso de cancelar el ciclo.
        /// </param>
        /// <param name="OnError">
        /// Parámetro opcional. Acción a ejecutar en caso de generarse un error
        /// durante la ejecución del ciclo.
        /// </param>
        /// <remarks>
        /// De forma predeterminada, el ciclo iniciará el contador en 0, y
        /// realizará incrementos de 1 por cada paso.
        /// </remarks>
        public async Task For(int CEnd, ForAction ForAct, string Message = null, bool NonStop = false, Action OnCancel = null, Action OnError = null)
        {
            await TaskReporter.For(0, CEnd, 1, ForAct, Message, NonStop, this, OnCancel, OnError);
        }
        /// <summary>
        /// Ejecuta un ciclo determinado por el delegado
        /// <paramref name="ForAct"/>.
        /// </summary>
        /// <param name="Cstart">Valor inicial del contador.</param>
        /// <param name="CEnd">Valor final del contador.</param>
        /// <param name="ForAct">Acción a ejecutar.</param>
        /// <param name="Message">
        /// Parámetro opcional. Mensaje a mostrar.
        /// </param>
        /// <param name="NonStop">
        /// Parámetro opcional. Si es <c>true</c>, el ciclo no podrá ser
        /// interrumpido. De forma predeterminada, se asume <c>false</c>.
        /// </param>
        /// <param name="OnCancel">
        /// Parámetro opcional. Acción a ejecutar en caso de cancelar el ciclo.
        /// </param>
        /// <param name="OnError">
        /// Parámetro opcional. Acción a ejecutar en caso de generarse un error
        /// durante la ejecución del ciclo.
        /// </param>
        /// <remarks>De forma predeterminada, el ciclo realizará incrementos de 1 por cada paso.</remarks>
        public async Task For(int Cstart, int CEnd, ForAction ForAct, string Message = null, bool NonStop = false, Action OnCancel = null, Action OnError = null)
        {
            await TaskReporter.For(Cstart, CEnd, 1, ForAct, Message, NonStop, this, OnCancel, OnError);
        }
        /// <summary>
        /// Ejecuta un ciclo determinado por el delegado 
        /// <paramref name="ForAct"/>.
        /// </summary>
        /// <param name="CStart">Valor inicial del contador.</param>
        /// <param name="CEnd">Valor final del contador.</param>
        /// <param name="CStep">Incrmento del contador por cada paso.</param>
        /// <param name="ForAct">Acción a ejecutar.</param>
        /// <param name="Message">
        /// Parámetro opcional. Mensaje a mostrar.
        /// </param>
        /// <param name="NonStop">
        /// Parámetro opcional. Si es <c>true</c>, el ciclo no podrá ser
        /// interrumpido. De forma predeterminada, se asume <c>false</c>.
        /// </param>
        /// <param name="OnCancel">
        /// Parámetro opcional. Acción a ejecutar en caso de cancelar el ciclo.
        /// </param>
        /// <param name="OnError">
        /// Parámetro opcional. Acción a ejecutar en caso de generarse un error
        /// durante la ejecución del ciclo.
        /// </param>
        public async Task For(int CStart, int CEnd, int CStep, ForAction ForAct, string Message = null, bool NonStop = false, Action OnCancel = null, Action OnError = null)
        {
            await TaskReporter.For(CStart, CEnd, CStep, ForAct, Message, NonStop, this, OnCancel, OnError);
        }
        /// <summary>
        /// Ejecuta un ciclo <c>For Each</c> determinado por el delegado
        /// <paramref name="ForEachAct"/>.
        /// </summary>
        /// <typeparam name="t">Tipo de la colección del ciclo.</typeparam>
        /// <param name="Coll">Colección del ciclo.</param>
        /// <param name="ForEachAct">Acción a ejecutar.</param>
        /// <param name="NonStop">
        /// Parámetro opcional. Si es <c>true</c>, el ciclo no podrá ser
        /// interrumpido. De forma predeterminada, se asume <c>false</c>.
        /// </param>
        /// <param name="OnCancel">
        /// Parámetro opcional. Acción a ejecutar en caso de cancelar el ciclo.
        /// </param>
        /// <param name="OnError">
        /// Parámetro opcional. Acción a ejecutar en caso de generarse un error
        /// durante la ejecución del ciclo.
        /// </param>
        /// <param name="Message">
        /// Parámetro opcional. Mensaje a mostrar.
        /// </param>
        public async Task ForEach<t>(IEnumerable<t> Coll, ForEachAction<t> ForEachAct, string Message = null, bool NonStop = false, Action OnCancel = null, Action OnError = null)
        {
            await TaskReporter.ForEach(Coll, ForEachAct, Message, NonStop, this, OnCancel, OnError);
        }
        /// <summary>
        /// Reporta el estado de la tarea actual. Genera el evento
        /// <see cref="Reporting"/> con la información provista sobre el
        /// progreso de la tarea.
        /// </summary>
        /// <param name="e">Información adicional del evento.</param>
        public void Report(ProgressEventArgs e) => Bsy(e);
        /// <summary>
        /// Reporta el estado de la tarea actual. Genera el evento 
        /// <see cref="Reporting"/> con la información provista sobre el
        /// progreso de la tarea.
        /// </summary>
        /// <param name="HelpText">Texto de ayuda sobre la tarea.</param>
        public void Report(string HelpText) => Bsy(new ProgressEventArgs(null, HelpText));
        /// <summary>
        /// Reporta el estado de la tarea actual. Genera el evento 
        /// <see cref="Reporting"/> con la información provista sobre el
        /// progreso de la tarea.
        /// </summary>
        /// <param name="Progress">
        /// <see cref="Nullable{T}"/> que representa el progreso actual de la
        /// tarea.
        /// </param>
        /// <param name="HelpText">Texto de ayuda sobre la tarea.</param>
        public void Report(float? Progress = null, string HelpText = null) => Bsy(new ProgressEventArgs(Progress, HelpText));
        /// <summary>
        /// Reinicia el contador de tiempo de espera durante una tarea.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Se produce cuando no se está ejecutando una tarea (el valor de
        /// <see cref="OnDuty"/> es <c>false</c>).</exception>
        public void ResetTimeout()
        {
            if (!OnDuty) throw new InvalidOperationException();
            Tmr.Reset();
        }
        /// <summary>
        /// Indica que la tarea actual ha sido detenida antes de finalizar.
        /// Genera el evento <see cref="Stopped"/>.
        /// </summary>
        /// <param name="e">
        /// <see cref="ProgressEventArgs"/> con información del progreso de la
        /// tarea al momento de la detención.
        /// </param>
        public void Stop(ProgressEventArgs e)
        {
            Rdy(St.UsrCncl);
            Stopped?.Invoke(this, e);
        }
        /// <summary>
        /// Indica que la tarea actual ha sido detenida antes de finalizar.
        /// Genera el evento <see cref="Stopped"/>.
        /// </summary>
        /// <param name="Progress">
        /// <see cref="Nullable{T}"/> que representa el progreso actual de la
        /// tarea.
        /// </param>
        /// <param name="HelpText">Texto de ayuda sobre la tarea.</param>
        public void Stop(float? Progress = null, string HelpText = null)
        {
            Rdy(St.UsrCncl);
            Stopped?.Invoke(this, new ProgressEventArgs(Progress, HelpText));
        }
        /// <summary>
        /// Indica que la tarea actual ha sido detenida antes de finalizar.
        /// Genera el evento <see cref="Stopped"/>.
        /// </summary>
        /// <param name="HelpText">Texto de ayuda sobre la tarea.</param>
        public void Stop(string HelpText)
        {
            Rdy(St.UsrCncl);
            Stopped?.Invoke(this, new ProgressEventArgs(null, HelpText));
        }
        #endregion
    }
}