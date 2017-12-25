/*
Examples.vb

This file is part of MCART

Author:
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2017 César Andrés Morgan

MCART Is free software: you can redistribute it And/Or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, Or
(at your option) any later version.

MCART Is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If Not, see <http:'www.gnu.org/licenses/>.

======================== N O T A   I M P O R T A N T E ========================

Este módulo contiene ejemplos de código para MCART, y no debe ser compilado ni
incluido en ninguna distribución de MCART. Su propósito es el de brindar un
espacio que permita la creación de documentación para compilar, por lo que el
código contenido aquí no está pensado para su distribución o uso en un entorno
de producción, sino más bien como una referencia a partir de la cual el
el desarrollador podrá aprender a utilizar la librería y crear su propio
código.
 */
public class MyPlugin
{
    #region Example1
    /// <summary>
    /// Constructor del Plugin.
    /// </summary>
    public MyPlugin()
    {
        // Crear la interacción...
        var Interac1 = new InteractionItem(Interaccion1, "Interacción 1", "Muestra un mensaje en la salida de la depuración.");

        // Agregar la interacción al menú de este plugin...
        MyMenu.Add(Interac1);


        // Alternativamente, pueden establecerse atributos a la acción de
        // interacción, lo que resulta en un mayor orden del código.
        MyMenu.Add(new InteractionItem(Interaccion2));


        // También es posible utilizar delegados o lambdas con firma compatible
        // con System.EventHandler
        MyMenu.Add(
            new InteractionItem((sender, e) =>
            {
                System.Diagnostics.Debug.Print("Interacción 3 ejecutada.");
            },
            "Interacción 3", "Muestra otro mensaje en la salida de la depuración."));
    }

    /// <summary>
    /// Lógica del primer elemento de interacción del plugin.
    /// </summary>
    /// <param name="sender">Objeto que generó el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    public void Interaccion1(object sender, System.EventArgs e)
    {
        System.Diagnostics.Debug.Print("Interacción 1 ejecutada.");
    }

    /// <summary>
    /// Lógica del segundo elemento de interacción del plugin.
    /// </summary>
    /// <param name="sender">Objeto que generó el evento.</param>
    /// <param name="e">Argumentos del evento.</param>

    public void Interaccion1(object sender, System.EventArgs e)
    {
        System.Diagnostics.Debug.Print("Interacción 2 ejecutada.");
    }

    #endregion
}