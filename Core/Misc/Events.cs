//
//  Events.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
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
    /// Incluye información de evento para cualquier clase con eventos de recepción de datos.
    /// </summary>
    public class IncommingDataEventArgs : EventArgs
    {
        /// <summary>
        /// Crea una nueva instancia de este objeto con los datos recibidos.
        /// </summary>
        /// <param name="data">Colección de <see cref="byte"/> con los datos recibidos.</param>
        public IncommingDataEventArgs(byte[] data) { Data = data; }
		/// <summary>
		/// Obtiene un arreglo de <see cref="byte"/> con los datos recibidos.
		/// </summary>
		public readonly byte[] Data;
    }

    /// <summary>
    /// Incluye información de evento para cualquier clase con eventos de excepción.
    /// </summary>
    public class ExceptionEventArgs : EventArgs
    {

        /// <summary>
        /// Obtiene la excepción generada en el código que invocó este evento.
        /// </summary>
        public readonly Exception Exception;

        /// <summary>
        /// Crea una nueva instancia de este objeto con la excepción especificada.
        /// </summary>
        /// <param name="ex"><see cref="System.Exception"/> generado en el código.</param>
        public ExceptionEventArgs(Exception ex)
        {
            Exception = ex;
        }
    }

    /// <summary>
    /// Incluye informaicón de evento para cualquier clase con eventos que incluyan tipos de valor.
    /// </summary>
    /// <typeparam name="T">Tipo del valor almacenado por esta instancia.</typeparam>
    public class ValueEventArgs<T> : EventArgs
    {

        /// <summary>
        /// Devuelve el valor asociado a este evento.
        /// </summary>
        /// <returns>Un valor de tipo <typeparamref name="T"/> con el valor asociado a este evento.</returns>
        public readonly T Value;

        /// <summary>
        /// Crea una nueva instancia de este objeto con el valor provisto.
        /// </summary>
        /// <param name="x">Valor asociado al evento generado.</param>
        public ValueEventArgs(T x) { Value = x; }
    }

    /// <summary>
    /// Incluye informaicón de evento para cualquier clase con eventos que reporten el progreso de una operación.
    /// </summary>
    public class ProgressionEventArgs : ValueEventArgs<double>
    {

        /// <summary>
        /// Devuelve una descripción rápida del estado de progreso.
        /// </summary>
        /// <returns>Una <see cref="string"/> con un mensaje que describe el estado de progreso del evento.</returns>
        public string HelpText { get; }

        /// <summary>
        /// Crea una nueva instancia de este objeto con los datos provistos.
        /// </summary>
        /// <param name="x">
		/// Valor de progreso. Debe ser un <see cref="double"/> entre 0.0 y 1.0 o los valores <see cref="double.NaN"/>,
		/// <see cref="double.PositiveInfinity"/> o <see cref="double.NegativeInfinity"/>.
		/// </param>
        /// <param name="y">Opcional. Descripción del estado de progreso que generó el evento.</param>
        public ProgressionEventArgs(double x, string y = null) : base(x)
        {
            if (x > 1 || x < 0) throw new ArgumentOutOfRangeException();
            HelpText = y;
        }
    }

    /// <summary>
    /// Contiene información de evento para cualquier clase con eventos donde se guarde información.
    /// </summary>
    public class ItemSaveEventArgs : CancelEventArgs
    {

        /// <summary>
        /// Crea una nueva instancia de esta clase con la información de evento provista.
        /// </summary>
        /// <param name="Item">Objeto que ha sido guardado</param>
        /// <param name="IsItemNew">Determina si el objeto es un nuevo objeto o si ha sido editado.</param>
        /// <param name="Cancelled">Determina si este evento se cancelará de forma predeterminada.</param>
        public ItemSaveEventArgs(object Item, bool IsItemNew, bool Cancelled = false) : base(Cancelled)
        {
            this.Item = Item;
            this.IsItemNew = IsItemNew;
        }

        /// <summary>
        /// Obtiene el elemento que ha sido creado/editado.
        /// </summary>
        /// <returns>Una referencia de instancia al objeto creado/editado.</returns>
        public readonly object Item;

        /// <summary>
        /// Obtiene un valor que indica si el elemento es nuevo o ha sido editado.
        /// </summary>
        /// <returns><c>true</c> si el elemento es nuevo, <c>false</c> si el elemento fue editado.</returns>
        public readonly bool IsItemNew;
    }
}