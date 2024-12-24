/*
TypeFactory.cs

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
using System.Reflection.Emit;
using System.Text;
using TheXDS.MCART.Misc;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types;

/// <summary>
/// Fábrica de tipos. Permite definir y compilar nuevos tipos en Runtime.
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
    /// Obtiene una referencia al ensamblado dinámico generado en el cual 
    /// se cargarán los tipos construidos por medio de este
    /// <see cref="TypeFactory"/>.
    /// </summary>
    public Assembly Assembly => _assembly;

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="TypeFactory"/>.
    /// </summary>
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public TypeFactory() : this("TheXDS.MCART.Types._Generated") { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="TypeFactory"/>.
    /// </summary>
    /// <param name="namespace"></param>
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public TypeFactory(string @namespace) : this(@namespace, true) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="TypeFactory"/>.
    /// </summary>
    /// <param name="useGuid">
    /// <see langword="true"/> para adjuntar un Guid al final del nombre de
    /// los tipos generados por medio de este <see cref="TypeFactory"/>.
    /// </param>
    [RequiresDynamicCode(AttributeErrorMessages.MethodCallsDynamicCode)]
    public TypeFactory(bool useGuid) : this()
    {
        _useGuid = useGuid;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="TypeFactory"/>.
    /// </summary>
    /// <param name="namespace">
    /// Espacio de nombres a utilizar para los tipos a construir.
    /// </param>
    /// <param name="useGuid">
    /// <see langword="true"/> para adjuntar un Guid al final del nombre de
    /// los tipos generados por medio de este <see cref="TypeFactory"/>.
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
    /// Crea una nueva clase pública.
    /// </summary>
    /// <param name="name">Nombre de la nueva clase.</param>
    /// <param name="baseType">Tipo base de la nueva clase.</param>
    /// <param name="interfaces">
    /// Interfaces a implementar por la nueva clase.
    /// </param>
    /// <returns>
    /// Un <see cref="TypeBuilder"/> por medio del cual se podrá definir a
    /// los miembros de la nueva clase.
    /// </returns>
    public TypeBuilder NewType(string name, Type baseType, IEnumerable<Type> interfaces)
    {
        return _mBuilder.DefineType(GetName(name), (baseType.Attributes & ~TypeAttributes.VisibilityMask & ~TypeAttributes.Abstract) | TypeAttributes.Public, baseType, interfaces.ToArray());
    }

    /// <summary>
    /// Crea una nueva clase pública.
    /// </summary>
    /// <param name="name">Nombre de la nueva clase.</param>
    /// <typeparam name="T">Tipo base de la nueva clase.</typeparam>
    /// <param name="interfaces">
    /// Interfaces a implementar por la nueva clase. Puede establecerse en
    /// <see langword="null"/> para no implementar ninguna interfaz
    /// adicional.
    /// </param>
    /// <returns>
    /// Un <see cref="TypeBuilder{T}"/> por medio del cual se podrá definir a
    /// los miembros de la nueva clase.
    /// </returns>
    public ITypeBuilder<T> NewType<T>(string name, IEnumerable<Type>? interfaces)
    {
        return new TypeBuilder<T>(_mBuilder.DefineType(GetName(name), (typeof(T).Attributes & ~TypeAttributes.VisibilityMask & ~TypeAttributes.Abstract) | TypeAttributes.Public, typeof(T), interfaces?.ToArray()));
    }

    /// <summary>
    /// Crea una nueva clase pública.
    /// </summary>
    /// <param name="name">Nombre de la nueva clase.</param>
    /// <typeparam name="T">Tipo base de la nueva clase.</typeparam>
    /// <returns>
    /// Un <see cref="TypeBuilder{T}"/> por medio del cual se podrá definir a
    /// los miembros de la nueva clase.
    /// </returns>
    public ITypeBuilder<T> NewType<T>(string name)
    {
        return NewType<T>(name, null);
    }

    /// <summary>
    /// Crea una nueva clase pública.
    /// </summary>
    /// <param name="name">Nombre de la nueva clase.</param>
    /// <returns>
    /// Un <see cref="TypeBuilder"/> por medio del cual se podrá definir a
    /// los miembros de la nueva clase.
    /// </returns>
    public TypeBuilder NewClass(string name)
    {
        return NewType(name, typeof(object), Type.EmptyTypes);
    }

    /// <summary>
    /// Crea una nueva clase pública.
    /// </summary>
    /// <param name="name">Nombre de la nueva clase.</param>
    /// <param name="interfaces">
    /// Interfaces a implementar por la nueva clase.
    /// </param>
    /// <returns>
    /// Un <see cref="TypeBuilder"/> por medio del cual se podrá definir a
    /// los miembros de la nueva clase.
    /// </returns>
    public TypeBuilder NewClass(string name, IEnumerable<Type> interfaces)
    {
        return NewType(name, typeof(object), interfaces);
    }

    /// <summary>
    /// Crea una nueva clase pública, especificando el tipo base o interfaz única de la misma.
    /// </summary>
    /// <typeparam name="T">Tipo base o interfaz a implementar.</typeparam>
    /// <param name="name">Nombre de la nueva clase.</param>
    /// <returns>
    /// Un <see cref="ITypeBuilder{T}"/> por medio del cual se podrá definir a
    /// los miembros de la nueva clase.
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
