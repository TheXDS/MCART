//
//  BarStepper.cs
//
//  This file is part of MCART
//
//  Author:
//       César Andrés Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Andrés Morgan
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

using MCART.Types.TaskReporter;
using System.Windows.Forms;
using St = MCART.Resources.Strings;

namespace MCART.Controls
{
    /// <summary>
    /// Este control permite reportar visualmente el progreso de una tarea en 
    /// una barra de estado; opcionalmente, mostrando además un botón para 
    /// cancelar dicha tarea.
    /// </summary>
    /// <remarks>
    /// Este control es compatible con la interfaz <see cref="ITaskReporter"/>.
    /// </remarks>
    /// <example>
    /// Vea la página de ayuda de <see cref="ITaskReporter"/> para ver ejemplos
    /// de cómo utilizar este control.
    /// </example>
    public partial class BarStepper : TaskReporterControl
    {
        /// <summary>
        /// Modifica el control para mostrar el progreso de una tarea.
        /// </summary>
        /// <param name="stoppable">
        /// Indica si la tarea puede ser detenida.
        /// </param>
        protected override void OnBegin(bool stoppable)
        {
            pbrProgres.Visible = true;
            btnCancel.Text = St.Cncl;
            btnCancel.Enabled = stoppable;
            btnCancel.Visible = stoppable;
            pbrProgres.Value = pbrProgres.Minimum;
            pbrProgres.Style= ProgressBarStyle.Continuous;
        }
        /// <summary>
        /// Modifica el control para mostrar el progreso actual.
        /// </summary>
        /// <param name="e">Progreso actual de la tarea.</param>
        protected override void OnBusy(ProgressEventArgs e)
        {
            lblStatus.Text = e.HelpText;
            if (e.Progress == null || !((float)e.Progress).IsBetween(0.0f, 1.0f))
                pbrProgres.Style = ProgressBarStyle.Marquee;
            else
            {
                pbrProgres.Style = ProgressBarStyle.Continuous;
                pbrProgres.Value = (int)(e.Progress * pbrProgres.Maximum);
            }
        }
        /// <summary>
        /// Modifica el estado del control, cuando no se desea mostrar el
        /// progreso de una tarea.
        /// </summary>
        /// <param name="msg">Mensaje del control.</param>
        protected override void OnReady(string msg = null)
        {
            pbrProgres.Visible = false;
            btnCancel.Visible = false;
            lblStatus.Text = msg;
            pbrProgres.Style=ProgressBarStyle.Continuous;
            pbrProgres.Value = 0;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="BarStepper"/>.
        /// </summary>
        public BarStepper()
        {
            InitializeComponent();
        }
    }
}
