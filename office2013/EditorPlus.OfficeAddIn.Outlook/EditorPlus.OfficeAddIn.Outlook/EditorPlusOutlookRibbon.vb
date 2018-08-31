Imports EditorPlus.UI
Imports Microsoft.Office.Interop.Outlook
Imports Microsoft.Office.Tools.Ribbon
Imports Net.Surviveplus.SakuraMacaron.OfficeAddIn.UI

Public Class EditorPlusOutlookRibbon

    Private Sub EditorPlusOutlookRibbon_Load(ByVal sender As System.Object, ByVal e As RibbonUIEventArgs) Handles MyBase.Load
    End Sub

    Private bulkAddTasksPane As ElementControlPane(Of BulkTaskItems)
    Private bulkTaskItemsControl As BulkTaskItems


    Private Sub BulkAddTasksButton_Click(sender As Object, e As RibbonControlEventArgs) Handles BulkAddTasksButton.Click

        If Me.bulkAddTasksPane Is Nothing Then

            Dim c = New BulkTaskItems
            AddHandler c.AddButtonClick,
                Sub(sender2, e2)

                    Dim count As Integer = 0
                    For Each t In (From a In e2.Items Where Not String.IsNullOrWhiteSpace(a.Subject))

                        Dim newItem As TaskItem = ThisAddIn.Current.Application.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olTaskItem)
                        newItem.Subject = t.Subject
                        If t.DueDate.HasValue Then
                            newItem.DueDate = t.DueDate.Value
                        End If

                        newItem.Save()
                        count += 1

                        ThisAddIn.Current.Application.ActiveExplorer.CurrentView = CType(newItem.Parent, Folder).CurrentView
                    Next t

                    MsgBox(String.Format("{0} items were saved.", count), MsgBoxStyle.Information, "Bulk Add Tasks")

                End Sub
            Me.bulkTaskItemsControl = c
            Me.bulkAddTasksPane = New ElementControlPane(Of BulkTaskItems)(c)
            Me.bulkAddTasksPane.Pane = ThisAddIn.Current.CustomTaskPanes.Add(Me.bulkAddTasksPane.Control, "Bulk Add Tasks", ThisAddIn.Current.Application.ActiveWindow)
            Me.bulkAddTasksPane.Pane.Width = 350
        End If

        Me.bulkTaskItemsControl.ApplyTheme()
        Me.bulkAddTasksPane?.Show()
    End Sub
End Class
