/*
TypeDetailsViewModel.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2023 César Andrés Morgan

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TheXDS.MCART.Types.Base;
using TheXDS.MCART.Types.Extensions;

namespace TheXDS.MCART.Dialogs.ViewModel;

/// <summary>
/// Contiene la lógica de control básica para la ventana de detalles de
/// tipos.
/// </summary>
public class TypeDetailsViewModel : NotifyPropertyChanged
{
    private Type? _type;

    /// <summary>
    /// Obtiene los tipos base del tipo especificado.
    /// </summary>
    public IEnumerable<Type> BaseTypes
    {
        get
        {
            Type? baseType = Type?.BaseType;
            while (baseType is not null)
            {
                yield return baseType;
                baseType = baseType.BaseType;
            }
        }
    }

    /// <summary>
    /// Crea una nueva instancia de la clase
    /// <see cref="TypeDetailsViewModel"/>.
    /// </summary>
    public static TypeDetailsViewModel Create => new();

    /// <summary>
    /// Obtiene una representación como cadena del valor predeterminado
    /// del tipo para el cual se están mostrando los detalles.
    /// </summary>
    public string DefaultValue => Type?.Default()?.ToString() ?? "null";

    /// <summary>
    /// Enumera todas las interfaces implementadas o heredadas del tipo
    /// para el cual se están mostrando los detalles.
    /// </summary>
    public IEnumerable<Type>? Inheritances => Type?.GetInterfaces();

    /// <summary>
    /// Enumera las interfaces heredadas del tipo para el cual se están
    /// mostrando los detalles dentro de una nueva instancia de la
    /// clase <see cref="TypeDetailsViewModel"/>.
    /// </summary>
    public IEnumerable<TypeDetailsViewModel>? InheritancesVm => Inheritances?.Select(p => new TypeDetailsViewModel(p));

    /// <summary>
    /// Obtiene un valor que indica si el tipo es instanciable con un
    /// constructor público sin argumentos.
    /// </summary>
    public bool Instantiable => Type?.IsInstantiable() ?? false;

    /// <summary>
    /// Obtiene un valor que indica si el tipo ha sido definido dentro
    /// de un ensamblado dinámico.
    /// </summary>
    public bool IsDynamic => Type?.Assembly.IsDynamic ?? false;

    /// <summary>
    /// Obtiene un valor que indica si el tipo es estático, es decir si
    /// es abstracto y sellado a la vez.
    /// </summary>
    public bool IsStatic => Type is not null && Type.IsAbstract && Type.IsSealed;

    /// <summary>
    /// Enumera de forma agrupara el árbol de miembros definidos dentro
    /// del tipo para el cual se están mostrando los detalles.
    /// </summary>
    public IEnumerable<IGrouping<MemberTypes, MemberInfo>>? MemberTree => Type?.GetMembers().GroupBy(p => p.MemberType);

    /// <summary>
    /// Obtiene un valor instanciado sin argumentos del tipo para el
    /// cual se están mostrando los detalles.
    /// </summary>
    public object? NewValue => Type?.IsInstantiable() ?? false ? Type?.New() : null;

    /// <summary>
    /// Obtiene o establece el tipo para el cual se están mostrando los
    /// detalles.
    /// </summary>
    public Type? Type
    {
        get => _type;
        set => Change(ref _type, value);
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="TypeDetailsViewModel"/>.
    /// </summary>
    public TypeDetailsViewModel() : this(null)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase 
    /// <see cref="TypeDetailsViewModel"/>.
    /// </summary>
    /// <param name="type">
    /// Tipo para el cual se mostrarán los detalles.
    /// </param>
    public TypeDetailsViewModel(Type? type)
    {
        Type = type;
        RegisterPropertyChangeBroadcast(nameof(Type),
            nameof(Inheritances),
            nameof(InheritancesVm),
            nameof(BaseTypes),
            nameof(MemberTree),
            nameof(DefaultValue),
            nameof(Instantiable),
            nameof(IsStatic),
            nameof(IsDynamic),
            nameof(NewValue));
    }
}
