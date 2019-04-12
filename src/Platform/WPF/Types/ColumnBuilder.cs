/*
ColumnBuilder.cs

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
using System.Collections.Generic;
using System.Linq;

namespace TheXDS.MCART.Types
{
    /// <summary>
    ///     Clase que describe a un selector de datos para generar una
    ///     columna de datos dentro de una tabla.
    /// </summary>
    /// <typeparam name="TObject">
    ///     Tipo de objeto desde el cual se extraerá la información de la
    ///     columna.
    /// </typeparam>
    /// <typeparam name="TItem">
    ///     Tipo de elemento a devolver para esta columna.
    /// </typeparam>
    public class ColumnBuilder<TObject, TItem> : IColumnBuilder<TObject>
    {
        /// <summary>
        ///     Título de la columna.
        /// </summary>
        public string Header { get; }

        /// <summary>
        ///     Contenido de la fila actual a establecer en esta columna.
        /// </summary>
        /// <param name="obj">Objeto del cual extraer el contenido.</param>
        /// <returns>
        ///     El contenido en formato de cadena a colocar dentro de la celda 
        ///     correspondiente a esta columna en la fila actual.
        /// </returns>
        public string Content(TObject obj) => Selector?.Invoke(obj)?.ToString() ?? obj.ToString();

        /// <summary>
        ///     Obtiene un estilo a utilizar sobre la celda en esta columna de la fila actual.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        ///     Un estilo a aplicar a la celda generada, o
        ///     <see langword="null"/> si no es posible obtener un estilo
        ///     válido.
        /// </returns>
        public ICellStyle? Style(object? item) => item is TItem tItem ? CurrentStyle(tItem) : null;

        /// <summary>
        ///     Función de selección de datos.
        /// </summary>
        public Func<TObject, TItem> Selector { get; }

        /// <summary>
        ///     Obtiene una colección con los estilos registrados para esta columna.
        /// </summary>
        public IList<CellStyle<TItem>> Styles { get; } = new List<CellStyle<TItem>>();

        /// <summary>
        ///     Obtiene un estilo a utilizar para el elemento actual.
        /// </summary>
        /// <param name="currentItem">
        ///     Valor contra el cual evaluar los estilos.
        /// </param>
        /// <returns>
        ///     Un estilo que pueda ser aplicado al valor, o
        ///     <see langword="null"/> si ningún estilo puede ser aplicado.
        /// </returns>
        public CellStyle<TItem> CurrentStyle(TItem currentItem) => Styles.FirstOrDefault(p => p.StyleApplies?.Invoke(currentItem) ?? true);

        /// <summary>
        ///     Inicializa una nueva instancia de la clase
        ///     <see cref="ColumnBuilder{TObject, TItem}"/>.
        /// </summary>
        /// <param name="header">
        ///     Título de la columna.
        /// </param>
        /// <param name="selector">
        ///     Función de selección de datos.
        /// </param>
        public ColumnBuilder(string header, Func<TObject, TItem> selector)
        {
            Header = header;
            Selector = selector;
        }
    }
}