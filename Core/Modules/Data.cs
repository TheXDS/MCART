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

namespace MCART
{
    /// <summary>
    /// Funciones especiales para bases de datos.
    /// </summary>
    public static partial class Data
    {
        public SqlDbType AsSqlType(Type t)
        {
            if (t is )
        }
        public static SqlParameter[] ToParams<T>(T data)
        {
            List<SqlParameter> outp = new List<SqlParameter>();
            foreach (FieldInfo j in typeof(T).GetFields())
                outp.Add(new SqlParameter(j.Name, SqlDbType.Binary));

            return outp.ToArray();
        }
        public static SqlCommand InsertCommand<T>(T data)
        {

            return new SqlCommand(
            "Insert Into"
            );
        }
    }
}
