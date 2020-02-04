﻿Public Class ThisAddIn

    Public Shared Property Current As ThisAddIn

    Private Sub ThisAddIn_Startup() Handles Me.Startup
        ThisAddIn.Current = Me
        UI.OfficeAccentColor.Current = UI.AccentColors.Project
    End Sub

    Private Sub ThisAddIn_Shutdown() Handles Me.Shutdown
        ThisAddIn.Current = Nothing
    End Sub

End Class
