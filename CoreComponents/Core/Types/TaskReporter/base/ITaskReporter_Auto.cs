//
//  ITaskReporter_Auto.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2018 César Andrés Morgan
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
using System.Threading.Tasks;

namespace MCART.Types.TaskReporter
{
    /// <summary>
    /// Define una serie de métodos a implementar por una clase que permita a
    /// una tarea reportar el progreso de una operación, generalmente cíclica.
    /// </summary>
    public partial interface ITaskReporter
    {
        /// <summary>
        /// Controla automáticamente una tarea en una estructura similar a un
        /// ciclo <c>For</c> de Visual Basic.
        /// </summary>
        /// <param name="cEnd">Valor final del contador.</param>
        /// <param name="forAct">Acción a ejecutar.</param>
        /// <param name="message">Mensaje genérico de estado.</param>
        /// <param name="nonStop">
        /// Si se establece en <c>true</c>, la tarea no podrá ser detenida.
        /// </param>
        /// <param name="onCancel">
        /// Acción en caso de que la tarea se cancele.
        /// </param>
        /// <param name="onError">
        /// Acción en caso de error.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/>que puede ser esperado por la palabra clave
        /// <c>await</c>.
        /// </returns>
        Task For(int cEnd, ForAction forAct, string message = null, bool nonStop = false, Action onCancel = null, Action onError = null);
        /// <summary>
        /// Controla automáticamente una tarea en una estructura similar a un
        /// ciclo <c>For</c> de Visual Basic.
        /// </summary>
        /// <param name="cStart">Valor inicial del contador.</param>
        /// <param name="cEnd">Valor final del contador.</param>
        /// <param name="forAct">Acción a ejecutar.</param>
        /// <param name="message">Mensaje genérico de estado.</param>
        /// <param name="nonStop">
        /// Si se establece en <c>true</c>, la tarea no podrá ser detenida.
        /// </param>
        /// <param name="onCancel">
        /// Acción en caso de que la tarea se cancele.
        /// </param>
        /// <param name="onError">
        /// Acción en caso de error.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/>que puede ser esperado por la palabra clave
        /// <c>await</c>.
        /// </returns>
        Task For(int cStart, int cEnd, ForAction forAct, string message = null, bool nonStop = false, Action onCancel = null, Action onError = null);
        /// <summary>
        /// Controla automáticamente una tarea en una estructura similar a un
        /// ciclo <c>For</c> de Visual Basic.
        /// </summary>
        /// <param name="cStart">Valor inicial del contador.</param>
        /// <param name="cEnd">Valor final del contador.</param>
        /// <param name="cStep">Incremento por iteración del contador.</param>
        /// <param name="forAct">Acción a ejecutar.</param>
        /// <param name="message">Mensaje genérico de estado.</param>
        /// <param name="nonStop">
        /// Si se establece en <c>true</c>, la tarea no podrá ser detenida.
        /// </param>
        /// <param name="onCancel">
        /// Acción en caso de que la tarea se cancele.
        /// </param>
        /// <param name="onError">
        /// Acción en caso de error.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/>que puede ser esperado por la palabra clave
        /// <c>await</c>.
        /// </returns>
        Task For(int cStart, int cEnd, int cStep, ForAction forAct, string message = null, bool nonStop = false, Action onCancel = null, Action onError = null);
        /// <summary>
        /// Controla automáticamente una tarea en una estructura similar a un
        /// ciclo <c>foreach</c>.
        /// </summary>
        /// <typeparam name="T">Tipo de elementos a iterar.</typeparam>
        /// <param name="coll">Colección a iterar.</param>
        /// <param name="FEachAct">
        /// Acción a ejecutar sobre cada uno de los elementos.
        /// </param>
        /// <param name="message">Mensaje genérico de estado.</param>
        /// <param name="nonStop">
        /// Si se establece en <c>true</c>, la tarea no podrá ser detenida.
        /// </param>
        /// <param name="onCancel">
        /// Acción en caso de que la tarea se cancele.
        /// </param>
        /// <param name="onError">
        /// Acción en caso de error.
        /// </param>
        /// <returns>
        /// Un <see cref="Task"/>que puede ser esperado por la palabra clave
        /// <c>await</c>.
        /// </returns>
        Task ForEach<T>(IEnumerable<T> coll, ForEachAction<T> FEachAct, string message = null, bool nonStop = false, Action onCancel = null, Action onError = null);
    }
}
