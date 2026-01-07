/*
ValidationSource_T.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2026 César Andrés Morgan

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

using System.Linq.Expressions;
using TheXDS.MCART.Helpers;

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Executes data validations within an <see cref="IValidatingViewModel"/>.
/// </summary>
/// <typeparam name="T">
/// The ViewModel type whose data will be validated by this instance.
/// </typeparam>
/// <param name="npcSource">
/// Instance that is the source of validation data.
/// </param>
public sealed class ValidationSource<T>(T npcSource) : ValidationSource(npcSource) where T : IValidatingViewModel
{
    /// <summary>
    /// Registers a set of rules in the validation instance.
    /// </summary>
    /// <typeparam name="TValue">Type of the property.</typeparam>
    /// <param name="propertySelector">
    /// Expression that selects the property to configure.
    /// </param>
    /// <returns>
    /// The same validation-instance to allow Fluent syntax.
    /// </returns>
    public IValidationEntry<TValue> RegisterValidation<TValue>(Expression<Func<T, TValue>> propertySelector)
    {
        ValidationEntry<TValue>? r = new(ReflectionHelpers.GetProperty(propertySelector));
        _validationRules.Add(r);
        return r;
    }

    /// <summary>
    /// Enumerates validation errors for the specified property.
    /// </summary>
    /// <param name="propertySelector">
    /// Property for which to obtain validation errors.
    /// </param>
    /// <returns>
    /// An enumeration with all validation errors for the selected
    /// property.
    /// </returns>
    public IEnumerable<string> GetErrors(Expression<Func<T, object?>> propertySelector)
    {
        return base[ReflectionHelpers.GetProperty(propertySelector).Name];
    }
}
