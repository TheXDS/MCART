Imports TheXDS.MCART.Controls

Public Class GrphTest
    Private grph As IGraph
    Public Sub ShowGraph(g As IGraph)
        pnlRoot.Children.Add(CType(g, UIElement))
        grph = g
        Show()
    End Sub
    Public Sub rfrsh(sender As Object, e As EventArgs) Handles BtnRfrsh.Click
        With grph
            .Redraw()
        End With
    End Sub
End Class