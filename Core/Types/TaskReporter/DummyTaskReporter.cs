//
//  DummyTaskReporter.cs
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

namespace MCART.Types.TaskReporter
{
    /// <summary>
    /// <see cref="TaskReporter"/> que no implementa ningún medio de 
    /// interacción con el usuario.
    /// </summary>
    public class DummyTaskReporter : TaskReporter
    {
        public override bool CancelPending => false;
        public override void Begin()=>BeginNonStop();        
        public override void BeginNonStop()
        {
            if (OnDuty) throw new InvalidOperationException();
            SetTimeStart(DateTime.Now);
            SetOnDuty(true);
            RaiseBegun(this, new BegunEventArgs(false, TStart));
        }
        public override void End()
        {
            if (!OnDuty) throw new InvalidOperationException();
            SetOnDuty(false);
            SetTimeStart(null);
            RaiseEnded(this);
        }
        public override void EndWithError(Exception ex = null)
        {
            if (!OnDuty) throw new InvalidOperationException();
            SetOnDuty(false);
            SetTimeStart(null);
            RaiseError(this, new Events.ExceptionEventArgs(ex));
        }
        public override void Report(ProgressEventArgs e)
        {
            if (!OnDuty) throw new InvalidOperationException();
            RaiseReporting(this, e);
        }
        public override void Report(string helpText)
        {
            if (!OnDuty) throw new InvalidOperationException();
            RaiseReporting(this, new ProgressEventArgs(null, helpText));
        }
        public override void Report(float? progress = default(float?), string helpText = null)
        {
            if (!OnDuty) throw new InvalidOperationException();
            RaiseReporting(this, new ProgressEventArgs(progress, helpText));
        }
        public override void Stop(ProgressEventArgs e)
        {
            if (!OnDuty) throw new InvalidOperationException();
            SetOnDuty(false);
            SetTimeStart(null);
            RaiseStopped(this, e);
        }
        public override void Stop(float? progress = default(float?), string helpText = null)
        {
            if (!OnDuty) throw new InvalidOperationException();
            SetOnDuty(false);
            SetTimeStart(null);
            RaiseStopped(this, new ProgressEventArgs(progress, helpText));
        }
        public override void Stop(string helpText)
        {
            if (!OnDuty) throw new InvalidOperationException();
            SetOnDuty(false);
            SetTimeStart(null);
            RaiseStopped(this, new ProgressEventArgs(null, helpText));
        }
    }
}
