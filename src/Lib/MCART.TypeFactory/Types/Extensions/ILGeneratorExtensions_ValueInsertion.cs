/*
ILGeneratorExtensions_ObjInstancing.cs

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

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Types.Extensions.ConstantLoaders;
using static System.Reflection.Emit.OpCodes;
using static TheXDS.MCART.Resources.Errors;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contains useful extensions for code generation using the
/// <see cref="ILGenerator"/> class.
/// </summary>
public static partial class ILGeneratorExtensions
{
    /// <summary>
    /// Registers an <see cref="IConstantLoader"/> for the
    /// <see cref="LoadConstant{T}(ILGenerator, T)"/> method.
    /// </summary>
    /// <param name="loader">
    /// The <see cref="IConstantLoader"/> to register.
    /// </param>
    public static void RegisterConstantLoader(IConstantLoader loader)
    {
        _constantLoaders.Add(loader);
    }

    /// <summary>
    /// Inserts the initialization of a local variable in the Microsoft®
    /// Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence in which to insert the local variable
    /// initialization.
    /// </param>
    /// <param name="local">
    /// The local variable in which to store the value.
    /// </param>
    /// <param name="value">
    /// The constant value to store in the local variable.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator InitLocal(this ILGenerator ilGen, LocalBuilder local, object? value)
    {
        return ilGen.LoadConstant(local.LocalType, value).StoreLocal(local);
    }

    /// <summary>
    /// Inserts the initialization of a new local variable in the Microsoft®
    /// Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence in which to insert the local variable
    /// initialization.
    /// </param>
    /// <param name="value">
    /// The constant value to store in the local variable.
    /// </param>
    /// <param name="local">
    /// The local variable in which to store the value.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator InitNewLocal<T>(this ILGenerator ilGen, T value, out LocalBuilder local)
    {
        return InitLocal(ilGen, local = ilGen.DeclareLocal(typeof(T)), value);
    }

    /// <summary>
    /// Inserts a constant in the Microsoft® Intermediate Language (MSIL)
    /// sequence.
    /// </summary>
    /// <typeparam name="T">
    /// The type of constant value to insert.
    /// </typeparam>
    /// <param name="ilGen">
    /// The instruction sequence in which to insert the constant load.
    /// </param>
    /// <param name="value">
    /// The constant value to insert.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Thrown when attempting to load an unknown constant value.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when attempting to load a non-constant value, such as an object
    /// instance.
    /// </exception>
    public static ILGenerator LoadConstant<T>(this ILGenerator ilGen, T value)
    {
        Type t = typeof(T);
        if (_constantLoaders.FirstOrDefault(p => p.CanLoadConstant(value)) is { } cl)
        {
            cl.Emit(ilGen, value);
        }
        else if (t.IsStruct())
        {
            ilGen.Emit(Newobj, t.GetConstructor(Type.EmptyTypes)!);
        }
        else
        {
            ilGen.Emit(Ldnull);
        }
        return ilGen;
    }
    
    /// <summary>
    /// Inserts a series of instructions that will increment the value on the
    /// stack by 1 in the Microsoft® Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence in which to insert the operations.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator Increment(this ILGenerator ilGen)
    {
        return ilGen.LoadConstant(1).Add();
    }

    /// <summary>
    /// Inserts a series of instructions that will increment the value of a
    /// local variable by 1 in the Microsoft® Intermediate Language (MSIL)
    /// sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence in which to insert the operations.
    /// </param>
    /// <param name="local">
    /// Reference to the local variable on which to perform the increment
    /// operation.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator Increment(this ILGenerator ilGen, LocalBuilder local)
    {
        return ilGen.LoadLocal(local).Increment().StoreLocal(local);
    }

    /// <summary>
    /// Inserts a constant in the Microsoft® Intermediate Language (MSIL)
    /// sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence in which to insert the constant load.
    /// </param>
    /// <param name="t">
    /// The type of constant value to insert.
    /// </param>
    /// <param name="value">
    /// The constant value to insert.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Thrown when attempting to load an unknown constant value.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when attempting to load a non-constant value, such as an object
    /// instance.
    /// </exception>
    public static ILGenerator LoadConstant(this ILGenerator ilGen, Type t, object? value)
    {
        if (_constantLoaders.FirstOrDefault(p => p.ConstantType == t) is { } cl)
        {
            cl.Emit(ilGen, value);
        }
        else if (t.IsStruct())
        {
            ilGen.Emit(Newobj, t.GetConstructor(Type.EmptyTypes) ?? throw ClassNotInstantiable(t));
        }
        else
        {
            ilGen.Emit(Ldnull);
        }
        return ilGen;
    }

    /// <summary>
    /// Inserts a constant in the Microsoft® Intermediate Language (MSIL)
    /// sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence in which to insert the constant load.
    /// </param>
    /// <param name="value">
    /// The constant value to insert.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Thrown when attempting to load an unknown constant value.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown when attempting to load a non-constant value, such as an object
    /// instance.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="value"/> is <see langword="null"/>. To load a
    /// <see langword="null"/> constant value, use the
    /// <see cref="LoadNull(ILGenerator)"/> method instead.
    /// </exception>
    public static ILGenerator LoadConstant(this ILGenerator ilGen, object? value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return LoadConstant(ilGen, value.GetType(), value);
    }

    /// <summary>
    /// Inserts a load operation of the constant
    /// <see langword="null"/> in the Microsoft® Intermediate Language (MSIL)
    /// sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence in which to insert the operation.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator LoadNull(this ILGenerator ilGen) => OneLiner(ilGen, Ldnull);

    /// <summary>
    /// Inserts a one-dimensional array initialization operation of the
    /// specified type in the Microsoft® Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <typeparam name="T">The type of the new array.</typeparam>
    /// <param name="ilGen">
    /// The instruction sequence in which to insert the operation.
    /// </param>
    /// <param name="local">
    /// Parameter that contains the local variable generated to store the new
    /// array.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// The operation requires the stack to contain the number of elements that
    /// the array should contain, expressed as an <see cref="int"/> value.
    /// <br/><br/>
    /// Net stack usage: -1
    /// </remarks>
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static ILGenerator NewArray<T>(this ILGenerator ilGen, out LocalBuilder local) => NewArray(ilGen, typeof(T), out local);

    /// <summary>
    /// Inserts a one-dimensional array initialization operation of the
    /// specified type in the Microsoft® Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <typeparam name="T">The type of the new array.</typeparam>
    /// <param name="ilGen">
    /// The instruction sequence in which to insert the operation.
    /// </param>
    /// <param name="length">
    /// The number of elements that the array should contain.
    /// </param>
    /// <param name="local">
    /// Parameter that contains the local variable generated to store the new
    /// array.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// The operation requires the stack to contain the number of elements that
    /// the array should contain, expressed as an <see cref="int"/> value.
    /// <br/><br/>
    /// Net stack usage: -1
    /// </remarks>
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static ILGenerator NewArray<T>(this ILGenerator ilGen, int length, out LocalBuilder local) => NewArray(ilGen, length, typeof(T), out local);

    /// <summary>
    /// Inserts a one-dimensional array initialization operation of the
    /// specified type in the Microsoft® Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence in which to insert the operation.
    /// </param>
    /// <param name="length">
    /// The number of elements that the array should contain.
    /// </param>
    /// <param name="arrayType">The type of the new array.</param>
    /// <param name="local">
    /// Parameter that contains the local variable generated to store the new
    /// array.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// The operation requires the stack to contain the number of elements that
    /// the array should contain, expressed as an <see cref="int"/> value.
    /// <br/><br/>
    /// Net stack usage: -1
    /// </remarks>
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static ILGenerator NewArray(this ILGenerator ilGen, int length, Type arrayType, out LocalBuilder local)
    {
        return ilGen.LoadConstant(length).NewArray(arrayType, out local);
    }

    /// <summary>
    /// Inserts a one-dimensional array initialization operation of the
    /// specified type in the Microsoft® Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <typeparam name="T">The type of the new array.</typeparam>
    /// <param name="ilGen">
    /// The instruction sequence in which to insert the operation.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// The operation requires the stack to contain the number of elements that
    /// the array should contain, expressed as an <see cref="int"/> value.
    /// <br/><br/>
    /// Net stack usage: 0
    /// </remarks>
    public static ILGenerator NewArray<T>(this ILGenerator ilGen) => NewArray(ilGen, typeof(T));

    /// <summary>
    /// Inserts a one-dimensional array initialization operation of the
    /// specified type in the Microsoft® Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="arrayType">The type of the new array.</param>
    /// <param name="local">
    /// Parameter that contains the local variable generated to store the new
    /// array.
    /// </param>
    /// <param name="ilGen">
    /// The instruction sequence in which to insert the operation.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static ILGenerator NewArray(this ILGenerator ilGen, Type arrayType, out LocalBuilder local)
    {
        return ilGen.NewArray(arrayType).StoreNewLocal(arrayType.MakeArrayType(), out local);
    }

    /// <summary>
    /// Inserts a one-dimensional array initialization operation of the
    /// specified type in the Microsoft® Intermediate Language (MSIL) sequence.
    /// </summary>
    /// <param name="arrayType">The type of the new array.</param>
    /// <param name="ilGen">
    /// The instruction sequence in which to insert the operation.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    /// <remarks>
    /// The operation requires the stack to contain the number of elements that
    /// the array should contain, expressed as an <see cref="int"/> value.
    /// <br/><br/>
    /// Net stack usage: 0
    /// </remarks>
    public static ILGenerator NewArray(this ILGenerator ilGen, Type arrayType)
    {
        ilGen.Emit(Newarr, arrayType);
        return ilGen;
    }

    /// <summary>
    /// Inserts an object instantiation operation in the Microsoft® Intermediate
    /// Language (MSIL) sequence.
    /// </summary>
    /// <param name="ilGen">
    /// The instruction sequence in which to insert the object instantiation.
    /// </param>
    /// <param name="newObjectType">
    /// The type of object to instantiate.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator NewObj(this ILGenerator ilGen, Type newObjectType)
    {
        ilGen.Emit(Newobj, newObjectType);
        return ilGen;
    }

    /// <summary>
    /// Inserts an object instantiation operation in the Microsoft® Intermediate
    /// Language (MSIL) sequence.
    /// </summary>
    /// <typeparam name="T">
    /// The type of object to instantiate.
    /// </typeparam>
    /// <param name="ilGen">
    /// The instruction sequence in which to insert the object instantiation.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    public static ILGenerator NewObj<T>(this ILGenerator ilGen)
    {
        return NewObj(ilGen, typeof(T));
    }

    /// <summary>
    /// Inserts an object instantiation operation in the Microsoft® Intermediate
    /// Language (MSIL) sequence.
    /// </summary>
    /// <typeparam name="T">
    /// The type of object to instantiate.
    /// </typeparam>
    /// <param name="ilGen">
    /// The instruction sequence in which to insert the object instantiation.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    /// <exception cref="ClassNotInstantiableException">
    /// Thrown if the class is not instantiable, or if there is no constructor
    /// that accepts the specified arguments. This exception can also be thrown
    /// if one of the parameters is an object without a parameterless
    /// constructor, in which case the exception will indicate the type that
    /// cannot be instantiated.
    /// </exception>
    public static ILGenerator NewObject<T>(this ILGenerator ilGen)
    {
        return NewObject(ilGen, typeof(T));
    }

    /// <summary>
    /// Inserts an object instantiation operation in the Microsoft® Intermediate
    /// Language (MSIL) sequence.
    /// </summary>
    /// <typeparam name="T">
    /// The type of object to instantiate.
    /// </typeparam>
    /// <param name="ilGen">
    /// The instruction sequence in which to insert the object instantiation.
    /// </param>
    /// <param name="args">
    /// Arguments to pass to the object's constructor.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, enabling fluent syntax.
    /// </returns>
    /// <exception cref="ClassNotInstantiableException">
    /// Thrown if the class is not instantiable, or if there is no constructor
    /// that accepts the specified arguments. This exception can also be thrown
    /// if one of the parameters is an object without a parameterless
    /// constructor, in which case the exception will indicate the type that
    /// cannot be instantiated.
    /// </exception>
    public static ILGenerator NewObject<T>(this ILGenerator ilGen, IEnumerable args)
    {
        return NewObject(ilGen, typeof(T), args);
    }

    /// <summary>
    /// Inserts the instantiation of an object into the Microsoft® 
    /// Intermediate Language (MSIL) stream.
    /// </summary>
    /// <param name="type">
    /// The type of object to instantiate.
    /// </param>
    /// <param name="ilGen">
    /// The instruction stream where the object instantiation will be 
    /// inserted.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for 
    /// Fluent syntax usage.
    /// </returns>
    /// <exception cref="ClassNotInstantiableException">
    /// Thrown if the class is not instantiable, or if there is no 
    /// constructor that accepts the specified arguments. It may also 
    /// occur if one of the parameters is an object that does not have 
    /// a parameterless constructor, in which case the exception will 
    /// indicate the type that cannot be instantiated.
    /// </exception>
    public static ILGenerator NewObject(this ILGenerator ilGen, Type type)
    {
        return NewObject(ilGen, type, Array.Empty<object>());
    }

    /// <summary>
    /// Inserts the instantiation of an object into the Microsoft® 
    /// Intermediate Language (MSIL) stream.
    /// </summary>
    /// <param name="type">
    /// The type of object to instantiate.
    /// </param>
    /// <param name="ilGen">
    /// The instruction stream where the object instantiation will be 
    /// inserted.
    /// </param>
    /// <param name="args">
    /// Arguments to pass to the object's constructor.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for 
    /// Fluent syntax usage.
    /// </returns>
    /// <exception cref="ClassNotInstantiableException">
    /// Thrown if the class is not instantiable, or if there is no 
    /// constructor that accepts the specified arguments. It may also 
    /// occur if one of the parameters is an object that does not have 
    /// a parameterless constructor, in which case the exception will 
    /// indicate the type that cannot be instantiated.
    /// </exception>
    public static ILGenerator NewObject(this ILGenerator ilGen, Type type, IEnumerable args)
    {
        return NewObject(ilGen, type, args.Cast<object?>().ToArray());
    }

    /// <summary>
    /// Inserts the instantiation of an object into the Microsoft® 
    /// Intermediate Language (MSIL) stream.
    /// </summary>
    /// <param name="type">
    /// The type of object to instantiate.
    /// </param>
    /// <param name="ilGen">
    /// The instruction stream where the object instantiation will be 
    /// inserted.
    /// </param>
    /// <param name="args">
    /// Arguments to pass to the object's constructor.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for 
    /// Fluent syntax usage.
    /// </returns>
    /// <exception cref="ClassNotInstantiableException">
    /// Thrown if the class is not instantiable, or if there is no 
    /// constructor that accepts the specified arguments. It may also 
    /// occur if one of the parameters is an object that does not have 
    /// a parameterless constructor, in which case the exception will 
    /// indicate the type that cannot be instantiated.
    /// </exception>
    public static ILGenerator NewObject(this ILGenerator ilGen, Type type, object?[] args)
    {
        if (type.GetConstructor(args.ToTypes().ToArray()) is not { } c) throw new ClassNotInstantiableException(type);
        foreach (object? j in args)
        {
            if (j is null)
                ilGen.LoadNull();
            else if (j.GetType().IsClass)
                NewObject(ilGen, j.GetType());
            else
                LoadConstant(ilGen, j);
        }
        ilGen.Emit(Newobj, c);
        return ilGen;
    }

    /// <summary>
    /// Inserts the instantiation of an object into the Microsoft® 
    /// Intermediate Language (MSIL) stream.
    /// </summary>
    /// <param name="type">
    /// The type of object to instantiate.
    /// </param>
    /// <param name="ilGen">
    /// The instruction stream where the object instantiation will be 
    /// inserted.
    /// </param>
    /// <param name="args">
    /// The types of arguments accepted by the object's constructor.
    /// </param>
    /// <returns>
    /// The same instance as <paramref name="ilGen"/>, allowing for 
    /// Fluent syntax usage.
    /// </returns>
    /// <exception cref="ClassNotInstantiableException">
    /// Thrown if the class is not instantiable, or if there is no 
    /// constructor that accepts the specified arguments. It may also 
    /// occur if one of the parameters is an object that does not have 
    /// a parameterless constructor, in which case the exception will 
    /// indicate the type that cannot be instantiated.
    /// </exception>
    public static ILGenerator NewObject(this ILGenerator ilGen, Type type, Type[] args)
    {
        if (type.GetConstructor(args.ToTypes().ToArray()) is not { } c) throw new ClassNotInstantiableException(type);
        ilGen.Emit(Newobj, c);
        return ilGen;
    }
}
