Imports System.Collections.ObjectModel
Imports EditorPlus.UI.ViewModels

Public Class BulkTaskItems

    Private items As New ObservableCollection(Of TaskItem)()

    Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)

        'Me.items = New ObservableCollection(Of TaskItem)({
        '    New TaskItem With {.Subject = "A"},
        '    New TaskItem With {.Subject = "B"}
        '    })

        Me.inputDataGrid.ItemsSource = Me.items
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
                    Let subject = geText(values, 0)
                    Where Not String.IsNullOrWhiteSpace(subject)
                    Let duedateText = geText(values, 1)
                    Select New TaskItem With {
                        .Subject = subject,
                        .DueDate = toDueDate(duedateText)
                        }

                For Each item In items
                    CType(Me.inputDataGrid.ItemsSource, ObservableCollection(Of TaskItem)).Add(item)
                Next item
                e.Handled = True
            End If

        End If

    End Sub

    Public Sub ApplyTheme()

        ControlTeheme.Apply(OfficeTheme.Current)
        Me.Resources("ControlBackground") = ControlTeheme.Control.Background
        Me.Resources("ControlForeground") = ControlTeheme.Control.Foreground

        Me.Resources("HorizontalGridLines") = ControlTeheme.Grid.HorizontalGridLines
        Me.Resources("VerticalGridLines") = ControlTeheme.Grid.VerticalGridLines
        Me.Resources("GridBackground") = ControlTeheme.Grid.Background
        Me.Resources("GridForeground") = ControlTeheme.Grid.Foreground
        Me.Resources("HilightBackground") = ControlTeheme.Grid.HilightBackground
        Me.Resources("HilightForeground") = ControlTeheme.Grid.HilightForeground
        Me.Resources("CurrentBorder") = ControlTeheme.Grid.CurrentBorder

        Me.Resources("HeaderlBackground") = ControlTeheme.Header.Background
        Me.Resources("HeaderForeground") = ControlTeheme.Header.Foreground
        Me.Resources("HeaderBorder") = ControlTeheme.Header.Border

    End Sub

    Private Sub selectAllCellsButton_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Me.inputDataGrid.SelectAllCells()
    End Sub

    Private Sub AddButton_Click(sender As Object, e As RoutedEventArgs) Handles AddButton.Click
        RaiseEvent AddButtonClick(Me, New BulkTaskItemsEventArgs With {.Items = Me.items})

    End Sub

    Public Event AddButtonClick As EventHandler(Of BulkTaskItemsEventArgs)

End Class

Namespace ViewModels

    Public Class TaskItem
        Public Property Subject As String
        Public Property DueDate As DateTime?
    End Class
End Namespace

Public Class BulkTaskItemsEventArgs
    Inherits EventArgs

    Public Property Items As IEnumerable(Of TaskItem)

End Class