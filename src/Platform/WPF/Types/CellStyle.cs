﻿/*
CellStyle.cs

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
using System.Windows;
using System.Windows.Media;

namespace TheXDS.MCART.Types
{

    /// <summary>
    ///     Estilo de elementos de WPF orientado a objetos
    ///     <see cref="System.Windows.Documents.TableCell"/>.
    /// </summary>
    public class CellStyle<T> : WpfStyle, ICellStyle
    {
        private Brush? _oddBackground;

        /// <summary>
        ///     <see cref="Brush"/> de fondo a aplicar al elemento para elementos impares.
        /// </summary>
        public Brush? OddBackground
        {
            get => _oddBackground ?? Background;
            set => _oddBackground = value;
        }

        /// <summary>
        ///     Ancho de la celda.
        /// </summary>
        public GridLength? Width { get; set; }

        /// <summary>
        ///     Alineación de texto a utilizar para colocar la información
        ///     de la celda.
        /// </summary>
        public TextAlignment Alignment { get; set; }

        /// <summary>
        ///     Obtiene o establece la función condicional para aplicar este
        ///     estilo.
        /// </summary>
        public Func<T, bool>? StyleApplies { get; set; } = _ => true;
    }
}