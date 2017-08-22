//
//  MySQLDialect.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2017 César Andrés Morgan
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCART.PluginSupport;

namespace MCART.Data.Triton
{
    /// <summary>
    /// Enumera todos los motores de base de datos con los que cuenta MySQL.
    /// </summary>
    enum Engine : byte
    {
        InnoDB,
        MyISAM,
        ndbcluster,
        MEMORY,
        EXAMPLE,
        FEDERATED,
        ARCHIVE,
        CSV,
        BLACKHOLE,
        infinidb,
        IBMDB2I,
        Brighthouse,
        KFDB,
        ScaleDB,
        TokuDB,
        XtraDB,
        Spider,
        Mroonga,
        MRG_MyISAM,
        Aria,
        PBXT
    }




    class MySQLDialect : Plugin, IDialect
    {
        

        IDbCommand IDialect.AlterTable(Type tableModel)
        {
            throw new NotImplementedException();
        }

        IDbCommand IDialect.AlterTable<T_tableModel>()
        {
            throw new NotImplementedException();
        }

        IDbCommand IDialect.CreateDB(Type dbModel)
        {
            throw new NotImplementedException();
        }

        IDbCommand IDialect.CreateDB<T_dbModel>()
        {
            throw new NotImplementedException();
        }

        IDbCommand IDialect.CreateFunction(string name, SqlDbType returnType, string body)
        {
            throw new NotImplementedException();
        }

        IDbCommand IDialect.CreateProcedure(string name, string body)
        {
            throw new NotImplementedException();
        }

        IDbCommand IDialect.CreateTable(Type tableModel)
        {
            throw new NotImplementedException();
        }

        IDbCommand IDialect.CreateTable<T_tableModel>()
        {
            throw new NotImplementedException();
        }

        IDbCommand IDialect.CreateView(string name, string body)
        {
            throw new NotImplementedException();
        }

        IDbCommand IDialect.Delete<T>(IEnumerable<Filter> filters)
        {
            throw new NotImplementedException();
        }

        IDbCommand IDialect.DropDB(Type dbModel)
        {
            throw new NotImplementedException();
        }

        IDbCommand IDialect.DropDb<T_dbModel>()
        {
            throw new NotImplementedException();
        }

        IDbCommand IDialect.DropFunction(string name)
        {
            throw new NotImplementedException();
        }

        IDbCommand IDialect.DropProcedure(string name)
        {
            throw new NotImplementedException();
        }

        IDbCommand IDialect.DropTable(Type tableModel)
        {
            throw new NotImplementedException();
        }

        IDbCommand IDialect.DropTable<T_tableModel>()
        {
            throw new NotImplementedException();
        }

        IDbCommand IDialect.DropView(string name)
        {
            throw new NotImplementedException();
        }

        IDbCommand IDialect.Insert<T>(T newRow)
        {
            throw new NotImplementedException();
        }

        IDbCommand IDialect.Update<T>(T rawData, IEnumerable<Filter> filters)
        {
            throw new NotImplementedException();
        }
    }
}
