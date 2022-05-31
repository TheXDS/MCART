/*
NotifyPropertyChangeBase.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2022 César Andrés Morgan

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

namespace TheXDS.MCART.Types.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Attributes;

/// <summary>
/// Clase base abstracta para todas las clases que implementen alguna
/// de las interfaces de notificación de propiedades disponibles en
/// .Net Framework / .Net Core.
/// </summary>
public abstract partial class NotifyPropertyChangeBase : INotifyPropertyChangeBase
{
    private readonly IDictionary<string, ICollection<string>> _observeTree
        = new Dictionary<string, ICollection<string>>();

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="NotifyPropertyChangeBase"/>.
    /// </summary>
    protected NotifyPropertyChangeBase()
    {
        ObserveTree = new ReadOnlyDictionary<string, ICollection<string>>(_observeTree);
    }

    /// <summary>
    /// Registra un Broadcast de notificación de cambio de propiedad.
    /// </summary>
    /// <param name="property">
    /// Propiedad a registrar.
    /// </param>
    /// <param name="affectedProperties">
    /// Colección de propiedades a notificar cuando se cambie el valor
    /// de esta propiedad.
    /// </param>
    protected void RegisterPropertyChangeBroadcast(string property, params string[] affectedProperties)
    {
        RegisterPropertyChangeBroadcast_Contract(property, affectedProperties);
        if (_observeTree.ContainsKey(property))
        {
            foreach (string? j in affectedProperties)
            {
                _observeTree[property].Add(j);
            }
        }
        else
        {
            _observeTree.Add(property, new HashSet<string>(affectedProperties));
        }
    }

    /// <summary>
    /// Registra la escucha de propiedades para notificar el cambio de otra.
    /// </summary>
    /// <param name="property">
    /// Propiedad a notificar.
    /// </param>
    /// <param name="listenedProperties">
    /// Propiedades a escuchar.
    /// </param>
    protected void RegisterPropertyChangeTrigger(string property, params string[] listenedProperties)
    {
        foreach (string? j in listenedProperties) RegisterPropertyChangeBroadcast(j, property);
    }

    /// <summary>
    /// Registra un Broadcast de notificación de cambio de propiedad.
    /// </summary>
    /// <param name="property">
    /// Propiedad a registrar.
    /// </param>
    /// <param name="affectedProperties">
    /// Colección de propiedades a notificar cuando se cambie el valor
    /// de esta propiedad.
    /// </param>
    protected void RegisterPropertyChangeBroadcast(string property, IEnumerable<string> affectedProperties)
    {
        RegisterPropertyChangeBroadcast(property, affectedProperties.ToArray());
    }

    /// <summary>
    /// Quita una entrada del registro de Broadcast de notificación de
    /// cambio de propiedad.
    /// </summary>
    /// <param name="property">
    /// Entrada a quitar del registro de Broadcast.
    /// </param>
    protected void UnregisterPropertyChangeBroadcast(string property)
    {
        if (_observeTree.ContainsKey(property))
            _observeTree.Remove(property);
    }

    /// <summary>
    /// Obtiene el árbol de notificaciones de cambio de propiedad.
    /// </summary>
    protected IReadOnlyDictionary<string, ICollection<string>> ObserveTree { get; }

    /// <summary>
    /// Notifica desde un punto externo el cambio en el valor de un
    /// conjunto de propiedades.
    /// </summary>
    /// <param name="properties">
    /// Colección con los nombres de las propiedades a notificar.
    /// </param>
    public void Notify(params string[] properties)
    {
        foreach (string? j in properties) Notify(j);
    }

    /// <summary>
    /// Notifica el cambio en el valor de una propiedad.
    /// </summary>
    /// <param name="property">
    /// Propiedad a notificar.
    /// </param>
    public abstract void Notify(string property);

    /// <summary>
    /// Ejecuta una propagación de notificación según el registro
    /// integrado de notificaciones suscritas.
    /// </summary>
    /// <param name="property">Propiedad a notificar.</param>
    protected void NotifyRegistroir(string property)
    {
        if (!_observeTree.ContainsKey(property)) return;
        foreach (string? j in _observeTree[property])
        {
            Notify(j);
        }
    }

    /// <summary>
    /// Notifica desde un punto externo el cambio en el valor de un
    /// conjunto de propiedades.
    /// </summary>
    /// <param name="properties">
    /// Enumeración con los nombres de las propiedades a notificar.
    /// </param>
    public void Notify(IEnumerable<string> properties) => Notify(properties.ToArray());

    /// <summary>
    /// Notifica desde un punto externo el cambio en el valor de un
    /// conjunto de propiedades.
    /// </summary>
    /// <param name="properties">
    /// Colección con las propiedades a notificar.
    /// </param>
    public void Notify(IEnumerable<PropertyInfo> properties) => Notify(properties.Select(p => p.Name));

    /// <summary>
    /// Obliga a notificar que todas las propiedades de este objeto han
    /// cambiado y necesitan refrescarse.
    /// </summary>
    public virtual void Refresh()
    {
        Notify(GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead));
    }

    private protected readonly HashSet<INotifyPropertyChangeBase> _forwardings = new();

    /// <summary>
    /// Agrega un objeto al cual reenviar los eventos de cambio de
    /// valor de propiedad.
    /// </summary>
    /// <param name="source">
    /// Objeto a registrar para el reenvío de eventos de cambio de
    /// valor de propiedad.
    /// </param>
    public void ForwardChange(INotifyPropertyChangeBase source)
    {
        _forwardings.Add(source);
    }

    /// <summary>
    /// Quita un objeto de la lista de reenvíos de eventos de cambio de
    /// valor de propiedad.
    /// </summary>
    /// <param name="source">
    /// Elemento a quitar de la lista de reenvío.
    /// </param>
    public void RemoveForwardChange(INotifyPropertyChangeBase source)
    {
        _forwardings.Remove(source);
    }

    /// <summary>
    /// Cambia el valor de un campo, y genera los eventos de
    /// notificación correspondientes.
    /// </summary>
    /// <typeparam name="T">Tipo de valores a procesar.</typeparam>
    /// <param name="field">Campo a actualizar.</param>
    /// <param name="value">Nuevo valor del campo.</param>
    /// <param name="propertyName">
    /// Nombre de la propiedad. Por lo general, este valor debe
    /// omitirse.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si el valor de la propiedad ha
    /// cambiado, <see langword="false"/> en caso contrario.
    /// </returns>
    [NpcChangeInvocator]
    protected abstract bool Change<T>(ref T field, T value, [CallerMemberName] string propertyName = null!);
}
