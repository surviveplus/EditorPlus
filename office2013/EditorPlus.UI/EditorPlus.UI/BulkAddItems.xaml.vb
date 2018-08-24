Public Class BulkAddItems
    Private Sub AddButton_Click(sender As Object, e As RoutedEventArgs) Handles AddButton.Click


        RaiseEvent AddButtonClick(Me, New BulkAddItemsEventArgs With {.Items = Me.itemsBox.Text})

    End Sub

    Public Event AddButtonClick As EventHandler(Of BulkAddItemsEventArgs)
End Class

Public Class BulkAddItemsEventArgs
    Inherits EventArgs

    Public Property Items As String

End Class