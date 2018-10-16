Public Class Layer

    Private Sub refreshButton_Click(sender As Object, e As RoutedEventArgs)


        Dim e2 = New TempEventArgs
        RaiseEvent Refresh(Me, e2)

        Me.layers.Text = e2.Text

    End Sub

    Public Event Refresh As EventHandler(Of TempEventArgs)

End Class

Public Class TempEventArgs
    Inherits EventArgs

    Public Property Text As String

End Class