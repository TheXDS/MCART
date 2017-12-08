//
//  Events.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  MCART is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  MCART is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.ComponentModel;

namespace MCART.Events
{
    /// <summary>
    /// Incluye información de evento para cualquier clase con eventos de
    /// logging (bitácora).
    /// </summary>
    public class LoggingEventArgs : ValueEventArgs<string>
    {
        /// <summary>
        /// Objeto relacionado a esta entrada de log.
        /// </summary>
        public readonly object Obj;
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="LoggingEventArgs"/>, sin definir un objeto relacionado.
        /// </summary>
        /// <param name="x">Mensaje de esta entrada de log.</param>
        public LoggingEventArgs(string x) : base(x) { }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="LoggingEventArgs"/>, definiendo un objeto relacionado.
        /// </summary>
        /// <param name="obj">Objeto relacionado a esta entrada de log.</param>
        /// <param name="x">Mensaje de esta entrada de log.</param>
        public LoggingEventArgs(object obj, string x) : base(x) { Obj = obj; }
    }

    /// <summary>
    /// Incluye información de evento para cualquier clase con eventos de 
    /// recepción de datos.
    /// </summary>
    public class IncommingDataEventArgs : EventArgs
    {
        /// <summary>
        /// Inicializa una nueva instancia de este objeto con los datos 
        /// recibidos.
        /// </summary>
        /// <param name="data">
        /// Colección de <see cref="byte"/> con los datos recibidos.
        /// </param>
        public IncommingDataEventArgs(byte[] data) { Data = data; }
        /// <summary>
        /// Obtiene un arreglo de <see cref="byte"/> con los datos recibidos.
        /// </summary>
        public readonly byte[] Data;
    }

    /// <summary>
    /// Incluye información de evento para cualquier clase con eventos de 
    /// excepción.
    /// </summary>
    public class ExceptionEventArgs : EventArgs
    {

        /// <summary>
        /// Obtiene la excepción generada en el código que invocó este evento.
        /// </summary>
        public readonly Exception Exception;

        /// <summary>
        /// Inicializa una nueva instancia de este objeto con la excepción 
        /// especificada.
        /// </summary>
        /// <param name="ex">
        /// <see cref="System.Exception"/> que se ha producido en el código.
        /// </param>
        public ExceptionEventArgs(Exception ex)
        {
            Exception = ex;
        }
    }

    /// <summary>
    /// Incluye informaicón de evento para cualquier clase con eventos que 
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
        public readonly T Value;

        /// <summary>
        /// Inicializa una nueva instancia de este objeto con el valor provisto.
        /// </summary>
        /// <param name="x">Valor asociado al evento generado.</param>
        public ValueEventArgs(T x) { Value = x; }
    }

    /// <summary>
    /// Incluye informaicón para cualquier evento que incluya tipos de valor y
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
        public readonly T OldValue;

        /// <summary>
        /// Devuelve el nuevo valor asociado a este evento.
        /// </summary>
        /// <returns>
        /// Un valor de tipo <typeparamref name="T"/> con el valor asociado al
        /// evento.
        /// </returns>
        public readonly T NewValue;

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
        /// Convierte implícitamente este 
        /// <see cref="ValueChangingEventArgs{T}"/> en un
        /// <see cref="ValueEventArgs{T}"/>.
        /// </summary>
        /// <param name="x">Objeto a convertir.</param>
        public static implicit operator ValueEventArgs<T>(ValueChangingEventArgs<T> x) => new ValueEventArgs<T>(x.NewValue);
    }

    /// <summary>
    /// Incluye informaicón de evento para cualquier clase con eventos que 
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
		/// Valor de progreso. Debe ser un <see cref="double"/> entre 0.0 y 1.0 
        /// o los valores <see cref="double.NaN"/>,
		/// <see cref="double.PositiveInfinity"/> o 
        /// <see cref="double.NegativeInfinity"/>.
		/// </param>
        /// <param name="y">
        /// Parámetro opcional. Descripción del estado de progreso que generó el
        /// evento.
        /// </param>
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
    public class ItemSavingEventArgs : CancelEventArgs
    {

        /// <summary>
        /// Inicializa una nueva instancia de esta clase con la información de
        /// evento provista.
        /// </summary>
        /// <param name="Item">Objeto que ha sido guardado.</param>
        /// <param name="IsItemNew">
        /// Determina si el objeto es un nuevo objeto o si ha sido editado.
        /// </param>
        /// <param name="Cancelled">
        /// Determina si este evento se cancelará de forma predeterminada.
        /// </param>
        public ItemSavingEventArgs(object Item, bool IsItemNew, bool Cancelled = false) : base(Cancelled)
        {
            this.Item = Item;
            this.IsItemNew = IsItemNew;
        }

        /// <summary>
        /// Obtiene el elemento que ha sido creado/editado.
        /// </summary>
        /// <returns>
        /// Una referencia de instancia al objeto creado/editado.
        /// </returns>
        public readonly object Item;

        /// <summary>
        /// Obtiene un valor que indica si el elemento es nuevo o ha sido 
        /// editado.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el elemento es nuevo, <c>false</c> si el elemento fue
        ///  editado.
        /// </returns>
        public readonly bool IsItemNew;

        /// <summary>
        /// Convierte implícitamente un <see cref="ItemSavingEventArgs"/> en un
        /// <see cref="ItemSavedEventArgs"/>.
        /// </summary>
        /// <param name="x">Objeto a convertir.</param>
        public static implicit operator ItemSavedEventArgs(ItemSavingEventArgs x) => new ItemSavedEventArgs(x.Item, x.IsItemNew);
    }
    /// <summary>
    /// Contiene información de evento para cualquier clase con eventos donde 
    /// se guardó información.
    /// </summary>
    public class ItemSavedEventArgs : EventArgs
    {

        /// <summary>
        /// Inicializa una nueva instancia de esta clase con la información de
        /// evento provista.
        /// </summary>
        /// <param name="item">Objeto que ha sido guardado.</param>
        /// <param name="wasNewItem">
        /// Determina si el objeto es un nuevo objeto o si ha sido editado.
        /// </param>
        public ItemSavedEventArgs(object item, bool wasNewItem)
        {
            Item = item;
            WasNewItem = wasNewItem;
        }

        /// <summary>
        /// Obtiene el elemento que ha sido creado/editado.
        /// </summary>
        /// <returns>
        /// Una referencia de instancia al objeto creado/editado.
        /// </returns>
        public readonly object Item;

        /// <summary>
        /// Obtiene un valor que indica si el elemento es nuevo o ha sido
        /// editado.
        /// </summary>
        /// <returns>
        /// <c>true</c> si el elemento es nuevo, <c>false</c> si el elemento fue
        /// editado.
        /// </returns>
        public readonly bool WasNewItem;
    }
}