//
//  Data.cs
//
//  This file is part of MCART
//
//  Author:
//       César Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Morgan
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
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace MCART.Data
{
    /// <summary>
    /// Funciones especiales para bases de datos.
    /// </summary>
    public static partial class Data
    {
        public static SqlDbType AsSqlType(this Type t)
        {
            if (t == typeof(int)) return SqlDbType.Int;
            if (t == typeof(string)) return SqlDbType.VarChar;
            if (t == typeof(bool)) return SqlDbType.Bit;
            if (t == typeof(float)) return SqlDbType.Real;
            if (t == typeof(double)) return SqlDbType.Float;
            if (t == typeof(Int64)) return SqlDbType.BigInt;
            if (t == typeof(DateTime)) return SqlDbType.DateTime;
            if (t == typeof(Decimal)) return SqlDbType.Decimal;

            // TODO: Incorporar más tipos...

            throw new Exceptions.InvalidTypeException(t);
        }
        /// <summary>
        /// Crea un arreglo de <see cref="SqlParameter"/> a partir de los campos
        /// públicos disponibles en la instancia provista de tipo 
        /// <typeparamref name="T"/>. 
        /// </summary>
        /// <returns>The parameters.</returns>
        /// <param name="data">Data.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static IEnumerable<SqlParameter> ToParams<T>(T data)
        {
            foreach (FieldInfo j in typeof(T).GetFields())
                yield return (new SqlParameter(j.Name, j.FieldType.AsSqlType()) { Value = j.GetValue(data) });
        }
        /// <summary>
        /// Genera un comando de inserción para la tabla especificada.
        /// </summary>
        /// <returns>
        /// Un <see cref="SqlCommand"/> compuesto de una transacción de 
        /// inserción de los datos provistos en la tabla especificada.
        /// </returns>
        /// <param name="table">Tabla a la cual insertar.</param>
        /// <param name="data">Datos a insertar.</param>
        /// <typeparam name="T">Tipo de los datos a ser insertados.</typeparam>
        public static SqlCommand InsertCommand<T>(string table, T data)
        {
            // TODO: verificar la validez del parámetro table.
            if (table.IsEmpty()) throw new ArgumentNullException(nameof(table));

            StringBuilder fields = new StringBuilder();
            StringBuilder values = new StringBuilder();
            SqlCommand cmd = new SqlCommand(
                $"Insert Into {table} ({fields.ToString()}) values ({values.ToString()})"
            );
            cmd.Parameters.AddRange(ToParams(data).ToArray());
            return cmd;
        }
    }
}
