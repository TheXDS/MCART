/*
MethodInfoExtensions.cs

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
using System.Reflection;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene extensiones para la clase <see cref="MethodInfo"/>.
/// </summary>
public static partial class MethodInfoExtensions
{
    /// <summary>
    /// Crea un delegado del tipo especificado a partir del método.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de delegado a obtener.
    /// </typeparam>
    /// <param name="m">Método del cual obtener un delegado.</param>
    /// <param name="targetInstance">
    /// Objetivo de instancia al cual enlazar el delegado generado, o
    /// <see langword="null"/> para generar un delegado de método estático.
    /// </param>
    /// <returns>
    /// Un delegado del tipo especificado a partir del método, o
    /// <see langword="null"/> si no es posible realizar la conversión.
    /// </returns>
    public static T? ToDelegate<T>(this MethodInfo m, object? targetInstance = null) where T : notnull, Delegate
    {
        ToDelegate_Contract(m, targetInstance);
        return (T?)Delegate.CreateDelegate(typeof(T), targetInstance, m, false);
    }

    /// <summary>
    /// Obtiene un valor que determina si el método no devuelve valores
    /// (si es <see langword="void"/>).
    /// </summary>
    /// <param name="m">Método a comprobar.</param>
    /// <returns>
    /// <see langword="true"/> si el método no devuelve valores, 
    /// <see langword="false"/> en caso contrario.
    /// </returns>
    public static bool IsVoid(this MethodInfo m)
    {
        IsVoid_Contract(m);
        return m.ReturnType == typeof(void);
    }

    /// <summary>
    /// Determina si el método invalida a una definición base.
    /// </summary>
    /// <param name="method"></param>
    /// <returns>
    /// <see langword="true"/> si el método invalida a una definición
    /// base, <see langword="false"/> en caso contrario.
    /// </returns>
    public static bool IsOverride(this MethodInfo method)
    {
        IsOverride_Contract(method);
        return method.GetBaseDefinition().DeclaringType != method.DeclaringType;
    }

    /// <summary>
    /// Comprueba que la firma de un método sea compatible con el delegado
    /// especificado.
    /// </summary>
    /// <param name="staticMethodInfo">
    /// <see cref="MethodInfo" /> a comprobar.
    /// </param>
    /// <param name="delegate">
    /// <see cref="Type" /> del <see cref="Delegate" /> a comprobar.
    /// </param>
    /// <returns>
    /// <see langword="true" /> si el método es compatible con la firma del
    /// delegado especificado, <see langword="false" /> en caso contrario.
    /// </returns>
    public static bool IsSignatureCompatible(this MethodInfo staticMethodInfo, Type @delegate)
    {
        return Delegate.CreateDelegate(@delegate, staticMethodInfo, false) is not null;
    }

    /// <summary>
    /// Comprueba que la firma de un método sea compatible con el delegado
    /// especificado.
    /// </summary>
    /// <param name="staticMethodInfo">
    /// <see cref="MethodInfo" /> a comprobar.
    /// </param>
    /// <typeparam name="T">
    /// Tipo del <see cref="Delegate" /> a comprobar.
    /// </typeparam>
    /// <returns>
    /// <see langword="true" /> si el método es compatible con la firma del
    /// delegado especificado, <see langword="false" /> en caso contrario.
    /// </returns>
    public static bool IsSignatureCompatible<T>(this MethodInfo staticMethodInfo) where T : Delegate
    {
        return IsSignatureCompatible(staticMethodInfo, typeof(T));
    }
}
