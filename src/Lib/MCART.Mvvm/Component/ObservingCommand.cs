/*
ObservingCommand.cs

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
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Base;
using static TheXDS.MCART.Misc.Internals;

namespace TheXDS.MCART.Component;

/// <summary>
/// Describe un comando que observa a un objeto que implemente
/// <see cref="INotifyPropertyChanged" /> y
/// escucha cualquier cambio ocurrido en el valor de sus propiedades
/// para habilitar o deshabilitar automáticamente la ejecución del
/// comando.
/// </summary>
public partial class ObservingCommand : CommandBase
{
    private Func<INotifyPropertyChanged, object?, bool>? _canExecute;
    private readonly HashSet<string> _properties = [];

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ObservingCommand" />.
    /// </summary>
    /// <param name="observedSource">Origen de datos observado.</param>
    /// <param name="action">Acción a ejecutar.</param>
    public ObservingCommand(INotifyPropertyChanged observedSource, Action action) : this(observedSource, _ => action()) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ObservingCommand" />.
    /// </summary>
    /// <param name="observedSource">Origen de datos observado.</param>
    /// <param name="task">Tarea a ejecutar.</param>
    public ObservingCommand(INotifyPropertyChanged observedSource, Func<Task> task) : this(observedSource, (Action<object?>)(async _ => await task())) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ObservingCommand" />.
    /// </summary>
    /// <param name="observedSource">Origen de datos observado.</param>
    /// <param name="task">Tarea a ejecutar.</param>
    public ObservingCommand(INotifyPropertyChanged observedSource, Func<object?, Task> task) : this(observedSource, (Action<object?>)(async o => await task(o))) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="ObservingCommand" />.
    /// </summary>
    /// <param name="observedSource">Origen de datos observado.</param>
    /// <param name="action">Acción a ejecutar.</param>
    public ObservingCommand(INotifyPropertyChanged observedSource, Action<object?> action) : base(action)
    {
        ObservedSource = NullChecked(observedSource);
    }

    /// <summary>
    /// Referencia al origen de datos observado por este <see cref="ObservingCommand"/>.
    /// </summary>
    public INotifyPropertyChanged ObservedSource { get; }

    /// <summary>
    /// Enumera las propiedades que están siendo observadas por este <see cref="ObservingCommand"/>.
    /// </summary>
    public IEnumerable<string> ObservedProperties => [.. _properties];

    /// <summary>
    /// Define el método que determina si el comando puede ejecutarse
    /// en su estado actual.
    /// </summary>
    /// <param name="parameter">
    /// Datos que usa el comando. Si el comando no exige pasar los
    /// datos, se puede establecer este objeto en
    /// <see langword="null" />.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si se puede ejecutar este comando; de
    /// lo contrario, <see langword="false" />.
    /// </returns>
    public override bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke(ObservedSource, parameter) ?? true;
    }

    /// <summary>
    /// Registra una nueva propiedad a observar en este comando.
    /// </summary>
    /// <param name="properties">
    /// Nombre(s) de la(s) propiedad(es) a observar.
    /// </param>
    /// <returns>
    /// Esta misma instancia, lo que permite usar esta función con
    /// sintaxis fluent.
    /// </returns>
    public ObservingCommand RegisterObservedProperty(params string[] properties)
    {
        foreach (string? j in properties.NotEmpty())
        {
            _properties.Add(j);
        }
        return this;
    }

    /// <summary>
    /// Registra una nueva propiedad a observar en este comando.
    /// </summary>
    /// <param name="property">Nombre de la propiedad a observar.</param>
    /// <returns>
    /// Esta misma instancia, lo que permite usar esta función con
    /// sintaxis fluent.
    /// </returns>
    public ObservingCommand RegisterObservedProperty(Expression<Func<object?>> property)
    {
        PropertyInfo? prop = (ReflectionHelpers.GetMember(property) as PropertyInfo) ?? throw new ArgumentException(null, nameof(property));
        RegisterObservedProperty(prop.Name);
        return this;
    }

    /// <summary>
    /// Establece la función de comprobación a ejecutar cuando se desee
    /// saber si es posible ejecutar el comando.
    /// </summary>
    /// <param name="canExecute">
    /// Función a ejecutar para determinar la posibilidad de ejecutar
    /// el comando.
    /// </param>
    /// <returns>
    /// Esta misma instancia, lo que permite usar esta función con
    /// sintaxis fluent.
    /// </returns>
    public ObservingCommand SetCanExecute(Func<bool> canExecute)
    {
        ArgumentNullException.ThrowIfNull(canExecute, nameof(canExecute));
        return SetCanExecute((_, _) => canExecute());
    }

    /// <summary>
    /// Establece la función de comprobación a ejecutar cuando se desee
    /// saber si es posible ejecutar el comando.
    /// </summary>
    /// <param name="canExecute">
    /// Función a ejecutar para determinar la posibilidad de ejecutar
    /// el comando.
    /// </param>
    /// <returns>
    /// Esta misma instancia, lo que permite usar esta función con
    /// sintaxis fluent.
    /// </returns>
    public ObservingCommand SetCanExecute(Func<object?, bool> canExecute)
    {
        ArgumentNullException.ThrowIfNull(canExecute, nameof(canExecute));
        return SetCanExecute((_, o) => canExecute(o));
    }

    /// <summary>
    /// Establece la función de comprobación a ejecutar cuando se desee
    /// saber si es posible ejecutar el comando.
    /// </summary>
    /// <param name="canExecute">
    /// Función a ejecutar para determinar la posibilidad de ejecutar
    /// el comando.
    /// </param>
    /// <returns>
    /// Esta misma instancia, lo que permite usar esta función con
    /// sintaxis fluent.
    /// </returns>
    public ObservingCommand SetCanExecute(Func<INotifyPropertyChanged, object?, bool>? canExecute)
    {
        if (canExecute is null)
        {
            ObservedSource.PropertyChanged -= RaiseCanExecuteChanged;
        }
        else if (_canExecute is null)
        {
            ObservedSource.PropertyChanged += RaiseCanExecuteChanged;
        }
        _canExecute = canExecute;
        return this;
    }

    /// <summary>
    /// Desconecta la función establecida para comprobar la posibilidad
    /// de ejecutar este comando.
    /// </summary>
    public void UnsetCanExecute() => SetCanExecute((Func<INotifyPropertyChanged, object?, bool>?)null);

    private void RaiseCanExecuteChanged(object? sender, PropertyChangedEventArgs e)
    {
        RaiseCanExecuteChanged_Contract(sender, e);
        if (_properties.Contains(e.PropertyName!)) RaiseCanExecuteChanged();
    }
}
