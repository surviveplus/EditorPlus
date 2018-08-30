Imports System.Collections.ObjectModel

Public Class Bulk
    Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)


        Dim items As New ObservableCollection(Of TaskItem)({
            New TaskItem With {.Subtitle = "A"},
            New TaskItem With {.Subtitle = "B"}
            })

        Me.inputDataGrid.ItemsSource = items

    End Sub


    Private Sub inputDataGrid_PreviewKeyDown(sender As Object, e As KeyEventArgs)

        If e.Key = Key.Enter Then
            With CType(sender, DataGrid)
                If .CurrentColumn.DisplayIndex + 1 = .Columns.Count Then
                    If .Items.Count = .SelectedIndex + 1 Then
                        ' Do Nothing
                    Else
                        .CommitEdit()
                        .SelectedIndex += 1
                        .CurrentCell = New DataGridCellInfo(.Items(.SelectedIndex), .Columns(0))
                        .BeginEdit()
                        e.Handled = True
                    End If

                Else
                    .CommitEdit()
                    .CurrentColumn = .Columns(.CurrentColumn.DisplayIndex + 1)
                    .BeginEdit()
                    e.Handled = True
                End If

            End With

        ElseIf e.Key = Key.V AndAlso
            (Keyboard.Modifiers And ModifierKeys.Control) = ModifierKeys.Control Then

            Dim text = Clipboard.GetText()

            If text.Contains(vbTab) OrElse text.Contains(vbCr) Then
                Dim toDueDate =
                Function(v As String) As DateTime?
                    Dim d As DateTime
                    If DateTime.TryParse(v, d) Then
                        Return d
                    End If
                    Return Nothing
                End Function

                Dim geText =
                Function(values As IEnumerable(Of String), index As Integer) As String
                    If values.Count >= index + 1 Then
                        Return values(index).Trim()
                    End If
                    Return Nothing
                End Function

                Dim items =
                    From line In text.Split(vbCr)
                    Let values = line.Split(vbTab)
                    Let subtitle = geText(values, 0)
                    Where Not String.IsNullOrWhiteSpace(subtitle)
                    Let duedateText = geText(values, 1)
                    Select New TaskItem With {
                        .Subtitle = subtitle,
                        .DueDate = toDueDate(duedateText)
                        }

                For Each item In items
                    CType(Me.inputDataGrid.ItemsSource, ObservableCollection(Of TaskItem)).Add(item)
                Next item
                e.Handled = True
            End If

        End If

    End Sub


End Class

Public Class TaskItem
    Public Property Subtitle As String
    Public Property DueDate As DateTime?
End Class