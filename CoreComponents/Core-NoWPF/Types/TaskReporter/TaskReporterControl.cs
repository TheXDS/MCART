//
//  TaskReporterControl.cs
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
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Types.TaskReporter
{
    /// <summary>
    /// Definición parcial de una clase base que permite crear controles,
    /// Widgets o componentes para las distintas plataformas de MCART, que
    /// permite reportar visualmente el progreso de una tarea. Esta definición
    /// parcial no es compatible con Windows Presentation Framework.
    /// </summary>
    public abstract partial class TaskReporterControl : ITaskReporter
    {
        #region Campos privados
        bool CancelPendingProperty;
        bool OnDutyProperty;
        bool? StoppableProperty;
        DateTime TStartProperty;
        bool TimedOutProperty;
        float? CurrentProgressProperty;
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
            Invoke(new Action<bool>(OnBegin), ns);
            Begun?.Invoke(this, new BegunEventArgs(ns, TStart));
        }
        void Bsy(ProgressEventArgs e)
        {
            if (!OnDuty) throw new InvalidOperationException();
            CurrentProgressProperty = e.Progress;
            Invoke(new Action<ProgressEventArgs>(OnBusy), e);
            Reporting?.Invoke(this, e);
        }
        void Rdy(string msg) => Invoke(new Action<string>(OnReady), msg ?? St.Rdy);
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
        #endregion
        #region Propiedades
        /// <summary>
        /// Indica si hay pendiente una solicitud para cancelar la tarea.
        /// </summary>
        public bool CancelPending => CancelPendingProperty;
        /// <summary>
        /// Indica el progreso actual de una tarea.
        /// </summary>
        public float? CurrentProgress => CurrentProgressProperty;
        /// <summary>
        /// Indica si se está ejecutando una tarea actualmente.
        /// </summary>
        public bool OnDuty => OnDutyProperty;
        /// <summary>
        /// Indica si la tarea puede ser detenida.
        /// </summary>
        /// <returns>
        /// <c>true</c> si la tarea puede ser detenida, <c>false</c> en caso
        /// contrario.
        /// </returns>
        public bool? Stoppable => StoppableProperty;
        /// <summary>
        /// Indica si ya se ha agotado el tiempo de espera de la tarea.
        /// </summary>
        /// <returns>
        /// <c>true</c> si ya se ha agotado el tiempo de espera, <c>false</c>
        /// en caso contrario.
        /// </returns>
        public bool TimedOut => TimedOutProperty;
        /// <summary>
        /// Obtiene el momento de inicio de la tarea.
        /// </summary>
        public DateTime TStart => TStartProperty;
        #endregion
    }
}