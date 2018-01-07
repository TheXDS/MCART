//
//  Delegates.cs
//
//  This file is part of Morgan's CLR Advanced Runtime (MCART)
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
//
//  Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace TheXDS.MCART.Types.TaskReporter
{
    /// <summary>
    /// Define un delegado que ejecutará una acción cíclica con la estructura
    /// de un ciclo <c>for</c>.
    /// </summary>
    /// <param name="counter">Contador de iteración actual.</param>
    /// <param name="tskReporter">
    /// Objeto <see cref="TaskReporter"/> por medio del cual una tarea podrá
    /// reportar su progreso.
    /// </param>
    public delegate void ForAction(int counter, ITaskReporter tskReporter);
    /// <summary>
    /// Define un delegado que ejecutará una acción cíclica con la estructura
    /// de un ciclo <c>foreach</c>.
    /// </summary>
    /// <param name="item">Objeto de iteración actual.</param>
    /// <param name="tskReporter">
    /// Objeto <see cref="TaskReporter"/> por medio del cual una tarea podrá
    /// reportar su progreso.
    /// </param>
    public delegate void ForEachAction<T>(T item, ITaskReporter tskReporter);
}