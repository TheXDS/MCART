/*
TypeFactory.cs

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
using System.Reflection.Emit;
using System.Text;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types;

/// <summary>
/// Type factory. Allows defining and compiling new types at runtime.
/// </summary>
public class TypeFactory : IExposeAssembly
{
    private static readonly Dictionary<string, ModuleBuilder> _builtModules = [];
    private static readonly Dictionary<string, AssemblyBuilder> _builtAssemblies = [];

    private readonly string _namespace;
    private readonly bool _useGuid;
    private readonly ModuleBuilder _mBuilder;
    private readonly AssemblyBuilder _assembly;

    /// <summary>
    /// Gets a reference to the dynamic assembly where types built by this
    /// <see cref="TypeFactory"/> are loaded.
    /// </summary>
    public Assembly Assembly => _assembly;

    /// <summary>
    /// Initializes a new instance of the <see cref="TypeFactory"/> class.
    /// </summary>
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public TypeFactory() : this("TheXDS.MCART.Types._Generated") { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TypeFactory"/> class.
    /// </summary>
    /// <param name="namespace">Namespace to use for the types to be built.</param>
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public TypeFactory(string @namespace) : this(@namespace, true) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TypeFactory"/> class.
    /// </summary>
    /// <param name="useGuid">
    /// True to append a GUID to generated type names.
    /// </param>
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public TypeFactory(bool useGuid) : this()
    {
        _useGuid = useGuid;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TypeFactory"/> class.
    /// </summary>
    /// <param name="namespace">Namespace to use for the types to be built.</param>
    /// <param name="useGuid">
    /// True to append a GUID to generated type names.
    /// </param>
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public TypeFactory(string @namespace, bool useGuid)
    {
        _namespace = @namespace;
        _useGuid = useGuid;
        if (_builtModules.TryGetValue(@namespace, out ModuleBuilder? value))
        {
            _mBuilder = value;
            _assembly = _builtAssemblies[@namespace];
        }
        else
        {
            lock (_builtAssemblies) _assembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(_namespace), AssemblyBuilderAccess.Run).PushInto(_namespace, _builtAssemblies);
            lock (_builtModules) _mBuilder = _assembly.DefineDynamicModule(_namespace).PushInto(_namespace, _builtModules);
        }
    }

    /// <summary>
    /// Creates a new public class.
    /// </summary>
    /// <param name="name">Name of the new class.</param>
    /// <param name="baseType">Base type of the new class.</param>
    /// <param name="interfaces">
    /// Interfaces to implement by the new class.
    /// </param>
    /// <returns>
    /// A <see cref="TypeBuilder"/> that can be used to define members of the
    /// new class.
    /// </returns>
    public TypeBuilder NewType(string name, Type baseType, IEnumerable<Type> interfaces)
    {
        return _mBuilder.DefineType(GetName(name), (baseType.Attributes & ~TypeAttributes.VisibilityMask & ~TypeAttributes.Abstract) | TypeAttributes.Public, baseType, interfaces.ToArray());
    }

    /// <summary>
    /// Creates a new public class.
    /// </summary>
    /// <param name="name">Name of the new class.</param>
    /// <typeparam name="T">Base type of the new class.</typeparam>
    /// <param name="interfaces">
    /// Interfaces to implement by the new class. Can be set to null to
    /// implement no additional interface.
    /// </param>
    /// <returns>
    /// A <see cref="TypeBuilder{T}"/> that can be used to define members of the
    /// new class.
    /// </returns>
    public ITypeBuilder<T> NewType<T>(string name, IEnumerable<Type>? interfaces)
    {
        return new TypeBuilder<T>(_mBuilder.DefineType(GetName(name), (typeof(T).Attributes & ~TypeAttributes.VisibilityMask & ~TypeAttributes.Abstract) | TypeAttributes.Public, typeof(T), interfaces?.ToArray()));
    }

    /// <summary>
    /// Creates a new public class.
    /// </summary>
    /// <param name="name">Name of the new class.</param>
    /// <typeparam name="T">Base type of the new class.</typeparam>
    /// <returns>
    /// A <see cref="TypeBuilder{T}"/> that can be used to define members
    /// of the new class.
    /// </returns>
    public ITypeBuilder<T> NewType<T>(string name)
    {
        return NewType<T>(name, null);
    }

    /// <summary>
    /// Creates a new public class.
    /// </summary>
    /// <param name="name">Name of the new class.</param>
    /// <returns>
    /// A <see cref="TypeBuilder"/> that can be used to define members of
    /// the new class.
    /// </returns>
    public TypeBuilder NewClass(string name)
    {
        return NewType(name, typeof(object), Type.EmptyTypes);
    }

    /// <summary>
    /// Creates a new public class.
    /// </summary>
    /// <param name="name">Name of the new class.</param>
    /// <param name="interfaces">Interfaces to implement.</param>
    /// <returns>
    /// A <see cref="TypeBuilder"/> that can be used to define members of
    /// the new class.
    /// </returns>
    public TypeBuilder NewClass(string name, IEnumerable<Type> interfaces)
    {
        return NewType(name, typeof(object), interfaces);
    }

    /// <summary>
    /// Creates a new public class, specifying the base type or single
    /// interface to implement.
    /// </summary>
    /// <typeparam name="T">Base type or interface to implement.</typeparam>
    /// <param name="name">Name of the new class.</param>
    /// <returns>
    /// An <see cref="ITypeBuilder{T}"/> that can be used to define members
    /// of the new class.
    /// </returns>
    public ITypeBuilder<T> NewClass<T>(string name)
    {
        var typeAttr = (typeof(T).Attributes & ~TypeAttributes.VisibilityMask & ~TypeAttributes.Abstract & ~TypeAttributes.ClassSemanticsMask) | TypeAttributes.Public;
        return typeof(T).IsInterface
            ? new TypeBuilder<T>(_mBuilder.DefineType(GetName(name), typeAttr, typeof(object), [typeof(T)]), false)
            : new TypeBuilder<T>(_mBuilder.DefineType(GetName(name), typeAttr, typeof(T), Type.EmptyTypes), true);
    }

    private string GetName(string name)
    {
        StringBuilder? nme = new();
        nme.Append($"{_namespace}.{name.OrNull() ?? throw new ArgumentNullException(nameof(name))}");
        if (_useGuid) nme.Append($"_{Guid.NewGuid().ToString().Replace("-", string.Empty)}");
        return nme.ToString();
    }
}
