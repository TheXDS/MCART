//
//  ClassAttributes.cs
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

namespace MCART.Data.Triton
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class DatabaseAttribute : Attribute
    {
        public readonly Dialect Dialect;
        public DatabaseAttribute(Dialect dialect = Dialect.SQL97)
        {
            if (!typeof(Dialect).IsEnumDefined(dialect))
                throw new ArgumentOutOfRangeException(nameof(dialect));

            Dialect = dialect;
        }
    }
}