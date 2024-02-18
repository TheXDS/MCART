/*
ObjectAttribute.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2024 César Andrés Morgan

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

namespace TheXDS.MCART.Attributes;

/* -= NOTA =-
 * Los atributos (al momento de incluir esta nota, incluso en C#10) no soportan
 * clases genéricas, por lo cual es necesario crear una implementación base
 * para cada posible tipo de atributo con base de valor que pueda ser
 * necesaria.
 * 
 * UPDATE: C#11 y .Net 7 soportan atributos genéricos. Sin embargo, al no ser
 * estos un release LTS, esta característica aún no será soportada por MCART.
 */

/// <summary>
/// Clase base para los atributos de cualquier tipo.
/// </summary>
public abstract class ObjectAttribute : Attribute, IValueAttribute<object?>
{
    /// <summary>
    /// Crea una nueva instancia de la clase
    /// <see cref="ObjectAttribute" />.
    /// </summary>
    /// <param name="attributeValue">Valor de este atributo.</param>
    protected ObjectAttribute(object? attributeValue)
    {
        Value = attributeValue;
    }

    /// <summary>
    /// Obtiene el valor asociado a este atributo.
    /// </summary>
    public object? Value { get; }
}
