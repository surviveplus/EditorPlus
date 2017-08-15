Imports EditorPlus.Core
Imports Net.Surviveplus.Localization

Public Class InsertText
    ''' <summary>
    ''' Initializes a new instance of the class.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()
        WpfLocalization.ApplyResources(Me, My.Resources.ResourceManager)
    End Sub

    Public Property LineButtonVisible() As Boolean
        Get
            Return Me.InsertToLineHeadButton.Visibility = Visibility.Visible
        End Get
        Set(ByVal value As Boolean)
            If value Then
                Me.InsertToLineHeadButton.Visibility = Visibility.Visible
                Me.InsertToLineEndButton.Visibility = Visibility.Visible
            Else
                Me.InsertToLineHeadButton.Visibility = Visibility.Collapsed
                Me.InsertToLineEndButton.Visibility = Visibility.Collapsed
            End If
        End Set
    End Property

    Private valueOfFavorites As IEnumerable(Of InsertTextFavorite)

    Public Property Favorites As IEnumerable(Of InsertTextFavorite)
        Get
            Return Me.valueOfFavorites
        End Get
        Set(value As IEnumerable(Of InsertTextFavorite))
            Me.valueOfFavorites = value
            Me.FavoritesList.ItemsSource = Me.valueOfFavorites

            If Me.valueOfFavorites IsNot Nothing AndAlso Me.valueOfFavorites.Any() Then
                Me.FavoritesList.Visibility = Visibility.Visible
            Else
                Me.FavoritesList.Visibility = Visibility.Collapsed
            End If
        End Set
    End Property

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

    Private Sub FavoritesList_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Dim item As InsertTextFavorite = Me.FavoritesList.SelectedItem
        If item IsNot Nothing Then
            Me.TextBox.Text = item.Text
        End If
    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As KeyEventArgs)
        If Keyboard.Modifiers = ModifierKeys.Control Then
            Try
                Select Case e.Key
                    Case Key.D1, Key.NumPad1
                        Me.TextBox.Text = Me.FavoritesList.Items(0).Text

                    Case Key.D2, Key.NumPad2
                        Me.TextBox.Text = Me.FavoritesList.Items(1).Text

                    Case Key.D3, Key.NumPad3
                        Me.TextBox.Text = Me.FavoritesList.Items(3).Text

                End Select

            Catch
            End Try
        End If
    End Sub
End Class

Public Class InsertTextEventArgs
    Inherits EventArgs

    Public Property InsertTo As InsertTo
    Public Property Text As String

    ' Skip if started/end with text
    Public Property SkipIfStartedOrEndWithText As Boolean

End Class

Public Class InsertTextFavorite

    Public Property Text As String

End Class