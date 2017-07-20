
//
//  BarStepper.cs
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

using MCART;
using MCART.Events;
using MCART.Types.TaskReporter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using St = MCART.Resources.Strings;
namespace Controls
{
    /// <summary>
    /// Este control permite reportar visualmente el progreso de una tarea en 
    /// una barra de estado; opcionalmente, mostrando además un botón para 
    /// cancelar dicha tarea.
    /// </summary>
    /// <remarks>
    /// Este control es compatible con la interfaz <see cref="ITaskReporter"/>.
    /// </remarks>
    /// <example>
    /// Vea la página de ayuda de <see cref="ITaskReporter"/> para ver ejemplos
    /// de cómo utilizar este control.
    /// </example>
    public class BarStepper : UserControl, ITaskReporter
    {
        #region Miembros compartidos / Propiedades de dependencia
        private static Type T = typeof(BarStepper);
        /// <summary>
        /// Referencia a la propiedad de dependencia 
        /// <see cref="StatusForReady"/>.
        /// </summary>
        public static DependencyProperty StatusForReadyProperty = DependencyProperty.Register(nameof(StatusForReady), typeof(Visibility), T, new PropertyMetadata(Visibility.Hidden), (a) => (Visibility)a != Visibility.Visible);
        /// <summary>
        /// Referencia a la propiedad de dependencia <see cref="Minimum"/>.
        /// </summary>
        public static DependencyProperty MinimumProperty = DependencyProperty.Register(nameof(Minimum), typeof(double), T, new PropertyMetadata(0.0, (d, e) =>
        {
            if ((double)e.NewValue > ((BarStepper)d).PB.Value || (double)e.NewValue > ((BarStepper)d).Maximum)
                throw new ArgumentOutOfRangeException();
        }));
        /// <summary>
        /// Referencia a la propiedad de dependencia <see cref="Maximum"/>.
        /// </summary>
        public static DependencyProperty MaximumProperty = DependencyProperty.Register(nameof(Maximum), typeof(double), T, new PropertyMetadata(100.0, (d, e) =>
        {
            if ((double)e.NewValue < ((BarStepper)d).PB.Value || (double)e.NewValue < ((BarStepper)d).Minimum)
                throw new ArgumentOutOfRangeException();
        }));
        #endregion
        #region Propiedades
        /// <summary>
        /// Obtiene o establece el máximo de pasos o el valor máximo de la 
        /// barra de progreso.
        /// </summary>
        /// <returns>
        /// El valor máximo actualmente establecido para el objeto que 
        /// representará el progreso.
        /// </returns>
        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }
        /// <summary>
        /// Obtiene o establece el mínimo de pasos o el valor mínimo de la 
        /// barra de progreso.
        /// </summary>
        /// <returns>
        /// El valor mínimo actualmente establecido para el objeto que 
        /// representará el progreso.
        /// </returns>
        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }
        /// <summary>
        /// Determina si al finalizar una tarea, los controles se ocultarán o 
        /// si serán colapsados.
        /// </summary>
        /// <returns>
        /// El valor de <seealso cref="System.Windows.Visibility"/> aplicado a
        /// los controles al finalizar una tarea.
        /// </returns>
        public Visibility StatusForReady
        {
            get => (Visibility)GetValue(StatusForReadyProperty);
            set => SetValue(StatusForReadyProperty, value);
        }
        /// <summary>
        /// Devuelve <c>true</c> si el <see cref="ITaskReporter"/> está 
        /// mostrando el progreso de una operación.
        /// </summary>
        public bool OnDuty => PB.Visibility == Visibility.Visible;
        /// <summary>
        /// Devuelve <c>true</c> si se ha solicitado el final de la tarea en 
        /// curso.
        /// </summary>
        /// <returns>
        /// <c>true</c> si se ha solicitado el final de la tarea en curso, de
        /// lo contrario, <c>false</c>.
        /// </returns>
        public bool CancelPending => cncl;
        /// <summary>
        /// Lee el estado de completación de la tarea.
        /// </summary>
        /// <returns>
        /// Un <seealso cref="double"/> que es el estado de completación actual
        /// de la tarea representada en este control.
        /// </returns>
        public double CurrentValue => PB.Value;
        /// <summary>
        /// Indica el momento en el que se inició una tarea.
        /// </summary>
        /// <returns>
        /// Un valor de tiempo que indica el momento en el que se inició una 
        /// tarea.
        /// </returns>
        public DateTime TStart => ts.Value;
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
            get {
                if (Tmr.IsNull()) return null;
                return TimeSpan.FromMilliseconds(Tmr.Interval);
            }            
            set {
                if (!OnDuty) throw new InvalidOperationException();
                if (Tmr.IsNull())
                {
                    Tmr = new MCART.Types.Extensions.Timer();
                    Tmr.Elapsed += Tmr_Elapsed;
                }
                Tmr.Interval = value?.TotalMilliseconds ?? throw new ArgumentNullException(nameof(value));
            }
        }
        /// <summary>
        /// Obtiene un valor que determina si ya se ha agotado el tiempo de 
        /// espera.
        /// </summary>
        /// <returns>
        /// Un valor que determina si ya se ha agotado el tiempo de espera.
        /// </returns>
        public bool TimedOut => !ts.IsNull() ? Tmr?.TimeLeft.Value.TotalMilliseconds <= 0 : false;
        #endregion
        #region Eventos
        /// <summary>
        /// Se genera cuando este <see cref="ITaskReporter"/> solicita a la
        /// tarea detenerse.
        /// </summary>
        public event CancelRequestedEventHandler CancelRequested;
        /// <summary>
        /// Se genera cuando una tarea se ha iniciado.
        /// </summary>
        public event BegunEventHandler Begun;
        /// <summary>
        /// Se genera cuando la tarea desea reportar su estado
        /// </summary>
        public event ReportingEventHandler Reporting;
        /// <summary>
        /// Se genera cuando una tarea finalizó correctamente
        /// </summary>
        public event EndedEventHandler Ended;
        /// <summary>
        /// Se genera cuando una tarea es cancelada
        /// </summary>
        public event StoppedEventHandler Stopped;
        /// <summary>
        /// Se genera cuando una tarea indica que finalizó con error
        /// </summary>
        public event ErrorEventHandler Error;
        /// <summary>
        /// Se genera cuando una tarea ha alcanzado el límite del tiempo de
        /// espera establecido.
        /// </summary>
        public event TaskTimeoutEventHandler TaskTimeout;
        #endregion
        #region Miembros privados
        private ProgressBar PB = new ProgressBar { Margin = new Thickness(8) };
        private Button Bt = new Button();
        private Label LB = new Label();
        private Visibility hr = Visibility.Hidden;
        private bool cncl = false;
        private DateTime? ts = null;
        private MCART.Types.Extensions.Timer Tmr;
        private bool ge;
        #endregion
        #region Métodos privados
        private void Bsy(ProgressEventArgs e)
        {
            LB.Content = e.HelpText;
            if (e.Progress == null || !((double)e.Progress).IsBetween(PB.Minimum, PB.Maximum))
            {
                PB.IsIndeterminate = true;
            }
            else
            {
                PB.IsIndeterminate = false;
                PB.Value = (double)e.Progress;
            }
        }
        private void DoBegin(bool Stoppable)
        {
            cncl = false;
            PB.Visibility = Visibility.Visible;
            Bt.Content = St.Cncl;
            Bt.IsEnabled = Stoppable;
            if (Stoppable)
                Bt.Visibility = Visibility.Visible;
            PB.Value = PB.Minimum;
            PB.IsIndeterminate = false;
        }
        private void DoRdy(string msg)
        {
            PB.SetBinding(VisibilityProperty, new Binding(nameof(StatusForReady)) { Source = this });
            Bt.SetBinding(VisibilityProperty, new Binding(nameof(StatusForReady)) { Source = this });
            LB.Content = msg;
            PB.IsIndeterminate = false;
            PB.Value = 0;
        }
        private void Rdy(string msg)
        {
            if (!OnDuty) throw new InvalidOperationException();
            if (Dispatcher.CheckAccess()) DoRdy(msg);            
            else Dispatcher.Invoke(new Action<string>(DoRdy), St.Rdy);            
        }
        private void Tmr_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Tmr.Stop();
            Tmr.Elapsed -= Tmr_Elapsed;
            Tmr = null;
            cncl = true;
            if (OnDuty)
            {
                try { Rdy(St.Timeout); }
                catch { throw; }
                TaskTimeout?.Invoke(this, null);
                if (ge) throw new TimeoutException();
            }
        }
        #endregion
        #region Métodos públicos
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="BarStepper"/>.
        /// </summary>
        public BarStepper()
        {
            MinWidth = 124;
            MinHeight = 24;
            MaxHeight = 24;
            Grid.SetColumn(Bt, 1);
            Grid.SetColumn(LB, 2);
            PB.SetBinding(RangeBase.MinimumProperty, new Binding(nameof(Minimum)) { Source = this });
            PB.SetBinding(RangeBase.MaximumProperty, new Binding(nameof(Maximum)) { Source = this });
            Grid a = new Grid();
            a.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
            a.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(24) });
            a.ColumnDefinitions.Add(new ColumnDefinition());
            a.Children.Add(PB);
            a.Children.Add(Bt);
            a.Children.Add(LB);
            Bt.Click += IssueStop;
            DoRdy(St.Rdy);
        }
        /// <summary>
        /// Solicita que la tarea se detenga. También genera el evento
        /// <see cref="CancelRequested"/>.
        /// </summary>
        /// <param name="sender">Objeto que ha invocado a este método.</param>
        /// <param name="e">Argumentos de evento.</param>
        public void IssueStop(object sender = null, RoutedEventArgs e = null)
        {
            if (!OnDuty) throw new InvalidOperationException();
            CancelEventArgs ev = new CancelEventArgs();
            CancelRequested?.Invoke(this, ev);
            cncl = !ev.Cancel;
        }
        /// <summary>
        /// Marca el inicio de una tarea. Genera el evento <see cref="Begun"/>.
        /// </summary>
        public void Begin()
        {
            if (OnDuty) throw new InvalidOperationException();
            ts = DateTime.Now;
            if (Dispatcher.CheckAccess()) DoBegin(true);
            else Dispatcher.Invoke(new Action<bool>(DoBegin), true);
            Begun?.Invoke(this, new BegunEventArgs(true, ts.GetValueOrDefault()));
        }
        /// <summary>
        /// Marca el inicio de una tarea que no se puede detener. Genera el
        /// evento <see cref="Begun"/>.
        /// </summary>
        public void BeginNonStop()
        {
            if (OnDuty) throw new InvalidOperationException();
            ts = DateTime.Now;
            if (Dispatcher.CheckAccess()) DoBegin(false);
            else Dispatcher.Invoke(new Action<bool>(DoBegin), false);
            Begun?.Invoke(this, new BegunEventArgs(false, ts.GetValueOrDefault()));
        }
        /// <summary>
        /// Marca el final de una tarea. Genera el evento <see cref="Ended"/>.
        /// </summary>
        public void End()
        {
            try { Rdy(St.Rdy); }
            catch { throw; }
            Ended?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Indica que la tarea finalizó con un error. Genera el evento 
        /// <see cref="Error"/>.
        /// </summary>
        /// <param name="ex">
        /// <see cref="Exception"/> que causó la finalización de esta tarea.
        /// </param>
        public void EndWithError(Exception ex)
        {
            try { Rdy(ex.Message); }
            catch { throw; }
            Error?.Invoke(this, new ExceptionEventArgs(ex));
        }
        /// <summary>
        /// Reporta el estado de la tarea actual. Genera el evento
        /// <see cref="Reporting"/> con la información provista sobre el
        /// progreso de la tarea.
        /// </summary>
        /// <param name="e">Información adicional del evento.</param>
        public void Report(ProgressEventArgs e)
        {
            if (!OnDuty) throw new InvalidOperationException();
            if (Dispatcher.CheckAccess()) Bsy(e);
            else Dispatcher.Invoke(new Action<ProgressEventArgs>(Bsy), e);
            Reporting?.Invoke(this, e);
        }
        /// <summary>
        /// Reporta el estado de la tarea actual. Genera el evento 
        /// <see cref="Reporting"/> con la información provista sobre el
        /// progreso de la tarea.
        /// </summary>
        /// <param name="HelpText">Texto de ayuda sobre la tarea.</param>
        public void Report(string HelpText)
        {
            if (!OnDuty) throw new InvalidOperationException();
            ProgressEventArgs e = new ProgressEventArgs(null, HelpText);
            if (Dispatcher.CheckAccess()) Bsy(e);
            else Dispatcher.Invoke(new Action<ProgressEventArgs>(Bsy), e);
            Reporting?.Invoke(this, e);
        }
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
        public void Report(double? Progress = null, string HelpText = null)
        {
            if (!OnDuty) throw new InvalidOperationException();
            ProgressEventArgs e = new ProgressEventArgs(Progress, HelpText);
            if (Dispatcher.CheckAccess()) Bsy(e);
            else Dispatcher.Invoke(new Action<ProgressEventArgs>(Bsy), e);
            Reporting?.Invoke(this, e);
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
            try { Rdy(St.UsrCncl); }
            catch { throw; }
            Stopped?.Invoke(this, e);
        }
        /// <summary>
        /// Indica que la tarea actual ha sido detenida antes de finalizar.
        /// Genera el evento <see cref="Stopped"/>.
        /// </summary>
        /// <param name="HelpText">Texto de ayuda sobre la tarea.</param>
        public void Stop(string HelpText)
        {
            try { Rdy(St.UsrCncl); }
            catch { throw; }
            Stopped?.Invoke(this, new ProgressEventArgs(null, HelpText));
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
        public void Stop(double? Progress = null, string HelpText = null)
        {
            try { Rdy(St.UsrCncl); }
            catch { throw; }
            Stopped?.Invoke(this, new ProgressEventArgs(Progress, HelpText));
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
        /// Marca el inicio de una tarea. Genera el evento <see cref="Begun"/>.
        /// </summary>
        /// <param name="Timeout">
        /// Indica el tiempo total de espera antes de generar el evento
        /// <see cref="TaskTimeout"/>.
        /// </param>
        /// <param name="GenTimeoutException">
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
        public void Begin(TimeSpan Timeout, bool GenTimeoutException = false)
        {
            if (OnDuty) throw new InvalidOperationException();
            ge = GenTimeoutException;
            Tmr = new MCART.Types.Extensions.Timer(Timeout.TotalMilliseconds)
            {
                AutoReset = false,
                Enabled = true
            };
            Tmr.Elapsed += Tmr_Elapsed;
            Begin();
        }
        /// <summary>
        /// Marca el inicio de una tarea que no se puede detener. Genera el
        /// evento <see cref="Begun"/>.
        /// </summary>
        /// <param name="Timeout">
        /// Indica el tiempo total de espera antes de generar el evento 
        /// <see cref="TaskTimeout"/>.
        /// </param>
        /// <param name="GenTimeoutException">Parámetro opcional. un valor <c>true</c> provocará una excepción
        /// <see cref="TimeoutException"/> además del evento <see cref="TaskTimeout"/>. Si se omite, o se establece en
        /// <c>false</c>, no se generará la excepción.</param>
        /// <remarks>
        /// Al iniciar una tarea con este método, la tarea no se podrá
        /// detener ni se mostrará la interfaz para detenerla.
        /// </remarks>
        public void BeginNonStop(TimeSpan Timeout, bool GenTimeoutException = false)
        {
            if (OnDuty) throw new InvalidOperationException();
            ge = GenTimeoutException;
            Tmr = new MCART.Types.Extensions.Timer(Timeout.TotalMilliseconds)
            {
                AutoReset = false,
                Enabled = true
            };
            Tmr.Elapsed += Tmr_Elapsed;
            BeginNonStop();
        }
        /// <summary>
        /// Reinicia el contador de tiempo de espera durante una tarea
        /// </summary>
        /// <exception cref="InvalidOperationException">Se produce cuando no se está dentro de una tarea (el valor de <see cref="OnDuty"/> es <c>false</c></exception>
        public void ResetTimeout()
        {
            if (!OnDuty) throw new InvalidOperationException();
            Tmr.Reset();
        }
        #endregion
    }
}
