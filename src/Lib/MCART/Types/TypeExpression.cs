/*
TypeExpressionTree.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo contiene funciones de manipulación de objetos, 

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using System;
using System.Collections.Generic;
using System.Linq;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types;

/// <summary>
/// Representa una expresión que puede resolverse a un tipo definido.
/// </summary>
public class TypeExpression
{
    private readonly string _fullName;
    private readonly List<TypeExpression> _genericArgs = new();

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="TypeExpression"/>.
    /// </summary>
    /// <param name="fullName">Nombre completo del tipo al que esta expresión hace referencia.</param>
    public TypeExpression(string fullName)
    {
        _fullName = fullName;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="TypeExpression"/>, utilizando el tipo especificado como
    /// base.
    /// </summary>
    /// <param name="type">
    /// Tipo a partir del cual generar el <see cref="TypeExpression"/>.
    /// </param>
    public TypeExpression(Type type)
        : this(type.FullName ?? type.Name)
    {
        _genericArgs.AddRange(type.GenericTypeArguments.Select(t => new TypeExpression(t)));
    }

    /// <summary>
    /// Convierte implícitamente un <see cref="TypeExpression"/> en un 
    /// <see cref="Type"/>.
    /// </summary>
    /// <param name="expression">Objeto a convertir.</param>
    public static implicit operator Type(TypeExpression expression) => expression.Resolve();

    /// <summary>
    /// Obtiene el nombre completo del tipo.
    /// </summary>
    public string FullName => _fullName;

    /// <summary>
    /// Obtiene el nombre del tipo.
    /// </summary>
    public string Name => _fullName.Split(".").Last();

    /// <summary>
    /// Obtiene el espacio de nombres del tipo.
    /// </summary>
    public string Namespace => _fullName.ChopEnd($".{Name}");

    /// <summary>
    /// Obtiene una colección por medio de la cual se pueden obtener y
    /// establecer los argumentos de tipo a incluir al tratar de resolver un
    /// tipo.
    /// </summary>
    public ICollection<TypeExpression> GenericArgs => _genericArgs;

    /// <summary>
    /// Resuelve un tipo a partir de esta expresión.
    /// </summary>
    /// <param name="throwOnFail">
    /// <see langword="true"/> para lanzar una excepción si no es posible
    /// resolver la expresión de tipo representada por esta instancia,
    /// <see langword="false"/> para retornar <see langword="null"/> en su
    /// lugar.
    /// </param>
    /// <returns>
    /// Un tipo resuelto a partir de esta expresión, o <see langword="null"/>
    /// en caso de no poder resolverse la expresión representada por esta
    /// instancia si <paramref name="throwOnFail"/> se establece en 
    /// <see langword="false"/>.
    /// </returns>
    /// <exception cref="MissingTypeException">
    /// Se produce si no ha sido posible resolver el tipo a partir de la
    /// expresión representada por esta instancia si
    /// <paramref name="throwOnFail"/> se establece en <see langword="true"/>.
    /// </exception>
    public Type? Resolve(bool throwOnFail)
    {
        if (_genericArgs.Any() && SearchTypeByName($"{_fullName}`{_genericArgs.Count}") is Type tg)
        {
            var g = _genericArgs.Select(p => p.Resolve(throwOnFail));
            if (!g.IsAnyNull())
                return tg.MakeGenericType(g.NotNull().ToArray());
        }
        else if (!_genericArgs.Any() && SearchTypeByName(_fullName) is Type t)
        {
            return t;
        }
        return throwOnFail ? throw new MissingTypeException() : null;
    }

    /// <summary>
    /// Resuelve un tipo a partir de esta expresión.
    /// </summary>
    /// <returns>Un tipo resuelto a partir de esta expresión.</returns>
    /// <exception cref="MissingTypeException">
    /// Se produce si no ha sido posible resolver el tipo a partir de la
    /// expresión representada por esta instancia.
    /// </exception>
    public Type Resolve()
    {
        return Resolve(true)!;
    }

    private static Type? SearchTypeByName(string name)
    {
        return ReflectionHelpers.GetTypes<object>().NotNull().FirstOrDefault(p => p.FullName == name);
    }
}
