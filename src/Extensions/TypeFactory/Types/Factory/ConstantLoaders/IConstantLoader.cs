/*
IConstantLoader.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2021 César Andrés Morgan

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

namespace TheXDS.MCART.Types.Factory.ConstantLoaders;
using System;
using System.Reflection.Emit;

/// <summary>
/// Define una serie de miembros a implementar por un tipo que permita
/// cargar valores constantes en una secuencia de instrucciones MSIL.
/// </summary>
public interface IConstantLoader
{
    /// <summary>
    /// Carga un valor constante en la secuencia de instrucciones MSIL.
    /// </summary>
    /// <param name="il">Generador de IL a utilizar.</param>
    /// <param name="value">
    /// Valor constante a cargar en la secuencia de instrucciones.
    /// </param>
    void Emit(ILGenerator il, object? value);

    /// <summary>
    /// Obtiene el tipo de constante que este 
    /// <see cref="IConstantLoader"/> es capaz de cargar en la
    /// secuencia de instrucciones MSIL.
    /// </summary>
    Type ConstantType { get; }
}
