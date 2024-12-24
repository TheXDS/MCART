/*
MethodBaseExtensions.cs

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

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Misc;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene extensiones para la clase <see cref="MethodBase"/>.
/// </summary>
public static partial class MethodBaseExtensions
{
    /// <summary>
    /// Obtiene un nombre completo para un método, incluyendo el tipo y
    /// el espacio de nombres donde el mismo ha sido definido.
    /// </summary>
    /// <param name="method">
    /// Método del cual obtener el nombre completo.
    /// </param>
    /// <returns>
    /// El nombre completo del método, incluyendo el tipo y el espacio
    /// de nombres donde el mismo ha sido definido.
    /// </returns>
    public static string FullName(this MethodBase method)
    {
        StringBuilder s = new();
        s.Append(method.DeclaringType?.CSharpName().OrNull("{0}."));
        s.Append(method.Name);
        if (method.IsGenericMethod)
        {
            s.Append(string.Join(", ", method.GetGenericArguments().Select(TypeExtensions.CSharpName)).OrNull("<{0}>"));
        }
        s.Append($"({string.Join(", ", method.GetParameters().Select(q => q.ParameterType.CSharpName()))})");
        return s.ToString();
    }

    /// <summary>
    /// Determina si el método especificado ha sido invalidado en la
    /// instancia provista.
    /// </summary>
    /// <param name="method">
    /// Método a comprobar.
    /// </param>
    /// <param name="instance">
    /// Instancia en la cual se debe realizar la comprobación.
    /// Generalmente, este argumento debe ser <see langword="this"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> si el método ha sido invalidado en la
    /// instancia especificada, <see langword="false"/> en caso 
    /// contrario.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Se produce si <paramref name="method"/> o
    /// <paramref name="instance"/> son <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidTypeException">
    /// Se produce si la definición de <paramref name="method"/> no existe
    /// en el tipo de <paramref name="instance"/>.
    /// </exception>
    [RequiresDynamicCode(AttributeErrorMessages.MethodCreatesNewTypes)]
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    public static bool IsOverridden(this MethodBase method, object instance)
    {
        IsOverridden_Contract(method, instance);
        MethodInfo m = instance.GetType().GetMethod(method.Name, GetBindingFlags(method), null, method.GetParameters().Select(p => p.ParameterType).ToArray(), null)
            ?? throw new TamperException(new MissingMethodException(instance.GetType().Name, method.Name));

        return method.DeclaringType != m.DeclaringType;
    }

    /// <summary>
    /// Infiere las <see cref="BindingFlags"/> utilizadas en la
    /// definición del método.
    /// </summary>
    /// <param name="method">
    /// Método para el cual inferir las <see cref="BindingFlags"/>.
    /// </param>
    /// <returns>
    /// Las <see cref="BindingFlags"/> inferidas basadas en las
    /// propiedades del método.
    /// </returns>
    public static BindingFlags GetBindingFlags(this MethodBase method)
    {
        BindingFlags retVal = BindingFlags.Default;

        void Test(MethodAttributes inFlag, BindingFlags orFlag, BindingFlags notFlags = BindingFlags.Default)
        {
            if (method.Attributes.HasFlag(inFlag))
            {
                retVal |= orFlag;
            }
            else
            {
                retVal |= notFlags;
            }
        }

        Test(MethodAttributes.Public, BindingFlags.Public);
        Test(MethodAttributes.Private, BindingFlags.NonPublic);
        Test(MethodAttributes.Family, BindingFlags.NonPublic);
        Test(MethodAttributes.Static, BindingFlags.Static, BindingFlags.Instance);

        return retVal;
    }

    /// <summary>
    /// Obtiene un arreglo con los tipos de parámetro del método.
    /// </summary>
    /// <param name="method">
    /// Método del cual extraer la colección de tipos de parámetro.
    /// </param>
    /// <returns>
    /// Un arreglo con los tipos de cada uno de los parámetros del método.
    /// </returns>
    public static Type[] GetParameterTypes(this MethodBase method)
    {
        return method.GetParameters().Select(p => p.ParameterType).ToArray();
    }
}
