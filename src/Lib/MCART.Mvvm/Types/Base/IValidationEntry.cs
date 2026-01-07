/*
IValidationEntry.cs

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

namespace TheXDS.MCART.Types.Base;

/// <summary>
/// Defines members to be implemented by a type that allows configuring
/// validation rules for a property.
/// </summary>
/// <typeparam name="T">Type of the selected property.</typeparam>
public interface IValidationEntry<T>
{
    /// <summary>
    /// Adds a validation rule for the selected property.
    /// </summary>
    /// <param name="rule">
    /// Function that performs the validation. The function must return
    /// <see langword="true"/> if the value passes the test,
    /// <see langword="false"/> otherwise. If the evaluator returns
    /// <see langword="null"/>, evaluation of any remaining rules that
    /// have not yet run will stop.
    /// </param>
    /// <param name="error">
    /// Error message to display if the rule fails.
    /// </param>
    /// <returns>
    /// The same validation-entry instance to allow Fluent syntax.
    /// </returns>
    IValidationEntry<T> AddRule(Func<T, bool?> rule, string error);

    /// <summary>
    /// Adds a validation rule for the selected property.
    /// </summary>
    /// <param name="rule">
    /// Function that performs the validation. The function must return
    /// <see langword="true"/> if the value passes the test,
    /// <see langword="false"/> otherwise.
    /// </param>
    /// <param name="error">
    /// Error message to display if the rule fails.
    /// </param>
    /// <returns>
    /// The same validation-entry instance to allow Fluent syntax.
    /// </returns>
    IValidationEntry<T> AddRule(Func<T, bool> rule, string error) => AddRule(p => (bool?)rule(p), error);
}
