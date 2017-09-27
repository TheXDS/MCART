Imports MCART.Controls

Public Class GrphTest
    Private grph As IGraph
    Public Sub ShowGraph(g As IGraph)
        pnlRoot.Children.Add(g)
        grph = g
        Show()
    End Sub
    Private Sub Rfrsh(sender As Object, e As RoutedEventArgs) Handles btnRefresh.Click
        grph.Redraw()
    End Sub
End Class