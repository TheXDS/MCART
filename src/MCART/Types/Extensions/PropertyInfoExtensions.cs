/*
PropertyInfoExtensions.cs

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

namespace TheXDS.MCART.Types.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Resources;

/// <summary>
/// Extensiones varias para objetos <see cref="PropertyInfo" />.
/// </summary>
public static class PropertyInfoExtensions
{
    /// <summary>
    /// Establece el valor de la propiedad de un objeto a su valor
    /// predeterminado.
    /// </summary>
    /// <param name="property">Propiedad a restablecer.</param>
    /// <param name="instance">
    /// Instancia del objeto que contiene la propiedad.
    /// </param>
    public static void SetDefault(this PropertyInfo property, object? instance)
    {
        object? GetDefault()
        {
            object? d = null;
            try
            {
                return (instance?.GetType().TryInstance(out d) ?? false) ? property.GetValue(d) : null;
            }
            finally
            {
                if (d is IDisposable i) i.Dispose();
            }
        }

        if (instance is null || instance.GetType().GetProperties().Any(p => p.Is(property)))
        {
            if (property.SetMethod is null)
            {
                throw Errors.PropIsReadOnly(property);
            }
            property.SetMethod.Invoke(instance, new[]
            {
                property.GetAttr<DefaultValueAttribute>()?.Value ??
                GetDefault() ??
                property.PropertyType.Default()
            });
        }
        else
        {
            throw Errors.MissingMember(instance.GetType(), property);
        }
    }

    /// <summary>
    /// Establece el valor de una propiedad estática a su valor
    /// predeterminado.
    /// </summary>
    /// <param name="property">Propiedad a restablecer.</param>
    public static void SetDefault(this PropertyInfo property)
    {
        SetDefault(property, null);
    }

    /// <summary>
    /// Obtiene un valor que determina si la propiedad admite lectura y
    /// escritura.
    /// </summary>
    /// <param name="property">
    /// Propiedad a comprobar.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si la propiedad admite lectura y
    /// escritura, <see langword="false"/> en caso contrario.
    /// </returns>
    [Sugar]
    public static bool IsReadWrite(this PropertyInfo property)
    {
        return property.CanRead && property.CanWrite;
    }
}
