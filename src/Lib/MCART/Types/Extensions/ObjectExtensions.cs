/*
ObjectExtensions.cs

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
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TheXDS.MCART.Attributes;
using TheXDS.MCART.Helpers;
using TheXDS.MCART.Misc;
using static TheXDS.MCART.Types.Extensions.EnumerableExtensions;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contains useful extensions for the <see cref="object"/> class.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Determines if an object is any of the specified ones.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if <paramref name="obj"/> is any of the
    /// specified objects, <see langword="false"/> otherwise.
    /// </returns>
    /// <param name="obj">Object to check.</param>
    /// <param name="objects">List of objects to compare.</param>
    public static bool IsEither(this object obj, params object[] objects)
    {
        return objects.Any(p => p?.Is(obj) ?? obj is null);
    }

    /// <summary>
    /// Determines if an object is any of the specified ones.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if <paramref name="obj"/> is any of the
    /// specified objects, <see langword="false"/> otherwise.
    /// </returns>
    /// <param name="obj">Object to check.</param>
    /// <param name="objects">List of objects to compare.</param>
    public static bool IsEither(this object obj, IEnumerable objects)
    {
        return objects.ToGeneric().Any(obj.Is);
    }

    /// <summary>
    /// Determines if an object is not any of the specified ones.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if <paramref name="obj"/> is not any of the
    /// specified objects, <see langword="false"/> otherwise.
    /// </returns>
    /// <param name="obj">Object to check.</param>
    /// <param name="objects">List of objects to compare.</param>
    public static bool IsNeither(this object obj, params object[] objects)
    {
        return obj.IsNeither(objects.AsEnumerable());
    }

    /// <summary>
    /// Determines if an object is not any of the specified ones.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if <paramref name="obj"/> is not any of the
    /// specified objects, <see langword="false"/> otherwise.
    /// </returns>
    /// <param name="obj">Object to check.</param>
    /// <param name="objects">List of objects to compare.</param>
    public static bool IsNeither(this object obj, IEnumerable objects)
    {
        return objects.ToGeneric().All(p => !p.Is(obj));
    }

    /// <summary>
    /// Determines if <paramref name="obj1"/> is the same instance as
    /// <paramref name="obj2"/>.
    /// </summary>
    /// <param name="obj1">Object to check.</param>
    /// <param name="obj2">Object against which to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the instance of <paramref name="obj1"/> is the same
    /// as <paramref name="obj2"/>, <see langword="false"/> otherwise.
    /// </returns>
    [Sugar]
    public static bool Is(this object? obj1, object? obj2)
    {
        return ReferenceEquals(obj1, obj2);
    }

    /// <summary>
    /// Retrieves the attribute associated with the declaration of the specified object.
    /// </summary>
    /// <typeparam name="T">
    /// Type of attribute to return. Must inherit from <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="obj">Object from which the attribute will be extracted.</param>
    /// <returns>
    /// An attribute of the type <typeparamref name="T"/> with the
    /// associated data in the object's declaration; or <see langword="null"/> if
    /// the specified attribute is not found.
    /// </returns>
    [Sugar]
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodGetsTypeMembersByName)]
    public static T? GetAttribute<T>(this object obj) where T : Attribute
    {
        HasAttribute(obj, out T? attribute);
        return attribute;
    }

    /// <summary>
    /// Determines if <paramref name="obj1"/> is a different instance than
    /// <paramref name="obj2"/>.
    /// </summary>
    /// <param name="obj1">Object to check.</param>
    /// <param name="obj2">Object against which to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the instance of <paramref name="obj1"/> is not the same
    /// as <paramref name="obj2"/>, <see langword="false"/> otherwise.
    /// </returns>
    [Sugar]
    public static bool IsNot(this object? obj1, object? obj2)
    {
        return !ReferenceEquals(obj1, obj2);
    }

    /// <summary>
    /// Determines if any of the objects are the same instance as
    /// <paramref name="obj"/>.
    /// </summary>
    /// <returns>
    /// An enumerator with the indices of the objects that are the same
    /// instance as <paramref name="obj"/>.
    /// </returns>
    /// <param name="obj">Object to check.</param>
    /// <param name="collection">Collection of objects to check.</param>
    public static IEnumerable<int> WhichAre(this object obj, IEnumerable<object> collection)
    {
        int c = 0;
        foreach (object? j in collection)
        {
            if (j.Is(obj)) yield return c;
            c++;
        }
    }

    /// <summary>
    /// Determines if any of the objects are the same instance as
    /// <paramref name="obj"/>.
    /// </summary>
    /// <returns>
    /// An enumerator with the indices of the objects that are the same
    /// instance as <paramref name="obj"/>.
    /// </returns>
    /// <param name="obj">Object to check.</param>
    /// <param name="collection">Collection of objects to check.</param>
    public static IEnumerable<int> WhichAre(this object obj, params object[] collection)
    {
        return obj.WhichAre(collection.AsEnumerable());
    }

    /// <summary>
    /// Enumerates the value of all properties that return values of
    /// type <typeparamref name="T"/> of the specified object.
    /// </summary>
    /// <typeparam name="T">Type of properties to obtain.</typeparam>
    /// <param name="instance">
    /// Instance from which to obtain the properties.
    /// </param>
    /// <returns>
    /// An enumeration of all values of type
    /// <typeparamref name="T"/> of the object.
    /// </returns>
    [Sugar]
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static IEnumerable<T> PropertiesOf<T>(this object instance)
    {
        return instance.GetType().GetProperties().PropertiesOf<T>(instance);
    }

    /// <summary>
    /// Determines if a member possesses an attribute defined.
    /// </summary>
    /// <typeparam name="T">
    /// Type of attribute to return. Must inherit from <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="obj">
    /// Member from which the attribute will be extracted.
    /// </param>
    /// <param name="attribute">
    /// Out parameter. If an attribute of type
    /// <typeparamref name="T"/> has been found, the same is returned.
    /// Null will be returned if the member does not possess the
    /// specified attribute.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the member possesses the attribute, <see langword="false"/>
    /// otherwise.
    /// </returns>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodGetsTypeMembersByName)]
    public static bool HasAttribute<T>(this object obj, [NotNullWhen(true)] out T? attribute) where T : notnull, Attribute
    {
        return obj switch
        {
            null => throw new ArgumentNullException(nameof(obj)),
            Assembly a => a.HasAttribute(out attribute),
            MemberInfo m => m.HasAttribute(out attribute),
            Enum e => e.HasAttribute(out attribute),
            _ => HasAttributes(obj, out IEnumerable<T>? attributes) & (attribute = attributes?.FirstOrDefault()) is not null
        };
    }

    /// <summary>
    /// Determines if a member possesses an attribute defined.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of value to return.
    /// </typeparam>
    /// <typeparam name="TAttribute">
    /// Type of attribute to search for. Must inherit from
    /// <see cref="Attribute"/> and from <see cref="IValueAttribute{T}"/>.
    /// </typeparam>
    /// <param name="obj">
    /// Member from which the attribute will be extracted.
    /// </param>
    /// <param name="value">
    /// Out parameter. If an attribute of type
    /// <typeparamref name="TAttribute" /> has been found, the value
    /// of the same is returned.
    /// Default will be returned if the member does not possess the
    /// specified attribute.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the member possesses the attribute, <see langword="false"/>
    /// otherwise.
    /// </returns>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodGetsTypeMembersByName)]
    public static bool HasAttrValue<TAttribute, TValue>(this object obj, [MaybeNullWhen(false)] out TValue value)
        where TAttribute : Attribute, IValueAttribute<TValue>
    {
        switch (obj)
        {
            case Assembly a:
                return a.HasAttrValue<TAttribute, TValue>(out value);
            case MemberInfo m:
                return m.HasAttrValue<TAttribute, TValue>(out value);
            case Enum e:
                return e.HasAttrValue<TAttribute, TValue>(out value);
            default:
                bool retVal = HasAttributes(obj, out IEnumerable<TAttribute>? attributes);
                value = attributes?.FirstOrDefault() is { Value: { } v } ? v : default!;
                return retVal;
        }
    }

    /// <summary>
    /// Determines if a member possesses an attribute defined.
    /// </summary>
    /// <typeparam name="T">
    /// Type of attribute to return. Must inherit from <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="obj">
    /// Member from which the attribute will be extracted.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the member possesses the attribute, <see langword="false"/>
    /// otherwise.
    /// </returns>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodGetsTypeMembersByName)]
    public static bool HasAttribute<T>(this object obj) where T : Attribute
    {
        return HasAttribute<T>(obj, out _);
    }

    /// <summary>
    /// Determines if a member possesses an attribute defined.
    /// </summary>
    /// <typeparam name="T">
    /// Type of attribute to return. Must inherit from <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="obj">
    /// Member from which the attribute will be extracted.
    /// </param>
    /// <param name="attribute">
    /// Out parameter. If an attribute of type
    /// <typeparamref name="T"/> has been found, the same is returned.
    /// Null will be returned if the member does not possess the attribute
    /// specified.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the member possesses the attribute, <see langword="false"/>
    /// otherwise.
    /// </returns>
    public static bool HasAttributes<T>(this object obj, [NotNullWhen(true)] out IEnumerable<T>? attribute) where T : Attribute
    {
        attribute = Attribute.GetCustomAttributes(obj.GetType(), typeof(T)).OfType<T>();
        return attribute.Any();
    }

    /// <summary>
    /// Returns the attribute associated with the specified assembly.
    /// </summary>
    /// <typeparam name="T">
    /// Type of attribute to return. Must inherit from <see cref="Attribute"/>.
    /// </typeparam>
    /// <param name="member">
    /// <see cref="object"/> from which the
    /// attribute will be extracted.
    /// </param>
    /// <returns>
    /// An attribute of the type <typeparamref name="T"/> with the
    /// associated data in the assembly declaration; or <see langword="null"/> in case
    /// of not finding the specified attribute.
    /// </returns>
    [Sugar]
    public static IEnumerable<T>? GetAttributes<T>(this object member) where T : Attribute
    {
        HasAttributes(member, out IEnumerable<T>? attributes);
        return attributes;
    }

    /// <summary>
    /// Enumerates the value of all fields that return values of
    /// type <typeparamref name="T"/> of the specified object.
    /// </summary>
    /// <typeparam name="T">Type of fields to obtain.</typeparam>
    /// <param name="instance">
    /// Instance from which to obtain the fields.
    /// </param>
    /// <returns>
    /// An enumeration of all values of type
    /// <typeparamref name="T"/> of the object.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="instance"/> is
    /// <see langword="null"/>.
    /// </exception>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public static IEnumerable<T> FieldsOf<T>(this object instance)
    {
        ArgumentNullException.ThrowIfNull(instance, nameof(instance));
        return ReflectionHelpers.FieldsOf<T>(instance.GetType().GetFields(), instance);
    }

#if DynamicLoading

    /// <summary>
    /// Gets the name of an object.
    /// </summary>
    /// <param name="obj">
    /// Object from which to get the name.
    /// </param>
    /// <returns>
    /// The name of the object.
    /// </returns>
    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    public static string NameOf(this object obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        return (obj as Base.INameable)?.Name
            ?? (string?)ScanNameMethod(obj.GetType())?.Invoke(null, [obj])
            ?? MemberInfoExtensions.NameOf(obj.GetType());
    }

    [RequiresUnreferencedCode(AttributeErrorMessages.MethodScansForTypes)]
    private static MethodInfo? ScanNameMethod(Type fromType)
    {
        foreach (var j in PrivateInternals.SafeGetExportedTypes(typeof(Objects).Assembly))
        {
            foreach (var k in j.GetMethods(BindingFlags.Static | BindingFlags.Public))
            {
                if (k.Name != "NameOf" || k.ReturnType != typeof(string)) continue;
                var pars = k.GetParameters();
                if (pars.Length != 1 || !pars[0].ParameterType.IsAssignableFrom(fromType)) continue;
                return k;
            }
        }
        return null;
    }

#endif
}
