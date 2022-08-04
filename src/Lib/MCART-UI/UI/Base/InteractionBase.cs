/*
InteractionBase.cs

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

using System;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.UI.Base;

/// <summary>
/// Clase base para todos los objetos que describan una interacción
/// disponible dentro de un sistema de menús.
/// </summary>
public abstract class InteractionBase : NotifyPropertyChanged, INameable, IDescriptible
{
    private string _Name;
    private string? _Description;

    /// <summary>
    /// Obtiene o establece el nombre a mostrar para esta interacción.
    /// </summary>
    /// <value>El nombre a mostrar para esta interacción.</value>
    public string Name
    {
        get => _Name;
        set => Change(ref _Name, value);
    }

    /// <summary>
    /// Obtiene o establece una descripción para esta interacción.
    /// </summary>
    /// <value>Una descripción para esta interacción.</value>
    public string? Description
    {
        get => _Description;
        set => Change(ref _Description, value);
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InteractionBase"/>, estableciendo un nombre
    /// descriptivo a mostrar.
    /// </summary>
    /// <param name="name">
    /// Nombre descriptivo a mostrar.
    /// </param>
    protected InteractionBase(string name) : this(name, null)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase
    /// <see cref="InteractionBase"/>, estableciendo un nombre
    /// y una descripción a mostrar.
    /// </summary>
    /// <param name="name">
    /// Nombre descriptivo a mostrar.
    /// </param>
    /// <param name="description">
    /// Descripción del elemento.
    /// </param>
    protected InteractionBase(string name, string? description)
    {
        _Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
    }
}
