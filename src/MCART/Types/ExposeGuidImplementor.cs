/*
ExposeGuidImplementor.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

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

#nullable enable

using System;
using System.Runtime.InteropServices;
using TheXDS.MCART.Exceptions;
using TheXDS.MCART.Types.Base;

namespace TheXDS.MCART.Types
{
    /// <summary>
    ///     Implementa directamente <see cref="IExposeGuid"/>.
    /// </summary>
    public class ExposeGuidImplementor : IExposeGuid
    {
        private readonly Type _t;

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ExposeGuidImplementor"/>.
        /// </summary>
        /// <param name="o">
        ///     Objeto del cual exponer el Guid; generalmente 
        ///     <see langword="this"/>.
        /// </param>
        public ExposeGuidImplementor(object o)
        {
            _t = o as Type ?? o?.GetType() ?? throw new ArgumentNullException(nameof(o));
        }

        /// <summary>
        ///     Obtiene el <see cref="Guid"/> asociado a este objeto.
        /// </summary>
        public virtual Guid Guid
        {
            get
            {
                var g = _t.GetAttr<GuidAttribute>() ?? throw new IncompleteTypeException(Resources.InternalStrings.ErrorDeclMustHaveGuidAttr(_t), _t);
                return new Guid(g.Value);
            }
        }
    }
}