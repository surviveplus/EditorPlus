Imports Net.Surviveplus.Localization

Public Class InsertSerialNumber

    ''' <summary>
    ''' Initializes a new instance of the class.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()

        WpfLocalization.ApplyResources(Me, My.Resources.ResourceManager)

        Me.StartNumberBox.SelectAll()
        Me.StartNumberBox.Focus()

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

    Private Function GetInsertSerialNumberEventArgs(insertTo As InsertTo) As InsertSerialNumberEventArgs

        Dim p As NumberPadding = NumberPadding.NonePadding
        If Me.spacePadding.IsChecked Then
            p = NumberPadding.SpacePadding

        ElseIf Me.zeroPadding.IsChecked Then
            p = NumberPadding.ZeroPadding
        End If

        Return New InsertSerialNumberEventArgs With {
            .InsertTo = insertTo,
            .StartNumber = Convert.ToInt64(Me.StartNumberBox.Text),
            .SkipIfStartedOrEndWithText = Me.SkipIfStartedOrEndWithTextCheckBox.IsChecked,
            .Padding = p}

    End Function

    Public Event InsertButtonClick As EventHandler(Of InsertSerialNumberEventArgs)

    Private Sub InsertToHeadButton_Click(sender As Object, e As RoutedEventArgs)
        RaiseEvent InsertButtonClick(Me, Me.GetInsertSerialNumberEventArgs(InsertTo.Head))
    End Sub

    Private Sub InsertToLineHeadButton_Click(sender As Object, e As RoutedEventArgs)
        RaiseEvent InsertButtonClick(Me, Me.GetInsertSerialNumberEventArgs(InsertTo.LineHead))
    End Sub

    Private Sub InsertToLineEndButton_Click(sender As Object, e As RoutedEventArgs)
        RaiseEvent InsertButtonClick(Me, Me.GetInsertSerialNumberEventArgs(InsertTo.LineEnd))
    End Sub

    Private Sub InsertToEndButton_Click(sender As Object, e As RoutedEventArgs)
        RaiseEvent InsertButtonClick(Me, Me.GetInsertSerialNumberEventArgs(InsertTo.End))
    End Sub

End Class


Public Class InsertSerialNumberEventArgs
    Inherits EventArgs

    Public Property InsertTo As InsertTo

    Public Property StartNumber As Long

    ' Skip if started/end with text
    Public Property SkipIfStartedOrEndWithText As Boolean

    Public Property Padding As NumberPadding

End Class

Public Enum NumberPadding
    NonePadding
    SpacePadding
    ZeroPadding
End Enum