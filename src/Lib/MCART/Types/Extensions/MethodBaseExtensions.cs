/*
MethodBaseExtensions.cs

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

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Misc;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contains extensions for the <see cref="MethodBase"/> class.
/// </summary>
public static partial class MethodBaseExtensions
{
    /// <summary>
    /// Gets a full name for a method, including the type and
    /// the namespace where the same has been defined.
    /// </summary>
    /// <param name="method">
    /// Method from which to get the full name.
    /// </param>
    /// <returns>
    /// The full name of the method, including the type and the
    /// namespace where the same has been defined.
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
    /// Determines if the specified method has been overridden in the
    /// provided instance.
    /// </summary>
    /// <param name="method">
    /// Method to check.
    /// </param>
    /// <param name="instance">
    /// Instance in which to perform the check.
    /// Generally, this argument should be <see langword="this"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the method has been overridden in the
    /// instance specified, <see langword="false"/> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="method"/> or
    /// <paramref name="instance"/> are <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidTypeException">
    /// Thrown if the definition of <paramref name="method"/> does not exist
    /// in the type of <paramref name="instance"/>.
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
    /// Infers the <see cref="BindingFlags"/> used in the
    /// definition of the method.
    /// </summary>
    /// <param name="method">
    /// Method for which to infer the <see cref="BindingFlags"/>.
    /// </param>
    /// <returns>
    /// The <see cref="BindingFlags"/> inferred based on the
    /// properties of the method.
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
    /// Gets an array with the parameter types of the method.
    /// </summary>
    /// <param name="method">
    /// Method from which to extract the collection of parameter types.
    /// </param>
    /// <returns>
    /// An array with the types of each of the parameters of the method.
    /// </returns>
    public static Type[] GetParameterTypes(this MethodBase method)
    {
        return method.GetParameters().Select(p => p.ParameterType).ToArray();
    }
}
