Public Class InsertText

    Private Function GetInsertTextEventArgs(insertTo As InsertTo) As InsertTextEventArgs
        Return New InsertTextEventArgs With {
            .InsertTo = insertTo,
            .Text = Me.TextBox.Text,
            .SkipIfStartedOrEndWithText = Me.SkipIfStartedOrEndWithTextCheckBox.IsChecked}
    End Function

    Public Event InsertButtonClick As EventHandler(Of InsertTextEventArgs)

    Private Sub InsertToHeadButton_Click(sender As Object, e As RoutedEventArgs)
        RaiseEvent InsertButtonClick(Me, Me.GetInsertTextEventArgs(InsertTo.Head))
    End Sub

    Private Sub InsertToLineHeadButton_Click(sender As Object, e As RoutedEventArgs)
        RaiseEvent InsertButtonClick(Me, Me.GetInsertTextEventArgs(InsertTo.LineHead))
    End Sub

    Private Sub InsertToLineEndButton_Click(sender As Object, e As RoutedEventArgs)
        RaiseEvent InsertButtonClick(Me, Me.GetInsertTextEventArgs(InsertTo.LineEnd))
    End Sub

    Private Sub InsertToEndButton_Click(sender As Object, e As RoutedEventArgs)
        RaiseEvent InsertButtonClick(Me, Me.GetInsertTextEventArgs(InsertTo.End))
    End Sub
End Class

Public Class InsertTextEventArgs
    Inherits EventArgs

    Public Property InsertTo As InsertTo
    Public Property Text As String

    ' Skip if started/end with text
    Public Property SkipIfStartedOrEndWithText As Boolean

End Class

Public Enum InsertTo
    Head
    LineHead
    LineEnd
    [End]
End Enum