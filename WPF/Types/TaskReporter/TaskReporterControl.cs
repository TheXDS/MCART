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

using MCART.Types.Extensions;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using St = MCART.Resources.Strings;

namespace MCART.Types.TaskReporter
{
    /// <summary>
    /// Clase base para los controles de Windows Presentation Framework
    /// que pueden utilizarse para mostrar el progreso de una tarea por medio
    /// de la interfaz <see cref="ITaskReporter"/>.
    /// </summary>
    public abstract partial class TaskReporterControl : UserControl
    {
        #region Declaración de propiedades de dependencia
        /// <summary>
        /// Llave de lectura/escritura para la propiedad de dependencia de sólo
        /// lectura <see cref="CancelPending"/>.
        /// </summary>
        protected static DependencyPropertyKey CancelPendingPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(CancelPending),
            typeof(bool),
            typeof(TaskReporterControl),
            new PropertyMetadata(false));
        /// <summary>
        /// Llave de lectura/escritura para la propiedad de dependencia de sólo
        /// lectura <see cref="OnDuty"/>.
        /// </summary>
        protected static DependencyPropertyKey OnDutyPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(OnDuty),
            typeof(bool),
            typeof(TaskReporterControl),
            new PropertyMetadata(false));
        /// <summary>
        /// Llave de lectura/escritura para la propiedad de dependencia de sólo
        /// lectura <see cref="Stoppable"/>.
        /// </summary>
        protected static DependencyPropertyKey StoppablePropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(Stoppable),
            typeof(bool?),
            typeof(TaskReporterControl),
            new PropertyMetadata(null));
        /// <summary>
        /// Llave de lectura/escritura para la propiedad de dependencia de sólo
        /// lectura <see cref="TStart"/>.
        /// </summary>
        protected static DependencyPropertyKey TStartPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(TStart),
            typeof(DateTime),
            typeof(TaskReporterControl),
            new PropertyMetadata(default(DateTime)));
        /// <summary>
        /// Llave de lectura/escritura para la propiedad de dependencia de sólo
        /// lectura <see cref="TimedOut"/>.
        /// </summary>
        protected static DependencyPropertyKey TimedOutPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(TimedOut),
            typeof(bool),
            typeof(TaskReporterControl),
            new PropertyMetadata(false));
        /// <summary>
        /// Llave de lectura/escritura para la propiedad de dependencia de sólo
        /// lectura <see cref="CurrentProgress"/>.
        /// </summary>
        protected static DependencyPropertyKey CurrentProgressPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(CurrentProgress),
            typeof(float?),
            typeof(TaskReporterControl),
            new PropertyMetadata(null));
        /// <summary>
        /// Identifica a la propiedad de dependencia de sólo lectura
        /// <see cref="CancelPending"/>.
        /// </summary>
        public static DependencyProperty CancelPendingProperty = CancelPendingPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifica a la propiedad de dependencia de sólo lectura
        /// <see cref="CurrentProgress"/>.
        /// </summary>
        public static DependencyProperty CurrentProgressProperty = CurrentProgressPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifica la propiedad de dependencia de sólo lectura
        /// <see cref="OnDuty"/>.
        /// </summary>
        public static DependencyProperty OnDutyProperty = OnDutyPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifica la propiedad de dependencia de sólo lectura
        /// <see cref="Stoppable"/>.
        /// </summary>
        public static DependencyProperty StoppableProperty = StoppablePropertyKey.DependencyProperty;
        /// <summary>
        /// Identifica la propiedad de dependencia de sólo lectura 
        /// <see cref="TStart"/>.
        /// </summary>
        public static DependencyProperty TStartProperty = TStartPropertyKey.DependencyProperty;
        /// <summary>
        /// Identifica la propiedad de dependencia de sólo lectura
        /// <see cref="TimedOut"/>.
        /// </summary>
        public static DependencyProperty TimedOutProperty = TimedOutPropertyKey.DependencyProperty;
        #endregion
        #region Propiedades
        /// <summary>
        /// Indica si hay pendiente una solicitud para cancelar la tarea.
        /// </summary>
        public bool CancelPending => (bool)GetValue(CancelPendingProperty);
        /// <summary>
        /// Indica el progreso actual de una tarea.
        /// </summary>
        public float? CurrentProgress => (float?)GetValue(CurrentProgressProperty);
        /// <summary>
        /// Indica si se está ejecutando una tarea actualmente.
        /// </summary>
        public bool OnDuty => (bool)GetValue(OnDutyProperty);
        /// <summary>
        /// Indica si la tarea puede ser detenida.
        /// </summary>
        /// <returns>
        /// <c>true</c> si la tarea puede ser detenida, <c>false</c> en caso
        /// contrario.
        /// </returns>
        public bool? Stoppable => (bool?)GetValue(StoppableProperty);
        /// <summary>
        /// Indica si ya se ha agotado el tiempo de espera de la tarea.
        /// </summary>
        /// <returns>
        /// <c>true</c> si ya se ha agotado el tiempo de espera, <c>false</c>
        /// en caso contrario.
        /// </returns>
        public bool TimedOut => (bool)GetValue(TimedOutProperty);
        /// <summary>
        /// Obtiene el momento de inicio de la tarea.
        /// </summary>
        public DateTime TStart => (DateTime)GetValue(TStartProperty);
        #endregion
        #region Métodos privados
        void Bgn(bool ns)
        {            
            if (OnDuty) throw new InvalidOperationException();
            SetValue(CancelPendingProperty, false);
            SetValue(TimedOutProperty, false);
            SetValue(OnDutyPropertyKey, true);
            SetValue(TStartPropertyKey, DateTime.Now);
            SetValue(StoppablePropertyKey, ns);
            if (Dispatcher.CheckAccess()) OnBegin(true);
            else Dispatcher.Invoke(new Action<bool>(OnBegin), true);
            Begun?.Invoke(this, new BegunEventArgs(ns, TStart));
        }
        void Bsy(ProgressEventArgs e)
        {
            if (!OnDuty) throw new InvalidOperationException();
            SetValue(CurrentProgressPropertyKey, e.Progress);
            if (Dispatcher.CheckAccess()) OnBusy(e);
            else Dispatcher.Invoke(new Action<ProgressEventArgs>(OnBusy), e);
            Reporting?.Invoke(this, e);
        }
        void Rdy(string msg)
        {
            if (Dispatcher.CheckAccess()) OnReady(msg);
            else Dispatcher.Invoke(new Action<string>(OnReady), St.Rdy);
        }
        void Tmr_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Tmr.Stop();
            Tmr.Elapsed -= Tmr_Elapsed;
            Tmr = null;
            RaiseCancelPending();
            SetValue(TimedOutPropertyKey, true);
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
            SetValue(CancelPendingPropertyKey, true);
        }
        #endregion
    }
}