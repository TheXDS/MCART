/*
ViewModelBase.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Clase base para la creación de ViewModels.
/// </summary>
public abstract partial class ViewModelBase : NotifyPropertyChanged
{
    private bool _isBusy;
    private readonly Dictionary<string, HashSet<Action>> _observeRegistry = [];

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="ViewModelBase"/>.
    /// </summary>
    protected ViewModelBase()
    {
        PropertyChanged += OnInvokeObservedProps;
    }

    /// <summary>
    /// Obtiene un valor que indica si este <see cref="ViewModelBase"/>
    /// está ocupado.
    /// </summary>
    public bool IsBusy
    {
        get => _isBusy;
        protected set => Change(ref _isBusy, value);
    }

    /// <summary>
    /// Registra una propiedad con notificación de cambio de valor para ser
    /// observada y manejada por el delegado especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de la propiedad.</typeparam>
    /// <param name="propertySelector">
    /// Función selectora de la propiedad a observar.
    /// </param>
    /// <param name="handler">
    /// Delegado a invocar cuando la propiedad haya cambiado.
    /// </param>
    /// <exception cref="InvalidArgumentException">
    /// Se produce si la función de selección de propiedad no ha
    /// seleccionado un miembro válido de la instancia a configurar.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="propertySelector"/> o
    /// <paramref name="handler"/> son <see langword="null"/>.
    /// </exception>
    protected void Observe<T>(Expression<Func<T>> propertySelector, Action handler)
    {
        Observe_Contract(propertySelector);
        ObserveFrom(this, ReflectionHelpers.GetProperty(propertySelector), handler);
    }

    /// <summary>
    /// Registra una propiedad con notificación de cambio de valor para ser
    /// observada y manejada por el delegado especificado.
    /// </summary>
    /// <param name="propertySelectors">
    /// Funciones selectoras de las propiedades a observar.
    /// </param>
    /// <param name="handler">
    /// Delegado a invocar cuando cualquiera de las propiedades haya cambiado.
    /// </param>
    /// <exception cref="InvalidArgumentException">
    /// Se produce si la función de selección de propiedad no ha
    /// seleccionado un miembro válido de la instancia a configurar.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="propertySelectors"/> o
    /// <paramref name="handler"/> son <see langword="null"/>.
    /// </exception>
    /// <exception cref="EmptyCollectionException">
    /// Se produce si <paramref name="propertySelectors"/> no contiene
    /// elementos.
    /// </exception>
    protected void Observe(Expression<Func<object?>>[] propertySelectors, Action handler)
    {
        Observe_Contract(propertySelectors);
        foreach (var prop in propertySelectors)
        {
            ObserveFrom(this, ReflectionHelpers.GetProperty(prop), handler);
        }
    }

    /// <summary>
    /// Registra una propiedad con notificación de cambio de valor para ser
    /// observada y manejada por el delegado especificado.
    /// </summary>
    /// <param name="propertyName">
    /// Nombre de la propiedad a observar.
    /// </param>
    /// <param name="handler">
    /// Delegado a invocar cuando la propiedad haya cambiado.
    /// </param>
    /// <exception cref="InvalidArgumentException">
    /// Se produce si <paramref name="propertyName"/> es una cadena vacía o
    /// una cadena de espacios.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="propertyName"/> o 
    /// <paramref name="handler"/> son  <see langword="null"/>.
    /// </exception>
    protected void Observe(string propertyName, Action handler)
    {
        Observe_Contract(propertyName, handler);
        if (!_observeRegistry.TryGetValue(propertyName, out HashSet<Action>? value))
        {
            value = [];
            _observeRegistry.Add(propertyName, value);
        }
        value.Add(handler);
    }

    /// <summary>
    /// Registra una propiedad con notificación de cambio de valor para ser
    /// observada y manejada por el delegado especificado.
    /// </summary>
    /// <param name="propertyNames">
    /// Nombres de las propiedades a observar.
    /// </param>
    /// <param name="handler">
    /// Delegado a invocar cuando cualquiera de las propiedades haya cambiado.
    /// </param>
    /// <exception cref="InvalidArgumentException">
    /// Se produce si cualquiera de los elementos de
    /// <paramref name="propertyNames"/> es <see langword="null"/>, una cadena
    /// vacía o una cadena de espacios.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="propertyNames"/> o 
    /// <paramref name="handler"/> son <see langword="null"/>.
    /// </exception>
    protected void Observe(string[] propertyNames, Action handler)
    {
        Observe_Contract(propertyNames, handler);
        foreach (var prop in propertyNames)
        {
            if (!_observeRegistry.TryGetValue(prop, out HashSet<Action>? value))
            {
                value = [];
                _observeRegistry.Add(prop, value);
            }
            value.Add(handler);
        }
    }

    /// <summary>
    /// Registra una propiedad con notificación de cambio de valor para ser
    /// observada y manejada por el delegado especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de la propiedad.</typeparam>
    /// <param name="source">Origen observado.</param>
    /// <param name="propertySelector">
    /// Función selectora de la propiedad a observar.
    /// </param>
    /// <param name="handler">
    /// Delegado a invocar cuando la propiedad haya cambiado.
    /// </param>
    /// <exception cref="InvalidArgumentException">
    /// Se produce si la función de selección de propiedad no ha
    /// seleccionado un miembro válido de la instancia a configurar.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="source"/>,
    /// <paramref name="propertySelector"/> o <paramref name="handler"/>
    /// son <see langword="null"/>.
    /// </exception>
    protected void ObserveFrom<T>(T source, Expression<Func<T, object?>> propertySelector, Action handler) where T : notnull, INotifyPropertyChanged
    {
        Observe_Contract(propertySelector);
        ObserveFrom(source, ReflectionHelpers.GetProperty(propertySelector), handler);
    }

    /// <summary>
    /// Registra una propiedad con notificación de cambio de valor para ser
    /// observada y manejada por el delegado especificado.
    /// </summary>
    /// <param name="source">Origen observado.</param>
    /// <param name="property">Propiedad a observar.</param>
    /// <param name="handler">
    /// Delegado a invocar cuando la propiedad haya cambiado.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="source"/>,
    /// <paramref name="property"/> o <paramref name="handler"/> son 
    /// <see langword="null"/>.
    /// </exception>
    /// <exception cref="MissingMemberException">
    /// Se produce si la propiedad no ha sido encontrada en la instancia a
    /// configurar.
    /// </exception>
    protected void ObserveFrom(INotifyPropertyChanged source, PropertyInfo property, Action handler)
    {
        Observe_Contract(source, property);
        Observe(property.Name, handler);
    }

    /// <summary>
    /// Ejecuta una acción controlando automáticamente el estado de
    /// 'ocupado' de este ViewModel.
    /// </summary>
    /// <param name="action">Acción a ejecutar.</param>
    protected void BusyOp(Action action)
    {
        BusyOp_Contract(action);
        IsBusy = true;
        action.Invoke();
        IsBusy = false;
    }

    /// <summary>
    /// Ejecuta una tarea controlando automáticamente el estado de
    /// 'ocupado' de este ViewModel.
    /// </summary>
    /// <param name="task">Tarea a ejecutar.</param>
    /// <returns>
    /// Un <see cref="Task"/> que puede utilizarse para monitorear la
    /// operación asíncrona.
    /// </returns>
    protected async Task BusyOp(Task task)
    {
        BusyOp_Contract(task);
        IsBusy = true;
        await task;
        IsBusy = false;
    }

    /// <summary>
    /// Ejecuta una función controlando automáticamente el estado de
    /// 'ocupado' de este ViewModel
    /// </summary>
    /// <typeparam name="T">Tipo de resultado de la función.</typeparam>
    /// <param name="function">Función a ejecutar.</param>
    /// <returns>
    /// El resultado de ejecutar la función especificada.
    /// </returns>
    protected T BusyOp<T>(Func<T> function)
    {
        BusyOp_Contract(function);
        IsBusy = true;
        T? result = function.Invoke();
        IsBusy = false;
        return result;
    }

    /// <summary>
    /// Ejecuta una tarea que devuelve un resultado controlando
    /// automáticamente el estado de 'ocupado' de este ViewModel.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de resultado devuelto por la tarea.
    /// </typeparam>
    /// <param name="task">Tarea a ejecutar.</param>
    /// <returns>
    /// Un <see cref="Task"/> que puede utilizarse para monitorear la
    /// operación asíncrona.
    /// </returns>
    protected async Task<T> BusyOp<T>(Task<T> task)
    {
        BusyOp_Contract(task);
        IsBusy = true;
        T? result = await task;
        IsBusy = false;
        return result;
    }

    private void OnInvokeObservedProps(object? sender, PropertyChangedEventArgs e)
    {
        OnInvokeObservedProps_Contract(sender, e);
        if (_observeRegistry.TryGetValue(e.PropertyName!, out HashSet<Action>? c))
        {
            foreach (Action? j in c!)
            {
                j.Invoke();
            }
        }
    }

    /// <summary>
    /// Destruye esta instancia de la clase <see cref="ViewModelBase"/>.
    /// </summary>
    ~ViewModelBase()
    {
        PropertyChanged -= OnInvokeObservedProps;
    }
}
