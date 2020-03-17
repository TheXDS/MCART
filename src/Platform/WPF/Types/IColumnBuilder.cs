/*
IColumnBuilder.cs

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

namespace TheXDS.MCART.Types
{
    /// <summary>
    /// Clase que describe a un selector de datos para generar una
    /// columna de datos dentro de una tabla.
    /// </summary>
    /// <typeparam name="TObject">
    /// Tipo de objeto desde el cual se extraerá la información de la
    /// columna.
    /// </typeparam>
    public interface IColumnBuilder<TObject>
    {
        /// <summary>
        /// Título de la columna.
        /// </summary>
        string Header { get; }

        /// <summary>
        /// Contenido de la fila actual a establecer en esta columna.
        /// </summary>
        /// <param name="obj">Objeto del cual extraer el contenido.</param>
        /// <returns>
        /// El contenido en formato de cadena a colocar dentro de la celda 
        /// correspondiente a esta columna en la fila actual.
        /// </returns>
        string Content(TObject obj);

        /// <summary>
        /// Obtiene un estilo a utilizar sobre la celda en esta columna de la fila actual.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// Un estilo a aplicar a la celda generada, o
        /// <see langword="null"/> si no es posible obtener un estilo
        /// válido.
        /// </returns>
        ICellStyle? Style(object? item);
    }
}