Imports TheXDS.MCART.Resources.RTInfo
Imports TheXDS.MCART.Attributes
<Assembly: MinMCARTVersion(0, 8)>
<Assembly: TargetMCARTVersion(0, 8)>
Class Application
    Private Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        If Not RTSupport(GetType(Application).Assembly) AndAlso MessageBox.Show(
            "Este programa podría no funcionar correctamente con esta versión de MCART. ¿Desea continuar?", RTVersion.ToString(),
            MessageBoxButton.YesNo, MessageBoxImage.Exclamation) = MessageBoxResult.No Then End
    End Sub
End Class