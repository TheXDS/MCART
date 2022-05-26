﻿/*
TypeFactory.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using TheXDS.MCART.Component;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Types;

/// <summary>
/// Fábrica de tipos. Permite definir y compilar nuevos tipos en Runtime.
/// </summary>
public class TypeFactory : IExposeAssembly
{
    private static readonly Dictionary<string, ModuleBuilder> _builtModules = new();
    private static readonly Dictionary<string, AssemblyBuilder> _builtAssemblies = new();

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
    public TypeFactory() : this("TheXDS.MCART.Types._Generated") { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="TypeFactory"/>.
    /// </summary>
    /// <param name="namespace"></param>
    public TypeFactory(string @namespace) : this(@namespace, true) { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="TypeFactory"/>.
    /// </summary>
    /// <param name="useGuid">
    /// <see langword="true"/> para adjuntar un Guid al final del nombre de
    /// los tipos generados por medio de este <see cref="TypeFactory"/>.
    /// </param>
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
    public TypeFactory(string @namespace, bool useGuid)
    {
        _namespace = @namespace;
        _useGuid = useGuid;
        if (_builtModules.ContainsKey(@namespace))
        {
            _mBuilder = _builtModules[@namespace];
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
    /// Crea una nueva clase pública que implementa el patrón ViewModel por
    /// medio de una clase base <see cref="NotifyPropertyChanged"/>, y que
    /// incluirá todas las propiedades del tipo <typeparamref name="TModel"/>
    /// como propiedades con notificación de cambio de valor.
    /// </summary>
    /// <typeparam name="TModel">
    /// Modelo para el cual crear la clase con patrón ViewModel.
    /// </typeparam>
    /// <param name="interfaces">
    /// Colección de interfaces adicionales a implementar por el tipo final.
    /// </param>
    /// <returns>
    /// Un <see cref="TypeBuilder"/> por medio del cual se podrá definir a
    /// los miembros de la nueva clase.
    /// </returns>
    public ITypeBuilder<INotifyPropertyChanged> CreateNpcClass<TModel>(IEnumerable<Type>? interfaces)
        where TModel : notnull, new()
    {
        ITypeBuilder<NotifyPropertyChanged> t = NewType<NotifyPropertyChanged>($"{typeof(TModel).Name}Npc", interfaces);
        foreach (var p in typeof(TModel).GetProperties().Where(p => p.CanRead && p.CanWrite))
        {
            ((ITypeBuilder<NotifyPropertyChangeBase>)t).AddNpcProperty(p.Name, p.PropertyType);
        }
        return t;
    }

    /// <summary>
    /// Crea una nueva clase pública que implementa el patrón ViewModel por
    /// medio de una clase base <see cref="NotifyPropertyChanged"/>, y que
    /// incluirá todas las propiedades del tipo <typeparamref name="TModel"/>
    /// como propiedades con notificación de cambio de valor.
    /// </summary>
    /// <typeparam name="TModel">
    /// Modelo para el cual crear la clase con patrón ViewModel.
    /// </typeparam>
    /// <returns>
    /// Un <see cref="TypeBuilder"/> por medio del cual se podrá definir a
    /// los miembros de la nueva clase.
    /// </returns>
    public ITypeBuilder<INotifyPropertyChanged> CreateNpcClass<TModel>()
        where TModel : notnull, new()
    {
        return CreateNpcClass<TModel>(null);
    }

    private string GetName(string name)
    {
        StringBuilder? nme = new();
        nme.Append($"{_namespace}.{name.OrNull() ?? throw new ArgumentNullException(nameof(name))}");
        if (_useGuid) nme.Append($"_{Guid.NewGuid().ToString().Replace("-", string.Empty)}");
        return nme.ToString();
    }
}
