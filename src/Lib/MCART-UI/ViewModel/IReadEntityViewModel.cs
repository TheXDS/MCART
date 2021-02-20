/*
IEntityViewModel.cs

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

namespace TheXDS.MCART.ViewModel
{
    /// <summary>
    /// Define una serie de métodos a implementar por una clase que exponga
    /// una entidad de sólo lectura dentro de una clase ViewModel del patrón
    /// MVVM.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de entidad a controlar.
    /// </typeparam>
    public interface IReadEntityViewModel<out T> : IViewModel
    {
        /// <summary>
        /// Instancia de la entidad controlada por este ViewModel.
        /// </summary>
        T Entity { get; }
    }
}