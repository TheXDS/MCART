/*
Disposable.cs

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

#nullable enable
#pragma warning disable CA1063 // IDisposable se implementa explícitamente de esa forma para simplificar la declaración de clases desechables.

using System;
using static System.Reflection.BindingFlags;

namespace TheXDS.MCART.Types.Base
{
    /// <summary>
    ///     Clase base que simplifica la implementación de la interfaz
    ///     <see cref="IDisposable"/> para los casos en los que no es necesario
    ///     finalizar recursos no administrados.
    /// </summary>
    public abstract class Disposable : IDisposable
    {
        /// <summary>
        ///     Obtiene un valor que indica si este objeto ha sido desechado.
        /// </summary>
        public bool Disposed { get; private set; } = false;

        /// <summary>
        ///     Libera los recursos utilizados por esta instancia.
        /// </summary>
        /// <param name="disposing">
        ///     Indica si deben liberarse los recursos administrados.
        /// </param>
        protected void Dispose(bool disposing)
        {
            if (Disposed) return;
            
            if (disposing)
            {
                OnDispose();
            }
            Disposed = true;            
        }

        /// <summary>
        ///     Desecha los objetos desechables detectados de esta instancia.
        /// </summary>
        /// <remarks>
        ///     De forma predeterminada, este método obtiene un listado de objetos desechables desde los campos privados del objeto para desechar. Si requiere una lógica personalizada para desechar objetos, reemplaze este método.
        /// </remarks>
        protected virtual void OnDispose()
        {
            foreach (var j in GetType().GetFields(NonPublic | Instance).FieldsOf<IDisposable>(this))
            {
                try
                {
                    j.Dispose();
                }
                catch { }
            }
        }

        /// <summary>
        ///     Libera los recursos utilizados por esta instancia.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
    }
}