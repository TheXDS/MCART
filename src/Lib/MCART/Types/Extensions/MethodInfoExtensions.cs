/*
MethodInfoExtensions.cs

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

using System.Reflection;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contains extensions for the <see cref="MethodInfo"/> class.
/// </summary>
public static partial class MethodInfoExtensions
{
    /// <summary>
    /// Creates a delegate of the specified type from the method.
    /// </summary>
    /// <typeparam name="T">
    /// Type of delegate to obtain.
    /// </typeparam>
    /// <param name="m">Method from which to obtain a delegate.</param>
    /// <param name="targetInstance">
    /// Target instance to which to link the generated delegate, or
    /// <see langword="null"/> to generate a static method delegate.
    /// </param>
    /// <returns>
    /// A delegate of the specified type from the method, or
    /// <see langword="null"/> if the conversion is not possible.
    /// </returns>
    public static T? ToDelegate<T>(this MethodInfo m, object? targetInstance = null) where T : notnull, Delegate
    {
        ToDelegate_Contract(m, targetInstance);
        return (T?)Delegate.CreateDelegate(typeof(T), targetInstance, m, false);
    }

    /// <summary>
    /// Gets a value that determines if the method does not return values
    /// (if it is <see langword="void"/>).
    /// </summary>
    /// <param name="m">Method to check.</param>
    /// <returns>
    /// <see langword="true"/> if the method does not return values,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool IsVoid(this MethodInfo m)
    {
        IsVoid_Contract(m);
        return m.ReturnType == typeof(void);
    }

    /// <summary>
    /// Determines if the method overrides a base definition.
    /// </summary>
    /// <param name="method"></param>
    /// <returns>
    /// <see langword="true"/> if the method overrides a base definition,
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool IsOverride(this MethodInfo method)
    {
        IsOverride_Contract(method);
        return method.GetBaseDefinition().DeclaringType != method.DeclaringType;
    }

    /// <summary>
    /// Checks that the signature of a method is compatible with the
    /// specified delegate.
    /// </summary>
    /// <param name="staticMethodInfo">
    /// <see cref="MethodInfo" /> to check.
    /// </param>
    /// <param name="delegate">
    /// <see cref="Type" /> of the <see cref="Delegate" /> to check.
    /// </param>
    /// <returns>
    /// <see langword="true" /> if the method is compatible with the signature of the
    /// specified delegate, <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsSignatureCompatible(this MethodInfo staticMethodInfo, Type @delegate)
    {
        return Delegate.CreateDelegate(@delegate, staticMethodInfo, false) is not null;
    }

    /// <summary>
    /// Checks that the signature of a method is compatible with the delegate
    /// specified.
    /// </summary>
    /// <param name="staticMethodInfo">
    /// <see cref="MethodInfo" /> to check.
    /// </param>
    /// <typeparam name="T">
    /// Type of the <see cref="Delegate" /> to check.
    /// </typeparam>
    /// <returns>
    /// <see langword="true" /> if the method is compatible with the signature of the
    /// specified delegate, <see langword="false" /> otherwise.
    /// </returns>
    public static bool IsSignatureCompatible<T>(this MethodInfo staticMethodInfo) where T : Delegate
    {
        return IsSignatureCompatible(staticMethodInfo, typeof(T));
    }
}
