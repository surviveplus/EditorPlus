Imports System.Diagnostics
Imports EditorPlus.Core
Imports EditorPlus.AI
Imports EditorPlus.UI
Imports Microsoft.Office.Tools.Ribbon
Imports Net.Surviveplus.SakuraMacaron.OfficeAddIn.Outlook
Imports Net.Surviveplus.SakuraMacaron.OfficeAddIn.UI
Imports Microsoft.Office.Interop.Outlook

Public Class EditorPlusRibbon

    Private Sub TopMostToggleButton_Click(sender As Object, e As RibbonControlEventArgs) Handles TopMostToggleButton.Click
        AlwaysOnTop.EqualizeWithRibbonToggleButton(sender)
    End Sub

    Private Sub OpenFolderButton_Click(sender As Object, e As RibbonControlEventArgs) Handles OpenFolderButton.Click

        ' Current Mail item folder
        Dim folder As Folder = Nothing
        Dim mail As MailItem = Nothing

        Dim item = ThisAddIn.Current.Application.ActiveInspector.CurrentItem
        If TypeName(item) = "MailItem" Then
            mail = CType(item, MailItem)
            folder = mail.Parent
        End If

        ' Open the folder
        If folder IsNot Nothing Then

            ' Open in New Explorer
            ' folder.Display()  

            ' Open in Current Explorer
            With ThisAddIn.Current.Application.ActiveExplorer
                .CurrentFolder = folder

                ' Select Mail item
                ' see: 
                ' https://msdn.microsoft.com/en-us/VBA/Outlook-VBA/articles/explorer-isitemselectableinview-method-outlook
                ' The IsItemSelectableInView method raises an error if the current view is a conversation view.

                Try
                    .ClearSelection()
                    .AddToSelection(mail)
                Catch
                    Debug.WriteLine("The IsItemSelectableInView method raises an error if the current view is a conversation view.")
                End Try
            End With
        End If


    End Sub
End Class
