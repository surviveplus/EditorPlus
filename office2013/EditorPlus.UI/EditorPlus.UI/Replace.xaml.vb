Imports Net.Surviveplus.Localization

Public Class Replace

    ''' <summary>
    ''' Initializes a new instance of the class.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()

        WpfLocalization.ApplyResources(Me, My.Resources.ResourceManager)

        Me.findBox.SelectAll()
        Me.findBox.Focus()

    End Sub

    Private Sub ReplaceAllButton_Click(sender As Object, e As RoutedEventArgs)
        RaiseEvent RepaceButtonClick(Me, New ReplaceToolControlEventArgs With {.FindText = Me.findBox.Text, .ReplaceText = Me.replaceBox.Text})
    End Sub

    Public Event RepaceButtonClick As EventHandler(Of ReplaceToolControlEventArgs)

End Class

Public Class ReplaceToolControlEventArgs
    Inherits EventArgs

    Public Property FindText As String

    Public Property ReplaceText As String
End Class
