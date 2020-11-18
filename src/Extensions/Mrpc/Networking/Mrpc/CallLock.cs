/*
CallLock.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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
using System.Threading.Tasks;
using System.Reflection;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Networking.Mrpc
{
    /// <summary>
    /// Contiene información sobre una llamada específica a un procedimiento remoto.
    /// </summary>
    public class CallLock
    {
        internal CallLock(MethodInfo method)
        {
            Method = method.FullName();
            if (!method.IsVoid()) ReturnType = method.ReturnType;
        }

        internal TaskCompletionSource<byte[]> Waiter { get; } = new TaskCompletionSource<byte[]>();

        /// <summary>
        /// Obtiene el nombre completo del método que ha sido llamado.
        /// </summary>
        public string Method { get; }

        /// <summary>
        /// Obtiene el tipo devuelto por el método.
        /// </summary>
        public Type? ReturnType { get; }

        /// <summary>
        /// Obtiene la marca de tiempo que indica el momento en el que se
        /// ha realizado una llamada al método remoto.
        /// </summary>
        public DateTime Timestamp { get; } = DateTime.Now;

        /// <summary>
        /// Cancela la llamada remota.
        /// </summary>
        public void Abort()
        {
            Waiter.SetResult(Array.Empty<byte>());
        }
    }
}
