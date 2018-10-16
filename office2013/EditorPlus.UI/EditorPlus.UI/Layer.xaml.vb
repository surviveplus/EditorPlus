Public Class Layer

    Private Sub refreshButton_Click(sender As Object, e As RoutedEventArgs)


        Dim e2 = New TempEventArgs
        RaiseEvent Refresh(Me, e2)

        Me.layers.ItemsSource = e2.Items

    End Sub

    Public Event Refresh As EventHandler(Of TempEventArgs)

    Private Sub layers_SelectedItemChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Object))

        RaiseEvent SelectedItemChanged(Me, New ItemEventArgs With {.Item = layers.SelectedItem})

    End Sub

    Public Event SelectedItemChanged As EventHandler(Of ItemEventArgs)

End Class

Public Class TempEventArgs
    Inherits EventArgs

    Public Property Items As IEnumerable(Of LayerTreeItem)

End Class

Public Class LayerTreeItem

    Public Property Parent As LayerTreeItem
    Public Property Children As New List(Of LayerTreeItem)

    ''' <summary>
    ''' Initializes a new instance of the class.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the class.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(parent As LayerTreeItem)
        Me.Parent = parent
    End Sub

    Public Property Text As String

    Public Property Shape As Object

End Class


Public Class ItemEventArgs
    Inherits EventArgs

    Public Property Item As LayerTreeItem

End Class