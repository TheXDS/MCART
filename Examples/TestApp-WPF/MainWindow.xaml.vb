'
' MainWindow.xaml.vb
'
' Author:
'      César Morgan <xds_xps_ivx@hotmail.com>
'
' Copyright (c) 2017 César Morgan
'
' This program Is free software: you can redistribute it And/Or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, Or
' (at your option) any later version.
'
' This program Is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program.  If Not, see <http://www.gnu.org/licenses/>.

Imports MCART.PluginSupport
Imports MCART.Forms
Imports MCART
Imports MCART.Controls
Public Class MainWindow
    Private pl As New List(Of IPlugin)
    Private Sub Wndtest_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        pl = Plugin.LoadEverything(Of IPlugin)()
        For Each j As IPlugin In pl
            If j.HasInteractions Then
                mnuPlugins.Items.Add(j.UIMenu)
            End If
        Next
    End Sub
    Private Sub MnuAboutMCART_Click(sender As Object, e As RoutedEventArgs) Handles mnuAboutMCART.Click
        'With New MCART.Forms.
        '    .ShowDialog()
        'End With
    End Sub
    Private Sub MnuExit_Click(sender As Object, e As RoutedEventArgs) Handles mnuExit.Click
        Close()
    End Sub
    Private Sub MnuPwd_Click(sender As Object, e As RoutedEventArgs) Handles mnuPwd.Click
        With (New PasswordDialog).GetPassword("Usuario", "Test@1234", True)
            If .Result = MessageBoxResult.OK Then MessageBox.Show(.Usr & vbCrLf & .Pwd)
        End With
    End Sub
    Private Sub MnuSetPw_Click(sender As Object, e As RoutedEventArgs) Handles mnuSetPw.Click
        With (New PasswordDialog).ChoosePassword(Security.Password.PwMode.UsrBoth)
            If .Result = MessageBoxResult.OK Then MessageBox.Show(.Usr & vbCrLf & .Pwd)
        End With
    End Sub
    Private Sub MnuImgfilter_Click(sender As Object, e As RoutedEventArgs) Handles mnuImgfilter.Click
        'MCART.UI.SaveBitmap(MCART.UITools.Render(Me))
    End Sub
    Private Sub Mnuitskrconsole_Click(sender As Object, e As RoutedEventArgs) Handles mnuitskrconsole.Click
        With New Types.TaskReporter.ConsoleTaskReporter
            .For(1000, Sub(a, b) Threading.Thread.Sleep(10))
        End With
    End Sub
    Private Sub Mnuitskrwndw_Click(sender As Object, e As RoutedEventArgs) Handles mnuitskrwndw.Click
        'With New ProgressWindow
        '    .For(1000, Sub(a, b) Threading.Thread.Sleep(50))
        'End With
    End Sub
    Private Sub MnuGenPw_Click(sender As Object, e As RoutedEventArgs) Handles mnuGenPw.Click
        MsgBox(Security.Password.Generators.Pin)
    End Sub
    Private Sub MnuGrphRing_Click(sender As Object, e As RoutedEventArgs) Handles mnuGrphRing.Click
        Dim r As New RingGraph() With {.SubLevelsShown = 2, .Title = "Test"}
        r.Slices.Add(New Slice() With {.SliceColor = Colors.Red})
        Dim a As New Slice() With {.SliceColor = Colors.Purple}
        a.SubSlices.Add(New Slice() With {.SliceColor = Colors.Blue})
        a.SubSlices.Add(New Slice() With {.SliceColor = Colors.Green})
        r.Slices.Add(a)
        r.Slices.Add(New Slice() With {.SliceColor = Colors.Yellow})
        With New GrphTest
            .ShowGraph(r)
        End With
    End Sub
End Class