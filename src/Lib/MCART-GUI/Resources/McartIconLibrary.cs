/*
McartIconLibrary.cs

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

using System.Runtime.CompilerServices;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Resources;

/// <summary>
/// Clase base interna para exponer íconos incrustados en los 
/// ensamblados de MCART.
/// </summary>
/// <typeparam name="T">
/// Tipo de ícono a obtener.
/// </typeparam>
public abstract class McartIconLibrary<T>
{
    private static readonly McartIconLibrary<T> _staticInstance = Objects.FindFirstObject<McartIconLibrary<T>>() ?? throw new MissingTypeException(typeof(McartIconLibrary<T>));

    /// <summary>
    /// Inicializa la clase <see cref="McartIconLibrary{T}"/>
    /// </summary>
    static McartIconLibrary()
    {
        if (!_staticInstance.GetType().Assembly.HasAttr<McartComponentAttribute>()) throw new TamperException();
    }

    /// <summary>
    /// Implementa el método de obtención del ícono basado en el nombre
    /// del ícono solicitado.
    /// </summary>
    /// <param name="id">
    /// Id del ícono solicitado.
    /// </param>
    /// <returns>
    /// El ícono solicitado.
    /// </returns>
    protected abstract T GetIcon([CallerMemberName] string? id = null!);

    /// <summary>
    /// Ícono principal de MCART.
    /// </summary>
    public static T MCART => _staticInstance.GetIcon();

    /// <summary>
    /// Ícono de plugin de MCART.
    /// </summary>
    public static T Plugin => _staticInstance.GetIcon();

    /// <summary>
    /// Ícono de archivo incorrecto.
    /// </summary>
    public static T BadFile => _staticInstance.GetIcon();

    /// <summary>
    /// Ícono de archivo no encontrado.
    /// </summary>
    public static T FileMissing => _staticInstance.GetIcon();

    /// <summary>
    /// Ícono de problema con archivo.
    /// </summary>
    public static T FileWarning => _staticInstance.GetIcon();
}
