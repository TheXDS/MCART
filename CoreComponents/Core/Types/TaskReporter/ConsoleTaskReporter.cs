//
//  ConsoleTaskReporter.cs
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
using St = MCART.Resources.Strings;

namespace MCART.Types.TaskReporter
{
    /// <summary>
    /// Implementación de <see cref="TaskReporter"/> que muestra el progreso de
    /// una tarea en la consola del sistema.
    /// </summary>
    public class ConsoleTaskReporter : TaskReporter
    {
        short sze = 20;
        bool ap;
        Point? p;
        void DoBegin()
        {
            SetOnDuty(true);
            Console.CursorVisible = false;
            if (Verbose) Console.Write(St.CtrlCCancel);
            if (p is null)
            {
                ap = true;
                p = new Point(Console.CursorLeft, Console.CursorTop);
            }
            else ap = false;
            for (short j = 1; j <= sze; j++) Console.Write("░");
        }
        void DoEnd(string msg = null, ConsoleColor c = ConsoleColor.Gray)
        {
            SetOnDuty(false);
            if (ap) p = default(Point);
            Console.WriteLine();
            if (Verbose && !(msg is null))
            {
                Console.ForegroundColor = c;
                Console.WriteLine(msg);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        void DoRprt(string msg, float? pgr)
        {
            if (p.HasValue) Console.SetCursorPosition((int)p?.X, (int)p?.Y);
            if (!(pgr is null))
            {
                int cnt = (int)(sze * 3 * pgr);
                short j = 0;
                while (j < sze | cnt > 0)
                {
                    if (cnt > 2) Console.Write('█');
                    else if (cnt > 1) Console.Write('▓');
                    else Console.Write('▒');
                    cnt -= 3;
                    j++;
                }
                for (; j < sze; j++) Console.Write('░');
            }
            else for (short j = 0; j < sze; j++) Console.Write('-');
            Console.Write(msg);
            while (Console.CursorLeft < Console.BufferWidth) Console.Write(' ');

        }
        /// <summary>
        /// Obtiene o establece el tamaño a utilizar para la barra de progreso.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Se produce si se intenta establecer un tamaño mayor al ancho del
        /// búffer de la consola/terminal.
        /// </exception>
        public short ProgressSize
        {
            get { return sze; }
            set
            {
                if (value.IsBetween((short)1, (short)Console.BufferWidth)) sze = value;
                else throw new ArgumentOutOfRangeException(nameof(value));
            }
        }
        /// <summary>
        /// Obtiene o establece la ubicación de la barra de progreso en la
        /// ventana de la consola/terminal.
        /// </summary>
        public Point? PBarLocation
        {
            get { return p; }
            set
            {
                if (!(bool)value?.FitsInBox(1, 1, Console.BufferWidth, Console.BufferHeight))
                    throw new ArgumentOutOfRangeException(nameof(value));
                if (OnDuty) throw new InvalidOperationException();
                p = value.Value;
            }
        }
        /// <summary>
        /// Obtiene o establece un valor que indica si se mostrará información
        /// detallada sobre la tarea al mostrar el progreso.
        /// </summary>
        public bool Verbose;
        /// <summary>
        /// Indica que una tarea se ha iniciado.
        /// </summary>
        public override void Begin()
        {
            if (OnDuty) throw new InvalidOperationException();
            DoBegin();
        }
        /// <summary>
        /// Indica que una tarea que no se puede detener ha iniciado.
        /// </summary>
        public override void BeginNonStop()
        {
            if (OnDuty) throw new InvalidOperationException();
            DoBegin();
        }
        /// <summary>
        /// Marca el final de una tarea.
        /// </summary>
        public override void End() { DoEnd(); }
        /// <summary>
        /// Indica que la tarea finalizó con un error.
        /// </summary>
        /// <param name="ex">
        /// <see cref="Exception"/> que causó la finalización de esta tarea.
        /// </param>
        public override void EndWithError(Exception ex = null)
        {
            if (ex is null) ex = new Exception();
            DoEnd(ex.StackTrace + "\n" + ex.Message, ConsoleColor.DarkRed);
        }
        /// <summary>
        /// Reporta el estado de la tarea actual.
        /// </summary>
        /// <param name="e">Información adicional del evento.</param>
        public override void Report(ProgressEventArgs e)
        {
            DoRprt(e.HelpText, e.Progress);
        }
        /// <summary>
        /// Reporta el estado de la tarea actual.
        /// </summary>
        /// <param name="helpText">Texto de ayuda sobre la tarea.</param>
        public override void Report(string helpText)
        {
            DoRprt(helpText, null);
        }
        /// <summary>
        /// Reporta el estado de la tarea actual.
        /// </summary>
        /// <param name="progress">
        /// <see cref="Nullable{T}"/> que representa el progreso actual de la
        /// tarea.
        /// </param>
        /// <param name="helpText">Texto de ayuda sobre la tarea.</param>
        public override void Report(float? progress = default(float?), string helpText = null)
        {
            DoRprt(helpText, progress);
        }
        /// <summary>
        /// Indica que la tarea actual ha sido detenida antes de finalizar.
        /// </summary>
        /// <param name="e">
        /// <see cref="ProgressEventArgs"/> con información del progreso de la
        /// tarea al momento de la detención.
        /// </param>
        public override void Stop(ProgressEventArgs e)
        {
            DoEnd(e?.HelpText ?? St.UsrCncl);
        }
        /// <summary>
        /// Indica que la tarea actual ha sido detenida antes de finalizar.
        /// </summary>
        /// <param name="progress">
        /// <see cref="Nullable{T}"/> que representa el progreso actual de la
        /// tarea.
        /// </param>
        /// <param name="helpText">Texto de ayuda sobre la tarea.</param>
        public override void Stop(float? progress = default(float?), string helpText = null)
        {
            DoEnd(helpText ?? St.UsrCncl);
        }
        /// <summary>
        /// Indica que la tarea actual ha sido detenida antes de finalizar.
        /// </summary>
        /// <param name="helpText">Texto de ayuda sobre la tarea.</param>
        public override void Stop(string helpText)
        {
            DoEnd(helpText);
        }
    }
}