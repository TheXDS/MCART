/*
ILGeneratorExtensions_MethodCalls.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

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

using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;
using static System.Reflection.Emit.OpCodes;
using Op = System.Reflection.Emit.OpCodes;

namespace TheXDS.MCART.Types.Extensions;

public static partial class ILGeneratorExtensions
{
    /// <summary>
    /// Inserts a call to the specified instance method into the Microsoft®
    /// Intermediate Language (MSIL) instruction stream.
    /// </summary>
    /// <typeparam name="TClass">
    /// Class in which the instance method resides.
    /// </typeparam>
    /// <typeparam name="TMethod">
    /// Delegate that describes the method to call.
    /// </typeparam>
    /// <param name="ilGen">
    /// Instruction sequence in which to insert the instance method call.
    /// </param>
    /// <param name="methodSelector">
    /// Expression that selects the instance method to call.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator Call<TClass, TMethod>(this ILGenerator ilGen, Expression<Func<TClass, TMethod>> methodSelector) where TMethod : Delegate
    {
        return Call(ilGen, ReflectionHelpers.GetMethod(methodSelector));
    }

    /// <summary>
    /// Inserts a call to the specified base type instance constructor into
    /// the Microsoft® Intermediate Language (MSIL) instruction stream.
    /// </summary>
    /// <typeparam name="TClass">
    /// Class from which the base type constructor will be called.
    /// </typeparam>
    /// <param name="ilGen">
    /// Instruction sequence in which to insert the base constructor call.
    /// </param>
    /// <param name="baseCtorArgs">
    /// Array of argument types for the constructor to call.
    /// </param>
    /// <param name="parameterLoadCallback">
    /// Callback to insert the loading of arguments to pass to the base
    /// constructor.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// When calling this method, the current reference (<see cref="LoadArg0(ILGenerator)"/>) will
    /// automatically be inserted when <paramref name="baseCtorArgs"/> is not empty
    /// and <paramref name="parameterLoadCallback"/> refers to a method that
    /// loads the constructor arguments, or when <paramref name="baseCtorArgs"/> is
    /// empty and <paramref name="parameterLoadCallback"/> is <see langword="null"/>.
    /// Otherwise, you must insert the loading of <c>Arg0</c> and all arguments
    /// before performing this call.
    /// </remarks>
    public static ILGenerator CallBaseCtor<TClass>(this ILGenerator ilGen, Type[] baseCtorArgs, Action<ILGenerator>? parameterLoadCallback)
    {
        if (parameterLoadCallback is not null)
        {
            if (baseCtorArgs.Length == 0) throw TypeFactoryErrors.CtorParamCallback();
            if (baseCtorArgs.Length > 0)
            { 
                ilGen.LoadArg0();
                parameterLoadCallback.Invoke(ilGen);
            }
        } else if (baseCtorArgs.Length == 0) ilGen.LoadArg0();

        ilGen.Emit(Op.Call, typeof(TClass).GetConstructor(baseCtorArgs)
            ?? typeof(TClass).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(p => p.GetParameters().Select(q => q.ParameterType).ItemsEqual(baseCtorArgs))
            ?? throw new MissingMemberException());
        return ilGen;
    }

    /// <summary>
    /// Inserts a call to the parameterless base type instance constructor
    /// into the Microsoft® Intermediate Language (MSIL) instruction stream.
    /// </summary>
    /// <typeparam name="TClass">
    /// Class from which the base type constructor will be called.
    /// </typeparam>
    /// <param name="ilGen">
    /// Instruction sequence in which to insert the base constructor call.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator CallBaseCtor<TClass>(this ILGenerator ilGen) => CallBaseCtor<TClass>(ilGen, Type.EmptyTypes, null);

    /// <summary>
    /// Inserts a call to the specified static method into the Microsoft®
    /// Intermediate Language (MSIL) instruction stream.
    /// </summary>
    /// <typeparam name="TMethod">
    /// Delegate that describes the method to call.
    /// </typeparam>
    /// <param name="ilGen">
    /// Instruction sequence into which the static method call is inserted.
    /// </param>
    /// <param name="methodSelector">
    /// Expression that selects the static method to call.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator Call<TMethod>(this ILGenerator ilGen, Expression<Func<TMethod>> methodSelector) where TMethod : Delegate
    {
        return Call(ilGen, ReflectionHelpers.GetMethod(methodSelector));
    }

    /// <summary>
    /// Inserts a call to the specified static method into the Microsoft®
    /// Intermediate Language (MSIL) instruction stream.
    /// </summary>
    /// <typeparam name="TMethod">
    /// Delegate that describes the method to call.
    /// </typeparam>
    /// <param name="ilGen">
    /// Instruction sequence into which the static method call is inserted.
    /// </param>
    /// <param name="method">
    /// Static method to call.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator Call<TMethod>(this ILGenerator ilGen, TMethod method) where TMethod : Delegate
    {
        return Call(ilGen, method.Method);
    }

    /// <summary>
    /// Inserts a call to the specified method.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which the method call is inserted.
    /// </param>
    /// <param name="method">
    /// Method to call.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator Call(this ILGenerator ilGen, MethodInfo method)
    {
        ilGen.Emit(method.IsVirtual ? Callvirt : Op.Call, method);
        return ilGen;
    }

    /// <summary>
    /// Inserts an explicitly virtual call to the specified method.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence into which the call is inserted.
    /// </param>
    /// <param name="method">
    /// Method to call.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator CallVirt(this ILGenerator ilGen, MethodInfo method)
    {
        ilGen.Emit(Callvirt, method);
        return ilGen;
    }
}
