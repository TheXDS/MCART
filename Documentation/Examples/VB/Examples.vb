'
' Examples.vb
'
'  This file is part of Morgan's CLR Advanced Runtime (MCART)
'
' Author:
'      César Andrés Morgan <xds_xps_ivx@hotmail.com>
'
' Copyright (c) 2011 - 2017 César Andrés Morgan
'
' Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it And/Or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, Or
' (at your option) any later version.
'
' Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will be useful,
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

Imports MCART.Attributes
Imports MCART.MCADatabase

Namespace Examples
    ''' <summary>
    ''' Ejemplos de MCART (NO UTILIZAR EN CÓDIGO)
    ''' </summary>
    <Unusable> Module Examples
        Private TskReporter As Threading.ITaskReporter = Nothing
        Private Sub iTaskReporterExample1()
#Region "ITaskReporterExample1"
            ' Las tareas que reportan su estado deben
            ' incluir esta llamada. Esto causa que el
            ' control se active.
            TskReporter.Begin()

            ' Este ciclo es muy tardado.
            For i As Integer = 1 To 1000

                ' Comprobar si la tarea se ha intentado cancelar
                If TskReporter.CancelPending Then Exit For

                ' Reportar el estado de la operación
                TskReporter.Report(i / 1000, "Haciendo algo mil veces...")
                Threading.Thread.Sleep(50)
                ' Aquí se coloca el cuerpo de las operaciones a realizar.
                ' *******************************************************
            Next
            ' Al finalizar una tarea, se debe informar
            ' al control por medio del siguiente
            ' método:
            TskReporter.End()
#End Region
#Region "ITaskReporterExample3"
            Dim Timeout As New TimeSpan(0, 0, 15)
            Dim TStart As Date = Date.Now

            TskReporter.Begin()
            While True
                If TskReporter.CancelPending Then Exit While
                If Now - TStart > Timeout Then
                    TskReporter.Stop("Se agotó el tiempo de espera")
                    Return
                End If

            End While
#End Region
#Region "ITaskReporterExample4"
            Dim O As New Text.StringBuilder

            ' Definir el cuerpo de un ciclo For Each.
            Dim Act As Threading.ForEachAction(Of Reflection.Assembly) =
                Sub(i As Reflection.Assembly, t As Threading.ITaskReporter)
                    O.AppendLine(i.Location)
                End Sub

            ' Ejecutar el ciclo For Each definido en Act en TskReporter
            TskReporter.ForEach(AppDomain.CurrentDomain.GetAssemblies, Act, "Obteniendo las rutas de todos los ensamblados cargados...")

            MessageBox.Show(O.ToString)
#End Region
        End Sub
#Region "ITaskReporterExample2"
        Private Async Sub iTaskReporterExample()
            ' Inicia una tarea no cancelable
            TskReporter.BeginNonStop()

            ' Reportar estado, sin mostrar el progreso
            TskReporter.Report("Esperando operación...")

            'Representa una operación larga que soporta Await
            Await Task.Run(Sub() Threading.Thread.Sleep(10000))

            ' Finalizó la tarea
            TskReporter.End()

        End Sub
#End Region
        Private Sub HacerAlgo(ParamArray a() As Object)
        End Sub
        Private Sub ItselfExample(List1 As Object, List2 As Object)
#Region "ItselfExample"
            ' Realizar un ciclo sobre todos los elementos de una lista
            For Each i As Object In List1

                'i.Miembro.SubMiembro es una clase que implementa .Itself
                With i.Miembro.SubMiembro

                    ' Hacer algo con la instalcia referenciada
                    ' en este bloque With
                    HacerAlgo(.Itself)

                    ' Llamar a un método miembro
                    .HacerOtraCosa(1, 2, 3)

                    ' Añadir la instancia referenciada en este
                    ' bloque With a una lista
                    List2.Add(.Itself)
                End With
            Next
#End Region
        End Sub
        Private Class SomeClass
        End Class
#Region "MCADBExample1"
        <Serializable> Public Class AnElement
            Inherits MCADBElement
            '
            ' Valores y propiedades de la clase
            '

            'Si hay elementos que no son tipos de valor, deben ser marcados como &lt;NonSerialized&gt;.
            <NonSerialized> Private SomeObj As SomeClass
        End Class
#End Region
#Region "MCADBExample2"
        ''' <summary>
        ''' Representa un libro
        ''' </summary>
        <Serializable> Public Class Libro
             Inherits MCADBElement

            'La propiedad Uid de MCADBElement es utilizada para
            'guardar la clave primaria que identifica de forma
            'única a un elemento dentro de un árbol.

            'Es posible utilizar la propiedad Name de MCADBElement
            'para almacenar el nombre del libro, por lo tanto no la
            'declararemos aquí.

            Private _ISBN As String

            ''' <summary>
            ''' Devuelve el ISBN de un libro
            ''' </summary>
            Public Property ISBN As String
                Get
                    Return _ISBN
                End Get
                Set(value As String)
                    _ISBN = value
                End Set
            End Property


            ' Las colecciones de tipos de valor son admitidas dentro de
            ' un MCADBElement. Sin embargo, no se pueden serializar.
            Private _Autores As New List(Of String)

            ''' <summary>
            ''' Devuelve una lista con los autores de un libro
            ''' </summary>
            <NonSerialized> Public Property Autores As List(Of String)
                Get
                    Return _Autores
                End Get
                Set(value As List(Of String))
                    _Autores = value
                End Set
            End Property


            'Esta propiedad establece un valor predeterminado para el elemento
            Private _Edicion As Integer = 1

            ''' <summary>
            ''' Devuelve el número de edición de un libro
            ''' </summary>
            Public Property Edicion As Integer
                Get
                    Return _Edicion
                End Get
                Set(value As Integer)
                    _Edicion = value
                End Set
            End Property
        End Class
#End Region
#Region "MCADBExample3"
        ''' <summary>
        ''' Representa una transacción. No requiere de un código legible por el usuario.
        ''' </summary>
        <Serializable> <AutoIncrement> Public Class Transaccion
            Inherits MCADBElement
            ' Al declarar la clase con el atributo <see cref="AutoIncrementAttribute"/>,
            ' el campo Uid incrementará automáticamente.

            Private _indice As Integer

            ''' <summary>
            ''' Este campo incrementa de valor automáticamente.
            ''' </summary>
            <AutoIncrement> Public Property Indice As Integer
                Get
                    Return _indice
                End Get
                Set(value As Integer)
                    _indice = value
                End Set
            End Property

            ''' <summary>
            ''' Este campo incrementa de valor automáticamente.
            ''' </summary>
            <AutoIncrement> Public Cuenta As Long
        End Class
#End Region
    End Module
End Namespace