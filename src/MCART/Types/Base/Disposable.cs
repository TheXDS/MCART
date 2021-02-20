/*
Disposable.cs

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

#pragma warning disable CA1063 // IDisposable se implementa explícitamente de esta forma para simplificar la declaración de clases desechables.

using System;
using System.Runtime.CompilerServices;
using static System.Reflection.BindingFlags;

namespace TheXDS.MCART.Types.Base
{
    /// <summary>
    /// Clase base que simplifica la implementación de la interfaz
    /// <see cref="IDisposable"/>.
    /// </summary>
    /// <remarks>
    /// Si la clase a implementar contendrá acciones asíncronas de limpieza,
    /// utilice la clase <see cref="AsyncDisposable"/> como clase base.
    /// </remarks>
    public abstract class Disposable : IDisposableEx
    {
        private bool ShouldFinalize() => GetType().GetMethod(nameof(OnFinalize), Instance | NonPublic)!.IsOverride();

        /// <summary>
        /// Obtiene un valor que indica si este objeto ha sido desechado.
        /// </summary>
        public bool IsDisposed { get; private set; } = false;

        /// <summary>
        /// Libera los recursos utilizados por esta instancia.
        /// </summary>
        /// <param name="disposing">
        /// Indica si deben liberarse los recursos administrados.
        /// </param>
        protected void Dispose(bool disposing)
        {
            if (IsDisposed) return;
            
            if (disposing)
            {
                OnDispose();
            }
            OnFinalize();
            IsDisposed = true;            
        }

        /// <summary>
        /// Realiza operaciones de limpieza para objetos no administrados.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnFinalize() { }

        /// <summary>
        /// Realiza las operaciones de limpieza de objetos administrados
        /// desechables de esta instancia.
        /// </summary>
        protected abstract void OnDispose();

        /// <summary>
        /// Libera los recursos utilizados por esta instancia.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            if (ShouldFinalize()) GC.SuppressFinalize(this);            
        }

        /// <summary>
        /// Destruye esta instancia de la clase <see cref="Disposable"/>.
        /// </summary>
        ~Disposable()
        {
            if (ShouldFinalize()) Dispose(false);            
        }
    }
}