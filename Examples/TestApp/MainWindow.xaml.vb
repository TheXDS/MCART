Imports MCART.PluginSupport
Imports MCART.Forms
Public Class MainWindow
    Private pl As New List(Of IPlugin)
    Private Sub wndtest_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        pl = Plugin.LoadEverything(Of IPlugin)()
        For Each j As IPlugin In pl
            If j.HasInteractions Then
                mnuPlugins.Items.Add(j.UIMenu)
            End If
        Next
    End Sub
    Private Sub mnuAboutMCART_Click(sender As Object, e As RoutedEventArgs) Handles mnuAboutMCART.Click
        'With New MCART.Forms.
        '    .ShowDialog()
        'End With
    End Sub
    Private Sub mnuExit_Click(sender As Object, e As RoutedEventArgs) Handles mnuExit.Click
        Close()
    End Sub
    Private Sub mnuPwd_Click(sender As Object, e As RoutedEventArgs) Handles mnuPwd.Click
        With (New PasswordDialog).GetPassword("Usuario", "Test@1234", True)
            If .Result = MessageBoxResult.OK Then MessageBox.Show(.Usr & vbCrLf & .Pwd)
        End With
    End Sub
    Private Sub mnuSetPw_Click(sender As Object, e As RoutedEventArgs) Handles mnuSetPw.Click
        With (New PasswordDialog).ChoosePassword(MCART.Security.Password.PwMode.UsrBoth)
            If .Result = MessageBoxResult.OK Then MessageBox.Show(.Usr & vbCrLf & .Pwd)
        End With
    End Sub
    Private Sub mnuImgfilter_Click(sender As Object, e As RoutedEventArgs) Handles mnuImgfilter.Click
        'MCART.UI.SaveBitmap(MCART.UITools.Render(Me))
    End Sub
    Private Sub mnuitskrconsole_Click(sender As Object, e As RoutedEventArgs) Handles mnuitskrconsole.Click
        With New MCART.Types.TaskReporter.ConsoleTaskReporter
            .For(1000, Sub(a, b) Threading.Thread.Sleep(10))
        End With
    End Sub
    Private Sub mnuitskrwndw_Click(sender As Object, e As RoutedEventArgs) Handles mnuitskrwndw.Click
        'With New ProgressWindow
        '    .For(1000, Sub(a, b) Threading.Thread.Sleep(50))
        'End With
    End Sub
    Private Sub mnuGenPw_Click(sender As Object, e As RoutedEventArgs) Handles mnuGenPw.Click
        MsgBox(MCART.Security.Password.Generators.Pin)
    End Sub
End Class