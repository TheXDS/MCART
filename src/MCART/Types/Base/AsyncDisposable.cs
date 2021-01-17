/*
AsyncDisposable.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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

namespace TheXDS.MCART.Types.Base
{
    /// <summary>
    /// Clase base que simplifica la implementación de las interfaces
    /// <see cref="IAsyncDisposable"/> y <see cref="IDisposable"/>.
    /// </summary>
    /// <remarks>
    /// Si la clase a implementar no contendrá acciones asíncronas de limpieza,
    /// utilice la clase <see cref="Disposable"/> como clase base.
    /// </remarks>
    public abstract class AsyncDisposable : Disposable, IAsyncDisposable
    {
        /// <inheritdoc/>
        public async ValueTask DisposeAsync()
        {
            await OnDisposeAsync();
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Realiza las operaciones de limpieza asíncrona de objetos
        /// administrados desechables asíncronamente de esta instancia.
        /// </summary>
        /// <returns>
        /// Un <see cref="ValueTask"/> que permite esperar a la operación
        /// asíncrona.
        /// </returns>
        protected abstract ValueTask OnDisposeAsync();
    }
}