Imports System.Windows.Threading

Public Class Layer

    Private Sub refreshButton_Click(sender As Object, e As RoutedEventArgs)
        Me.ExecuteRefresh()

    End Sub

    Private Sub DoEvents()
        Dim frame As New DispatcherFrame()
        Dispatcher.CurrentDispatcher.BeginInvoke(
            DispatcherPriority.Background,
            Function(o)
                CType(o, DispatcherFrame).Continue = False
                Return Nothing
            End Function,
            frame
        )
        Dispatcher.PushFrame(frame)

    End Sub


    Private Sub ExecuteRefresh()

        Me.progrressBar.Visibility = Visibility.Visible
        Me.layers.Visibility = Visibility.Collapsed
        Me.DoEvents()

        Dim e2 = New TempEventArgs With {.DoEvents = Sub() Me.DoEvents()}

        RaiseEvent Refresh(Me, e2)

        Me.DoEvents()

        Dim keywords =
            (From s In Me.SearchKeywordBox.Text.Split(" ")
             Let keyword = s.Trim().ToLower()
             Where Not String.IsNullOrWhiteSpace(keyword)
             Select keyword).ToList()

        Me.clearButton.Visibility = If(keywords.Count > 0, Visibility.Visible, Visibility.Collapsed)
        Me.DoEvents()

        Dim filter As Action(Of IEnumerable(Of LayerTreeItem)) =
            Sub(items)
                For Each item In items
                    If keywords.Count = 0 Then
                        item.IsVisible = True
                    Else
                        item.IsVisible = (From s In keywords Where item.Text.ToLower().Contains(s)).Count = keywords.Count
                    End If

                    If item.IsVisible AndAlso item.Parent IsNot Nothing Then item.Parent.IsVisible = True
                    filter(item.Children)
                Next
            End Sub

        filter(e2.Items)

        Me.DoEvents()

        Me.layers.ItemsSource = From item In e2.Items Where item.IsVisible

        Me.progrressBar.Visibility = Visibility.Collapsed
        Me.layers.Visibility = Visibility.Visible
    End Sub

    Public Event Refresh As EventHandler(Of TempEventArgs)

    Private Sub layers_SelectedItemChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Object))

        RaiseEvent SelectedItemChanged(Me, New ItemEventArgs With {.Item = layers.SelectedItem})

    End Sub

    Public Event SelectedItemChanged As EventHandler(Of ItemEventArgs)

    Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)
        Me.ExecuteRefresh()
    End Sub

    Private Sub SearchKeywordBox_KeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Enter Then
            Me.ExecuteRefresh()

        ElseIf e.Key = Key.Escape Then
            Me.ExecuteClear()
        End If

    End Sub

    Private Sub clearButton_Click(sender As Object, e As RoutedEventArgs)
        Me.ExecuteClear()
    End Sub

    Private Sub ExecuteClear()
        Me.SearchKeywordBox.Text = String.Empty
        Me.ExecuteRefresh()
    End Sub
End Class

Public Class TempEventArgs
    Inherits EventArgs

    Public Property Items As IEnumerable(Of LayerTreeItem)
    Public Property DoEvents As Action
End Class

Public Class LayerTreeItem

    Public Property Parent As LayerTreeItem
    Public Property Children As New List(Of LayerTreeItem)
    Public Property IsVisible As Boolean

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