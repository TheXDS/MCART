/*
FormViewModelBase.cs

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
using System.Linq.Expressions;
using System.Reflection;
using TheXDS.MCART.Component;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;
using St = TheXDS.MCART.Resources.Strings.MvvmErrors;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Base class for view models that provide form services and data
/// validation.
/// </summary>
public abstract class FormViewModelBase : ViewModelBase, INotifyDataErrorInfo
{
    private interface IValidationEntry
    {
        IEnumerable<string> Check(object? value);
        PropertyInfo Property { get; }
    }

    /// <summary>
    /// Defines a set of members available to configure a validation
    /// rule.
    /// </summary>
    /// <typeparam name="T">Property type.</typeparam>
    protected interface IValidationEntry<T>
    {
        /// <summary>
        /// Adds a validation rule to the entry.
        /// </summary>
        /// <param name="rule">
        /// Function that performs validation. Must return
        /// <see langword="true"/> if the value passes the test,
        /// <see langword="false"/> otherwise.
        /// </param>
        /// <param name="error">Error message to present if validation fails.</param>
        /// <returns>This same entry to allow fluent chaining.</returns>
        IValidationEntry<T> AddRule(Func<T, bool> rule, string error);

        /// <summary>
        /// Adds a validation rule to the entry.
        /// </summary>
        /// <param name="rule">
        /// Function that performs validation. Must return
        /// <see langword="true"/> if the value passes the test,
        /// <see langword="false"/> otherwise.
        /// </param>
        /// <returns>This same entry to allow fluent chaining.</returns>
        IValidationEntry<T> AddRule(Func<T, bool> rule);
    }

    private class ValidationEntry<T> : IValidationEntry, IValidationEntry<T>
    {
        private record ValidationRule(Func<T, bool> Rule, string Error);

        private readonly List<ValidationRule> _rules = [];

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

        IValidationEntry<T> IValidationEntry<T>.AddRule(Func<T, bool> rule)
        {
            return ((IValidationEntry<T>)this).AddRule(rule, St.FieldRequired);
        }

        IEnumerable<string> IValidationEntry.Check(object? value)
        {
            return _rules.Where(p => !p.Rule(value is T v ? v : default!)).Select(p => p.Error);
        }
    }

    private readonly Dictionary<string, List<string>> _errors = [];
    private readonly List<IValidationEntry> _validationRules = [];
    private SimpleCommand[]? _validationAffectedCommands;

    /// <summary>
    /// Triggered whenever validations are run.
    /// </summary>
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    /// <summary>
    /// Returns this instance; required for proper
    /// <see cref="INotifyPropertyChanged"/> implementation.
    /// </summary>
    public INotifyDataErrorInfo ErrorSource => this;

    /// <summary>
    /// Gets a value indicating whether this instance has any validation
    /// errors.
    /// </summary>
    public bool HasErrors => _errors.Count != 0;

    /// <summary>
    /// Runs all validations and returns whether they all passed.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if all validations passed and there is at
    /// least one rule; otherwise, <see langword="false"/>.
    /// </returns>
    public bool CheckErrors()
    {
        return _validationRules.Locked(p =>
        {
            foreach (IValidationEntry? j in p)
            {
                AppendErrors(j, j.Property.GetValue(this));
            }
            var pass = !HasErrors && p.Count != 0;
            SetAffectedCommands(pass);
            return pass;
        });
    }

    /// <summary>
    /// Enumerates the errors for the provided property name, or for all
    /// properties if <paramref name="propertyName"/> is
    /// <see langword="null"/> or empty.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the property for which to get the errors. If it is an empty
    /// string or <see langword="null"/>, this method returns all validation
    /// errors.
    /// </param>
    /// <returns>
    /// An enumeration of all validation error messages for the specified
    /// property or for all properties.
    /// </returns>
    public IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName.IsEmpty())
        {
            return _errors.SelectMany(p => p.Value);
        }
        if (_errors.TryGetValue(propertyName, out List<string>? value) && value.Count != 0)
        {
            return value;
        }
        return Array.Empty<string>();
    }

    /// <summary>
    /// Overrides <see cref="NotifyPropertyChangeBase.Change{T}(ref T, T, string)"/>
    /// to run validations for a property when its value changes.
    /// </summary>
    /// <typeparam name="T">Type of the backing field.</typeparam>
    /// <param name="backingStore">Field that holds the property value.</param>
    /// <param name="value">Value to set.</param>
    /// <param name="propertyName">
    /// Name of the property. This parameter should normally be omitted
    /// (compiler-inferred) unless a different property must be notified.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the property value changed; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    protected override void OnDoChange<T>(ref T backingStore, T value, string propertyName)
    {
        backingStore = value;
        PropertyInfo? prop = GetType().GetProperty(propertyName);
        IValidationEntry? vr = _validationRules.FirstOrDefault(p => p.Property == prop);
        if (vr is not null)
        {
            AppendErrors(vr, value);
            bool act = GetErrors(propertyName).ToGeneric().Any();
            SetAffectedCommands(act);
        }
    }

    /// <summary>
    /// Registers a validation ruleset for a property.
    /// </summary>
    /// <typeparam name="T">Property type.</typeparam>
    /// <param name="propertySelector">
    /// Expression that selects the property to configure.
    /// </param>
    /// <returns>
    /// An object that allows configuring the validation rules to apply to
    /// the selected property.
    /// </returns>
    protected IValidationEntry<T> RegisterValidation<T>(Expression<Func<T>> propertySelector)
    {
        ValidationEntry<T>? r = new(ReflectionHelpers.GetProperty(propertySelector));
        _validationRules.Add(r);
        return r;
    }

    /// <summary>
    /// Informs the validation engine which commands are affected by
    /// validation failures.
    /// </summary>
    /// <param name="commands">
    /// Commands that should be disabled when validation fails.
    /// </param>
    /// <remarks>
    /// Call this method after instantiating the commands.
    /// </remarks>
    protected void ValidationAffects(params SimpleCommand[] commands)
    {
        _validationAffectedCommands = commands;
    }

    private void AppendErrors(IValidationEntry entry, object? value)
    {
        _errors.Remove(entry.Property.Name);
        foreach (string? j in entry?.Check(value) ?? [])
        {
            if (_errors.TryGetValue(entry!.Property.Name, out List<string>? errors))
            {
                errors.Add(j);
            }
            else
            {
                _errors.Add(entry.Property.Name, [j]);
            }
        }
        RaisePropertyChangeEvent(nameof(HasErrors), PropertyChangeNotificationType.PropertyChanged);
        RaisePropertyChangeEvent(nameof(ErrorSource), PropertyChangeNotificationType.PropertyChanged);
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(entry?.Property.Name));
    }

    private void SetAffectedCommands(bool canExecute)
    {
        foreach (SimpleCommand? j in _validationAffectedCommands ?? []) j.SetCanExecute(canExecute);
    }
}
