/*
IValidatingViewModel.cs

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

namespace TheXDS.MCART.ViewModel;
using TheXDS.MCART.Types.Base;

/// <summary>
/// Define una serie de miembros a implementar por un tipo que extienda
/// <see cref="IViewModel"/> que provee de servicios de validación de datos.
/// </summary>
public interface IValidatingViewModel : IViewModel, INotifyPropertyChangeBase
{
    /// <summary>
    /// Obtiene el origen de validación para esta instancia.
    /// </summary>
    /// <remarks>
    /// Esta propiedad debe establecerse en el constructor del ViewModel de la siguiente manera:
    /// <code lang="csharp">
    /// ErrorSource = new ValidationSource&lt;TViewModel&gt;(this);
    /// </code>
    /// </remarks>
    ValidationSource ErrorSource { get; }
}
