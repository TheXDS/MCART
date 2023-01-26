﻿/*
TextAttribute.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

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
using static System.AttributeTargets;

namespace TheXDS.MCART.Attributes;

/// <summary>
/// Agrega un elemento textual genérico a un elemento, además de ser la
/// clase base para los atributos que describan un valor representable como
/// <see cref="string" /> para un elemento.
/// </summary>
[AttributeUsage(All), Serializable]
public class TextAttribute : Attribute, IValueAttribute<string?>
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="TextAttribute" />.
    /// </summary>
    /// <param name="text">Valor de este atributo.</param>
    protected TextAttribute(string? text)
    {
        Value = text;
    }

    /// <summary>
    /// Obtiene el valor asociado a este atributo.
    /// </summary>
    /// <value>El valor de este atributo.</value>
    public string? Value { get; }
}
