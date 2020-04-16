Imports System.Collections.ObjectModel

Public Class Layer2

#Region " Properties "

    Private _Items As ObservableCollection(Of LayerTreeItem2)

    Public Property Items As ObservableCollection(Of LayerTreeItem2)
        Get
            Return _Items
        End Get
        Set
            _Items = Value
            Me.layers.ItemsSource = Me.Items

        End Set
    End Property


#End Region

    Private doingFilterByKeyword As Boolean

    Private Sub FilterByKeyword()
        Me.doingFilterByKeyword = True

        Dim keywords =
            (From s In Me.SearchKeywordBox.Text.Split(" ")
             Let keyword = s.Trim().ToLower()
             Where Not String.IsNullOrWhiteSpace(keyword)
             Select keyword).ToList()

        Dim updateViewFilter As Action(Of Predicate(Of Object), IEnumerable(Of LayerTreeItem2)) =
                Sub(f, items)
                    Dim view = CollectionViewSource.GetDefaultView(items)
                    view.Filter = f

                    For Each a In (From b In items Select b.Children)
                        updateViewFilter(f, a)
                    Next
                End Sub

        If keywords.Count() > 0 Then

            Dim updateParentIsVisible As Action(Of LayerTreeItem2) =
                Sub(parent)
                    parent.IsVisibleByFilter = True
                    parent.IsExpanded = True
                    If parent.Parent IsNot Nothing Then
                        updateParentIsVisible(parent.Parent)
                    End If
                End Sub

            Dim filter As Action(Of IEnumerable(Of LayerTreeItem2)) =
                Sub(items)
                    If items Is Nothing Then Return

                    For Each item In items
                        item.IsVisibleByFilter = (From s In keywords Where item.Text.ToLower().Contains(s)).Count = keywords.Count

                        If item.IsVisibleByFilter AndAlso item.Parent IsNot Nothing Then
                            updateParentIsVisible(item.Parent)
                        End If
                        filter(item.Children)
                    Next
                End Sub
            filter(Me.Items)

            updateViewFilter(Function(item As LayerTreeItem2) item.IsVisibleByFilter, Me.Items)
            Me.clearButton.Visibility = Visibility.Visible
        Else
            updateViewFilter(Function(item) True, Me.Items)
            Me.clearButton.Visibility = Visibility.Collapsed
        End If

        Me.doingFilterByKeyword = False

    End Sub

    Private Sub ClearFilterKeyword()
        Me.SearchKeywordBox.Text = String.Empty
        Me.FilterByKeyword()
    End Sub

#Region " Event Handlers "

    Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub clearButton_Click(sender As Object, e As RoutedEventArgs)
        Me.ClearFilterKeyword()
    End Sub

    Private Sub refreshButton_Click(sender As Object, e As RoutedEventArgs)
        Me.FilterByKeyword()
    End Sub

    Private Sub SearchKeywordBox_KeyDown(sender As Object, e As KeyEventArgs)

        If e.Key = Key.Enter Then
            Me.FilterByKeyword()

        ElseIf e.Key = Key.Escape Then
            Me.ClearFilterKeyword()
        End If
    End Sub


    Private Sub HideButton_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub ShowButton_Click(sender As Object, e As RoutedEventArgs)

    End Sub


    Private Sub TreeViewItem_MouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs)

        If Not Me.doingFilterByKeyword Then

            Dim item As TreeViewItem = sender
            Dim newItem As LayerTreeItem2 = item.DataContext

            Dim controlKey As Boolean = CBool((Keyboard.Modifiers And ModifierKeys.Control) = ModifierKeys.Control)
            If Not controlKey Then
                Dim updateObjectIsSelected As Action(Of IEnumerable(Of LayerTreeItem2)) =
                Sub(items)
                    For Each c As LayerTreeItem2 In items
                        If c IsNot newItem Then
                            c.ObjectIsSelected = False
                        End If
                        updateObjectIsSelected(c.Children)
                    Next
                End Sub

                updateObjectIsSelected(Me.Items)
            End If

            newItem.ObjectIsSelected = True
            e.Handled = True
        End If
    End Sub

#End Region

End Class


Public Class LayerTreeItem2
    Inherits BindableBase

    Private _IsExpanded As Boolean
    Private _Filtered As Boolean
    Private _IsMacthed As Boolean
    Private _VisibilityByIsMatched As Visibility
    Private _ObjectIsSelected As Boolean
    Public Property Parent As LayerTreeItem2

    Public ReadOnly Property Own As LayerTreeItem2
        Get
            Return Me
        End Get
    End Property

    Public Property Children As New ObservableCollection(Of LayerTreeItem2)

    Public Property Text As String

    Public Property IsExpanded As Boolean
        Get
            Return _IsExpanded
        End Get
        Set
            Me.SetProperty(_IsExpanded, Value)
        End Set
    End Property

    Friend Property IsVisibleByFilter As Boolean

    Public Property ObjectIsSelected As Boolean
        Get
            Return _ObjectIsSelected
        End Get
        Set
            Me.SetProperty(_ObjectIsSelected, Value)
        End Set
    End Property
End Class