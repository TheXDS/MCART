/*
ValidationSource.cs

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

using System.Collections;
using System.ComponentModel;
using System.Reflection;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Executes data validations within an <see cref="IValidatingViewModel"/>.
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
        private class ValidationRule(Func<T, bool?> rule, string error)
        {
            public readonly Func<T, bool?> Rule = rule;
            public readonly string Error = error;
        }

        private readonly List<ValidationRule> _rules = [];

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
    private readonly Dictionary<string, List<string>> _errors = [];
    private protected readonly List<IValidationEntry> _validationRules = [];

    /// <inheritdoc/>
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    /// <inheritdoc/>
    public bool HasErrors => _errors.Count != 0;

    /// <summary>
    /// Indicates whether the object under validation has passed all
    /// validation checks.
    /// </summary>
    public bool PassesValidation => !HasErrors;

    /// <summary>
    /// Gets a collection of validation error messages for the specified
    /// property.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the property to retrieve error messages for.
    /// </param>
    /// <returns>
    /// An enumeration of error messages for the specified property, or an
    /// empty enumeration if the property has no errors.
    /// </returns>
    public IEnumerable<string> this[string propertyName] => _errors.TryGetValue(propertyName, out List<string>? l) ? l.ToArray() : [];

    private protected ValidationSource(IValidatingViewModel npcSource)
    {
        (_npcSource = npcSource).PropertyChanged += NpcSource_PropertyChanged;
    }

    /// <summary>
    /// Executes validation checks for all configured validation entries.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if all validations passed; <see
    /// langword="false"/> otherwise.
    /// </returns>
    public bool CheckErrors()
    {
        foreach (IValidationEntry? j in _validationRules)
        {
            AppendErrors(j, j.Property.GetValue(_npcSource));
        }
        return _validationRules.Count == 0 || !HasErrors;
    }

    /// <summary>
    /// Enumerates validation errors for the specified property or for all
    /// properties.
    /// </summary>
    /// <param name="propertyName">
    /// Property name to get validation errors for. If null or empty, errors
    /// for all properties are returned.
    /// </param>
    /// <returns>
    /// An enumeration with all validation errors for the property or the
    /// instance.
    /// </returns>
    public IEnumerable GetErrors(string? propertyName)
    {
        return propertyName.IsEmpty() ? _errors.SelectMany(p => p.Value) : this[propertyName];
    }

    /// <summary>
    /// Enumerates validation error messages for all configured properties
    /// of the observed ViewModel.
    /// </summary>
    /// <returns>
    /// A collection with all validation error messages for the observed
    /// ViewModel.
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
            { 
                l.Add(j);
            }
            else
            {
                _errors.Add(entry.Property.Name, [j]);
            }
        }
    }
}
