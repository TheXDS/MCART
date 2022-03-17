/*
ValidationSource.cs

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
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.Types.Extensions;

/// <summary>
/// Ejecuta validaciones de datos dentro de un 
/// <see cref="IValidatingViewModel"/>.
/// </summary>
public abstract class ValidationSource : INotifyDataErrorInfo
{
    private protected interface IValidationEntry
    {
        IEnumerable<string> Check(object? value);
        PropertyInfo Property { get; }
    }
    private protected class ValidationEntry<T> : IValidationEntry, IValidationEntry<T>
    {
        private class ValidationRule
        {
            public readonly Func<T, bool> Rule;
            public readonly string Error;
            public ValidationRule(Func<T, bool> rule, string error)
            {
                Rule = rule;
                Error = error;
            }
        }

        private readonly List<ValidationRule> _rules = new();

        public PropertyInfo Property { get; }

        internal ValidationEntry(PropertyInfo property)
        {
            Property = property;
        }

        IValidationEntry<T> IValidationEntry<T>.AddRule(Func<T, bool> rule, string error)
        {
            _rules.Add(new ValidationRule(rule, error));
            return this;
        }

        IEnumerable<string> IValidationEntry.Check(object? value)
        {
            return _rules.Where(p => !p.Rule((T)value!)).Select(p => p.Error).NotNull();
        }
    }

    private readonly IValidatingViewModel _npcSource;
    private readonly IDictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
    private protected readonly List<IValidationEntry> _validationRules = new();

    /// <inheritdoc/>
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    /// <inheritdoc/>
    public bool HasErrors => _errors.Any();

    /// <summary>
    /// Obtiene una colección de los errores de validación para la
    /// propiedad especificada.
    /// </summary>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public IEnumerable<string> this[string propertyName] => _errors.TryGetValue(propertyName, out List<string>? l) ? l.ToArray() : Array.Empty<string>();

    private protected ValidationSource(IValidatingViewModel npcSource)
    {
        (_npcSource = npcSource).PropertyChanged += NpcSource_PropertyChanged;
    }

    /// <summary>
    /// Ejecuta una comprobación de errores de validación.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> si todas las validaciones han sido exitosas,
    /// <see langword="false"/> en caso contrario.
    /// </returns>
    public bool CheckErrors()
    {
        foreach (IValidationEntry? j in _validationRules)
        {
            AppendErrors(j, j.Property.GetValue(this));
        }
        return !HasErrors && _validationRules.Any();
    }

    /// <summary>
    /// Enumera los errores de validación para la propiedad
    /// <paramref name="propertyName"/>, o todos los errores de validación.
    /// </summary>
    /// <param name="propertyName">
    /// Nombre de la propiedad para la cual obtener los errores de
    /// validación. Si es una cadena vacía o <see langword="null"/>, se
    /// devolverán los errores de validación de todas las propiedades.
    /// </param>
    /// <returns>
    /// Una enumeración con todos los errores de validación de la propiedad
    /// o de la instancia.
    /// </returns>
    public IEnumerable GetErrors(string? propertyName)
    {
        return propertyName.IsEmpty() ? _errors.SelectMany(p => p.Value) : this[propertyName];
    }

    /// <summary>
    /// Enumera los errores de validación para todas las propiedades
    /// configuradas del ViewModel observado.
    /// </summary>
    /// <returns>
    /// Una coleccion con todos los errores de validación para el ViewModel
    /// observado.
    /// </returns>
    public IEnumerable<string> GetErrors() => GetErrors(null).Cast<string>();

    private void NpcSource_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        PropertyInfo? prop = _npcSource.GetType().GetProperty(e.PropertyName ?? throw new ArgumentException(null, nameof(e)))
            ?? throw new MissingMemberException(_npcSource.GetType().Name, e.PropertyName);
        if (_validationRules.FirstOrDefault(p => p.Property == prop) is { } vr)
        {
            AppendErrors(vr, prop.GetValue(_npcSource));
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(e.PropertyName));
        }
    }
    private void AppendErrors(IValidationEntry entry, object? value)
    {
        _errors.Remove(entry.Property.Name);
        foreach (string? j in entry.Check(value) ?? Array.Empty<string>())
        {
            if (_errors.TryGetValue(entry.Property.Name, out List<string>? l))
            {
                l.Add(j);
            }
            else
            {
                _errors.Add(entry.Property.Name, new List<string> { j });
            }
        }
    }
}
