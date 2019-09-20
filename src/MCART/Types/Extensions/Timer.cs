/*
Timer.cs

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

using System;
using System.Timers;

namespace TheXDS.MCART.Types.Extensions
{
    /// <inheritdoc />
    /// <summary>
    /// Extensión de la clase <see cref="Timer" />. provee de toda
    /// la funcionalidad previamente disponible, e incluye algunas extensiones
    /// útiles.
    /// </summary>
    public class Timer : System.Timers.Timer
    {
        private void Tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (AutoReset) StartTime = DateTime.Now;
        }
        /// <summary>
        /// Indica el momento de inicio de este <see cref="Timer"/>.
        /// </summary>
        public DateTime? StartTime { get; private set; }

        /// <summary>
        /// Indica la cantidad de tiempo disponible antes de cumplir con el 
        /// intervalo establecido en 
        /// <see cref="System.Timers.Timer.Interval"/>.
        /// </summary>
        public TimeSpan? TimeLeft
        {
            get
            {
                if (!(StartTime is null)) return TimeSpan.FromMilliseconds(Interval) - (DateTime.Now - StartTime);
                return null;
            }
        }
        /// <summary>
        /// Obtiene o establece un valor que indica si este <see cref="Timer"/>
        /// debe generar el evento <see cref="System.Timers.Timer.Elapsed"/>.
        /// </summary>
        public new bool Enabled
        {
            get=> base.Enabled;
            set
            {
                StartTime = value ? (DateTime?)DateTime.Now : null;
                base.Enabled = value;
            }
        }
        /// <summary>
        /// Empieza a generar el evento 
        /// <see cref="System.Timers.Timer.Elapsed"/> al establecer 
        /// <see cref="Enabled"/> en <see langword="true"/>.
        /// </summary>
        public new void Start()
        {
            StartTime = DateTime.Now;
            base.Start();
        }
        /// <summary>
        /// Deja de generar el evento <see cref="System.Timers.Timer.Elapsed"/>
        /// al establecer <see cref="Enabled"/> en <see langword="false"/>.
        /// </summary>
        public new void Stop() { StartTime = null; base.Stop(); }
        /// <summary>
        /// Reinicia este <see cref="Timer"/>.
        /// </summary>
        public void Reset() { Stop(); Start(); }
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Extensions.Timer" />.
        /// </summary>
        public Timer() { Elapsed += Tmr_Elapsed; }
        /// <inheritdoc />
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Extensions.Timer" /> y 
        /// establece la propiedad <see cref="P:System.Timers.Timer.Interval" />
        /// en el número de milisegundos especificado.
        /// </summary>
        /// <param name="interval">
        /// Tiempo, en milisegundos, entre eventos. Este valor debe ser mayor
        /// que cero y menor que <see cref="F:System.Int32.MaxValue" />.
        /// </param>
        public Timer(double interval) : base(interval) { }
    }
}