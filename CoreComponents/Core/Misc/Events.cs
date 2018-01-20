/*
Events.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2018 César Andrés Morgan

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
using System.ComponentModel;

namespace TheXDS.MCART.Events
{
    /// <summary>
    /// Incluye información de evento para cualquier clase con eventos que 
    /// incluyan tipos de valor.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo del valor almacenado por esta instancia.
    /// </typeparam>
    public class ValueEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Devuelve el valor asociado a este evento.
        /// </summary>
        /// <returns>
        /// Un valor de tipo <typeparamref name="T"/> con el valor asociado a
        /// este evento.
        /// </returns>
        public T Value { get; }
        /// <summary>
        /// Inicializa una nueva instancia de este objeto con el valor provisto.
        /// </summary>
        /// <param name="value">Valor asociado al evento generado.</param>
        public ValueEventArgs(T value) { Value = value; }
    }
    /// <summary>
    /// Incluye información para cualquier evento que incluya tipos de valor y
    /// puedan ser cancelados.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo del valor almacenado por esta instancia.
    /// </typeparam>
    public class ValueChangingEventArgs<T> : CancelEventArgs
    {
        /// <summary>
        /// Devuelve el valor original asociado a este evento.
        /// </summary>
        /// <returns>
        /// Un valor de tipo <typeparamref name="T"/> con el valor asociado al
        /// evento.
        /// </returns>
        public T OldValue { get; }
        /// <summary>
        /// Devuelve el nuevo valor asociado a este evento.
        /// </summary>
        /// <returns>
        /// Un valor de tipo <typeparamref name="T"/> con el valor asociado al
        /// evento.
        /// </returns>
        public T NewValue { get; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ValueChangingEventArgs{T}"/> con el valor provisto.
        /// </summary>
        /// <param name="oldValue">
        /// Valor original asociado al evento generado.
        /// </param>
        /// <param name="newValue">
        /// Nuevo valor asociado al evento generado.
        /// </param>
        public ValueChangingEventArgs(T oldValue, T newValue) { OldValue = oldValue; NewValue = newValue; }
        /// <summary>
        /// Convierte explícitamente este 
        /// <see cref="ValueChangingEventArgs{T}"/> en un
        /// <see cref="ValueEventArgs{T}"/>.
        /// </summary>
        /// <param name="fromValue">Objeto a convertir.</param>
        /// <returns>
        /// Un <see cref="ValueEventArgs{T}"/> con la información pertinente
        /// del <see cref="ValueChangingEventArgs{T}"/> especificado.
        /// </returns>
        public static explicit operator ValueEventArgs<T>(ValueChangingEventArgs<T> fromValue) => new ValueEventArgs<T>(fromValue.NewValue);
    }
    /// <summary>
    /// Incluye información de evento para cualquier clase con eventos que 
    /// incluyan tipos de valor.
    /// </summary>
    public class ValueEventArgs : ValueEventArgs<object>
    {
        /// <summary>
        /// Inicializa una nueva instancia de este objeto con el valor provisto.
        /// </summary>
        /// <param name="value">Valor asociado al evento generado.</param>
        public ValueEventArgs(object value) : base(value) { }
    }
    /// <summary>
    /// Incluye información para cualquier evento que incluya tipos de valor y
    /// puedan ser cancelados.
    /// </summary>
    public class ValueChangingEventArgs : ValueChangingEventArgs<object>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase
        /// <see cref="ValueChangingEventArgs{T}"/> con el valor provisto.
        /// </summary>
        /// <param name="oldValue">
        /// Valor original asociado al evento generado.
        /// </param>
        /// <param name="newValue">
        /// Nuevo valor asociado al evento generado.
        /// </param>
        public ValueChangingEventArgs(object oldValue, object newValue) : base(oldValue, newValue) { }
    }
    /// <summary>
    /// Incluye información de evento para cualquier clase con eventos de
    /// logging (bitácora).
    /// </summary>
    public class LoggingEventArgs : ValueEventArgs<string>
    {
        /// <summary>
        /// Objeto relacionado a esta entrada de log.
        /// </summary>
        public object Subject { get; }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="LoggingEventArgs"/>, sin definir un objeto relacionado.
        /// </summary>
        /// <param name="message">Mensaje de esta entrada de log.</param>
        public LoggingEventArgs(string message) : base(message) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="LoggingEventArgs"/>, definiendo un objeto relacionado.
        /// </summary>
        /// <param name="subject">Objeto relacionado a esta entrada de log.</param>
        /// <param name="message">Mensaje de esta entrada de log.</param>
        public LoggingEventArgs(object subject, string message) : base(message) { Subject = subject; }
    }
    /// <summary>
    /// Incluye información de evento para cualquier clase con eventos de 
    /// recepción de datos.
    /// </summary>
    public class IncommingDataEventArgs : ValueEventArgs<byte[]>
    {
        /// <summary>
        /// Inicializa una nueva instancia de este objeto con los datos 
        /// recibidos.
        /// </summary>
        /// <param name="data">
        /// Colección de <see cref="byte"/> con los datos recibidos.
        /// </param>
        public IncommingDataEventArgs(byte[] data) : base(data) { }
    }
    /// <summary>
    /// Incluye información de evento para cualquier clase con eventos de 
    /// excepción.
    /// </summary>
    public class ExceptionEventArgs : ValueEventArgs<Exception>
    {
        /// <summary>
        /// Inicializa una nueva instancia de este objeto con la excepción 
        /// especificada.
        /// </summary>
        /// <param name="ex">
        /// <see cref="System.Exception"/> que se ha producido en el código.
        /// </param>
        public ExceptionEventArgs(Exception ex) : base(ex) { }
    }
    /// <summary>
    /// Incluye información de evento para cualquier clase con eventos que 
    /// reporten el progreso de una operación.
    /// </summary>
    public class ProgressionEventArgs : ValueEventArgs<double>
    {
        /// <summary>
        /// Devuelve una descripción rápida del estado de progreso.
        /// </summary>
        /// <returns>
        /// Un <see cref="string"/> con un mensaje que describe el estado de 
        /// progreso del evento.
        /// </returns>
        public string HelpText { get; }
        /// <summary>
        /// Inicializa una nueva instancia de este objeto con los datos
        /// provistos.
        /// </summary>
        /// <param name="x">
		/// Valor de progreso. Debe ser un <see cref="double"/> entre
        /// <c>0.0</c> y <c>1.0</c>, o los valores <see cref="double.NaN"/>,
		/// <see cref="double.PositiveInfinity"/> o 
        /// <see cref="double.NegativeInfinity"/>.
		/// </param>
        /// <param name="y">
        /// Parámetro opcional. Descripción del estado de progreso que generó el
        /// evento.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Se produce si <paramref name="x"/> no en un valor entre <c>0.0</c>
        /// y <c>1.0</c>.
        /// </exception>
        public ProgressionEventArgs(double x, string y = null) : base(x)
        {
            if (x > 1 || x < 0) throw new ArgumentOutOfRangeException();
            HelpText = y;
        }
    }
    /// <summary>
    /// Contiene información de evento para cualquier clase con eventos donde 
    /// se guardará información.
    /// </summary>
    public class ItemCreatingEventArgs<T> : CancelEventArgs
    {
        /// <summary>
        /// Obtiene el elemento que ha sido creado/editado.
        /// </summary>
        /// <returns>
        /// Una referencia de instancia al objeto creado/editado.
        /// </returns>
        public T Item { get; }
        /// <summary>
        /// Inicializa una nueva instancia de esta clase con la información de
        /// evento provista.
        /// </summary>
        /// <param name="item">Objeto que ha sido guardado.</param>
        public ItemCreatingEventArgs(T item) : this(item, false) { }
        /// <summary>
        /// Inicializa una nueva instancia de esta clase con la información de
        /// evento provista.
        /// </summary>
        /// <param name="item">Objeto que ha sido guardado.</param>
        /// <param name="cancel">
        /// Determina si este evento se cancelará de forma predeterminada.
        /// </param>
        public ItemCreatingEventArgs(T item, bool cancel) : base(cancel) { Item = item; }
        /// <summary>
        /// Convierte explícitamente un <see cref="ItemCreatingEventArgs{T}"/> en un
        /// <see cref="ItemCreatedEventArgs{T}"/>.
        /// </summary>
        /// <param name="fromValue">Objeto a convertir.</param>
        /// <returns>
        /// Un <see cref="ItemCreatedEventArgs{T}"/> con la misma información de
        /// evento que el <see cref="ItemCreatingEventArgs{T}"/> especificado.
        /// </returns>
        public static explicit operator ItemCreatedEventArgs<T>(ItemCreatingEventArgs<T> fromValue) => new ItemCreatedEventArgs<T>(fromValue.Item);
    }
    /// <summary>
    /// Contiene información de evento para cualquier clase con eventos donde 
    /// se guardó información.
    /// </summary>
    public class ItemCreatedEventArgs<T> : ValueEventArgs<T>
    {
        /// <summary>
        /// Inicializa una nueva instancia de esta clase con la información de
        /// evento provista.
        /// </summary>
        /// <param name="item">Objeto que ha sido guardado.</param>
        public ItemCreatedEventArgs(T item) : base(item) { }
    }
}