Public Class InsertTextWindow
    Implements IHasUsercontrol

    Public ReadOnly Property MainUserControl As UserControl Implements IHasUsercontrol.MainUserControl
        Get
            Return Me.insertText
        End Get
    End Property

    Private favorites As New List(Of InsertTextFavorite)

    Private Sub insertText_InsertButtonClick(sender As Object, e As InsertTextEventArgs)

        Me.favorites.Insert(0, New InsertTextFavorite With {.Text = e.Text})
        Me.insertText.Favorites = Me.favorites.ToArray()


    End Sub
End Class
