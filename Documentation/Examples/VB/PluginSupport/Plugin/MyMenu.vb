'
' MyPlugin.vb
'
'  This file is part of MCART
'
' Author:
'      César Andrés Morgan <xds_xps_ivx@hotmail.com>
'
' Copyright (c) 2011 - 2018 César Andrés Morgan
'
' MCART Is free software: you can redistribute it And/Or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, Or
' (at your option) any later version.
'
' MCART Is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program.  If Not, see <http:'www.gnu.org/licenses/>.
'
' ======================= N O T A   I M P O R T A N T E =======================
'
' Este módulo contiene ejemplos de código para MCART, y no debe ser compilado
' ni incluido en ninguna distribución de MCART. Su propósito es el de brindar
' un espacio que permita la creación de documentación para compilar, por lo que
' el código contenido aquí no está pensado para su distribución o uso en un
' entorno de producción, sino más bien como una referencia a partir de la cual
' el desarrollador podrá aprender a utilizar la librería y crear su propio
' código.

Public Class MyPlugin
#Region "Example1"
    ''' <summary>
    ''' Constructor del Plugin.
    ''' </summary>
    Public Sub New()
        ' Crear la interacción...
        Dim Interact1 As New InteractionItem(AddressOf Interaccion1, "Interacción 1", "Muestra un mensaje en la salida de la depuración.")

        ' Agregar la interacción al menú de este plugin...
        MyMenu.Add(Interac1)


        ' También es posible utilizar delegados o lambdas con firma compatible
        ' con System.EventHandler
        MyMenu.Add(
            New InteractionItem(
            Sub(sender As Object, e As System.EventArgs)
                System.Diagnostics.Debug.Print("Interacción 2 ejecutada.")
            End Sub,
            "Interacción 2", "Muestra otro mensaje en la salida de la depuración."))
    End Sub

    ''' <summary>
    ''' Lógica del primer elemento de interacción del plugin.
    ''' </summary>
    ''' <param name="sender">Objeto que generó el evento.</param>
    ''' <param name="e">Argumentos del evento.</param>
    Public Sub Interaccion1(sender As Object, e As System.EventArgs)
        System.Diagnostics.Debug.Print("Interacción 1 ejecutada.")
    End Sub
#End Region
End Class