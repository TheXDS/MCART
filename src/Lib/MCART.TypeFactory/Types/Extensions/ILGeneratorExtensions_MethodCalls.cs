/*
ILGeneratorExtensions_MethodCalls.cs

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

using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Resources;
using static System.Reflection.Emit.OpCodes;
using Op = System.Reflection.Emit.OpCodes;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene extensiones útiles para la generación de código por medio
/// de la clase <see cref="ILGenerator"/>.
/// </summary>
public static partial class ILGeneratorExtensions
{
    /// <summary>
    /// Inserta una llamada al método de instancia especificado en la
    /// secuencia del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <typeparam name="TClass">
    /// Clase en la que reside el método de instancia.
    /// </typeparam>
    /// <typeparam name="TMethod">
    /// Delegado que describe al método a llamar.
    /// </typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la llamada al método
    /// de instancia.
    /// </param>
    /// <param name="methodSelector">
    /// Expresión que permite seleccionar al método de instancia a llamar.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Call<TClass, TMethod>(this ILGenerator ilGen, Expression<Func<TClass, TMethod>> methodSelector) where TMethod : Delegate
    {
        return Call(ilGen, ReflectionHelpers.GetMethod(methodSelector));
    }

    /// <summary>
    /// Inserta una llamada al constructor de instancia especificado del
    /// tipo base en la secuencia del lenguaje intermedio de Microsoft®
    /// (MSIL).
    /// </summary>
    /// <typeparam name="TClass">
    /// Clase desde la cual se llamará al constructor de instancia del tipo
    /// base.
    /// </typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la llamada al método
    /// de instancia.
    /// </param>
    /// <param name="baseCtorArgs">
    /// Arreglo de tipos de argumentos del constructor a llamar.
    /// </param>
    /// <param name="parameterLoadCallback">
    /// Llamada a ejecutar para insertar la carga de argumentos a pasar al constructor base.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    /// <remarks>
    /// Al llamar a este método, se insertará automáticamente la llamada a la
    /// referencia actual (<see cref="LoadArg0(ILGenerator)"/>) cuando
    /// <paramref name="baseCtorArgs"/> no sea una colección vacía y
    /// <paramref name="parameterLoadCallback"/> haga referencia a un método a
    /// llamar para cargar los parámetros del constructor, o cuando
    /// <paramref name="baseCtorArgs"/> sea una colección vacía y
    /// <paramref name="parameterLoadCallback"/> se establezca en
    /// <see langword="null"/>. De lo contrario, deberá insertar la carga de
    /// <c>Arg0</c> y todos los argumentos a pasar al constructor antes de
    /// realizar esta llamada.
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
    /// Inserta una llamada al constructor de instancia del tipo base sin
    /// argumentos en la secuencia del lenguaje intermedio de Microsoft®
    /// (MSIL).
    /// </summary>
    /// <typeparam name="TClass">
    /// Clase desde la cual se llamará al constructor de instancia del tipo
    /// base.
    /// </typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la llamada al método
    /// de instancia.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator CallBaseCtor<TClass>(this ILGenerator ilGen) => CallBaseCtor<TClass>(ilGen, Type.EmptyTypes, null);

    /// <summary>
    /// Inserta una llamada al método estático especificado en la secuencia
    /// del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <typeparam name="TMethod">
    /// Delegado que describe al método a llamar.
    /// </typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la llamada al método
    /// estático.
    /// </param>
    /// <param name="methodSelector">
    /// Expresión que permite seleccionar al método estático a llamar.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Call<TMethod>(this ILGenerator ilGen, Expression<Func<TMethod>> methodSelector) where TMethod : Delegate
    {
        return Call(ilGen, ReflectionHelpers.GetMethod(methodSelector));
    }

    /// <summary>
    /// Inserta una llamada al método estático especificado en la secuencia
    /// del lenguaje intermedio de Microsoft® (MSIL).
    /// </summary>
    /// <typeparam name="TMethod">
    /// Delegado que describe al método a llamar.
    /// </typeparam>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la llamada al método
    /// estático.
    /// </param>
    /// <param name="method">
    /// Método estático a llamar.
    /// </param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Call<TMethod>(this ILGenerator ilGen, TMethod method) where TMethod : Delegate
    {
        return Call(ilGen, method.Method);
    }

    /// <summary>
    /// Inserta una llamada al método especificado.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la llamada al método
    /// estático.
    /// </param>
    /// <param name="method">Método a llamar.</param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator Call(this ILGenerator ilGen, MethodInfo method)
    {
        ilGen.Emit(method.IsVirtual ? Callvirt : Op.Call, method);
        return ilGen;
    }

    /// <summary>
    /// Inserta una llamada explícitamente virtual al método especificado.
    /// </summary>
    /// <param name="ilGen">
    /// Secuencia de instrucciones en la cual insertar la llamada.
    /// </param>
    /// <param name="method">Método a llamar.</param>
    /// <returns>
    /// La misma instancia que <paramref name="ilGen"/>, permitiendo el uso
    /// de sintaxis Fluent.
    /// </returns>
    public static ILGenerator CallVirt(this ILGenerator ilGen, MethodInfo method)
    {
        ilGen.Emit(Callvirt, method);
        return ilGen;
    }
}
