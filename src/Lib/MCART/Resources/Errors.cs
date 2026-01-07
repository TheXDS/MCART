/*
Errors.cs

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

using System.Collections;
using System.Reflection;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Extensions;
using Ers = TheXDS.MCART.Resources.Strings.Errors;
using Str = TheXDS.MCART.Resources.Strings.Common;

namespace TheXDS.MCART.Resources;

/// <summary>
/// Contains resources that generate new exception instances to be
/// thrown.
/// </summary>
public static class Errors
{
    /// <summary>
    /// Creates a new instance of an <see cref="InvalidOperationException"/>
    /// that occurs when a type was expected to implement an interface,
    /// but it did not.
    /// </summary>
    /// <typeparam name="T">The expected interface type.</typeparam>
    /// <returns>
    /// A new instance of the
    /// <see cref="InvalidOperationException"/> class.
    /// </returns>
    public static InvalidOperationException InterfaceNotImplemented<T>() => InterfaceNotImplemented(typeof(T));

    /// <summary>
    /// Creates a new instance of an <see cref="InvalidOperationException"/>
    /// that occurs when a type was expected to implement an interface,
    /// but it did not.
    /// </summary>
    /// <param name="t">The expected interface type.</param>
    /// <returns>
    /// A new instance of the
    /// <see cref="InvalidOperationException"/> class.
    /// </returns>
    public static InvalidOperationException InterfaceNotImplemented(Type t)
    {
        return new InvalidOperationException(string.Format(Ers.InterfaceNotImplented, t.Name));
    }

    /// <summary>
    /// Creates a new <see cref="NullReferenceException"/> with a message
    /// indicating an attempt to dereference a field or property of an
    /// argument, which resulted in null.
    /// </summary>
    /// <param name="valuePath">Path to the field or property being
    /// dereferenced.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <returns>
    /// A new instance of the
    /// <see cref="NullReferenceException"/> class.
    /// </returns>
    public static NullReferenceException NullArgumentValue(string valuePath, string argumentName)
    {
        return new(string.Format(Ers.PropXOnArgYNull, valuePath, argumentName));
    }

    /// <summary>
    /// Creates a new <see cref="InvalidOperationException"/> with a
    /// default message indicating that a list must contain two specific
    /// objects in the context in which the exception is thrown.
    /// </summary>
    /// <returns>
    /// A new instance of the
    /// <see cref="InvalidOperationException"/> class.
    /// </returns>
    public static InvalidOperationException ListMustContainBoth()
    {
        return new(Ers.ListMustContainBoth);
    }

    /// <summary>
    /// Creates a new <see cref="InvalidOperationException"/> with a
    /// default message indicating that the specified expression is not
    /// a valid member selector.
    /// </summary>
    /// <returns>
    /// A new instance of the
    /// <see cref="InvalidOperationException"/> class.
    /// </returns>
    public static InvalidOperationException InvalidSelectorExpression()
    {
        return new(Ers.InvalidSelectorExpression);
    }

    /// <summary>
    /// Creates a new <see cref="FormatException"/> with a predefined
    /// formatted message.
    /// </summary>
    /// <param name="offendingFormat">The format string that caused the
    /// exception.</param>
    /// <returns>
    /// A new instance of the <see cref="FormatException"/> class.
    /// </returns>
    public static FormatException FormatNotSupported(string offendingFormat)
    {
        return new(string.Format(Ers.FormatNotSupported, offendingFormat));
    }

    /// <summary>
    /// Creates a new <see cref="ArgumentException"/> indicating that the
    /// value provided to an argument is not valid.
    /// </summary>
    /// <param name="argName">Name of the argument for which the exception
    /// will be generated.</param>
    /// <param name="value">The value of the argument that generated the
    /// exception.</param>
    /// <param name="inner">The exception that is the original cause of this
    /// exception.</param>
    /// <returns>
    /// A new instance of the <see cref="ArgumentException"/> class.
    /// </returns>
    public static ArgumentException InvalidValue(string? argName, object? value, Exception? inner = null)
    {
        string msg = string.Format(Ers.InvalidXValue, value?.ToString() ?? Str.Null);
        return argName is { } s
            ? new(msg, s, inner)
            : new(msg, inner);
    }

    /// <summary>
    /// Creates a new <see cref="ArgumentException"/> indicating that the
    /// value provided to an argument is not valid.
    /// </summary>
    /// <param name="argName">Name of the argument for which the exception
    /// will be generated.</param>
    /// <returns>
    /// A new instance of the <see cref="ArgumentException"/> class.
    /// </returns>
    public static ArgumentException InvalidValue(string argName)
    {
        return new(Ers.InvalidValue, argName);
    }

    /// <summary>
    /// Creates a new <see cref="ArgumentOutOfRangeException"/> indicating
    /// that the value of the argument is outside of a specific range of
    /// values.
    /// </summary>
    /// <param name="argName">Name of the argument for which the exception
    /// will be generated.</param>
    /// <param name="min">Minimum accepted value.</param>
    /// <param name="max">Maximum accepted value.</param>
    /// <returns>
    /// A new instance of the
    /// <see cref="ArgumentOutOfRangeException"/> class.
    /// </returns>
    public static ArgumentOutOfRangeException ValueOutOfRange(string argName, object min, object max)
    {
        return new(argName, string.Format(Ers.ValueMustBeBetweenXandY, min, max));
    }

    /// <summary>
    /// Creates a new <see cref="ArgumentOutOfRangeException"/> indicating
    /// that the value is outside the defined values of the enumeration.
    /// </summary>
    /// <typeparam name="T">The enumeration type.</typeparam>
    /// <param name="argName">Name of the argument for which the exception
    /// will be generated.</param>
    /// <param name="offendingValue">Value that caused the exception.</param>
    /// <returns>
    /// A new instance of the
    /// <see cref="ArgumentOutOfRangeException"/> class.
    /// </returns>
    public static ArgumentOutOfRangeException UndefinedEnum<T>(string argName, T offendingValue) where T : Enum
    {
        return UndefinedEnum(typeof(T), argName, offendingValue);
    }

    /// <summary>
    /// Creates a new <see cref="ArgumentOutOfRangeException"/> indicating
    /// that the value is outside the defined values of the enumeration.
    /// </summary>
    /// <param name="enumType">The enumeration type.</param>
    /// <param name="argName">Name of the argument for which the exception
    /// will be generated.</param>
    /// <param name="offendingValue">Value that caused the exception.</param>
    /// <returns>
    /// A new instance of the
    /// <see cref="ArgumentOutOfRangeException"/> class.
    /// </returns>
    public static ArgumentOutOfRangeException UndefinedEnum(Type enumType, string argName, Enum offendingValue)
    {
        return new(argName, string.Format(Ers.UndefinedEnum, offendingValue, enumType.NameOf()));
    }

    /// <summary>
    /// Creates a new <see cref="InvalidOperationException"/> indicating
    /// that the specified object could not be written.
    /// </summary>
    /// <param name="t">Type of the object that could not be written.</param>
    /// <returns>
    /// A new instance of the
    /// <see cref="InvalidOperationException"/> class.
    /// </returns>
    public static InvalidOperationException CantWriteObj(Type t)
    {
        return new(string.Format(Ers.CantWriteObjOfT, t.NameOf()));
    }

    /// <summary>
    /// Creates a new <see cref="InvalidOperationException"/> indicating that
    /// the property is read-only.
    /// </summary>
    /// <param name="prop">Property for which the exception has been
    /// generated.</param>
    /// <returns>
    /// A new instance of the
    /// <see cref="InvalidOperationException"/> class.
    /// </returns>
    public static InvalidOperationException PropIsReadOnly(PropertyInfo prop)
    {
        return new(string.Format(Ers.PropIsReadOnly, prop.NameOf()));
    }

    /// <summary>
    /// Creates a new <see cref="MissingMemberException"/> indicating that an
    /// attempt has been made to access a non-existent member in the specified
    /// type.
    /// </summary>
    /// <param name="type">Type in which the attempt was made to access the
    /// non-existent member.</param>
    /// <param name="missingMember">Member that was attempted to be
    /// accessed.</param>
    /// <returns>
    /// A new instance of the
    /// <see cref="MissingMemberException"/> class.
    /// </returns>
    public static MissingMemberException MissingMember(Type type, MemberInfo missingMember)
    {
        return new(type.Name, missingMember.Name);
    }

    /// <summary>
    /// Creates a new <see cref="InvalidTypeException"/> indicating that the
    /// specified type is not a type that inherits from <see cref="Enum"/>
    /// (declared as <see langword="enum"/>).
    /// </summary>
    /// <param name="argName">Name of the argument that produced the
    /// exception.</param>
    /// <param name="offendingType">Type for which the exception occurred.</param>
    /// <returns>
    /// A new instance of the <see cref="ArgumentException"/> class.
    /// </returns>
    public static ArgumentException EnumExpected(string argName, Type offendingType)
    {
        return new(string.Format(Ers.EnumTypeExpected, offendingType.NameOf()), argName);
    }

    /// <summary>
    /// Creates a new <see cref="ArgumentException"/> indicating that a
    /// minimum value is greater than the maximum when specifying a range of
    /// values.
    /// </summary>
    /// <returns>
    /// A new instance of the <see cref="ArgumentException"/> class.
    /// </returns>
    public static ArgumentException MinGtMax()
    {
        return new(Ers.MinGtMax);
    }

    /// <summary>
    /// Creates a new <see cref="ClassNotInstantiableException"/> indicating
    /// the type of the class that could not be instantiated.
    /// </summary>
    /// <param name="class">Type of the class that could not be instantiated.</param>
    /// <returns>
    /// A new instance of the
    /// <see cref="ClassNotInstantiableException"/> class.
    /// </returns>
    public static ClassNotInstantiableException ClassNotInstantiable(Type? @class = null)
    {
        return new(@class);
    }

    /// <summary>
    /// Creates a new <see cref="IncompleteTypeException"/> indicating that
    /// the type must contain a <see cref="System.Runtime.InteropServices.GuidAttribute"/>
    /// in its declaration to be valid in this context.
    /// </summary>
    /// <param name="type">Type for which the exception occurred.</param>
    /// <returns>
    /// A new instance of the <see cref="IncompleteTypeException"/> class.
    /// </returns>
    public static IncompleteTypeException MissingGuidAttribute(Type type)
    {
        return new(string.Format(Ers.MissingGuidAttrFromType, type), type);
    }

    /// <summary>
    /// Creates a new <see cref="TamperException"/> indicating that the
    /// application has entered an unexpected state.
    /// </summary>
    /// <returns>
    /// A new instance of the <see cref="TamperException"/> class.
    /// </returns>
    public static Exception Tamper() => new TamperException();

    /// <summary>
    /// Creates a new <see cref="InvalidTypeException"/> indicating that the
    /// specified type is not an enumeration type.
    /// </summary>
    /// <param name="offendingType">Type for which the exception occurred.</param>
    /// <returns>
    /// A new instance of the <see cref="InvalidTypeException"/> class.
    /// </returns>
    public static InvalidTypeException EnumerableTypeExpected(Type offendingType)
    {
        return new(Ers.EnumerableTypeExpected, offendingType);
    }

    /// <summary>
    /// Creates a new <see cref="InvalidTypeException"/> indicating that the
    /// specified type is not an interface.
    /// </summary>
    /// <param name="offendingType">Type for which the exception occurred.</param>
    /// <returns>
    /// A new instance of the <see cref="InvalidTypeException"/> class.
    /// </returns>
    public static InvalidTypeException InterfaceTypeExpected(Type offendingType)
    {
        return new(Ers.InterfaceExpected, offendingType);
    }

    /// <summary>
    /// Creates a new <see cref="InvalidTypeException"/> indicating that the
    /// specified type is not a valid type.
    /// </summary>
    /// <param name="offendingType">Type for which the exception occurred.</param>
    /// <param name="expectedType">Type expected by the code that threw the
    /// exception.</param>
    /// <returns>
    /// A new instance of the <see cref="InvalidTypeException"/> class.
    /// </returns>
    public static InvalidTypeException UnexpectedType(Type offendingType, Type expectedType)
    {
        return new(string.Format(Ers.UnexpectedType, offendingType, expectedType), offendingType);
    }

    /// <summary>
    /// Creates a new <see cref="InvalidOperationException"/> indicating that
    /// executing the operation causes a circular operation.
    /// </summary>
    /// <returns>
    /// A new instance of the <see cref="InvalidOperationException"/> class.
    /// </returns>
    public static InvalidOperationException CircularOpDetected()
    {
        return new(Ers.CircularOpDetected);
    }

    /// <summary>
    /// Creates a new <see cref="TypeLoadException"/> indicating that the type
    /// could not be instantiated with the provided constructor arguments.
    /// </summary>
    /// <param name="t">Type that is the cause of this exception.</param>
    /// <param name="inner">
    /// Exception that has been trhown trying to instantiate
    /// <paramref name="t"/>.
    /// </param>
    /// <returns>
    /// A new instance of the <see cref="TypeLoadException"/> class.
    /// </returns>
    /// <remarks>
    /// The error will be captured when an error occurs within the
    /// constructor of the type to be instantiated. If the class does not
    /// contain a constructor that accepts the arguments, call the
    /// <see cref="ClassNotInstantiable(Type?)"/> method instead.
    /// </remarks>
    /// <seealso cref="ClassNotInstantiable(Type?)"/>
    public static TypeLoadException CannotInstanceClass(Type t, Exception? inner = null)
    {
        return new(string.Format(Ers.ClassNotInstantiableWIthArgs, t.NameOf()), inner);
    }

    /// <summary>
    /// Creates a new <see cref="InvalidOperationException"/> indicating that
    /// the operation duplicates previously existing data when this is not a
    /// desired scenario.
    /// </summary>
    /// <param name="id">Identifier of the data that has been attempted to
    /// be duplicated.</param>
    /// <returns>
    /// A new instance of the <see cref="InvalidOperationException"/> class.
    /// </returns>
    public static InvalidOperationException DuplicateData(object id)
    {
        return new(string.Format(Ers.DuplicateData, id));
    }

    /// <summary>
    /// Creates a new <see cref="InvalidReturnValueException"/> indicating that
    /// the specified function has returned an invalid value in this context.
    /// </summary>
    /// <param name="call">Function that has returned the invalid value.</param>
    /// <param name="returnValue">Invalid value returned.</param>
    /// <returns>
    /// A new instance of the <see cref="InvalidReturnValueException"/> class.
    /// </returns>
    public static InvalidReturnValueException InvalidReturnValue(Delegate call, object? returnValue)
    {
        return new InvalidReturnValueException(call, returnValue);
    }

    /// <summary>
    /// Creates a new <see cref="NotSupportedException"/> indicating that it is
    /// not possible to execute a binary write for an object of the specified
    /// type.
    /// </summary>
    /// <param name="offendingType">Type of the object for which a binary
    /// write has been attempted.</param>
    /// <param name="alternative">Suggested alternative method for executing
    /// the binary write.</param>
    /// <returns>
    /// A new instance of the <see cref="NotSupportedException"/> class.
    /// </returns>
    public static NotSupportedException BinaryWriteNotSupported(Type offendingType, MethodInfo alternative)
    {
        return new NotSupportedException(string.Format(Ers.BinWriteXNotSupported, offendingType, alternative.Name, alternative.DeclaringType));
    }

    /// <summary>
    /// Creates a new instance of an
    /// <see cref="InvalidOperationException"/> indicating that it is not
    /// possible to process the operation because the collection does not
    /// contain elements.
    /// </summary>
    /// <param name="collection">Empty collection on which the operation has
    /// been attempted to be executed.</param>
    /// <returns>
    /// A new instance of the <see cref="InvalidOperationException"/> class.
    /// </returns>
    public static InvalidOperationException EmptyCollection(IEnumerable collection)
    {
        return new InvalidOperationException(Ers.EmptyCollection, new EmptyCollectionException(collection));
    }

    /// <summary>
    /// Creates a new exception of type <see cref="NullItemException"/> that
    /// can be thrown whenever an item inside a collection is
    /// <see langword="null"/>.
    /// </summary>
    /// <param name="index">
    /// Index of the item that is <see langword="null"/>.
    /// </param>
    /// <returns>
    /// A new instance of the <see cref="NullItemException"/> class.
    /// </returns>
    public static NullItemException NullItem(int index)
    {
        return new NullItemException() { NullIndex = index };
    }

    /// <summary>
    /// Creates an <see cref="InvalidOperationException"/> indicating that a
    /// quorum was not reached.
    /// </summary>
    /// <returns>
    /// An <see cref="InvalidOperationException"/> with a message describing
    /// the quorum failure.
    /// </returns>
    public static InvalidOperationException NoQuorum()
    {
        return new InvalidOperationException(Ers.NoQuorum);
    }

    internal static NullReferenceException FieldIsNull(FieldInfo j)
    {
        return new NullReferenceException(string.Format(Ers.FieldValueShouldNotBeNull, j.Name));
    }
}
