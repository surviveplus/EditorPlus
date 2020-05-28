Imports Net.Surviveplus.Localization

Public Class Navigation

    ''' <summary>
    ''' Initializes a new instance of the class.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Me.InitializeComponent()
        WpfLocalization.ApplyResources(Me, My.Resources.ResourceManager)
    End Sub

    Private valueOfPageSize As Size

    Public Property PageSize() As Size
        Get
            Return Me.valueOfPageSize
        End Get
        Set(ByVal value As Size)
            Me.valueOfPageSize = value
            With Me.valueOfPageSize
                Me.pageFrame.Width = .Width
                Me.pageFrame.Height = .Height
            End With

        End Set
    End Property

    Public Event Click As EventHandler(Of NavigationClickEventArgs)

    Private Sub pageFrame_MouseDown(sender As Object, e As MouseButtonEventArgs)
        RaiseClickEvent()
    End Sub

    Private Sub RaiseClickEvent()
        Dim realPosition = Mouse.GetPosition(Me.pageFrame)
        Dim pagePosition = New Point(
            Me.PageSize.Width * realPosition.X / Me.pageFrame.RenderSize.Width,
            Me.PageSize.Height * realPosition.Y / Me.pageFrame.RenderSize.Height)

        RaiseEvent Click(Me, New NavigationClickEventArgs With {.Position = pagePosition})
    End Sub

    Private Sub pageFrame_MouseMove(sender As Object, e As MouseEventArgs)
        If e.LeftButton = MouseButtonState.Pressed Then
            RaiseClickEvent()
        End If
    End Sub

    Public Sub SetPreviewImage(bitmap As WriteableBitmap)
        Me.previewImage.Source = bitmap
    End Sub

End Class

Public Class NavigationClickEventArgs
    Inherits EventArgs

    Public Property Position As Point
End Class
