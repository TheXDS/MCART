/*
BasicParseConverter.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Este archivo define la estructura Range<T>, la cual permite representar rangos
de valores.

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2019 César Andrés Morgan

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
using System.Globalization;

namespace TheXDS.MCART.Types.Converters
{
    /// <inheritdoc />
    /// <summary>
    ///     Clase base para un
    ///     <see cref="T:System.ComponentModel.TypeConverter" /> básico que
    ///     permita transformar un valor de <see cref="T:System.String" /> en
    ///     un <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     Tipo de elemento de salida de este
    ///     <see cref="T:System.ComponentModel.TypeConverter" />.
    /// </typeparam>
    public abstract class BasicParseConverter<T> : TypeConverter
    {
        /// <inheritdoc />
        /// <summary>
        ///   Devuelve si este convertidor puede convertir un objeto del tipo especificado al tipo de este convertidor, mediante el contexto especificado.
        /// </summary>
        /// <param name="context">
        ///   <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> que ofrece un contexto de formato.
        /// </param>
        /// <param name="sourceType">
        ///   Un <see cref="T:System.Type" /> que representa el tipo que desea convertir.
        /// </param>
        /// <returns>
        ///   <see langword="true" /> si este convertidor puede realizar la conversión; en caso contrario, <see langword="false" />.
        /// </returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        /// <inheritdoc />
        /// <summary>
        ///   Devuelve si este convertidor puede convertir el objeto al tipo especificado, con el contexto especificado.
        /// </summary>
        /// <param name="context">
        ///   Interfaz <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> que ofrece un contexto de formato.
        /// </param>
        /// <param name="destinationType">
        ///   <see cref="T:System.Type" /> que representa el tipo al que se quiere convertir.
        /// </param>
        /// <returns>
        ///   <see langword="true" /> si este convertidor puede realizar la conversión; en caso contrario, <see langword="false" />.
        /// </returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(T);
        }

        /// <inheritdoc />
        /// <summary>
        ///   Convierte el objeto determinado al tipo de este convertidor usando el contexto especificado y la información de referencia cultural.
        /// </summary>
        /// <param name="context">
        ///   <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> que ofrece un contexto de formato.
        /// </param>
        /// <param name="culture">
        ///   <see cref="T:System.Globalization.CultureInfo" /> que se va a usar como referencia cultural actual.
        /// </param>
        /// <param name="value">
        ///   <see cref="T:System.Object" /> que se va a convertir.
        /// </param>
        /// <returns>
        ///   Un <see cref="T:System.Object" /> que representa el valor convertido.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        ///   No se puede realizar la conversión.
        /// </exception>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return ConvertFrom(value?.ToString());
        }
        /// <summary>
        ///     Ejecuta la conversión de la cadena al tipo de este <see cref="BasicParseConverter{T}"/>.
        /// </summary>
        /// <param name="value">Cadena a convertir.</param>
        /// <returns>
        ///     Un valor de tipo <typeparamref name="T"/> creado a partir de la cadena especificada.
        /// </returns>
        protected abstract T ConvertFrom(string value);

        /// <inheritdoc />
        /// <summary>
        ///   Convierte el objeto de valor determinado al tipo especificado usando el contexto y la información de referencia cultural especificados.
        /// </summary>
        /// <param name="context">
        ///   <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> que proporciona un contexto de formato.
        /// </param>
        /// <param name="culture">
        ///   Objeto <see cref="T:System.Globalization.CultureInfo" />.
        ///    Si se pasa <see langword="null" />, se supone que se va a usar la referencia cultural actual.
        /// </param>
        /// <param name="value">
        ///   <see cref="T:System.Object" /> que se va a convertir.
        /// </param>
        /// <param name="destinationType">
        ///   El <see cref="T:System.Type" /> para convertir el <paramref name="value" /> parámetro.
        /// </param>
        /// <returns>
        ///   Un <see cref="T:System.Object" /> que representa el valor convertido.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        ///   El parámetro <paramref name="destinationType" /> es <see langword="null" />.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///   No se puede realizar la conversión.
        /// </exception>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is T v) return ConvertTo(v);
            return base.ConvertTo(context,culture,value,destinationType);
        }

        /// <summary>
        ///     Ejecuta la conversión de la cadena al tipo de este <see cref="BasicParseConverter{T}"/>.
        /// </summary>
        /// <param name="value">Cadena a convertir.</param>
        /// <returns>
        ///     La representación como cadena del objeto especificado.
        /// </returns>
        protected virtual string ConvertTo(T value)
        {
            return value.ToString();
        }
    }
}