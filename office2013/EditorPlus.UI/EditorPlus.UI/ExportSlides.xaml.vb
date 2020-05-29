Public Class ExportSlides
    Private Sub SaveFilesButton_Click(sender As Object, e As RoutedEventArgs)
        RaiseEvent SaveFilesButtonClick(Me, EventArgs.Empty)
    End Sub

    Public Event SaveFilesButtonClick As EventHandler(Of EventArgs)
End Class

Public Class ExportSlidesModel
    Inherits BindableBase


    Public Property Theme As Theme


    Private _TargetIsAll As Boolean = True

    Property TargetIsAll As Boolean
        Get
            Return _TargetIsAll
        End Get
        Set
            Me.SetProperty(_TargetIsAll, Value)
        End Set
    End Property

    Private _TargetIsWithoutHidden As Boolean

    Property TargetIsWithoutHidden As Boolean
        Get
            Return _TargetIsWithoutHidden
        End Get
        Set
            Me.SetProperty(_TargetIsWithoutHidden, Value)
        End Set
    End Property

    Private _TargetIsSelection As Boolean

    Property TargetIsSelection As Boolean
        Get
            Return _TargetIsSelection
        End Get
        Set
            Me.SetProperty(_TargetIsSelection, Value)
        End Set
    End Property


    Private _FileNameIsSlideNumber As Boolean = True

    Property FileNameIsSlideNumber As Boolean
        Get
            Return _FileNameIsSlideNumber
        End Get
        Set
            Me.SetProperty(_FileNameIsSlideNumber, Value)
        End Set
    End Property

    Private _FileNameIsSlideName As Boolean

    Property FileNameIsSlideName As Boolean
        Get
            Return _FileNameIsSlideName
        End Get
        Set
            Me.SetProperty(_FileNameIsSlideName, Value)
        End Set
    End Property

    Private _SaveSlideImage As Boolean = True

    Property SaveSlideImage As Boolean
        Get
            Return _SaveSlideImage
        End Get
        Set
            Me.SetProperty(_SaveSlideImage, Value)
        End Set
    End Property

    Private _Width As Integer = 1280

    Property Width As Integer
        Get
            Return _Width
        End Get
        Set
            Me.SetProperty(_Width, Value)
        End Set
    End Property

    Private _SaveNotes As Boolean = True

    Property SaveNotes As Boolean
        Get
            Return _SaveNotes
        End Get
        Set
            Me.SetProperty(_SaveNotes, Value)
        End Set
    End Property


End Class
