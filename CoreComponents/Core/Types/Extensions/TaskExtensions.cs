/*
TaskExtensions.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCART.Types.Extensions
{
    public static class TaskExtensions
    {
        [DebuggerStepThrough]
        public static T Yield<T>(this Task<T> task)
        {
            task.Wait();
            return task.Result;
        }

        [DebuggerStepThrough]
        public static T Yield<T>(this Task<T> task, CancellationToken ct)
        {
            task.Wait(ct);
            return task.Result;
        }

        [DebuggerStepThrough]
        public static T Yield<T>(this Task<T> task, TimeSpan timeout)
        {
            task.Wait(timeout);
            return task.Result;
        }

        [DebuggerStepThrough]
        public static T Yield<T>(this Task<T> task, int msTimeout)
        {
            task.Wait(msTimeout);
            return task.Result;
        }

        [DebuggerStepThrough]
        public static T Yield<T>(this Task<T> task, int msTimeout, CancellationToken ct)
        {
            task.Wait(msTimeout,ct);
            return task.Result;
        }
    }
}