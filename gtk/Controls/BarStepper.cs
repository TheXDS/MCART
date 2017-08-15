//
//  BarStepper.cs
//
//  This file is part of MCART
//
//  Author:
//       César Morgan <xds_xps_ivx@hotmail.com>
//
//  Copyright (c) 2011 - 2017 César Morgan
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
using Gtk;
using St = MCART.Resources.Strings;

namespace MCART.Controls
{
	/// <summary>
	/// Barra de estado que muestra el progreso de una tarea por medio de la
	/// interfaz <see cref="ITaskReporter"/>
	/// </summary>
	[System.ComponentModel.ToolboxItem(true)]
	public class BarStepper : TaskReporterControl
	{
		readonly ProgressBar pbrProgress = new ProgressBar();
		readonly Button btnCancel = new Button(Stock.Cancel);
		readonly Label lblStatus = new Label(St.Rdy);
		/// <summary>
		/// Inicializa una nueva instancia de la clase 
		/// <see cref="BarStepper"/>.
		/// </summary>
		public BarStepper()
		{
			pbrProgress.Visible = false;
            btnCancel.Visible = false;

            HBox root = new HBox
            {
                pbrProgress,
                btnCancel,
                lblStatus
            };
            Child = root;
		}
	}
}