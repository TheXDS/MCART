//
//  IDialect.cs
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
using System.Collections.Generic;
using System.Data;

namespace MCART.Data.Triton
{
    /// <summary>
    /// Define un conjunto de métodos a implementar por una clase que genere
    /// código de SQL u otros lenguajes para interactuar con un servidor de
    /// bases de datos remoto.
    /// </summary>
    public interface IDialect
    {
        #region Comandos para base de datos
        IDbCommand CreateDB(Type dbModel);
        IDbCommand CreateDB<T_dbModel>();
        IDbCommand DropDB(Type dbModel);
        IDbCommand DropDb<T_dbModel>();
        #endregion

        IDbCommand CreateTable(Type tableModel);
        IDbCommand CreateTable<T_tableModel>();
        IDbCommand AlterTable(Type tableModel);
        IDbCommand AlterTable<T_tableModel>();
        IDbCommand DropTable(Type tableModel);
        IDbCommand DropTable<T_tableModel>();

        IDbCommand Insert<T>(T newRow);
        IDbCommand Update<T>(T rawData, IEnumerable<Filter> filters);
        IDbCommand Delete<T>(IEnumerable<Filter> filters);

        #region Elementos misceláneos
        IDbCommand CreateFunction(string name, SqlDbType returnType, string body);
        IDbCommand DropFunction(string name);
        IDbCommand CreateProcedure(string name, string body);
        IDbCommand DropProcedure(string name);
        IDbCommand CreateView(string name, string body);
        IDbCommand DropView(string name);
        #endregion

        // TODO: Falta todo lo que es Select X(
    }
}
