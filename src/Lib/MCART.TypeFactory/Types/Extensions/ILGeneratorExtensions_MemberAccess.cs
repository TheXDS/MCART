/*
ILGeneratorExtensions_MemberLoading.cs

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
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Helpers;
using static System.Reflection.Emit.OpCodes;

namespace TheXDS.MCART.Types.Extensions;

public static partial class ILGeneratorExtensions
{
    /// <summary>
    /// Inserts the loading of a local variable's address into the MSIL
    /// instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator instance where the address load will be inserted.
    /// </param>
    /// <param name="local">
    /// Local variable for which to load the reference.
    /// </param>
    /// <returns>
    /// The same ILGenerator instance, enabling fluent syntax.
    /// </returns>
    public static ILGenerator LoadLocalAddress(this ILGenerator ilGen, LocalBuilder local)
    {
        ilGen.Emit(Ldloca, local);
        return ilGen;
    }

    /// <summary>
    /// Inserts the loading of a local variable's value into the MSIL
    /// instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator instance where the value load will be inserted.
    /// </param>
    /// <param name="local">
    /// Local variable from which to load the value.
    /// </param>
    /// <returns>
    /// The same ILGenerator instance, enabling fluent syntax.
    /// </returns>
    public static ILGenerator LoadLocal(this ILGenerator ilGen, LocalBuilder local)
    {
        ilGen.Emit(Ldloc, local);
        return ilGen;
    }

    /// <summary>
    /// Inserts the storage of a value into a local variable in the MSIL
    /// instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator instance where the value will be stored.
    /// </param>
    /// <param name="local">
    /// Local variable where the value will be stored.
    /// </param>
    /// <returns>
    /// The same ILGenerator instance, enabling fluent syntax.
    /// </returns>
    public static ILGenerator StoreLocal(this ILGenerator ilGen, LocalBuilder local)
    {
        ilGen.Emit(Stloc, local);
        return ilGen;
    }

    /// <summary>
    /// Declares a new local variable of type T, stores a value into it,
    /// and returns the ILGenerator instance for fluent usage.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the local variable to declare.
    /// </typeparam>
    /// <param name="ilGen">
    /// The ILGenerator instance where the value will be stored.
    /// </param>
    /// <param name="local">
    /// The newly declared local variable that will hold the value.
    /// </param>
    /// <returns>
    /// The same ILGenerator instance, enabling fluent syntax.
    /// </returns>
    public static ILGenerator StoreNewLocal<T>(this ILGenerator ilGen, out LocalBuilder local)
    {
        local = ilGen.DeclareLocal(typeof(T));
        ilGen.Emit(Stloc, local);
        return ilGen;
    }

    /// <summary>
    /// Declares a new local variable of the specified type, stores a value
    /// into it, and returns the ILGenerator instance for fluent usage.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator instance where the value will be stored.
    /// </param>
    /// <param name="localType">
    /// Type of the local variable to declare.
    /// </param>
    /// <param name="local">
    /// The newly declared local variable that will hold the value.
    /// </param>
    /// <returns>
    /// The same ILGenerator instance, enabling fluent syntax.
    /// </returns>
    public static ILGenerator StoreNewLocal(this ILGenerator ilGen, Type localType, out LocalBuilder local)
    {
        local = ilGen.DeclareLocal(localType);
        ilGen.Emit(Stloc, local);
        return ilGen;
    }

    /// <summary>
    /// Inserts the load of a field value into the MSIL instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator sequence where the value load will be inserted.
    /// </param>
    /// <param name="field">
    /// The field from which to load the value.
    /// </param>
    /// <returns>
    /// The same ILGenerator instance, enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// This method automatically determines whether a reference to
    /// <see langword="this"/> (<see langword="Me"/> in Visual Basic) is
    /// required when the field is static; therefore, it should not
    /// insert a call to load the instance (<see cref="LoadArg0(ILGenerator)"/>).
    /// <br/><br/>
    /// Net stack effect: -1
    /// </remarks>
    public static ILGenerator LoadField(this ILGenerator ilGen, FieldInfo field)
    {
        ilGen.Emit(Ldfld, field);
        return ilGen;
    }

    /// <summary>
    /// Inserts the load of a field value into the MSIL instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator sequence where the value load will be inserted.
    /// </param>
    /// <param name="field">
    /// The field from which to load the value.
    /// </param>
    /// <returns>
    /// The same ILGenerator instance, enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// This method automatically determines whether a reference to
    /// <see langword="this"/> (<see langword="Me"/> in Visual Basic) is
    /// required when the field is static; therefore, it should not
    /// insert a call to load the instance (<see cref="LoadArg0(ILGenerator)"/>).
    /// <br/><br/>
    /// Net stack effect: -1
    /// </remarks>
    public static ILGenerator GetField(this ILGenerator ilGen, FieldInfo field)
    {
        return GetField(ilGen, field, Ldfld);
    }

    /// <summary>
    /// Inserts the storage of a value into a field in the MSIL instruction
    /// stream.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator sequence where the value will be stored.
    /// </param>
    /// <param name="field">
    /// The field into which to store the value.
    /// </param>
    /// <returns>
    /// The same ILGenerator instance, enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// This method requires that the stack contain the value to store at
    /// the top, immediately followed by a reference to the instance if
    /// the field is an instance field. If the field is static, no
    /// reference to <see langword="this"/> (<see langword="Me"/> in Visual
    /// Basic) is required.
    /// <br/><br/>
    /// Net stack effect: -1
    /// </remarks>
    /// <seealso cref="LoadArg0(ILGenerator)"/>
    /// <seealso cref="StoreField(ILGenerator, FieldInfo, Action{ILGenerator})"/>
    public static ILGenerator StoreField(this ILGenerator ilGen, FieldInfo field)
    {
        ilGen.Emit(Stfld, field);
        return ilGen;
    }

    /// <summary>
    /// Inserts the storage of a value into a field in the Microsoft
    /// Intermediate Language (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence where the value will be stored.
    /// </param>
    /// <param name="field">
    /// Field into which to store the value.
    /// </param>
    /// <param name="value">
    /// Callback that will load the value to be stored in the field.
    /// </param>
    /// <returns>
    /// The same <see cref="ILGenerator"/> instance as <paramref name="ilGen"/>,
    /// enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// This method does not automatically insert a reference to
    /// <see langword="this"/> (<see langword="Me"/> in Visual Basic) when the
    /// field is static; the caller must insert the instance load
    /// (<see cref="LoadArg0(ILGenerator)"/>).
    /// <br/><br/>
    /// Net stack effect: -1
    /// </remarks>
    /// <seealso cref="SetField(ILGenerator, FieldInfo, Action{ILGenerator})"/>
    public static ILGenerator StoreField(this ILGenerator ilGen, FieldInfo field, Action<ILGenerator> value)
    {
        value.Invoke(ilGen);
        return StoreField(ilGen, field);
    }

    /// <summary>
    /// Inserts the storage of a value into a field in the Microsoft
    /// Intermediate Language (MSIL) instruction sequence.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence where the value will be stored.
    /// </param>
    /// <param name="field">
    /// Field into which to store the value.
    /// </param>
    /// <param name="value">
    /// Callback that will load the value to be stored in the field.
    /// </param>
    /// <returns>
    /// The same <see cref="ILGenerator"/> instance as <paramref name="ilGen"/>,
    /// enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// This method does not automatically determine whether a reference
    /// to <see langword="this"/> (<see langword="Me"/> in Visual Basic) is
    /// needed for static fields; the caller must insert the instance
    /// load (<see cref="LoadArg0(ILGenerator)"/>).
    /// <br/><br/>
    /// Net stack effect: -1
    /// </remarks>
    /// <seealso cref="SetField(ILGenerator, FieldInfo, Func{ILGenerator, ILGenerator})"/>
    [Sugar]
    public static ILGenerator StoreField(this ILGenerator ilGen, FieldInfo field, Func<ILGenerator, ILGenerator> value)
    {
        return StoreField(ilGen, field, (Action<ILGenerator>)(il => _ = value(il)));
    }

    /// <summary>
    /// Inserts the storage of a value into a field in the Microsoft
    /// Intermediate Language (MSIL) instruction sequence, automatically
    /// determining whether a reference to <see langword="this"/> is required.
    /// </summary>
    /// <param name="ilGen">
    /// Instruction sequence where the value storage will be inserted.
    /// </param>
    /// <param name="field">
    /// Field into which to store the value.
    /// </param>
    /// <param name="value">
    /// Callback that will load the value to be stored in the field.
    /// </param>
    /// <returns>
    /// The same <see cref="ILGenerator"/> instance as <paramref name="ilGen"/>,
    /// enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// This method automatically determines if a reference to
    /// <see langword="this"/> (<see langword="Me"/> in Visual Basic) is
    /// needed for non‑static fields; the instance load is omitted for
    /// static fields.
    /// <br/><br/>
    /// Net stack effect: -1
    /// </remarks>
    /// <seealso cref="StoreField(ILGenerator, FieldInfo, Func{ILGenerator, ILGenerator})"/>
    public static ILGenerator SetField(this ILGenerator ilGen, FieldInfo field, Func<ILGenerator, ILGenerator> value)
    {
        if (!field.IsStatic) ilGen.LoadArg0();
        return StoreField(ilGen, field, value);
    }

    /// <summary>
    /// Inserts the storage of a value into a field in the Microsoft Intermediate
    /// Language (MSIL) sequence, automatically determining whether a reference
    /// to <see langword="this"/> is required.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator instance to which the field storage will be inserted.
    /// </param>
    /// <param name="field">
    /// The field into which to store the value.
    /// </param>
    /// <param name="value">
    /// Callback that loads the value to be stored in the field.
    /// </param>
    /// <returns>
    /// The same <see cref="ILGenerator"/> instance as <paramref name="ilGen"/>,
    /// enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// This method automatically determines if a reference to
    /// <see langword="this"/> (<see langword="Me"/> in Visual Basic) is needed
    /// for non‑static fields; the instance load is omitted for static fields.
    /// <br/><br/>
    /// Net stack effect: -1
    /// </remarks>
    /// <seealso cref="StoreField(ILGenerator, FieldInfo, Action{ILGenerator})"/>
    public static ILGenerator SetField(this ILGenerator ilGen, FieldInfo field, Action<ILGenerator> value)
    {
        if (!field.IsStatic) ilGen.LoadArg0();
        return StoreField(ilGen, field, value);
    }

    /// <summary>
    /// Inserts the storage of a value into a property in the Microsoft
    /// Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator instance to which the property storage will be inserted.
    /// </param>
    /// <param name="prop">
    /// The property into which to store the value.
    /// </param>
    /// <param name="value">
    /// Callback that loads the value to be stored in the property.
    /// </param>
    /// <returns>
    /// The same <see cref="ILGenerator"/> instance as <paramref name="ilGen"/>,
    /// enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// This method automatically determines if a reference to
    /// <see langword="this"/> (<see langword="Me"/> in Visual Basic) is required
    /// for non‑static properties; the instance load is omitted for static
    /// properties.
    /// <br/><br/>
    /// Net stack effect: -1
    /// </remarks>
    public static ILGenerator StoreProperty(this ILGenerator ilGen, PropertyInfo prop, Action<ILGenerator> value)
    {
        if (!prop.SetMethod?.IsStatic ?? throw new InvalidOperationException()) ilGen.LoadArg0();
        value.Invoke(ilGen);
        return StoreProperty(ilGen, prop);
    }

    /// <summary>
    /// Stores a value into a property in the Microsoft Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator instance to which the property storage will be inserted.
    /// </param>
    /// <param name="prop">
    /// The property into which to store the value.
    /// </param>
    /// <returns>
    /// The same <see cref="ILGenerator"/> instance as <paramref name="ilGen"/>,
    /// enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// This method requires the value to be stored to be on the top of the
    /// stack, immediately followed by a reference to the instance if the
    /// property is an instance property. If the property is static, no
    /// reference to <see langword="this"/> (<see langword="Me"/> in Visual
    /// Basic) is required.
    /// <br/><br/>
    /// Net stack effect: -1
    /// </remarks>
    /// <seealso cref="LoadArg0(ILGenerator)"/>
    /// <seealso cref="StoreProperty(ILGenerator, PropertyInfo, Action{ILGenerator})"/>
    public static ILGenerator StoreProperty(this ILGenerator ilGen, PropertyInfo prop)
    {
        return ilGen.Call(prop.SetMethod!);
    }

    /// <summary>
    /// Inserts the storage of a value into a property in the Microsoft
    /// Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator instance to which the property storage will be inserted.
    /// </param>
    /// <param name="prop">
    /// The property into which the value will be stored.
    /// </param>
    /// <param name="value">
    /// Callback that loads the value to be stored in the property.
    /// </param>
    /// <returns>
    /// The same <see cref="ILGenerator"/> instance as <paramref name="ilGen"/>,
    /// enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// This method automatically determines whether a reference to
    /// <see langword="this"/> (<see langword="Me"/> in Visual Basic) must
    /// be added for instance properties; no reference is loaded for static
    /// properties.
    /// <br/><br/>
    /// Net stack effect: -1
    /// </remarks>
    [Sugar]
    public static ILGenerator StoreProperty(this ILGenerator ilGen, PropertyInfo prop, Func<ILGenerator, ILGenerator> value)
    {
        return StoreProperty(ilGen, prop, (Action<ILGenerator>)(il => _ = value(il)));
    }

    /// <summary>
    /// Inserts the load of the address of a field into the IL sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator instance to which the field address will be loaded.
    /// </param>
    /// <param name="field">
    /// The field for which the address is loaded.
    /// </param>
    /// <returns>
    /// The same <see cref="ILGenerator"/> instance as <paramref name="ilGen"/>,
    /// enabling fluent syntax.
    /// </returns>
    public static ILGenerator LoadFieldAddress(this ILGenerator ilGen, FieldInfo field)
    {
        return GetField(ilGen, field, Ldflda);
    }

    /// <summary>
    /// Inserts the load of a property value into the IL sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator instance to which the property value will be loaded.
    /// </param>
    /// <param name="property">
    /// The property from which the value is loaded.
    /// </param>
    /// <returns>
    /// The same <see cref="ILGenerator"/> instance as <paramref name="ilGen"/>,
    /// enabling fluent syntax.
    /// </returns>
    public static ILGenerator LoadProperty(this ILGenerator ilGen, PropertyInfo property)
    {
        MethodInfo m = property.GetGetMethod()!;
        if (!m.IsStatic) ilGen.LoadArg0();
        return ilGen.Call(m);
    }

    /// <summary>
    /// Inserts the load of a property value into the IL sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator instance to which the property value will be loaded.
    /// </param>
    /// <param name="property">
    /// The property from which the value is loaded.
    /// </param>
    /// <returns>
    /// The same <see cref="ILGenerator"/> instance as <paramref name="ilGen"/>,
    /// enabling fluent syntax.
    /// </returns>
    public static ILGenerator LoadProperty(this ILGenerator ilGen, PropertyBuildInfo property)
    {
        return LoadProperty(ilGen, property.Member);
    }

    /// <summary>
    /// Inserts the load of a property value into the Microsoft
    /// Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <typeparam name="T">
    /// Type of object from which the property to load is selected.
    /// </typeparam>
    /// <param name="ilGen">
    /// The ILGenerator instance to which the property value will be
    /// loaded.
    /// </param>
    /// <param name="propertySelector">
    /// Expression indicating which property of the type should be
    /// returned.
    /// </param>
    /// <returns>
    /// The same <see cref="ILGenerator"/> instance as <paramref name="ilGen"/>,
    /// enabling fluent syntax.
    /// </returns>
    public static ILGenerator LoadProperty<T>(this ILGenerator ilGen, Expression<Func<T, object?>> propertySelector)
    {
        return LoadProperty(ilGen, ReflectionHelpers.GetProperty(propertySelector));
    }

    /// <summary>
    /// Inserts the load of argument with index 0 of a method into the
    /// Microsoft Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator instance to which the operation will be inserted.
    /// </param>
    /// <returns>
    /// The same <see cref="ILGenerator"/> instance as <paramref name="ilGen"/>,
    /// enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// For instance methods, this call loads a reference to the current
    /// instance; for static methods it loads the first parameter.
    /// <br/><br/>
    /// Net stack usage: 1
    /// </remarks>
    public static ILGenerator LoadArg0(this ILGenerator ilGen) => OneLiner(ilGen, Ldarg_0);

    /// <summary>
    /// Inserts the load of argument with index 1 of a method into the
    /// Microsoft Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator instance to which the operation will be inserted.
    /// </param>
    /// <returns>
    /// The same <see cref="ILGenerator"/> instance as <paramref name="ilGen"/>,
    /// enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// For instance methods, this call loads the first argument of the
    /// method; for static methods it loads the second parameter.
    /// <br/><br/>
    /// Net stack usage: 1
    /// </remarks>
    public static ILGenerator LoadArg1(this ILGenerator ilGen) => OneLiner(ilGen, Ldarg_1);

    /// <summary>
    /// Inserts the load of argument with index 2 of a method into the
    /// Microsoft Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator instance to which the operation will be inserted.
    /// </param>
    /// <returns>
    /// The same <see cref="ILGenerator"/> instance as <paramref name="ilGen"/>,
    /// enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// For instance methods, this call loads the second argument of the
    /// method; for static methods it loads the third parameter.
    /// <br/><br/>
    /// Net stack usage: 1
    /// </remarks>
    public static ILGenerator LoadArg2(this ILGenerator ilGen) => OneLiner(ilGen, Ldarg_2);

    /// <summary>
    /// Inserts the load of the third argument of a method into the
    /// Microsoft Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator instance to which the operation will be inserted.
    /// </param>
    /// <returns>
    /// The same <see cref="ILGenerator"/> instance as <paramref name="ilGen"/>,
    /// enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// For instance methods, this call loads the third argument of the
    /// method; for static methods it loads the fourth parameter.
    /// <br/><br/>
    /// Net stack usage: 1
    /// </remarks>
    public static ILGenerator LoadArg3(this ILGenerator ilGen) => OneLiner(ilGen, Ldarg_3);

    /// <summary>
    /// Inserts the load of an argument into the Microsoft Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator instance into which the operation will be inserted.
    /// </param>
    /// <param name="argIndex">
    /// Index of the argument to load onto the stack. A value of 0 will load a
    /// reference to the current instance (the value this) when called from an
    /// instance method, causing the method parameters to start at index 1.
    /// </param>
    /// <returns>
    /// The same <paramref name="ilGen"/> instance, allowing fluent syntax.
    /// </returns>
    /// <remarks>
    /// Net stack usage: 1.
    /// </remarks>
    public static ILGenerator LoadArg(this ILGenerator ilGen, short argIndex)
    {
        if (argIndex < 0) throw new ArgumentOutOfRangeException(nameof(argIndex));
        ilGen.Emit(Ldarg, argIndex);
        return ilGen;
    }

    /// <summary>
    /// Inserts the load of a reference to an argument into the Microsoft
    /// Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator instance into which the operation will be inserted.
    /// </param>
    /// <param name="argIndex">
    /// Index of the argument for which to load a reference. A value of 0 will
    /// load a reference to the current instance (the value this).
    /// </param>
    /// <returns>
    /// The same <paramref name="ilGen"/> instance, allowing fluent syntax.
    /// </returns>
    public static ILGenerator LoadArgAddress(this ILGenerator ilGen, short argIndex)
    {
        if (argIndex < 0) throw new ArgumentOutOfRangeException(nameof(argIndex));
        ilGen.Emit(Ldarga, argIndex);
        return ilGen;
    }

    /// <summary>
    /// Inserts the load of a parameter into the Microsoft Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The ILGenerator instance into which the operation will be inserted.
    /// </param>
    /// <param name="parameter">
    /// The parameter to load.
    /// </param>
    /// <returns>
    /// The same <paramref name="ilGen"/> instance, allowing fluent syntax.
    /// </returns>
    public static ILGenerator LoadParameter(this ILGenerator ilGen, ParameterInfo parameter)
    {
        ilGen.Emit(Ldarg, parameter.Position);
        return ilGen;
    }
}
