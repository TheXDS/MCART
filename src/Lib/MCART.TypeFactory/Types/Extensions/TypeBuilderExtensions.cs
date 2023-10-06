/*
TypeBuilderExtensions.cs

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

using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using TheXDS.MCART.Attributes;
using static System.Reflection.MethodAttributes;
using Errors = TheXDS.MCART.Resources.TypeFactoryErrors;

namespace TheXDS.MCART.Types.Extensions;

/// <summary>
/// Contiene extensiones útiles para la generación de miembros por medio
/// de la clase <see cref="TypeBuilder"/>.
/// </summary>
public static class TypeBuilderExtensions
{
    /// <summary>
    /// Inicializa una nueva instancia del tipo en runtime especificado.
    /// </summary>
    /// <returns>La nueva instancia del tipo especificado.</returns>
    /// <param name="tb">
    /// <see cref="TypeBuilder"/> desde el cual instanciar un nuevo objeto.
    /// </param>
    [DebuggerStepThrough]
    [Sugar]
    public static object New(this TypeBuilder tb)
    {
        if (!tb.IsCreated()) tb.CreateType();
        return TypeExtensions.New(tb);
    }

    /// <summary>
    /// Determina si el método es reemplazable.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> si el método es reemplazable
    /// <see langword="false"/> en caso contrario, o
    /// <see langword="null"/> si el método no existe en la clase base
    /// del constructor de tipos.
    /// </returns>
    /// <param name="tb">
    /// Constructor de tipo sobre el cual ejecutar la consulta.
    /// </param>
    /// <param name="method">Nombre del método a buscar.</param>
    /// <param name="args">Tipo de argumentos del método a buscar.</param>
    public static bool? Overridable(this TypeBuilder tb, string method, params Type[] args)
    {
        MethodInfo? bm = tb.BaseType?.GetMethod(method, args);
        if (bm is null) return null;
        return bm.IsVirtual || bm.IsAbstract;
    }

    /// <summary>
    /// Agrega una propiedad automática al tipo.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad
    /// automática.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
    /// <param name="virtual">
    /// Si se establece en <see langword="true"/>, la propiedad será
    /// definida como virtual, por lo que podrá ser reemplazada en una
    /// clase derivada. 
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddAutoProperty(this TypeBuilder tb, string name, Type type, MemberAccess access, bool @virtual)
    {
        PropertyBuildInfo p = AddProperty(tb, name, type, true, access, @virtual).WithBackingField(out FieldBuilder? field);
        p.Setter!
            .SetField(field, il => il.LoadArg1())
            .Return();
        return new PropertyBuildInfo(tb, p.Member, field);
    }

    /// <summary>
    /// Agrega una propiedad automática al tipo.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad
    /// automática.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddAutoProperty(this TypeBuilder tb, string name, Type type, MemberAccess access)
    {
        return AddAutoProperty(tb, name, type, access, false);
    }

    /// <summary>
    /// Agrega una propiedad automática pública al tipo.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad
    /// automática.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddAutoProperty(this TypeBuilder tb, string name, Type type)
    {
        return AddAutoProperty(tb, name, type, MemberAccess.Public);
    }

    /// <summary>
    /// Agrega una propiedad automática pública al tipo.
    /// </summary>
    /// <typeparam name="T">Tipo de la nueva propiedad.</typeparam>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad
    /// automática.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddAutoProperty<T>(this TypeBuilder tb, string name)
    {
        return AddAutoProperty(tb, name, typeof(T));
    }

    /// <summary>
    /// Agrega una sustitución para el método especificado.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad
    /// automática.
    /// </param>
    /// <param name="method">
    /// Método a sustituir. Debe existir en el tipo base.
    /// </param>
    /// <returns></returns>
    public static MethodBuildInfo AddOverride(this TypeBuilder tb, MethodInfo method)
    {
        MethodBuilder newMethod = tb.DefineMethod(method.Name, GetNonAbstract(method), method.IsVoid() ? null : method.ReturnType, method.GetParameters().Select(p => p.ParameterType).ToArray());
        tb.DefineMethodOverride(newMethod, method);
        return new MethodBuildInfo(tb, newMethod);
    }

    /// <summary>
    /// Agrega una propiedad al tipo sin implementaciones de
    /// <see langword="get"/> ni <see langword="set"/> establecidas.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="writable">
    /// <see langword="true"/> para crear una propiedad que contiene
    /// accesor de escritura (accesor <see langword="set"/>),
    /// <see langword="false"/> para no incluir un accesor de escritura en
    /// la propiedad.
    /// </param>
    /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
    /// <param name="virtual">
    /// Si se establece en <see langword="true"/>, la propiedad será
    /// definida como virtual, por lo que podrá ser reemplazada en una
    /// clase derivada. 
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    /// <remarks>
    /// La propiedad generada requerirá que se implementen los accesores
    /// antes de construir el tipo.
    /// </remarks>
    public static PropertyBuildInfo AddProperty(this TypeBuilder tb, string name, Type type, bool writable, MemberAccess access, bool @virtual)
    {
        return PropertyBuildInfo.Create(tb, name, type, writable, access, @virtual);
    }

    /// <summary>
    /// Agrega una propiedad al tipo sin implementaciones de
    /// <see langword="get"/> ni <see langword="set"/> establecidas.
    /// </summary>
    /// <typeparam name="T">Tipo de la nueva propiedad.</typeparam>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="writable">
    /// <see langword="true"/> para crear una propiedad que contiene
    /// accesor de escritura (accesor <see langword="set"/>),
    /// <see langword="false"/> para no incluir un accesor de escritura en
    /// la propiedad.
    /// </param>
    /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
    /// <param name="virtual">
    /// Si se establece en <see langword="true"/>, la propiedad será
    /// definida como virtual, por lo que podrá ser reemplazada en una
    /// clase derivada. 
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    /// <remarks>
    /// La propiedad generada requerirá que se implementen los accesores
    /// antes de construir el tipo.
    /// </remarks>
    public static PropertyBuildInfo AddProperty<T>(this TypeBuilder tb, string name, bool writable, MemberAccess access, bool @virtual)
    {
        return AddProperty(tb, name, typeof(T), writable, access, @virtual);
    }

    /// <summary>
    /// Agrega una propiedad al tipo sin implementaciones de
    /// <see langword="get"/> ni <see langword="set"/> establecidas.
    /// </summary>
    /// <typeparam name="T">Tipo de la nueva propiedad.</typeparam>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="writable">
    /// <see langword="true"/> para crear una propiedad que contiene
    /// accesor de escritura (accesor <see langword="set"/>),
    /// <see langword="false"/> para no incluir un accesor de escritura en
    /// la propiedad.
    /// </param>
    /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    /// <remarks>
    /// La propiedad generada requerirá que se implementen los accesores
    /// antes de construir el tipo.
    /// </remarks>
    public static PropertyBuildInfo AddProperty<T>(this TypeBuilder tb, string name, bool writable, MemberAccess access)
    {
        return AddProperty(tb, name, typeof(T), writable, access, false);
    }

    /// <summary>
    /// Agrega una propiedad al tipo sin implementaciones de
    /// <see langword="get"/> ni <see langword="set"/> establecidas.
    /// </summary>
    /// <typeparam name="T">Tipo de la nueva propiedad.</typeparam>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="writable">
    /// <see langword="true"/> para crear una propiedad que contiene
    /// accesor de escritura (accesor <see langword="set"/>),
    /// <see langword="false"/> para no incluir un accesor de escritura en
    /// la propiedad.
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    /// <remarks>
    /// La propiedad generada requerirá que se implementen los accesores
    /// antes de construir el tipo.
    /// </remarks>
    public static PropertyBuildInfo AddProperty<T>(this TypeBuilder tb, string name, bool writable)
    {
        return AddProperty(tb, name, typeof(T), writable, MemberAccess.Public, false);
    }

    /// <summary>
    /// Agrega una propiedad al tipo sin implementaciones de
    /// <see langword="get"/> ni <see langword="set"/> establecidas.
    /// </summary>
    /// <typeparam name="T">Tipo de la nueva propiedad.</typeparam>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    /// <remarks>
    /// La propiedad generada requerirá que se implementen los accesores
    /// antes de construir el tipo.
    /// </remarks>
    public static PropertyBuildInfo AddProperty<T>(this TypeBuilder tb, string name)
    {
        return AddProperty(tb, name, typeof(T), true, MemberAccess.Public, false);
    }

    /// <summary>
    /// Agrega una propiedad computada al tipo.
    /// </summary>
    /// <typeparam name="T">Tipo de la nueva propiedad.</typeparam>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="getterDefinition">
    /// Acción que implementará las instrucciones a ejecutar dentro del
    /// método asociado al accesor <see langword="get"/> de la propiedad 
    /// computada.
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddComputedProperty<T>(this TypeBuilder tb, string name, Action<ILGenerator> getterDefinition)
    {
        return AddComputedProperty(tb, name, typeof(T), getterDefinition);
    }

    /// <summary>
    /// Agrega una propiedad computada al tipo.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="getterDefinition">
    /// Acción que implementará las instrucciones a ejecutar dentro del
    /// método asociado al accesor <see langword="get"/> de la propiedad 
    /// computada.
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddComputedProperty(this TypeBuilder tb, string name, Type type, Action<ILGenerator> getterDefinition)
    {
        PropertyBuildInfo prop = AddProperty(tb, name, type, false, MemberAccess.Public, false);
        getterDefinition(prop.Getter!);
        return prop;
    }

    /// <summary>
    /// Agrega una propiedad con un valor constante.
    /// </summary>
    /// <typeparam name="T">Tipo de la nueva propiedad.</typeparam>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="value">Valor constante a asignar.</param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddConstantProperty<T>(this TypeBuilder tb, string name, T value)
    {
        return AddConstantProperty(tb, name, typeof(T), value);
    }

    /// <summary>
    /// Agrega una propiedad con un valor constante.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="value">Valor constante a asignar.</param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddConstantProperty(this TypeBuilder tb, string name, Type type, object? value)
    {
        PropertyBuildInfo prop = AddProperty(tb, name, type, false, MemberAccess.Public, false);
        prop.Getter!.LoadConstant(type, value).Return();
        return prop;
    }

    /// <summary>
    /// Agrega una propiedad pública de solo escritura al tipo.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddWriteOnlyProperty(this TypeBuilder tb, string name, Type type)
    {
        return AddWriteOnlyProperty(tb, name, type, MemberAccess.Public, false);
    }

    /// <summary>
    /// Agrega una propiedad de solo escritura al tipo.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddWriteOnlyProperty(this TypeBuilder tb, string name, Type type, MemberAccess access)
    {
        return AddWriteOnlyProperty(tb, name, type, access, false);
    }

    /// <summary>
    /// Agrega una propiedad pública de solo escritura al tipo.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="virtual">
    /// Si se establece en <see langword="true"/>, la propiedad será
    /// definida como virtual, por lo que podrá ser reemplazada en una
    /// clase derivada. 
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddWriteOnlyProperty(this TypeBuilder tb, string name, Type type, bool @virtual)
    {
        return AddWriteOnlyProperty(tb, name, type, MemberAccess.Public, @virtual);
    }

    /// <summary>
    /// Agrega una propiedad de solo escritura al tipo.
    /// </summary>
    /// <param name="tb">
    /// Constructor del tipo en el cual crear la nueva propiedad.
    /// </param>
    /// <param name="name">Nombre de la nueva propiedad.</param>
    /// <param name="type">Tipo de la nueva propiedad.</param>
    /// <param name="access">Nivel de acceso de la nueva propiedad.</param>
    /// <param name="virtual">
    /// Si se establece en <see langword="true"/>, la propiedad será
    /// definida como virtual, por lo que podrá ser reemplazada en una
    /// clase derivada. 
    /// </param>
    /// <returns>
    /// Un <see cref="PropertyBuildInfo"/> que contiene información sobre
    /// la propiedad que ha sido construida.
    /// </returns>
    public static PropertyBuildInfo AddWriteOnlyProperty(this TypeBuilder tb, string name, Type type, MemberAccess access, bool @virtual)
    {
        return PropertyBuildInfo.CreateWriteOnly(tb, name, type, access, @virtual);
    }

    /// <summary>
    /// Inserta explícitamente un constructor público sin argumentos al
    /// tipo.
    /// </summary>
    /// <param name="tb">
    /// <see cref="TypeBuilder"/> en el cual se definirá el nuevo constructor.
    /// </param>
    /// <returns>
    /// Un <see cref="ILGenerator"/> que permite definir las instrucciones
    /// del constructor.
    /// </returns>
    public static ILGenerator AddPublicConstructor(this TypeBuilder tb) => AddPublicConstructor(tb, Type.EmptyTypes);

    /// <summary>
    /// Inserta explícitamente un constructor público al tipo,
    /// especificando los argumentos requeridos por el mismo.
    /// </summary>
    /// <param name="tb">
    /// <see cref="TypeBuilder"/> en el cual se definirá el nuevo
    /// constructor.
    /// </param>
    /// <param name="arguments">
    /// Arreglo con los tipos de argumentos aceptados por el nuevo
    /// constructor.
    /// </param>
    /// <returns>
    /// Un <see cref="ILGenerator"/> que permite definir las instrucciones
    /// del constructor.
    /// </returns>
    public static ILGenerator AddPublicConstructor(this TypeBuilder tb, params Type[] arguments)
    {
        return tb.DefineConstructor(Public | SpecialName | HideBySig | RTSpecialName, CallingConventions.HasThis, arguments).GetILGenerator();
    }

    /// <summary>
    /// Implementa explícitamente un método abstracto.
    /// </summary>
    /// <param name="tb">
    /// <see cref="TypeBuilder"/> en el cual se implementará de forma
    /// explícita el método abstracto.
    /// </param>
    /// <param name="method">
    /// Método abstracto a implementar. El <see cref="TypeBuilder"/>
    /// especificado debe implementar la interfaz en la cual está definido
    /// el método.
    /// </param>
    /// <returns>
    /// Un <see cref="MethodBuildInfo"/> que contiene información sobre
    /// el método que ha sido implementado de forma explícita.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Se produce si <paramref name="tb"/> no implementa o hereda la
    /// interfaz en la cual se ha definido el método
    /// <paramref name="method"/>, o si <paramref name="method"/> no es un
    /// miembro definido en una interfaz.
    /// </exception>
    public static MethodBuildInfo ExplicitImplementMethod(this TypeBuilder tb, MethodInfo method)
    {
        if (!method.DeclaringType?.IsInterface ?? true) throw Errors.IFaceMethodExpected();
        if (!tb.GetInterfaces().Contains(method.DeclaringType!)) throw Errors.InterfaceNotImplemented(method.DeclaringType!);

        MethodBuilder m = tb.DefineMethod($"{method.DeclaringType!.Name}.{method.Name}",
            Private | HideBySig | NewSlot | Virtual | Final,
            method.IsVoid() ? null : method.ReturnType,
            method.GetParameters().Select(p => p.ParameterType).ToArray());
        tb.DefineMethodOverride(m, method);

        return new MethodBuildInfo(tb, m);
    }

    /// <summary>
    /// Define un método que no devuelve valor.
    /// </summary>
    /// <param name="tb">
    /// <see cref="TypeBuilder"/> en el cual se creará el nuevo método.
    /// </param>
    /// <param name="name">Nombre del nuevo método.</param>
    /// <param name="parameterTypes">
    /// Tipo de los parámetros aceptados por el método.
    /// </param>
    /// <returns>
    /// Un <see cref="MethodBuilder"/> con el que se podrá definir al método.
    /// </returns>
    [Sugar]
    public static MethodBuilder DefineVoidMethod(this TypeBuilder tb, string name, params Type[] parameterTypes)
    {
        return tb.DefineMethod(name, Public, typeof(void), parameterTypes);
    }

    /// <summary>
    /// Define un método que devuelve un tipo especificado.
    /// </summary>
    /// <typeparam name="TResult">
    /// Tipo de resultado devuelto por el método.
    /// </typeparam>
    /// <param name="tb">
    /// <see cref="TypeBuilder"/> en el cual se creará el nuevo método.
    /// </param>
    /// <param name="name">Nombre del nuevo método.</param>
    /// <param name="parameterTypes">
    /// Tipo de los parámetros aceptados por el método.
    /// </param>
    /// <returns>
    /// Un <see cref="MethodBuilder"/> con el que se podrá definir al método.
    /// </returns>
    [Sugar]
    public static MethodBuilder DefineMethod<TResult>(this TypeBuilder tb, string name, params Type[] parameterTypes)
    {
        return tb.DefineMethod(name, Public, typeof(TResult), parameterTypes);
    }

    /// <summary>
    /// Agrega un evento al tipo.
    /// </summary>
    /// <param name="builder">
    /// <see cref="TypeBuilder"/> en el cual se creará el nuevo evento y sus
    /// métodos auxiliares requeridos.
    /// </param>
    /// <param name="name">Nombre del nuevo evento.</param>
    /// <returns>
    /// Un <see cref="EventBuildInfo"/> que contiene información sobre el
    /// evento que ha sido definido.
    /// </returns>
    public static EventBuildInfo AddEvent(this TypeBuilder builder, string name)
    {
        return AddEvent<EventHandler, EventArgs>(builder, name);
    }

    /// <summary>
    /// Agrega un evento al tipo.
    /// </summary>
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
    public static EventBuildInfo AddEvent<TEventArgs>(this TypeBuilder builder, string name) where TEventArgs : EventArgs
    {
        return AddEvent<EventHandler<TEventArgs>, TEventArgs>(builder, name);
    }

    /// <summary>
    /// Agrega un evento al tipo.
    /// </summary>
    /// <typeparam name="TEventHandler">
    /// Delegado del manejador de eventos. Debe seguir la firma estándar de un
    /// manejador de eventos, es decir, debe ser un método con tipo de retorno
    /// <see langword="void"/>, y debe contener un argumento de tipo
    /// <see cref="object"/>) y un argumento de tipo
    /// <typeparamref name="TEventArgs"/>.
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
    public static EventBuildInfo AddEvent<TEventHandler, TEventArgs>(this TypeBuilder builder, string name) where TEventHandler : Delegate where TEventArgs : EventArgs
    {
        return AddEvent<TEventHandler, object, TEventArgs>(builder, name);
    }

    /// <summary>
    /// Agrega un evento al tipo.
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
    public static EventBuildInfo AddEvent<TEventHandler, TSender, TEventArgs>(this TypeBuilder builder, string name) where TEventHandler : Delegate where TEventArgs : EventArgs
    {
        return EventBuildInfo.Create<TEventHandler, TSender, TEventArgs>(builder, name);
    }

    private static MethodAttributes GetNonAbstract(MethodInfo m)
    {
        int a = (int)m.Attributes;
        a &= ~(int)Abstract;
        a |= (int)Virtual;
        return (MethodAttributes)a;
    }
}
