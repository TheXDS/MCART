/*
ConfiguredObservingCommand_T.cs

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

namespace TheXDS.MCART.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheXDS.MCART.Math;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ViewModel;
using static TheXDS.MCART.Helpers.ReflectionHelpers;

/// <summary>
/// Clase envolvente que permite configurar y generar un 
/// <see cref="ObservingCommand"/>.
/// </summary>
/// <typeparam name="T">Tipo de objeto observado.</typeparam>
public partial class ConfiguredObservingCommand<T> where T : INotifyPropertyChanged
{
    private readonly ObservingCommand command;
    private readonly T observedObject;
    private readonly List<Func<object?, bool>> canExecuteTree = new();

    /// <summary>
    /// Obtiene un valor que indica si ya se ha terminado de definir el
    /// <see cref="ObservingCommand"/> a obtener por medio de esta
    /// instancia.
    /// </summary>
    public bool IsBuilt { get; private set; }

    internal ConfiguredObservingCommand(T observedObject, Action action)
    {
        command = new (this.observedObject = observedObject, action);
    }

    internal ConfiguredObservingCommand(T observedObject, Action<object?> action)
    {
        command = new(this.observedObject = observedObject, action);
    }

    internal ConfiguredObservingCommand(T observedObject, Func<Task> action)
    {
        command = new(this.observedObject = observedObject, action);
    }

    internal ConfiguredObservingCommand(T observedObject, Func<object?, Task> action)
    {
        command = new(this.observedObject = observedObject, action);
    }

    /// <summary>
    /// Indica que el <see cref="ObservingCommand"/> a configurar escuchará
    /// los cambios de las propiedades especificadas.
    /// </summary>
    /// <param name="properties">
    /// Colección de expresiones selectoras que permiten especificar las
    /// propiedades a escuchar.
    /// </param>
    /// <returns>
    /// Esta instancia de la clase
    /// <see cref="ConfiguredObservingCommand{T}"/>, permitiendo el uso de
    /// sintaxis Fluent.
    /// </returns>
    public ConfiguredObservingCommand<T> ListensTo(params Expression<Func<T, object?>>[] properties)
    {
        ListensTo<object?>(properties);
        return this;
    }

    /// <summary>
    /// Indica que el <see cref="ObservingCommand"/> a configurar escuchará
    /// los cambios de las propiedades especificadas.
    /// </summary>
    /// <param name="properties">
    /// Colección de expresiones selectoras que permiten especificar las
    /// propiedades a escuchar.
    /// </param>
    /// <returns>
    /// Esta instancia de la clase
    /// <see cref="ConfiguredObservingCommand{T}"/>, permitiendo el uso de
    /// sintaxis Fluent.
    /// </returns>
    public ConfiguredObservingCommand<T> ListensTo<TValue>(params Expression<Func<T, TValue>>[] properties)
    {
        ListensToProperty_Contract(properties);
        command.RegisterObservedProperty(properties.Select(GetProperty).Select(p => p.Name).ToArray());
        return this;
    }

    /// <summary>
    /// Indica que el <see cref="ObservingCommand"/> a configurar deberá
    /// escuchar a una propiedad de tipo <see cref="bool"/> y establecer la
    /// misma como el determinante de si el comando puede o no ser
    /// ejecutado.
    /// </summary>
    /// <param name="selector">
    /// Función selectora de la propiedad a utilizar.
    /// </param>
    /// <returns>
    /// Esta instancia de la clase
    /// <see cref="ConfiguredObservingCommand{T}"/>, permitiendo el uso de
    /// sintaxis Fluent.
    /// </returns>
    public ConfiguredObservingCommand<T> ListensToCanExecute(Expression<Func<T, bool>> selector)
    {
        var pi = GetProperty(selector);
        ListensToCanExecute_Contract(pi, typeof(T));
        command.RegisterObservedProperty(pi.Name);
        canExecuteTree.Add(_ => selector.Compile().Invoke(observedObject));
        return this;
    }

    /// <summary>
    /// Establece explícitamente la función de comprobación que determina
    /// si el <see cref="ObservingCommand"/> a configurar puede ser 
    /// ejecutada.
    /// </summary>
    /// <param name="canExecute">
    /// Función que determina so el comando puede ser ejecutado. La función
    /// admitirá un parámetro proporcionado por el enlace de datos.
    /// </param>
    /// <returns>
    /// Esta instancia de la clase
    /// <see cref="ConfiguredObservingCommand{T}"/>, permitiendo el uso de
    /// sintaxis Fluent.
    /// </returns>
    public ConfiguredObservingCommand<T> CanExecute(Func<object?, bool> canExecute)
    {
        IsBuilt_Contract();
        canExecuteTree.Add(canExecute);
        return this;
    }

    /// <summary>
    /// Establece explícitamente la función de comprobación que determina
    /// si el <see cref="ObservingCommand"/> a configurar puede ser 
    /// ejecutada.
    /// </summary>
    /// <param name="canExecute">
    /// Función que determina so el comando puede ser ejecutado.
    /// </param>
    /// <returns>
    /// Esta instancia de la clase
    /// <see cref="ConfiguredObservingCommand{T}"/>, permitiendo el uso de
    /// sintaxis Fluent.
    /// </returns>
    public ConfiguredObservingCommand<T> CanExecute(Func<bool> canExecute)
    {
        IsBuilt_Contract();
        canExecuteTree.Add(_ => canExecute());
        return this;
    }

    /// <summary>
    /// Configura el <see cref="ObservingCommand"/> para poder ejecutarse
    /// cuando las propiedades especificadas no sean igual a
    /// <see langword="null"/>.
    /// </summary>
    /// <param name="properties">
    /// Colección de expresiones selectoras que permiten especificar las
    /// propiedades a escuchar.
    /// </param>
    /// <returns>
    /// Esta instancia de la clase
    /// <see cref="ConfiguredObservingCommand{T}"/>, permitiendo el uso de
    /// sintaxis Fluent.
    /// </returns>
    public ConfiguredObservingCommand<T> CanExecuteIfNotNull(params Expression<Func<T, object?>>[] properties)
    {
        return CanExecuteIf(p => p is not null, properties);
    }

    /// <summary>
    /// Configura el <see cref="ObservingCommand"/> para poder ejecutarse
    /// cuando las propiedades especificadas no sean igual a
    /// <see langword="null"/> o a su valor predeterminado en caso de ser
    /// estructuras.
    /// </summary>
    /// <param name="properties">
    /// Colección de expresiones selectoras que permiten especificar las
    /// propiedades a escuchar.
    /// </param>
    /// <returns>
    /// Esta instancia de la clase
    /// <see cref="ConfiguredObservingCommand{T}"/>, permitiendo el uso de
    /// sintaxis Fluent.
    /// </returns>
    public ConfiguredObservingCommand<T> CanExecuteIfNotDefault(params Expression<Func<T, object?>>[] properties)
    {
        return CanExecuteIf(p => p is not null && p != p.GetType().Default(), properties);
    }

    /// <summary>
    /// Configura el <see cref="ObservingCommand"/> para poder ejecutarse
    /// cuando las propiedades especificadas contengan valores distintos
    /// del valor predeterminado para el tipo de cada una.
    /// </summary>
    /// <param name="properties">
    /// Colección de expresiones selectoras que permiten especificar las
    /// propiedades a escuchar.
    /// </param>
    /// <returns>
    /// Esta instancia de la clase
    /// <see cref="ConfiguredObservingCommand{T}"/>, permitiendo el uso de
    /// sintaxis Fluent.
    /// </returns>
    public ConfiguredObservingCommand<T> CanExecuteIfFilled(params Expression<Func<T, string?>>[] properties)
    {
        return CanExecuteIf(p => !p.IsEmpty(), properties);
    }

    /// <summary>
    /// Configura el <see cref="ObservingCommand"/> para poder ejecutarse
    /// cuando las propiedades especificadas tengan valores de punto
    /// flotante válidos, es decir, que no sean 
    /// <see cref="float.NegativeInfinity"/>,
    /// <see cref="float.PositiveInfinity"/> o <see cref="float.NaN"/>.
    /// </summary>
    /// <param name="properties">
    /// Colección de expresiones selectoras que permiten especificar las
    /// propiedades a escuchar.
    /// </param>
    /// <returns>
    /// Esta instancia de la clase
    /// <see cref="ConfiguredObservingCommand{T}"/>, permitiendo el uso de
    /// sintaxis Fluent.
    /// </returns>
    public ConfiguredObservingCommand<T> CanExecuteIfValid(params Expression<Func<T, float>>[] properties)
    {
        return CanExecuteIf(p => p.IsValid(), properties);
    }

    /// <summary>
    /// Configura el <see cref="ObservingCommand"/> para poder ejecutarse
    /// cuando las propiedades especificadas tengan valores de punto
    /// flotante válidos, es decir, que no sean 
    /// <see cref="float.NegativeInfinity"/>,
    /// <see cref="float.PositiveInfinity"/>, <see cref="float.NaN"/> o
    /// <see langword="null"/>.
    /// </summary>
    /// <param name="properties">
    /// Colección de expresiones selectoras que permiten especificar las
    /// propiedades a escuchar.
    /// </param>
    /// <returns>
    /// Esta instancia de la clase
    /// <see cref="ConfiguredObservingCommand{T}"/>, permitiendo el uso de
    /// sintaxis Fluent.
    /// </returns>
    public ConfiguredObservingCommand<T> CanExecuteIfValid(params Expression<Func<T, float?>>[] properties)
    {
        return CanExecuteIf(p => p.HasValue && p.Value.IsValid(), properties);
    }

    /// <summary>
    /// Configura el <see cref="ObservingCommand"/> para poder ejecutarse
    /// cuando las propiedades especificadas tengan valores de punto
    /// flotante válidos, es decir, que no sean 
    /// <see cref="double.NegativeInfinity"/>,
    /// <see cref="double.PositiveInfinity"/> o <see cref="double.NaN"/>.
    /// </summary>
    /// <param name="properties">
    /// Colección de expresiones selectoras que permiten especificar las
    /// propiedades a escuchar.
    /// </param>
    /// <returns>
    /// Esta instancia de la clase
    /// <see cref="ConfiguredObservingCommand{T}"/>, permitiendo el uso de
    /// sintaxis Fluent.
    /// </returns>
    public ConfiguredObservingCommand<T> CanExecuteIfValid(params Expression<Func<T, double>>[] properties)
    {
        return CanExecuteIf(p => p.IsValid(), properties);
    }

    /// <summary>
    /// Configura el <see cref="ObservingCommand"/> para poder ejecutarse
    /// cuando las propiedades especificadas tengan valores de punto
    /// flotante válidos, es decir, que no sean 
    /// <see cref="double.NegativeInfinity"/>,
    /// <see cref="double.PositiveInfinity"/>, <see cref="double.NaN"/> o
    /// <see langword="null"/>.
    /// </summary>
    /// <param name="properties">
    /// Colección de expresiones selectoras que permiten especificar las
    /// propiedades a escuchar.
    /// </param>
    /// <returns>
    /// Esta instancia de la clase
    /// <see cref="ConfiguredObservingCommand{T}"/>, permitiendo el uso de
    /// sintaxis Fluent.
    /// </returns>
    public ConfiguredObservingCommand<T> CanExecuteIfValid(params Expression<Func<T, double?>>[] properties)
    {
        return CanExecuteIf(p => p.HasValue && p.Value.IsValid(), properties);
    }

    /// <summary>
    /// Configura el <see cref="ObservingCommand"/> para poder ejecutarse
    /// cuando las propiedades especificadas tengan valores distintos de su
    /// valor predeterminado ordinal.
    /// </summary>
    /// <typeparam name="TValue">Tipo de valores.</typeparam>
    /// <param name="properties">
    /// Colección de expresiones selectoras que permiten especificar las
    /// propiedades a escuchar.
    /// </param>
    /// <returns>
    /// Esta instancia de la clase
    /// <see cref="ConfiguredObservingCommand{T}"/>, permitiendo el uso de
    /// sintaxis Fluent.
    /// </returns>
    public ConfiguredObservingCommand<T> CanExecuteIfNotZero<TValue>(params Expression<Func<T, TValue>>[] properties) where TValue : notnull, IComparable<TValue>
    {
        return CanExecuteIf(p => p.CompareTo((TValue)p.GetType().Default()!) != 0, properties);
    }

    /// <summary>
    /// Configura el <see cref="ObservingCommand"/> para poder ejecutarse
    /// cuando todas las propiedades que admiten lectura y escritura del
    /// objeto observado tengan valores distintos a su respectivo valor
    /// predeterminado.
    /// </summary>
    /// <returns>
    /// Esta instancia de la clase
    /// <see cref="ConfiguredObservingCommand{T}"/>, permitiendo el uso de
    /// sintaxis Fluent.
    /// </returns>
    public ConfiguredObservingCommand<T> CanExecuteIfObservedIsFilled()
    {
        static bool IsFilled(object? p)
        {
            return p switch
            {
                null => false,
                string s => !s.IsEmpty(),
                ValueType v => v != p.GetType().Default(),                    
                _ => true
            };
        }
        IsBuilt_Contract();            
        var props = observedObject.GetType().GetProperties().Where(p => p.CanRead && p.CanWrite);
        command.RegisterObservedProperty(props.Select(p => p.Name).ToArray());
        canExecuteTree.Add(_ => props.Select(p => p.GetValue(observedObject)).All(IsFilled));
        return this;
    }

    /// <summary>
    /// Finaliza la configuración del <see cref="ObservingCommand"/>
    /// subyacente, devolviéndolo.
    /// </summary>
    /// <returns>
    /// El <see cref="ObservingCommand"/> que ha sido configurado por medio
    /// de esta instancia.
    /// </returns>
    public ObservingCommand Build()
    {
        if (IsBuilt) return command;
        IsBuilt = true;
        return command.SetCanExecute(o => canExecuteTree.All(p => p(o)));            
    }

    private ConfiguredObservingCommand<T> CanExecuteIf<TValue>(Func<TValue, bool> predicate, params Expression<Func<T, TValue>>[] properties)
    {
        if (IsBuilt) throw new InvalidOperationException();
        ListensTo(properties);
        CanExecute(_ => properties.Select(p => p.Compile().Invoke(observedObject)).All(predicate));
        return this;
    }

    /// <summary>
    /// Convierte implícitamente esta instancia en un
    /// <see cref="ObservingCommand"/>.
    /// </summary>
    /// <param name="command">Objeto a convertir.</param>
    public static implicit operator ObservingCommand (ConfiguredObservingCommand<T> command) => command.Build();
}
