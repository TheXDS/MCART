/*
uiMenu.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author:
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright (c) 2011 - 2017 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it And/Or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, Or
(at your option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
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
#region uiMenu1
public class MyPlugin : Plugin
{
    // Este es un elemento de interacción que MCART cableará automáticamente.
    [Name("Interacción 1")]
    [Description("Muestra un mensaje de interacción.")]
    [InteractionItem]
    public void Interaccion1(object sender, System.EventArgs e)
    {
        System.Diagnostics.Debug.Print("Interacción 1 ejecutada.");
    }

    // Este es un elemento de interacción cableado manualmente.
    public void Interaccion2(object sender, System.EventArgs e)
    {
        System.Diagnostics.Debug.Print("Interacción 2 ejecutada.");
    }

    // Este es un elemento de interacción cableado manualmente, 
    // con atributos que especifican el nombre y la descripción.
    [Name("Interacción 3")]
    [Description("Muestra un tercer mensaje de interacción.")]
    public void Interaccion3(object sender, System.EventArgs e)
    {
        System.Diagnostics.Debug.Print("Interacción 3 ejecutada.");
    }

    /// <summary>
    /// Constructor del Plugin.
    /// </summary>
    public MyPlugin()
    {
        // Crear una segunda interacción...
        var Interac2 = new InteractionItem(Interaccion2,
            "Interacción 2", 
            "Muestra un segundo mensaje en la salida de la depuración.");

        // Agregar la interacción al menú de este plugin...
        uiMenu.Add(Interac2);


        // Alternativamente, pueden establecerse atributos a la acción de
        // interacción, lo que resulta en un mayor orden del código.
        uiMenu.Add(new InteractionItem(Interaccion3));


        // También es posible utilizar delegados o lambdas con firma compatible
        // con System.EventHandler
        uiMenu.Add(
            new InteractionItem((sender, e) =>
            {
                System.Diagnostics.Debug.Print("Interacción 4 ejecutada.");
            },
            "Interacción 4", "Muestra otro mensaje en la salida de la depuración."));
    }
}
#endregion
