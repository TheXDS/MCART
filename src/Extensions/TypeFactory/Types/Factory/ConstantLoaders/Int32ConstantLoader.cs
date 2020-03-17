/*
Int32ConstantLoader.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2020 César Andrés Morgan

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

using System.Reflection.Emit;
using static System.Reflection.Emit.OpCodes;

namespace TheXDS.MCART.Types.Extensions
{
    /// <summary>
    /// Carga un valor constante <see cref="int"/> en la secuencia de
    /// instrucciones MSIL.
    /// </summary>
    public class Int32ConstantLoader : ConstantLoader<int>
    {
        /// <summary>
        /// Carga un valor constante <see cref="int"/> en la secuencia de
        /// instrucciones MSIL.
        /// </summary>
        /// <param name="il">Generador de IL a utilizar.</param>
        /// <param name="value">
        /// Valor constante a cargar en la secuencia de instrucciones.
        /// </param>
        public override void Emit(ILGenerator il, int value)
        {
            switch (value)
            {
                case -1:
                    il.Emit(Ldc_I4_M1);
                    break;
                case 0:
                    il.Emit(Ldc_I4_0);
                    break;
                case 1:
                    il.Emit(Ldc_I4_1);
                    break;
                case 2:
                    il.Emit(Ldc_I4_2);
                    break;
                case 3:
                    il.Emit(Ldc_I4_3);
                    break;
                case 4:
                    il.Emit(Ldc_I4_4);
                    break;
                case 5:
                    il.Emit(Ldc_I4_5);
                    break;
                case 6:
                    il.Emit(Ldc_I4_6);
                    break;
                case 7:
                    il.Emit(Ldc_I4_7);
                    break;
                case 8:
                    il.Emit(Ldc_I4_8);
                    break;
                default:
                    il.Emit(Ldc_I4, value);
                    break;
            }
        }
    }
}