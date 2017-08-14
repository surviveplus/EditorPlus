Public Class ThisAddIn

    Public Shared Property Current As ThisAddIn

    Private Sub ThisAddIn_Startup() Handles Me.Startup
        ThisAddIn.Current = Me

        System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo(ThisAddIn.Current.Application.LanguageSettings.LanguageID(Office.MsoAppLanguageID.msoLanguageIDUI))

    End Sub

    Private Sub ThisAddIn_Shutdown() Handles Me.Shutdown
        ThisAddIn.Current = Nothing
    End Sub

End Class
