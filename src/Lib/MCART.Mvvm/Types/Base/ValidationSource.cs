/*
ValidationSource.cs

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

using System.Collections;
using System.ComponentModel;
using System.Reflection;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types.Base;

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
            public readonly Func<T, bool?> Rule;
            public readonly string Error;
            public ValidationRule(Func<T, bool?> rule, string error)
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

        IValidationEntry<T> IValidationEntry<T>.AddRule(Func<T, bool?> rule, string error)
        {
            _rules.Add(new ValidationRule(rule, error));
            return this;
        }

        IEnumerable<string> IValidationEntry.Check(object? value)
        {
            foreach (var rule in _rules)
            {
                switch (rule.Rule((T)value!))
                {
                    case false:
                        yield return rule.Error;
                        break;
                    case null:
                        yield return rule.Error;
                        yield break;
                }
            }
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
    /// Indica si el objeto a comprobar ha pasado todas las pruebas de validación.
    /// </summary>
    public bool PassesValidation => !HasErrors;

    /// <summary>
    /// Obtiene una colección de los errores de validación para la
    /// propiedad especificada.
    /// </summary>
    /// <param name="propertyName">
    /// Nombre de la propiedad para la cual se deben obtener los mensajes de
    /// error.
    /// </param>
    /// <returns>
    /// Una enumeración de los mensajes de error para la propiedad
    /// especificada, o una enumeración vacía si la propiedad no contiene
    /// errores.
    /// </returns>
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
            AppendErrors(j, j.Property.GetValue(_npcSource));
        }
        return !_validationRules.Any() || !HasErrors;
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
    /// Una colección con todos los errores de validación para el ViewModel
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
        foreach (string? j in entry.Check(value))
        {
            if (_errors.TryGetValue(entry.Property.Name, out List<string>? l))
                l.Add(j);
            else
            {
                _errors.Add(entry.Property.Name, new List<string> { j });
            }
        }
    }
}
