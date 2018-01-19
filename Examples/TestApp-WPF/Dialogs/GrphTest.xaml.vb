Imports TheXDS.MCART.Controls

Public Class GrphTest
    Private grph As IGraph
    Public Sub ShowGraph(g As IGraph)
        pnlRoot.Children.Add(CType(g, UIElement))
        grph = g
        Show()
    End Sub
End Class