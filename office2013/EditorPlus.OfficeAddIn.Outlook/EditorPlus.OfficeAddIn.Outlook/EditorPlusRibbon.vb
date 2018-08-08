Imports System.Diagnostics
Imports EditorPlus.Core
Imports EditorPlus.AI
Imports EditorPlus.UI
Imports Microsoft.Office.Tools.Ribbon
Imports Net.Surviveplus.SakuraMacaron.OfficeAddIn.Outlook
Imports Net.Surviveplus.SakuraMacaron.OfficeAddIn.UI

Public Class EditorPlusRibbon

    Private Sub TopMostToggleButton_Click(sender As Object, e As RibbonControlEventArgs) Handles TopMostToggleButton.Click
        AlwaysOnTop.EqualizeWithRibbonToggleButton(sender)
    End Sub

End Class
