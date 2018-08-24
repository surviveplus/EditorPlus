Imports EditorPlus.UI
Imports Microsoft.Office.Interop.Outlook
Imports Microsoft.Office.Tools.Ribbon
Imports Net.Surviveplus.SakuraMacaron.OfficeAddIn.UI

Public Class EditorPlusOutlookRibbon

    Private Sub EditorPlusOutlookRibbon_Load(ByVal sender As System.Object, ByVal e As RibbonUIEventArgs) Handles MyBase.Load

    End Sub

    Private bulkAddTasksPane As ElementControlPane(Of BulkAddItems)

    Private Sub BulkAddTasksButton_Click(sender As Object, e As RibbonControlEventArgs) Handles BulkAddTasksButton.Click

        If Me.bulkAddTasksPane Is Nothing Then

            Dim c = New BulkAddItems
            AddHandler c.AddButtonClick,
                Sub(sender2, e2)

                    Dim tasks = From line In e2.Items.Split(vbLf)
                                Let properties = line.Split(vbTab)
                                Select New With {
                                    .Subject = properties(0).Trim(),
                                    .DueDate = If(properties.Count > 1, properties(1).Trim(), Nothing)
                                    }

                    'Dim mapi = ThisAddIn.Current.Application.GetNamespace("MAPI")
                    'Dim view = mapi.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderTasks)
                    'view.Display()

                    For Each t In (From a In tasks Where Not String.IsNullOrWhiteSpace(a.Subject))

                        Dim newItem As TaskItem = ThisAddIn.Current.Application.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olTaskItem)
                        newItem.Subject = t.Subject

                        Dim dueDate As DateTime
                        If DateTime.TryParse(t.DueDate, dueDate) Then 
                            newItem.DueDate = dueDate
                        End If

                        newItem.Save()

                        ThisAddIn.Current.Application.ActiveExplorer.CurrentView = CType(newItem.Parent, Folder).CurrentView
                    Next t


                End Sub

            Me.bulkAddTasksPane = New ElementControlPane(Of BulkAddItems)(c)
            Me.bulkAddTasksPane.Pane = ThisAddIn.Current.CustomTaskPanes.Add(Me.bulkAddTasksPane.Control, "Bulk Add Tasks", ThisAddIn.Current.Application.ActiveWindow)
            Me.bulkAddTasksPane.Pane.Width = 350
        End If

        Me.bulkAddTasksPane?.Show()
    End Sub
End Class
