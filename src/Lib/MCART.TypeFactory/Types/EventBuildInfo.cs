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
/// Contains information about event definitions in types built at runtime.
/// </summary>
public class EventBuildInfo
{
    /// <summary>
    /// The name defined for the event represented by this instance.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets a reference to the field used to hold the active event
    /// handler that is invoked when the event occurs.
    /// </summary>
    public FieldBuilder HandlerField { get; }

    /// <summary>
    /// Gets a reference to the method used to raise the event
    /// represented by this instance.
    /// </summary>
    /// <remarks>
    /// The delegate represented by this property will be compatible
    /// with the Action&lt;T1, T2&gt; signature where the first type
    /// argument will be a specific type or <see cref="object"/>, and
    /// the second will be <see cref="EventArgs"/> or a type derived
    /// from it.
    /// </remarks>
    public MethodBuilder RaiseMethod { get; }

    /// <summary>
    /// Gets a reference to the method used to subscribe an event
    /// handler to the event represented by this instance.
    /// </summary>
    /// <remarks>
    /// The delegate represented by this property will be compatible
    /// with the Action&lt;T&gt; signature where the type argument will
    /// be <see cref="EventArgs"/> or a type derived from it.
    /// </remarks>
    public MethodBuilder AddMethod { get; }

    /// <summary>
    /// Gets a reference to the method used to unsubscribe an event
    /// handler from the event represented by this instance.
    /// </summary>
    /// <remarks>
    /// The delegate represented by this property will be compatible
    /// with the Action&lt;T&gt; signature where the type argument will
    /// be <see cref="EventArgs"/> or a type derived from it.
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
    /// Creates a new event on the specified
    /// <see cref="TypeBuilder"/>, adding the required accessor
    /// methods.
    /// </summary>
    /// <typeparam name="TEventHandler">
    /// The event handler delegate. It must follow the standard
    /// event handler signature, i.e. a void method with a
    /// <typeparamref name="TSender"/> argument (commonly
    /// <see cref="object"/>) and a <typeparamref name="TEventArgs"/>
    /// argument.
    /// </typeparam>
    /// <typeparam name="TSender">
    /// The type of the object that raises the event. Can be
    /// <see cref="object"/>.
    /// </typeparam>
    /// <typeparam name="TEventArgs">
    /// The event argument type passed when the event occurs.
    /// </typeparam>
    /// <param name="builder">
    /// The <see cref="TypeBuilder"/> on which the event and its
    /// required accessor methods will be defined.
    /// </param>
    /// <param name="name">The name of the new event.</param>
    /// <returns>
    /// An <see cref="EventBuildInfo"/> containing information about
    /// the event that has been defined.
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
