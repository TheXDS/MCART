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
    Private pl As IEnumerable(Of IPlugin)
    Private Sub Wndtest_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        pl = (New PluginLoader()).LoadEverything()
        For Each j As IPlugin In pl
            If j.HasInteractions Then
                mnuPlugins.Items.Add(j.UIMenu)
            End If
        Next
    End Sub
    Private Sub MnuPlgBrowser_Click(sender As Object, e As EventArgs) Handles mnuPlgBrowser.Click
        With New PluginBrowser
            .ShowDialog()
        End With
    End Sub
    Private Sub MnuAboutMCART_Click(sender As Object, e As RoutedEventArgs) 'Handles mnuAboutMCART.Click
        'With New MCART.Forms.
        '    .ShowDialog()
        'End With
    End Sub
    Private Sub MnuExit_Click(sender As Object, e As RoutedEventArgs) Handles mnuExit.Click
        Close()
    End Sub
    Private Sub MnuPwd_Click(sender As Object, e As RoutedEventArgs) Handles mnuPwd.Click
        With (New PasswordDialog).Login("Usuario", "Test@1234")
            If .Result = MessageBoxResult.OK Then MessageBox.Show(.Usr & vbCrLf & .Pwd.ReadString())
        End With
    End Sub
    Private Sub MnuSetPw_Click(sender As Object, e As RoutedEventArgs) Handles mnuSetPw.Click
        With (New PasswordDialog).ChoosePassword(Security.Password.PwMode.UsrBoth)
            If .Result = MessageBoxResult.OK Then MessageBox.Show(.Usr & vbCrLf & .Pwd.ReadString())
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
    Private Sub MnuPwGenPin_Click(sender As Object, e As RoutedEventArgs) Handles mnuPwGenPin.Click
        MsgBox(Security.Password.Generators.Pin.ReadString())
    End Sub
    Private Sub MnuPwGenSafe_Click(sender As Object, e As RoutedEventArgs) Handles mnuPwGenSafe.Click
        MsgBox(Security.Password.Generators.Safe.ReadString())
    End Sub
    Private Sub MnuPwGenComplex_Click(sender As Object, e As RoutedEventArgs) Handles mnuPwGenComplex.Click
        MsgBox(Security.Password.Generators.VeryComplex.ReadString())
    End Sub
    Private Sub MnuPwGenExtreme_Click(sender As Object, e As RoutedEventArgs) Handles mnuPwGenExtreme.Click
        MsgBox(Security.Password.Generators.ExtremelyComplex.ReadString())
    End Sub
    Private Sub MnuGrphRing_Click(sender As Object, e As RoutedEventArgs) Handles mnuGrphRing.Click
        Dim r As New RingGraph() With {
            .SubLevelsShown = 3,
            .Title = "Test",
            .Colorizer = New HeatColorizer(),
            .ToolTipFormat = "{0}: {1}"}
        r.Slices.Add(New Slice())
        r.Slices.Add(New Slice())
        r.Slices.Add(New Slice())
        r.Slices.Add(New Slice())
        r.Slices.Add(New Slice())
        Dim a As New Slice()
        a.SubSlices.Add(New Slice())
        Dim b As New Slice()
        Dim c As New Slice()
        b.SubSlices.Add(c)
        a.SubSlices.Add(b)
        r.Slices.Add(a)
        r.Slices.Add(New Slice())
        With New GrphTest
            .ShowGraph(r)
        End With
    End Sub
    Private Sub MnuProgressRing_Click(sender As Object, e As RoutedEventArgs) Handles mnuProgressRing.Click
        With New ProgressTest()
            .ShowDialog()
        End With
    End Sub
End Class