/*
ObservableWrap.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

using System.Collections.Specialized;
using TheXDS.MCART.Types.Base;
using System.Collections;
using System.Diagnostics;
using NcchEa = System.Collections.Specialized.NotifyCollectionChangedEventArgs;

namespace TheXDS.MCART.Types.Base
{
    /// <summary>
    ///     Clase base para los envoltorios observables de colecciones.
    /// </summary>
    [DebuggerStepThrough]
    public abstract class ObservableWrapBase : NotifyPropertyChanged, INotifyCollectionChanged, IEnumerable
    {
        /// <summary>
        ///     Se produce al ocurrir un cambio en la colección.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        ///     Genera el evento <see cref="CollectionChanged"/>.
        /// </summary>
        /// <param name="eventArgs">Argumentos del evento.</param>
        protected void RaiseCollectionChanged(NcchEa eventArgs)
        {
            CollectionChanged?.Invoke(this, eventArgs);
        }

        /// <summary>
        ///     Obtiene un enumerador que itera sobre la colección.
        /// </summary>
        /// <returns>
        ///     Un enumerador que puede ser utilizado para iterar sobre la colección.
        /// </returns>
        public abstract IEnumerator GetEnumerator();
    }
}