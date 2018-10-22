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

    Public Sub Update()
        Me.ExecuteRefresh()
    End Sub

    Public Sub RefreshSelection()
        Dim e2 = New TempEventArgs With {.DoEvents = Sub() Me.DoEvents()}
        'e2.Items = Me.allItems
        e2.Items = Me.layers.ItemsSource
        RaiseEvent SelectionChanged(Me, e2)
    End Sub

    Private allItems As IEnumerable(Of LayerTreeItem)

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

        Dim newSelected As New List(Of LayerTreeItem)

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

                    If item.IsVisible AndAlso item.IsSelected Then newSelected.Add(item)
                Next
            End Sub

        filter(e2.Items)

        Me.DoEvents()

        Me.layers.ItemsSource = From item In e2.Items Where item.IsVisible
        Me.selected = newSelected

        Me.progrressBar.Visibility = Visibility.Collapsed
        Me.layers.Visibility = Visibility.Visible
    End Sub

    Private selected As List(Of LayerTreeItem)

    Public Event SelectionChanged As EventHandler(Of TempEventArgs)
    Public Event Refresh As EventHandler(Of TempEventArgs)

    Private Sub layers_SelectedItemChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Object))

        'RaiseEvent SelectedItemChanged(Me, New ItemEventArgs With {.Item = layers.SelectedItem})

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

    Private Sub TextBlock_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Dim t As TextBlock = sender
        Dim item As LayerTreeItem = t.Tag

        Dim controlKey As Boolean = CBool((Keyboard.Modifiers And ModifierKeys.Control) = ModifierKeys.Control)
        Dim mustReplaceSelection As Boolean = Not controlKey

        If mustReplaceSelection Then
            For Each s As LayerTreeItem In Me.selected
                s.IsSelected = False
            Next
            Me.selected.Clear()

        End If
        item.IsSelected = True
        Me.selected.Add(item)


        RaiseEvent SelectedItemChanged(Me, New ItemEventArgs With {.Item = item, .MustReplaceSelection = mustReplaceSelection})
    End Sub

End Class

Public Class TempEventArgs
    Inherits EventArgs

    Public Property Items As IEnumerable(Of LayerTreeItem)
    Public Property DoEvents As Action
End Class

Public Class LayerTreeItem
    Inherits BindableBase

    Public Property Parent As LayerTreeItem
    Public Property Children As New List(Of LayerTreeItem)
    Public Property IsVisible As Boolean

    Private valueOfIsSelected As Boolean
    Public Property IsSelected As Boolean
        Get
            Return Me.valueOfIsSelected
        End Get
        Set(value As Boolean)
            Me.SetProperty(Of Boolean)(Me.valueOfIsSelected, value)
            Me.SelectedIsVisibile = If(Me.IsSelected, Visibility.Visible, Visibility.Collapsed)
        End Set
    End Property

    Private valueOfSelectedIsVisibile As Visibility = Visibility.Collapsed
    Public Property SelectedIsVisibile As Visibility
        Get
            Return Me.valueOfSelectedIsVisibile
        End Get
        Set(value As Visibility)
            Me.SetProperty(Of Visibility)(Me.valueOfSelectedIsVisibile, value)
        End Set
    End Property


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

    Public ReadOnly Property Own As LayerTreeItem
        Get
            Return Me
        End Get
    End Property

End Class


Public Class ItemEventArgs
    Inherits EventArgs

    Public Property Item As LayerTreeItem
    Public Property MustReplaceSelection As Boolean

End Class