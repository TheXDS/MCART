/*
INotifyPropertyChangeBase.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Reflection;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Define una serie de miembros a implementar por un tipo que permita
/// notificar cambios en los valores de propiedades.
/// </summary>
public interface INotifyPropertyChangeBase
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
