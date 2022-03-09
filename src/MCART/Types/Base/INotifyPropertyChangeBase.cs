/*
INotifyPropertyChangeBase.cs

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

namespace TheXDS.MCART.Types.Base;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// Define una serie de miembros a implementar por un tipo que permita
/// notificar cambios en los valores de propiedades.
/// </summary>
public interface INotifyPropertyChangeBase : IRefreshable
{
    /// <summary>
    /// Agrega un objeto al cual reenviar los eventos de cambio de
    /// valor de propiedad.
    /// </summary>
    /// <param name="source">
    /// Objeto a registrar para el reenvío de eventos de cambio de
    /// valor de propiedad.
    /// </param>
    void ForwardChange(INotifyPropertyChangeBase source);

    /// <summary>
    /// Notifica desde un punto externo el cambio en el valor de una propiedad.
    /// </summary>
    /// <param name="properties">
    /// Colección con los nombres de las propiedades a notificar.
    /// </param>
    void Notify(IEnumerable<PropertyInfo> properties);

    /// <summary>
    /// Notifica desde un punto externo el cambio en el valor de una propiedad.
    /// </summary>
    /// <param name="properties">
    /// Colección con los nombres de las propiedades a notificar.
    /// </param>
    void Notify(IEnumerable<string> properties);

    /// <summary>
    /// Notifica desde un punto externo el cambio en el valor de una propiedad.
    /// </summary>
    /// <param name="properties">
    /// Colección con los nombres de las propiedades a notificar.
    /// </param>
    void Notify(params string[] properties);

    /// <summary>
    /// Notifica el cambio en el valor de una propiedad.
    /// </summary>
    /// <param name="property">
    /// Propiedad a notificar.
    /// </param>
    void Notify(string property);

    /// <summary>
    /// Quita un objeto de la lista de reenvíos de eventos de cambio de
    /// valor de propiedad.
    /// </summary>
    /// <param name="source">
    /// Elemento a quitar de la lista de reenvío.
    /// </param>
    void RemoveForwardChange(INotifyPropertyChangeBase source);
}
