/*
TypeExpression.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

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

using System.Diagnostics.CodeAnalysis;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types;

/// <summary>
/// Represents an expression that can be resolved to a defined type.
/// </summary>
/// <param name="fullName">Full name of the type this expression references.</param>
public class TypeExpression(string fullName)
{
    private readonly string _fullName = fullName;
    private readonly List<TypeExpression> _genericArgs = [];

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="TypeExpression"/>, using the specified type as
    /// base.
    /// </summary>
    /// <param name="type">
    /// Type from which to generate the <see cref="TypeExpression"/>.
    /// </param>
    public TypeExpression(Type type)
        : this(type.FullName ?? type.Name)
    {
        _genericArgs.AddRange(type.GenericTypeArguments.Select(t => new TypeExpression(t)));
    }

    /// <summary>
    /// Implicitly converts a <see cref="TypeExpression"/> to a 
    /// <see cref="Type"/>.
    /// </summary>
    /// <param name="expression">Object to convert.</param>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCreatesNewTypes)]
    public static implicit operator Type(TypeExpression expression) => expression.Resolve();

    /// <summary>
    /// Gets the full name of the type.
    /// </summary>
    public string FullName => _fullName;

    /// <summary>
    /// Gets the name of the type.
    /// </summary>
    public string Name => _fullName.Split(".").Last();

    /// <summary>
    /// Gets the namespace of the type.
    /// </summary>
    public string Namespace => _fullName.ChopEnd($".{Name}");

    /// <summary>
    /// Gets a collection through which generic type arguments can be
    /// retrieved and set when resolving a type.
    /// </summary>
    public ICollection<TypeExpression> GenericArgs => _genericArgs;

    /// <summary>
    /// Resolves a type from this expression.
    /// </summary>
    /// <param name="throwOnFail">
    /// <see langword="true"/> to throw an exception if it is not possible
    /// to resolve the type expression represented by this instance,
    /// <see langword="false"/> to return <see langword="null"/> instead.
    /// </param>
    /// <returns>
    /// A resolved type from this expression, or <see langword="null"/>
    /// if the expression cannot be resolved if <paramref name="throwOnFail"/>
    /// is set to <see langword="false"/>.
    /// </returns>
    /// <exception cref="MissingTypeException">
    /// Thrown if it was not possible to resolve the type from the
    /// expression represented by this instance if
    /// <paramref name="throwOnFail"/> is set to <see langword="true"/>.
    /// </exception>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCreatesNewTypes)]
    public Type? Resolve(bool throwOnFail)
    {
        if (_genericArgs.Count != 0 && SearchTypeByName($"{_fullName}`{_genericArgs.Count}") is Type tg)
        {
            var g = _genericArgs.Select(p => p.Resolve(throwOnFail));
            if (!g.IsAnyNull())
                return tg.MakeGenericType([.. g.NotNull()]);
        }
        else if (_genericArgs.Count == 0 && SearchTypeByName(_fullName) is Type t)
        {
            return t;
        }
        return throwOnFail ? throw new MissingTypeException() : null;
    }

    /// <summary>
    /// Resolves a type from this expression.
    /// </summary>
    /// <returns>A resolved type from this expression.</returns>
    /// <exception cref="MissingTypeException">
    /// Thrown if it was not possible to resolve the type from the
    /// expression represented by this instance.
    /// </exception>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCreatesNewTypes)]
    public Type Resolve()
    {
        return Resolve(true)!;
    }

    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    private static Type? SearchTypeByName(string name)
    {
        return ReflectionHelpers.GetTypes<object>().NotNull().FirstOrDefault(p => p.FullName == name);
    }
}
