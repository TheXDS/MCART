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

Imports TheXDS.MCART.PluginSupport
Imports TheXDS.MCART.Dialogs
Imports TheXDS.MCART
Imports TheXDS.MCART.Controls
Imports TheXDS.MCART.Pages
Imports TheXDS.MCART.Resources
Imports TheXDS.MCART.Security.Password
Imports TheXDS.MCART.Types

Public Class ModelTest
    Public Property Text As String
    Public Property ValidRange As Range(Of Integer) = New Range(Of Integer)(1, 10)
End Class

Public Class MainWindow
    Private pl As IEnumerable(Of IPlugin)
    Private Sub MnuWindow_Click(sender As Object, e As EventArgs) Handles MnuWindow.Click
        Dim v = New Window() With {
            .WindowStyle = WindowStyle.None,
            .Opacity = 0.5,
            .AllowsTransparency = True
        }
        AddHandler v.Loaded, Sub(o, args) v.EnableBlur()
        v.Show()
    End Sub

    Private Sub Wndtest_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        pl = New PluginLoader().LoadEverything()
        For Each j As IPlugin In pl
            If j.HasInteractions Then
                mnuPlugins.Items.Add(j.UIMenu)
            End If
        Next
    End Sub
    Private Sub MnuPlgBrowser_Click(sender As Object, e As EventArgs) Handles mnuPlgBrowser.Click
        With New PluginBrowser
            MessageBox.Show(.ShowDialog()?.ToString())
        End With
    End Sub
    Private Sub MnuAboutApp_Click(sender As Object, e As RoutedEventArgs) Handles mnuAboutApp.Click
        AboutBox.ShowDialog(My.Application)
    End Sub
    Private Sub MnuAboutMCART_Click(sender As Object, e As RoutedEventArgs) Handles mnuAboutMCART.Click
        RTInfo.Show()
    End Sub
    Private Sub MnuExit_Click(sender As Object, e As RoutedEventArgs) Handles mnuExit.Click
        Close()
    End Sub
    Private Async Function LoginTest(credential As ICredential) As Task(Of Boolean)
        Await Task.Run(Sub() System.Threading.Thread.Sleep(1500))
        Return credential.Username = "Usuario" AndAlso credential.Password.Read() = "Test@1234"
    End Function
    Private Sub MnuPwd_Click(sender As Object, e As RoutedEventArgs) Handles mnuPwd.Click
        With PasswordDialog.Login("Usuario", AddressOf LoginTest)
            If .Itself() Then MessageBox.Show(.Itself().ToString())
        End With
    End Sub
    Private Sub MnuSetPw_Click(sender As Object, e As RoutedEventArgs) Handles mnuSetPw.Click
        Dim ud As UserData
        With PasswordDialog.GetUserData(String.Empty, String.Empty, String.Empty, PasswordEvaluators.ComplexEvaluator, Nothing, Nothing, ud, True)
            If .Itself() Then MessageBox.Show($"{ud.Username}\n{ud.Password.Read()}\n{ud.Hint}\n{ud.Quality}")
        End With
    End Sub
    Private Sub MnuImgfilter_Click(sender As Object, e As RoutedEventArgs) Handles mnuImgfilter.Click
        'MCART.UI.SaveBitmap(MCART.UITools.Render(Me))
    End Sub
    Private Sub Mnuitskrconsole_Click(sender As Object, e As RoutedEventArgs) Handles mnuitskrconsole.Click
    End Sub
    Private Sub Mnuitskrwndw_Click(sender As Object, e As RoutedEventArgs) Handles mnuitskrwndw.Click
        'With New ProgressWindow
        '    .For(1000, Sub(a, b) Threading.Thread.Sleep(50))
        'End With
    End Sub
    Private Sub MnuPwGenPin_Click(sender As Object, e As RoutedEventArgs) Handles mnuPwGenPin.Click
        MsgBox(Security.Password.Generators.Pin.Generate().Read())
    End Sub
    Private Sub MnuPwGenSafe_Click(sender As Object, e As RoutedEventArgs) Handles mnuPwGenSafe.Click
        MsgBox(Security.Password.Generators.Safe.Generate().Read())
    End Sub
    Private Sub MnuPwGenComplex_Click(sender As Object, e As RoutedEventArgs) Handles mnuPwGenComplex.Click
        MsgBox(Security.Password.Generators.VeryComplex.Generate().Read())
    End Sub
    Private Sub MnuPwGenExtreme_Click(sender As Object, e As RoutedEventArgs) Handles mnuPwGenExtreme.Click
        MsgBox(Security.Password.Generators.ExtremelyComplex.Generate().Read())
    End Sub
    Private Sub MnuGrphRing_Click(sender As Object, e As RoutedEventArgs) Handles mnuGrphRing.Click
        Dim r As New RingGraph() With {
            .SubLevelsShown = 3,
            .Title = "Test",
            .Colorizer = New HeatColorizer(),
            .ToolTipFormat = "{0}: {1}"}
        'r.Slices.Add(New Slice())
        'r.Slices.Add(New Slice() With{.Value=2.5})
        'r.Slices.Add(New Slice())
        Dim rrr As New Random()
        For i As Integer = 0 To 2
            r.Slices.Add(New Slice()) 'With {.Value = rrr.NextDouble() + 0.5})
        Next
        'r.Slices.Add(New Slice() With{.Value=0.7})
        'Dim a As New Slice()
        'a.SubSlices.Add(New Slice())
        'Dim b As New Slice() With{ .Value=2.1}
        'Dim c As New Slice()
        'b.SubSlices.Add(c)
        'a.SubSlices.Add(b)
        'r.Slices.Add(a)
        'r.Slices.Add(New Slice())
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