//
//  BarStepper.cs
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

using TheXDS.MCART.Types.TaskReporter;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using St = TheXDS.MCART.Resources.Strings;

namespace TheXDS.MCART.Controls
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
    public class BarStepper : TaskReporterControl
    {
        /// <summary>
        /// Identifica la propiedad de dependencia 
        /// <see cref="StatusForReady"/>.
        /// </summary>
        public static DependencyProperty StatusForReadyProperty = DependencyProperty.Register(
                nameof(StatusForReady), typeof(Visibility), typeof(BarStepper),
                new PropertyMetadata(Visibility.Hidden), (a) => (Visibility)a != Visibility.Visible);
        /// <summary>
        /// Identifica a la propiedad de dependencia <see cref="ButtonBrush"/>.
        /// </summary>
        public static DependencyProperty ButtonBrushProperty = DependencyProperty.Register(
                nameof(ButtonBrush), typeof(Brush), typeof(BarStepper),
                new PropertyMetadata(SystemColors.ControlBrush));
        /// <summary>
        /// Obtiene o establece El <see cref="Brush"/> a utilizar para dibujar 
        /// el botón de cancelar de este control.
        /// </summary>
        public Brush ButtonBrush
        {
            get => (Brush)GetValue(ButtonBrushProperty);
            set => SetValue(ButtonBrushProperty, value);
        }
        /// <summary>
        /// Determina si al finalizar una tarea, los controles se ocultarán o 
        /// si serán colapsados.
        /// </summary>
        /// <returns>
        /// El valor de <seealso cref="System.Windows.Visibility"/> aplicado a
        /// los controles al finalizar una tarea.
        /// </returns>
        public Visibility StatusForReady
        {
            get => (Visibility)GetValue(StatusForReadyProperty);
            set => SetValue(StatusForReadyProperty, value);
        }


        ProgressBar PB = new ProgressBar { Margin = new Thickness(8), Width = 100 };
        Button Bt = new Button();
        TextBlock LB = new TextBlock { VerticalAlignment=VerticalAlignment.Center };
        /// <summary>
        /// Modifica el control para mostrar el progreso de una tarea.
        /// </summary>
        /// <param name="stoppable">
        /// Indica si la tarea puede ser detenida.
        /// </param>
        protected override void OnBegin(bool stoppable)
        {
            PB.Visibility = Visibility.Visible;
            Bt.Content = $"  {St.Cncl}  ";
            Bt.IsEnabled = stoppable;
            Bt.Visibility = stoppable ? Visibility.Visible : Visibility.Collapsed;
            PB.Value = PB.Minimum;
            PB.IsIndeterminate = false;
        }
        /// <summary>
        /// Modifica el control para mostrar el progreso actual.
        /// </summary>
        /// <param name="e">Progreso actual de la tarea.</param>
        protected override void OnBusy(ProgressEventArgs e)
        {
            LB.Text = e.HelpText;
            if (e.Progress == null || !((double)e.Progress).IsBetween(PB.Minimum, PB.Maximum))
                PB.IsIndeterminate = true;
            else
            {
                PB.IsIndeterminate = false;
                PB.Value = (double)e.Progress;
            }
        }
        /// <summary>
        /// Modifica el estado del control, cuando no se desea mostrar el
        /// progreso de una tarea.
        /// </summary>
        /// <param name="msg">Mensaje del control.</param>
        protected override void OnReady(string msg = null)
        {
            PB.SetBinding(VisibilityProperty, new Binding(nameof(StatusForReady)) { Source = this });
            Bt.SetBinding(VisibilityProperty, new Binding(nameof(StatusForReady)) { Source = this });
            LB.Text = msg;
            PB.IsIndeterminate = false;
            PB.Value = 0;
        }
        /// <summary>
        /// Inicializa una nueva instancia de la clase 
        /// <see cref="BarStepper"/>.
        /// </summary>
        public BarStepper()
        {
            MinWidth = 124;
            MinHeight = 20;
            MaxHeight = 32;
            DockPanel a = new DockPanel();
            LB.SetBinding(TextBlock.ForegroundProperty, new Binding(nameof(Foreground)) { Source = this });
            Bt.SetBinding(ForegroundProperty, new Binding(nameof(Foreground)) { Source = this });
            Bt.SetBinding(BackgroundProperty, new Binding(nameof(ButtonBrushProperty)) { Source = this });
            a.Children.Add(PB);
            a.Children.Add(Bt);
            a.Children.Add(LB);
            Content = a;
            Bt.Click += (sender, e) => RaiseCancelPending();
            OnReady(St.Rdy);
        }
    }
}