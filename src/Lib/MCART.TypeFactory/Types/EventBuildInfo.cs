/*
PropertyBuildInfo.cs

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

using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Types.Extensions;
using static System.Reflection.MethodAttributes;

namespace TheXDS.MCART.Types;

/// <summary>
/// Contiene información sobre la definición de eventos en tipos construidos en
/// Runtime.
/// </summary>
public class EventBuildInfo
{
    /// <summary>
    /// Contiene el nombre definido para el evento representado por esta
    /// instancia.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Obtiene una referencia al campo utilizado para contener la referencia
    /// activa del manejador de eventos a llamar cuando se produzca el evento.
    /// </summary>
    public FieldBuilder HandlerField { get; }

    /// <summary>
    /// Obtiene una referencia a un método a utilizar para generar el evento
    /// representado por esta instancia.
    /// </summary>
    /// <remarks>
    /// El delegado representado por esta propiedad será compatible con la
    /// firma <see cref="Action{T1, T2}"/> donde el primer argumento de tipo
    /// será un tipo en particular u <see cref="object"/>, y el segundo será 
    /// <see cref="EventArgs"/> o un tipo que derive de esta clase.
    /// </remarks>
    public MethodBuilder RaiseMethod { get; }

    /// <summary>
    /// Obtiene una referencia al método utilizado para subscribir un manejador
    /// de eventos al evento representado por esta instancia.
    /// </summary>
    /// <remarks>
    /// El delegado representado por esta propiedad será compatible con la
    /// firma <see cref="Action{T}"/> donde el primer argumento de tipo
    /// será <see cref="EventArgs"/> o un tipo que derive de esta clase.
    /// </remarks>
    public MethodBuilder AddMethod { get; }

    /// <summary>
    /// Obtiene una referencia al método utilizado para remover la subscripción
    /// de un manejador de eventos al evento representado por esta instancia.
    /// </summary>
    /// <remarks>
    /// El delegado representado por esta propiedad será compatible con la
    /// firma <see cref="Action{T}"/> donde el primer argumento de tipo
    /// será <see cref="EventArgs"/> o un tipo que derive de esta clase.
    /// </remarks>
    public MethodBuilder RemoveMethod { get; }

    private EventBuildInfo(string name, FieldBuilder handlerField, MethodBuilder raiseMethod, MethodBuilder addMethod, MethodBuilder removeMethod)
    {
        HandlerField = handlerField;
        RaiseMethod = raiseMethod;
        AddMethod = addMethod;
        RemoveMethod = removeMethod;
        Name = name;
    }

    /// <summary>
    /// Crea un nuevo evento en el <see cref="TypeBuilder"/> especificado,
    /// agregando los métodos accesorios requeridos por el mismo.
    /// </summary>
    /// <typeparam name="TEventHandler">
    /// Delegado del manejador de eventos. Debe seguir la firma estándar de un
    /// manejador de eventos, es decir, debe ser un método con tipo de retorno
    /// <see langword="void"/>, y debe contener un argumento de tipo
    /// <typeparamref name="TSender"/> (generalmente, se prefiere el tipo
    /// <see cref="object"/>) y un argumento de tipo
    /// <typeparamref name="TEventArgs"/>.
    /// </typeparam>
    /// <typeparam name="TSender">
    /// Tipo del objeto que genera el evento. Puede utilizarse
    /// <see cref="object"/>.
    /// </typeparam>
    /// <typeparam name="TEventArgs">
    /// Tipo de argumentos de evento a pasar cuando se produzca el evento.
    /// </typeparam>
    /// <param name="builder">
    /// <see cref="TypeBuilder"/> en el cual se creará el nuevo evento y sus
    /// métodos auxiliares requeridos.
    /// </param>
    /// <param name="name">Nombre del nuevo evento.</param>
    /// <returns>
    /// Un <see cref="EventBuildInfo"/> que contiene información sobre el
    /// evento que ha sido definido.
    /// </returns>
    public static EventBuildInfo Create<TEventHandler, TSender, TEventArgs>(TypeBuilder builder, string name) where TEventHandler : Delegate where TEventArgs : EventArgs
    {
        var field = builder.DefineField($"_{name}_EventHandler", typeof(TEventHandler), FieldAttributes.Private);
        var evt = builder.DefineEvent(name, EventAttributes.None, typeof(TEventHandler));
        var raiseMethod = CreateRaiseMethod<TEventHandler, TSender, TEventArgs>(name, builder, field);
        var addMethod = CreateAddEventMethod<TEventHandler>(builder, name, field);
        var removeMethod = CreateRemoveEventMethod<TEventHandler>(builder, name, field);

        evt.SetRaiseMethod(raiseMethod);
        evt.SetAddOnMethod(addMethod);
        evt.SetRemoveOnMethod(removeMethod);

        return new(name, field, raiseMethod, addMethod, removeMethod);
    }

    private static MethodBuilder CreateAddEventMethod<TEventHandler>(TypeBuilder tb, string name, FieldInfo evtHandlerField)
    {
        var method = tb.DefineMethod($"add_{name}", Public | SpecialName | HideBySig, null, new Type[] { typeof(TEventHandler) });
        method.GetILGenerator().LoadArg0().LoadArg1().StoreField(evtHandlerField).Return();
        return method;
    }

    private static MethodBuilder CreateRemoveEventMethod<TEventHandler>(TypeBuilder tb, string name, FieldInfo evtHandlerField)
    {
        var method = tb.DefineMethod($"remove_{name}", Public | SpecialName | HideBySig, null, new Type[] { typeof(TEventHandler) });
        method.GetILGenerator().LoadArg0().LoadNull().StoreField(evtHandlerField).Return();
        return method;
    }

    private static MethodBuilder CreateRaiseMethod<TEventHandler, TSender, TEventArgs>(string name, TypeBuilder tb, FieldInfo field) where TEventHandler : Delegate
    {
        MethodBuilder method = tb.DefineMethod($"Raise{name}", Public, null, new Type[] { typeof(TSender), typeof(TEventArgs) });

        var il = method.GetILGenerator();

        il
            .LoadArg0()
            .LoadField(field)
            .Duplicate()
            .BranchTrueNewLabel(out var raiseLabel)
            .Pop().Return();
        il
            .PutLabel(raiseLabel)
            .LoadArg1()
            .LoadArg2()
            .CallVirt(typeof(TEventHandler).GetMethod("Invoke")!)
            .Return();
        return method;
    }
}
